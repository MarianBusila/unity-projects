using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    void Start()
    {
        //unlocks the mouse Coursor and makes it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
	public void LoadLevel(string _levelName)
    {
        SceneManager.LoadScene(_levelName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
