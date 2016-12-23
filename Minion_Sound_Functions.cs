using UnityEngine;
using System.Collections;

public class Minion_Sound_Functions : SoundFunctions {

    [FMODUnity.EventRef]
    public string walkSound = "Put walk sound here";
    public FMOD.Studio.EventInstance walkEV;

    [FMODUnity.EventRef]
    public string attackSound = "Put attack Sound here";
    public FMOD.Studio.EventInstance attackEV;

    [FMODUnity.EventRef]
    public string deathSound = "Put death Sound here";
    public FMOD.Studio.EventInstance deathEV;

    [FMODUnity.EventRef]
    public string hitSound = "Put hit sound here";
    public FMOD.Studio.EventInstance hitEV;

    [FMODUnity.EventRef]
    public string hitSound2 = "Put hit sound here";
    public FMOD.Studio.EventInstance hit2EV;

    public bool disableRangeLimit;

    void Start()
    {
        FindGM();
        soundStartFunctions();
        setBasicSounds();
    }

    public void setBasicSounds()
    {
        walkEV = FMODUnity.RuntimeManager.CreateInstance(walkSound);
        attackEV = FMODUnity.RuntimeManager.CreateInstance(attackSound);
        deathEV = FMODUnity.RuntimeManager.CreateInstance(deathSound);
        hitEV = FMODUnity.RuntimeManager.CreateInstance(hitSound);
        if (hitSound2 != "Put hit sound here")
        {
            hit2EV = FMODUnity.RuntimeManager.CreateInstance(hitSound2);
        }

    }

    public void Play_Walk_Sound()
    {
        walkEV.setVolume(checkRange());
        walkEV.start();
    }

    public void Play_Hit_Sound()
    {
        hitEV.setVolume(checkRange());
        hitEV.start();
    }

    public void Play_Hit2_Sound()
    {
        hit2EV.setVolume(checkRange());
        hit2EV.start();
    }

    public void Play_Attack_Sound()
    {
        attackEV.setVolume(checkRange());
        attackEV.start();
    }

    public void Play_Death_Sound()
    {
        deathEV.setVolume(checkRange());
        deathEV.start();
    }


}
