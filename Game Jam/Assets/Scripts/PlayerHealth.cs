using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    BatteryBar batteryBar;
    float battery;
    Animator anim;
    PlayerMovement playerMovement;

    [SerializeField] GameObject deathMenu;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        anim = GetComponent<Animator>();
        battery = 75;
        batteryBar = FindObjectOfType<BatteryBar>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L) && battery <= 50)
        {
            TakeDamage(25);
        }
    }

    private void FixedUpdate()
    {
        batteryBar.SetBattery(battery);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(25);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet") || collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(25);
        }
    }

    void TakeDamage(int damage)
    {
        if(battery <= 10)
        {
            Death();
        }
        if (!playerMovement.isDashing)
        {
            anim.SetTrigger("hit");
            battery -= damage;
        }
    }

    void Death()
    {
        Time.timeScale = 0;
        deathMenu.SetActive(true);
    }
}
