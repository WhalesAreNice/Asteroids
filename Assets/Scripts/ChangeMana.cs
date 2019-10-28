using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMana : MonoBehaviour
{
    GameObject myShip;
    Vehicle myShipScript;
    int mana;
    Vector3 manaBarScale;
    // Start is called before the first frame update
    void Start()
    {
        myShip = GameObject.Find("Ship");
        myShipScript = myShip.GetComponent<Vehicle>();
        manaBarScale = new Vector3(0.75f, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        mana = myShipScript.mana;

        manaBarScale.x = mana / 900f * 0.75f;

        transform.localScale = manaBarScale;
    }
}
