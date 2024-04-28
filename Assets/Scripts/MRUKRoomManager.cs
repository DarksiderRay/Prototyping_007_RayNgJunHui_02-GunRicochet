using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;
using UnityEngine.Assertions;

public class MRUKRoomManager : MonoBehaviour
{
    [SerializeField] private MRUK mruk;

    [Header("MRUK Room Config")]
    [SerializeField] private PhysicMaterial meshPhysicMat;

    private void Awake()
    {
        Assert.IsNotNull(mruk);

        Assert.IsNotNull(meshPhysicMat);

        mruk.SceneLoadedEvent.AddListener(ApplyPhysicsMaterial);
    }

    private void ApplyPhysicsMaterial()
    {
        var room = mruk.GetCurrentRoom();
        var meshColliders = room.GetComponentsInChildren<Collider>();
        foreach (var col in meshColliders)
        {
            col.material = meshPhysicMat;
        }
    }
}
