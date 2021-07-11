using UnityEngine;
public class EnemyStarControllerLab2 : MonoBehaviour
{
    private float originalX;
    public float maxOffset = 5.0f;
    public float enemyPatroltime = 2.0f;
    public int moveRight = -1;
    private Vector2 velocity;

    private Rigidbody2D enemyBody;

    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        originalX = transform.position.x;
        ComputeVelocity();
    }

    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }

    void MoveStar()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
        {
            MoveStar();
        }
        else
        {
            moveRight *= -1;
            ComputeVelocity();
            MoveStar();
        }
    }
}