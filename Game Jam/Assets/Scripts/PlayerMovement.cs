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
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        var deltaX = Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime;
        var deltaY = Input.GetAxisRaw("Vertical") * Time.fixedDeltaTime;

        rb2d.velocity = new Vector2(deltaX * speed, deltaY * speed);

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        
    }
}
