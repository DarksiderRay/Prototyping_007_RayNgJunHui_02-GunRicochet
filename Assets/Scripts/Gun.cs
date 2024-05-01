using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gun : MonoBehaviour
{
    [SerializeField] private ParticleSystem shootingSystem;
    [SerializeField] private Transform bulletSpawnPoint;
    //[SerializeField] private ParticleSystem impactParticleSystem;
    [SerializeField] private TrailRenderer bulletTrail;
    [SerializeField] private float shootDelay = 0.1f;
    [SerializeField] private float speed = 100;
    [SerializeField] private LayerMask mask;
    [SerializeField] private bool bouncingBullets;
    [SerializeField] private float bounceDistance = 10f;

    [Header("Input")] 
    [SerializeField] private OVRInput.RawButton shootButton;

    [Header("SFX")] 
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioClip audioClip_GunShoot;
    [SerializeField] private List<AudioClip> audioClips_Ricochet;

    private float lastShootTime;

    private void Update()
    {
        if (OVRInput.GetDown(shootButton))
            Shoot();
    }

    [Button]
    public void Shoot()
    {
        sfxAudioSource.PlayOneShot(audioClip_GunShoot);
        
        if (lastShootTime + shootDelay < Time.time)
        {
            shootingSystem.Play();
            Vector3 direction = bulletSpawnPoint.transform.forward;
            TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            if (Physics.Raycast(bulletSpawnPoint.position, direction, out RaycastHit hit, float.MaxValue))
            {
                if (hit.collider.gameObject.layer.Equals(LayerMask.NameToLayer("BouncableWall")))
                {
                    StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, bounceDistance, true));
                }
                else
                {
                    StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, Vector3.Distance(hit.point, bulletSpawnPoint.position), false));
                }
                
            }
            else
            {
                StartCoroutine(SpawnTrail(trail, direction * 100, Vector3.zero, bounceDistance, false));
            }

            lastShootTime = Time.time;
        }
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 hitPoint, Vector3 hitNormal, float bounceDistance,
        bool madeImpact)
    {
        Vector3 startPosition = trail.transform.position;
        Vector3 direction = (hitPoint - startPosition).normalized;

        float distance = Vector3.Distance(startPosition, hitPoint);
        float startingDistance = distance;

        while (distance > 0)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hitPoint, 1 - (distance / startingDistance));
            distance -= Time.deltaTime * speed;

            yield return null;
        }

        trail.transform.position = hitPoint;
        if (madeImpact)
        {
            //Instantiate(impactParticleSystem, hitPoint, Quaternion.LookRotation(hitNormal));
            
            trail.GetComponentInChildren<AudioSource>().PlayOneShot(GetRicochetSFXClip());

            if (bouncingBullets && bounceDistance > 0)
            {
                Vector3 bounceDirection = Vector3.Reflect(direction, hitNormal);
                if (Physics.Raycast(hitPoint, bounceDirection, out RaycastHit hit, bounceDistance))
                {
                    
                    if (hit.collider.gameObject.layer.Equals(LayerMask.NameToLayer("BouncableWall")))
                    {
                        yield return StartCoroutine(SpawnTrail(
                            trail,
                            hit.point,
                            hit.normal,
                            bounceDistance - Vector3.Distance(hit.point, hitPoint),
                            true));
                    }
                    else
                    {
                        // Debug.Log("HAHA");
                        yield return StartCoroutine(SpawnTrail(
                            trail,
                            hit.point,
                            hit.normal,
                            0,
                            false));

                        
                    }
                    
                }
                else
                {
                    yield return StartCoroutine(SpawnTrail(
                        trail,
                        hitPoint + bounceDirection * bounceDistance,
                        Vector3.zero,
                        0,
                        false));
                }
            }
        }
        
        Destroy(trail.gameObject, trail.time);
    }

    private AudioClip GetRicochetSFXClip()
    {
        return audioClips_Ricochet[Random.Range(0, audioClips_Ricochet.Count)];
    }
}
