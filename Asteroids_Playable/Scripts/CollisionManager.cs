using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public GameObject asteroidSpawner;          
    AsteroidSpawning asteroidSpawningScript;    
    List<GameObject> asteroids;
    AsteroidMovement asteroidMovementScript;

    public GameObject ship;                     
    Vehicle shipVehicleScript;
    SpriteRenderer shipRender;

    public GameObject shield;
    SpriteRenderer shieldRender;

    ShootBullet shipShootBulletScript;
    List<GameObject> bullets;

    CollisionDetection detect;

    public AudioSource source;
    public AudioClip explosionSound;

    int score;
    // Start is called before the first frame update
    void Start()
    {
        detect = gameObject.GetComponent<CollisionDetection>();
        asteroidSpawningScript = asteroidSpawner.GetComponent<AsteroidSpawning>();
        shipVehicleScript = ship.GetComponent<Vehicle>();
        shipShootBulletScript = ship.GetComponent<ShootBullet>();
        score = 0;
        source.clip = explosionSound;
    }

    // Update is called once per frame
    void Update()
    {
        if (ship != null)
        {
            ShipCollision();
            BulletOutOfBounds();
            BulletCollision();

            if (shipVehicleScript.shieldsOn)
            {
                ShieldCollision();
            }
        }
    }

    //problematic
    void BulletOutOfBounds()
    {
        bullets = shipShootBulletScript.bullets;
        if (bullets.Count > 0)
        {
            for(int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].transform.position.x > 8 || bullets[i].transform.position.x < -8 || bullets[i].transform.position.y > 6.5f || bullets[i].transform.position.y < -6.5f)
                {
                    Destroy(bullets[i]);
                    bullets.RemoveAt(i);
                }
            }
        }
    }

    void BulletCollision()
    {
        bullets = shipShootBulletScript.bullets;
        if (bullets.Count > 0)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                SpriteRenderer bulletRender;
                if (bullets[i] != null)
                {
                    bulletRender = bullets[i].GetComponent<SpriteRenderer>();
                    for (int j = 0; j < asteroids.Count; j++)
                    {
                        SpriteRenderer asteroidRender = asteroids[j].GetComponent<SpriteRenderer>();
                        if (detect.AABBCollision(bullets[i], asteroids[j]))
                        {
                            source.Play();
                            asteroidMovementScript = asteroids[j].GetComponent<AsteroidMovement>();
                            //this makes sure that the asteroid is the first so it splits
                            if (asteroidMovementScript.firstIteration)
                            {
                                //creating 2 new asteroids
                                GameObject asteroid1 = Instantiate(asteroidSpawningScript.asteroidPrefabs[Random.Range(0,asteroidSpawningScript.asteroidPrefabs.Count)], asteroids[j].transform.position, Quaternion.identity);
                                asteroid1.transform.localScale /= 2;
                                AsteroidMovement asteroid1Movement = asteroid1.GetComponent<AsteroidMovement>();
                                asteroid1Movement.asteroidVelocity = asteroids[j].GetComponent<AsteroidMovement>().asteroidVelocity * Random.Range(0.9f, 1.1f);
                                asteroid1Movement.firstIteration = false;
                                asteroids.Add(asteroid1);

                                GameObject asteroid2 = Instantiate(asteroidSpawningScript.asteroidPrefabs[Random.Range(0, asteroidSpawningScript.asteroidPrefabs.Count)], asteroids[j].transform.position, Quaternion.identity);
                                asteroid2.transform.localScale /= 2;
                                AsteroidMovement asteroid2Movement = asteroid2.GetComponent<AsteroidMovement>();
                                asteroid2Movement.asteroidVelocity = asteroids[j].GetComponent<AsteroidMovement>().asteroidVelocity * Random.Range(0.9f, 1.1f);
                                asteroid2Movement.firstIteration = false;
                                asteroids.Add(asteroid2);
                            }

                            //GameObject asteroid2 = Instantiate(asteroidSpawningScript.asteroidPrefabs[asteroidSpawningScript.asteroidPrefabIndex], asteroids[j].transform.position, Quaternion.identity);
                            Destroy(bullets[i]);
                            bullets.RemoveAt(i);
                            if (asteroidMovementScript.firstIteration)
                            {
                                score += 20;
                            }
                            else {
                                score += 50;
                            }
                            Destroy(asteroids[j]);
                            asteroids.RemoveAt(j);
                            asteroidSpawningScript.asteroids = asteroids;
                            shipShootBulletScript.bullets = bullets;
                            break;
                        }
                    }
                }
            }
        }
    }

    void ShipCollision()
    {
        asteroids = asteroidSpawningScript.asteroids;   //set the asteroid list with asteroids from the spawningScript
        for (int i = 0; i < asteroids.Count; i++)
        {
            shipRender = ship.GetComponent<SpriteRenderer>();
            SpriteRenderer planetRender = asteroids[i].GetComponent<SpriteRenderer>();
            if (detect.AABBCollision(ship, asteroids[i]))
            {
                source.Play();
                shipVehicleScript.health--;             //subtract health from ship
                Destroy(asteroids[i]);                  //destroys the current asteroid that collided with the ship
                asteroids.RemoveAt(i);                  //remove the asteroid from the list
                shipRender.color = Color.red;           //change the ship's color to indicate that it was hit
            }
            else
            {
                shipRender.color = Color.white;         //resets the color of the ship after it has been hit from the previous frame
            }
        }
        /*
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            for(int i = 0; i < planets.Count; i++)
            {
                shipRender = ship.GetComponent<SpriteRenderer>();
                SpriteRenderer planetRender = planets[i].GetComponent<SpriteRenderer>();
                if (detect.CircleCollision(ship, planets[i]))
                {
                    shipRender.color = Color.red;
                    planetRender.color = Color.red;
                }
                else
                {
                    shipRender.color = Color.white;
                    planetRender.color = Color.white;
                }
            }
        }
        */
    }

    void ShieldCollision()
    {
        asteroids = asteroidSpawningScript.asteroids;   //set the asteroid list with asteroids from the spawningScript
        for (int i = 0; i < asteroids.Count; i++)
        {
            shieldRender = shield.GetComponent<SpriteRenderer>();
            SpriteRenderer planetRender = asteroids[i].GetComponent<SpriteRenderer>();
            if (detect.AABBCollision(shield, asteroids[i]))
            {
                source.Play();
                asteroidMovementScript = asteroids[i].GetComponent<AsteroidMovement>();
                if (asteroidMovementScript.firstIteration)
                {
                    score += 20;
                }
                else
                {
                    score += 50;
                }
                Destroy(asteroids[i]);                  //destroys the current asteroid that collided with the ship
                asteroids.RemoveAt(i);                  //remove the asteroid from the list
            }
        }
    }
    private void OnGUI()
    {
        
        GUI.Box(new Rect(800, 10, 150, 50), "Score: " + score);
    }
}
