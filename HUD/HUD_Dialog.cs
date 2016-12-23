using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HUD_Dialog : GameStateFunctions {

	public Image speechBubble;
	public Text dialogue;

	void Start () {
		FindGM ();
		speechBubble.color = Color.clear;
		dialogue.color = Color.clear;
		if (name == "ArcanProfile") {
			GM.HD [1] = this;
		} else {
			GM.HD [0] = this;
		}
	}

	public void ShowDialogue(float time){
		StartCoroutine ("Show_Hide_Dialog", time);
	}

	public void Talk(string sentence, float width, float heigth,int x,int y,float duration){
		RectTransform bubbleRect = speechBubble.GetComponent<RectTransform> ();
		bubbleRect.sizeDelta= new Vector2(width,heigth);
		bubbleRect.anchoredPosition3D = new Vector3 (x, y, 0);
		dialogue.text = sentence.Replace("  ", "\n");
		ShowDialogue (duration);
	}



	IEnumerator Show_Hide_Dialog(float t){
		if (name == "ArcanProfile") {
			GM.AHB.anim.SetBool ("Talking", true);
		} else {
			GM.ZHB.anim.SetBool ("Talking", true);
		}

		speechBubble.color = new Color(1,1,1,0.9f);
		dialogue.color = Color.black;
		yield return new WaitForSeconds(t);
		if (name == "ArcanProfile") {
			GM.AHB.anim.SetBool ("Talking", false);
		} else {
			GM.ZHB.anim.SetBool ("Talking", false);
		}
		speechBubble.color = Color.clear;
		dialogue.color = Color.clear;
	}
}
