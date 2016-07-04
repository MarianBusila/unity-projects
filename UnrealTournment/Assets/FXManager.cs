using UnityEngine;
using System.Collections;

public class FXManager : MonoBehaviour {

    public GameObject sniperBulletFXPrefab;
    [PunRPC]
    void SniperBulletFX(Vector3 startPos, Vector3 endPos)
    {
        Debug.Log("SniperBulletFX");
        GameObject sniperFX = Instantiate(sniperBulletFXPrefab, startPos, Quaternion.LookRotation(endPos -startPos)) as GameObject;
        LineRenderer lr = sniperFX.transform.Find("LineFX").GetComponent<LineRenderer>();
        lr.SetPosition(0, startPos);
        lr.SetPosition(1, endPos);
      
    }
}
