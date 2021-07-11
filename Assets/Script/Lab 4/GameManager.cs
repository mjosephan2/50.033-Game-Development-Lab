using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text score;
    private int playerScore = 0;
    public delegate void gameEvent();
    public static event gameEvent OnPlayerDeath;

    public GameObject AudioTheme;
    public GameObject DyingSFX;
    public GameObject CoinSFX;
    public void increaseScore()
    {
        playerScore += 1;
        score.text = "SCORE: " + playerScore.ToString();
    }

    public void damagePlayer()
    {
        OnPlayerDeath();
    }

    // public void poolReady(){
    //     OnEnemyPoolReady();
    // }

    public void stopTheme()
    {
        AudioTheme.GetComponent<AudioSource>().Stop();
    }

    public void playDyingSFX()
    {
        AudioSource sfx = DyingSFX.GetComponent<AudioSource>();
        sfx.PlayOneShot(sfx.clip);
    }

    public void playCoinSFX()
    {
        AudioSource sfx = CoinSFX.GetComponent<AudioSource>();
        sfx.PlayOneShot(sfx.clip);
    }
}