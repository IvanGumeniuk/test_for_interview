using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Networking : Photon.PunBehaviour{
    [SerializeField]
    private GameObject Players;
    /*
        // Use this for initialization
        private string m_GameVersion = "0.1";
        public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;
        private string roomName = "room1";
        private RoomOptions roomOptions;

        private void Awake()
        {
            PhotonNetwork.autoJoinLobby = false;
            PhotonNetwork.automaticallySyncScene = true;
            PhotonNetwork.logLevel = Loglevel;
            roomOptions = new RoomOptions() { isVisible = true, maxPlayers = 2 };
        }

        void Start () {
            Connect();
        }

        public override void OnConnectedToMaster()
        {
            print("OnConnected");
            PhotonNetwork.JoinOrCreateRoom(roomName/* + System.Guid.NewGuid().ToString("N")*//*, roomOptions, TypedLobby.Default);
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 100, 20), roomName);
        }

        public override void OnDisconnectedFromPhoton()
        {
            print("OnDisconnected");
            PhotonNetwork.ReconnectAndRejoin();
        }



        public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
        {
            print("FAILED : PhotonNetwork.CreateRoom(roomName, new RoomOptions() {maxPlayers = 4}, null);");
        }
        public override void OnJoinedRoom()
        {
            print("This client is in the room"+ PhotonNetwork.room.name);
        }

        private void Connect()
        {
            if(PhotonNetwork.connected)
            {
               // PhotonNetwork.JoinRandomRoom();
                PhotonNetwork.JoinRoom(roomName );
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings(m_GameVersion);
            }
        }

        // Update is called once per frame
        void Update () {

        }

        */

   
    // Use this for initialization
    void Start()
    {
        Connect();

    }

    void Connect()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
       // PhotonNetwork.offlineMode = true;

    }
    void OnGUI()
    {
   //     GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
    void OnConnectedToMaster()
    {
        Debug.Log("Starting Connection");
        PhotonNetwork.JoinRandomRoom();
    }
    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Connection Failed");
        PhotonNetwork.CreateRoom(null);
    }
    void OnJoinedRoom()
    {
        Debug.Log("Connect to Server");
        SpawnMyPlayer();
    }
    void SpawnMyPlayer()
    {
        GameObject myPlayerGO = (GameObject)PhotonNetwork.Instantiate("Player123", Players.transform.position, transform.rotation, 0);
       // GameObject myPlayerCamera = (GameObject)PhotonNetwork.Instantiate("vThirdPersonCamera", transform.position, transform.rotation, 0);
    }

}
