using UnityEngine;
using System.Collections;

public class EnemyWaveHandler : MonoBehaviour {

    public Transform[] path;
    public EnemyWave[] enemyWaves;
    int currentWave = 0;
    float timeLastEnemyGameObjectSpawned;
    bool allWavesCompleted = false;
	
    // Use this for initialization
	void Start () {
        timeLastEnemyGameObjectSpawned = Time.time;
	
	}
	
	// Update is called once per frame
	void Update () {

        if(!allWavesCompleted)
        { 
            float nextObjectDue = enemyWaves[currentWave].delayBetweenEnemySpawns;
            if (Time.time - timeLastEnemyGameObjectSpawned > nextObjectDue)
            {
                GameObject enemyGameObject = Instantiate(enemyWaves[currentWave].GetNextEnemy(), Vector3.zero, Quaternion.identity) as GameObject;
                EnemyPathRoute epr = enemyGameObject.AddComponent<EnemyPathRoute>() as EnemyPathRoute;
                epr.path = path;
                epr.moveSpeed = enemyWaves[currentWave].waveSpeeed;

                timeLastEnemyGameObjectSpawned = Time.time;

                if (!enemyWaves[currentWave].ThereAreEnemiesRemaining())
                {
                    Debug.Log("Next wave");
                    currentWave++;

                    if (currentWave == enemyWaves.Length)
                    {
                        allWavesCompleted = true;
                        Debug.Log("All waves completed");
                    }
                }
            }
        }
	}
}
