using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // Components
    private Transform transform;
    private Rigidbody2D rigidbody;

    // Motion
    private Vector2 velocity;            // Forward velocity
    private float acceleration = 0f;        // Acceleration of the car
    private float turnValue = 0f;           // Turn acceleration
    private float direction = 0f;           // Direction in radians
    private float maxAcceleration = 100f;   // Engine acceleration limit
    private float reverseAcceleration = 50f; // Acceleration while reversing
    private float turnRadius = .5f;         // Speed of turning
    private float sidewaysDrag = .25f;

    // Properties
    private float drunkLevel = 0f;          // Score multiplier and self control inhibitor

	// Use this for initialization
	void Start ()
	{
	    this.transform = GetComponent<Transform>();
	    this.rigidbody = GetComponent<Rigidbody2D>();
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
    }

    void UpdatePosition()
    {
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
                acceleration = maxAcceleration*accel;
            }
        }
    }

    void Turn(float accel)
    {
        turnValue = turnRadius*accel;
    }

    void HitPedestrian(PedestrianController ped)
    {
        
    }
}
