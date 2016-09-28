using UnityEngine;
using System.Collections;

public class MicLoudness : MonoBehaviour {

    const int OpenMouth = 0;
    const int BlinkLeftEye = 1;
    const int BlinkRightEye = 2;
    SkinnedMeshRenderer skinnedMeshRenderer;
    float blinkTime = 1.0f;
    float startBlinkTime_L;
    float startBlinkTime_R;
    bool leftEyeBlinking = false;
    bool rightEyeBlinking = false;

    // Use this for initialization
    void Start () {
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        float loudness = MicInput.loudness;
        Debug.Log("loudness: " + loudness);
        if(loudness > 10.0f)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(OpenMouth, loudness);
        }
        else
        {
            skinnedMeshRenderer.SetBlendShapeWeight(OpenMouth, 0);
        }

        int random = Random.Range(0, 1000);

        if (random == 25 && !leftEyeBlinking && !rightEyeBlinking)
        {
            leftEyeBlinking = true;
            startBlinkTime_L = Time.time;
            skinnedMeshRenderer.SetBlendShapeWeight(BlinkLeftEye, 100);

            rightEyeBlinking = true;
            startBlinkTime_R = Time.time;
            skinnedMeshRenderer.SetBlendShapeWeight(BlinkRightEye, 100);
        }
        else
        if (random == 50 && !leftEyeBlinking)
        {
            leftEyeBlinking = true;
            startBlinkTime_L = Time.time;
            skinnedMeshRenderer.SetBlendShapeWeight(BlinkLeftEye, 100);
        }
        else
        if (random == 75 && !rightEyeBlinking)
        {
            rightEyeBlinking = true;
            startBlinkTime_R = Time.time;
            skinnedMeshRenderer.SetBlendShapeWeight(BlinkRightEye, 100);
        }
        if(leftEyeBlinking && Time.time > startBlinkTime_L + blinkTime)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(BlinkLeftEye, 0);
            leftEyeBlinking = false;
        }
        if (rightEyeBlinking && Time.time > startBlinkTime_R + blinkTime)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(BlinkRightEye, 0);
            rightEyeBlinking = false;
        }

    }
}
