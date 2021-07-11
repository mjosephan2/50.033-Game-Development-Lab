using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerControllers : MonoBehaviour
{
    private Rigidbody2D marioBody;
    private bool onGround = true;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    private bool countScoreState = false;
    private int score = 0;
    public float speed;
    public float maxSpeed = 30;
    public float upSpeed = 10;
    public Transform enemies;
    public Transform enemyLocation;
    public Text scoreText;
    public MenuController startScreen;
    public GameOverController gameOverScreen;

    void Awake()
    {

    }
    // Use this for initialization
    void Start()
    {
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();

        int restartCounter = GameState.getRestartCount();
        Debug.Log("Starting game");
        Debug.Log("Restart counter: " + restartCounter);

        if (restartCounter > 0)
        {
            startScreen.StartButtonClicked();
        }

    }
    // FixedUpdate may be called once per frame. See documentation for details.
    void FixedUpdate()
    {
        // dynamic rigidbody
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < maxSpeed)
                marioBody.AddForce(movement * speed);
        }

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            // stop
            marioBody.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown("space") && onGround)
        {
            // Debug.Log("[Jump]");
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGround = false;
            countScoreState = true; //check if Gomba is underneath
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
        if (col.gameObject.CompareTag("Ground"))
        {
            onGround = true; // back on ground
            countScoreState = false; // reset score state
            scoreText.text = "Score: " + score.ToString();
        };
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with Gomba! Restarting the game");
            // pause the game
            Time.timeScale = 0;

            // disable startScreen
            startScreen.Disable();

            // start GameOverScreen
            gameOverScreen.setup(score);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a") && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
        }

        if (Input.GetKeyDown("d") && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
        }

     // when jumping, and Gomba is near Mario and we haven't registered our score
      if (!onGround && countScoreState)
      {
          Debug.Log(Mathf.Abs(transform.position.x - enemyLocation.position.x));
          if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
          {
              countScoreState = false;
              score++;
              Debug.Log(score);
          }
      }
    }
}
