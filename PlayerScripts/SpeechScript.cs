using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeechScript : MonoBehaviour {

    public Transform speechBub;
    SpriteRenderer sr;
    public Text words;
	// Use this for initialization
	void Start () {
        sr = speechBub.GetComponent<SpriteRenderer>();
	}

    public void Speak(float xScale, float yScale, string speech, float duration)
    {
        StartCoroutine("ShowBubble",duration);
        float xOffset =xScale-speechBub.localScale.x;
        speechBub.localScale = new Vector3(xScale, yScale, 0);
        speechBub.position = new Vector3(speechBub.position.x+xOffset, speechBub.position.y, speechBub.position.z);
        words.text = speech.Replace("  ", "\n");
    }

    IEnumerator ShowBubble(float duration)
    {
        sr.enabled = true;
        words.color = Color.black;
        yield return new WaitForSeconds(duration);
        sr.enabled = false;
        words.color = Color.clear;
    }
}
