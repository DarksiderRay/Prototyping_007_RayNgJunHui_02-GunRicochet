using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    [SerializeField] private MRUK mruk;
    [SerializeField] private FindSpawnPositions_Custom findSpawnPositions;

    private void Awake()
    {
        mruk.SceneLoadedEvent?.AddListener(() =>
        {
            StartSpawnDrones(5);
        });
    }

    private void StartSpawnDrones(int count)
    {
        findSpawnPositions.StartSpawn(count, out var spawnedDrones);

        foreach (var spawnedDrone in spawnedDrones)
        {
            if (!spawnedDrone.TryGetComponent<Drone>(out var drone))
                continue;

            drone.onInvokeDead += () =>
            {
                StartSpawnDrones(1);
            };
        }
    }
}
