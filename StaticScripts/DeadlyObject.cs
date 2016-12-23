using UnityEngine;
using System.Collections;

public class DeadlyObject : GameStateFunctions {


	void Start(){
		FindGM ();
	}

	public virtual void hit(Collider2D other, string deadlyTag){
		if (other.GetComponent<DeathScript> () != null&&other.tag!="AttackCollider") {
			DeathScript DS = other.GetComponent<DeathScript> ();
			DS.HitBy(deadlyTag);
		}
		if(other.GetComponent<BreakableScript>()!=null){
			BreakableScript BS = other.GetComponent<BreakableScript>();
			BS.Break(deadlyTag);
		}
	}
	void OnTriggerEnter2D(Collider2D other){
        Kill(other);
	}

	void OnTriggerStay2D(Collider2D other){
	if (tag == "Fire" && other.tag == "Player") {
			if (other.GetComponent<PlayerControls> () == null) {
				return;
			}
			PlayerControls tempPC = other.GetComponent<PlayerControls> ();
			if (!tempPC.dying && !tempPC.stoneSkinActivated) {
				hit (other, tag);
			}
		} else if(name=="DeadlySpikeCollider"){
			hit (other,tag);
		}
	}


	void OnTriggerExit2D(Collider2D other){
		if (tag == "Lava"&&other.tag=="Player"&&(!GM.PM.PC1.rezing&& !GM.PM.PC2.rezing)) {
			hit (other, "InstaKillLava");
		}
	}
    public void Kill(Collider2D other)
    {
        if (other.GetComponent<PlayerDeathScript>() != null)
        {
            PlayerDeathScript PDS = other.GetComponent<PlayerDeathScript>();
            if (PDS.COD == "InstaKillLava")
            {
                return;
            }
            else
            {
                hit(other, tag);
            }
        }
        else
        {
            hit(other, tag);
        }
    }
}
