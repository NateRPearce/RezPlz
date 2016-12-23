using UnityEngine;
using System.Collections;

public class PlayerDetectionTrigger : MonoBehaviour {

    public bool playerFound;
    public LayerMask whatIsPlayer;
    public Transform[] target=new Transform[1];
    public RemoteTriggerScript[] RTS;
    float t;

	void Start ()
    {
        RTS = new RemoteTriggerScript[target.Length];
        if (target[0].GetComponent<RemoteTriggerScript>() != null)
        {
            for (int i = 0; i < target.Length; i++)
            {
                RTS[i] = target[i].GetComponent<RemoteTriggerScript>();
            }
        }
	}
	

    void FixedUpdate()
    {
        playerFound = Physics2D.OverlapCircle(transform.position, 2f,whatIsPlayer);
        if (RTS[0].Activated)
        {
            t += Time.deltaTime;
        }
        if (playerFound)
        {
            for (int i = 0; i < target.Length; i++)
            {
                RTS[i].Activated = true;
            }
        }
        else if(t>2)
        {
            t = 0;
            for (int i = 0; i < target.Length; i++)
            {
                RTS[i].Activated = false;
            }
        }
    }
}
