using UnityEngine;
using System.Collections;

public class MetalPortalExit : GameStateFunctions {

    [FMODUnity.EventRef]
    string eatSound = "event:/MetalLevel/MetalLevel_DemonPortal (2)";
    FMOD.Studio.EventInstance eatEV;

    int playerNumber;
    public Transform hidePos;
    public Transform playerPoint;
    Animator anim;
    SpriteRenderer sr;
    RemoteTriggerScript RTS;
    float alpha;
    public SpriteRenderer fade;
    bool startFade;
    float fadeAlpha;
    Collider2D col;


    void Start () {
        FindGM();
        FindCBS();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.clear;
        RTS = GetComponent<RemoteTriggerScript>();
        alpha = 0;
        col = GetComponent<Collider2D>();
        fade.color = new Color(0, 0, 0, 0);
        eatEV = FMODUnity.RuntimeManager.CreateInstance(eatSound);

    }

    void FixedUpdate()
    {
        if (RTS.Activated && !col.enabled)
        {
            col.enabled = true;
        }

        if (RTS.Activated && alpha < 1)
        {
            sr.color = new Color(1, 1, 1, alpha);
            alpha += 0.1f;
        }

        if (startFade && fadeAlpha < 1)
        {
            fadeAlpha += 0.025f;
            fade.color = new Color(0, 0, 0, fadeAlpha);
        }
    }

	void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "Player"&&GM.raceMode){
            GM.PM.raceOver = true;
        }

        if (other.name == "Player1")
        {
            StartCoroutine("StartEating");
            playerNumber = 0;
            Eat();
        }
        else if(other.name == "Player2")
        {
            StartCoroutine("StartEating");
            playerNumber = 1;
            Eat();
        }

    }

    IEnumerator StartEating()
    {
        yield return new WaitForSeconds(0.5f);
        if (playerNumber == 0)
        {
            anim.SetTrigger("EatP1");
        }
        else
        {
            anim.SetTrigger("EatP2");
        }
        yield return new WaitForSeconds(3.2f);
        GM.loadNextLevel();
    }
    public void Eat()
    {
        StartCoroutine("StartEating");
        GM.PM.eventHappening = true;
        GM.PM.controlsLocked = true;
        GM.PM.PC1.moveLocked = true;
        GM.PM.PC2.moveLocked = true;
        GM.PM.PC1.move= 0;
        GM.PM.PC2.move= 0;
        GM.PM.PC1.rbody.velocity = new Vector2(0, GM.PM.PC1.rbody.velocity.y);
        GM.PM.PC2.rbody.velocity = new Vector2(0, GM.PM.PC2.rbody.velocity.y);
        GM.LST.HighScoreCheck();
    }

    void SetPlayerPos()
    {
        if (playerNumber == 0)
        {
            GM.PM.PC1.transform.position = playerPoint.position;
        }
        else
        {
            GM.PM.PC2.transform.position = playerPoint.position;
        }
    }

    void RemovePlayer(int playerNum)
    {
        if (playerNum == 0)
        {
            GM.PM.PC1.transform.position = hidePos.position;
        }else
        {
            GM.PM.PC2.transform.position = hidePos.position;
        }
    }
    public void StartFadeOut()
    {
        fadeAlpha = 0;
        startFade = true;
    }

    public void PlaySound()
    {
        eatEV.start();
    }
}
