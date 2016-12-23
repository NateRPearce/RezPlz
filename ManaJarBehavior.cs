using UnityEngine;
using System.Collections;

public class ManaJarBehavior : GameStateFunctions {


    [FMODUnity.EventRef]
    string aquiredSound = "event:/Player/General_Player_LazarusStone_Acquire";
    public FMOD.Studio.EventInstance aquiredEV;

    ParticleSystem PS;
    ParticleSystem.EmissionModule PE;
    public bool used;
    public Transform target;
    int targetPlayer;
    public Transform shadow;

    Animator anim;

	void Start () {
        aquiredEV = FMODUnity.RuntimeManager.CreateInstance(aquiredSound);
		FindGM ();
		anim = GetComponent<Animator> ();
        PS = GetComponentInChildren<ParticleSystem>();
        PE = PS.emission;
	}

    void FixedUpdate()
    {
        if (used)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y + 3, -20),2);
            if (Mathf.Abs(transform.position.x - target.position.x) < 15)
            {
                anim.SetTrigger("Used");
                PE.rate = 0;
            }
            if (Mathf.Abs(transform.position.x - target.position.x) < 1)
            {
                PE.rate = 0;
            }
            if (transform.position == new Vector3(target.position.x, target.position.y + 3, -20))
            {
                GM.MB[targetPlayer].manaCrystalAnims[GM.MB[targetPlayer].targetManaSlot].SetTrigger("Restore");
            }
        }
    }

	void OnTriggerEnter2D(Collider2D other) {
		if(other.name=="Player1"){
			if(GM.MB[0].mana<GM.maxMana){
				Use(1);
			}
		}else if(other.name=="Player2"){
			if(GM.MB[1].mana<GM.maxMana){
				Use(2);
			}
		}
	}


	void Use(int player){
        targetPlayer = player - 1;
        aquiredEV.start();
		GM.GainMana(player);
        //anim.SetTrigger ("Used");
        used = true;
        target = GM.MB[targetPlayer].manaImgs[GM.MB[targetPlayer].targetManaSlot + 1];
        transform.GetComponent<Collider2D>().enabled = false;
        PE.rate = 100;
        shadow.gameObject.SetActive(false);
    }


	public void DestroyMana(){
		Destroy(gameObject);
	}
	
}
