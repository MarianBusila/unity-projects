using UnityEngine;
using System.Collections;

public class SectionMovement : MonoBehaviour {

    public GameObject sectionPrefab;
    public GameObject obstaclePrefab;
    public int numSections = 10;
    public float sectionSize = 6f;
    public float speed = 0.25f;

    GameObject[] sections; 
	// Use this for initialization
	void Start () {
        sections = new GameObject[numSections];

        for (int i = 0; i < numSections; i++)
        {
            int randomX = Random.Range(-2, 2);
            int randomY = Random.Range(0, 4);
            Vector3 sectionPos = new Vector3(0, 0, i * sectionSize);
            sections[i] = Instantiate(sectionPrefab, sectionPos, sectionPrefab.transform.rotation) as GameObject;
            Vector3 obstaclePos = sectionPos + new Vector3(randomX, randomY, 0);
            GameObject obstacle = Instantiate(obstaclePrefab, obstaclePos, obstaclePrefab.transform.rotation) as GameObject;
            obstacle.transform.parent = sections[i].transform;
            sections[i].name = "Section_" + i;
        }
	
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < numSections; i++)
        {
            if(sections[i].transform.position.z < -6)
            {
                float maxZ = 0;
                for(int j = 0 ; j < numSections; j++)
                {
                    if (maxZ < sections[j].transform.position.z)
                        maxZ = sections[j].transform.position.z;
                }
                sections[i].transform.position = new Vector3(sections[i].transform.position.x, sections[i].transform.position.y, maxZ + sectionSize);
                break;

            }
            sections[i].transform.Translate(Vector3.forward * Time.deltaTime * -speed);
        }
	}
}
