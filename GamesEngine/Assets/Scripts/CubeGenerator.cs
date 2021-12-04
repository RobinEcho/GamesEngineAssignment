using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    public AudioCollecter audio;

    public GameObject originalOBJ;

    private GameObject audiocube;

    int cubeCount,blankCount;

    float Distance;

    // Start is called before the first frame update
    void Start()
    {
        // To calculate where should the cube instantiate
        cubeCount = audio.audioBand.Length;
        blankCount = cubeCount + (cubeCount - 1);
        Distance = this.GetComponent<RectTransform>().rect.height/ blankCount;

        for (int i = 0; i < cubeCount; i++)
        {
            audiocube = Instantiate(originalOBJ, new Vector3(this.transform.position.x + i * Distance, this.transform.position.y, this.transform.position.z), Quaternion.identity);
            audiocube.GetComponent<CubeAnimator>().band = i;
            audiocube.transform.SetParent(this.transform);
        }
    }
}
