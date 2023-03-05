using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCombat : MonoBehaviour
{
    public GameObject gate, gate1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gate.SetActive(false);
            gate1.SetActive(false);
        }
    }
}
