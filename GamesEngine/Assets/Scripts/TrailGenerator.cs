using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailGenerator : MonoBehaviour
{
    // Some object we gonna use
    public AudioCollecter AudioData;


    // Generate point with TrailRenderer as its trails
    public float degree, scale;

    #region IterationTracker
    // step between each element of point, use iteration to control it
    public int maxIteration;
    public int step;
    private int currentIteration;
    #endregion


    #region LerpSys
    // use public int to change from unity, private number to assign value
    public int numberStart;
    private int number;

    private Vector2 ElementPosition;

    // start position and end position the trail will be
    private Vector3 startposition, endposition;

    // Use timer start from 0 to 1 with certain speed, the speed base on incoming audio
    private float lerpPosTimer,lerpPosSpeed;


    // Lerp trail between min and max speed
    public Vector2 lerpSpeedMin_Max;

    // To handle how the number increase or decrease
    public AnimationCurve lerpPosAnimCurve;

    // to change our lerpping speed by the band we want
    public int lerpPosBand;
    #endregion


    private void Awake()
    {
        // sign the value from public to private
        number = numberStart;
        SetLerpPosition();
    }

    void SetLerpPosition()
    {
        // calculate the postition of all elements and record its start/end position
        ElementPosition = CalculateData(degree, scale, number);
        startposition = this.transform.localPosition;
        endposition = new Vector3(ElementPosition.x, ElementPosition.y, 0);
    }

    private Vector2 CalculateData(float degree, float scale, int number)
    {
        // to holds more values, use double instead of float
        // calculate where each element will be

        double angle = number * (degree * Mathf.Deg2Rad);

        // get radius
        float r = scale * Mathf.Sqrt(number);

        // get x, cast double to float
        float x = r * (float)System.Math.Cos(angle);

        // get y, so do the cast here
        float y = r * (float)System.Math.Sin(angle);

        // return by storing in vector2 and return
        Vector2 result = new Vector2(x, y);

        return result;
    }

    // Update is called once per frame
    void Update()
    {
        // lerp speed between min and max by animation curve controlled band.
        lerpPosSpeed = Mathf.Lerp(lerpSpeedMin_Max.x, lerpSpeedMin_Max.y, lerpPosAnimCurve.Evaluate(AudioData.audioBandBuffer[lerpPosBand]));

        // apply speed to timer
        lerpPosTimer += Time.deltaTime * lerpPosSpeed;

        // use timer to lerp between start position and end position
        transform.localPosition = Vector3.Lerp(startposition, endposition, Mathf.Clamp01(lerpPosTimer));

        // limit lerpPosTimer between 0 and 1, once its hit 1 ,rest its position
        if(lerpPosTimer >= 1)
        {
            lerpPosTimer -= 1;
            SetLerpPosition();

            // iteration change its value
            number += step;
            currentIteration++;
        }

        // Normally lerpping, so keep sign new position of arrived element 
        if(currentIteration > 0 && currentIteration < maxIteration)
        {
            SetLerpPosition();
        }
        // current iteration has hit 0 or maxiteration, which means this loop have done
        // resign a new loop to it
        else
        {
            number = numberStart;
            currentIteration = 0;
            SetLerpPosition();
        }

    }
}
