using UnityEngine;
using System.Collections;

public class StalacScript : MonoBehaviour {

    public float targetPos;
    public Collider2D deadlyCol;
    Collider2D col;

    void Start()
    {
        col = GetComponent<Collider2D>();
    }

	void Update () {
        if (transform.position.y < targetPos)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().isKinematic = true;
            deadlyCol.enabled = false;
            col.enabled = true;
            GetComponent<Animator>().SetTrigger("Break");
        }
    }
}
