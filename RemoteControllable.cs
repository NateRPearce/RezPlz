using UnityEngine;
using System.Collections;

public class RemoteControllable : DifficultySetting {

	public bool activated;
	public bool invertToggle;
	public RemoteTriggerScript RTS;
	public bool RemoteControl;


	public void CheckActiveStatus(){
		if (invertToggle) {
			activated = !RTS.Activated;	
		} else {
			activated = RTS.Activated;		
		}
	}
}
