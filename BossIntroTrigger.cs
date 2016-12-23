using UnityEngine;
using System.Collections;

public class BossIntroTrigger : MonoBehaviour {

    [FMODUnity.EventRef]
    string introSound = "event:/Music/Lava Boss Intro 1";
    FMOD.Studio.EventInstance IntroEV;


    void Start () {
     IntroEV = FMODUnity.RuntimeManager.CreateInstance(introSound);
	}
	
	void OnTriggerEnter2D(Collider2D other)
    {
        FMOD.Studio.PLAYBACK_STATE playCheck;
        IntroEV.getPlaybackState(out playCheck);
        if (other.tag == "Player"&&playCheck!=FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            LevelAtmosphereScript.musicEv.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            IntroEV.setVolume(0.2f);
            IntroEV.start();
           StartCoroutine("FadeInBossIntro");
        }
    }

    public IEnumerator FadeInBossIntro()
    {
        for (float vol = 0.2f; vol <= 1; vol+=0.025f)
        {
            IntroEV.setVolume(vol);
            yield return new WaitForSeconds(0.01f);
        }
    }

}
