using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public float fillDuration = 0.5f;

    private Coroutine fillCoroutine;

    public void SetBattery(float battery)
    {
        if (fillCoroutine != null)
        {
            StopCoroutine(fillCoroutine);
        }

        fillCoroutine = StartCoroutine(FillSliderCoroutine(battery));
    }

    private IEnumerator FillSliderCoroutine(float targetValue)
    {
        float elapsedTime = 0f;
        float startValue = slider.value;

        while (elapsedTime < fillDuration)
        {
            elapsedTime += Time.deltaTime;
            slider.value = Mathf.Lerp(startValue, targetValue, elapsedTime / fillDuration);
            fill.color = gradient.Evaluate(slider.normalizedValue);
            yield return null;
        }

        slider.value = targetValue;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
