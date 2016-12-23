using UnityEngine;
using System.Collections;

public class FireBallCollision : MonoBehaviour {

    Animator anim;
    Rigidbody2D rbody;

    void Awake()
    {
        anim = GetComponentInParent<Animator > ();
        rbody = GetComponentInParent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Tags.ground || other.tag == "Player"||other.tag==Tags.enemy||other.tag=="MagicBlast")
        {
            rbody.velocity = Vector2.zero;
            rbody.constraints = RigidbodyConstraints2D.FreezePositionY;
            anim.SetTrigger("Destroy");
        }
    }
}
