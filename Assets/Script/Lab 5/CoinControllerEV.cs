using UnityEngine;
using UnityEngine.Events;
public class CoinControllerEV : MonoBehaviour
{
    private AudioSource coinAudio;
    public UnityEvent onCoinGained;
    void Awake()
    {
        Debug.Log("Coin Spawned");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            onCoinGained.Invoke();
            // disable
            gameObject.SetActive(false);
        }
    }
}