using UnityEngine;
using System.Collections;

public class SquishCheck : MonoBehaviour {


    PlayerControls PC;
	// Use this for initialization
	void Start () {
	}
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Tags.corpse)
        {
            tag = "Explosion";
        }
    }

	// Update is called once per frame
	void OnTriggerStay2D (Collider2D other) {

        if (other.GetComponent<PlayerControls>())
        {
            PC = other.GetComponent<PlayerControls>();
            if (PC.grounded)
            {
                tag = "Explosion";
                StartCoroutine("OffOn");
            }
        }
	}

    IEnumerator OffOn()
    {
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(0.001f);
        GetComponent<Collider2D>().enabled = true; 
    }
}
