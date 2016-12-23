using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EidolonTrigger : GameStateFunctions {

    [FMODUnity.EventRef]
    string bossSound = "event:/Music/Boss 1";
    public static FMOD.Studio.EventInstance BossEV;

    public Transform skullGuide;
	SkullGuideHideInWalls SG;
	public Transform eidolon;
	EidolonBehavior EB;
	PlayerControls PC;
	public Transform boss;
	CaveBossBehavior CBB;
	public Vector3 bossFightCamPosition;
    public Transform fader;
    Restart faderScrpt;
    void Awake()
    {
        EB = eidolon.GetComponent<EidolonBehavior>();
        faderScrpt = fader.GetComponent<Restart>();
        if (boss.GetComponent<CaveBossBehavior>() != null)
        {
            CBB = boss.GetComponent<CaveBossBehavior>();
        }
        if (BossEV == null)
        {
            BossEV = FMODUnity.RuntimeManager.CreateInstance(bossSound);
        }
    }

    void Start(){
        FindGM ();
		FindCBS ();
        SG = skullGuide.GetComponent<SkullGuideHideInWalls>();
    }

    void OnTriggerEnter2D(Collider2D other){
	if (other.GetComponent<PlayerControls> ()) {
			PC=other.GetComponent<PlayerControls> ();
            //StartCoroutine("DemoEndSequence");
            StartCoroutine("ActivateEidolon");
		}
	}

    IEnumerator DemoEndSequence()
    {
        CBS.cameraEvent=true;
		CBS.EventLocation=bossFightCamPosition;
		CBS.camSize = 34;
        if (CBB != null)
        {
            CBB.bossFightCamPosition = bossFightCamPosition;
            //CBB.activated = true;
        }
        CBB.anim.SetBool("Taunt", true);
        yield return new WaitForSeconds(3f);
        faderScrpt.fadeOut = true;
        yield return new WaitForSeconds(1.6f);
        FMOD.Studio.PLAYBACK_STATE playcheck;
        BossEV.getPlaybackState(out playcheck);
        if (playcheck != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            BossEV.setVolume(0.2f);
            BossEV.start();
            StartCoroutine("FadeInBossMusic");
        }
        yield return new WaitForSeconds(0.4f);
        GM.LST.HighScoreCheck();
        SceneManager.LoadScene("Final_Splash_Screen");
    }

	IEnumerator ActivateEidolon(){
		GM.PM.controlsLocked=true;
		CBS.cameraEvent=true;
		CBS.EventLocation=bossFightCamPosition;
		CBS.camSize = 34;
		yield return new WaitForSeconds (1.5f);
        /*
      GM.HD [0].Talk ("Look at the size of that thing!",157,43,-153,-2,2);
      yield return new WaitForSeconds (2.5f);
      GM.HD [1].Talk ("What are we going to do?",140,43,130,-2,2);
      yield return new WaitForSeconds (2.5f);
      GM.HD [0].Talk ("Something is happening to the skullerfly!",215,43,-193,-2,2);
      yield return new WaitForSeconds (2);
       */
        SG.transforming = true;
		SG.anim.SetTrigger ("Transform");
		//yield return new WaitForSeconds (1.8f);
		GM.PM.PC1.mask.enabled=false;
		GM.PM.PC2.mask.enabled=false;
		PC.eidolonMode=true;
		EB.eidolonActivated=true;
        EB.anim.SetBool("Activated", true);
		EB.SetControls(PC,PC.otherPC);
		GetComponent<Collider2D>().enabled = false;
		GM.EidolonLifeBar.mana=GM.MB[0].mana+GM.MB[1].mana;
		GM.EidolonLifeBar.UpdateManaBar(0);
		if(CBB!=null){
			CBB.activated=true;
            CBB.readyToAttack = true;
            CBB.bossFightCamPosition = bossFightCamPosition;
        }
        yield return new WaitForSeconds (1f);
		SG.transforming = false;
		SG.eidolonActivated=true;
        EB.rbody.isKinematic = false;
	}
    public IEnumerator FadeInBossMusic()
    {
        for (float vol = 0.2f; vol <= 1; vol += 0.025f)
        {
            BossEV.setVolume(vol);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
