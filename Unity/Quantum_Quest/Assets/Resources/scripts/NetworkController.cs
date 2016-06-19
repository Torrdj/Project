using UnityEngine;
using System.Collections;

public class NetworkController : MonoBehaviour {

    private string _gameVersion = "0.1";

    public Transform prefab;

	// Use this for initialization
	void Start () {
        PhotonNetwork.ConnectUsingSettings(_gameVersion);
	}
	
    void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("1", new RoomOptions(), new TypedLobby());
    }

    void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("prefabs/players/" + prefab.name, prefab.position, transform.rotation, 0);
    }

    void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
        
    }

	// Update is called once per frame
	void Update () {
        //Debug.Log("Status : " + PhotonNetwork.connectionStateDetailed.ToString());
	}
}
