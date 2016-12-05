using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public Text scoreText;
    private float speed;
    private SectionMovement sm;
    // Use this for initialization
    void Start () {
         sm = GetComponent<SectionMovement>();        
	}
	
	// Update is called once per frame
	void Update () {
        speed = sm.speed;
        scoreText.text = "Score: " + Mathf.Round(Time.time * 10 * speed);
	}
}
