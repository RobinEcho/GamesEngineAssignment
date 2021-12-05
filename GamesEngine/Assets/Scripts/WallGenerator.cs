using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    // necessary variables to generate tunnel
    public AudioCollecter audio;
    public float degree, scale;
    private TrailRenderer trailRenderer;

    void Awake()
    {

        // get TrailRenderer on awake
        trailRenderer = GetComponent<TrailRenderer>();
    }
}
