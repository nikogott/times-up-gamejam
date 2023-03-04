using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{

    [SerializeField] Collider2D coll;
    SpriteRenderer sr;
    Animator anim;
    CameraShake camShake;

    bool isExploded = false;

    [SerializeField] GameObject explodeParticles;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        camShake = FindObjectOfType<CameraShake>();
        anim = GetComponent<Animator>();
        coll.enabled = false;
        sr.enabled = true;
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            if (!isExploded)
            {
                StartCoroutine(Explode());
            }
        }
        if (collision.CompareTag("Seith"))
        {
            if (!isExploded)
            {
                StartCoroutine(Explode());
            }
        }
    }

    IEnumerator Explode()
    {
        camShake.ShakeCam();
        anim.SetTrigger("explode");
        isExploded = true;
        coll.enabled = true;
        sr.enabled = false;
        Instantiate(explodeParticles, transform.position, Quaternion.identity);


        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }

}
