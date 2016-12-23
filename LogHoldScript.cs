using UnityEngine;
using System.Collections;

public class LogHoldScript : MonoBehaviour {

    public Vector3 startingPos;
    Rigidbody2D rbody;
	// Use this for initialization
	void Start () {
        startingPos = transform.position;
        rbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = startingPos;
        if (transform.rotation.z < -22)
        {
            transform.rotation = Quaternion.Euler(0, 0, -22);
        }else if (transform.rotation.z > 22)
        {
            transform.rotation = Quaternion.Euler(0, 0, 22);
        }
        if(rbody.angularVelocity>400) 
        {
            rbody.angularVelocity = 400;
        }
        else if (rbody.angularVelocity < -400)
        {
            rbody.angularVelocity = -400;
        }
        //if(rbody.angularVelocity>100|| rbody.angularVelocity < -100)
        //Debug.Log(rbody.angularVelocity);
    }
}
