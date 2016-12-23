using UnityEngine;
using System.Collections;

public class MetalPortalEnterance : GameStateFunctions {

    [FMODUnity.EventRef]
    string eatSound = "event:/MetalLevel/MetalLevel_DemonPortal";
    FMOD.Studio.EventInstance eatEV;

    Animator portalAnim;
    public float delay;
    public Transform playerPoint;
    public GameObject startSequnce;
    public int playerNum;
	void Start () {
        FindGM();
        FindCBS();
        portalAnim = GetComponent<Animator>();
        StartCoroutine("PortalSequence");
        eatEV = FMODUnity.RuntimeManager.CreateInstance(eatSound);
    }

    IEnumerator PortalSequence()
    {
        yield return new WaitForEndOfFrame();
        if (!GM.raceMode)
        {
            if (GM.portalPlayer == 0)
            {
                GM.PM.PDS2.playerDead = true;
                GM.PM.PC2.anim.SetBool("CODExplode", true);
                GM.PM.PC2.anim.SetBool("CODExplode", true);
                GM.PM.PC2.anim.SetBool("Dead", true);
            }
            else
            {
                GM.PM.PDS1.playerDead = true;
                GM.PM.PC1.anim.SetBool("CODExplode", true);
                GM.PM.PC1.anim.SetBool("CODExplode", true);
                GM.PM.PC1.anim.SetBool("Dead", true);
            }
        }

        yield return new WaitForSeconds(delay);
        if (GM.raceMode)
        {
            if (GM.portalPlayer == playerNum)
            {
                portalAnim.SetTrigger("Enter");
            }
            else
            {
                portalAnim.SetTrigger("Enter2");
            }
            yield break;
        }
            if (GM.portalPlayer == 0)
        {
            portalAnim.SetTrigger("Enter");
        }
        else
        {
            portalAnim.SetTrigger("Enter2");
        }
    }
    public void BringPlayer()
    {
        if (GM.raceMode)
        {
            if (GM.portalPlayer == playerNum)
            {
                GM.PM.PC1.transform.position = playerPoint.position;
            }
            else
            {
                GM.PM.PC2.transform.position = playerPoint.position;
            }
            return;
        }
        if (GM.portalPlayer == 0)
        {
            GM.PM.PC1.transform.position = playerPoint.position;
        }else
        {
            GM.PM.PC2.transform.position = playerPoint.position;
        }
    }
    public void UnlockControls()
    {
        CBS.cameraEvent = false;
        CBS.maxPosOverride = false;
        GM.PM.controlsLocked = false;
        CBS.CamSpeed = 2;
        GM.PM.eventHappening = false;
        GM.PM.controlsLocked = false;
        GM.PM.PC1.moveLocked = false;
        GM.PM.PC2.moveLocked = false;
        Destroy(startSequnce);
        if (GM.raceMode)
        {
            return;
        }
        if (GM.portalPlayer == 0)
        {
            RezP2();
        }
        else
        {
            RezP1();
        }
    }
    void RezP1()
    {
        GM.PM.PC2.summoning = true;
        GM.PM.PC2.anim.SetBool("Summoning", true);
        GM.PM.PC1.transform.position = GM.PM.PC2.rezPoint.position;
        GM.PM.PC1.rezing = true;
        GM.PM.PDS1.PlayerAliveReset();
    }

    void RezP2()
    {
        GM.PM.PC1.summoning = true;
        GM.PM.PC1.anim.SetBool("Summoning", true);
        GM.PM.PC2.transform.position = GM.PM.PC1.rezPoint.position;
        GM.PM.PC2.rezing = true;
        GM.PM.PDS2.PlayerAliveReset();
        GM.PM.player2FoundDead = false;
    }
    public void PlaySound()
    {
        eatEV.start();
    }
}
