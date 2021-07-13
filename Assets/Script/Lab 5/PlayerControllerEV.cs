using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControllerEV : MonoBehaviour
{
    private float force;
    public GameConstantsLab5 gameConstants;
    public IntVariable marioUpSpeed;
    public IntVariable marioMaxSpeed;
    public Material shader;
    public CustomCastEvent OnPlayerCastPowerUp;
    private bool isDead;
    private bool isADKeyUp;
    private bool isSpacebarUp;
    private bool onGroundState;
    private bool faceRightState;
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private Animator marioAnimator;
    private AudioSource marioAudio;
    private Material originalMat;

    void Awake()
    {

    }
    void Start()
    {
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioAudio = GetComponent<AudioSource>();
        originalMat = marioSprite.material;

        isDead = false;
        isADKeyUp = true;
        isSpacebarUp = true;
        onGroundState = true;
        faceRightState = true;

        marioUpSpeed.SetValue(gameConstants.playerMaxJumpSpeed);
        marioMaxSpeed.SetValue(gameConstants.playerStartingMaxSpeed);
        force = gameConstants.playerDefaultForce;
    }

    void Update()
    {
        // default
        isSpacebarUp = !Input.GetKey("space");
        isADKeyUp = !Input.GetKey("a") && !Input.GetKey("d"); // it is up if both are not pressed
        if (Input.GetKeyDown("a"))
        {
            // check if it triggers skid
            if ((Mathf.Abs(marioBody.velocity.x) > 1.0) && faceRightState)
                marioAnimator.SetTrigger("onSkid");
            // Debug.Log("face right");
            faceRightState = false;
            marioSprite.flipX = true;

        }

        if (Input.GetKeyDown("d"))
        {
            // check if it triggers skid
            if ((Mathf.Abs(marioBody.velocity.x) > 1.0) && !faceRightState)
                marioAnimator.SetTrigger("onSkid");
            faceRightState = true;
            marioSprite.flipX = false;
            // if (Mathf.Abs(marioBody.velocity.x) > 1.0)
            //     marioAnimator.SetTrigger("onSkid");
        }

        if (Input.GetKeyDown("z"))
        {
            OnPlayerCastPowerUp.Invoke(KeyCode.Z);
        }

        if (Input.GetKeyDown("x"))
        {
            OnPlayerCastPowerUp.Invoke(KeyCode.X);
        }
    }
    void FixedUpdate()
    {
        if (!isDead)
        {
            //check if a or d is pressed currently
            if (!isADKeyUp)
            {
                float direction = faceRightState ? 1.0f : -1.0f;
                Vector2 movement = new Vector2(force * direction, 0);
                if (marioBody.velocity.magnitude < marioMaxSpeed.Value)
                    marioBody.AddForce(movement);
            }

            if (!isSpacebarUp && onGroundState)
            {
                marioBody.AddForce(Vector2.up * marioUpSpeed.Value, ForceMode2D.Impulse);
                onGroundState = false;
                // part 2
                marioAnimator.SetBool("onGround", onGroundState);
                // countScoreState = true; //check if Gomba is underneath
                PlayJumpSound();
            }
        }

        if (marioBody.velocity.magnitude > 0)
        {
            marioSprite.material = shader;
        }
        else
        {
            marioSprite.material = originalMat;
        }
        // set speed
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.CompareTag("Obstacle") || col.gameObject.CompareTag("Pipe") || col.gameObject.CompareTag("Ground")) && Mathf.Abs(marioBody.velocity.y) < 0.01f)
        {
            onGroundState = true; // back on ground
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    void PlayJumpSound()
    {
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    public void PlayerDiesSequence()
    {
        isDead = true;
        // marioAnimator.SetBool("isDead", true);
        GetComponent<Collider2D>().enabled = false;
        marioBody.AddForce(Vector3.up * 30, ForceMode2D.Impulse);
        marioBody.gravityScale = 30;
        StartCoroutine(dead());
    }

    IEnumerator dead()
    {
        yield return new WaitForSeconds(1.0f);
        marioBody.bodyType = RigidbodyType2D.Static;
    }
}