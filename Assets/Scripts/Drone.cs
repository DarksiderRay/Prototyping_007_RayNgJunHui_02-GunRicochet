using System;
using System.Collections;
using System.Collections.Generic;
using CartoonFX;
using DG.Tweening;
using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField] private CFXR_Effect explosionEffect;
    
    void Start()
    {
        var initialScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(initialScale, 0.25f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            InvokeDead();
        }
    }

    private void InvokeDead()
    {
        Instantiate(explosionEffect.gameObject, transform.position, Quaternion.identity);
        transform.DOScale(Vector3.zero, 0.25f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
