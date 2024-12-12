using UnityEngine;

public class PlatformManager : MonoBehaviour
{
  public int lostObjects = 0;
  public int maxLostObjects = 5;
  public GameManager gameManager;

  void Start()
  {
    if (gameManager == null)
    {
      gameManager = FindObjectOfType<GameManager>();
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Coin"))
    {
      Destroy(collision.gameObject, 5f);
      lostObjects++;
      if (lostObjects >= maxLostObjects)
      {
        GameOver();
      }
    }
  }

  void GameOver()
  {
    gameManager.GameOver();
  }
}
