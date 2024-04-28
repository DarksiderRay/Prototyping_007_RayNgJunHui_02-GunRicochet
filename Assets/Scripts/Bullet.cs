using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float bulletSpeed = 10f;

    [Header("DEBUG")]
    [SerializeField] private float velocity;
    
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(Vector3.forward * bulletSpeed, ForceMode.VelocityChange);
    }

    void Update()
    {
        velocity = rb.velocity.magnitude;
    }
}
