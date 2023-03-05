using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float speed = 5f;
    public float constantSpeed = 5f;

    private Rigidbody2D rb2d;
    private SpriteRenderer sprite;

    public float mana;

    public Transform weapon;

    private float currentSpeed;
    public float dashSpeed;

    float elapsed = 0;

    float timer = 1f;

    bool canMove = true;

    Animator anim;

    [SerializeField] AudioSource footSteps;

    [SerializeField] ParticleSystem dustParticles;
    [SerializeField] ParticleSystem dashParticles;

    Collider2D coll;

    ManaBar manaBar;

    GameObject weaponObj;

    public bool isDashing = false;

    public bool isTutorial = false;

    void Start()
    {
        mana = 100;
        constantSpeed = speed;
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        manaBar = FindObjectOfType<ManaBar>();
    }

    void FixedUpdate()
    {
        if (Time.timeScale < 1 && speed < constantSpeed * 10)
        {
            speed *= 10;
        }

        if (Time.timeScale == 1 && speed != constantSpeed)
        {
            speed = constantSpeed;
        }

        manaBar.SetMana(mana);

        currentSpeed = rb2d.velocity.SqrMagnitude();

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            footSteps.Play();
        }


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

        if (timer > 0)
        {
            timer -= 1 * Time.deltaTime;
        }

        elapsed += Time.deltaTime;
        if (elapsed >= .4f && mana < 100)
        {
            elapsed = elapsed % .4f;
            mana += 1;
        }

        if (currentSpeed > 0.1f)
        {
            dustParticles.Play();
        }
        else
        {
            dustParticles.Stop();
        }

        if (Input.GetMouseButtonDown(1) && timer <= 0 && mana > 24)
        {
            if (Input.GetMouseButtonDown(1) && timer <= 0 && mana > 24)
            {
                Vector3 mousePosScreen = Input.mousePosition;
                Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(new Vector3(mousePosScreen.x, mousePosScreen.y, transform.position.z - Camera.main.transform.position.z));
                Vector3 direction = mousePosWorld - transform.position;

                direction.Normalize();

                float speed = dashSpeed;
                Vector3 velocity = direction * speed;

                rb2d.velocity = velocity;

                StartCoroutine(dash());
                mana -= 25;
                timer = 1f;
            }
        }

    }

    IEnumerator dash()
    {
        isDashing = true;
        anim.SetTrigger("dash");
        speed = speed * 1.75f;
        canMove = false;
        dashParticles.Play();

        yield return new WaitForSeconds(0.2f);

        isDashing = false;
        dashParticles.Stop();
        canMove = true;
        speed = speed / 1.75f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Blood"))
        {
            if (mana < 98)
                mana += 3;
        }
        if(collision.CompareTag("SeithHolder"))
        {
            FindObjectOfType<PlayerAttack>().enabled = true;
            FindObjectOfType<WeaponMovement>().enabled = true;
            FindObjectOfType<WeaponMovement>().GetComponent<Collider2D>().enabled = false;
        }
    }

}
