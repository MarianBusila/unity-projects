using UnityEngine;
using System.Collections;

public class EnemyWave : MonoBehaviour {

    public GameObject enemyGameObject;
    public int enemyCount = 10;
    public float waveSpeeed = 2f;
    public float delayBetweenEnemySpawns = 2f;
    private int enemiesRemaining;
	// Use this for initialization
	void Start () {
        enemiesRemaining = enemyCount;
	}

    public bool ThereAreEnemiesRemaining()
    {
        return enemiesRemaining > 0;
    }

    public GameObject GetNextEnemy()
    {
        enemiesRemaining--;
        return enemyGameObject;
    }

}
