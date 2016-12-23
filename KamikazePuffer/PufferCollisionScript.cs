using UnityEngine;
using System.Collections;

public class PufferCollisionScript : DeathScript {

	public PufferScript PS;

	void Start () {
		PS = GetComponentInParent<PufferScript> ();
	}


	public override void HitBy (string OBJ)
	{
		if (OBJ == "AttackCollider" || OBJ == "Fireball") {
			PS.hitByPlayer=true;
			PS.exploding=true;
		}
	}
}
