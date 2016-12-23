using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NewAbilityStone : NewAbilityObject {

	public enum abilities{StoneSkin,DragonWings,Blink,Absorb,SpiritAnimal};
	public abilities ability;
	[HideInInspector]

	public Collider2D col;
	public Light L;

    public SpriteRenderer[] crystalSR;
    public GameObject abilityCanvas;
    AbilityCrystalMenu ACM;
    int dialogueNum=0;
    public string[] dialogue=new string[0];

    void Awake()
    {
        crystalSR = GetComponentsInChildren<SpriteRenderer>();
        abilityCanvas = GameObject.FindGameObjectWithTag("AbilityCanvas");
        ACM = abilityCanvas.GetComponent<AbilityCrystalMenu>();
    }

    void Start () {
		abilityName = ability.ToString();
		//source = GetComponent<AudioSource> ();
		col = GetComponent<Collider2D> ();
		L = GetComponent<Light> ();
        FindGM();
    }
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetButtonUp(GM.PM.PC1.jumpbtn) || Input.GetButtonUp(GM.PM.PC2.jumpbtn))&&ACM.ACCanvas.enabled)
        {
            if (dialogueNum >= dialogue.Length)
            {
                dialogueNum = 0;
                ACM.ACCanvas.enabled = false;
                GM.PM.eventHappening = false;
                GM.PM.controlsLocked = false;
            }else
            {
                UpdateTutorial();
                dialogueNum += 1;
                if (dialogueNum >= dialogue.Length)
                {
                    dialogueNum = 0;
                    ACM.ACCanvas.enabled = false;
                    GM.PM.eventHappening = false;
                    GM.PM.controlsLocked = false;
                }
            }
        }

        if (GM.PM.eventHappening)
        {
            return;
        }

        if (triggered && col.enabled) {
            GM.PM.eventHappening = true;
            GM.PM.controlsLocked = true;
            GM.PM.PC1.rbody.velocity = new Vector2(0, 0);
            GM.PM.PC2.rbody.velocity = new Vector2(0, 0);
            ACM.ACCanvas.enabled=true;
            foreach (SpriteRenderer sr in crystalSR)
            {
                sr.enabled = false;
            }
            col.enabled=false;
			L.enabled=false;
            UpdateTutorial();
        }
	}

    public void UpdateTutorial()
    {
            ACM.dialogueText.text = dialogue[dialogueNum].Replace("  ", "\n");
    }
}
