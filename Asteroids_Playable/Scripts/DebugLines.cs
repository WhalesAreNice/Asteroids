using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLines : MonoBehaviour
{
    Vehicle myVehicleScript;
    // Start is called before the first frame update
    void Start()
    {
        myVehicleScript = gameObject.GetComponent<Vehicle>();
    }

    // Update is called once per frame
    void Update()
    {
        //velocity line
        Debug.DrawLine(gameObject.transform.position, myVehicleScript.velocity * 20 + gameObject.transform.position, Color.red);
        //direction line
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + myVehicleScript.direction, Color.yellow);
        //origin line
        Debug.DrawLine(gameObject.transform.position, new Vector3(0, 0, 0), Color.blue);
    }
}
