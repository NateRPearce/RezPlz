using UnityEngine;
using System.Collections;
public class PlayerDeathScript : PlayerDeathFunctions {



	void Start(){
		RunStart ();
	}
	
	void Update(){
        if (CBS == null)
        {
            return;
        }
		if (PC.eidolonMode) {
			return;
		}
			if (PC.isPlayer1) {
				PM.PDS1.playerDead=playerDead;	
			}else{
				PM.PDS2.playerDead=playerDead;
			}
		RezMe ();
		contactCheck ();
		DeadCheck ();
	}
	void FixedUpdate(){
		if (PC.eidolonMode) {
			return;
		}
		if (attackDeflected) {
			deflectionCooldown();
		}

	}
}
