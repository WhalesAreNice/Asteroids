using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    Vector3 asteroidPosition;
    public Vector3 asteroidVelocity;
    public bool firstIteration = true;
    // Start is called before the first frame update
    void Start()
    {
        asteroidVelocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        asteroidVelocity /= 40;
        asteroidPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        asteroidPosition += asteroidVelocity;

        if (asteroidPosition.x < -7.5f)
            asteroidPosition.x = 7.5f;
        if (asteroidPosition.x > 7.5f)
            asteroidPosition.x = -7.5f;
        if (asteroidPosition.y < -5.5f)
            asteroidPosition.y = 5.5f;
        if (asteroidPosition.y > 5.5f)
            asteroidPosition.y = -5.5f;

        transform.position = asteroidPosition;
    }
}
