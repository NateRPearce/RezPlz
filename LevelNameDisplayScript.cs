using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelNameDisplayScript : MonoBehaviour
{

    public Text LevelName;
    int l;
    // Update is called once per frame

    void FixedUpdate()
    {
        if (l != SceneManager.GetActiveScene().buildIndex - 2)
        {
            l = SceneManager.GetActiveScene().buildIndex - 2;
            if (l <= 0)
            {
                LevelName.text = "";
            }
            else {
                LevelName.text = "Level " + l;
            }
        }
    }
}
