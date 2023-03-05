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

    public GameObject[] gadgets;
    private bool isDead = false;
    public GameObject seith;

    CountdownTimer countdown;
    void Start()
    {
        anim = GetComponent<Animator>();
        health = 1;
        player = FindObjectOfType<PlayerMovement>();
        if (FindObjectOfType<CountdownTimer>() != null)
        {
            countdown = FindObjectOfType<CountdownTimer>();
        }
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (health == 0 && !isDead)
        {
            anim.SetTrigger("death");

            isDead = true;
            spawnBlood();
            int chance = Random.Range(1, 11);
            Debug.Log(chance);
            if (chance == 1)
            {
                int dropItme = Random.Range(0, 5);
                Instantiate(gadgets[dropItme], transform.position, Quaternion.identity);
            }
        }
    }

    void spawnBlood()
    {
        int bloodAmount = Random.Range(4, 11);


        for (int i = 0; i <= bloodAmount; i++)
        {
            float randPosX = Random.Range(0.2f, 2);
            float randPosY = Random.Range(0.2f, 2);
            Instantiate(bloodObj, new Vector2(transform.position.x + randPosX, transform.position.y + randPosY), Quaternion.identity);

        }
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        if (countdown != null)
        {
            countdown.currentTime -= 0.5f;
        }
        Destroy(gameObject);
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
        }
    }

}
