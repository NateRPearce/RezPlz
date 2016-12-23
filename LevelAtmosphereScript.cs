using UnityEngine;
using System.Collections;

public class LevelAtmosphereScript : MonoBehaviour {


    [FMODUnity.EventRef]
    public string BGSound = "event:/NATECHECKTHISOUT/LAVA_AMB_V2";
    public FMOD.Studio.EventInstance BGEv;

    [FMODUnity.EventRef]
    public string[] music = new string[3];
    public static FMOD.Studio.EventInstance musicEv;


    void Start()
    {
        if (BGSound != "")
        {
            BGEv = FMODUnity.RuntimeManager.CreateInstance(BGSound);
            BGEv.start();
        }
        if (music[0] != "")
        {
            musicEv = FMODUnity.RuntimeManager.CreateInstance(music[0]);
            musicEv.start();
        }
    }

    // Update is called once per frame
    void OnDestroy () {
        if (BGEv != null)
        {
            BGEv.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            BGEv.release();
        }
        if (musicEv != null)
        {
            musicEv.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            musicEv.release();
        }  
    }

    public void Mute()
    {
        if (BGEv != null)
            BGEv.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        if (musicEv != null)
            musicEv.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
