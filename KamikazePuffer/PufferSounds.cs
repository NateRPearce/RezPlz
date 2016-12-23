using UnityEngine;
using System.Collections;

public class PufferSounds : SoundFunctions {

    [FMODUnity.EventRef]
    string puffSound = "event:/Puffer/LavaLevel_Puffer_Float_3";
    public FMOD.Studio.EventInstance puffEv;
    PufferScript PS;

	void Start () {
        soundStartFunctions();
        PS = GetComponent<PufferScript>();
        puffEv = FMODUnity.RuntimeManager.CreateInstance(puffSound);
    }


    public void Play_Puff_Sound()
    {
        if (!PS.Entered)
        {
            puffEv.setVolume(checkRange()/10);
        }else
        {
            puffEv.setVolume(checkRange());
        }
        puffEv.start();
    }
}
