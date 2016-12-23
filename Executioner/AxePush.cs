using UnityEngine;
using System.Collections;

public class AxePush : MonoBehaviour {

    PlayerControls PC;
    MinionScript MS;

    void Start()
    {
        if (GetComponentInParent<MinionScript>() != null)
        {
            MS = GetComponentInParent<MinionScript>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerControls>() != null)
        {
            PC = other.GetComponent<PlayerControls>();
            if (!PC.anim.GetBool("FullStone"))
            {
                return;
            }
            if (MS.facingRight)
            {
                Debug.Log("push Right");
                PC.rbody.AddForce(new Vector2(3000, 10),ForceMode2D.Impulse);
            }
            else
            {
                Debug.Log("push Left");
                PC.rbody.AddForce(new Vector2(-3000, 10),ForceMode2D.Impulse);
            }
        }
    }
}
