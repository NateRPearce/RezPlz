using UnityEngine;
using System.Collections;

public class GibCorpse : MonoBehaviour {

    public Transform gibCorpse;
    public Vector3 posOffset;
    public Rigidbody2D[] rbodies;
    Transform newGibs;


    public void createGiblets()
    {
        newGibs = Instantiate(gibCorpse, transform.position+posOffset, Quaternion.identity)as Transform;
        rbodies = newGibs.GetComponentsInChildren<Rigidbody2D>();
        foreach(Rigidbody2D r in rbodies)
        {
            int x = Random.Range(-100, 100);
            int y = Random.Range(-100, 100);
            r.AddForce(new Vector2(x, y));
        }
    }
}
