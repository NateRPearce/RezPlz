using UnityEngine;
using System.Collections;

public class SpiritClawScript : MonoBehaviour {

	PlayerControls PC;
	Transform corpse;
	public Transform bloodSpurt;
    bool triggered;

	void Update(){
		if (PC != null && GetComponent<Collider2D>().enabled) {
			if(!PC.facingRight){
				PC.transform.position=new Vector3(transform.position.x+2,transform.position.y,PC.transform.position.z);
			}else{
				PC.transform.position=new Vector3(transform.position.x-2,transform.position.y,PC.transform.position.z);
			}
		}

		if (corpse != null&&GetComponent<Collider2D>().enabled) {
			if(!PC.facingRight){
			corpse.position = new Vector3 (transform.position.x+2, transform.position.y, corpse.position.z);
			}else{
				corpse.position = new Vector3 (transform.position.x-2, transform.position.y, corpse.position.z);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<PlayerControls> () != null&&!triggered) {
			PC=other.GetComponent<PlayerControls> ();
            triggered = true;
			Instantiate(bloodSpurt,new Vector3(transform.position.x,other.transform.position.y,-13),bloodSpurt.rotation);
		}
		if (other.tag == "Corpse" && other.name == "BodyCollider") {
			corpse=other.transform;
		}
	}
}
