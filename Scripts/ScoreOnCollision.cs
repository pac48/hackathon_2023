using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreOnCollision : MonoBehaviour
{
    public int scoreToSubtract = 250; // The amount to subtract from the score on collision
    public Spotmanager SpotManager; // The score manager script that manages the score
    public GameObject[] prefabsToCheck; // Array of prefabs to check collision against

    private void OnCollisionEnter2D(Collision2D other)
    {
        int score = SpotManager.score - scoreToSubtract;
        SpotManager.Setscore(score);
    }
    
}