using UnityEngine;
using System.Collections;

public class UlmockSounds : Minion_Sound_Functions {

    [FMODUnity.EventRef]
    string roar1Sound = "event:/ULMOK/LavaLevel_Ulmok_Roar_1";
    FMOD.Studio.EventInstance roar1EV;

    [FMODUnity.EventRef]
    string roar2Sound = "event:/ULMOK/LavaLevel_Ulmok_Roar_2";
    FMOD.Studio.EventInstance roar2EV;

    [FMODUnity.EventRef]
    string roar3Sound = "event:/ULMOK/LavaLevel_Ulmok_Roar_3";
    FMOD.Studio.EventInstance roar3EV;

    [FMODUnity.EventRef]
    string roar4Sound = "event:/ULMOK/LavaLevel_Ulmok_Roar_4";
    FMOD.Studio.EventInstance roar4EV;

    [FMODUnity.EventRef]
    string jumpSound = "event:/Player/General_Player_Spike_BodySquish";
    FMOD.Studio.EventInstance jumpEV;

    float jumpVol=1;
    float bellyFlopVol=1;
    float roarVol=1;
    CaveBossBehavior CBB;

    void Start()
    {
        soundStartFunctions();
        setBasicSounds();
        CBB = GetComponent<CaveBossBehavior>();
        jumpEV = FMODUnity.RuntimeManager.CreateInstance(jumpSound);
        roar1EV = FMODUnity.RuntimeManager.CreateInstance(roar1Sound);
        roar2EV = FMODUnity.RuntimeManager.CreateInstance(roar2Sound);
        roar3EV = FMODUnity.RuntimeManager.CreateInstance(roar3Sound);
        roar4EV = FMODUnity.RuntimeManager.CreateInstance(roar4Sound);
    }

    public void Play_Jump_Sound()
    {
        jumpEV.start();
    }


    public void Play_Roar4()
    {
        roar4EV.start();
        CBB.StartCoroutine("AllGeysers");
    }


    public void Play_Roar_Sound()
    {
        switch (CBB.health)
        {
            case (4):
                roar1EV.start();
                break;
            case (3):
                roar2EV.start();
                break;
            case (2):
                roar3EV.start();
                break;
            case (1):
                roar4EV.start();
                break;
            default:
                break;
        }
    }
}
