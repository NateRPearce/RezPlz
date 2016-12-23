using UnityEngine;
using System.Collections;

public class SlowPlayerDecent : MonoBehaviour {

	public bool minorSlow;


	void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player")
        {
            
            if (other.GetComponent<PlayerControls>() && other.GetComponent<Rigidbody2D>().velocity.y < -30)
            {
                if (!minorSlow)
                {
                    other.GetComponent<Rigidbody2D>().velocity = new Vector2(other.GetComponent<Rigidbody2D>().velocity.x, -30);
                }
                else {
                    other.GetComponent<Rigidbody2D>().velocity = new Vector2(other.GetComponent<Rigidbody2D>().velocity.x, other.GetComponent<Rigidbody2D>().velocity.y / 2);
                }
            }
           
        }
	}


}
