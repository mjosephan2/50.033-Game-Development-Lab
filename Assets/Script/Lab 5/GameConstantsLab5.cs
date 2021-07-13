using UnityEngine;

[CreateAssetMenu(fileName = "GameConstantsLab5", menuName = "ScriptableObjects/GameConstantsLab5", order = 3)]
public class GameConstantsLab5 : ScriptableObject
{
    // set your data here
    // for Scoring system
    int currentScore;
    int currentPlayerHealth;

    // for Reset values
    Vector3 gombaSpawnPointStart = new Vector3(2.5f, -0.45f, 0); // hardcoded location
                                                                 // .. other reset values 

    // for Consume.cs
    public int consumeTimeStep = 10;
    public int consumeLargestScale = 4;

    // for Break.cs
    public int breakTimeStep = 30;
    public int breakDebrisTorque = 10;
    public int breakDebrisForce = 10;

    // for SpawnDebris.cs
    public int spawnNumberOfDebris = 10;

    // for Rotator.cs
    public int rotatorRotateSpeed = 6;

    // for testing
    public int testValue;

    public float groundSurface = -1;
    public float maxOffset = 5;
    public float enemyPatroltime = 2;

    public float spawnStart = -11;
    public float spawnEnd = -3;

    public int playerStartingMaxSpeed = 5;
    public int playerMaxJumpSpeed = 30;
    public int playerDefaultForce = 150;

    public float groundDistance = -1.0f;
}