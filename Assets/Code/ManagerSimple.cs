using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class ManagerSimple : MonoBehaviour
{

    public static ManagerSimple Instance;

    public int score = 100;
    public TextMeshPro outText;
    void Start()
    {
        
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the TextMeshPro component with a specific name
        outText = GameObject.Find("ScoreText").GetComponent<TextMeshPro>();
        // Update the text to show current score
        UpdateScore(0);
    }

    private void Awake() {

        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    public void UpdateScore(int points){
        score+=points;
        outText.text = "Score: " + score;

    }

}
