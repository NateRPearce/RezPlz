using UnityEngine;
using System.Collections;

public class AbilitiesBar : GameStateFunctions {

	public PlayerControls PC;
	public Transform targetPlayer;

	public Crystal MetalCrystal = new Crystal ();
	public Crystal NecroCrystal = new Crystal ();
	public Crystal ZooCrystal = new Crystal ();
	public Crystal CaveCrystal = new Crystal ();
	public Crystal	TimeCrystal0 = new Crystal ();
	public Crystal[] CrystalList = new Crystal[5];

	public SpriteRenderer ablBarSR;
	public SpriteRenderer CrystalSelector;

	public SpriteRenderer[] CrystalsSRs = new SpriteRenderer[5];

	int n = 90;
	public float ablAlphaMain;
	public float ablAlphaSub;
	public float selectDirect;
	// Use this for initialization
	void Start () {
			FindGM ();
			CreateCrystalList ();
			SetCrystalSRs ();
			HideAllAbilities ();
			PC = targetPlayer.GetComponent<PlayerControls> ();
		for (int i = 0; i<5; i++) {
				//one rad unit 0.575
				float r = 0.575f;
			float rads=n*Mathf.Deg2Rad;
				float xPos = r * Mathf.Cos (rads);
				float yPos = r * Mathf.Sin (rads);
				CrystalsSRs [i].transform.localPosition = new Vector2 (xPos, yPos);
				n += 72;
				if (n > 360) {
					n -= 360;
				}
			}
	}

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (targetPlayer.position.x, targetPlayer.position.y, transform.position.z);
	}

	public int SelectAbility(float dirX,float dirY, int selAbl){
		float rad=Mathf.Atan2 (dirY, dirX);
		float degree = rad*Mathf.Rad2Deg;
		if (degree < 0) {
			degree+=360;
		}
		//point 1
		if (degree > 54 && degree < 126) {
			//up
			selAbl = 0;
			//point 2
		}else if(degree > 126 && degree < 198) {
			selAbl = 1;
			//point 3
		}else if(degree > 198 && degree < 270) {
			selAbl = 2;
			//point 4
		}else if (degree > 270 && degree < 342) {
			selAbl = 3;
			//point 5
		}else if(degree > 342 && degree < 360||degree > 0 && degree < 54) {
			selAbl = 4;
		}

		CrystalSelector.transform.position = CrystalList [selAbl].SR.transform.position;
		if (targetPlayer.name == "Player1") {
			GM.UpdateHUD (selAbl, 1);
		} else {
				GM.UpdateHUD (selAbl, 2);
		}
		return selAbl;
	}
	public void HideAbilities(int sel){
		if (ablAlphaMain > 0) {
			ablAlphaMain-=Time.deltaTime/2;
		}
		if (ablAlphaSub > 0) {
			ablAlphaSub-=Time.deltaTime/2;
		}
		for (int i = 0; i<CrystalList.Length; i++) {
			if(CrystalList[i].unlocked){

			if(sel==i){

				CrystalList[i].SR.color=new Color(1,1,1,ablAlphaMain);
			}else{

				CrystalList[i].SR.color=new Color(1,1,1,ablAlphaSub);
			}
			}
		}
		CrystalSelector.color=new Color(1,1,1,ablAlphaSub);
		ablBarSR.color=new Color(1,1,1,ablAlphaSub);
	}
	public void DisplayAbilities(int sel){
		ablAlphaMain = 1;
		ablAlphaSub = 0.5f;
		for (int i = 0; i<CrystalList.Length; i++) {
			if(CrystalList[i].unlocked){
			if(sel==i){
			CrystalList[i].SR.color=new Color(1,1,1,ablAlphaMain);
			PC.ablText.text = CrystalList [sel].name;
			PC.ablText.color = new Color (1, 1, 1, ablAlphaMain);
			}else{
			CrystalList[i].SR.color=new Color(1,1,1,ablAlphaSub);
			}
			}
		}
		CrystalSelector.color=new Color(1,1,1,ablAlphaSub);
		ablBarSR.color=new Color(1,1,1,ablAlphaSub);
	}

	public void HideAllAbilities(){
		transform.position = new Vector3 (targetPlayer.position.x, targetPlayer.position.y, transform.position.z);
		foreach (Crystal Cr in CrystalList){
			Cr.SR.color = new Color (1, 1, 1, 0);
		}
		CrystalSelector.color=new Color(1,1,1,0);
		ablBarSR.color=new Color(1,1,1,0);
	}


	public void AttemptLoadCrystals(){
			for(int i= 0;i<5;i++){
			if (targetPlayer.name == "Player1") {
				CrystalList[i]=GM.SavedCrystals1[i];
			} else {
				CrystalList[i]=GM.SavedCrystals2[i];
			}
			}
	}
	public void SetCrystalSRs(){
		for(int i = 0;i<CrystalList.Length;i++){
			CrystalList[i].SR = CrystalsSRs[i];
		}
	}
	public void CreateCrystalList(){
		if (GM.SavedCrystals1[0] != null) {
			AttemptLoadCrystals();
			return;
		}
		MetalCrystal.name = "DragonWings";
		ZooCrystal.name = "SpiritAnimal";
		NecroCrystal.name = "Absorb";
		CaveCrystal.name = "StoneSkin";
		TimeCrystal0.name = "Blink";
		CrystalList[4] = NecroCrystal;
		CrystalList[3]=MetalCrystal;
		CrystalList[2]=ZooCrystal;
		CrystalList[1]=CaveCrystal;
		CrystalList[0]=TimeCrystal0;
		for(int i = 0;i<CrystalList.Length;i++){
			CrystalList[i].SR = CrystalsSRs[i];
			CrystalList[i].unlocked=false;
			CrystalList[i].enabled=false;
		}
	}

}

public class Crystal{
	public SpriteRenderer SR;
	public bool unlocked;
	public bool enabled;
	public string name;
}