using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandclock : MonoBehaviour
{
    public float slowdownFactor = 0.5f;
    public float effectDuration = 5f;

    private float fixedDeltaTime;
    private bool isActivated = false;

    void Start()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
    }

    void FixedUpdate()
    {
        if (isActivated)
        {
            Time.fixedDeltaTime = fixedDeltaTime * slowdownFactor;
        }
        else
        {
            Time.fixedDeltaTime = fixedDeltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActivated && other.CompareTag("Player"))
        {
            isActivated = true;
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = fixedDeltaTime * slowdownFactor;
            StartCoroutine(DeactivateEffect());
        }
    }

    private IEnumerator DeactivateEffect()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(effectDuration);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = fixedDeltaTime;
        Destroy(gameObject);
    }

}
