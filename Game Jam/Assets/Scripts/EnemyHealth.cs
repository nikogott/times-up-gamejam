using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health;
    float timer = 0.1f;
    [SerializeField] GameObject bloodObj;
    bool shouldSpawnBlood = false;
    void Start()
    {
        health = 3;
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= 1 * Time.deltaTime;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Seith"))
        {
            health -= 1;
            timer = .1f;
        }
    }
}
