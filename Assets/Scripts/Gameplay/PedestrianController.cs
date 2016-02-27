using UnityEngine;
using System.Collections;
using System.Runtime.Remoting.Activation;
using UnityEngine.Assertions.Comparers;

public class PedestrianController : MonoBehaviour
{
    [SerializeField] public float SightRadius = 100;
    [SerializeField] public float CalmRadius = 150;
    private float runVelocity = 10f;
    private float walkVelocity = 10f;
    private float direction = 0f;
    private PlayerController player;

    private bool afraid = false;
    private int framesSinceTurn = 0;

    private Rigidbody2D rigidbody;

	// Use this for initialization
	void Start ()
	{
	    rigidbody = GetComponent<Rigidbody2D>();
	    player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    framesSinceTurn += 1;

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
            rigidbody.velocity = -transform.up * runVelocity;
	    }
	    else
	    {
	        if (framesSinceTurn > 300)
	        {
                RotateRandom();
	            framesSinceTurn = 0;
	        }
	        rigidbody.velocity = -transform.up*walkVelocity;
	    }
	}

    private void RotateRandom()
    {
        if (Random.value > .2f)
        {
           RotateAwayFrom(new Vector3(Random.Range(-100000, 100000), Random.Range(-100000, 100000), 0f));
        }
    }

    private void RotateAwayFrom(Vector3 position)
    {
        Vector3 vectorToTarget = position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 5f);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().HitPedestrian(this);
            Destroy(this.gameObject);
        }
    }
}
