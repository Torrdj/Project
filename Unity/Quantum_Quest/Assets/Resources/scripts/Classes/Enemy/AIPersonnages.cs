using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AIPersonnages : MonoBehaviour
{
    protected PhotonView myView;
    protected PlayerInfo.TYPES type;

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

    public void Start()
    {
        m_name = "Ennemi";
        m_vieMax = 1000; m_vie = m_vieMax;
        m_manaMax = 100; m_mana = m_manaMax;
        m_attaque = 50; m_defense = 20;
        m_vitesseAtt = 1.0f;

    }

    public void FixedUpdate()
    {
        if (m_vie <= 0)
        {
            _dead = true;

            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z - 90),
                Time.deltaTime * 10);
            Destroy(gameObject, 2);
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
    }

    protected void receiveDamages(float damages)
    {
        if (damages > m_defense)
            m_vie -= damages - m_defense;

        if (m_vie <= 0)
            m_vie = 0;

        if (m_vie >= m_vieMax)
            m_vie = m_vieMax;
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
