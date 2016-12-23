using UnityEngine;
using System.Collections;

public class WeaknessesScript : MonoBehaviour {

	public enum weaknesses{Lava, AttackCollider, DeathSplit,Explosion,WallSpikes,Fire,Fireball,GroundSpikes};
	public weaknesses[] weakness  = new weaknesses[1];
	[HideInInspector]
	public string[] weaknessArray;
	// Use this for initialization
	void Awake () {
		weaknessArray = new string[weakness.Length];
		for (int i=0; i<weakness.Length; i++) {
			weaknessArray[i] = weakness[i].ToString ();
		}
	}

}
