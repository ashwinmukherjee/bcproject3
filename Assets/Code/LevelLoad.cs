using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{
    public string level = "Level1";

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerController"))
        {
            SceneManager.LoadScene(level, LoadSceneMode.Single);

            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }
    }
}