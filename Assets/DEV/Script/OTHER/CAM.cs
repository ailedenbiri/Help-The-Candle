using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAM : MonoBehaviour
{
    [SerializeField] private Transform target;


    private float smoothSpeed = 1f;
    private Vector3 offset;

    private void Awake()
    {
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.position.x, smoothSpeed), transform.position.y, target.position.z + offset.z);
    }
}
