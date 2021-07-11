using UnityEngine;

public class MushroomControllerLab4 : MonoBehaviour
{	public  Texture t;
    public int index;
    private Rigidbody2D mushroomBody;
    [SerializeField]
    private float constantSpeed;
    private Vector2 direction;
    public bool constantMove;
    private Collider2D bottomCollider;
    private ConsumableInterface parentScript;
    void Start()
    {
        mushroomBody = GetComponent<Rigidbody2D>();
        mushroomBody.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
        mushroomBody.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
        bottomCollider = gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();

        // intial is right
        direction = new Vector2(1, 0);
        constantMove = false;

        parentScript = transform.parent.GetComponent<ConsumableInterface>();
    }
    void Update()
    {
        // make sure the mushroom move at constant speed
        if (constantMove)
        {
            mushroomBody.velocity = mushroomBody.velocity.normalized * constantSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            mushroomBody.velocity = new Vector2(0, 0);
            constantMove = false;

            CentralManager.centralManagerInstance.addPowerup(t, index, parentScript);
            GetComponent<Collider2D>().enabled = false;
            gameObject.SetActive(false);

        }
        if (col.gameObject.CompareTag("Pipe"))
        {
            direction = direction * -1;
            mushroomBody.velocity = direction * constantSpeed;
        }
    }
    void FixedUpdate()
    {

    }

    public void setConstantSpeed()
    {
        mushroomBody.velocity = direction * constantSpeed;
        constantMove = true;
    }
    // void OnBecameInvisible()
    // {
    //     Destroy(gameObject);
    // }
}