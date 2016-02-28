using UnityEngine;
using System.Collections;

public class ScoreMenuController : MonoBehaviour
{
    private int score;

	// Use this for initialization
	void Start ()
	{
	    score = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelController>().GetScore();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
