using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
  public float playerSpeed = 3f;
  public Sprite[] walkSpritesLeft;
  public Sprite[] walkSpritesRight;
  public Sprite idleSprite;
  public float animationSpeed = 0.1f;
  private int walkIndex = 0;
  private Coroutine currentCoroutine = null;
  private bool isMovingRight = false;

  private Rigidbody2D myRigidbody2D;
  private SpriteRenderer mySpriteRenderer;
  private GameManager myGameManager;

  void Start()
  {
    myRigidbody2D = GetComponent<Rigidbody2D>();
    mySpriteRenderer = GetComponent<SpriteRenderer>();
    myGameManager = FindObjectOfType<GameManager>();
  }

  void Update()
  {
    HandleMovement();
  }

  void HandleMovement()
  {
    if (Input.GetKey(KeyCode.D))
    {
      myRigidbody2D.linearVelocity = new Vector2(playerSpeed, myRigidbody2D.linearVelocity.y);
      if (!isMovingRight)
      {
        isMovingRight = true;
        StartWalking(walkSpritesRight);
      }
    }
    else if (Input.GetKey(KeyCode.A))
    {
      myRigidbody2D.linearVelocity = new Vector2(-playerSpeed, myRigidbody2D.linearVelocity.y);
      if (isMovingRight || currentCoroutine == null)
      {
        isMovingRight = false;
        StartWalking(walkSpritesLeft);
      }
    }
    else
    {
      myRigidbody2D.linearVelocity = new Vector2(0, myRigidbody2D.linearVelocity.y);
      if (currentCoroutine != null)
      {
        StopCoroutine(currentCoroutine);
        currentCoroutine = null;
      }
      mySpriteRenderer.sprite = idleSprite;
    }
  }

  void StartWalking(Sprite[] walkSprites)
  {
    if (currentCoroutine != null)
    {
      StopCoroutine(currentCoroutine);
    }
    currentCoroutine = StartCoroutine(WalkCoroutine(walkSprites));
  }

  IEnumerator WalkCoroutine(Sprite[] walkSprites)
  {
    while (true)
    {
      walkIndex = (walkIndex + 1) % walkSprites.Length;
      mySpriteRenderer.sprite = walkSprites[walkIndex];
      yield return new WaitForSeconds(animationSpeed);
    }
  }

  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Coin"))
    {
      Destroy(collision.gameObject);
      if (myGameManager != null)
      {
        myGameManager.AddScore();
      }
    }
  }
}
