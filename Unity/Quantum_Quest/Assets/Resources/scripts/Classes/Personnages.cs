using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Personnages : MonoBehaviour
{
    protected PhotonView myView;
    protected PlayerInfo info;
    
    protected GameObject EnnemiProfile;

    protected int cible = -1;

    [SerializeField]
    protected float m_vie, m_vieMax,
        m_mana, m_manaMax,
        m_attaque, m_defense,
        m_vitesseAtt;

    protected string m_name;
    protected bool m_isParalyzed = false;
    protected bool _isLoad = true;
    protected bool _dead = false;

    public void Start()
    {
        myView = gameObject.GetComponent<PhotonView>();
        info = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>();
        EnnemiProfile = GameObject.Find("EnnemiProfile");

        if (tag == "Player")
        {
            if (myView.isMine)
            {
                tag = "Player";//Just in case...
                
                m_vieMax = 1000; m_vie = m_vieMax;
                m_manaMax = 1000; m_mana = m_manaMax;
                m_attaque = 50; m_defense = 20;
                m_vitesseAtt = 1.0f;

                EnnemiProfile.SetActive(false);

                info.Vie = m_vie; info.VieMax = m_vieMax;
            }
            else
            {
                tag = "Ennemis";
            }
        }
    }

    public void FixedUpdate()
    {
        if (myView.isMine)
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
                        EnnemiProfile.SetActive(true);
                    }
                    else
                    {
                        cible = -1;
                        EnnemiProfile.SetActive(false);
                    }
                }
            }

            if (!isDead && cible != -1)
            {
                if (isLoad)
                {
                    if (Input.GetKey(KeyCode.Alpha1))
                    {
                        Debug.Log("1");
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

        cible.SendMessage("receiveDamages", 50);

        if (GetComponent<PhotonView>().isMine)
        {
            myView.RPC("coupDeMolette", PhotonTargets.OthersBuffered, viewID);
        }
    }
    #endregion

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
    }

    public bool isLoad
    {
        get { return _isLoad; }
    }

    public float Defense
    {
        get { return m_defense; }
        private set { m_defense = value; }
    }

    protected void UpdateDef(float def)
    {
        m_defense = def;
    }

    public bool IsParalysed
    {
        get { return m_isParalyzed; }
    }

    protected float Vie
    {
        get { return m_vie; }
        set { m_vie = value; }
    }

    protected float VieMax
    {
        get { return m_vieMax; }
        set { m_vieMax = value; }
    }

    protected float Mana
    {
        get { return m_mana; }
        set { m_mana = value; }
    }

    protected float VitAtt
    {
        get { return m_vitesseAtt; }
        set { m_vitesseAtt = value; }
    }
    #endregion
}
