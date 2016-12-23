using UnityEngine;
using System.Collections;

public class RemoteTriggerAnim : MonoBehaviour {

    RemoteTriggerScript RTS;
    Animator anim;
    public bool inverted;  
	// Use this for initialization
	void Start () {
        RTS = GetComponent<RemoteTriggerScript>();
        if (GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }else
        {
            anim = GetComponentInParent<Animator>();
        }
        InvokeRepeating("ActiveCheck", Time.deltaTime, 0.5f);
	}
	
	void ActiveCheck()
    {
        if (inverted)
        {
            anim.SetBool("Activated", !RTS.Activated);
        }
        else
        {
            anim.SetBool("Activated", RTS.Activated);
        }
    }
}
