using UnityEngine;
using System.Collections;
using System.Runtime.Remoting.Activation;

public class PedestrianController : MonoBehaviour
{
    [SerializeField] public float SightRadius = 100;
    [SerializeField] public float CalmRadius = 150;
    private float velocity = 10f;
    private float direction = 0f;
    private PlayerController player;

    private bool afraid = false;

    private Rigidbody2D rigidbody;

	// Use this for initialization
	void Start ()
	{
	    rigidbody = GetComponent<Rigidbody2D>();
	    player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        // Check against position of player
	    if ((player.transform.position - transform.position).magnitude < SightRadius)
	    {
	        afraid = true;
	    }
	    if ((player.transform.position - transform.position).magnitude > CalmRadius)
	    {
	        afraid = false;
	    }

	    if (afraid)
	    {
            RotateAwayFrom(player.transform.position);
            rigidbody.velocity = -transform.up * velocity;
	    }
	    else
	    {
	    }
	}

    private void RotateAwayFrom (Vector3 position)
    {
        Vector3 vectorToTarget = position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 5f);
    }
}
