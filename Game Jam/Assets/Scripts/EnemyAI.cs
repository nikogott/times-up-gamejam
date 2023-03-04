using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject bulletPrefab;
    public Transform gun;
    public Transform shotPos;

    private Rigidbody2D rb2d;

    public float detectionRange = 10f;
    public float shootingRange = 5f;
    public float moveSpeed = 5f;
    public float maxOffset = 3f;
    public float fireRate = 1f;

    float shootCooldown = 0f;
    float shootCooldownDuration = 1f;

    public string gunType = "";
    private float fireTimer;
    private bool canShoot;

    private Vector2 targetPosition;
    private int maxDeviationAngle = 5;
    private int recoilForce = 5;

    void Start()
    {
        Vector2 offset = Random.insideUnitCircle * maxOffset;
        targetPosition = (Vector2)playerTransform.position + offset;

        fireTimer = fireRate;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < detectionRange)
        {
            Vector2 direction = (playerTransform.position - gun.position).normalized;
            gun.right = direction;

            canShoot = (distanceToPlayer < shootingRange);

            if (distanceToPlayer > shootingRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                Vector2 offset = Random.insideUnitCircle * maxOffset;
                targetPosition = (Vector2)playerTransform.position + offset;
            }

            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            if (Quaternion.Angle(gun.rotation, Quaternion.identity) > 0.1f)
            {
                gun.rotation = Quaternion.Lerp(gun.rotation, Quaternion.identity, 0.1f);
            }
        }

        if (canShoot)
        {
            fireTimer -= Time.deltaTime;

            if (fireTimer <= 0f && Time.time > shootCooldown)
            {
                if (gunType == "Pistol")
                {
                    float spread = Random.Range(-1.5f, 1.5f);

                    GameObject bullet = Instantiate(bulletPrefab, shotPos.position, gun.transform.rotation);
                    Vector2 direction = (playerTransform.position - shotPos.position).normalized;
                    direction.x += spread;
                    bullet.GetComponent<Rigidbody2D>().velocity = direction * bullet.GetComponent<Bullet>().speed;

                    fireTimer = fireRate;
                }
                else if (gunType == "Shotgun")
                {
                    for (int i = 0; i < 5; i++)
                    {
                        GameObject bullet = Instantiate(bulletPrefab, shotPos.position, gun.transform.rotation);

                        float randomRotation = Random.Range(-15, 15);
                        bullet.transform.Rotate(0f, 0f, randomRotation);

                        bullet.GetComponent<Rigidbody2D>().AddForce(gun.transform.right * bullet.GetComponent<Bullet>().speed, ForceMode2D.Impulse);
                    }
                }
                shootCooldown = Time.time + shootCooldownDuration;
            }
        }
    }

}