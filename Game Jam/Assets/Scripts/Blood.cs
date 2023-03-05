using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    public float followDistance = 10f;
    public float followSpeed = 15f;
    private float constantSpeed = 15f;

    private Transform playerTransform;

    private void Start()
    {
        constantSpeed = followSpeed;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Time.timeScale != 1 && followSpeed < constantSpeed * 4)
        {
            followSpeed *= 4;
        }

        if (Time.timeScale == 1 && followSpeed != constantSpeed)
        {
            followSpeed = constantSpeed;
        }

        if (playerTransform != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= followDistance)
            {
                Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
                transform.position += (Vector3)directionToPlayer * followSpeed * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}