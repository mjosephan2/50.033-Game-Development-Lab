using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverController : MonoBehaviour
{
    public Text scoreText;
    public void setup(int score)
    {
        gameObject.SetActive(true);
        scoreText.text = "Score: " + score;
    }

    public void Restart()
    {
        // increase the restartCount
        GameState.incrementRestart();
        // restart the mario scene
        SceneManager.LoadScene("Lab 1");
    }
}
