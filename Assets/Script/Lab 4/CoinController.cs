using UnityEngine;
public class CoinController : MonoBehaviour
{
    private AudioSource coinAudio;

    void Awake()
    {
        coinAudio = GetComponent<AudioSource>();
        Debug.Log("Coin Spawned");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            CentralManager.centralManagerInstance.increaseScore();
            startAudio();
            // summon new monster
            CentralManager.centralManagerInstance.spawnNewEnemy();

            // disable
            gameObject.SetActive(false);
        }
    }

    void startAudio()
    {   
        Debug.Log("Start Audio");
        CentralManager.centralManagerInstance.playCoinSFX();
    }
}