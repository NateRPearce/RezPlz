using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SoundControls : SetButton {

    public Button musicVolBTN;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Button sfxBTN;
    public Button exitBTN;
    PauseMenuController PMC;
    bool noInput;

    void Start()
    {
        FindGM();
        musicSlider.value = GM.musicVol;
        sfxSlider.value = GM.SFXVol;
        ES = EventSystem.current;
        PMC = GetComponentInParent<PauseMenuController>();
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!PMC.soundPanel.activeSelf)
        {
            return;
        }

        if (ES.currentSelectedGameObject == musicVolBTN.gameObject)
        {
            if (Input.GetButtonDown(GM.jumpbtn[0]) || Input.GetButtonDown(GM.jumpbtn[1]))
            {
                SelectMusicVol();
                return;
            }
            if (Input.GetButtonDown(GM.grabbtn[0]) || Input.GetButtonDown(GM.grabbtn[1]))
            {
                SetMusicVol();
                SetSFXVol();
                Exit();
                return;
            }
        }
        else if (ES.currentSelectedGameObject == sfxBTN.gameObject)
        {
            if (Input.GetButtonDown(GM.jumpbtn[0]) || Input.GetButtonDown(GM.jumpbtn[1]))
            {
                SelectSFXVol();
                return;
            }
            if (Input.GetButtonDown(GM.grabbtn[0]) || Input.GetButtonDown(GM.grabbtn[1])){
                SetMusicVol();
                SetSFXVol();
                Exit();
                return;
            }

        }else if (ES.currentSelectedGameObject == exitBTN.gameObject)
        {
            if (Input.GetButtonDown(GM.grabbtn[0]) || Input.GetButtonDown(GM.grabbtn[1]))
            {
                SetMusicVol();
                SetSFXVol();
                Exit();
                return;
            }
        }



        if (ES.currentSelectedGameObject == musicSlider.gameObject)
        {
            if (Input.GetButtonDown(GM.jumpbtn[0]) || Input.GetButtonDown(GM.jumpbtn[1]) || Input.GetButtonDown(GM.grabbtn[0]) || Input.GetButtonDown(GM.grabbtn[1]))
            {
                SetMusicVol();
            }
        }else if(ES.currentSelectedGameObject == sfxSlider.gameObject)
        {
            if (Input.GetButtonDown(GM.jumpbtn[0]) || Input.GetButtonDown(GM.jumpbtn[1])|| Input.GetButtonDown(GM.grabbtn[0]) || Input.GetButtonDown(GM.grabbtn[1]))
            {
                SetSFXVol();
            }
        }
    }

    public void SelectMusicVol()
    {
        if (!PMC.soundPanel.activeSelf)
        {
            return;
        }
        Debug.Log("Clicked music vol button");
        musicVolBTN.interactable=false;
        sfxSlider.interactable=false;
        exitBTN.interactable = false;
        sfxBTN.interactable = false; ;
        musicSlider.interactable = true;
        SetCurrentButton(musicSlider.gameObject);
    }


    public void SetMusicVol()
    {
        if (!PMC.soundPanel.activeSelf)
        {
            return;
        }

        GM.musicVol = musicSlider.value;
        musicVolBTN.interactable = true;
        exitBTN.interactable = true;
        sfxBTN.interactable = true;
        SetCurrentButton(musicVolBTN.gameObject);
    }

    public void SelectSFXVol()
    {
        if (!PMC.soundPanel.activeSelf)
        {
            return;
        }
        musicVolBTN.interactable = false;
        sfxSlider.interactable = true;
        sfxBTN.interactable = false; ;
        exitBTN.interactable = false;
        SetCurrentButton(sfxSlider.gameObject);
    }

    public void SetSFXVol()
    {
        if (!PMC.soundPanel.activeSelf)
        {
            return;
        }
        GM.SFXVol = sfxSlider.value;
        musicVolBTN.interactable = true;
        exitBTN.interactable = true;
        sfxBTN.interactable = true;
        SetCurrentButton(sfxBTN.gameObject);
    }

    public void Exit()
    {
        SetMusicVol();
        SetSFXVol();
        PMC.musicBus.setFaderLevel(GM.musicVol);
        PMC.soundFXBus.setFaderLevel(GM.SFXVol);
        PMC.ReturnToPauseMenu();
    }

}
