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

    private Transform myTransform;
    private Vector3 lastPosition;
    private Quaternion lastRotation;

    private Transform m_Cam;
    private Vector3 m_CamForward;
    private Vector3 m_Move;
    float m_TurnAmount;
    float m_ForwardAmount;

    [SyncVar]
    private Vector3 SyncPosition;
    [SyncVar]
    private Quaternion SyncRotation;

    void Start()
    {
        FindObjectOfType<NetworkManagerHUD>().enabled = false;

        myTransform = GetComponent<Transform>();
        lastPosition = myTransform.position;
        lastRotation = myTransform.rotation;
        TransmitPosition();//Initialise la position sur le serveur
        TransmitRotation();//Idem

        m_Cam = myTransform.GetComponentInChildren<Camera>().transform;

        if (isLocalPlayer)
        {
            myTransform.FindChild("Camera").GetComponent<Camera>().enabled = true;
        }
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

                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
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
        }
        else
        {
            myTransform.position = Vector3.Lerp(myTransform.position, SyncPosition, Time.deltaTime * 15);
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, SyncRotation, Time.deltaTime * 15);
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

    #region COMMANDES
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
    #endregion
}
