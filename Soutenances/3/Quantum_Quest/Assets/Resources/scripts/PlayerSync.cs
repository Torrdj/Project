using UnityEngine;
using System.Collections;

public class PlayerSync : MonoBehaviour
{
    string updatedName;
    float updatedLife, updatedLifeMax,
        updatedMana, updatedManaMax,
        updatedAttack, updatedDefense,
        updatedVitesseAtt;
    bool updatedIsParalyzed, updatedIsLoad, updatedDead;

    Vector3 correctPlayerPos;
    Quaternion correctPlayerRot;

    PhotonView myView;
    Personnages player;

    // Use this for initialization
    void Start()
    {
        myView = this.gameObject.GetComponent<PhotonView>();
        player = GetComponent<Personnages>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!myView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 10);
            transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 10);
            
            player.m_name = updatedName;
            player.Vie = updatedLife; player.VieMax = updatedLifeMax;
            player.Mana = updatedMana; player.ManaMax = updatedManaMax;
            player.Attack = updatedAttack; player.Defense = updatedDefense;
            player.VitAtt = updatedVitesseAtt;
            player.IsParalysed = updatedIsParalyzed;
            player.isLoad = updatedIsLoad;
            player.isDead = updatedDead;
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);

            stream.SendNext(player.m_name);
            stream.SendNext(player.Vie); stream.SendNext(player.VieMax);
            stream.SendNext(player.Mana); stream.SendNext(player.ManaMax);
            stream.SendNext(player.Attack); stream.SendNext(player.Defense);
            stream.SendNext(player.VitAtt);
            stream.SendNext(player.IsParalysed);
            stream.SendNext(player.isLoad);
            stream.SendNext(player.isDead);

        }
        else
        {
            this.correctPlayerPos = (Vector3)stream.ReceiveNext();
            this.correctPlayerRot = (Quaternion)stream.ReceiveNext();

            updatedName = (string)stream.ReceiveNext();
            updatedLife = (float)stream.ReceiveNext();
            updatedLifeMax = (float)stream.ReceiveNext();
            updatedMana = (float)stream.ReceiveNext();
            updatedManaMax = (float)stream.ReceiveNext();
            updatedAttack = (float)stream.ReceiveNext();
            updatedDefense = (float)stream.ReceiveNext();
            updatedVitesseAtt = (float)stream.ReceiveNext();
            updatedIsParalyzed = (bool)stream.ReceiveNext();
            updatedIsLoad = (bool)stream.ReceiveNext();
            updatedDead = (bool)stream.ReceiveNext();
        }
    }
}
