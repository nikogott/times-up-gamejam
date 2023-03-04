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
        if(Input.GetKeyDown(KeyCode.O))
        {
            battery -= 25;
        }
        if(Input.GetKeyDown(KeyCode.L) && battery <= 50)
        {
            battery += 25;
        }
    }

    private void FixedUpdate()
    {
        batteryBar.SetBattery(battery);
    }
}
