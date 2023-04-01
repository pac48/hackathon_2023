using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TimerSlider : MonoBehaviour
{
    public float totalTime;
    public float currentTime;

    private Slider slider;
    private TextMeshProUGUI countdownText;
    public GameObject text;
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.minValue = 0f;
        slider.maxValue = totalTime;

        countdownText = text.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        slider.value = currentTime;

        float percentRemaining = currentTime / totalTime;
        float sliderColorValue = 1 - percentRemaining;

        if (currentTime <= 0)
        {
            // Timer is up, handle this as desired
        }

        // Update the countdown text
        int secondsRemaining = Mathf.CeilToInt(currentTime);
        countdownText.text = secondsRemaining.ToString();
    }
}
