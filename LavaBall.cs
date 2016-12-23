using UnityEngine;
using System.Collections;

public class LavaBall : MonoBehaviour {

    Collider2D col;
    Rigidbody2D rbody;
	// Use this for initialization
	void Start () {
        col = GetComponent<Collider2D>();
        rbody = GetComponent<Rigidbody2D>();
        InvokeRepeating("KinCheck", Time.deltaTime, .1f);
	}
	
    void KinCheck()
    {
        if (rbody.isKinematic||rbody.velocity.y<-2)
        {
            col.enabled = false;
        }else
        {
            col.enabled = true;
        }
    }
}
