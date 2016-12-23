using UnityEngine;
using System.Collections;

public class VelocityBasedRotation : MonoBehaviour {
    public bool disabled;
	public float angleMod;
	void Update () {
        if (disabled)
        {
            return;
        }
		if (!GetComponent<Rigidbody2D>().freezeRotation || tag==Tags.fireball||tag=="MagicBlast") {
			float angle = Mathf.Atan2 (GetComponent<Rigidbody2D>().velocity.y, GetComponent<Rigidbody2D>().velocity.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis (angle-angleMod, Vector3.forward);
		}
	}
}
