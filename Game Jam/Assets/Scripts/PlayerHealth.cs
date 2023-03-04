using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    BatteryBar batteryBar;
    float battery;
    void Start()
    {
        battery = 75;
        batteryBar = FindObjectOfType<BatteryBar>();
    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.L) && battery <= 50)
        {
            battery += 25;
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
            battery -= 25;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet") || collision.gameObject.CompareTag("Enemy"))
        {
            battery -= 25;

        }
    }
}
