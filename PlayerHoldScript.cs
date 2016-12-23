using UnityEngine;
using System.Collections;

public class PlayerHoldScript : MonoBehaviour {

	Transform tempParent;
	public PlayerControls PC;
	public bool GrabCorpesInstead;
    public float width;
    public float height;
    public LayerMask whatIsPlayer;
    public bool playerNear;
    public Collider2D col;

    void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    void Start()
    {
        whatIsPlayer = LayerMask.GetMask("Player","HitBox","Enemy");
    }

    void Update()
    {
        if (!col.enabled)
        {
            return;
        }
        playerNear = Physics2D.OverlapBox(transform.position,new Vector2(width,height),0, whatIsPlayer);
        if (PC != null && !playerNear)
        {
            PC.transform.parent = PC.trueParent;
            PC = null;
        }
    }

	void OnTriggerEnter2D(Collider2D other){
        if (!col.enabled)
        {
            return;
        }

        if (GrabCorpesInstead) {
			if(other.tag=="Corpse"){
				tempParent=other.transform.parent;
				other.transform.parent=transform;
			}
			return;
		}
		if (other.name == "Player1"||other.name=="Player2") {
            if (other.GetComponent<PlayerControls>() != null)
            {
                PC = other.GetComponent<PlayerControls>();
            }
                tempParent = other.transform.parent;
			other.transform.parent = transform;		
		}
    }

	void OnTriggerExit2D(Collider2D other){
        if (!col.enabled)
        {
            return;
        }

        if (GrabCorpesInstead)
        {
            if (other.tag == "Corpse")
            {
                other.transform.parent = tempParent;
            }
            return;
        }
        if (other.name == "Player1" || other.name == "Player2")
        {
            if (other.GetComponent<PlayerControls>() != null)
            {
                PC = other.GetComponent<PlayerControls>();
                other.transform.parent = PC.trueParent;
            }
            else
            {
                Debug.Log("trigger exit change parent2");
                other.transform.parent = tempParent;
            }
        }
    }
    public void DropPlayer()
    {
        if (PC != null)
        {
            Debug.Log("Drop em!!!");
            PC.transform.parent = PC.trueParent;
            PC = null;
        }
    }

    public void ReleasePlayer(Collider2D other)
    {
        if (GrabCorpesInstead)
        {
            if (other.tag == "Corpse")
            {
                other.transform.parent = tempParent;
            }
            return;
        }
        if (other.name == "Player1" || other.name == "Player2")
        {
            if (other.GetComponent<PlayerControls>() != null)
            {
                PC = other.GetComponent<PlayerControls>();
                other.transform.parent = PC.trueParent;
            }
            else
            {
                other.transform.parent = tempParent;
            }
        }
    }
}
