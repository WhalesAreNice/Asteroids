using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawning : MonoBehaviour
{
    public List<GameObject> asteroidPrefabs;
    public int asteroidPrefabIndex;
    GameObject myShip;

    public List<GameObject> asteroids;

    Vector3 asteroidPosition;

    const int asteroidSpawnTimer = 120;
    int currentSpawnTimer = 0;

    void Start()
    {
        myShip = GameObject.Find("Ship");
        asteroidPosition = new Vector3(0, 0, 0);
        asteroids = new List<GameObject>();
        //make 2 initial asteroids
        SpawnAsteroid();
        SpawnAsteroid();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentSpawnTimer == 0)
        {
            SpawnAsteroid();
            currentSpawnTimer = asteroidSpawnTimer;
        }

        if (currentSpawnTimer > 0)
            currentSpawnTimer--;
    }

    void SpawnAsteroid()
    {
        if (myShip != null)
        {
            //these 2 do while loops makes sure that the place where the asteroids are spawned aren't 
            //too close to the ship, which can damage the ship on spawn
            do
            {
                asteroidPosition.x = Random.Range(-6f, 6f);
            } while (Mathf.Abs(asteroidPosition.x - myShip.transform.position.x) < 2);

            do
            {
                asteroidPosition.y = Random.Range(-3.25f, 3.25f);
            } while (Mathf.Abs(asteroidPosition.y - myShip.transform.position.y) < 1.5f);

            //gets a random prefab out of the prefab list
            asteroidPrefabIndex = Random.Range(0, asteroidPrefabs.Count);

            asteroids.Add(Instantiate(asteroidPrefabs[asteroidPrefabIndex], asteroidPosition, Quaternion.identity));
        }
    }
}
