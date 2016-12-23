using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SecretMask : MonoBehaviour {

    [FMODUnity.EventRef]
    string acheivementSound = "event:/Bad Rez";
    FMOD.Studio.EventInstance acheivementEV;


    public SpriteRenderer maskSR;
	public GameObject maskMenu;
	MaskMenu MM;
	public Collider2D col;

	void Awake () 
	{
		maskSR = GetComponent<SpriteRenderer> ();
		maskMenu = GameObject.FindGameObjectWithTag ("Mask_Canvas");
		MM = maskMenu.GetComponent<MaskMenu> ();
		col = GetComponent<Collider2D> ();
        acheivementEV = FMODUnity.RuntimeManager.CreateInstance(acheivementSound);
    }
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Player1"||other.name == "Player2") {

            if (other.name == "Player1") 
			{
				MM.currentPlayer=1;
			}
			
			if (other.name == "Player2") 
			{
				MM.currentPlayer=2;
			}
            StartCoroutine("MenuPopUp");
		}
	}
    public IEnumerator MenuPopUp()
    {
        acheivementEV.start();
        yield return new WaitForSeconds(0.1f);
        MM.currentMask = this;
        MM.maskImage.sprite = maskSR.sprite;
        col.enabled = false;
        maskSR.enabled = false;
        MM.mmCanvas.enabled = true;
        MM.SetCurrentButton(MM.yesBtn);
        Time.timeScale = 0;
    }
}
