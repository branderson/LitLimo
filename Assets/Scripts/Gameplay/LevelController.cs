using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private float levelTime = 60f;
    private float remainingTime;

    private int score;
    private bool discoTime = false;             // Is the party turnin' up?

    private PlayerController player;

	// Use this for initialization
	void Start ()
	{
	    player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	    remainingTime = levelTime;
        GameObject.DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    score = player.GetScore();
	    remainingTime -= Time.deltaTime;
	    if (remainingTime <= 0)
	    {
            SceneManager.LoadScene("ScoreScreen");
	    }
	}

    public int GetScore()
    {
        return score;
    }
}
