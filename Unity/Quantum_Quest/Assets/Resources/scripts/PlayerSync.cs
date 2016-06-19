using UnityEngine;
using System.Collections;

public class PlayerSync : MonoBehaviour {

    Vector3 correctPlayerPos;
    Quaternion correctPlayerRot;
    PhotonView myView;

	// Use this for initialization
	void Start () {
        myView = this.gameObject.GetComponent<PhotonView>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(!myView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 10);
            transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 10);
        }
	}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            this.correctPlayerPos = (Vector3)stream.ReceiveNext();
            this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
