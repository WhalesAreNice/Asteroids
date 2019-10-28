using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteInfo : MonoBehaviour
{
    SpriteRenderer mySpriteRenderer;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float spriteSizeX;
    public float spriteSizeY;

    public float MinX
    {
        get
        {
            return minX;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        minX = mySpriteRenderer.bounds.min.x;
        maxX = mySpriteRenderer.bounds.max.x;
        minY = mySpriteRenderer.bounds.min.y;
        maxY = mySpriteRenderer.bounds.max.y;
        spriteSizeX = mySpriteRenderer.bounds.size.x;
        spriteSizeY = mySpriteRenderer.bounds.size.y;
    }
}
