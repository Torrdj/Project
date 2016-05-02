using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class Personnage : NetworkBehaviour
{

    protected bool load = true;
    protected string m_name;


    protected bool m_isParalyzed = false;


    [SyncVar][SerializeField]
    float SyncDamages;
    [SyncVar][SerializeField]
    uint SyncId;

    public uint my_id;

    // Use this for initialization
    void Start()
    {
        FindObjectOfType<NetworkManagerHUD>().enabled = false;
        my_id = netId.Value;

        if (isLocalPlayer)
        {
            tag = "Player";
        }
        else
        {
            tag = "Ennemi";
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isLocalPlayer)
        {
        }
    }

    public void attaque(Personnage cible, float damages)
    {
        //cible.receiveDamages(damages);
        new System.Threading.Thread(() =>
        {
            //System.Threading.Thread.Sleep(Convert.ToInt32(1000 * m_vitesseAtt));
            load = true;
        }).Start();
        load = false;
    }

    [Command]
    void CmdSendDamages(uint id, float damages)
    {

    }
    [Client]
    void TransmitDamages(uint id, float damages)
    {

    }



}
