using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    // necessary variables to generate tunnel
    public AudioCollecter audio;

    public float degree, scale;

    // ElementPostion to sign position of each element 
    private Vector2 ElementPosition;
    private TrailRenderer trailRenderer;

    private int count;




    // To generate stuff, calculate its position data of each element

    // number means count of generate object
    private Vector2 CalculateData(float degree, float scale, int count)
    {
        // to holds more values, use double instead of float
        // calculate where each element will be

        double angle = count * (degree * Mathf.Deg2Rad);

        // get radius
        float r = scale * Mathf.Sqrt(count);

        // get x, cast double to float
        float x = r * (float)System.Math.Cos(angle);

        // get y, so do the cast here
        float y = r * (float)System.Math.Sin(angle);

        // return by storing in vector2 and return
        Vector2 result = new Vector2(x, y);

        return result;
    }


    void Awake()
    {

        // get TrailRenderer on awake
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        ElementPosition = CalculateData(degree, scale, count);
        transform.localPosition = new Vector3(ElementPosition.x, ElementPosition.y, 0);
        count ++;
    }
}
