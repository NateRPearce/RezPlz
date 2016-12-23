using UnityEngine;
using System.Collections;

public class BuzzSawSound : SoundFunctions {
    [FMODUnity.EventRef]
    public string bladeSound = "event:/MetalLevel/MetalLevel_SpinningBlades";
    FMOD.Studio.EventInstance bladeEV;


    public bool playerNear;
    LayerMask whatIsPlayer;
    public float range;
    float vol;

    void Start () {
        soundStartFunctions();
        bladeEV = FMODUnity.RuntimeManager.CreateInstance(bladeSound);
        bladeEV.setVolume(0);
        bladeEV.start();
        whatIsPlayer = LayerMask.GetMask("Player");
    }

    void Update()
    {
        playerNear = Physics2D.OverlapCircle(transform.position, range, whatIsPlayer);
        bladeEV.getVolume(out vol);
        if (!playerNear && vol != 0)
        {
            bladeEV.setVolume(0);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<PlayerControls>() != null)
        {
            bladeEV.setVolume(checkRange());
        }
    }
    void OnDestroy()
    {
        bladeEV.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

}
