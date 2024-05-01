using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private bool invertLook;
    [SerializeField] private bool lockLocalRotX;
    [SerializeField] private bool lockLocalRotY;
    [SerializeField] private bool lockLocalRotZ;

    void Awake()
    {
        if (camera == null)
            camera = Camera.main;
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.forward = invertLook? transform.position - camera.transform.position: 
            camera.transform.position - transform.position;
        Vector3 localRot = transform.localEulerAngles;

        localRot.x = lockLocalRotX ? 0 : localRot.x;
        localRot.y = lockLocalRotY ? 0 : localRot.y;
        localRot.z = lockLocalRotZ ? 0 : localRot.z;

        transform.localEulerAngles = localRot;
    }
}
