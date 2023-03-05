using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float currentTime = 0f;
    float startingTime = 8f;

    PlayerHealth player;
    GameObject[] enemies;
    [SerializeField] Text countDownTxt;
    string countdown;

    public bool shouldCountdown;

    private void Start()
    {
        player = FindObjectOfType<PlayerHealth>();
    }

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (shouldCountdown)
        {
            currentTime += Time.deltaTime;
            float timeLeft = startingTime - currentTime;
            countdown = string.Format("{0:00}:{1:00}:{2:00}", Mathf.FloorToInt(timeLeft / 60), Mathf.FloorToInt(timeLeft % 60), Mathf.FloorToInt((timeLeft * 100) % 100));

            if (timeLeft >= 0)
                countDownTxt.text = countdown;
        }

        if (enemies.Length >= 0 && shouldCountdown && currentTime >= 8)
        {
            player.Death();
        }

        if (enemies.Length < 0)
        {
            player.Win();
        }
    }

}