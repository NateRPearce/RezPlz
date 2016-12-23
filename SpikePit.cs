using UnityEngine;
using System.Collections;

public class SpikePit : MonoBehaviour {

    PlayerControls PC;
    float xPos;

    void OnTriggerEnter2D(Collider2D other)
	{
	if (other.name == "Player1" || other.name == "Player2") {
			PC = other.GetComponent<PlayerControls> ();
			if (!PC.stoneSkinActivated) {
                PC.ControlsEnabled = false;
                xPos = other.transform.position.x;
                PC.move = 0;
                PC.runningMomentum = 0;
                PC.rbody.velocity = new Vector2(0, 0);
                other.transform.position = new Vector3 (xPos, transform.position.y + 3.2f, other.transform.position.z);
            }
		}
	}
}
