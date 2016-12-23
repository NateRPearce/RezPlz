using UnityEngine;
using System.Collections;

public class MetalServantCollisionDetection : MonoBehaviour {

	
	public MetalServantBehavior MSB;
	public WeaknessesScript WS;
	
	void Start(){
		MSB = GetComponentInParent<MetalServantBehavior> ();
		WS = GetComponentInParent<WeaknessesScript> ();
	}
	
	void HitBy (string OBJ)
	{
		for(int i=0;i<WS.weakness.Length;i++){
			if (OBJ == WS.weaknessArray[i]&&!MSB.getDead()) {
				MSB.setDead(true);
			}
		}
	}
	
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "Ground") {
			HitBy (other.tag);
		}
	}
}
