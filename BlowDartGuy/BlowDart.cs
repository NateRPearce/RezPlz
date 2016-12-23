using UnityEngine;
using System.Collections;

public class BlowDart : MonoBehaviour {

    Rigidbody2D rbody;
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) {
        if (other.GetComponent<PlayerControls>() != null)
        {
            StartCoroutine("DestroyDart");
        }
	}
    IEnumerator DestroyDart()
    {
        rbody.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(0.01f);
        Destroy(gameObject);
    }
}
