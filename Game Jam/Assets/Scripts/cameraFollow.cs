using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float smoothtime;
    [SerializeField] private Transform playerPos;
    Vector3 velocity = Vector3.zero;
    void Start()
    {

    }
    void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(playerPos.position.x, playerPos.position.y, -10), ref velocity, smoothtime, speed);
    }
}