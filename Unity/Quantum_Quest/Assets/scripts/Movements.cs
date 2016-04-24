using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Movements : NetworkBehaviour
{

    [SerializeField]
    private float speed = 7.0f;
    [SerializeField]
    private float rotSpeed = 250.0f;

    //private Renderer myRenderer;
    private Transform myTransform;
    private Vector3 lastPosition;
    private Quaternion lastRotation;

    //private Camera myCam;

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

        //myCam = myTransform.GetComponentInChildren<Camera>();

        if (isLocalPlayer)
        {
            myTransform.FindChild("Camera").GetComponent<Camera>().enabled = true;
        }
    }

    void FixedUpdate()
    {
        if (isLocalPlayer)
        {/*
            if (camera_move.buttonDown)
            {
                Vector3 m_CamForward = Vector3.Scale(myCam.transform.forward, new Vector3(1, 0, 1)).normalized;
                myTransform.Translate((-Input.GetAxis("Horizontal") * m_CamForward
                + Input.GetAxis("Vertical") * myCam.transform.right)
                * Time.deltaTime * speed, Space.Self);
                myTransform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotSpeed, 0);
            }
            else
            {*/
                myTransform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed, Space.Self);
                myTransform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotSpeed, 0);
            //}

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
}
