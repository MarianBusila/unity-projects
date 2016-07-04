using UnityEngine;
using System.Collections;

public class NetworkCharacterFPS : Photon.MonoBehaviour {

    Vector3 realPosition = Vector3.zero;
    Quaternion realRotation = Quaternion.identity;

    Animator anim;

    bool gotFirstUpdate = false;

    // Use this for initialization
    void Awake () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(photonView.isMine)
        {
            //do nothing
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, realPosition, 0.5f);
            transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 0.5f);
        }
	}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.isWriting)
        {
            //This is OUR player. We need to send our actual position to the network
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(anim.GetFloat("Speed"));
        }
        else
        {
            //this is someone elses's player. We need to receive their position and update our version of that player
            realPosition = (Vector3)stream.ReceiveNext();
            realRotation = (Quaternion)stream.ReceiveNext();
            anim.SetFloat("Speed", (float)stream.ReceiveNext());

            if(gotFirstUpdate == false)
            {
                transform.position = realPosition;
                transform.rotation = realRotation;
                gotFirstUpdate = true;
            }
        }
    }
}
