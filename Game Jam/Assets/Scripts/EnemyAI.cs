using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float detectionRange = 5f;
    public float shootingRange = 3f;
    public float shootingDelay = 1f;
    public GameObject bulletPrefab;
    public Transform shootPosition;

    private Rigidbody2D rb;
    private Transform playerTransform;
    private bool canShoot;
    private float timeSinceLastShot;
    private Vector2 moveDirection;
    private float moveDuration;
    private float timeSinceLastMove;
    private float rotationSpeed = 180f;
    [SerializeField] Animator animGun;
    [SerializeField] Animator anim;
    float currentSpeed;

    [SerializeField] GameObject gunObj;
    [SerializeField] ParticleSystem shootParticles; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        canShoot = false;
        timeSinceLastShot = 0f;
        timeSinceLastMove = 0f;
        moveDirection = Vector2.zero;
        moveDuration = 0f;
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        currentSpeed = rb.velocity.sqrMagnitude;
        anim.SetFloat("speed", currentSpeed);

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if(distanceToPlayer > 5 && distanceToPlayer < detectionRange)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;

            gunObj.transform.right = direction;
            transform.Translate(direction * 5 * Time.deltaTime);
            canShoot = (distanceToPlayer < shootingRange);
        }
        else if(distanceToPlayer > detectionRange)
        {
            canShoot = false;
        }
        /*if (distanceToPlayer < detectionRange)
        {
            if (distanceToPlayer < 5)
            {
                Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
                float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                gunObj.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                moveDirection = directionToPlayer;

                rb.velocity = moveDirection * moveSpeed;

            }
            canShoot = (distanceToPlayer < shootingRange);
        }
        else
        {
            canShoot = false;
        }*/

        if (canShoot)
        {
            if (timeSinceLastShot >= shootingDelay)
            {
                animGun.SetTrigger("shoot");
                shootParticles.Play();

                GameObject bullet = Instantiate(bulletPrefab, shootPosition.position, gunObj.transform.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = (playerTransform.position - transform.position).normalized * bullet.GetComponent<Bullet>().speed;
                timeSinceLastShot = 0f;

            }
            else
            {
                timeSinceLastShot += Time.deltaTime;
            }
        }
    }

}
