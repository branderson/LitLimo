using UnityEngine;
using System.Collections;

public class BloodSplatController : MonoBehaviour
{
    [SerializeField] private int lifespan = 1200;
    [SerializeField] private float spread = 1f;

    private Rigidbody2D rigidbody;

	// Use this for initialization
	void Start ()
	{
	    rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (lifespan == 0)
	    {
	        gameObject.SetActive(false);
	    }
	    lifespan--;
	}

    public void RandomizePosition(Vector3 position)
    {
        transform.position = new Vector3(position.x + Random.Range(-spread, spread), position.y + Random.Range(-spread, spread), 0);
    }
}
