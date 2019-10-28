using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHealth : MonoBehaviour
{
    GameObject myShip;
    Vehicle myShipScript;
    int health;
    Vector3 healthBarScale;
    // Start is called before the first frame update
    void Start()
    {
        myShip = GameObject.Find("Ship");
        myShipScript = myShip.GetComponent<Vehicle>();
        healthBarScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        health = myShipScript.health;

        if(health == 3)
        {
            healthBarScale.x = 1f;
        }else if(health == 2)
        {
            healthBarScale.x = 1f/3*2;
        }
        else if(health == 1)
        {
            healthBarScale.x = 1f/3;
        }
        else
        {
            healthBarScale.x = 0;
        }
        transform.localScale = healthBarScale;
    }
}
