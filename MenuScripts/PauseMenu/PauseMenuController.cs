using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseMenuController : SetButton {

    public FMOD.Studio.Bus soundFXBus;
    string sfxbusPath = "bus:/InGameSounds";

    public FMOD.Studio.Bus musicBus;    
    string mbusPath = "bus:/Music";

    Canvas PMCanvas;
	public Text numOfPlayersT;
	public GameObject NOP;
	public GameObject pausePanel;
	public GameObject controlsPanel;
    public GameObject soundPanel;
    public GameObject maskSelector;
    ParticleController PAC;
    //public GameObject xboxControls;
	public int pausedPlayer;
	public GameObject maskMenu;
    public GameObject abilityMenu;
    public Sprite blankSprite;

    SoundControls SC;
	MaskSelector MS;

	void Start(){
        soundFXBus = FMODUnity.RuntimeManager.GetBus(sfxbusPath);
        musicBus = FMODUnity.RuntimeManager.GetBus(mbusPath);
        Time.timeScale = 1;
		FindGM ();
		FindCBS ();
        if (GetComponentInParent<ParticleController>() != null)
        {
            PAC = GetComponentInParent<ParticleController>();
        }
		numOfPlayersT.text="Number of Players "+GM.numOfPlayers;
		PMCanvas = GetComponent<Canvas> ();
		NOP = numOfPlayersT.gameObject;
		PMCanvas.enabled = false;
		MS = maskSelector.GetComponent<MaskSelector> ();
		ES = EventSystem.current;
        SC = soundPanel.GetComponent<SoundControls>();
        musicBus.setFaderLevel(GM.musicVol);
        soundFXBus.setFaderLevel(GM.SFXVol);
        if (GameObject.FindGameObjectWithTag ("Mask_Canvas") != null) {
			maskMenu = GameObject.FindGameObjectWithTag ("Mask_Canvas");
		}
        if (GameObject.FindGameObjectWithTag("AbilityCanvas") != null)
        {
            abilityMenu = GameObject.FindGameObjectWithTag("AbilityCanvas");
        }
    }


	void Update () {
		if (GameObject.FindGameObjectWithTag ("Mask_Canvas") != null) {
			if (maskMenu.GetComponent<Canvas> ().enabled) {
				return;
			}
		}
        if (GameObject.FindGameObjectWithTag("AbilityCanvas") != null)
        {
            if (abilityMenu.GetComponent<Canvas>().enabled)
            {
                return;
            }
        }
        if (Input.GetButtonDown (GM.pause[0]) || Input.GetButtonDown (GM.pause[1])) {
			if(!PMCanvas.enabled||!pausePanel.activeSelf){
				SetCurrentButton (NOP);
			}else if(controlsPanel.activeSelf||maskSelector.activeSelf||soundPanel.activeSelf){
				ReturnToPauseMenu();
			}
			Pause();
			if(PMCanvas.enabled){
                if (GM.numOfPlayers==1){
					if(GM.PM.PC1.ControlsEnabled){
						pausedPlayer=1;
					}else{
						pausedPlayer=2;
					}
				}else{
					if(Input.GetButtonDown(GM.pause[0])){
						pausedPlayer=1;
					}
					if(Input.GetButtonDown (GM.pause[1])) {
						pausedPlayer=2;
					}
				}
			}
		}
		if(PMCanvas.enabled&&(Input.GetButtonDown(GM.PM.PC1.grabbtn)||Input.GetButtonDown(GM.PM.PC2.grabbtn))){
			if(controlsPanel.activeSelf||maskSelector.activeSelf)
            {
				ReturnToPauseMenu();
			}else if(!soundPanel.activeSelf){
			Pause();
			}
		}

	}

	public void Pause(){
        if (PMCanvas.enabled)
        {
            ReturnToPauseMenu();
        }
		PMCanvas.enabled=!PMCanvas.enabled;	
		if (PMCanvas.enabled) {
			Time.timeScale = 0;
            musicBus.setFaderLevel(0.1f);
            soundFXBus.setPaused(true);
		} else{
            soundFXBus.setPaused(false);
            Time.timeScale = 1;
		}
	}

	public void ResetPlayerPos(int playerNum){
		if (!PMCanvas.enabled||!pausePanel.activeSelf) {
			return;
		}
		if (playerNum == 1) {
			GM.PM.Player1.GetComponent<Renderer>().enabled=false;
			GM.PM.Player1.position=new Vector3(GM.CheckPointPos.x+5,GM.CheckPointPos.y,GM.PM.Player1.position.z);	
			//GM.PM.PC1.rezing=true;
		}else{
			GM.PM.Player2.GetComponent<Renderer>().enabled=false;
			GM.PM.Player2.position=new Vector3(GM.CheckPointPos.x-5,GM.CheckPointPos.y,GM.PM.Player2.position.z);	
			//GM.PM.PC2.rezing=true;
		}
		Pause();
	}
    
    void OnDestroy()
    {
        Debug.Log("Just F@#king work!!!");
        
        /*
        GM.musicVol = SC.musicSlider.value;
        GM.SFXVol = SC.sfxSlider.value;
        musicBus.setFaderLevel(GM.musicVol);
        soundFXBus.setFaderLevel(GM.SFXVol);
        soundFXBus.setPaused(false);
        */
    }

	public void RestartLVL(){
		if (!PMCanvas.enabled||!pausePanel.activeSelf) {
			return;
		}
        GM.startAtBoss = false;
        SceneManager.LoadScene(GM.currentLevel+2);
	}

	public void MainMenu(){
		if (!PMCanvas.enabled||!pausePanel.activeSelf) {
			return;
		}
        GM.startAtBoss = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
	}
	
	public void ReturnToLvlSelect(){
		if (!PMCanvas.enabled||!pausePanel.activeSelf) {
			return;
		}
        GM.raceMode = false;
        GM.startAtBoss = false;
        Time.timeScale = 1;
		SceneManager.LoadScene(0);
	}

	public void PlayersSelect(){
		if (!PMCanvas.enabled||!pausePanel.activeSelf) {
			return;
		}

		if (GM.numOfPlayers == 1) {
			GM.numOfPlayers=2;	
			numOfPlayersT.text="Number of Players "+GM.numOfPlayers;
		}else{
			GM.numOfPlayers=1;
			numOfPlayersT.text="Number of Players "+GM.numOfPlayers;
		}
		GM.setControls ();
		GM.PM.PC1.CheckControls ();
		GM.PM.PC2.CheckControls ();
	}

    public void ShowSounds()
    {
        if (!PMCanvas.enabled || !pausePanel.activeSelf)
        {
            return;
        }
        pausePanel.SetActive(false);
        soundPanel.SetActive(true);
        SetCurrentButton(SC.musicVolBTN.gameObject);
    }

	public void ShowControls(){
		if (!PMCanvas.enabled||!pausePanel.activeSelf) {
			return;
		}
		pausePanel.SetActive (false);
		controlsPanel.SetActive (true);
       // SetCurrentButton(xboxControls);
    }


    public void ShowMasks(){
		if (!PMCanvas.enabled) {
			return;
		}

		if (pausedPlayer == 1) {
			for (int i=0; i<GM.p1MaskCollection.Length; i++) {
				if (GM.p1MaskCollection [i] != null) {
					MS.masks [i].sprite = GM.p1MaskCollection [i];
				}else{
					MS.masks [i].sprite = blankSprite;
				}
			}
		} else if (pausedPlayer == 2) {
			for (int i=0; i<GM.p2MaskCollection.Length; i++) {
				if (GM.p2MaskCollection [i] != null) {
					MS.masks [i].sprite = GM.p2MaskCollection [i];
				}else{
					MS.masks [i].sprite = blankSprite;
				}
			}
		}
		pausePanel.SetActive (false);
		maskSelector.SetActive (true);
		SetCurrentButton (MS.noMask);
	}

	public void ReturnToPauseMenu(){
        pausePanel.SetActive (true);
		controlsPanel.SetActive (false);
		maskSelector.SetActive (false);
        soundPanel.SetActive(false);
        SetCurrentButton(NOP);
    }
    public void DisablePAC()
    {
        if (PAC == null)
        {
            return;
        }
        PAC.qualityLvl = 2;
        PAC.DisableParticles();
    }
    public void Quit()
	{
		if (!PMCanvas.enabled) {
			return;
		}
		#if UNITY_EDITOR 
		EditorApplication.isPlaying = false;
		#else 
		Application.Quit();
		#endif
	}

}
