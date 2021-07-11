using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this has methods callable by players
public class CentralManager : MonoBehaviour
{
    public GameObject gameManagerObject;
    private GameManager gameManager;
    public static CentralManager centralManagerInstance;
    // add reference to PowerupManager
    public GameObject powerupManagerObject;
    private PowerUpManager powerUpManager;

    public GameObject spawnManagerObject;
    private SpawnManagerLab4 spawnManager;
    void Awake()
    {
        centralManagerInstance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        // instantiate in start
        gameManager = gameManagerObject.GetComponent<GameManager>();
        powerUpManager = powerupManagerObject.GetComponent<PowerUpManager>();
        spawnManager = spawnManagerObject.GetComponent<SpawnManagerLab4>();
    }

    public void increaseScore()
    {
        gameManager.increaseScore();
    }

    public void damagePlayer()
    {
        gameManager.damagePlayer();
    }

    public void consumePowerup(KeyCode k, GameObject g)
    {
        powerUpManager.consumePowerup(k, g);
    }

    public void addPowerup(Texture t, int i, ConsumableInterface c)
    {
        powerUpManager.addPowerup(t, i, c);
    }
    // public void poolReady(){
    //     gameManager.poolReady();
    // }

    public void spawnNewEnemy()
    {
        spawnManager.spawnNewEnemy();
    }

    public void stopTheme()
    {
        gameManager.stopTheme();
    }

    public void playDyingSFX()
    {
        gameManager.playDyingSFX();
    }

    public void playCoinSFX()
    {
        gameManager.playCoinSFX();
    }
}