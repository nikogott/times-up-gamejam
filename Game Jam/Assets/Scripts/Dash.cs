using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class Dash : MonoBehaviour
{
    private bool isActivated = false;
    public float speedMultiplier = 2;
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
            player.dashSpeed *= speedMultiplier;
            StartCoroutine(DeactivateEffect());
        }
    }

    private IEnumerator DeactivateEffect()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        light.enabled = false;
        yield return new WaitForSeconds(effectDuration);

        player.dashSpeed /= speedMultiplier;
        Destroy(gameObject);
    }
}
