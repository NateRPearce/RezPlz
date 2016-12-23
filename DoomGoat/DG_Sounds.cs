using UnityEngine;
using System.Collections;

public class DG_Sounds : Minion_Sound_Functions {

    [FMODUnity.EventRef]
    string fallOverSound = "event:/Player/General_Player_Landing";
    FMOD.Studio.EventInstance fallOverEv;

    [FMODUnity.EventRef]
    string fireBallSound = "event:/LAVALEVEL/LavaLevel_Fireball_Create";
    FMOD.Studio.EventInstance fireBallEv;



    void Start()
    {
        soundStartFunctions();
        setBasicSounds();
        fireBallEv = FMODUnity.RuntimeManager.CreateInstance(fireBallSound);
        fallOverEv = FMODUnity.RuntimeManager.CreateInstance(fallOverSound);
    }

    public void Play_Fireball_Sound()
    {
        fireBallEv.setVolume(checkRange());
        fireBallEv.start();
    }


    public void Play_FallOver_Sound()
    {
        fallOverEv.setVolume(checkRange());
        fallOverEv.start();
    }
}
