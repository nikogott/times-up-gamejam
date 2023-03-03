using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float speed = 5f;

    private Rigidbody2D rb2d;
    private SpriteRenderer sprite;

    public Transform weapon;

    public float currentSpeed;

    float timer = 0.4f;

    bool canMove = true;

    Animator anim;

    [SerializeField] ParticleSystem dustParticles;
    [SerializeField] ParticleSystem dashParticles;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        currentSpeed = rb2d.velocity.SqrMagnitude();

        var deltaX = Input.GetAxisRaw("Horizontal");
        var deltaY = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(deltaX, deltaY).normalized;

        if (canMove)
        {
            rb2d.velocity = movement * speed * Time.fixedDeltaTime;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;


    }

    private void Update()
    {
        anim.SetFloat("speed", currentSpeed);

        if(timer > 0)
        {
            timer -= 1 * Time.deltaTime;
        }

        if(currentSpeed > 0.1f)
        {
            dustParticles.Play();
        }
        else
        {
            dustParticles.Stop();
        }

        if (Input.GetKeyDown("f") && timer <= 0)
        {
            StartCoroutine(dash());

            var deltaX = Input.GetAxisRaw("Horizontal");
            var deltaY = Input.GetAxisRaw("Vertical");

            Vector2 movement = new Vector2(deltaX, deltaY).normalized;

            if(Input.GetAxisRaw("Horizontal") > 0)
            {
                rb2d.velocity = new Vector2(2, 0) * speed * Time.fixedDeltaTime;
            }
            else if(Input.GetAxisRaw("Horizontal") < 0)
            {
                rb2d.velocity = new Vector2(-2, 0) * speed * Time.fixedDeltaTime;
            }
            else
            {
                if(transform.localScale.x == -1.5f)
                {
                    rb2d.velocity = new Vector2(-2, 0) * speed * Time.fixedDeltaTime;

                }
                else
                {
                    rb2d.velocity = new Vector2(2, 0) * speed * Time.fixedDeltaTime;

                }
            }


            timer = 0.4f;
        }

    }

    IEnumerator dash()
    {
        anim.SetTrigger("dash");
        speed = speed * 1.75f;
        canMove = false;
        dashParticles.Play();

        yield return new WaitForSeconds(0.2f);

        dashParticles.Stop();
        canMove = true;
        speed = speed / 1.75f;
    }

}
