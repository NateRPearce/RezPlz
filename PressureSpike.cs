using UnityEngine;
using System.Collections;

public class PressureSpike : MonoBehaviour {

    public Transform corpse;
    public RemoteTriggerScript RTS;

	void Start () {
        RTS = GetComponentInParent<RemoteTriggerScript>();
	}
	

	void Update () {
        if (corpse != null && RTS.Activated)
        {
            corpse.position = Vector3.Lerp(corpse.position, transform.position,Time.deltaTime*5);
            corpse.rotation = transform.rotation;
            corpse.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            if (!corpse.GetComponent<Rigidbody2D>().isKinematic)
            {
                corpse.GetComponent<Rigidbody2D>().isKinematic = true;
            }
        }
        if (!RTS.Activated&& corpse != null&& corpse.GetComponent<Rigidbody2D>().isKinematic)
        {
            corpse.GetComponent<Rigidbody2D>().isKinematic = false;
            corpse = null;
        }
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.name=="BodyCollider"&&!other.GetComponentInParent<PlayerControls>()&&RTS.Activated)
        {
            corpse = other.transform;
        }
    }
}
