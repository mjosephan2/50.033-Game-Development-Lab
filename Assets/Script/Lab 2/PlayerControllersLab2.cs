using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerControllersLab2 : MonoBehaviour
{
    private Rigidbody2D marioBody;
    private bool onGround = true;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    private bool countScoreState = false;
    private Animator marioAnimator;
    private AudioSource marioAudio;
    private int score = 0;
    public float speed;
    public float maxSpeed = 30;
    public float upSpeed = 20;
    public Transform enemies;
    public Transform enemyLocation;
    public Text scoreText;
    public MenuControllerLab2 startScreen;
    public GameOverControllerLab2 gameOverScreen;

    void Awake()
    {

    }
    // Use this for initialization
    void Start()
    {
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioAudio = GetComponent<AudioSource>();

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
            // Debug.Log("Mario velocity set zero: " + marioBody.velocity.magnitude);
        }

        if (Input.GetKeyDown("space") && onGround)
        {
            // Debug.Log("[Jump]");
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGround = false;
            marioAnimator.SetBool("onGround", onGround);
            countScoreState = true; //check if Gomba is underneath
        }
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            onGround = true; // back on ground
            marioAnimator.SetBool("onGround", onGround);
            countScoreState = false; // reset score state
            scoreText.text = "Score: " + score.ToString();
        };

        if ((col.gameObject.CompareTag("Obstacle") || col.gameObject.CompareTag("Pipe"))&& Mathf.Abs(marioBody.velocity.y) < 0.01f){
            onGround = true; // back on ground
            marioAnimator.SetBool("onGround", onGround);
        }
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
            Debug.Log("face right");
            faceRightState = false;
            marioSprite.flipX = true;
            if (Mathf.Abs(marioBody.velocity.x) > 1.0)
                marioAnimator.SetTrigger("onSkid");
        }

        if (Input.GetKeyDown("d") && !faceRightState)
        {
            Debug.Log("face left");
            faceRightState = true;
            marioSprite.flipX = false;
            if (Mathf.Abs(marioBody.velocity.x) > 1.0)
                marioAnimator.SetTrigger("onSkid");
        }

        // when jumping, and Gomba is near Mario and we haven't registered our score
        if (!onGround && countScoreState)
        {
            //   Debug.Log(Mathf.Abs(transform.position.x - enemyLocation.position.x));
            if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
            {
                countScoreState = false;
                score++;
                Debug.Log(score);
            }
        }
    }

    void PlayJumpSound()
    {
        marioAudio.PlayOneShot(marioAudio.clip);
    }
}
