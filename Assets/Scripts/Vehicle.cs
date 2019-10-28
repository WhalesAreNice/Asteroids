using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{

    // Unnecessary
    //public float speed;                       // Speed of the vehicle, not needed anymore

    // Necessary
    public float accelRate;                     // Small, constant rate of acceleration
    public Vector3 vehiclePosition;             // Local vector for movement calculation
    public Vector3 direction;                   // Way the vehicle should move
    public Vector3 velocity;                    // Change in X and Y
    public Vector3 acceleration;                // Small accel vector that's added to velocity
    public float angleOfRotation;               // 0 
    public float maxSpeed;                      // 0.5 per frame, limits mag of velocity

    public int health;
    public int mana;
    public GameObject gameover;
    public GameObject playButton;
    Vector3 center;

    public bool shieldsOn;
    public GameObject shield;
    const int shieldCDReset = 60;
    int shieldCD;

    public AudioClip thrusterSound;
    public AudioSource source;

    public AudioSource source2;
    public AudioClip shieldSound;

    // Use this for initialization
    void Start ()
    {
        vehiclePosition = new Vector3(0, 0, 0);     // Or you could say Vector3.zero
        direction = new Vector3(1, 0, 0);           // Facing right
        velocity = new Vector3(0, 0, 0);            // Starting still (no movement)
        health = 3;
        center = new Vector3(0, 0, 0);
        mana = 900;
        shieldsOn = false;
        shieldCD = 0;
        source.clip = thrusterSound;
        source2.clip = shieldSound;
    }
	
	// Update is called once per frame
	void Update ()
    {
        RotateVehicle();

        Drive();

        SetTransform();

        PositionWrap();

        DestroyShip();

        RecoverMana();

        ShieldsUp();
        
    }

    /// <summary>
    /// Changes / Sets the transform component
    /// </summary>
    public void SetTransform()
    {
        // Rotate vehicle sprite
        transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);

        // Set the transform position
        transform.position = vehiclePosition;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Drive()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            accelRate = 0.001f;
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
        else
        {
            accelRate = 0;
            velocity.x *= 0.975f;
            velocity.y *= 0.975f;
            if (source.isPlaying)
            {
                source.Stop();
            }
        }

        // Accelerate
        // Small vector that's added to velocity every frame
        acceleration = accelRate * direction;

        // We used to use this, but acceleration will now increase the vehicle's "speed"
        // Velocity will remain intact from one frame to the next
        //velocity = direction * speed;             // Unnecessary
        velocity += acceleration;

        
        // Limit velocity so it doesn't become too large
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // Add velocity to vehicle's position
        vehiclePosition += velocity;
    }

    public void RotateVehicle()
    {
        // Player can control direction
        // Left arrow key = rotate left by 2 degrees
        // Right arrow key = rotate right by 2 degrees
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            angleOfRotation += 2;
            direction = Quaternion.Euler(0, 0, 2) * direction;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            angleOfRotation -= 2;
            direction = Quaternion.Euler(0, 0, -2) * direction;
        }
    }

    public void PositionWrap()
    {
        
        if (vehiclePosition.x < -7)
            vehiclePosition.x = 7;
        if (vehiclePosition.x > 7)
            vehiclePosition.x = -7;
        if (vehiclePosition.y < -5.5f)
            vehiclePosition.y = 5.5f;
        if (vehiclePosition.y > 5.5f)
            vehiclePosition.y = -5.5f;
            
        //not sure how to do it in a way where it detects the screen width in world space, googling it gives some confusing stuff that doesn't work when I try it
        
    }

    void RecoverMana()
    {
        if (mana < 900)
        {
            mana++;
        }
        else if (mana >= 900)
        {
            mana = 900;
        }
    }

    void ShieldsUp()
    {
        if (Input.GetKey(KeyCode.LeftShift) && mana >= 5 && shieldCD <= 0)
        {
            source2.Play();
            shieldsOn = true;
            mana -= 10;
            shield.GetComponent<Renderer>().enabled = true;
        } else if (Input.GetKeyUp(KeyCode.LeftShift) || (Input.GetKey(KeyCode.LeftShift) && mana < 60))
        {
            if (source2.isPlaying)
            {
                source2.Stop();
            }
            shieldCD = shieldCDReset;
            shieldsOn = false;
            shield.GetComponent<Renderer>().enabled = false;
            shieldCD--;
        }
        else //catches all other possiblities 
        {
            if (source2.isPlaying)
            {
                source2.Stop();
            }
            shieldsOn = false;
            shield.GetComponent<Renderer>().enabled = false;
            shieldCD--;
        }
    }

    void DestroyShip()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
            Instantiate(gameover, center, Quaternion.identity);
            Instantiate(playButton, center, Quaternion.identity);
        }
    }
}
