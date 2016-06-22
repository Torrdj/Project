using UnityEngine;
using System.Collections;

public class Server : Personnages
{
    protected bool ddos_load = true;
    protected bool firewall_load = true;
    /*
    public Server(string name, GameObject player) : base (name, player)
    {
        m_vieMax *= 1.5f; m_vie = m_vieMax;
        m_defense *= 1.2f;
        m_vitesseAtt *= 1.25f;
    }*/

    // Use this for initialization
    new void Start()
    {
        base.Start();

        if (tag == "Player")
        {
            if (myView.isMine)
            {
                m_vieMax *= 1.5f; m_vie = m_vieMax;
                m_defense *= 1.2f;
                m_vitesseAtt *= 1.25f;

                info.Vie = m_vie; info.VieMax = m_vieMax;
            }
        }
    }

    // Update is called once per frame
    new void FixedUpdate()
    {
        base.FixedUpdate();

        if (!isDead && !IsParalysed)
        {
            if (isLoad)
            {
                if (ddos_load && cible != -1 && Input.GetKey(KeyCode.Alpha2))
                {
                    DDOS(cible);
                    StartCoroutine(Loading());
                }
                else if (firewall_load && Input.GetKey(KeyCode.Alpha3))
                {
                    firewallRPC(cible);
                    StartCoroutine(Loading());
                }
            }
        }
    }

    #region DDOS
    [PunRPC]
    public void DDOS(int viewID)
    {
        GameObject cible = PhotonView.Find(viewID).gameObject;

        StartCoroutine(Paralyze(cible));

        StartCoroutine(LoadingDDOS());

        if(GetComponent<PhotonView>().isMine)
        {
            myView.RPC("DDOS", PhotonTargets.OthersBuffered, viewID);
        }
    }

    IEnumerator Paralyze(GameObject cible)
    {
        cible.GetComponent<Personnages>().IsParalysed = true;
        yield return new WaitForSeconds(3);
        cible.GetComponent<Personnages>().IsParalysed = false;
    }

    IEnumerator LoadingDDOS()
    {
        ddos_load = false;
        yield return new WaitForSeconds(20);
        ddos_load = true;
    }
    #endregion

    #region Firewall
    [PunRPC]
    public void firewallRPC(int viewID)
    {
        SendMessage("firewall");

        if(GetComponent<PhotonView>().isMine)
        {
            myView.RPC("firewallRPC", PhotonTargets.OthersBuffered, viewID);
        }
    }

    void firewall()
    {
        float oldDef = m_defense;
        float newDef = m_defense * 2;

        StartCoroutine(UpdateDefense(newDef, oldDef));

        StartCoroutine(LoadingFirewall());
    }

    IEnumerator UpdateDefense(float newDef, float oldDef)
    {
        Defense = newDef;
        yield return new WaitForSeconds(5);
        Defense = oldDef;
    }

    IEnumerator LoadingFirewall()
    {
        firewall_load = false;
        yield return new WaitForSeconds(20);
        firewall_load = true;
    }
    #endregion
}
