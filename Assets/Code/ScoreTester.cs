using UnityEngine;

public class ScoreTester : MonoBehaviour
{
    public float decreaseInterval = 2.0f;  // Time in seconds between score decreases
    public int decreaseAmount = 5;         // Amount to decrease by each time
    
    private float timer = 0.0f;
    
    void Start()
    {
        Debug.Log("ScoreTester started. GameManager null: " + (GameManager.Instance == null));
    }
    
    void Update()
    {
        if (GameManager.Instance == null)
            return;
            
        timer += Time.deltaTime;
        
        if (timer >= decreaseInterval)
        {
            timer = 0.0f;
            GameManager.Instance.DecreaseScore(decreaseAmount);
            Debug.Log("Score decreased to: " + GameManager.Instance.playerScore);
        }
    }
}