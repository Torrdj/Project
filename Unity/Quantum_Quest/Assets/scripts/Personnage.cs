using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Personnage : NetworkBehaviour
{

    protected bool load = true;
    protected string m_name;

    [SyncVar][SerializeField]
    protected float m_vie, m_vieMax,
        m_mana, m_manaMax,
        m_attaque, m_defense,
        m_vitesseAtt;

    protected bool m_isParalyzed = false;

    Text pv, pv_e;
    
    Personnage cible;

    [SyncVar][SerializeField]
    float SyncDamages;
    [SyncVar][SerializeField]
    uint SyncId;

    public uint my_id;

    // Use this for initialization
    void Start()
    {
        m_vieMax = 1000; m_vie = m_vieMax;
        m_manaMax = 1000; m_mana = m_manaMax;
        m_attaque = 50; m_defense = 20;
        m_vitesseAtt = 1.0f;
        my_id = netId.Value;

        if (isLocalPlayer)
        {
            tag = "Player";
            pv = GameObject.Find("PV").GetComponent<Text>();
            pv_e = GameObject.Find("PV_Ennemi").GetComponent<Text>();
            pv_e.enabled = false;
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
            pv.text = "PV : " + m_vie + " / " + m_vieMax;

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.CompareTag("Ennemi"))
                    {
                        cible = hit.transform.gameObject.GetComponent<Personnage>();
                        pv_e.enabled = true;
                    }
                    else
                    {
                        cible = null;
                        pv_e.enabled = false;
                    }
                }
            }

            if (cible != null)
            {
                pv_e.text = "PV Ennemi : " + cible.Vie + "/" + cible.VieMax;
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    if(isServer)
                        attaque(cible, coupDeMolette);
                    else
                        TransmitDamages(cible.netId.Value, coupDeMolette);
                }
            }
        }
        
        if (netId.Value == SyncId)
        {
            Debug.Log("bah alors ?");
            receiveDamages(SyncDamages);
        }
    }

    public void attaque(Personnage cible, float damages)
    {
        cible.receiveDamages(damages);
        new System.Threading.Thread(() =>
        {
            System.Threading.Thread.Sleep(Convert.ToInt32(1000 * m_vitesseAtt));
            load = true;
        }).Start();
        load = false;
    }

    [Command]
    void CmdSendDamages(uint id, float damages)
    {
        SyncId = id;
        SyncDamages = damages;
    }
    [Client]
    void TransmitDamages(uint id, float damages)
    {
        CmdSendDamages(id, damages);
    }

    void receiveDamages(float damages)
    {
        if (damages > m_defense)
            m_vie -= damages - m_defense;

        if (m_vie <= 0)
            m_vie = 0;

        if (m_vie >= m_vieMax)
            m_vie = m_vieMax;
    }


    #region Getter/Setter
    public bool IsLoad
    {
        get { return load; }
    }

    public bool IsParalysed
    {
        get { return m_isParalyzed; }
    }

    public float coupDeMolette//attaque de base ET getter
    {
        get { return m_attaque; }
    }

    public float Defense
    {
        get { return m_defense; }
        set { m_defense = value; }
    }


    public float Vie
    {
        get { return m_vie; }
        set { m_vie = value; }
    }

    public float VieMax
    {
        get { return m_vieMax; }
        set { m_vieMax = value; }
    }

    public float Mana
    {
        get { return m_mana; }
        set { m_mana = value; }
    }

    public float VitAtt
    {
        get { return m_vitesseAtt; }
        set { m_vitesseAtt = value; }
    }
    #endregion

}
