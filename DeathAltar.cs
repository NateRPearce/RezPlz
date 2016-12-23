using UnityEngine;
using System.Collections;

public class DeathAltar : MonoBehaviour {

    bool corpseOn;
    public LayerMask whatIsCorpse;
    bool Activated;
    Animator anim;
    public Transform[] target = new Transform[1];
    RemoteTriggerScript[] TargetRTS;
    public float[] TriggerDelays= new float[1];
	void Start () {
        anim = GetComponent<Animator>();
        whatIsCorpse = LayerMask.GetMask("Corpse","Giblets");
        TargetRTS = new RemoteTriggerScript[target.Length];
        for(int i = 0; i < target.Length; i++)
        {
            TargetRTS[i] = target[i].GetComponent<RemoteTriggerScript>();
        }
    }

    void Update () {
        if (Activated)
        {
            return;
        }
        corpseOn = Physics2D.OverlapBox(transform.position, new Vector2(7, 2), 0, whatIsCorpse);
        if (corpseOn)
        {
            anim.SetTrigger("Activate");
            Activated = true;
            StartCoroutine("ActivateTargets");
        }
    }

    IEnumerator ActivateTargets()
    {
        TargetRTS[0].Activated = true;
        if (target.Length == 1)
        {
            yield break;
        }
        yield return new WaitForSeconds(TriggerDelays[0]);
        TargetRTS[1].Activated = true;
        if (target.Length == 2)
        {
            yield break;
        }
        yield return new WaitForSeconds(TriggerDelays[1]);
        TargetRTS[2].Activated = true;
    }
}
