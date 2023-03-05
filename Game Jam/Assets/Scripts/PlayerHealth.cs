using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    BatteryBar batteryBar;
    public int battery;
    Animator anim;
    PlayerMovement playerMovement;

    float timer = 0;

    [SerializeField] GameObject deathMenu;

    [SerializeField] GameObject[] hearts;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        anim = GetComponent<Animator>();
        battery = 3;
        //    batteryBar = FindObjectOfType<BatteryBar>();


        for (int i = 0; i < 3; i++)
        {
            hearts[i].SetActive(true);
        }

    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {


        if (battery <= 0)
        {
            Death();
        }

     //  batteryBar.SetBattery(battery);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") || collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        if (!playerMovement.isDashing && timer <= 0)
        {
            anim.SetTrigger("hit");
            battery -= damage;
            timer = 0.5f;

            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < battery)
                {
                    hearts[i].SetActive(true);
                }
                else
                {
                    hearts[i].SetActive(false);
                }
            }
        }
    }

    public void GetHealth(int health)
    {
        if (timer <= 0)
        {
            battery += health;
            timer = 0.5f;

            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < battery)
                {
                    hearts[i].SetActive(true);
                }
                else
                {
                    hearts[i].SetActive(false);
                }
            }
        }
    }


    void Death()
    {
        Time.timeScale = 0;
        deathMenu.SetActive(true);
    }
}
