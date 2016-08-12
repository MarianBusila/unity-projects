using UnityEngine;
using System.Collections;

public class Block
{
    public enum Type
    {
        Snow,
        Grass,
        Sand,
        Cloud,
        Diamond
    };
    public Type type;
    public bool vis;
    public GameObject block;

    public Block(Type t, bool v, GameObject b)
    {
        type = t;
        vis  = v;
        block = b;
    }
}
public class GenerateLandscape : MonoBehaviour {

    public int width = 128;
    public int depth = 128;
    public int height = 128;

    public int heightScale = 20;
    public int heightOffset = 100;
    public float detailScale = 25.0f;

    public GameObject grassBlock;
    public GameObject sandBlock;
    public GameObject snowBlock;
    public GameObject cloudBlock;
    public GameObject diamondBlock;

    Block[,,] worldBlocks;

    // Use this for initialization
    void Start() {
        worldBlocks = new Block[width, height, depth];
        int seed = (int)Network.time * 10;
        for (int z = 0; z < depth; z++)
            for (int x = 0; x < width; x++)
            {
                int y = (int)(Mathf.PerlinNoise((x + seed) / detailScale, (z + seed) / detailScale) * heightScale) + heightOffset;
                Vector3 blockPos = new Vector3(x, y, z);

                CreateBlock(y, blockPos, true);
                while (y > 0)
                {
                    y--;
                    blockPos = new Vector3(x, y, z);
                    CreateBlock(y, blockPos, false);
                }
            }

        DrawClouds(20, 100);
        DigMines(5, 500);
    }

    void DrawClouds(int numCoulds, int cSize)
    {
        for(int i = 0; i < numCoulds; i++)
        {
            int xpos = Random.Range(0, width);
            int zpos = Random.Range(0, depth);

            for(int j = 0; j < cSize; j++)
            {
                Vector3 blockPos = new Vector3(xpos, height - 1, zpos);
                GameObject newBlock = (GameObject)Instantiate(cloudBlock, blockPos, Quaternion.identity);
                worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z] = new Block(Block.Type.Cloud, true, newBlock);
                xpos += Random.Range(-1, 2);
                zpos += Random.Range(-1, 2);
                if (xpos < 0 || xpos >= width) xpos = width / 2;
                if (zpos < 0 || zpos >= depth) zpos = depth / 2;
            }
        }
    }

    void DigMines(int numMines, int mSize)
    {
        int holSize = 2;
        for(int i = 0; i < numMines; i++)
        {
            int xpos = Random.Range(10, width-10);
            int ypos = Random.Range(10, height - 10);
            int zpos = Random.Range(10, depth - 10);

            for(int j = 0; j < mSize; j++)
            {
                for(int x = -holSize; x <= holSize; x++)
                    for (int y = -holSize; y <= holSize; y++)
                        for (int z = -holSize; z <= holSize; z++)
                        {
                            if(!(x == 0 && y == 0 && z == 0))
                            {
                                Vector3 blockPos = new Vector3(xpos + x, ypos + y, zpos + z);
                                if (worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z] != null)
                                    if (worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].block != null)
                                        Destroy(worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].block);
                                worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z] = null;
                            }
                        }

                xpos += Random.Range(-1, 2);
                ypos += Random.Range(-1, 2);
                zpos += Random.Range(-1, 2);

                if (xpos < holSize || xpos >= width - holSize) xpos = width / 2;
                if (ypos < holSize || ypos >= height - holSize) ypos = height / 2;
                if (zpos < holSize || zpos >= depth - holSize) zpos = depth / 2;
            }
        }

        for(int z = 1; z < depth - 1; z++)
            for(int x = 1; x < width - 1; x++)
                for(int y = 1; y < height -1; y++)
                {
                    if(worldBlocks[x, y, z] == null)
                    {
                        for(int x1 = -1; x1 < 1; x1++)
                            for (int y1 = -1; y1 < 1; y1++)
                                for (int z1 = -1; z1 < 1; z1++)
                                {
                                    if(!(x1 == 0 && y1 == 0 && z1 == 0))
                                    {
                                        Vector3 neighbour = new Vector3(x + x1, y + y1, z + z1);
                                        DrawBlock(neighbour);
                                    }
                                }
                    }
                }


    }

    void CreateBlock(int y, Vector3 blockPos, bool create)
    {
        GameObject newBlock = null;
        if (y > heightScale / 4 * 3 + heightOffset)
        {
            if (create)
                newBlock = (GameObject)Instantiate(snowBlock, blockPos, Quaternion.identity);
            worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z] = new Block(Block.Type.Snow, create, newBlock);
        }
        else if (y < heightScale / 4 + heightOffset)
        {
            if (create)
                newBlock = (GameObject)Instantiate(sandBlock, blockPos, Quaternion.identity);
            worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z] = new Block(Block.Type.Sand, create, newBlock);
        }
        else
        {
            if (create)
                newBlock = (GameObject)Instantiate(grassBlock, blockPos, Quaternion.identity);
            worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z] = new Block(Block.Type.Grass, create, newBlock);
        }

        //create diamond
        if( y > heightOffset - 2 && y < heightOffset && Random.Range(0, 100) < 5)
        {         
            if(create)
                newBlock = (GameObject)Instantiate(diamondBlock, blockPos, Quaternion.identity);
            worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z] = new Block(Block.Type.Diamond, create, newBlock);
        }
    }

    void DrawBlock(Vector3 blockPos)
    {
        //do not draw block outside of the world
        if (blockPos.x < 0 || blockPos.x >= width || blockPos.y < 0 || blockPos.y >= height || blockPos.z < 0 || blockPos.z >= depth)
            return;

        if (worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z] == null)
            return;

        if (!worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].vis)
        {
            GameObject newBlock = null;
            worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].vis = true;
            if(worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].type == Block.Type.Snow)
                newBlock = (GameObject)Instantiate(snowBlock, blockPos, Quaternion.identity);
            else if (worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].type == Block.Type.Grass)
                newBlock = (GameObject)Instantiate(grassBlock, blockPos, Quaternion.identity);
            else if (worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].type == Block.Type.Sand)
                newBlock = (GameObject)Instantiate(sandBlock, blockPos, Quaternion.identity);
            else if (worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].type == Block.Type.Diamond)
                newBlock = (GameObject)Instantiate(diamondBlock, blockPos, Quaternion.identity);
            else
                worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].vis = false;

            if(newBlock != null)
                worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].block = newBlock;
        }

    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0));
            if(Physics.Raycast(ray, out hit, 1000.0f))
            {
                Vector3 blockPos = hit.transform.position;
                //this is the bottom block. don't delete it
                if ((int)blockPos.y == 0)
                    return;

                worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z] = null;

                Destroy(hit.transform.gameObject);

                for(int x = -1; x <= 1; x++)
                    for (int y = -1; y <= 1; y++)
                        for (int z = -1; z <= 1; z++)
                        {
                            if(!(x == 0 && y==0 && z == 0))
                            {
                                Vector3 neighbour = new Vector3(blockPos.x + x, blockPos.y + y, blockPos.z + z);
                                DrawBlock(neighbour);
                            }
                        }
            }
        }
	}
}
