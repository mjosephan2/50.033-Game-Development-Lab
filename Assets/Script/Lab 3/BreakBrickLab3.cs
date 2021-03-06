using UnityEngine;
public class BreakBrickLab3 : MonoBehaviour
{
    public GameObject prefab;
    public bool broken;
    private AudioSource breakAudio;
    void Start(){
        broken = false;
        breakAudio = GetComponent<AudioSource>();
    }
    void  OnTriggerEnter2D(Collider2D col){
	if (col.gameObject.CompareTag("Player") &&  !broken){
        Debug.Log("Player hit");
		broken  =  true;
        // run audio
        breakAudio.PlayOneShot(breakAudio.clip);
		// assume we have 5 debris per box
		for (int x =  0; x<5; x++){
			Instantiate(prefab, transform.position, Quaternion.identity);
		}
		gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled  =  false;
		gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled  =  false;
		GetComponent<EdgeCollider2D>().enabled  =  false;
	}
}
}