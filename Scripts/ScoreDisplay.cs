using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI scoredisplay;
    public Spotmanager spotmanager;
    public GameObject text;

    public int score;
    
    // Start is called before the first frame update
    void Start()
    {
        scoredisplay = text.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        score = spotmanager.score;
        scoredisplay.text = "Score: $" + score.ToString();
    }
}