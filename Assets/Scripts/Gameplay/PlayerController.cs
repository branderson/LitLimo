using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // Components
    private Rigidbody2D rigidbody;

    // Motion
    private Vector2 velocity;            // Forward velocity
    private float acceleration = 0f;        // Acceleration of the car
    private float turnValue = 0f;           // Turn acceleration
    private float direction = 0f;           // Direction in radians
    [SerializeField] private float maxAcceleration = 50f;   // Engine acceleration limit
    private float reverseAcceleration; // Acceleration while reversing
    [SerializeField] private float turnRadius = .5f;         // Speed of turning
    private float sidewaysDrag = .25f;
    private float friction = 2f;

    // Properties
    private float drunkLevel = 100f;          // Score multiplier and self control inhibitor
    private int score = 0;

    private int framesPerDrunkLevel = 30;
    private int framesSinceSoberUp = 0;

	// Use this for initialization
	void Start ()
	{
	    this.rigidbody = GetComponent<Rigidbody2D>();
	    reverseAcceleration = maxAcceleration/1.5f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y);
        HandleInput();
    }

    void FixedUpdate()
    {
        UpdatePosition();
	    UpdateIntoxication();
    }

    void UpdatePosition()
    {
        float tempVel = velocity.magnitude;

        if (velocity.magnitude > 0)
        {
            rigidbody.AddForce(transform.up * -friction);
        }
        if (tempVel < velocity.magnitude)
        {
            rigidbody.velocity = new Vector2(0, 0);
        }
        rigidbody.AddForce(transform.up * acceleration);
        transform.Rotate((velocity.magnitude / 5f) * Vector3.forward * turnValue);

        // Apply sideways drag
         Vector3 dir = transform.InverseTransformDirection(rigidbody.velocity);
         dir.x = dir.x * sidewaysDrag;
         rigidbody.velocity = transform.TransformDirection(dir);
    }

    void HandleInput()
    {
        Accelerate(Input.GetAxis("Acceleration"));
        Turn(-Input.GetAxis("Turn"));
    }

    void UpdateIntoxication()
    {
        Debug.Log("Drunk level: " + drunkLevel);
        if (drunkLevel > 0)
        {
            if (framesSinceSoberUp >= framesPerDrunkLevel)
            {
                framesSinceSoberUp = 0;
                drunkLevel -= 1;
            }
            framesSinceSoberUp += 1;
        }
    }

    void Accelerate(float accel)
    {
        if (accel > 0)
        {
            acceleration = maxAcceleration*accel;
        }
        else if (accel < 0)
        {
            if (acceleration < 0)
            {
                acceleration = reverseAcceleration*accel;
            }
            else
            {
                acceleration = 1.5f * maxAcceleration * accel;
            }
        }
    }

    void Turn(float accel)
    {
        turnValue = turnRadius*accel;
    }

    public void HitPedestrian(PedestrianController ped)
    {
        score += 1 + (int)drunkLevel/4;
        Debug.Log("Score: " + score);
    }

    public void DestroyLiquorStore(LiquorStoreController liq)
    {
        drunkLevel += 25;
        if (drunkLevel > 100)
        {
            drunkLevel = 100;
        }
    }
}
