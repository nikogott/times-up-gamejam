using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator anim;
    Animation anima;
    float timer = 0.25f;

    public float speed = 1;
    private float constantSpeed = 1;
    CapsuleCollider2D coll;
    void Start()
    {
        coll = GetComponent<CapsuleCollider2D>();
        coll.enabled = false;
        anim = GetComponent<Animator>();
        anima = GetComponent<Animation>();
    }

    void Update()
    {
        if (Time.timeScale != 1 && speed < constantSpeed * 25)
        {
            speed *= 25;
            anima["player_hit"].speed = speed;
        }

        if (Time.timeScale == 1 && speed != constantSpeed)
        {
            speed = constantSpeed;
            anima["player_hit"].speed = speed;
        }

        if (timer > 0)
        {
            timer -= 1 * Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0) && timer <= 0)
        {
            StartCoroutine(attack());
            anim.SetTrigger("hit");
            timer = 0.25f;
        }
    }

    IEnumerator attack()
    {
        coll.enabled = true;
        transform.gameObject.tag = "Seith";

        yield return new WaitForSeconds(.8f);

        transform.gameObject.tag = "Untagged";
        coll.enabled = false;

    }
}
