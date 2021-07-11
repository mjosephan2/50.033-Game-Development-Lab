using System.Collections;
using UnityEngine;

public  class OrangeMushroom : MonoBehaviour, ConsumableInterface
{
	public  void  consumedBy(GameObject player){
		// give player speed boost
		player.GetComponent<PlayerControllersLab4>().speed  *=  2;
        Debug.Log("Increase max speed");
        Debug.Log(player.GetComponent<PlayerControllersLab4>().maxSpeed);
		StartCoroutine(removeEffect(player));
	}

	IEnumerator  removeEffect(GameObject player){
		yield  return  new  WaitForSeconds(5.0f);
		player.GetComponent<PlayerControllersLab4>().speed  /=  2;
	}
}