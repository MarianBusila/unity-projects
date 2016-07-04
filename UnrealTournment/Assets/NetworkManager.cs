using UnityEngine;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour {

    public GameObject standbyCamera;
    SpawnSpot[] spawnSpots;
    bool connecting = false;
    List<string> chatMessages;
    int maxChatMessages = 5;

    // Use this for initialization
    void Start ()
    {
        spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();
        //Connect();
        PhotonNetwork.player.name = PlayerPrefs.GetString("Username", "Awesome Dude");
        chatMessages = new List<string>();

    }
    void OnDestroy()
    {
        PlayerPrefs.SetString("Username", PhotonNetwork.player.name);
    }

    public void AddChatMessage(string m)
    {
        GetComponent<PhotonView>().RPC("AddChatMessage_RPC", PhotonTargets.AllBuffered, m);
    }

    [PunRPC]
    public void AddChatMessage_RPC(string m)
    {
        while (chatMessages.Count >= maxChatMessages)
            chatMessages.RemoveAt(0);
        chatMessages.Add(m);
    }
    void Connect()
    {
        PhotonNetwork.ConnectUsingSettings("MultiFPS v001");
        
    }
    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        if (PhotonNetwork.connected == false && connecting == false)
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Username: ");
            PhotonNetwork.player.name = GUILayout.TextField(PhotonNetwork.player.name);
            GUILayout.EndHorizontal();


            if (GUILayout.Button("Single Player"))
            {
                connecting = true;
                PhotonNetwork.offlineMode = true;
                OnJoinedLobby();
            }
            if (GUILayout.Button("Multi Player"))
            {
                connecting = true;
                Connect();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        if (PhotonNetwork.connected == true && connecting == false)
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            foreach (string message in chatMessages)
            {
                GUILayout.Label(message);
            }

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }

    void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("OnPhotonRandomJoinFailed");
        PhotonNetwork.CreateRoom(null);
    }
    void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        connecting = false;
        SpawnMyPlayer();
    }
    void SpawnMyPlayer()
    {
        AddChatMessage("Spawing player: " + PhotonNetwork.player.name);
        if(spawnSpots == null)
        {
            Debug.LogError("No spawn points");
            return;
        }

        SpawnSpot mySpawnSpot = spawnSpots[Random.Range(0, spawnSpots.Length)];
        GameObject myPlayerGO = PhotonNetwork.Instantiate("PlayerController", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0) as GameObject;
        standbyCamera.SetActive(false);
        myPlayerGO.transform.FindChild("FirstPersonCharacter").gameObject.SetActive(true);
        ((MonoBehaviour)myPlayerGO.GetComponent("FirstPersonController")).enabled = true;
        ((MonoBehaviour)myPlayerGO.GetComponent("PlayerMovement")).enabled = true;
        ((MonoBehaviour)myPlayerGO.GetComponent("PlayerShooting")).enabled = true;
    }
	
}
