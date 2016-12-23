using UnityEngine;
using System.Collections;

public class CameraBehaviorScript : GameStateFunctions {

	private int facingMod;
	public float cameraPositionX;
	public float cameraPositionY;
	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;
	public float trueXmin;
	private float trueYmin;
	public float trueXmax;
	private float trueYmax;
	public float xdistApart;
	public float ydistApart;
	public MainCamScript MCS;
	public bool cameraEvent;
	public Vector3 EventLocation;
	public float CamSpeed;
	public bool maxPosOverride;
	public GameObject deadIndicator;
	public bool[] nearLight=new bool[3];
	public bool[] onLight=new bool[3];
	public float nearLightTime;
	public float onLightTime;
	public float camSize;

    public Camera cam;
    bool camControlLock;

	void Start(){
		MCS = GetComponent<MainCamScript> ();
		FindGM ();
		CamSpeed = 2;
		camSize = GetComponent<Camera>().orthographicSize;
        cam = Camera.main.GetComponent<Camera>();
    }



    void CamNearTarget()
    {
        if (GM.PM.eventHappening)
        {
            return;
        }
        if (transform.position.x > cameraPositionX + 50|| transform.position.x < cameraPositionX - 50)
        {
            GM.PM.controlsLocked = true;
            camControlLock = true;
        }else if(camControlLock)
        {
            GM.PM.controlsLocked = false;
            camControlLock = false;
        }
    }

	void Update () {
		trueXmin = xMin + cam.orthographicSize * Screen.width / Screen.height;
		trueXmax = xMax - cam.orthographicSize * Screen.width / Screen.height;
		trueYmin = yMin + cam.orthographicSize;
		trueYmax = yMax -cam.orthographicSize;
		SelectedPlayerFacingCheck ();
		SetCameraTarget ();
		BoundryCheck ();
		EventOverrideCheck ();
		CameraTracking ();
	}

	void FixedUpdate(){
        CamNearTarget();
        if (cam.orthographicSize < camSize) {
			cam.orthographicSize+=0.2f;
		}
		if (cam.orthographicSize > camSize) {
			cam.orthographicSize-=0.2f;
		}
	}

	private void CameraTracking(){
			transform.position = Vector3.Lerp (transform.position, new Vector3 (cameraPositionX, cameraPositionY, transform.position.z), Time.deltaTime * CamSpeed);		
	}



	private void SelectedPlayerFacingCheck(){
		if (GM.camNum == 1) 
		{
			GM.selectedPlayerPos=GM.PM.Player1;
		} else if(GM.camNum == 2){
			GM.selectedPlayerPos=GM.PM.Player2;
		}
        if (GM.raceMode) {
            facingMod = 0;
        }
        else if (GM.selectedPlayerPos.localScale.x > 0) {
			facingMod=12;
		}else{
			facingMod=-12;
		}
        if (GM.camNum == 4)
        {
            GM.selectedPlayerPos = transform;
            facingMod = 0;
        }
    }


	private void SetCameraTarget(){
			cameraPositionX = GM.selectedPlayerPos.position.x+facingMod;
			cameraPositionY = GM.selectedPlayerPos.position.y;
	}


	private void BoundryCheck(){
		if (cameraPositionX< trueXmin&&!maxPosOverride) {
			cameraPositionX= trueXmin;		
		}
		if (cameraPositionY< trueYmin) {
            cameraPositionY = trueYmin;		
		}
		if (cameraPositionX> trueXmax && !maxPosOverride) {
            cameraPositionX = trueXmax;		
		}
		if (cameraPositionY> trueYmax && !maxPosOverride) {
            cameraPositionY = trueYmax;		
		}
	}
	private void EventOverrideCheck(){
		if(cameraEvent){
			if(EventLocation.x>trueXmax&&!maxPosOverride) {
				cameraPositionX = trueXmax;	
			}else{
				cameraPositionX=EventLocation.x;
			}
			if(EventLocation.y>trueYmax&&!maxPosOverride) {
				cameraPositionY = trueYmax;	
			}else{
				cameraPositionY = EventLocation.y;
			}
			if(EventLocation.x<trueXmin&&!maxPosOverride) {
				cameraPositionX = trueXmin;	
			}else{
				cameraPositionX=EventLocation.x;
			}
			if(EventLocation.y<trueYmin&&!maxPosOverride) {
				cameraPositionY = trueYmin;	
			}else{
				cameraPositionY = EventLocation.y;
			}
		}
	}

}