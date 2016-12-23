using UnityEngine;
using System.Collections;

public class DifficultySetting : MonoBehaviour {

	public int difficultyLvl;
	public bool disabled;

	public void CheckDifficulty()
	{
		if (difficultyLvl > GameManager.instance.difficulty) {
			disabled = true;
		} else {
			disabled=false;
		}
	}

}
