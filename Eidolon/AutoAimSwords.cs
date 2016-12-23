using UnityEngine;
using System.Collections;

public class AutoAimSwords : MonoBehaviour {

    Rigidbody2D rbody;
    public Transform target;
    FollowScript FS;
    Collider2D col;
    public bool trackTargetX;
    public bool trackTargetY;
    public bool targetFound;
    Collider2D localCol;
    void Start()
    {
        FS = GetComponent<FollowScript>();
        FS.target = target;
        rbody = target.GetComponent<Rigidbody2D>();
        col = target.GetComponent<Collider2D>();
        localCol = GetComponent<Collider2D>();

    }

    public void FixedUpdate()
    {
        if (!col.enabled)
        {
            if (!localCol.enabled)
            {
                localCol.enabled = true;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!col.enabled)
        {
            if (rbody.velocity != Vector2.zero)
            {
                rbody.velocity = Vector2.zero;
            }
            return;
        }
        if (other.name == "WeakPointCollider")
        {
            StartCoroutine("TempDisable");
        }
    }

        void OnTriggerStay2D(Collider2D other)
    {
        if (!col.enabled)
        {
            if (rbody.velocity != Vector2.zero)
            {
                rbody.velocity = Vector2.zero;
            }
            return;
        }
        if (other.name == "WeakPointCollider")
        {
            
            float xDist = other.transform.position.x - target.position.x;
            float yDist = other.transform.position.y - target.position.y;
            if (Mathf.Abs(xDist) + Mathf.Abs(yDist) > 20)
            {
                return;
            }

            if ((rbody.velocity.x > 0 && xDist > 0) || (rbody.velocity.x < 0 && xDist < 0))
            {
                //target is in front of sword X
                trackTargetX = true;
            }
            else
            {
                trackTargetX = false;
            }

            if ((rbody.velocity.y > 0 && yDist > 0) || (rbody.velocity.y < 0 && yDist < 0))
            {
                //target is in front of sword Y
                trackTargetY = true;
            }
            else
            {
                trackTargetY = false;
            }

            if (trackTargetX || trackTargetY)
            {
                rbody.AddForce(new Vector2(xDist,yDist)*60);
                rbody.velocity=new Vector2(rbody.velocity.x, rbody.velocity.y).normalized * 80;
            } 
        }
    }

    IEnumerator TempDisable()
    {
        yield return new WaitForSeconds(0.1f);
        localCol.enabled = false;
    }
}
