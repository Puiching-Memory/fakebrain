using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepCameraFllow : MonoBehaviour
{
    public Transform SecondCamera;

    void Update()
    {
        SecondCamera.position = transform.position;
    }
}
