using UnityEngine;
using System.Collections;

public class BatlingSounds : SoundFunctions {

    [FMODUnity.EventRef]
    string flapSound = "event:/MetalLevel/MetalLevel_Batling_Idle_Flap_2";
    FMOD.Studio.EventInstance flapEV;

    [FMODUnity.EventRef]
    string wooshSound = "event:/Player/General_Player_Swing";
    FMOD.Studio.EventInstance wooshEV;

    [FMODUnity.EventRef]
    string attackSound = "event:/MetalLevel/MetalLevel_Batling_SpinAttack_2";
    FMOD.Studio.EventInstance attackEV;

    [FMODUnity.EventRef]
    string deathSound = "event:/MetalLevel/MetalLevel_Batling_DeathScreech";
    FMOD.Studio.EventInstance deathEV;

    [FMODUnity.EventRef]
    string hitSound = "event:/NATECHECKTHISOUT/General_Player_Swing_Hit_Enemy_02";
    FMOD.Studio.EventInstance hitEV;

    void Start () {
        soundStartFunctions();
        flapEV = FMODUnity.RuntimeManager.CreateInstance(flapSound);
        wooshEV = FMODUnity.RuntimeManager.CreateInstance(wooshSound);
        attackEV = FMODUnity.RuntimeManager.CreateInstance(attackSound);
        deathEV = FMODUnity.RuntimeManager.CreateInstance(deathSound);
        hitEV = FMODUnity.RuntimeManager.CreateInstance(hitSound);
    }

    public void PlayHitSound()
    {
        hitEV.setVolume(checkRange());
        hitEV.start();
    }
    public void FlapSound()
    {
        flapEV.setVolume(checkRange());
        flapEV.start();
    }
    public void WooshSound()
    {
        wooshEV.setVolume(checkRange());
        wooshEV.start();
    }

    public void PlayDeathSound()
    {
        deathEV.setVolume(checkRange());
        deathEV.start();
    }
    public void AttackSound()
    {
        attackEV.setVolume(checkRange());
        attackEV.start();
    }
    public void StopFlap()
    {
        flapEV.setVolume(0);
    }
    public void Stopwoosh()
    {
        wooshEV.setVolume(0);
    }
    public void StopAttack()
    {
        attackEV.setVolume(0);
    }
}
