using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class menu : MonoBehaviourPunCallbacks
{
    [Header("screens")]
    public GameObject mainScreen;
    public GameObject lobbyScreen;

    [Header("mainScreen")]
    public Button createRoomBtn;
    public Button joinRoomBtn;

    [Header("lobbuScreen")]
    public Button startGameBtn;
    public TextMeshProUGUI playerListText;


    private void Start()
    {
        createRoomBtn.interactable = false;
        joinRoomBtn.interactable = false;
    }
    
    public override void OnConnectedToMaster()
    {
        createRoomBtn.interactable = true;
        joinRoomBtn.interactable = true;
    }

    void setScreen(GameObject screen)
    {
        mainScreen.SetActive(false);
        lobbyScreen.SetActive(false);

        screen.SetActive(true);
    }

    public void OncreateRoomBtn(TMP_InputField roonNameInput)
    {
        NetWorkManager.instance.CreateRoom(roonNameInput.text);
    }

    public void OnJoinRoomBtn(TMP_InputField roonNameInput)
    {
        NetWorkManager.instance.JoinRoom(roonNameInput.text);
    }

    public void OnplayerNameUpdate(TMP_InputField playerNameInput)
    {
        PhotonNetwork.NickName = playerNameInput.text;
    }

    public override void OnJoinedRoom()
    {
        setScreen(lobbyScreen);

        //since there's now a new player in the lobby, tell everyone to update the lobby
        photonView.RPC("UpdateLobbyUI", RpcTarget.All);
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateLobbyUI();
    }

    //updates the lobby UI to show player list and host buttons
    [PunRPC]
    public void UpdateLobbyUI()
    {
        playerListText.text = "";

        //display all the player currently in the lobby
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            playerListText.text += player.NickName + "\n"; 
        }

        if (PhotonNetwork.IsMasterClient)
        {
            startGameBtn.interactable = true;
        }
        else
        {
            startGameBtn.interactable = false;
        }
    }

    public void OnleaveLobbyBtn()
    {
        PhotonNetwork.LeaveRoom();
        setScreen(mainScreen);
    }

    public void OnstartGameBtn()
    {
        NetWorkManager.instance.photonView.RPC("changeScene", RpcTarget.All, "Game");
    }
}
