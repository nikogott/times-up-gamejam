using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private float smoothtime;
    [SerializeField] private Transform playerPos;
    Vector3 velocity = Vector3.zero;
    float Z;
    void Start()
    {
        Z = transform.position.z;
    }
    void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(playerPos.position.x, playerPos.position.y, Z), ref velocity, smoothtime, speed);
    }
}