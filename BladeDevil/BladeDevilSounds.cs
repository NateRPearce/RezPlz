using UnityEngine;
using System.Collections;

public class BladeDevilSounds : Minion_Sound_Functions {

    [FMODUnity.EventRef]
    string walkSound2 = "event:/Player/General_Player_Walking_1";
    public FMOD.Studio.EventInstance walkEV2;

    [FMODUnity.EventRef]
    string hitKneesSound = "event:/Player/General_Player_Landing";
    public FMOD.Studio.EventInstance hitKneesEV;

    [FMODUnity.EventRef]
    string spotPlayerSound = "event:/Demon Noise";
    public FMOD.Studio.EventInstance spotPlayerEV;

    void Start()
    {
        soundStartFunctions();
        setBasicSounds();
        walkEV2 = FMODUnity.RuntimeManager.CreateInstance(walkSound2);
        hitKneesEV= FMODUnity.RuntimeManager.CreateInstance(hitKneesSound);
        spotPlayerEV = FMODUnity.RuntimeManager.CreateInstance(spotPlayerSound);
    }

    public void Play_Walk_Sound2()
    {

        walkEV2.setVolume(checkRange());
        walkEV2.start();
    }

    public void Play_hitKnees_Sound()
    { 
        hitKneesEV.setVolume(checkRange());
        hitKneesEV.start();
    }

    public void PlaySnicker()
    {
        spotPlayerEV.setVolume(checkRange());
        spotPlayerEV.start();
    }
}
