using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreMenuController : MonoBehaviour
{
    private int score;
    private Text scoreText;

	// Use this for initialization
	void Start ()
	{
	    score = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelController>().GetScore();
	    Destroy(GameObject.FindGameObjectWithTag("GameController"));
	    scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
	    scoreText.text = score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void GoToLevel()
    {
        SceneManager.LoadScene("LevelScreen");
    }
}
