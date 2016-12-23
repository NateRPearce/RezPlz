using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LvlSelectBook : GameStateFunctions {


    [FMODUnity.EventRef]
    string narration1Sound = "event:/RezPlz Page 1.2";
    FMOD.Studio.EventInstance narration1EV;

    [FMODUnity.EventRef]
    string narration2Sound = "event:/RezPlz Page 2.3";
    FMOD.Studio.EventInstance narration2EV;

    [FMODUnity.EventRef]
    string narration3Sound = "event:/RezPlz Page 3.2";
    FMOD.Studio.EventInstance narration3EV;

    [FMODUnity.EventRef]
    string narration4Sound = "event:/RezPlz Page 4.3";
    FMOD.Studio.EventInstance narration4EV;

    string bookSound = "event:/Book Open";
    FMOD.Studio.EventInstance bookEV;

    [FMODUnity.EventRef]
    string bookCloseSound = "event:/Book Close";
    FMOD.Studio.EventInstance bookCloseEV;

    [FMODUnity.EventRef]
    string pageSound = "event:/Page Turn";
    FMOD.Studio.EventInstance pageEV;

    [FMODUnity.EventRef]
    string wooshSound = "event:/Book Moving";
    FMOD.Studio.EventInstance wooshEV;

    NewMainMenuScript NMMS;
    public Vector3 OnScreenPOS;
    Vector3 OffScreenPOS;
    Animator anim;
    bool breakWhile;
    Vector3 tempPOS;
    public Text[] bookText;
    public Image[] bookImages;
    public Button[] bookButtons;
    public Button[] pageTurnBtns = new Button[3];
    public GameObject controls;
    public Text[] controlsText;
    public Text LoadingTxt;
    public Text btnPrompt;
    bool readyToPlay;
    public int lvlToLoad;

    void Awake()
    {
        NMMS = GetComponentInParent<NewMainMenuScript>();
        NMMS.LSB = this;
        OffScreenPOS = transform.localPosition;
        anim = GetComponent<Animator>();
        bookButtons = GetComponentsInChildren<Button>();
        controlsText = controls.GetComponentsInChildren<Text>();
        bookText = GetComponentsInChildren<Text>();
        bookEV = FMODUnity.RuntimeManager.CreateInstance(bookSound);
        pageEV = FMODUnity.RuntimeManager.CreateInstance(pageSound);
        wooshEV = FMODUnity.RuntimeManager.CreateInstance(wooshSound);
        bookCloseEV = FMODUnity.RuntimeManager.CreateInstance(bookCloseSound);
        narration1EV = FMODUnity.RuntimeManager.CreateInstance(narration1Sound);
        narration2EV = FMODUnity.RuntimeManager.CreateInstance(narration2Sound);
        narration3EV = FMODUnity.RuntimeManager.CreateInstance(narration3Sound);
        narration4EV = FMODUnity.RuntimeManager.CreateInstance(narration4Sound);
        for (int i = 0; i < 3; i++)
        {
            pageTurnBtns[i].interactable = false;
            pageTurnBtns[i].enabled = false;
            pageTurnBtns[i].GetComponent<Animator>().SetTrigger("Disabled");
        }
    }


    void Start()
    {
        FindGM();
        HideGraphics();
    }

    void Update()
    {
        if (!readyToPlay)
        {
            return;
        }
        if (Input.GetButtonDown(GM.jumpbtn[0])|| Input.GetButtonDown(GM.jumpbtn[1]))
        {
            btnPrompt.color = Color.clear;
            LoadingTxt.color = Color.white;
            GM.mainMenuPageNumber = 0;
            Debug.Log("Load level");
            SceneManager.LoadScene(lvlToLoad);
        }
    }

    public void PlayBookOpen()
    {
        bookEV.start();
    }

    public void PlayPageTurn()
    {
        pageEV.start();
    }

    public void HideGraphics()
    {
        for (int j = 0; j < bookButtons.Length; j++)
        {
            bookButtons[j].enabled = false;
            bookButtons[j].interactable = false;
        }
        for (int t = 0; t < bookText.Length; t++)
        {
            bookText[t].color = new Color(bookText[t].color.r, bookText[t].color.g, bookText[t].color.b, 0);
        }
        for (int t = 0; t < bookImages.Length; t++)
        {
            if (bookImages[t].name != "LvlSelectGraphic")
            {
                bookImages[t].color = new Color(bookImages[t].color.r, bookImages[t].color.g, bookImages[t].color.b, 0);
            }
        }
    }

    public void LowerDownBook()
    {
        anim.SetTrigger("CloseBook");
        StartCoroutine("HideBook");
        wooshEV.start();
    }

    public void PlayCloseSound()
    {
        bookCloseEV.start();
    }

    public IEnumerator HideBook()
    {
        yield return new WaitForSeconds(1);
        do
        {
            Debug.Log("Hide");
            tempPOS = transform.localPosition;
            transform.localPosition = Vector3.Lerp(transform.localPosition, OffScreenPOS, Time.deltaTime * 5);
            yield return new WaitForSeconds(0.02f);
            if (transform.localPosition == tempPOS)
            {
                Debug.Log("hide Break");
                breakWhile = true;
            }
            if (Mathf.Abs(transform.localPosition.y - OffScreenPOS.y) < 8)
            {
                transform.localPosition = OffScreenPOS;
                anim.ResetTrigger("CloseBook");
                break;
            }
        } while (transform.localPosition != OffScreenPOS && !breakWhile);

    }

    public void BringUpBook()
    {
        wooshEV.start();
        StartCoroutine("ShowBook");
    }

    public IEnumerator ShowBook()
    {
        do
        {
            tempPOS = transform.localPosition;
            transform.localPosition = Vector3.Lerp(transform.localPosition, OnScreenPOS, Time.deltaTime*5);
            yield return new WaitForSeconds(0.02f);
            if (transform.localPosition == tempPOS)
            {
                breakWhile = true;
            }
            if (Mathf.Abs(transform.localPosition.y - OnScreenPOS.y) < 2)
            {
                transform.localPosition = OnScreenPOS;
                yield return new WaitForSeconds(0.1f);
                anim.SetTrigger("OpenBook");
                PlayBookOpen();
                break;
            }
        } while (transform.localPosition != OnScreenPOS && !breakWhile);
    }


    public void ShowMenu()
    {
        if (GM.mainMenuPageNumber < 4)
        {
            StartCoroutine("TellStory", GM.mainMenuPageNumber);
        }
        else if(GM.mainMenuPageNumber == 4)
        {
            StartCoroutine("ShowLevelSelect");
            for (int j = 0; j < bookButtons.Length; j++)
            {
                bookButtons[j].enabled = true;
                bookButtons[j].interactable = false;
            }
        }else
        {
            StartCoroutine("ShowControls");
        }
    }

    IEnumerator TellStory(int imageNum)
    {
        if (GM.mainMenuPageNumber == 0)
        {
            narration1EV.start();
        }
        else if (GM.mainMenuPageNumber == 1)
        {
            narration2EV.start();
        }
        else if (GM.mainMenuPageNumber == 2)
        {
            narration3EV.start();
        }
        else if (GM.mainMenuPageNumber == 3)
        {
            narration4EV.start();
        }
        for (float i = 0; i <= 1; i += 0.1f)
        {
            bookImages[imageNum].color = new Color(bookImages[imageNum].color.r, bookImages[imageNum].color.g, bookImages[imageNum].color.b, i);
            yield return new WaitForSeconds(0.1f);
            Debug.Log(bookImages[imageNum].color.a);
        }
        if (bookImages[imageNum].color.a > 0.9f)
        {
            Debug.Log("Color Reached");
            for (int i = 0; i < 3; i++)
            {
                pageTurnBtns[i].enabled = true;
                pageTurnBtns[i].interactable = true;
            }
            NMMS.SetCurrentButton(pageTurnBtns[2].gameObject);
            pageTurnBtns[2].GetComponent<Animator>().SetTrigger("Highlighted");
        }
    }
    public void SkipIt()
    {
        if (GM.mainMenuPageNumber == 0)
        {
            narration1EV.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        else if (GM.mainMenuPageNumber == 1)
        {
            narration2EV.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        else if (GM.mainMenuPageNumber == 2)
        {
            narration3EV.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        else if (GM.mainMenuPageNumber == 3)
        {
            narration4EV.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        bookImages[GM.mainMenuPageNumber].color = new Color(bookImages[GM.mainMenuPageNumber].color.r, bookImages[GM.mainMenuPageNumber].color.g, bookImages[GM.mainMenuPageNumber].color.b, 0);
        GM.mainMenuPageNumber=4;
        for (int i = 0; i < 3; i++)
        {
            pageTurnBtns[i].interactable = false;
            pageTurnBtns[i].enabled = false;
            pageTurnBtns[i].GetComponent<Animator>().SetTrigger("Disabled");
        }

        NMMS.SetCurrentButton(null);
        StopAllCoroutines();
        anim.SetTrigger("TurnPage");
        PlayPageTurn();
    }
    public void PreviousPage()
    {
        if (GM.mainMenuPageNumber == 0)
        {
            return;
        }
        else if (GM.mainMenuPageNumber == 1)
        {
            narration2EV.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        else if (GM.mainMenuPageNumber == 2)
        {
            narration3EV.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        else if (GM.mainMenuPageNumber == 3)
        {
            narration4EV.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        bookImages[GM.mainMenuPageNumber].color = new Color(bookImages[GM.mainMenuPageNumber].color.r, bookImages[GM.mainMenuPageNumber].color.g, bookImages[GM.mainMenuPageNumber].color.b, 0);
        GM.mainMenuPageNumber--;
        for (int i = 0; i < 3; i++)
        {
            pageTurnBtns[i].interactable = false;
            pageTurnBtns[i].enabled = false;
            pageTurnBtns[i].GetComponent<Animator>().SetTrigger("Disabled");
        }

        NMMS.SetCurrentButton(null);
        StopAllCoroutines();
        anim.SetTrigger("PrevPage");
        PlayPageTurn();
    }

    public void TurnThePage()
    {
        if (GM.mainMenuPageNumber == 0)
        {
            narration1EV.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        else if (GM.mainMenuPageNumber == 1)
        {
            narration2EV.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        else if (GM.mainMenuPageNumber == 2)
        {
            narration3EV.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        else if (GM.mainMenuPageNumber == 3)
        {
            narration4EV.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        bookImages[GM.mainMenuPageNumber].color = new Color(bookImages[GM.mainMenuPageNumber].color.r, bookImages[GM.mainMenuPageNumber].color.g, bookImages[GM.mainMenuPageNumber].color.b, 0);
        GM.mainMenuPageNumber++;
        for (int i = 0; i < 3; i++)
        {
            pageTurnBtns[i].interactable = false;
            pageTurnBtns[i].enabled = false;
            pageTurnBtns[i].GetComponent<Animator>().SetTrigger("Disabled");
        }

        NMMS.SetCurrentButton(null);
        StopAllCoroutines();
        anim.SetTrigger("TurnPage");
        PlayPageTurn();
    }

    IEnumerator ShowLevelSelect()
    {
        for (float i = 0; i <= 1; i +=0.1f)
        {
            bookImages[4].color = new Color(bookImages[4].color.r, bookImages[4].color.g, bookImages[4].color.b, i);
            for (int t=0;t<bookText.Length;t++)
            {
                if (bookText[t].name == "Demotxt" || bookText[t].name == "Pic caption" || bookText[t].name == "Header" || bookText[t].name == "Quittxt" || bookText[t].name == "Returntxt")
                {
                    bookText[t].color = new Color(bookText[t].color.r, bookText[t].color.g, bookText[t].color.b, i);
                }else if (bookText[t].color.a < 0.2)
                {
                    bookText[t].color = new Color(bookText[t].color.r, bookText[t].color.g, bookText[t].color.b, i);
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
        if (bookText[0].color.a > 0.9)
        {
            for (int j = 0; j < bookButtons.Length; j++)
            {
                bookButtons[j].interactable = true;
            }
            NMMS.SetCurrentButton(NMMS.demoLvlBtn);
            yield break;
        }
    }

    public void StartDemo(int lvl)
    {
        lvlToLoad = lvl;
        HideGraphics();
        NMMS.bookGraphics.SetActive(false);
        NMMS.SetCurrentButton(null);
        GM.mainMenuPageNumber++;
        anim.SetTrigger("TurnPage");
        PlayPageTurn();      
    }

    public IEnumerator ShowControls()
    {
        for (float i = 0; i <= 100; i++)
        {
            for (int t = 0; t < controlsText.Length; t++)
            {
                controlsText[t].color = new Color(controlsText[t].color.r, controlsText[t].color.g, controlsText[t].color.b, i / 100);
            }
        }
        yield return new WaitForSeconds(1);
        btnPrompt.color = Color.white;
        readyToPlay = true;
    }
}
