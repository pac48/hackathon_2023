using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public ScoreDisplay scoredisplay;
    public TimerSlider timerslider;
    public TextMeshProUGUI position1;
    public TextMeshProUGUI score1;
    public TextMeshProUGUI name1;
    private float timeLeft;
    private Scores[] scores;
    private const string SCORE_KEY = "Scores6";
    bool ScoreAdded = false;
    public Button Submit;
    private bool submit = false;
    public TextMeshProUGUI PlayerName;
    private GameObject text;
    public struct Scores
    
    {
        public string name;
        public int score;
        public int place;
    }

    void Start()
    {
        float timeLeft = timerslider.currentTime;
        scores = new Scores[10];
        for (int i = 0; i < 10; i++)
        {
            scores[i].name = "";
            scores[i].score = 0;
            scores[i].place = i + 1;
        }
        UpdateScores();
        Submit.onClick.AddListener(SubmitPlayerName);
        PlayerName = text.GetComponent<TextMeshProUGUI>();
    }

    public void SaveScore(string name, int score, bool remove = false)
    {
        for (int i = 0; i < 10; i++)
        {
            if (remove && scores[i].name == name)
            {
                for (int j = i; j < 9; j++)
                {
                    scores[j].score = scores[j + 1].score;
                    scores[j].name = scores[j + 1].name;
                }
                scores[9].name = "";
                scores[9].score = 0;
                SaveScores();
                break;
            }
            if (scores[i].score <= score)
            {
                for (int j = 9; j > i; j--)
                {
                    scores[j].score = scores[j - 1].score;
                    scores[j].name = scores[j - 1].name;
                }
                scores[i].score = score;
                scores[i].name = name;
                SaveScores();
                break;
            }
        }
    }

    private void SaveScores()
    {
        string scoreString = "";
        for (int i = 0; i < 10; i++)
        {
            scoreString += scores[i].name + ":" + scores[i].score + ":" + scores[i].place + ";";
        }
        PlayerPrefs.SetString(SCORE_KEY, scoreString);
    }

    private void LoadScores()
    {
        string scoreString = PlayerPrefs.GetString(SCORE_KEY, "");
        if (scoreString.Length > 0)
        {
            string[] scoreStrings = scoreString.Split(';');
            for (int i = 0; i < 10; i++)
            {
                if (i < scoreStrings.Length)
                {
                    string[] parts = scoreStrings[i].Split(':');
                    scores[i].name = parts[0];
                    scores[i].score = int.Parse(parts[1]);
                    scores[i].place = int.Parse(parts[2]);
                }
                else
                {
                    scores[i].name = "";
                    scores[i].score = 0;
                    scores[i].place = i + 1;
                }
            }
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                scores[i].name = "";
                scores[i].score = 0;
                scores[i].place = i + 1;
            }
            SaveScores();
        }
    }

    private void UpdateScores()
    {
        LoadScores();
        System.Array.Sort(scores, (a, b) => b.score.CompareTo(a.score));
        position1.text = "";
        score1.text = "";
        name1.text = "";
        for (int i = 0; i < 10; i++)
        {
            position1.text += (i + 1).ToString() + "\n";
            score1.text += scores[i].score + "\n";
            name1.text += scores[i].name + "\n";
        }
    }

    private void SubmitPlayerName() {
        submit = true;
    }
    public void Update()
    {
        timeLeft -= Time.deltaTime;
        
        if (submit && !ScoreAdded)
        {
            for(int i = 0; i < 10; i++) {
                if(scores[i].name == PlayerName.text) {
                    SaveScore(PlayerName.text, scoredisplay.score, true);
                }
            }
            SaveScore(PlayerName.text, scoredisplay.score, false);
            Debug.Log("added Score");
            UpdateScores();
            ScoreAdded = true;
        }
    }

}