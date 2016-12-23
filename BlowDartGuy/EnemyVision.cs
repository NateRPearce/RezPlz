using UnityEngine;
using System.Collections;

public class EnemyVision : MonoBehaviour {

    public BDGBehaviors BDG;

    void Start () {
        BDG = GetComponentInParent<BDGBehaviors>();
        
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (BDG.getDead())
        {
            return;
        }
        if (other.GetComponent<PlayerControls>()!=null&&!BDG.isAttacking)
        {
            BDG.targetPC = other.GetComponent<PlayerControls>();
            BDG.StartCoroutine("AttemptAttack");
        }
    }
}
