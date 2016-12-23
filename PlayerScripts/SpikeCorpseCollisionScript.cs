using UnityEngine;
using System.Collections;

public class SpikeCorpseCollisionScript : MonoBehaviour {

	public Transform Head;
	public Transform Arms;
	public Transform Body;
	public float torque;
	public bool triggered;

    [FMODUnity.EventRef]
    string squishSound = "event:/Player/General_Player_Spike_BodySquish";
    FMOD.Studio.EventInstance squishEV;


    void Awake()
    {
        squishEV = FMODUnity.RuntimeManager.CreateInstance(squishSound);
    }

    void OnTriggerEnter2D(Collider2D other){
		if (other.name=="FeetCollider") {
            Arms.GetComponent<Rigidbody2D>().AddTorque(torque * -1);
            Head.GetComponent<Rigidbody2D>().AddTorque((torque * -1) / 2);
            squishEV.start();
            if (!triggered){
			Body.position=Vector3.MoveTowards(Body.position,new Vector3(Body.position.x,Body.position.y-0.1f,Body.position.z),1);
			}
			triggered=true;
        }
	}
}
