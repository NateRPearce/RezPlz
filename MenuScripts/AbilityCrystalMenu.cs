using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilityCrystalMenu : GameStateFunctions {

    public GameObject crystalPanel;
    public Image crystalImage;
    public Canvas ACCanvas;
    public Text dialogueText;
    public bool finished;

    void Start()
    {
        FindGM();
        crystalPanel = this.gameObject;
        ACCanvas = GetComponent<Canvas>();
        ACCanvas.enabled = false;
        dialogueText = GetComponentInChildren<Text>();
    }


    void Update () {
	
	}
}
