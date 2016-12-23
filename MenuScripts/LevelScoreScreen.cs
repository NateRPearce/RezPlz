using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelScoreScreen : GameStateFunctions {

	public Text lvlTime;
	public Text lvlNOD;
    //public Text lvlDif;
    //public Text lvlScore;
    //public Text HighScore;
    public Text extraCredit;
    public Text finalGrade;
    public Image waxGrade;
    //public Text HeroStatus;

    public float finalScore;
	public float fadeAlpha;
	public SpriteRenderer fadeSR;
	public bool FadeIn;
	public Text pressAtxt;
	int currentKingdom;
	string king;
	float CT;
	int NOD;
	int DIF;
	float FS;
	int HS;
	float KP;
	int firstlvl;
	int lastlvl;
    public float tempTime;
    public int tempDeaths;
    public int randomTimeCountUp;
    public int randomDeathCountUp;
    public Image reportCard;
    public float cardSize;
    public float cardColor;
    public Text[] allText;
    public int EC;
    int tempEC=0;
    public bool jumpPulse;
    public float alpha;
    bool fullAlpha;

    void Awake()
    {
        reportCard.transform.localScale = new Vector3(.15f, .15f, .15f);
        cardSize = .15f;
        cardColor = 0;
        allText = GetComponentsInChildren<Text>();
    }
    void Start () {
		FadeIn = true;
		FindGM ();
		currentKingdom = GM.currentKingdomNum;
		firstlvl = GM.Kingdoms[currentKingdom].firstLevel;
		lastlvl = GM.Kingdoms[currentKingdom].lastLevel;
		ConvertDataToStrings ();
        randomTimeCountUp = Random.Range(30, 50);
        tempTime = CT - randomTimeCountUp;
        randomDeathCountUp = Random.Range(3, NOD);
        tempDeaths = NOD - randomDeathCountUp;
        lvlTime.text = "" + tempTime;
        lvlNOD.text = "" + tempDeaths;
        extraCredit.text = tempEC + "%";

        //lvlDif.text = "Difficulty: " + DIF ;
        //lvlScore.text = "Level Score: " + FS;
        //HighScore.text = "High Score: " + HS;
        //HeroStatus.text = "A heroes death";
        StartCoroutine("ScaleReportCard");
        StartCoroutine("ShowReportCard");
        StartCoroutine("CountUp");
    }

    IEnumerator ScaleReportCard()
    {
        do
        {
            cardSize += 0.05f;
            reportCard.transform.localScale = Vector3.Lerp(reportCard.transform.localScale,new Vector3(cardSize, cardSize, cardSize),Time.deltaTime);
            yield return new WaitForSeconds(0.025f);
        } while (reportCard.transform.localScale.z < .75f);
        reportCard.transform.localScale = new Vector3(.75f, .75f, .75f);
    }

    IEnumerator CountUp()
    {
        yield return new WaitForSeconds(1.5f);

        do
        {
            lvlTime.text = "" + tempTime;
            lvlNOD.text = "" + tempDeaths;
            extraCredit.text = tempEC + "%";
            if (tempTime != CT)
            {
                tempTime += 1;
            }else
            {
                lvlTime.color = new Color(.55f, 0, 0, 1);
            }
            if (tempDeaths != NOD)
            {
                tempDeaths += 1;
            }else
            {
                lvlNOD.color = new Color(.55f, 0, 0, 1);
            }
            if (tempEC != EC)
            {
                tempEC += 5;
            }else
            {
                extraCredit.color = new Color(.55f, 0, 0, 1);
            }
            yield return new WaitForSeconds(.05f);
        } while (tempTime != CT||tempEC!=EC);
        lvlTime.text = "" + CT;
        lvlNOD.text = "" + NOD;
        extraCredit.text = EC + "%";
        lvlTime.color = new Color(.55f, 0, 0, 1);
        waxGrade.color = new Color(1, 1, 1, 1);
        jumpPulse = true;
        StartCoroutine("JumpBtnPulse");
        yield break;
    }


    IEnumerator JumpBtnPulse()
    {
        do
        {
            if (alpha < 0.25f)
            {
                fullAlpha = false;
            }
            if (alpha > 1)
            {
                fullAlpha = true;
            }
            if (fullAlpha)
            {
                alpha -= 0.02f;
            }
            else
            {
                alpha += 0.02f;
            }
            pressAtxt.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0.01f);
        } while (jumpPulse);
        {
            yield break;
        }
    }




    IEnumerator HideReportCard()
    {
        do
        {
            cardColor -= 0.01f;
            reportCard.color = new Color(1, 1, 1, cardColor);
            waxGrade.color = new Color(1, 1, 1, cardColor);
            fadeSR.color = new Color(0, 0, 0, 1 - cardColor);
            foreach (Text t in allText)
            {
                t.color = new Color(0, 0, 0, cardColor);
            }
            yield return new WaitForSeconds(0.01f);
            if(cardColor == 0)
            {
                yield break;
            }
        } while (cardColor> 0);
        cardColor = 0;
        reportCard.color = new Color(1, 1, 1, cardColor);
        waxGrade.color = new Color(1, 1, 1, cardColor);
        foreach (Text t in allText)
        {
            t.color = new Color(0, 0, 0, cardColor);
        }
    }

    IEnumerator ShowReportCard()
    {
        do
        {
            cardColor += 0.01f;
            reportCard.color = new Color(1, 1, 1, cardColor);
            fadeSR.color = new Color(0, 0, 0, 1 - cardColor);
            foreach (Text t in allText)
            {
                t.color = new Color(0, 0, 0, cardColor);
            }
            yield return new WaitForSeconds(0.01f);
            if (cardColor == 1)
            {
                yield break;
            }
        } while (cardColor < 1);
        cardColor = 1;
        reportCard.color = new Color(1, 1, 1, cardColor);
        foreach (Text t in allText)
        {
            t.color = new Color(0, 0, 0, cardColor);
        }
    }


    void Update(){
        if (GM.PM == null)
        {
            return;
        }
		if (Input.GetButtonDown (GM.PM.PC1.jumpbtn) || Input.GetButtonDown (GM.PM.PC2.jumpbtn)) {
            StartCoroutine("HideReportCard");
            StartCoroutine("LoadNext");
            FadeIn = false;
            jumpPulse = false;
        }

    }
    IEnumerator LoadNext()
    {
        yield return new WaitForSeconds(2);
        if (GM.nextLvl + 1 > SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(GM.nextLvl);
        }
    }

	void ConvertDataToStrings(){
		king = GM.GameLevels [GM.currentLevel].KNGDM;
		CT = GM.GameLevels [GM.currentLevel].lastCompletionTime;
		NOD=GM.GameLevels [GM.currentLevel].lastnumOfDeaths;
		//DIF=GM.GameLevels [GM.currentLevel].lastCompletionDifficulty;
		//FS = GM.GameLevels [GM.currentLevel].score;
		//HS = GM.GameLevels [GM.currentLevel].highScore;
		for (int i=firstlvl; i<=lastlvl; i++) {
			Debug.Log("level:"+i +"Highscore" + GM.GameLevels[i].highScore);
			KP+=GM.GameLevels [i].highScore;
		}
		GM.TotalKingdomPoints[currentKingdom] = (int)KP;
	}

}