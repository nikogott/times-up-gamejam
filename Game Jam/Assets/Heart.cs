using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Heart : MonoBehaviour
{
    private bool isActivated = false;
    public int healthGiven = 1;
    public float effectDuration = 5f;

    private PlayerMovement player;

    [SerializeField] Light2D light;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActivated && other.CompareTag("Player"))
        {
            isActivated = true;

            player.GetComponent<PlayerHealth>().GetHealth(healthGiven);
            GetComponent<SpriteRenderer>().enabled = false;
            light.enabled = false;

            Time.timeScale = 1f;
            Destroy(gameObject);
        }
    }


}
