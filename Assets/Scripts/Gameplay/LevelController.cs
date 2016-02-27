using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private float levelTime = 60f;
    [SerializeField] private int maxPeds = 30;
    private float remainingTime;
    private List<GameObject> activePeds; 

    private int score;
    private bool discoTime = false;             // Is the party turnin' up?

    private PlayerController player;
    private PedestrianPooler pedPooler;

    // UI
    private Text scoreText;
    private Image drunkImage;
    private Text timeText;
    private Text drunkText;

	// Use this for initialization
	void Start ()
	{
	    timeText = GameObject.FindGameObjectWithTag("TimeText").GetComponent<Text>();
	    drunkText = GameObject.FindGameObjectWithTag("DrunkDisplay").GetComponent<Text>();
	    scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
	    player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	    pedPooler = GetComponent<PedestrianPooler>();
        activePeds = new List<GameObject>();
	    remainingTime = levelTime;
        GameObject.DontDestroyOnLoad(this.gameObject);

	    foreach (GameObject ped in pedPooler.GetPedestrians(maxPeds))
	    {
	        activePeds.Add(ped);
	        RandomizePosition(ped);
            ped.SetActive(true);
	    }
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (SceneManager.GetActiveScene().name == "LevelScreen")
	    {
            score = player.GetScore();
            remainingTime -= Time.deltaTime;
            scoreText.text = score.ToString();
	        drunkText.text = player.GetDrunk().ToString();
            timeText.text = "0:" + ((int)remainingTime).ToString();
	        if (remainingTime <= 0)
	        {
	            SceneManager.LoadScene("ScoreScreen");
	        }
	    }
	}

    public int GetScore()
    {
        return score;
    }

    void RandomizePosition(GameObject ped)
    {
        // Place in a position not occupied by player, within the game area, and not colliding with anything
        Vector3 position;
        do
        {
            position = new Vector3(Random.Range(-100, 100), Random.Range(-80, 80), 0);
        } while ((player.transform.position - ped.transform.position).magnitude < 1);
        ped.transform.position = position;
    }
}
