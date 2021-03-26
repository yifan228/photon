using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetWorkManager : MonoBehaviourPunCallbacks
{
    public static NetWorkManager instance;
    void Awake()
    {
        if (instance !=null & instance !=this)
        {
            gameObject.SetActive(false);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CreateRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName);
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    [PunRPC]
    public void changeScene(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }


    //public override void OnConnectedToMaster()
    //{
    //    Debug.Log("Connect to master server");
    //    CreateRoom("testRoom");
    //}
    //public override void OnCreatedRoom()
    //{
    //    Debug.Log("Created room: " + PhotonNetwork.CurrentRoom.Name);
    //}
}
