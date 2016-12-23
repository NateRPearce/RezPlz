using UnityEngine;
using System.Collections;

public class NewAbilityObject : GameStateFunctions {



	public string abilityName;
	[HideInInspector]
	public bool dontDestroy;
	public AudioClip PickUpSound;
	public AudioSource source;
	public bool triggered;

	void Start(){
		//source = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<PlayerControls> () != null) {
			PlayerControls PC =other.GetComponent<PlayerControls> ();
			for(int i=0;i<PC.AB.CrystalList.Length;i++){
				//source.PlayOneShot(PickUpSound,0.3f);
				if(PC.AB.CrystalList[i].name==abilityName){
					PC.AB.CrystalList[i].unlocked=true;
					PC.ablText.text = abilityName;
					//PC.ablTextAlpha=1;
					//PC.ablText.color = new Color (1, 1, 1, 1);
					if (GetComponentInParent<PlayerControls> ()) {
						PlayerControls myPC = GetComponentInParent<PlayerControls> ();
						myPC.RemoveCrystal(myPC.currentAbility);
					}
					PC.AB.CrystalSelector.transform.position = PC.AB.CrystalList [i].SR.transform.position;
					PC.ChangeAbilityOverride(i);
				}
			}
			triggered=true;
		}
	}

}
