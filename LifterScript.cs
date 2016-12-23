using UnityEngine;
using System.Collections;

public class LifterScript : MonoBehaviour {
    ExecutionerBehavior EB;
	public float xForce;
	public float yForce;
	public bool LiftPlatforms;

    void Awake()
    {
        EB = GetComponentInParent<ExecutionerBehavior>();
    }
 void OnTriggerEnter2D(Collider2D other){
		if (LiftPlatforms) {
			if(other.name=="LiftablePlatform"){
				other.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce,yForce*1.5f));		
			}
		}
	if ((other.name == "Player1" || other.name == "Player2")&&other.GetComponent<Rigidbody2D>()!=null) {
            if (EB.facingRight)
            {
                Rigidbody2D rbody = other.GetComponent<Rigidbody2D>();
                rbody.AddForce(new Vector2(-xForce, yForce), ForceMode2D.Impulse);
            }
            else
            {
                Rigidbody2D rbody = other.GetComponent<Rigidbody2D>();
                rbody.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
            }
		}
	}
}
