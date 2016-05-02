using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class Movements : NetworkBehaviour
{

    [SerializeField]
    private float speed = 7.0f;
    [SerializeField]
    private float rotSpeed = 250.0f;
    [SerializeField]
    float m_MovingTurnSpeed = 1000;
    [SerializeField]
    float m_StationaryTurnSpeed = 500;

    [SerializeField]
    GameObject bulletPrefab;

    Text pv, pv_e;
    Image pvBarre, pvBarre_e;
    RectTransform pvBtrsf, pvBtrsf_e;
    
    [SerializeField]
    Movements cible;

    [SyncVar]
    protected float m_vie, m_vieMax,
        m_mana, m_manaMax,
        m_attaque, m_defense,
        m_vitesseAtt;

    private Transform myTransform;
    private Vector3 lastPosition;
    private Quaternion lastRotation;

    private Transform m_Cam;
    private Vector3 m_CamForward;
    private Vector3 m_Move;
    float m_TurnAmount;
    float m_ForwardAmount;

    Vector3 posCible;

    [SyncVar]
    private Vector3 SyncPosition;
    [SyncVar]
    private Quaternion SyncRotation;

    void Start()
    {
        myTransform = GetComponent<Transform>();
        lastPosition = myTransform.position;
        lastRotation = myTransform.rotation;
        TransmitPosition();//Initialise la position sur le serveur
        TransmitRotation();//Idem

        m_Cam = myTransform.GetComponentInChildren<Camera>().transform;

        m_vieMax = 1000; m_vie = m_vieMax;
        m_manaMax = 1000; m_mana = m_manaMax;
        m_attaque = 50; m_defense = 20;
        m_vitesseAtt = 1.0f;

        posCible = new Vector3(0, -10, 0);

        if (isLocalPlayer)
        {
            myTransform.FindChild("Camera").GetComponent<Camera>().enabled = true;

            pv = GameObject.Find("PV").GetComponent<Text>();
            pvBarre = GameObject.Find("healthBar").GetComponent<Image>();
            pvBtrsf = pvBarre.GetComponent<RectTransform>();

            pv_e = GameObject.Find("PV_Ennemi").GetComponent<Text>();
            pvBarre_e = GameObject.Find("HealthBar_e").GetComponent<Image>();
            pvBtrsf_e = pvBarre_e.GetComponent<RectTransform>();
            pv_e.enabled = false;
            pvBarre_e.enabled = false;
        }
    }

    void Move(Vector3 move)
    {
        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);
        
        m_TurnAmount = Mathf.Atan2(move.x, move.z);
        m_ForwardAmount = move.z;

        myTransform.Translate(move * Time.deltaTime * speed, Space.Self);
        ApplyExtraTurnRotation();
    }

    void ApplyExtraTurnRotation()
    {
        // help the character turn faster (this is in addition to root rotation in the animation)
        float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
        transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
    }

    void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            if (camera_move.buttonDown)
            {
                float h = CrossPlatformInputManager.GetAxis("Horizontal");
                float v = CrossPlatformInputManager.GetAxis("Vertical");
                
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v * m_CamForward + h * m_Cam.right;

                if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                    Move(m_Move);
            }
            else
            {
                myTransform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed, Space.Self);
                myTransform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotSpeed, 0);
            }

            if (Vector3.Distance(lastPosition, myTransform.position) > 0.05f)
            {
                TransmitPosition();
                lastPosition = myTransform.position;
            }
            if (Quaternion.Angle(lastRotation, myTransform.rotation) > 0.15f)
            {
                TransmitRotation();
                lastRotation = myTransform.rotation;
            }
            pv.text = "PV : " + m_vie + " / " + m_vieMax;
            pvBtrsf.sizeDelta = new Vector2(m_vie / m_vieMax * 300, 10);


            if (cible != null)
            {
                pv_e.text = "PV Ennemi : " + cible.Vie + "/" + cible.VieMax;
                pvBtrsf_e.sizeDelta = new Vector2(cible.Vie / cible.VieMax * 300, 10);
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {

                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.CompareTag("Ennemi"))
                    {
                        cible = hit.transform.gameObject.GetComponent<Movements>();
                        posCible = cible.transform.position;
                        pv_e.enabled = true;
                        pvBarre_e.enabled = true;
                    }
                    else
                    {
                        cible = null;
                        posCible = new Vector3(0, -100, 0);
                        pv_e.enabled = false;
                        pvBarre_e.enabled = false;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(cible != null)
                    CmdTellMyShotToTheServer(posCible, coupDeMolette);
            }
        }
        else
        {
            myTransform.position = Vector3.Lerp(myTransform.position, SyncPosition, Time.deltaTime * 15);
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, SyncRotation, Time.deltaTime * 15);
        }
    }

    [Command]
    void CmdTellMyShotToTheServer(Vector3 posTarget, float damages)
    {
        GameObject bullet = Instantiate(bulletPrefab, posTarget, Quaternion.identity) as GameObject;
        //bullet.GetComponent<Bullet>().damages = damages;
        NetworkServer.Spawn(bullet);
    }

    [Command]
    void CmdSendMyPositionToTheServer(Vector3 positionReceived)
    {
        SyncPosition = positionReceived;
    }

    [Command]
    void CmdSendMyRotationToTheServer(Quaternion rotationReceived)
    {
        SyncRotation = rotationReceived;
    }

    [Client]
    void TransmitPosition()
    {
        CmdSendMyPositionToTheServer(myTransform.position);
    }

    [Client]
    void TransmitRotation()
    {
        CmdSendMyRotationToTheServer(myTransform.rotation);
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

    #region Getter/Setter
    /*    public bool IsLoad
        {
            get { return load; }
        }

        public bool IsParalysed
        {
            get { return m_isParalyzed; }
        }
        */
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
