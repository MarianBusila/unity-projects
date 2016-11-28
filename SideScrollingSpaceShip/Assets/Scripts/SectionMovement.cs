using UnityEngine;
using System.Collections;

public class SectionMovement : MonoBehaviour {

    public GameObject sectionPrefab;
    public int numSections = 10;
    public float sectionSize = 6f;
    public float speed = 0.25f;

    GameObject[] sections; 
	// Use this for initialization
	void Start () {
        sections = new GameObject[numSections];

        for(int i = 0; i < numSections; i++)
        {
            sections[i] = Instantiate(sectionPrefab, new Vector3(0, 0, i * sectionSize), sectionPrefab.transform.rotation) as GameObject;
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
