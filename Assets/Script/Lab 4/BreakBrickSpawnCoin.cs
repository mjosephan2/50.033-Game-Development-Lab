using UnityEngine;
public class BreakBrickSpawnCoin : MonoBehaviour
{
    private BreakBrickLab3 parentScript;
    private bool summonCoin;
    public GameObject coinPrefab;
    void Awake(){
        parentScript = transform.parent.GetComponent<BreakBrickLab3>();
        summonCoin = false;
    }

    void Update(){
        if (parentScript.broken && !summonCoin){
            SummonCoin();
        }
    }

    void SummonCoin(){
        Instantiate(coinPrefab, new Vector3(this.transform.position.x, this.transform.position.y + 2.0f, this.transform.position.z), Quaternion.identity);
        summonCoin = true;
    }
}