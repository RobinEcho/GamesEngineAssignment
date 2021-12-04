using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAnimator : MonoBehaviour
{
    public int band;
    public float startScale, scaleMultiplier;

    void Start()
    {
        startScale = 1;
        scaleMultiplier = 10;
    }


    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x, (AudioCollecter.bandBuffer[band] * scaleMultiplier) + startScale, transform.localScale.z);
    }
}
