using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraContoller : MonoBehaviour
{
    [SerializeField]
    public Transform target;


    private void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

}