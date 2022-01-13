using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviourPunCallbacks
{
    [Tooltip("Content Object")]
    public GameObject ScrollViewContent;

    [Tooltip("UI Row Room Prefab")]
    public GameObject RowRoom;

    [Tooltip("Input Player Name")]
    public GameObject InputPlayerName;

    [Tooltip("Input Room Name")]
    public GameObject InputRoomName;

    [Tooltip("Status Label")]
    public GameObject Status;

    [Tooltip("Button Create Room")]
    public GameObject BtnCreateRoom;

    [Tooltip("Panel Lobby")]
    public GameObject PanelLobby;

    [Tooltip("Panel waiting for other player")]
    public GameObject PanelWaitingForPlayer;


    List<RoomInfo> availableRooms = new List<RoomInfo>();

    UnityEngine.Events.UnityAction buttonCallback;



    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;


        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = "1.1";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        AssignPlayerNumber(1);
    }

    public void OnClickCreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = (byte)2;

        PhotonNetwork.JoinOrCreateRoom(InputRoomName.GetComponent<TMP_InputField>().text,
            roomOptions, TypedLobby.Default);
      
    }

    public override void OnCreatedRoom()
    {
        PhotonNetwork.NickName = InputPlayerName.GetComponent<TMP_InputField>().text;
        //PhotonNetwork.LoadLevel("MainGame");

    }

    public override void OnJoinedRoom()
    {
        print("on joined room");
        PanelLobby.SetActive(false);
        PanelWaitingForPlayer.SetActive(true);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
     
        if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.LoadLevel("MainGame");
        }

    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        print("Number of Rooms:" + roomList.Count);
        availableRooms = roomList;
        UpdateRoomList();

        if (roomList.Count > 0)
        {
            InputPlayerName.GetComponent<TMP_InputField>().text = "Player 2";
            AssignPlayerNumber(2);
        }

    }

    private void UpdateRoomList()
    {
        foreach(RoomInfo roomInfo in availableRooms)
        {
            //Instatiate the row room prefab
            GameObject rowRoom = Instantiate(RowRoom);
            rowRoom.transform.parent = ScrollViewContent.transform;
            rowRoom.transform.localScale = Vector3.one;

            //update the prefab with room details
            rowRoom.transform.Find("RoomName").GetComponent<TextMeshProUGUI>().text = roomInfo.Name;
            rowRoom.transform.Find("RoomPlayers").GetComponent<TextMeshProUGUI>().text
                = roomInfo.PlayerCount.ToString();

            //assign button callback
            buttonCallback = () => this.OnClickJoinRoom(roomInfo.Name);
            rowRoom.transform.Find("BtnJoin").GetComponent<Button>().onClick.AddListener(buttonCallback);

        }

    }

    public void OnClickJoinRoom(string roomName)
    {
        //set our player name
        PhotonNetwork.NickName = InputPlayerName.GetComponent<TMP_InputField>().text;
        //join the room
        PhotonNetwork.JoinRoom(roomName);
    }

    private void OnGUI()
    {
        Status.GetComponent<TextMeshProUGUI>().text = "Status:" + PhotonNetwork.NetworkClientState.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignPlayerNumber(int player)
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Player"))
        {
            PhotonNetwork.LocalPlayer.CustomProperties["Player"] = player;
        }
        else
        {
            //setting player properties; Player 1 = 0, Player2 = 1
            ExitGames.Client.Photon.Hashtable playerProps = new ExitGames.Client.Photon.Hashtable
            {
                {"Player", player}
            };
            PhotonNetwork.SetPlayerCustomProperties(playerProps);
        }
    }
}
