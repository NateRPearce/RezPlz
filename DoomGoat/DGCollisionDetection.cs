using UnityEngine;
using System.Collections;

public class DGCollisionDetection : MonoBehaviour {

    Animator anim;
    GibCorpse GB;
	public DoomGoatScript DGS;
	public WeaknessesScript WS;
    public DG_Sounds DGSounds;

	void Start(){
		DGS = GetComponentInParent<DoomGoatScript> ();
		WS = GetComponentInParent<WeaknessesScript> ();
        DGSounds = GetComponentInParent<DG_Sounds>();
        anim = GetComponentInParent<Animator>();
        GB=GetComponentInParent<GibCorpse>();
	}

	void HitBy (string OBJ)
	{
		for(int i=0;i<WS.weakness.Length;i++){
			if (OBJ == WS.weaknessArray[i]&&!DGS.getDead()) {
                if (OBJ == Tags.explosion)
                {
                    anim.SetTrigger("Exploded");
                    GB.createGiblets();
                    DGS.setDead(true);
                }
                DGS.setDead(true);
			}
		}
	}


	void OnTriggerEnter2D(Collider2D other)
	{
        if (DGS.getDead()&&other.tag=="AttackCollider")
        {
            DGSounds.Play_Hit_Sound();
            return;
        }
		if (other.tag != "Ground") {
			HitBy (other.tag);
		}
	}
}
