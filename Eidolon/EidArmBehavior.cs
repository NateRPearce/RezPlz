using UnityEngine;
using System.Collections;

public class EidArmBehavior : MonoBehaviour {

	Quaternion lArmNeutralRot;
	Quaternion SkullNeutralRot;
	public Transform Skull;
	RemoteTriggerScript RTS;
	ProjectileSpawner PS;
	EidolonBehavior EB;
	public Vector2 fireDir;
	public LayerMask solidObject;
	bool fireBlocked;
	// Use this for initialization
	void Awake () {
		EB = GetComponentInParent<EidolonBehavior> ();
		lArmNeutralRot = transform.parent.rotation;
		SkullNeutralRot = Skull.rotation;
		RTS = GetComponentInChildren<RemoteTriggerScript> ();
		PS = GetComponentInChildren<ProjectileSpawner> ();
	}



	public void resetArmPos(){
		transform.parent.rotation = lArmNeutralRot;
		Skull.rotation = SkullNeutralRot;
		EB.attacking = false;
	}
}
