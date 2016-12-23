using UnityEngine;
using System.Collections;


public class PlayerSounds : GameStateFunctions {

    [FMODUnity.EventRef]
    string fallSound = "event:/Player/General_Player_FallingScream";
    FMOD.Studio.EventInstance fallEV;

    [FMODUnity.EventRef]
    string bisectSound = "event:/Player/General_Player_QuickScream";
    FMOD.Studio.EventInstance bisectEV;


    [FMODUnity.EventRef]
    string hitSound = "event:/Player/General_Player_AhOhAh";
    FMOD.Studio.EventInstance hitEV;

    [FMODUnity.EventRef]
    string hit2Sound = "event:/Player/General_Player_AhOhAh (2)";
    FMOD.Studio.EventInstance hit2EV;

    [FMODUnity.EventRef]
    string hit3Sound = "event:/Player/General_Player_AhOhAh (3)";
    FMOD.Studio.EventInstance hit3EV;

    [FMODUnity.EventRef]
    string hit4Sound = "event:/Player/General_Player_Ow";
    FMOD.Studio.EventInstance hit4EV;

    [FMODUnity.EventRef]
    string turnToStoneSound = "event:/Abilities/General_Player_TurnToStone";
    FMOD.Studio.EventInstance turnToStoneEV;

    [FMODUnity.EventRef]
    string turnOffStoneSound = "event:/Abilities/General_Player_StoneSkinDeactivate";
    FMOD.Studio.EventInstance turnOffStoneEV;

    [FMODUnity.EventRef]
    string StoneSmashSound = "event:/ULMOK/LavaLevel_Ulmok_Flop";
    FMOD.Studio.EventInstance StoneSmashEV;

    [FMODUnity.EventRef]
    string oopSound = "event:/Player/General_Player_AlleyOop_Adjusted";
    FMOD.Studio.EventInstance oopEV;

    [FMODUnity.EventRef]
    string explodedSound = "event:/Player/General_Ambient_Viscera";
    FMOD.Studio.EventInstance explodedEV;

    [FMODUnity.EventRef]
    string inLavaSound = "event:/LAVA Death";
    FMOD.Studio.EventInstance inLavaEV;

    [FMODUnity.EventRef]
    string turnToSkeletonSound = "event:/NATECHECKTHISOUT/General_Player_OnFire";
    FMOD.Studio.EventInstance turnToSkeletonEV;

    [FMODUnity.EventRef]
    string skeletonSound = "event:/ROB_WORKFOLDER/Player_TurnToBones";
    FMOD.Studio.EventInstance skeletonEV;

    [FMODUnity.EventRef]
    string onFireSound = "event:/NATECHECKTHISOUT/OnFire_Death";
    FMOD.Studio.EventInstance onFireEV;

    [FMODUnity.EventRef]
    string stepSound = "event:/ROB_WORKFOLDER/General_Footsteps_Walk";
    FMOD.Studio.EventInstance walkEV;

    [FMODUnity.EventRef]
    string landSound = "event:/Player/General_Player_Landing";
    FMOD.Studio.EventInstance landEV;

    [FMODUnity.EventRef]
    string jumpSound = "event:/Player/General_Player_Jump_1";
    FMOD.Studio.EventInstance jumpEV;

    [FMODUnity.EventRef]
    string wallDragSound = "event:/Player/General_Player_SlideDownWall";
    public FMOD.Studio.EventInstance wallDragEV;

    [FMODUnity.EventRef]
    string squishSound = "event:/Player/General_Player_Spike_BodySquish";
    FMOD.Studio.EventInstance squishEV;

    [FMODUnity.EventRef]
    string rezSound = "event:/Player/General_Player_Resurrection";
    FMOD.Studio.EventInstance rezEV;

    [FMODUnity.EventRef]
    string failedRezSound = "event:/Bad Rez";
    FMOD.Studio.EventInstance failedRezEV;

    [FMODUnity.EventRef]
    string attackSound = "event:/Player/General_Player_Swing";
    FMOD.Studio.EventInstance attackEV;


    float inLavaVol = 0;
    float wallDragVol=1f;

    PlayerControls PC;

    void Start(){
        FindGM();
        PC = GetComponent<PlayerControls>();
        walkEV = FMODUnity.RuntimeManager.CreateInstance(stepSound);
        landEV = FMODUnity.RuntimeManager.CreateInstance(landSound);
        jumpEV = FMODUnity.RuntimeManager.CreateInstance(jumpSound);
        wallDragEV = FMODUnity.RuntimeManager.CreateInstance(wallDragSound);
        squishEV = FMODUnity.RuntimeManager.CreateInstance(squishSound);
        rezEV = FMODUnity.RuntimeManager.CreateInstance(rezSound);
        attackEV = FMODUnity.RuntimeManager.CreateInstance(attackSound);
        failedRezEV = FMODUnity.RuntimeManager.CreateInstance(failedRezSound);
        oopEV = FMODUnity.RuntimeManager.CreateInstance(oopSound);
        inLavaEV = FMODUnity.RuntimeManager.CreateInstance(inLavaSound);
        onFireEV = FMODUnity.RuntimeManager.CreateInstance(onFireSound);
        explodedEV = FMODUnity.RuntimeManager.CreateInstance(explodedSound);
        turnToSkeletonEV = FMODUnity.RuntimeManager.CreateInstance(turnToSkeletonSound);
        skeletonEV = FMODUnity.RuntimeManager.CreateInstance(skeletonSound);
        turnOffStoneEV = FMODUnity.RuntimeManager.CreateInstance(turnOffStoneSound);
        turnToStoneEV = FMODUnity.RuntimeManager.CreateInstance(turnToStoneSound);
        StoneSmashEV = FMODUnity.RuntimeManager.CreateInstance(StoneSmashSound);
        hitEV = FMODUnity.RuntimeManager.CreateInstance(hitSound);
        hit2EV = FMODUnity.RuntimeManager.CreateInstance(hit2Sound);
        hit3EV = FMODUnity.RuntimeManager.CreateInstance(hit3Sound);
        hit4EV = FMODUnity.RuntimeManager.CreateInstance(hit4Sound);
        fallEV = FMODUnity.RuntimeManager.CreateInstance(fallSound);
        bisectEV = FMODUnity.RuntimeManager.CreateInstance(bisectSound);
    }


    public float checkRange()
    {
        float distanceToPlayer = (((GM.skullGuide.position.x - transform.position.x) + (GM.skullGuide.position.y - transform.position.y)));
        float howFar = Mathf.Abs(distanceToPlayer);
        if (howFar > 60)
        {
            howFar = 0;
        }
        else
        {
            howFar = (60 - howFar) / 60;
        }
        return howFar;
    }

    public void PlayHit()
    {
        hitEV.start();
    }

    public void PlayHit2()
    {
        hit2EV.start();
    }

    public void PlayHit3()
    {
        hit3EV.start();
    }

    public void PlayHit4()
    {
        hit4EV.start();
    }

    public void PlayInLavaSound(bool on)
    {
        if (inLavaEV == null)
        {
            return;
        }
        if (!on)
        {
            inLavaEV.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        else if(on)
        {
            FMOD.Studio.PLAYBACK_STATE lavaState;
            inLavaEV.getPlaybackState(out lavaState);
            if (lavaState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                Debug.Log("already playing lava Sound");
                return;
            }
            inLavaEV.setVolume(1);
            inLavaEV.start();
        }
    }

    public void PlayExploded()
    {
        explodedEV.setVolume(checkRange());
        explodedEV.start();
    }

    public void PlayOnFireSound()
    {
        //onFireEV.setVolume(checkRange());
        onFireEV.start();
    }

    public void PlayTurnToSkeletonSound()
    {
        //onFireEV.setVolume(checkRange());
        turnToSkeletonEV.start();
    }

    public void PlayTurnSkeletonSound()
    {
        //onFireEV.setVolume(checkRange());
        skeletonEV.start();
    }

    public void StopOnFireSound()
    {
        onFireEV.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void PlayDeathSplitSound()
    {
    }

    public void PlayRezSound(){
        rezEV.start();
    }

    public void PlayFailedRezSound()
    {
        failedRezEV.start();
    }

    public void playSquish()
    {
        squishEV.start();
    }

    public void playStoneSmash()
    {
        StoneSmashEV.start();
    }

    public void playStone()
    {
        turnToStoneEV.start();
    }

    public void playStoneOff()
    {
        turnOffStoneEV.start();
    }

    public void playBoneBreak()
    {
   }

    public void playOopSound(){
        oopEV.start();
    }

	public void PlayJumpSound(){
        jumpEV.start();
   }

    public void playBisectSound()
    {
        bisectEV.start();
    }


    public void playFallSound()
    {
        fallEV.start();
    }

	public void PlayLandSound(){
        landEV.start();
   }

    public void playWalkSound()
    {
        walkEV.setVolume(Mathf.Abs(PC.move)*2);
        walkEV.start();
     }

    public void playWallDragSound()
    {
        wallDragEV.setVolume(wallDragVol);
        FMOD.Studio.PLAYBACK_STATE play_state;
        wallDragEV.getPlaybackState(out play_state);
        if(play_state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            wallDragEV.start();
        }
   }

    public void stopWallDragSound()
    {
        wallDragEV.setVolume(0);
        wallDragEV.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void PlaySwingSound()
    {
        attackEV.start();
    }
}
