using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class checkPrefab : NetworkBehaviour
{

    [SerializeField]
    GameObject perso;
    [SerializeField]
    GameObject ntmgr;
    NetworkManager ntmgr_;

    // Use this for initialization
    void Start()
    {
        ntmgr = FindObjectOfType<NetworkManager>().gameObject;
        ntmgr_ = ntmgr.GetComponent<NetworkManager>();
        
        StartCoroutine(Example());
    }

    IEnumerator Example()
    {
        yield return new WaitForSeconds(5);
        perso = FindObjectOfType<Personnage>().gameObject;
        if (perso.name != ntmgr_.playerPrefab.name + "(Clone)")
        {
            Destroy(FindObjectOfType<Personnage>().gameObject);
            CmdChangePlayer(ntmgr);
        }
    }

    [Command]
    void CmdChangePlayer(GameObject nt)
    {
        GameObject player = (GameObject)Instantiate(nt.GetComponent<NetworkManager>().playerPrefab);
        NetworkServer.Spawn(player);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
