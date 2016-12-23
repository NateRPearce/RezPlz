using UnityEngine;
using System.Collections;

public class RemoteTriggerScript : MonoBehaviour {

	public bool Activated;
    public bool halfActivated;
	public bool hold;
	public bool singleFire;
    public Transform childTarget;
	void Start(){
		InvokeRepeating ("singleFireCheck", Time.deltaTime, 0.1f);
	}

	void singleFireCheck(){
	if (singleFire && Activated) {
			Activated=false;
		}
	}
    public void ActivateChildren()
    {
        childTarget.GetComponent<RemoteTriggerScript>().Activated = true;
    }
}
