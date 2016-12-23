using UnityEngine;
using System.Collections;

public class GeyserDetector : MonoBehaviour {

    public LayerMask whatIsUlmock;
    public bool ulmockNear;

	void Update () {
        ulmockNear = Physics2D.OverlapBox(transform.position, new Vector2(6, 200), 0, whatIsUlmock);
	}
}
