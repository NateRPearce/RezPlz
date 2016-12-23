using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SwitchtoFourK : MonoBehaviour {


    public Text[] allText;
    public bool in4k;
    Text buttonTxt;

    void Awake()
    {
        buttonTxt = GetComponentInChildren<Text>();
    }



    public void SwitchTo4K()
    {
        Debug.Log("Clicked");
        allText = FindObjectsOfType<Text>();
        for(int i=0;i<allText.Length;i++)
        {
            if (!in4k)
            {
                allText[i].transform.localScale = new Vector3(allText[i].transform.localScale.x * 2, allText[i].transform.localScale.y * 2, 1);
                buttonTxt.text = "4K on";
            }else
            {
                allText[i].transform.localScale = new Vector3(allText[i].transform.localScale.x / 2, allText[i].transform.localScale.y / 2, 1);
                buttonTxt.text = "4k off";
            }
        }
        in4k = !in4k;
    }
}
