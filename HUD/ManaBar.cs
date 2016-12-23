using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ManaBar : GameStateFunctions
{
	public int mana;
    public int targetManaSlot;
	public Transform[] manaImgs = new Transform[10];
	public Transform[] Extensions = new Transform[5];
	public Animator[] manaCrystalAnims;



	void Start () {
		FindGM ();
		if (name == "ArcanManaBar") {
			GM.MB [1] = this;
			mana = GM.maxMana;
		} else if (name == "ZephManaBar") {		
			GM.MB [0] = this;
			mana = GM.maxMana;
		} else {
			GM.EidolonLifeBar=this;
			mana=100;
		}
		int hideNum;
		manaCrystalAnims = new Animator[manaImgs.Length];
		for(int i=0;i<manaImgs.Length;i++){
			manaCrystalAnims[i] = manaImgs[i].GetComponent<Animator> ();
		}
		if (name != "EidolonLifeBar") {
			hideNum = (int)(100 - GM.maxMana) / 10;
		} else {
			hideNum = 0;
		}
		if (hideNum > 5) {
			hideNum=5;
			UpdateManaBar(0);
		}
		HideExtensions(hideNum);
	}

    public void UpdateManaBar(int manaCost){
		if(mana==0&&manaCost>0){		
			return;
		}
		mana -= manaCost;
		if (mana < 100&&Extensions[0].gameObject.activeSelf) {
			manaCrystalAnims [9].SetBool ("Broken", true);
		} else if (mana == 100&&manaCost<0) {
			manaCrystalAnims[9].SetBool("Broken",false);
			//manaCrystalAnims [9].SetTrigger ("Restore");
            targetManaSlot = 9;

        }
		if (mana < 90&&Extensions[1].gameObject.activeSelf) {
			manaCrystalAnims[8].SetBool("Broken",true);
		}else if (mana == 90&&manaCost<0) {
			manaCrystalAnims[8].SetBool("Broken",false);
			//manaCrystalAnims [8].SetTrigger ("Restore");
            targetManaSlot = 8;
        }
		if (mana < 80&&Extensions[2].gameObject.activeSelf) {
			manaCrystalAnims[7].SetBool("Broken",true);
		}else if (mana == 80&&manaCost<0) {
			manaCrystalAnims[7].SetBool("Broken",false);
			//manaCrystalAnims [7].SetTrigger ("Restore");
            targetManaSlot = 7;
        }
		if (mana < 70&&Extensions[3].gameObject.activeSelf) {
			manaCrystalAnims[6].SetBool("Broken",true);
		}else if (mana == 70&&manaCost<0) {
			manaCrystalAnims[6].SetBool("Broken",false);
			//manaCrystalAnims [6].SetTrigger ("Restore");
            targetManaSlot = 6;
        }
		if (mana < 60&&Extensions[4].gameObject.activeSelf) {
			manaCrystalAnims[5].SetBool("Broken",true);
		}else if (mana == 60&&manaCost<0) {
			manaCrystalAnims[5].SetBool("Broken",false);
			//manaCrystalAnims [5].SetTrigger("Restore");
            targetManaSlot = 5;
        }
		if (mana < 50&& manaCrystalAnims[4].gameObject.activeSelf) {
			manaCrystalAnims[4].SetBool("Broken",true);
		}else if (mana == 50&&manaCost<0) {
			manaCrystalAnims[4].SetBool("Broken",false);
			//manaCrystalAnims [4].SetTrigger ("Restore");
            targetManaSlot = 4;
        }
		if (mana < 40 && manaCrystalAnims[3].gameObject.activeSelf) {
			manaCrystalAnims[3].SetBool("Broken",true);
		}else if (mana == 40&&manaCost<0) {
			manaCrystalAnims[3].SetBool("Broken",false);
			//manaCrystalAnims [3].SetTrigger ("Restore");
            targetManaSlot = 3;
        }
		if (mana < 30) {
			manaCrystalAnims[2].SetBool("Broken",true);
		}else if (mana == 30&&manaCost<0) {
			manaCrystalAnims[2].SetBool("Broken",false);
			//manaCrystalAnims [2].SetTrigger ("Restore");
            targetManaSlot = 2;
        }
		if (mana < 20) {
			manaCrystalAnims[1].SetBool("Broken",true);
		}else if (mana == 20&&manaCost<0) {
			manaCrystalAnims[1].SetBool("Broken",false);
			//manaCrystalAnims [1].SetTrigger ("Restore");
            targetManaSlot = 1;
        }
		if (mana < 10) {
			manaCrystalAnims[0].SetBool("Broken",true);
		}else if (mana == 10&&manaCost<0) {
			manaCrystalAnims[0].SetBool("Broken",false);
			//manaCrystalAnims [0].SetTrigger ("Restore");
            targetManaSlot = 0;
        }
		if (name == "EidolonLifeBar") {
			if (GM.EidolonLifeBar.mana == 0) {
				GM.r.restartLvl = true;
			}
		}
	}


	public void HideExtensions(int ext){
		for (int i=0; i<ext; i++) {
			Extensions[i].gameObject.SetActive(false);
		}
	}
}
