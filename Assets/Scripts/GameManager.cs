using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
  public int score = 0;
  public Text textScore;

  void Start()
  {
    if (textScore != null)
    {
      textScore.text = "Score: " + score.ToString();
    }
  }
  public void AddScore()
  {
    score += 10;
    textScore.text = "Score: " + score.ToString();
  }
  public void GameOver()
  {
    SceneManager.LoadScene("SampleScene");
  }
}
