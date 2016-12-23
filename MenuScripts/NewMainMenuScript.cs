using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class NewMainMenuScript : SetButton {


    public GameObject OpeningPanel;
	public GameObject NumOfPlayersPanel;
	public GameObject newGameBtn;
    public GameObject onePlayerBtn;
    public GameObject demoLvlBtn;
    public GameObject bookGraphics;
    AudioSource Source;
    public AudioClip Scroll;
    public AudioClip Select;
    GameObject button;
    GameObject tempButton;
    public LvlSelectBook LSB;

    // Use this for initialization
    void Start () {
		FindGM ();
        Source = GetComponent<AudioSource>();
		SetCurrentButton (newGameBtn);
		NumOfPlayersPanel.SetActive (false);
        button = ES.currentSelectedGameObject;
    }

   void Update()
    {
		if (ES.currentSelectedGameObject == null) {
			SetCurrentButton (newGameBtn);
			return;
		}
       tempButton= ES.currentSelectedGameObject;
        if (button != tempButton)
        {
            Source.PlayOneShot(Scroll,0.1f);
        }
        button = tempButton;
    }

    public void PlaySelectSound()
    {
        Source.PlayOneShot(Select, 0.1f);
    }

	public void setNumOfPlayers(int numOfPlayers){
		GM.numOfPlayers = numOfPlayers;
		GM.setControls ();
	}


	public void SwitchPanels(){
		OpeningPanel.SetActive (false);
		NumOfPlayersPanel.SetActive (true);
        SetCurrentButton(onePlayerBtn);
    }
	public void Back(){
		OpeningPanel.SetActive (true);
		NumOfPlayersPanel.SetActive (false);
		SetCurrentButton (newGameBtn);
	}
	public void LoadFromMenu(){
		//GM.LoadGame ();
	}

    public void ReturnToMenu()
    {
        GM.mainMenuPageNumber = 0;
        LSB.LowerDownBook();
        LSB.HideGraphics();
        bookGraphics.SetActive(false);
        StartCoroutine("WaitAndReturn");
    }

    IEnumerator WaitAndReturn()
    {
        yield return new WaitForSeconds(2);
        NumOfPlayersPanel.SetActive(true);
        SetCurrentButton(onePlayerBtn);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
		Application.Quit();
        #endif
    }
}
