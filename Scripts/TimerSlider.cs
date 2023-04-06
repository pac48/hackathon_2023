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
    public TextMeshProUGUI countdownText;
    public GameObject text;
    public int secondsRemaining;
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.minValue = 0f;
        slider.maxValue = totalTime;

        countdownText = text.GetComponent<TextMeshProUGUI>();

        currentTime = totalTime; // Set the current time to the total time at start
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        slider.value = currentTime;

        float percentRemaining = currentTime / totalTime;

        // Update the slider color based on percentage of time remaining
        Color color;
        if (percentRemaining > 0.5f) {
            color = Color.Lerp(Color.yellow, Color.green, (percentRemaining - 0.5f) * 2f);
            slider.fillRect.GetComponent<Image>().color = color;
        } else if (percentRemaining < 0.5f){
            color = Color.Lerp(Color.red, Color.yellow, percentRemaining * 2f);
            slider.fillRect.GetComponent<Image>().color = color;
        }
        

        // Update the countdown text
        secondsRemaining = Mathf.CeilToInt(currentTime);
        countdownText.text = secondsRemaining.ToString() + " Seconds";
    }
}