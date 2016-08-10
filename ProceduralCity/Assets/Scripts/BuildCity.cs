using UnityEngine;
using System.Collections;

public class BuildCity : MonoBehaviour {

    public GameObject[] buildings;
    public GameObject xstreets;
    public GameObject zstreets;
    public GameObject crossroad;

    public int mapWidth = 20;
    public int mapHeight = 20;
    int[,] mapgrid;
    int buildingFootPrint = 3;

	// Use this for initialization
	void Start () {
        mapgrid = new int[mapWidth, mapHeight];
        float seed = Random.Range(0, 100);
        //generate the map data
        for (int h = 0; h < mapHeight; h++)
            for (int w = 0; w < mapHeight; w++)
            {
                mapgrid[w, h] = (int)(Mathf.PerlinNoise(w / 10.0f + seed, h / 10.0f + seed) * 10);
            }

        //build streets
        int x = 0;
        for(int n = 0; n < 50 ; n++)
        {
            for( int h = 0; h < mapHeight; h++)
            {
                mapgrid[x, h] = -1;
            }
            x += Random.Range(3, 3);
            if (x >= mapWidth)
                break;
        }

        int z = 0;
        for (int n = 0; n < 50; n++)
        {
            for (int w = 0; w < mapWidth; w++)
            {
                if(mapgrid[w, z] == -1)
                    mapgrid[w, z] = -3; //crossroad
                else
                    mapgrid[w, z] = -2;
            }
            z += Random.Range(2, 10);
            if (z >= mapHeight)
                break;
        }



        //generate city blocks
        for (int h = 0; h < mapHeight; h++)
            for (int w = 0; w < mapHeight; w++)
            {
                int result = mapgrid[w, h];
                Vector3 pos = new Vector3(w * buildingFootPrint, 0, h * buildingFootPrint);
                if (result < -2)
                    Instantiate(crossroad, pos, crossroad.transform.rotation);
                else if (result < -1)
                    Instantiate(xstreets, pos, xstreets.transform.rotation);
                else if (result < 0)
                    Instantiate(zstreets, pos, zstreets.transform.rotation);
                else if (result < 2)
                    Instantiate(buildings[0], pos, Quaternion.identity);
                else if (result < 4)
                    Instantiate(buildings[1], pos, Quaternion.identity);
                else if (result < 5)
                    Instantiate(buildings[2], pos, Quaternion.identity);
                else if (result < 6)
                    Instantiate(buildings[3], pos, Quaternion.identity);
                else if (result < 7)
                    Instantiate(buildings[4], pos, Quaternion.identity);
                else if (result < 10)
                    Instantiate(buildings[5], pos, Quaternion.identity);
            }
    }
	
}
