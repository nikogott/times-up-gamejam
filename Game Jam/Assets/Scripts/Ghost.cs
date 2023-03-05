using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class Ghost : MonoBehaviour
{
    private bool isActivated = false;
    public float effectDuration = 5f;
    float ogLensDisStrength;
    float ogShootingRange;

    private PlayerMovement player;

    [SerializeField] Light2D light;

    Volume volume;
    LensDistortion lensDistortion;

    GameObject[] enemies;

    float ogDetectionRange;
    float ogMoveSpeed;

    bool isDone = false;
    bool hasStarted = false;

    private void Start()
    {
        volume = FindAnyObjectByType<Volume>();

        volume.profile.TryGet(out lensDistortion);

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        ogLensDisStrength = lensDistortion.intensity.value;

        player = FindObjectOfType<PlayerMovement>();

        foreach (GameObject enemy in enemies)
        {
            ogDetectionRange = enemy.GetComponent<EnemyAI>().detectionRange;
            ogShootingRange = enemy.GetComponent<EnemyAI>().shootingRange;
            ogMoveSpeed = enemy.GetComponent<EnemyAI>().moveSpeed;

        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActivated && other.CompareTag("Player"))
        {
            isActivated = true;
            Time.timeScale = 1.01f;
            StartCoroutine(DeactivateEffect());
        }
    }

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (!isDone && hasStarted)
        {
            float targetIntensity = 0.5f;
            lensDistortion.intensity.value = Mathf.Lerp(lensDistortion.intensity.value, targetIntensity, Time.deltaTime * 2.75f);
        }
        else if (isDone)
        {
            float targetIntensity = ogLensDisStrength;
            lensDistortion.intensity.value = Mathf.Lerp(lensDistortion.intensity.value, targetIntensity, Time.deltaTime * 2.75f);
        }
    }

    private IEnumerator DeactivateEffect()
    {
        hasStarted = true;

        GetComponent<SpriteRenderer>().enabled = false;
        light.enabled = false;
        Color color = player.GetComponent<SpriteRenderer>().color;
        color.a = 0.3f;
        player.GetComponent<SpriteRenderer>().color = color;

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyAI>().detectionRange = 0;
            enemy.GetComponent<EnemyAI>().moveSpeed = 0;
            enemy.GetComponent<EnemyAI>().shootingRange = 0;
        }

        yield return new WaitForSeconds(effectDuration);
        isDone = true;

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyAI>().detectionRange = ogDetectionRange;
            enemy.GetComponent<EnemyAI>().moveSpeed = ogMoveSpeed;
            enemy.GetComponent<EnemyAI>().shootingRange = ogShootingRange;
        }

        color.a = 1f;
        player.GetComponent<SpriteRenderer>().color = color;
        Time.timeScale = 1f;

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

}
