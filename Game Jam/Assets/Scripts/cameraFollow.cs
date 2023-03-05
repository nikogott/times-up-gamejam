using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private float smoothtime;
    [SerializeField] private Transform playerPos;
    Vector3 velocity = Vector3.zero;
    CountdownTimer timer;

    float Z;
    void Start()
    {
        if(FindObjectOfType<CountdownTimer>() != null)
        timer = FindObjectOfType<CountdownTimer>();

        Z = transform.position.z;
        StartCoroutine(startTimer());
    }
    void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(playerPos.position.x, playerPos.position.y, Z), ref velocity, smoothtime, speed);
    }

    IEnumerator startTimer()
    {
        yield return new WaitForSeconds(3.75f);
        if (timer != null)
        {
            timer.shouldCountdown = true;
        }
    }
}