using UnityEngine;
using System.Collections;

public class Projectile_Sounds : SoundFunctions {


    [FMODUnity.EventRef]
    string collisionSound = "event:/LAVALEVEL/LavaLevel_Fireball_Create";
    public FMOD.Studio.EventInstance collisionEv;



    void Start()
    {
        soundStartFunctions();
        collisionEv = FMODUnity.RuntimeManager.CreateInstance(collisionSound);
    }

    public void playCollisionSound()
    {
        collisionEv.setVolume(checkRange());
        collisionEv.start();
    }
	
}
