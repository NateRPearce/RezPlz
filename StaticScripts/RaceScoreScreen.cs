using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class RaceScoreScreen : GameStateFunctions
 {

    public Text winner;

    public Text player1Score;
    public Text player1NOD;
    public Text player1Win;

    public Text player2Score;
    public Text player2NOD;
    public Text player2Win;

    public Text restart;
    bool restartEnabled;
    float targetAlpha;
    float alpha;


    void Start () {
        alpha = 0;
        FindGM();
        int p1deathPenalty= GM.player1Deaths * 50;
        int p2deathPenalty= GM.player2Deaths * 50;
        int p1Score = 1000 - p1deathPenalty + GM.p1Win;
        int p2Score = 1000 - p2deathPenalty + GM.p2Win;
        player1NOD.text = "Deaths: " + GM.player1Deaths + " - " + p1deathPenalty + " points";
        player2NOD.text = "Deaths: " + GM.player2Deaths + " - " + p2deathPenalty + " points";
        player1Win.text = "Win bonus: " + GM.p1Win + " points";
        player2Win.text = "Win bonus: " + GM.p2Win + " points";
        player1Score.text = "Score: " + p1Score + " points";
        player2Score.text = "Score: " + p2Score + " points";
        if (p1Score > p2Score)
        {
            winner.text = "Player 1 is the winnner!";
        }else
        {
            winner.text = "Player 2 is the winnner!";
        }

        StartCoroutine("EnableRestart");
    }

    void Update () {
        if (!restartEnabled)
        {
            return;
        }

        if (Input.GetButtonDown(GM.jumpbtn[0]) || Input.GetButtonDown(GM.jumpbtn[1]))
        {
            //restart race
            SceneManager.LoadScene("Metal Race Course");
        }
        if (Input.GetButtonDown(GM.grabbtn[0]) || Input.GetButtonDown(GM.grabbtn[1]))
        {
            //return to the menu
            GM.raceMode = false;
            SceneManager.LoadScene(0);
        }
    }

    void FixedUpdate()
    {
        if (restartEnabled)
        {
            if (targetAlpha == 1 && alpha >= 1)
            {
                targetAlpha = 0.25f;
            }
            else if (targetAlpha == 0.25f && alpha <= 0.25f)
            {
                targetAlpha = 1;
            }

            if (targetAlpha==0.25f&&alpha>0.25f)
            {
                alpha -= 0.025f;
            }else if(targetAlpha == 1 && alpha < 1)
            {
                alpha += 0.025f;
            }

            restart.color = new Color(1, 1, 1, alpha);
        }
    }

    IEnumerator EnableRestart()
    {
        yield return new WaitForSeconds(3);
        restartEnabled = true;
        targetAlpha = 1;
    }

}
