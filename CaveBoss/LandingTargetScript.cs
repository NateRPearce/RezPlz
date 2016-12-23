using UnityEngine;
using System.Collections;

public class LandingTargetScript : MonoBehaviour {

    public bool tempDisable;
    RemoteTriggerScript RTS;

    void Start()
    {
        RTS = GetComponentInParent<RemoteTriggerScript>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "StompCollider"&&!tempDisable)
        {
            RTS.Activated = false;
            StartCoroutine("SinkandRise");
        }
    }


    IEnumerator SinkandRise()
    {
        tempDisable = true;
        yield return new WaitForSeconds(1f);
        RTS.Activated = true;
        tempDisable = false;
    }
}
