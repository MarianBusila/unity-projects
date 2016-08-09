using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Text highScoreText;
	// Use this for initialization
	void Start () {
        highScoreText.text = "Highscore: " + (int)PlayerPrefs.GetFloat("Highscore");
	}

    public void ToGame()
    {
        SceneManager.LoadScene("Game");
    }
}
