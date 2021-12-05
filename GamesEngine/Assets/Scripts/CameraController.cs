using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Object that camera gonna followed.
    public Transform Target;
    public AudioCollecter Audio;

    // Varibales that controls camera move speed, distance between followed object and camera
    public float moveSpeed, Distance,mouseSensitivity;

    // private float to record rotate info
    float RotX, RotY = 0f;
    bool CanRotate;


    // Start is called before the first frame update
    void Start()
    {
        mouseSensitivity = 1.5f;
        CanRotate = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Only space pressed, user can rotate, otherwise no, switch states by press space
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(CanRotate)
            {
                CanRotate = false;
            }
            else
            {
                CanRotate = true;
            }
            
        }

        if(CanRotate)
        {
            // Camera controller, for 3D camera, its always rotate, no change on position.

            // get input of mouse then calculate by sensitivity
            RotY = Input.GetAxis("Mouse X") * mouseSensitivity;
            RotX += Input.GetAxis("Mouse Y") * mouseSensitivity;

            // Use eulerangle to rotate camera, freeze z Axis
            this.transform.eulerAngles = new Vector3(-RotX, this.transform.eulerAngles.y + RotY, 0);
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }


        // Change Position of target object base on amplitude of audio, this means higher amplitude we have, the faster object gonna move

        // x and y will not change, only z will change by time, which create the tunnel as expect
        Target.position = new Vector3(Target.position.x, Target.position.y, Target.position.z + (Audio.AmplitudeBuffer * moveSpeed));

            


    }
}
