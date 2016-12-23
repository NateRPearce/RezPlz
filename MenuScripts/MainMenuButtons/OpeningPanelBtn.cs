using UnityEngine;
using System.Collections;

public class OpeningPanelBtn : MonoBehaviour {

    NewMainMenuScript NMMS;
    LvlSelectBook LSB;

	void Start () {
        NMMS = GetComponentInParent<NewMainMenuScript>();
        LSB = NMMS.LSB;
	}
	
	public void get_SwitchPanels()
    {
        NMMS.SwitchPanels();
    }

    public void get_Back()
    {
        NMMS.Back();
    }

    public void Get_Sound()
    {
        NMMS.PlaySelectSound();
    }

    public void get_GoToLevelSelect()
    {
        NMMS.bookGraphics.SetActive(true);
        LSB.BringUpBook();
        NMMS.NumOfPlayersPanel.SetActive(false);
    }

    public void get_LoadFromMenu()
    {
        NMMS.LoadFromMenu();
    }

    public void get_setNumOfPlayers(int numOfPlayers)
    {
        NMMS.setNumOfPlayers(numOfPlayers);
    }

    public void QuitGame()
    {
        NMMS.Quit();
    }
}
