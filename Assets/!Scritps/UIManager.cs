// Gives access to MonoBehaviour

using System;
using UnityEngine;
// Gives access to TMP_Text
using TMPro;

// Shows the score on screen; it listens to Score, and Score never knows about it
public class UIManager : MonoBehaviour
{
    // The text that shows the score
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private TMP_Text healthText;

    [SerializeField] private Player Player;

    private void Start()
    {
        Player.GetHealth().OnHealthChanged += updateHealth;
        updateHealth(Player.GetHealth().GetCurrentHealth());
    }

    // Runs when this object turns on, before Start
    private void OnEnable()
    {
        // Subscribe: call UpdateScore whenever the score changes
        Score.OnPointsChanged += UpdateScore;
    }

    // Runs when this object turns off or is destroyed
    private void OnDisable()
    {
        // Unsubscribe, so Score doesn't try to call a UI that's gone
        Score.OnPointsChanged -= UpdateScore;
    }

    // Called by Score's announcement, with the new score
    private void UpdateScore(int points)
    {
        // Show it
        scoreText.text = "Score: " + points;
    }
    
    private void updateHealth(int health)
    {
        healthText.text = "Health: " + health;
    }
}