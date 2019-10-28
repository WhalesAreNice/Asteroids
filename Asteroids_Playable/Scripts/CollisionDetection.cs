using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    //public float test;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AABBCollision(GameObject objA, GameObject objB)
    {
        SpriteRenderer renderA = objA.GetComponent<SpriteRenderer>();
        SpriteRenderer renderB = objB.GetComponent<SpriteRenderer>();

        //Debug.Log(renderB.bounds.max.x);

        return (renderA.bounds.min.x < renderB.bounds.max.x && renderA.bounds.max.x > renderB.bounds.min.x &&
            renderA.bounds.min.y < renderB.bounds.max.y && renderA.bounds.max.y > renderB.bounds.min.y);

        //return true;

        //SpriteInfo scriptA = objA.GetComponent<SpriteInfo>();
        //test = scriptA.minX;

    }

    public bool CircleCollision(GameObject objA, GameObject objB)
    {
        SpriteRenderer renderA = objA.GetComponent<SpriteRenderer>();
        SpriteRenderer renderB = objB.GetComponent<SpriteRenderer>();

        float ACenterX = renderA.bounds.center.x;
        float ACenterY = renderA.bounds.center.y;
        float BCenterX = renderB.bounds.center.x;
        float BCenterY = renderB.bounds.center.y;

        float ASizeX = renderA.bounds.extents.x/2;
        float ASizeY = renderA.bounds.extents.y/2;
        float BSizeX = renderB.bounds.extents.x/2;
        float BSizeY = renderB.bounds.extents.y/2;

        //Debug.Log(AExtendX);
        //Debug.Log(BExtendX);

        //Debug.Log("point" + (AExtendX + BExtendX));
        //Debug.Log(Mathf.Pow(2, ACenterX - BCenterX));
        //Debug.Log(Mathf.Pow(2, ACenterY - BCenterY));
        //Debug.Log(Mathf.Pow(2, ACenterX - BCenterX) + Mathf.Pow(2, ACenterY - BCenterY));
        //Debug.Log(Mathf.Sqrt(Mathf.Pow(2, ACenterX - BCenterX) + Mathf.Pow(2, ACenterY - BCenterY)));


        return ((ASizeX + BSizeX)*2 > (Mathf.Sqrt(Mathf.Pow(ACenterX - BCenterX, 2) + Mathf.Pow(ACenterY - BCenterY, 2))));
        //Debug.Log(Mathf.Sqrt(Mathf.Pow(2, ACenterX - BCenterX) + Mathf.Pow(2, ACenterY - BCenterY)));

        /*
        Debug.Log("points: " + (renderA.bounds.extents.x + renderB.bounds.extents.x));
        Debug.Log("dist: " + Mathf.Sqrt(Mathf.Pow(2, renderA.bounds.center.x - renderB.bounds.center.x) +
            Mathf.Pow(2, renderA.bounds.center.y - renderB.bounds.center.y)));

        Debug.Log("A Center x:" + renderA.bounds.center.x + "  y: " + renderA.bounds.center.y);
        return ((renderA.bounds.extents.x + renderB.bounds.extents.x) > 
            Mathf.Sqrt(Mathf.Pow(2, renderA.bounds.center.x - renderB.bounds.center.x) + 
            Mathf.Pow(2, renderA.bounds.center.y - renderB.bounds.center.y)));
            */
        //return true;
    }
}
