using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    CameraFollow camFollow;


    private void Start()
    {
        camFollow = FindObjectOfType<CameraFollow>();
    }

    public void ShakeCam()
    {
        StartCoroutine(Shake());
    }

    public IEnumerator Shake()
    {
        camFollow.enabled = false;

        int shakeTimes = Random.Range(4, 8);

        Vector3 originalPosition = transform.position;

        for (int i = 0; i < shakeTimes; i++)
        {
            float randomTime = Random.Range(0.05f, 0.15f);

            Vector3 randomOffset = new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), 0);

            transform.position += randomOffset;

            yield return new WaitForSeconds(randomTime);

            transform.position = originalPosition;
        }

        yield return new WaitForSeconds(0.5f);

        camFollow.enabled = true;
    }
}
