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

        for (int i = 0; i <= shakeTimes; i++)
        {
            float randomTime = Random.Range(0.05f, 0.15f);

            yield return new WaitForSeconds(randomTime);

            int randomX = Random.Range(-1, 2);
            int randomY = Random.Range(-1, 2);

            transform.position = new Vector3(transform.position.x + randomX, transform.position.y + randomY, -10);
        }

        yield return new WaitForSeconds(0.5f);

        camFollow.enabled = false;
    }
}
