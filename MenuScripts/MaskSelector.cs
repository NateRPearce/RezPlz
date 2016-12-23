using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MaskSelector : SetButton {

	public Image [] masks = new Image[11];
	public GameObject pauseMenu;
	PauseMenuController PMC;
	public GameObject noMask;

	void Start () {
		FindGM ();
		ES = EventSystem.current;
		PMC = pauseMenu.GetComponent<PauseMenuController> ();
	}

	public void EquipMask(){
		if (!gameObject.activeSelf) {
			return;
		}

		if (PMC.pausedPlayer == 1) {
			if(!GM.PM.PC1.mask.enabled){
				GM.PM.PC1.mask.enabled=true;
			}

			Image[] buttonSprites = new Image[2];
			buttonSprites=ES.currentSelectedGameObject.GetComponentsInChildren<Image>();
			GM.PM.PC1.mask.sprite=buttonSprites[1].sprite;
		} else if (PMC.pausedPlayer == 2) {
			if(!GM.PM.PC2.mask.enabled){
				GM.PM.PC2.mask.enabled=true;
			}

			Image[] buttonSprites = new Image[2];
			buttonSprites=ES.currentSelectedGameObject.GetComponentsInChildren<Image>();
			GM.PM.PC2.mask.sprite=buttonSprites[1].sprite;
		}
	}
}
