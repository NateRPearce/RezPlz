using UnityEngine;
using System.Collections;

public class RecycleCorpse : MonoBehaviour {

	Animator anim;
	WallCorpse WC;
	HeadCorpse HC;

	void Start () {
		anim = GetComponent<Animator> ();
		if(GetComponentInParent<WallCorpse>()!=null){
			WC = GetComponentInParent<WallCorpse> ();
		}
		if(GetComponent<HeadCorpse>()!=null){
			HC = GetComponentInParent<HeadCorpse> ();
		}
	}

	public void Disable(){
		anim.SetBool ("Enabled", false);
		anim.SetBool ("Dissolve", false);
		if (WC != null) {
			WC.DissableCols ();
			if(WC.PS!=null){
                ParticleSystem.EmissionModule PE = WC.PS.emission;
				PE.rate=new ParticleSystem.MinMaxCurve(0);
			}
		} else if (HC != null) {
			HC.DissableCols ();
            ParticleSystem.EmissionModule PE = HC.PS.emission;
			PE.rate=new ParticleSystem.MinMaxCurve(0);
		} else {
			if(GetComponentInChildren<Collider2D>()!=null){
			GetComponentInChildren<Collider2D>().enabled=false;
			}else{
                if (GetComponentsInParent<Collider2D>() != null)
                {
                    Collider2D[] cols = GetComponentsInParent<Collider2D>();
                    foreach (Collider2D c in cols)
                    {
                        c.enabled = false;
                    }
                }else
                {
                    Collider2D[] cols = GetComponents<Collider2D>();
                    foreach (Collider2D c in cols)
                    {
                        c.enabled = false;
                    }
                }
			}
		}
	}
}
