using UnityEngine;
using System.Collections;

public class MeatHook : GameStateFunctions {

    PlayerControls PC;
    DeadlyObject DO;
    public Transform targetCorpse;
    public int dir;
    public Rigidbody2D chainEnd;
    void Start()
    {
        FindGM();
        DO = GetComponent<DeadlyObject>();
    }

    void Update()
    {
        if (gameObject.tag == "WallSpikes"&& !GM.PM.PDS1.playerDead && !GM.PM.PDS2.playerDead)
        {
            gameObject.tag = "Untagged";
            gameObject.layer = 0;
            GetComponent<Collider2D>().enabled = true;
            PC = null;
        }
        if (PC != null)
        {
            targetCorpse.position = transform.position;
        }
    }

 void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "HandCollider")
        {
            PC = other.GetComponentInParent<PlayerControls>();
            Debug.Log(PC.facingRight + " " + dir);
            if (PC.facingRight && dir < 0)
            {
                PC = null;
                return;
            }
            if (!PC.facingRight && dir > 0)
            {
                PC = null;
                return;
            }
            gameObject.tag = "WallSpikes";
            gameObject.layer = 20;
            PC.transform.position = transform.position;
            targetCorpse = PC.GetComponent<CreateCorpses>().WallHangingDeadBody;
            DO.Kill(PC.GetComponent<Collider2D>());
            chainEnd.AddForce(new Vector2(6000*dir, 0));
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
