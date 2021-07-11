using UnityEngine;
public class SpawnManagerLab4 : MonoBehaviour
{
    public GameConstants gameConstants;
    float groundDistance;
    void Start()
    {
        // wait until the all the pool manager is ready
        // GameManager.OnEnemyPoolReady += Initialize;
        groundDistance = gameConstants.groundSurface;
        Initialize();
    }

    void Initialize(){
        // spawn two gombaEnemy
        for (int j = 0; j < 2; j++)
            spawnFromPooler(ObjectType.gombaEnemy);
    }
    void spawnFromPooler(ObjectType i)
    {
        Debug.Log(ObjectPoolerLab4.SharedInstance);
        Debug.Log(ObjectPoolerLab4.SharedInstance.GetPooledObject(0));
        GameObject item = ObjectPoolerLab4.SharedInstance.GetPooledObject(i);
        Debug.Log(ObjectPoolerLab4.SharedInstance);
        if (item != null)
        {
            //set position
            item.transform.localScale = new Vector3(1, 1, 1);
            item.transform.position = new Vector3(Random.Range(gameConstants.spawnStart, gameConstants.spawnEnd), groundDistance + item.GetComponent<SpriteRenderer>().bounds.extents.y, 0);
            item.SetActive(true);
        }
        else
        {
            Debug.Log("not enough items in the pool!");
        }
    }

    public void spawnNewEnemy()
    {

        Debug.Log("Spawning new enemy");
        ObjectType i = Random.Range(0, 2) == 0 ? ObjectType.gombaEnemy : ObjectType.starEnemy;
        spawnFromPooler(i);

    }
}