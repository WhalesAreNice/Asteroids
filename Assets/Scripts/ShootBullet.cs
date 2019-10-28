using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    public GameObject sofaBulletPrefab;
    Vehicle myShip;

    public List<GameObject> bullets;

    const int bulletCD = 30;
    int currentCD = 0;

    public AudioSource source;
    public AudioClip shootBulletSound;

    // Start is called before the first frame update
    void Start()
    {
        
        myShip = GetComponent<Vehicle>();

        bullets = new List<GameObject>();

        source.clip = shootBulletSound;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Space) && currentCD == 0)
        {
            bullets.Add(Instantiate(sofaBulletPrefab, gameObject.transform.localPosition, Quaternion.identity));
            source.Play();
            currentCD = bulletCD;
        }

        if(currentCD > 0)
            currentCD--;
    }
}
