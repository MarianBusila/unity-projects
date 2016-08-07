using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour {

    public GameObject[] tilePrefabs;

    private Transform playerTransform;
    private float spawnZ = 0.0f;
    private float tileLength = 20.0f;
    private float safeZone = 25.0f;
    private int amnTilesOnScreen = 7;
    private int lastPrefaIndex = 0;

    private List<GameObject> activeTiles;

	// Use this for initialization
	void Start () {
        activeTiles = new List<GameObject>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < amnTilesOnScreen; i++)
        {
            if (i < 3)
                SpawnTile(0);
            else
                SpawnTile();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (playerTransform.transform.position.z - safeZone > spawnZ - amnTilesOnScreen * tileLength)
        {
            SpawnTile();
            DeleteTile();
        }
	}

    void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;
        if(prefabIndex == -1)        
            go = Instantiate(tilePrefabs[RandomPrefabIndex()]);
        else
            go = Instantiate(tilePrefabs[prefabIndex]);

        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;

        activeTiles.Add(go);
    }

    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
            return 0;

        int randomIndex = lastPrefaIndex;
        while(randomIndex == lastPrefaIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }

        lastPrefaIndex = randomIndex;
        return randomIndex;
    }
}
