using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControlsMenu : SetButton {


    public GameObject xboxBtn;
    public GameObject pcBtn;
    public Text xboxTxt;
    public Text pcTxt;

    void OnEnable()
    {
        ES = EventSystem.current;
        SetCurrentButton(xboxBtn);
    }

    void Update()
    {
        if (ES == null)
        {
            return;
        }
        if(ES.currentSelectedGameObject== xboxBtn)
        {
            xboxBtn.GetComponent<Text>().color= new Color(1, 1, 1, 1);
            pcBtn.GetComponent<Text>().color = new Color(1, 1, 1, 0.25f);

            xboxTxt.color = new Color(1, 1, 1, 1);
            pcTxt.color = new Color(1, 1, 1, 0);
        }
        else
        {
            xboxBtn.GetComponent<Text>().color = new Color(1, 1, 1, 0.25f);
            pcBtn.GetComponent<Text>().color = new Color(1, 1, 1, 1);
            pcTxt.color = new Color(1, 1, 1, 1);
            xboxTxt.color = new Color(1, 1, 1, 0);
        }
    }
}
