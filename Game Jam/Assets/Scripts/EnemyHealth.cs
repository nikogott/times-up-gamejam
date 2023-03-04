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

    public GameObject seith;
    void Start()
    {
        health = 3;
        player = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if(health <= 0)
        {
            shouldSpawnBlood = true;
            if (shouldSpawnBlood)
            {
                spawnBlood();
                shouldSpawnBlood = false;
            }
        }

        float distanceToSeith = Vector2.Distance(transform.position, seith.transform.position);
        if (distanceToSeith < 0.5 && seith.CompareTag("Seith") && timer <= 0.0)
        {
            Debug.Log("Attacked");
            health -= 1;
            timer = 1f;
            ApplyKnockback(player.gameObject.transform.position, kbForce);

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
        Destroy(gameObject);
    }

    public void ApplyKnockback(Vector2 direction, float force)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(-direction * force, ForceMode2D.Impulse);
    }
}
