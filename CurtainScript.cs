using UnityEngine;
using System.Collections;

public class CurtainScript : MonoBehaviour {


	public Transform[] CurtainSegments = new Transform[1];
	public PlayerControls PC;
	float pcSpeed;
	Vector3 startingPos;
	bool touchingPlayer;
	public LayerMask whatIsPlayer;
	float touchtime;
	float cutSpeed;

	void Start(){
		startingPos = transform.localPosition;
	}
	void FixedUpdate(){
		touchingPlayer = Physics2D.OverlapCircle (new Vector3(transform.position.x,transform.position.y+4,transform.position.z), 4f, whatIsPlayer);

		if (PC == null) {
			return;
		}

		if (touchingPlayer) {
			touchtime += Time.deltaTime;
		} else if (touchtime >= 0) {
			touchtime-=Time.deltaTime;
		}

		if (!touchingPlayer && touchtime == 0) {
			return;
		}


		if (touchingPlayer&&(PC.move>0.3f||PC.move<-0.3f)&&!PC.hanging) {
			pcSpeed = PC.move;

			if(transform.localPosition.x<startingPos.x+1&&transform.localPosition.x>startingPos.x-1){
				transform.localPosition=Vector3.Lerp(transform.localPosition,new Vector3(transform.localPosition.x+pcSpeed,transform.localPosition.y,transform.localPosition.z),Time.deltaTime*3);
				for(int i=0;i<CurtainSegments.Length;i++){
					if(i==0){
					cutSpeed=pcSpeed/2f;
					}else{
						cutSpeed=pcSpeed/(1.55f+i);
					}
					CurtainSegments[i].localPosition=Vector3.Lerp(CurtainSegments[i].localPosition,new Vector3(CurtainSegments[i].localPosition.x+cutSpeed,CurtainSegments[i].localPosition.y,CurtainSegments[i].localPosition.z),Time.deltaTime*5);
				}
			}

		} else {
			transform.localPosition=Vector3.Lerp(transform.localPosition,startingPos,Time.deltaTime*4);

			for(int i=0;i<CurtainSegments.Length;i++){
				CurtainSegments[i].localPosition=Vector3.MoveTowards(CurtainSegments[i].localPosition,new Vector3(startingPos.x,CurtainSegments[i].localPosition.y,CurtainSegments[i].localPosition.z),Time.deltaTime*3);
			}
		}

	}


	void OnTriggerStay2D(Collider2D other){

		if (other.GetComponent<PlayerControls>()!=null) {
			PC=other.GetComponent<PlayerControls>();

		}
	}


}
