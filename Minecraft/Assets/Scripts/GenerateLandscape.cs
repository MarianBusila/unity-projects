using UnityEngine;
using System.Collections;

public class GenerateLandscape : MonoBehaviour {

    public int width = 128;
    public int depth = 128;
    public int heightScale = 20;
    public float detailScale = 25.0f;

    public GameObject grassBlock;
    public GameObject sandBlock;
    public GameObject snowBlock;


    // Use this for initialization
    void Start () {
        int seed = (int)Network.time * 10;
	    for(int z = 0 ; z < depth; z++)
            for (int x = 0 ; x < width; x++)
            {
                int y = (int)(Mathf.PerlinNoise((x + seed) / detailScale, (z + seed) / detailScale) * heightScale);
                Vector3 blockPos = new Vector3(x, y, z);
                if(y > heightScale / 4 * 3)
                    Instantiate(snowBlock, blockPos, Quaternion.identity);
                else if (y < heightScale / 4)
                    Instantiate(sandBlock, blockPos, Quaternion.identity);
                else
                    Instantiate(grassBlock, blockPos, Quaternion.identity);
            }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
