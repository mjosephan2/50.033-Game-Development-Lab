using UnityEngine;

public class BottomColliderLab5 : MonoBehaviour
{
    private MushroomControllerLab5 parent;
    private Rigidbody2D parentBody;
    void Awake(){
        parent = transform.parent.GetComponent<MushroomControllerLab5>();
        parentBody = transform.parent.GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Obstacle"))
        {
            parent.setConstantSpeed();
        }
    }
}