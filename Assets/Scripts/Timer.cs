using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
  public Text timerText;
  public float timeRemaining = 30f;
  public SpawnManager[] spawners;
  public float timeReduction = 0.5f;

  void Update()
  {
    if (timeRemaining > 0)
    {
      timeRemaining -= Time.deltaTime;
      timerText.text = "Time: " + Mathf.Ceil(timeRemaining);

      if (timeRemaining <= 0)
      {
        AdjustSpawnRate();
        timeRemaining = 30f;
      }
    }
  }
  void AdjustSpawnRate()
  {
    if (spawners != null && spawners.Length > 0)
    {
      foreach (SpawnManager spawner in spawners)
      {
        if (spawner != null)
        {
          // Reducir los tiempos mínimo y máximo del SpawnManager
          spawner.minTime = Mathf.Max(0.5f, spawner.minTime - timeReduction);
          spawner.maxTime = Mathf.Max(1f, spawner.maxTime - timeReduction);
        }
      }
    }
  }
}