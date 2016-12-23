using UnityEngine;
using System.Collections;

public class BuzzSawKill : SoundFunctions {

    [FMODUnity.EventRef]
    string bladeSound = "event:/MetalLevel/MetalLevel_Batling_SpinAttack_2";
    FMOD.Studio.EventInstance bladeEV;

    FMOD.Studio.PLAYBACK_STATE bladeState;
    public int timesTriggered;


    void Start () {
        timesTriggered = 0;
        soundStartFunctions();
        bladeEV = FMODUnity.RuntimeManager.CreateInstance(bladeSound);
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "Explodable")
        {
            bladeEV.getPlaybackState(out bladeState);
            if (bladeState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                timesTriggered += 1;
                if (timesTriggered == 2)
                {
                    StartCoroutine("tempNoSound");
                }
                if (timesTriggered > 4)
                {
                    return;
                }
                bladeEV.setVolume(checkRange());
                bladeEV.start();
            }
        }
	}
    public IEnumerator tempNoSound()
    {
        yield return new WaitForSeconds(1);
        timesTriggered = 0;
    }
}
