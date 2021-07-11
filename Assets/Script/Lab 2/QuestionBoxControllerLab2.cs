using UnityEngine;
using System.Collections;

public class QuestionBoxControllerLab2 : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public SpringJoint2D springJoint;
    public GameObject consummablePrefab; // the spawned mushroom prefab
    public SpriteRenderer spriteRenderer;
    public Sprite usedQuestionBox; // the sprite that indicates empty box instead of a question mark
    private bool hit = false;
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && !hit)
        {
            hit = true;

            // ensure that we move this object sufficiently 
            rigidBody.AddForce(new Vector2(0, rigidBody.mass * 10), ForceMode2D.Impulse);

            // spawn the mushroom prefab slightly above the box
            Instantiate(consummablePrefab, new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z), Quaternion.identity);
            StartCoroutine(DisableHittable());
        }
    }

    // this will return true of the object is stationary after moving
    bool ObjectMovedAndStopped()
    {
        return Mathf.Abs(rigidBody.velocity.magnitude) < 0.01;
    }

    IEnumerator DisableHittable()
    {
        if (!ObjectMovedAndStopped())
        {
            yield return new WaitUntil(() => ObjectMovedAndStopped());
        }

        //continues here when the ObjectMovedAndStopped() returns true
        spriteRenderer.sprite = usedQuestionBox; // change sprite to be "used-box" sprite
        spriteRenderer.sortingLayerName = "Environment";
        rigidBody.bodyType = RigidbodyType2D.Static; // make the box unaffected by Physics

        //reset box position
        this.transform.localPosition = Vector3.zero;
        springJoint.enabled = false; // disable spring
    }
}