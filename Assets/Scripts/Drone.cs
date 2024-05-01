using System;
using System.Collections;
using System.Collections.Generic;
using CartoonFX;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField] private CFXR_Effect explosionEffect;
    [SerializeField] private AudioSource explosionAudioSource;

    public delegate void OnInvokeDead();

    public OnInvokeDead onInvokeDead;
    
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
            StartCoroutine(InvokeDead());
        }
    }

    [Button]
    private void StartInvokeDead()
    {
        StartCoroutine(InvokeDead());
    }
    
    private IEnumerator InvokeDead()
    {
        Instantiate(explosionEffect.gameObject, transform.position, Quaternion.identity);
        onInvokeDead?.Invoke();
        //transform.DOScale(Vector3.zero, 0.1f);
        transform.localScale = Vector3.zero;

        explosionAudioSource.Play();
        yield return new WaitUntil(() => !explosionAudioSource.isPlaying);
        Destroy(gameObject);
    }
}
