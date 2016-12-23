using UnityEngine;
using System.Collections;

public class ToggleControlsEnabled : GameStateFunctions {

	public Transform entranceCollider;
	public bool UnlockControls;
	public bool DestroySequence;
    public float unlockDelay;
	void Start(){
		FindGM ();
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
            StartCoroutine("Unlock");
		}
	}
    IEnumerator Unlock()
    {
        yield return new WaitForSeconds(unlockDelay);
        GM.PM.eventHappening = !UnlockControls;
        GM.PM.controlsLocked = !UnlockControls;
        GM.PM.PC1.moveLocked = false;
        GM.PM.PC2.moveLocked = false;
        if (entranceCollider != null)
        {
            entranceCollider.GetComponent<Collider2D>().isTrigger = false;
        }
        if (DestroySequence)
        {
            DestroySequenceScript DSS = GetComponent<DestroySequenceScript>();
            DSS.DestroySequence();
        }
    }
}
