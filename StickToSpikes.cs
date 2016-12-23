using UnityEngine;
using System.Collections;

public class StickToSpikes : MonoBehaviour {

    LayerMask whatIsDeadly;
    bool floating;

    void Start()
    {
        whatIsDeadly = LayerMask.GetMask("DeadlyOBJ");
    }


    void Update()
    {
        floating = Physics2D.BoxCast(transform.position, new Vector2(7, 2), 0,new Vector2(0, 0), 0, whatIsDeadly);
        if (!floating)
        {
            GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }

 void OnTriggerEnter2D(Collider2D other){
	if (other.tag == "GroundSpikes"&&(other.name!="Claw_Collider"&& other.GetComponent<MovingSpikes>() == null)) {
			GetComponent<Rigidbody2D>().isKinematic=true;
			transform.rotation=Quaternion.AngleAxis(0,Vector3.forward);
		}
	}
	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "GroundSpikes") {
			GetComponent<Rigidbody2D>().isKinematic=true;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "GroundSpikes"&&other.GetComponent<MovingSpikes>()==null) {
			GetComponent<Rigidbody2D>().isKinematic=false;
		}
	}
}
