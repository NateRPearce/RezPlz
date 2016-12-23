using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CurrentLevelScoreTracker : GameStateFunctions {

	public int levelNumber;
	public int levelCompletionBonus;
	public Text levelTimer;
	string KingdomName;
	float lvlTime;
	public float finalScore;
    public int player1Points;
    public int player2Points;
	// Use this for initialization
	void Start () {
		FindGM ();
		GM.LST = this;
		GM.currentLevel = SceneManager.GetActiveScene().buildIndex - 2;
		findKingdomName ();
		SetCurrentScore ();
		GM.GameLevels [GM.currentLevel].lastCompletionDifficulty = GM.difficulty;
		if (GM.numOfPlayers == 1) {
			GM.singlePlayerBonus=50;
		}
        player1Points = 1000;
        player2Points = 1000;
	}

	void FixedUpdate(){
		if (GM.numOfPlayers == 2) {
			GM.singlePlayerBonus=0;
		}
		lvlTime += Time.deltaTime;
		levelTimer.text = ""+Mathf.Round(lvlTime*100)/100;
	}
	public void SetCurrentScore(){
		levelNumber = GM.currentLevel;
		GM.GameLevels [levelNumber].KNGDM = KingdomName;
		GM.GameLevels [levelNumber].lastCompletionDifficulty = GM.difficulty;
		GM.GameLevels [levelNumber].lastnumOfDeaths = 0;
	}
	public void findKingdomName(){
		if (GM.currentLevel < 21) {
			KingdomName = "The Caves";
		}else if (GM.currentLevel > 20&&GM.currentLevel < 41) {
			KingdomName = "Kingdom 2";
		}else if (GM.currentLevel > 40&&GM.currentLevel < 61) {
			KingdomName = "Kingdom 3";
		}else if (GM.currentLevel > 60&&GM.currentLevel < 81) {
			KingdomName = "Kingdom 4";
		}else if (GM.currentLevel > 80&&GM.currentLevel < 101) {
			KingdomName = "Kingdom 5";
		}
	}

	public void HighScoreCheck(){
        if (GM.raceMode)
        {
            int player1Deduction= (GM.player1Deaths * 50);
            int player2Deduction= (GM.player2Deaths * 50);
            player1Points -= player1Deduction;
            player2Points -= player2Deduction;
            player1Points += GM.p1Win;
            player2Points += GM.p2Win;

            Debug.Log("Player 1 Died: " + GM.player1Deaths + " times - "+player1Deduction+ "points");
            Debug.Log("Player 2 Died: " + GM.player2Deaths + " times - "+ player2Deduction + "points");
            Debug.Log("player 1 Score: " + player1Points + " points");
            Debug.Log("player 2 Score: " + player2Points + " points");
            if (player1Points > player2Points)
            {
                Debug.Log("Player1 wins!!!");
            }else if(player1Points < player2Points)
            {
                Debug.Log("Player2 wins!!!");
            }
        }
        GM.GameLevels [levelNumber].lastCompletionTime = +Mathf.Round(lvlTime*100)/100;
		finalScore = levelCompletionBonus + ((100 * GM.difficulty))+GM.singlePlayerBonus + GM.masksCollected * 1000 - ((lvlTime*4) + (GM.GameLevels[levelNumber].lastnumOfDeaths * 200));
		finalScore=Mathf.Round (finalScore * 1) / 1;
		if (finalScore < 500) {
			finalScore=500;
		}
		GM.GameLevels [levelNumber].score = (int)finalScore;
		if (finalScore > GM.GameLevels [levelNumber].highScore) {
			GM.GameLevels[levelNumber].highScore = (int)finalScore;
			GM.GameLevels[levelNumber].recordCompletionDifficulty=GM.difficulty;
			GM.GameLevels[levelNumber].recordCompletionTime=+Mathf.Round(lvlTime*100)/100;
			GM.GameLevels[levelNumber].recordnumOfDeaths=GM.GameLevels[levelNumber].lastnumOfDeaths;
		}
	}
}
