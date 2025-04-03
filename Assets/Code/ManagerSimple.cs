using UnityEngine;
using TMPro;

public class ManagerSimple : MonoBehaviour
{
    public int score = 100;
    public TextMeshPro outText;
    void Start()
    {
        
    }

    public void UpdateScore(int points){
        score+=points;
        outText.text = "Score: " + score;

    }

}
