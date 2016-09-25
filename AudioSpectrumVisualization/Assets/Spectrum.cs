using UnityEngine;
using System.Collections;

public class Spectrum : MonoBehaviour {

    public GameObject prefab;
    public int numberOfObjects = 20;
    public float radius = 5f;

    GameObject[] cubes;
    float speed = 10f;

    void Start()
    {
        cubes = new GameObject[numberOfObjects];
        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            GameObject cube = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
            cubes[i] = cube;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float[] samples = new float[1024];
        AudioListener.GetSpectrumData(samples, 0, FFTWindow.Hamming);
        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 previousScale = cubes[i].transform.localScale;
            previousScale.y = Mathf.Lerp(previousScale.y, samples[i] * 40, Time.deltaTime * speed);
            cubes[i].transform.localScale = previousScale;
        }
    }
}
