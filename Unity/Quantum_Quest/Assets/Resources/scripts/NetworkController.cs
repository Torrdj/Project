using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class NetworkController : MonoBehaviour
{
    private string _gameVersion = "0.1";
    string currentSceneName;
    public string lastSceneName;

    public Transform prefab;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    void OnLevelWasLoaded()
    {
        if (PhotonNetwork.connectionState == ConnectionState.Disconnected)
            PhotonNetwork.ConnectUsingSettings(_gameVersion);
    }

    void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    void OnJoinedLobby()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "first")
            PhotonNetwork.JoinOrCreateRoom("1", new RoomOptions(), new TypedLobby());
        else if (currentSceneName == "second")
            PhotonNetwork.JoinOrCreateRoom("2", new RoomOptions(), new TypedLobby());
    }

    void OnJoinedRoom()
    {
        Transform spawnpoint = GameObject.Find("SpawnPoint").GetComponent<Transform>();

        if (currentSceneName == "first")
            if (lastSceneName != null && lastSceneName == "second")
                spawnpoint = GameObject.Find("SpawnPoint2").GetComponent<Transform>();
        
        PhotonNetwork.Instantiate("prefabs/players/" + prefab.name, spawnpoint.position, spawnpoint.rotation, 0);
        GameObject.Find("Map").GetComponent<AudioSource>().Play();
    }

    void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
        Debug.Log("Can't join the room.");
    }
}
