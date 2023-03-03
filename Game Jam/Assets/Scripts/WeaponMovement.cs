using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    public float rotationSpeed = 5f;

    private PlayerMovement player;
    [SerializeField] GameObject seith;
    private Camera mainCamera;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        transform.parent = null;
        mainCamera = Camera.main;
    }
    void Update()
    {
        transform.position = player.transform.position;

        Vector3 mousePosScreen = Input.mousePosition;
        Vector3 mousePosWorld = mainCamera.ScreenToWorldPoint(new Vector3(mousePosScreen.x, mousePosScreen.y, transform.position.z - mainCamera.transform.position.z));

        Vector3 direction = mousePosWorld - transform.position;
        float angleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction) * Quaternion.Euler(0, 0, 90);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (angleZ > 90 || angleZ < -90)
        {
            player.transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
            seith.transform.localScale = new Vector3(0.6f, -0.6f, 0.6f);

        }
        else
        {
            player.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            seith.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        }
    }
}
