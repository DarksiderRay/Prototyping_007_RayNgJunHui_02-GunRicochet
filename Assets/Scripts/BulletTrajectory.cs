using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class BulletTrajectory : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float totalDistance;
    [SerializeField] private LayerMask mask;

    // Update is called once per frame
    void Update()
    {
        CreateTrajectory();
    }

    [Button]
    void CreateTrajectory()
    {
        var targetHit = false;
        
        var positions = new List<Vector3>();
        Vector3 lastPos, currentPos;
        lastPos = transform.position;
        positions.Add(lastPos);

        var dist = totalDistance;
        var dir = lineRenderer.transform.forward;

        while (dist > 0)
        {
            if (Physics.Raycast(lastPos, dir, out var hit))
            {
                currentPos = hit.point;
                positions.Add(currentPos);

                if (!hit.collider.gameObject.layer.Equals(LayerMask.NameToLayer("BouncableWall")))
                {
                    if (hit.collider.gameObject.CompareTag("Target"))
                        targetHit = true;
                    
                    break;
                }

                
                
                dist -= Vector3.Distance(lastPos, currentPos);
                dir = Vector3.Reflect(dir, hit.normal);
                lastPos = currentPos;
            }
            else
            {
                currentPos = lastPos + dir * dist;
                positions.Add(currentPos);
                break;
            }
        }

        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());

        lineRenderer.startColor = targetHit ? Color.green : Color.red;
        lineRenderer.endColor = targetHit ? Color.green : Color.red;
    }
}
