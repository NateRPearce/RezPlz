using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FinalScreen_Stats : GameStateFunctions {

    float CT;
    float NOD;
    float score;
    float masksCollected;

    public Text lvlTime;
    public Text lvlNOD;
    //public Text lvlDif;
    public Text lvlScore;
    public Text secretMasks;

    void Start () {
        FindGM();
        ConvertDataToStrings();
        lvlTime.text = "Completion Time: " + CT;
        lvlNOD.text = "Number Of Deaths: " + NOD;
        secretMasks.text = "Secret Masks found: " + masksCollected + " of 1";
        //lvlDif.text = "Difficulty: " + DIF;
        lvlScore.text = "Level Score: " + score;
        GM.masksCollected = 0;
    }

    void ConvertDataToStrings()
    {
        CT = GM.GameLevels[GM.currentLevel].lastCompletionTime;
        NOD = GM.GameLevels[GM.currentLevel].lastnumOfDeaths;
        masksCollected = GM.masksCollected;
       // DIF = GM.GameLevels[GM.currentLevel].lastCompletionDifficulty;
        score = GM.GameLevels[GM.currentLevel].score;
    }
}
