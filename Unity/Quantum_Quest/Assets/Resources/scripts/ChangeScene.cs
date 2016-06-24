using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    void OnTriggerEnter(Collider coll)
    {
        PhotonNetwork.Disconnect();
        string currentSceneName = SceneManager.GetActiveScene().name;
        switch(currentSceneName)
        {
            case "first":
                GameObject.Find("NetworkHolder").GetComponent<NetworkController>().lastSceneName = "first";
                GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>().SceneToLoad = "second";
                break;
            case "second":
                GameObject.Find("NetworkHolder").GetComponent<NetworkController>().lastSceneName = "second";
                GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>().SceneToLoad = "first";
                break;
        }
        SceneManager.LoadScene("loadingScene");
    }
}
