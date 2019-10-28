using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    GameObject myShip;
    Vehicle myShipScript;
    Vector3 bulletVelocity;
    // Start is called before the first frame update
    void Start()
    {
        myShip = GameObject.Find("Ship");
        myShipScript = myShip.GetComponent<Vehicle>();
        bulletVelocity = (myShipScript.direction) / 4;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += bulletVelocity;
        //if (transform.position.x > 8 || transform.position.x < -8 || transform.position.y > 6.5f || transform.position.y < -6.5f) {
        //    Destroy(gameObject); 
        //}
    }
}
