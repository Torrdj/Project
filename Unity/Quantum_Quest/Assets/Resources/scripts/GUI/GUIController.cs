using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIController : MonoBehaviour {

    Text statusText, masterText;

	// Use this for initialization
	void Start () {
        statusText = GameObject.Find("statusText").GetComponent<Text>();
        masterText = GameObject.Find("masterText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        statusText.text = "Status : " + PhotonNetwork.connectionStateDetailed.ToString();
        masterText.text = "isMaster : " + PhotonNetwork.isMasterClient; 
	}
}
