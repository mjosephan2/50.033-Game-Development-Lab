using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerupIndex
{
    ORANGEMUSHROOM = 0,
    REDMUSHROOM = 1
}
public class PowerupManagerEV : MonoBehaviour
{
    // reference of all player stats affected
    public IntVariable marioJumpSpeed;
    public IntVariable marioMaxSpeed;
    public PowerupInventory powerupInventory;
    public List<GameObject> powerupIcons;

    void Start()
    {
        if (!powerupInventory.gameStarted)
        {
            powerupInventory.gameStarted = true;
            powerupInventory.Setup(powerupIcons.Count);
            resetPowerup();
        }
        else
        {
            // re-render the contents of the powerup from the previous time
            for (int i = 0; i < powerupInventory.Items.Count; i++)
            {
                Powerup p = powerupInventory.Get(i);
                if (p != null)
                {
                    AddPowerupUI(i, p.powerupTexture);
                }
                else{
                    powerupIcons[i].SetActive(false);
                }
            }
        }
    }

    public void resetPowerup()
    {
        for (int i = 0; i < powerupIcons.Count; i++)
        {
            powerupIcons[i].SetActive(false);
        }
    }

    void AddPowerupUI(int index, Texture t)
    {
        powerupIcons[index].GetComponent<RawImage>().texture = t;
        powerupIcons[index].SetActive(true);
    }

    void RemovePowerupUI(int index){
        powerupIcons[index].SetActive(false);
    }

    public void AddPowerup(Powerup p)
    {
        powerupInventory.Add(p, (int)p.index);
        AddPowerupUI((int)p.index, p.powerupTexture);
    }

    public void OnApplicationQuit()
    {
        ResetValues();
    }

    public void ResetValues()
    {
        powerupInventory.Clear();
    }

    void castPowerUp(int index)
    {
        if (powerupInventory.Get(index) != null)
        {
            Debug.Log("Consume effect");
            consumePowerUp(powerupInventory.Get(index));
            powerupInventory.Remove(index);
            RemovePowerupUI(index);
        }
    }
    void consumePowerUp(Powerup p){
        applyEffect(p);
        StartCoroutine(removeEffect(p));        
    }
    void applyEffect(Powerup p){
        Debug.Log("Apply effect");
        marioJumpSpeed.ApplyChange(p.absoluteJumpBooster);
        marioMaxSpeed.ApplyChange(p.absoluteSpeedBooster);
    }

    void effectWornOut(Powerup p){
        marioJumpSpeed.ApplyChange(-p.absoluteJumpBooster);
        marioMaxSpeed.ApplyChange(-p.absoluteSpeedBooster); 
        Debug.Log("Effect worn out");
    }

	IEnumerator removeEffect(Powerup p){
        Debug.Log("Wait for " + p.duration);
		yield  return  new  WaitForSeconds(p.duration);
		effectWornOut(p);
	}
    public void attemptUsingPowerUp(KeyCode k)
    {
        switch (k)
        {
            case KeyCode.Z:
                castPowerUp(0);
                break;
            case KeyCode.X:
                castPowerUp(1);
                break;
            default:
                break;
        }
    }
}