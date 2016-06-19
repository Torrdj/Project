using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class Movements : MonoBehaviour
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
    PhotonView myView;

    private Transform m_Cam;
    private Vector3 m_CamForward;
    private Vector3 m_Move;
    float m_TurnAmount;
    float m_ForwardAmount;

    void Start()
    {
        myTransform = this.gameObject.GetComponent<Transform>();
        myView = this.gameObject.GetComponent<PhotonView>();

        m_Cam = myTransform.GetComponentInChildren<Camera>().transform;
        
        if (myView.isMine)
        {
            myTransform.FindChild("Camera").GetComponent<Camera>().enabled = true;
            myTransform.FindChild("Camera").GetComponent<AudioListener>().enabled = true;
        }
    }

    void FixedUpdate()
    {
        if (myView.isMine /*&& !gameObject.GetComponent<Personnage>().dead*/)
        {
            if (Camera_move.buttonDown)
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
}
