using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MaskMenu : SetButton {



    [FMODUnity.EventRef]
    string selectSound = "event:/Menu_Scroll";
    FMOD.Studio.EventInstance selectEV;



    public GameObject maskPanel;
	public Image maskImage;
	public Image maskShadow;
	public int currentPlayer;
	public GameObject yesBtn;
	public Canvas mmCanvas;
	public SecretMask currentMask;

    void Awake()
    {
        selectEV = FMODUnity.RuntimeManager.CreateInstance(selectSound);
    }

	void Start()
	{
		FindGM ();
		maskPanel = this.gameObject;
		ES = EventSystem.current;
		mmCanvas = GetComponent<Canvas> ();
		mmCanvas.enabled = false;
	}

	public void YES()
	{
		if (!mmCanvas.enabled) {
			return;
		}
        selectEV.start();
		if (currentPlayer == 1) {
			if(!GM.PM.PC1.mask.enabled){
				GM.PM.PC1.mask.enabled=true;
			}
			GM.PM.PC1.mask.sprite=maskImage.sprite;
			GM.addNewMask(1,maskImage.sprite);
		} else {
			if(!GM.PM.PC2.mask.enabled){
				GM.PM.PC2.mask.enabled=true;
			}
			GM.PM.PC2.mask.sprite=maskImage.sprite;
			GM.addNewMask(2,maskImage.sprite);
		}
        GM.masksCollected += 1;
        Time.timeScale = 1;
		mmCanvas.enabled = false;
	}

    public void Store()
    {
        if (!mmCanvas.enabled)
        {
            return;
        }
        selectEV.start();
        if (currentPlayer == 1)
        {
            GM.addNewMask(1, maskImage.sprite);
        }
        else
        {
            GM.addNewMask(2, maskImage.sprite);
        }
        GM.masksCollected += 1;
        Time.timeScale = 1;
        mmCanvas.enabled = false;
    }

    public void No()
	{
		if (!mmCanvas.enabled) {
			return;
		}
        selectEV.start();
        Time.timeScale = 1;
		mmCanvas.enabled = false;
		StartCoroutine ("ResetMask");
	}

	IEnumerator ResetMask()
    {
		currentMask.maskSR.enabled = true;
		yield return new WaitForSeconds (2);
		currentMask.col.enabled = true;
	}
}
