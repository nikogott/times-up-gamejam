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

    private float timer = 5f;

    private Vector2 targetPosition;

    [SerializeField] Transform pistolShotPos;
    [SerializeField] Transform shotgunShotPos;
    [SerializeField] Transform uziShotPos;

    [SerializeField] GameObject pistol;
    [SerializeField] GameObject shotgun;
    [SerializeField] GameObject uzi;

    SpriteRenderer sr;

    void Start()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
        Vector2 offset = Random.insideUnitCircle * maxOffset;
        targetPosition = (Vector2)playerTransform.position + offset;

        fireTimer = fireRate;

        if (gunType.ToLower() == "pistol")
        {
            pistol.SetActive(true);
            shotPos = pistolShotPos;
        }
        else if (gunType.ToLower() == "shotgun")
        {
            shotgun.SetActive(true);
            shotPos = shotgunShotPos;
        }
        else if(gunType.ToLower() == "uzi")
        {
            uzi.SetActive(true);
            shotPos = uziShotPos;
        }
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {

            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer < detectionRange)
            {
                Vector2 direction = (playerTransform.position - gun.position).normalized;
                gun.right = direction;

                if (direction.x < 0)
                {
                    sr.flipX = true;
                    if (gunType.ToLower() == "pistol")
                    {
                        pistol.GetComponent<SpriteRenderer>().flipY = true;
                    }
                    else if (gunType.ToLower() == "shotgun")
                    {
                        shotgun.GetComponent<SpriteRenderer>().flipY = true;
                    }
                    else if (gunType.ToLower() == "uzi")
                    {
                        uzi.GetComponent<SpriteRenderer>().flipY = true;
                    }
                }
                else if (direction.x > 0)
                {
                    sr.flipX = false;
                    if (gunType.ToLower() == "pistol")
                    {
                        pistol.GetComponent<SpriteRenderer>().flipY = false;
                    }
                    else if (gunType.ToLower() == "shotgun")
                    {
                        shotgun.GetComponent<SpriteRenderer>().flipY = false;
                    }
                    else if (gunType.ToLower() == "uzi")
                    {
                        uzi.GetComponent<SpriteRenderer>().flipY = false;
                    }
                }

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
                    if (gunType.ToLower() == "pistol")
                    {
                        float spread = Random.Range(-1.5f, 1.5f);

                        GameObject bullet = Instantiate(bulletPrefab, shotPos.position, gun.transform.rotation);
                        Vector2 direction = (playerTransform.position - shotPos.position).normalized;
                        direction.x += spread;
                        bullet.GetComponent<Rigidbody2D>().velocity = direction * bullet.GetComponent<Bullet>().speed;

                        fireTimer = fireRate;
                    }
                    else if (gunType.ToLower() == "shotgun")
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            GameObject bullet = Instantiate(bulletPrefab, shotPos.position, gun.transform.rotation);

                            float randomRotation = Random.Range(-15, 15);
                            bullet.transform.Rotate(0f, 0f, randomRotation);

                            bullet.GetComponent<Rigidbody2D>().AddForce(gun.transform.right * bullet.GetComponent<Bullet>().speed, ForceMode2D.Impulse);
                            Destroy(bullet, 1f);
                        }
                        fireTimer = fireRate;
                    }
                    else if (gunType.ToLower() == "uzi")
                    {
                        StartCoroutine(ShootUzi());
                    }

                    shootCooldown = Time.time + shootCooldownDuration;
                }
            }
        }
    }
    IEnumerator ShootUzi()
    {
        for (int i = 0; i < 4; i++)
        {
            float spread = Random.Range(-1.5f, 1.5f);

            yield return new WaitForSeconds(0.1f);

            GameObject bullet = Instantiate(bulletPrefab, shotPos.position, gun.transform.rotation);
            Vector2 direction = (playerTransform.position - shotPos.position).normalized;
            direction.x += spread;
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bullet.GetComponent<Bullet>().speed;
        }
    }
}