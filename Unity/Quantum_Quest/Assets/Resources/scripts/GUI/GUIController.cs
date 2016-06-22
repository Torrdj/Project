using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIController : MonoBehaviour
{
    PeerState lastState;
    Text statusText, masterText;

    // Use this for initialization
    void Start()
    {
        statusText = GameObject.Find("statusText").GetComponent<Text>();
        masterText = GameObject.Find("masterText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        PeerState newState = PhotonNetwork.connectionStateDetailed;
        if (lastState != newState)
        {
            statusText.text = "Status : " + lastState.ToString();
            lastState = newState;
        }
        masterText.text = "isMaster : " + PhotonNetwork.isMasterClient;
    }
}
