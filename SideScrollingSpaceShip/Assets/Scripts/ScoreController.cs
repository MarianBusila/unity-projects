using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public Text scoreText;
    private float speed;
    private SectionMovement sm;
    public static bool isAlive = true;
    private float score;

    public float Score
    {
        get { return score; }
    }

    // Use this for initialization
    void Start () {
         sm = GetComponent<SectionMovement>();        
	}
	
	// Update is called once per frame
	void Update () {
        speed = sm.speed;
        if (isAlive)
            score = Mathf.Round(Time.time * 10 * speed);
        scoreText.text = "Score: " + score;
	}

}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            