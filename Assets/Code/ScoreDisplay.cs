using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreDisplay : MonoBehaviour
{
    private TextMeshPro tmpText;
    private int lastDisplayedScore = -1;
    private Coroutine temporaryMessageCoroutine;

    void Start()
    {
        // Get the TMP component from this same object
        tmpText = GetComponent<TextMeshPro>();
        
        if (tmpText == null)
        {
            Debug.LogError("No TextMeshPro component found on this object!");
            return;
        }
        
        UpdateScoreDisplay();
    }

    void Update()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager instance is null!");
            return;
        }
        
        // Only update if score changed and we're not showing a temporary message
        if (GameManager.Instance.playerScore != lastDisplayedScore && temporaryMessageCoroutine == null)
        {
            UpdateScoreDisplay();
        }
    }

    private void UpdateScoreDisplay()
    {
        if (tmpText != null && GameManager.Instance != null)
        {
            lastDisplayedScore = GameManager.Instance.playerScore;
            tmpText.text = $"Score: {lastDisplayedScore}";
        }
    }

    public void ShowTemporaryMessage(string message, float duration = 2f)
    {
        if (temporaryMessageCoroutine != null)
        {
            StopCoroutine(temporaryMessageCoroutine);
        }
        temporaryMessageCoroutine = StartCoroutine(TemporaryMessageCoroutine(message, duration));
    }

    private IEnumerator TemporaryMessageCoroutine(string message, float duration)
    {
        // Store current score
        int currentScore = lastDisplayedScore;
        
        // Show temporary message
        tmpText.text = message;
        
        // Wait for specified duration
        yield return new WaitForSeconds(duration);
        
        // Revert back to score display
        UpdateScoreDisplay();
        
        temporaryMessageCoroutine = null;
    }
} 