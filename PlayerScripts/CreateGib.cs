using UnityEngine;
using System.Collections;

public class CreateGib : MonoBehaviour {

	public Transform gib;
    Animator anim;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other){
	if (other.tag == "Explosion" && anim.GetBool("Enabled"))
        {
            anim.SetBool("Enabled", false);
            if (GetComponent<Collider2D>().enabled){
			Instantiate(gib,transform.position,transform.rotation);
			}
			GetComponent<Collider2D>().enabled=false;
		}
	}
}
