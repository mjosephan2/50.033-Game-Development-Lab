using System.Collections;
using UnityEngine;

public class EnemyControllerLab4 : MonoBehaviour
{
    public GameConstants gameConstants;
    private int moveRight;
    private float originalX;
    private Vector2 velocity;
    private Rigidbody2D enemyBody;
    private AudioSource audioStomp;
    private bool Rejoice;
    private bool Dying;
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        audioStomp = GetComponent<AudioSource>();
        // get the starting position
        originalX = transform.position.x;

        // randomise initial direction
        moveRight = Random.Range(0, 2) == 0 ? -1 : 1;
        // subscribe to player event
        GameManager.OnPlayerDeath += EnemyRejoice;
        Rejoice = false;
        // compute initial velocity
        ComputeVelocity();
    }

    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * gameConstants.maxOffset / gameConstants.enemyPatroltime, 0);
    }

    void MoveEnemy()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    void Update()
    {
        if (Rejoice)
        {
            // keep changing direction
            moveRight *= -1;
            ComputeVelocity();
            MoveEnemy();
        }
        else if (Mathf.Abs(enemyBody.position.x - originalX) < gameConstants.maxOffset)
        {// move gomba
            MoveEnemy();
        }
        else
        {
            // change direction
            moveRight *= -1;
            ComputeVelocity();
            MoveEnemy();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // check if it collides with Mario
        if (other.gameObject.tag == "Player")
        {
            // check if collides on top
            float yoffset = (other.transform.position.y - this.transform.position.y);
            if (yoffset > 0.75f)
            {
                KillSelf();
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                // hurt player
                CentralManager.centralManagerInstance.damagePlayer();
            }
        }

        // // check if collides with pipe
        // if (other.gameObject.tag == "Pipe")
        // {
        //     // change direction?
        //     moveRight *= -1;
        // }
    }

    void KillSelf()
    {
        // enemy dies
        startAudioStomp();
        CentralManager.centralManagerInstance.increaseScore();
        StartCoroutine(flatten());
        Debug.Log("Kill sequence ends");
    }

    void startAudioStomp()
    {
        audioStomp.PlayOneShot(audioStomp.clip);
    }
    IEnumerator flatten()
    {
        Debug.Log("Flatten starts");
        int steps = 5;
        float stepper = 1.0f / (float)steps;

        for (int i = 0; i < steps; i++)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y - stepper, this.transform.localScale.z);

            // make sure enemy is still above ground
            this.transform.position = new Vector3(this.transform.position.x, gameConstants.groundSurface + GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
            yield return null;
        }
        Debug.Log("Flatten ends");
        this.gameObject.SetActive(false);
        Debug.Log("Enemy returned to pool");
        yield break;
    }

    void EnemyRejoice()
    {
        Debug.Log("Enemy killed Mario");
        // do whatever you want here, animate etc
        // ...

        // do some animation? move back and forth
        Rejoice = true;
    }
}