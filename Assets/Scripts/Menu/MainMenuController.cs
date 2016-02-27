using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButtonDown("Submit"))
	    {
            GoToLevel();
	    }
	}

    public void GoToLevel()
    {
        SceneManager.LoadScene("LevelScreen");
    }
}
