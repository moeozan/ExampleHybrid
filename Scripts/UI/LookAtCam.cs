using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCam : MonoBehaviour
{
    private Camera mainCam;
    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void LateUpdate()
    {
        transform.LookAt(-mainCam.transform.position);
    }

}
