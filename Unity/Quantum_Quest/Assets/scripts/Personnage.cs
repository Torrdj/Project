using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class Personnage : NetworkBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;

    [SyncVar]
    protected float m_vie, m_vieMax,
        m_mana, m_manaMax,
        m_attaque, m_defense,
        m_vitesseAtt;

    protected string m_name;
    protected bool m_isParalyzed = false;

    protected bool load = true;

    Text pv, pv_e;
    Image pvBarre, pvBarre_e, pvBarre_e2;
    RectTransform pvBtrsf, pvBtrsf_e;

    Personnage cible;
    Vector3 posCible;

    void Start()
    {
        m_vieMax = 1000; m_vie = m_vieMax;
        m_manaMax = 1000; m_mana = m_manaMax;
        m_attaque = 50; m_defense = 20;
        m_vitesseAtt = 1.0f;

        posCible = new Vector3(0, -10, 0);

        if (isLocalPlayer)
        {
            tag = "Player";

            pv = GameObject.Find("PV").GetComponent<Text>();
            pvBarre = GameObject.Find("healthBar").GetComponent<Image>();
            pvBtrsf = pvBarre.GetComponent<RectTransform>();

            pv_e = GameObject.Find("PV_Ennemi").GetComponent<Text>();
            pvBarre_e = GameObject.Find("healthBar_e").GetComponent<Image>();
            pvBarre_e2 = GameObject.Find("healthBar_e (1)").GetComponent<Image>();
            pvBtrsf_e = pvBarre_e.GetComponent<RectTransform>();
            pv_e.enabled = false;
            pvBarre_e.enabled = false;
            pvBarre_e2.enabled = false;
        }
        else
        {
            tag = "Ennemi";
        }
    }

    void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            pv.text = "PV : " + m_vie + " / " + m_vieMax;
            pvBtrsf.sizeDelta = new Vector2(m_vie / m_vieMax * 300, 10);

            if (Input.GetKey(KeyCode.I))
            {
                GameObject.FindObjectOfType<InventoryManager>().ToogleInventory();
            }

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.CompareTag("Ennemi"))
                    {
                        cible = hit.transform.GetComponent<Personnage>();
                        posCible = hit.transform.position;
                        pv_e.enabled = true;
                        pvBarre_e.enabled = true;
                        pvBarre_e2.enabled = true;
                    }
                    else
                    {
                        cible = null;
                        posCible = new Vector3(0, -100, 0);
                        pv_e.enabled = false;
                        pvBarre_e.enabled = false;
                        pvBarre_e2.enabled = false;
                    }
                }

            }

            if (cible != null)
            {
                pv_e.text = "PV Ennemi : " + cible.Vie + "/" + cible.VieMax;
                pvBtrsf_e.sizeDelta = new Vector2(cible.Vie / cible.VieMax * 300, 10);
            }

            if (posCible != new Vector3(0, -100, 0))
            {
                if (Input.GetKey(KeyCode.Alpha1))
                {
                    CmdTellMyShotToTheServer(posCible);
                }
            }
        }
    }
    public void receiveDamages(float damages)
    {
        if (damages > m_defense)
            m_vie -= damages - m_defense;

        if (m_vie <= 0)
            m_vie = 0;

        if (m_vie >= m_vieMax)
            m_vie = m_vieMax;
    }

    #region COMMANDES
    [Command]
    void CmdTellMyShotToTheServer(Vector3 posTarget)
    {
        GameObject bullet = Instantiate(bulletPrefab, posTarget, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(bullet);
    }
    #endregion

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
