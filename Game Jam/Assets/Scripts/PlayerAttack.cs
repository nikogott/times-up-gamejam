using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator anim;
    float timer = 0.25f;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        if(timer > 0)
        {
            timer -= 1 * Time.deltaTime;
        }

        if(Input.GetMouseButtonDown(0) && timer <= 0)
        {
            StartCoroutine(attack());
            anim.SetTrigger("hit");
            timer = 0.25f;
        }
    }

    IEnumerator attack()
    {
        transform.gameObject.tag = "Seith";

        yield return new WaitForSeconds(0.5f);

        transform.gameObject.tag = "Untagged";

    }

}
