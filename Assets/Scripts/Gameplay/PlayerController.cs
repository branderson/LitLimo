﻿using UnityEngine;
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
    [SerializeField] private float drunkScale = 3f;
    private Vector3 startPosition;

    // Properties
    private float drunkLevel = 100f;          // Score multiplier and self control inhibitor
    private float leaning = 0f;
    private int score = 0;

    private int framesPerDrunkLevel = 30;
    private int framesSinceSoberUp = 0;

	// Use this for initialization
	void Start ()
	{
	    this.rigidbody = GetComponent<Rigidbody2D>();
	    startPosition = transform.position;
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
        UpdateLeaning();
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
        if (Input.GetButtonDown("Teleport"))
        {
            Teleport();
        }
        Accelerate(Input.GetAxis("Acceleration"));
        Turn(-Input.GetAxis("Turn"));
        if (Input.GetButtonDown("Escape"))
        {
            Application.Quit();
        }
    }

    void UpdateIntoxication()
    {
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

    void UpdateLeaning()
    {
//        leaning = Mathf.PingPong(.5f*Time.time, .5f) - .25f;
        leaning = Mathf.PingPong(.75f*Time.time, 2f)*2 - 2f;
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
        if (acceleration >= 0)
        {
            turnValue = turnRadius*accel + drunkLevel*drunkScale*leaning/100;
        }
        else if (acceleration < 0)
        {
            turnValue = -turnRadius*accel - drunkLevel*drunkScale*leaning/100;
        }
    }

    void Teleport()
    {
        transform.position = startPosition;
    }

    public void HitPedestrian(PedestrianController ped)
    {
        score += 1 + (int)drunkLevel/4;
    }

    public void DestroyLiquorStore(LiquorStoreController liq)
    {
        drunkLevel += 25;
        if (drunkLevel > 100)
        {
            drunkLevel = 100;
        }
    }

    public int GetScore()
    {
        return score;
    }

    public int GetDrunk()
    {
        return (int)drunkLevel;
    }
}
