using UnityEngine;
using System.Collections;

public class SplitBodyScript : MonoBehaviour {

	Animator anim;
    Rigidbody2D rbody;
    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
    }

    void Start () {        
        InvokeRepeating("KinCheck", Time.deltaTime, 0.5f);
	}
	
	void KinCheck()
    {
        if (anim.GetBool("Active") && rbody.isKinematic)
        {
            rbody.isKinematic = false;
        }else if (!anim.GetBool("Active") && !rbody.isKinematic)
        {
            rbody.isKinematic = true;
        }
    }


	void ResetCorpse(){
		transform.rotation = new Quaternion (0, 0, 0, 0);
		anim.SetBool ("Active", false);
		anim.SetBool ("Dissolve", false);
		//gameObject.SetActive(false);
	}
}
