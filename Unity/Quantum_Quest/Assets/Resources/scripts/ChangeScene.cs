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
                SceneManager.LoadScene("second");
                break;
            case "second":
                GameObject.Find("NetworkHolder").GetComponent<NetworkController>().lastSceneName = "second";
                SceneManager.LoadScene("first");
                break;
        }
    }
}
