using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    // Event for score changes
    public event Action<int> OnScoreChanged;

    // Game state variables
    private int _playerScore;
    public int playerScore 
    { 
        get { return _playerScore; }
        private set 
        { 
            _playerScore = value;
            OnScoreChanged?.Invoke(_playerScore);
        }
    }
    
    public int initialScore = 100; // Starting score

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // This makes the object persist across scenes
            playerScore = initialScore; // Initialize score
            Debug.Log($"GameManager initialized with score: {playerScore}");
        }
        else
        {
            Debug.Log("Duplicate GameManager found - destroying");
            Destroy(gameObject); // Destroy duplicate if it exists
        }
    }

    // Method to decrease score when player hits a laser
    public void DecreaseScore(int amount)
    {
        int oldScore = playerScore;
        playerScore = Mathf.Max(0, playerScore - amount); // Ensure score doesn't go below 0
        Debug.Log($"Score decreased from {oldScore} to {playerScore} (by {amount})");
    }

    // Method to reset score
    public void ResetScore()
    {
        playerScore = initialScore;
        Debug.Log($"Score reset to {initialScore}");
    }
} 