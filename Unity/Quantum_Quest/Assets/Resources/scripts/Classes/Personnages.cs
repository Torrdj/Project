﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Personnages : MonoBehaviour
{
    protected PhotonView myView;
    protected PlayerInfo info;
    protected PlayerInfo.TYPES type;

    protected GameObject EnnemiProfile;
    protected GameObject AIProfile;

    protected int cible = -1;
    protected int attaquant = -1;

    protected float m_vie, m_vieMax,
        m_mana, m_manaMax,
        m_attaque, m_defense,
        m_vitesseAtt;

    public string m_name;
    public bool _isLoad = true;
    public bool _dead = false;

    public bool m_isParalyzed = false;

    public bool turbo_boost_isactiavte = false;
    public bool Dot_isactive = false;
    public bool def_isreduced = false;
    public bool speed_isreduced = false;
    public bool def_isincrese = false;

    float lastTime;

    public void Start()
    {
        myView = gameObject.GetComponent<PhotonView>();
        info = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>();
        EnnemiProfile = GameObject.Find("EnnemiProfile");
        AIProfile = GameObject.Find("AIProfile");

        if (tag == "Player")
        {
            if (myView.isMine)
            {
                tag = "Player";//Just in case...

                m_name = info.Name;
                m_vieMax = 1000; m_vie = m_vieMax;
                m_manaMax = 100; m_mana = m_manaMax;
                m_attaque = 50; m_defense = 20;
                m_vitesseAtt = 1.0f;
                GameObject.Find("PlayerProfile").GetComponent<PlayerState>().ViewID = myView.viewID;

                EnnemiProfile.SetActive(false);
                AIProfile.SetActive(false);

                info.Vie = m_vie; info.VieMax = m_vieMax;
                info.Mana = m_mana; info.ManaMax = m_manaMax;
                m_name = info.Name;

                lastTime = Time.fixedTime;
            }
            else
            {
                tag = "Ennemis";
            }
        }
    }

    public void FixedUpdate()
    {
        if (myView.isMine && gameObject.layer != 9)
        {
            if (m_vie <= 0)
            {
                _dead = true;

                transform.rotation = Quaternion.Lerp(transform.rotation,
                    Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z - 90),
                    Time.deltaTime * 10);
            }

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.CompareTag("Ennemis"))
                    {
                        cible = hit.collider.gameObject.GetComponentInParent<PhotonView>().viewID;

                        if (hit.collider.gameObject.layer != 9)
                        {
                            EnnemiProfile.SetActive(true);
                            GameObject.Find("EnnemiProfile").GetComponent<TargetState>().ViewID = cible;
                        }
                        else
                        {
                            AIProfile.SetActive(true);
                            GameObject.Find("AIProfile").GetComponent<AITargetState>().ViewID = cible;
                        }
                    }
                    else
                    {
                        cible = -1;
                        if(EnnemiProfile.GetActive())
                        {
                        EnnemiProfile.GetComponent<TargetState>().ViewID = cible;
                        EnnemiProfile.SetActive(false);
                        }
                        else
                        {
                            AIProfile.GetComponent<AITargetState>().ViewID = cible;
                            AIProfile.SetActive(false);
                        }
                    }
                }
            }

            if (!isDead)
            {
                float newTime;
                if ((newTime = Time.fixedTime) - lastTime >= 2)
                {
                    updateManaRPC(-3);
                    lastTime = newTime;
                }
                if (!IsParalysed && isLoad)
                {
                    if (cible != -1 && Input.GetKey(KeyCode.Alpha1)
                        && checkMana(3))
                    {
                        updateManaRPC(3);
                        coupDeMolette(cible);
                        StartCoroutine(Loading());
                    }
                }
            }
        }
    }

    #region CoupDeMolette
    protected IEnumerator Loading()
    {
        _isLoad = false;
        yield return new WaitForSeconds(m_vitesseAtt);
        _isLoad = true;
    }

    [PunRPC]
    protected void coupDeMolette(int viewID)
    {
        GameObject cible = PhotonView.Find(viewID).gameObject;

        cible.SendMessage("receiveDamages", m_attaque);

        if (GetComponent<PhotonView>().isMine)
        {
            myView.RPC("coupDeMolette", PhotonTargets.OthersBuffered, viewID);
        }
    }
    #endregion

    protected bool checkMana(float mana)
    {
        return mana <= m_mana;
    }

    [PunRPC]
    public void updateManaRPC(float mana)
    {
        SendMessage("updateMana", mana);

        if (GetComponent<PhotonView>().isMine)
        {
            myView.RPC("updateManaRPC", PhotonTargets.OthersBuffered, mana);
        }
    }

    protected void updateMana(float mana)
    {
        m_mana -= mana;

        if (m_mana >= m_manaMax)
            m_mana = m_manaMax;

        if (m_mana <= 0)
            m_mana = 0;

        if (myView.isMine)
            info.Mana = m_mana;
    }

    protected void receiveDamages(float damages)
    {
        if (damages > m_defense)
            m_vie -= damages - m_defense;

        if (m_vie <= 0)
            m_vie = 0;

        if (m_vie >= m_vieMax)
            m_vie = m_vieMax;

        if (myView.isMine)
            info.Vie = m_vie;
    }

    #region Getters/Setters
    public bool isDead
    {
        get { return _dead; }
        set { _dead = value; }
    }

    public PlayerInfo.TYPES Type
    {
        get { return type; }
    }

    public bool isLoad
    {
        get { return _isLoad; }
        set { _isLoad = value; }
    }

    public float Defense
    {
        get { return m_defense; }
        set { m_defense = value; }
    }

    protected void UpdateDef(float def)
    {
        if (def_isreduced)
            def_isreduced = false;
        else if (def_isincrese)
            def_isincrese = false;
        else if (!def_isincrese && !def_isreduced && def > m_defense)
            def_isincrese = true;
        else if (!def_isincrese && !def_isreduced && def < m_defense)
            def_isreduced = true;

        m_defense = def;
    }

    protected void Update_DoT()
    {
        if (Dot_isactive)
            Dot_isactive = false;
        else
            Dot_isactive = true;
    }

    protected void UpdateVit(float vit)
    {
        if (speed_isreduced)
            speed_isreduced = false;
        else
            speed_isreduced = true;

        m_vitesseAtt = vit;
    }

    public bool IsParalysed
    {
        get { return m_isParalyzed; }
        set { m_isParalyzed = value; }
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

    public float ManaMax
    {
        get { return m_manaMax; }
        set { m_manaMax = value; }
    }

    public float Attack
    {
        get { return m_attaque; }
        set { m_attaque = value; }
    }

    public float VitAtt
    {
        get { return m_vitesseAtt; }
        set { m_vitesseAtt = value; }
    }
    #endregion
}
