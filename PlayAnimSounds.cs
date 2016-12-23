using UnityEngine;
using System.Collections;

public class PlayAnimSounds : SoundFunctions {

    [FMODUnity.EventRef]
    public string sound1 = "place sound here";
    FMOD.Studio.EventInstance sound1EV;

    [FMODUnity.EventRef]
    public string sound2 = "place sound here";
    FMOD.Studio.EventInstance sound2EV;

    [FMODUnity.EventRef]
    public string sound3 = "place sound here";
    FMOD.Studio.EventInstance sound3EV;

    void Start () {
        if (sound1!= "place sound here")
        {
            sound1EV = FMODUnity.RuntimeManager.CreateInstance(sound1);

        }
        if (sound2 != "place sound here")
        {
            sound2EV = FMODUnity.RuntimeManager.CreateInstance(sound2);
        }
        if (sound3 != "place sound here")
        {
            sound3EV = FMODUnity.RuntimeManager.CreateInstance(sound3);
        }
        soundStartFunctions();
    }
	
	public void PlaySound1()
    {
        sound1EV.setVolume(checkRange());
        sound1EV.start();
    }

    public void PlaySound2()
    {
        sound2EV.setVolume(checkRange());
        sound2EV.start();
    }

    public void PlaySound3()
    {
        sound3EV.setVolume(checkRange());
        sound3EV.start();
    }
}
