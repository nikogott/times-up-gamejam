using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;

    private PlayerMovement player;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>();
    }
    void Update()
    {
        Vector3 mousePosScreen = Input.mousePosition;

        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(new Vector3(mousePosScreen.x, mousePosScreen.y, transform.position.z - Camera.main.transform.position.z));

        Vector3 targetDir = target.position - transform.position;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);

        Vector3 direction = mousePosWorld - transform.position;
        float angleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angleZ));

        if (angleZ > 90 || angleZ < -90)
        {
            player.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            player.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
