using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Sandclock : MonoBehaviour
{
    public float slowdownFactor = 0.5f;
    public float effectDuration = 5f;

    private float fixedDeltaTime;
    private bool isActivated = false;

    public bool timeIsSlowed = false;

    [SerializeField] Light2D light;

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
            timeIsSlowed = true;
            isActivated = true;
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = fixedDeltaTime * slowdownFactor;
            StartCoroutine(DeactivateEffect());
        }
    }

    private IEnumerator DeactivateEffect()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        light.enabled = false;
        yield return new WaitForSeconds(effectDuration);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = fixedDeltaTime;
        timeIsSlowed = false;
        Destroy(gameObject);
    }

}
