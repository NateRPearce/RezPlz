using UnityEngine;
using System.Collections;

public class BatlingAtkCol : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerControls>())
        {
            //PC = other.GetComponent<PlayerControls>();
           // if (PC.anim.GetBool("FullStone"))
          //  {
           //     Debug.Log("Hit stone");
          //      BB.rBody.velocity = new Vector2(0, 0);
          //      BB.isAttacking = false;
          //      BB.anim.SetBool("isAttacking", false);
          //      BB.atkTimer = 1;
          //      return;
            }
       // }
    }
    }
