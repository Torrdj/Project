using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class spawnAI : NetworkBehaviour {

    [SerializeField]
    GameObject AIPrefab;

	// Use this for initialization
	void Start () {
        GameObject AI = (GameObject)Instantiate(AIPrefab, gameObject.transform.position, gameObject.transform.rotation);
        NetworkServer.Spawn(AI);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
