using UnityEngine;
using System.Collections;

public class VelocityLimiter : MonoBehaviour {
    Rigidbody2D rbody;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Rigidbody2D>() != null && rbody == null)
        {
            rbody = other.GetComponent<Rigidbody2D>();
        }
        if (rbody == null)
        {
            return;
        }
        if (rbody.velocity.y > 60)
        {
            rbody.AddForce(new Vector2(0, -10));
        }
    }
}
