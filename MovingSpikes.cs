using UnityEngine;
using System.Collections;

public class MovingSpikes : MonoBehaviour {

    public bool corpseOn;
    public Transform BC;
    public LayerMask whatIsCorpse;
    public LayerMask whatIsPlayer;
    Collider2D col;
    public bool playerOn;
    PlayerDeathScript PDS;
    public float yOffset;
    DeadlyObject DO;
    void Start()
    {
        col = GetComponent<Collider2D>();
        whatIsCorpse = LayerMask.GetMask("Corpse");
        whatIsPlayer = LayerMask.GetMask("Player");
        DO = GetComponent<DeadlyObject>();
    }

    void Update()
    {
        corpseOn = Physics2D.OverlapBox(transform.position, new Vector2(1, 9),0, whatIsCorpse);
        col.enabled = !corpseOn;

        if (playerOn)
        {
            playerOn = Physics2D.OverlapBox(transform.position, new Vector2(1, 9), 0, whatIsPlayer);
        }

        if (corpseOn&&BC!=null)
        {
            BC.GetComponent<Rigidbody2D>().isKinematic = true; 
            BC.GetComponent<Rigidbody2D>().MovePosition(new Vector3(BC.position.x, transform.position.y, BC.position.z));
            BC.rotation = Quaternion.AngleAxis(0, Vector3.forward);
        }else if(!corpseOn && BC != null)
        {
            BC.GetComponent<Rigidbody2D>().isKinematic = false;
            BC = null;
        }
        if (BC != null)
        {
            if (!corpseOn)
            {
                BC.GetComponent<Rigidbody2D>().isKinematic = false;
                BC = null;
            }
        }
        
        if (playerOn&&PDS!=null)
        {
            if (!PDS.PC.dying&&!PDS.anim.GetBool("FullStone")&&!PDS.playerDead)
            {
                Debug.Log("Kill again");
                DO.hit(PDS.GetComponent<Collider2D>(), tag);
            }
            PDS.PC.rbody.MovePosition(new Vector3(PDS.transform.position.x, transform.position.y+yOffset, PDS.transform.position.z));
            if (PDS.playerDead)
            {
                playerOn = false;
                PDS = null;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerDeathScript>() != null&&!playerOn)
        {
            PDS = other.GetComponent<PlayerDeathScript>();
            playerOn = true;
        }
        if (other.GetComponentInParent<StickToSpikes>()!=null && other.tag == Tags.corpse&&BC==null)
        {
            BC = other.GetComponentInParent<StickToSpikes>().transform;
        }
        if (other.GetComponent<StickToSpikes>() != null && other.tag == Tags.corpse && BC == null)
        {
            BC = other.GetComponent<StickToSpikes>().transform;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<PlayerDeathScript>() != null && !playerOn)
        {
            PDS = other.GetComponent<PlayerDeathScript>();
            playerOn = true;
        }
        if (other.GetComponentInParent<StickToSpikes>() != null && other.tag == Tags.corpse&&BC==null)
        {
            BC = other.GetComponentInParent<StickToSpikes>().transform;
        }
        if (other.GetComponent<StickToSpikes>() != null && other.tag == Tags.corpse && BC == null)
        {
            BC = other.GetComponent<StickToSpikes>().transform;
        }
    }
}
