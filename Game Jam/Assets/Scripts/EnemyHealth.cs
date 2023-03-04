using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health;
    float timer = 0.1f;
    [SerializeField] GameObject bloodObj;
    bool shouldSpawnBlood = false;
    [SerializeField] float kbForce;
    PlayerMovement player;
    [SerializeField] GameObject deathParticles;
    Animator anim;

    public GameObject seith;
    void Start()
    {
        anim = GetComponent<Animator>();
        health = 1;
        player = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (health <= 0)
        {
            anim.SetTrigger("death");

            shouldSpawnBlood = true;
            if (shouldSpawnBlood)
            {
                spawnBlood();
                shouldSpawnBlood = false;
            }
        }
    }

    void spawnBlood()
    {
        int bloodAmount = Random.Range(3, 7);


        for (int i = 0; i <= bloodAmount; i++)
        {
            float randPosX = Random.Range(0.2f, 2);
            float randPosY = Random.Range(0.2f, 2);
            Instantiate(bloodObj, new Vector2(transform.position.x + randPosX, transform.position.y + randPosY), Quaternion.identity);

        }
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.25f);
    }

    public void ApplyKnockback(Vector2 direction, float force)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(-direction * force, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Seith") && timer <= 0.1)
        {
            Hit();
        }
        if (collision.CompareTag("Explosion"))
        {
            Hit();
        }
    }

    void Hit()
    {
        {
            anim.SetTrigger("hit");
            health -= 1;
            timer = 1f;
            ApplyKnockback(player.gameObject.transform.position, kbForce);
        }
    }

}
