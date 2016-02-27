using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // Components
    private Transform transform;
    private Rigidbody2D rigidbody;

    // Motion
//    private float velocity = 0f;            // Forward velocity
    private float acceleration = 0f;        // Acceleration of the car
    private float direction = 0f;           // Direction in radians
    private float friction = 5f;            // Friction of tires on road
    private float maxAcceleration = 100f;   // Engine acceleration limit

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
        HandleInput();
	}

    void FixedUpdate()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        this.rigidbody.AddForce(transform.forward * (acceleration - friction) * Time.deltaTime);
    }

    void HandleInput()
    {
        if (Input.GetButton("Accelerate"))
        {
            Accelerate(50f);
        }

        if (Input.GetButton("Deccelerate"))
        {
            Accelerate(-10f);
        }
    }

    void Accelerate(float accel)
    {
        acceleration += accel;

        // Cap max forward/reverse acceleration
        if (acceleration > maxAcceleration)
        {
            acceleration = maxAcceleration;
        }
        else if (acceleration < -maxAcceleration / 4)
        {
            acceleration = -maxAcceleration / 4;
        }
    }

    void HitPedestrian(PedestrianController ped)
    {
        
    }
}
