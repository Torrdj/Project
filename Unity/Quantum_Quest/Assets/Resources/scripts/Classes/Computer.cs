using UnityEngine;
using System.Collections;

public class Computer : Personnages
{
    protected bool failuresystem_load = true;
    protected bool trojan_load = true;

    new void Start()
    {
        types = PlayerInfo.TYPES.Computer;
        base.Start();

        if (tag == "Player")
        {
            if (myView.isMine)
            {
                m_attaque *= 1.2f;

                info.Vie = m_vie; info.VieMax = m_vieMax;
            }
        }
    }

    // Update is called once per frame
    new void FixedUpdate()
    {
        base.FixedUpdate();

        if (!isDead && !IsParalysed)
        {
            if (isLoad)
            {
                if(failuresystem_load && cible != -1 && Input.GetKey(KeyCode.Alpha2))
                {
                    failureSystem(cible);
                    StartCoroutine(Loading());
                }
                else if (trojan_load && cible != -1 && Input.GetKey(KeyCode.Alpha3))
                {
                    trojan(cible);
                    StartCoroutine(Loading());
                }
            }
        }
    }

    #region FailureSystem
    [PunRPC]
    void failureSystem(int viewID)
    {
        GameObject cible = PhotonView.Find(viewID).gameObject;

        cible.SendMessage("receiveDamages", 200 + m_attaque);

        StartCoroutine(LoadingFailure());

        if (GetComponent<PhotonView>().isMine)
        {
            myView.RPC("failureSystem", PhotonTargets.OthersBuffered, viewID);
        }
    }

    IEnumerator LoadingFailure()
    {
        failuresystem_load = false;
        yield return new WaitForSeconds(15);
        failuresystem_load = true;
    }
    #endregion

    #region Trojan
    [PunRPC]
    void trojan(int viewID)
    {
        GameObject cible = PhotonView.Find(viewID).gameObject;

        cible.SendMessage("receiveDamages", 100 + m_attaque);

        float oldVit = cible.GetComponent<Personnages>().VitAtt;
        float newVit = oldVit * 1.5f;

        StartCoroutine(Infect(cible, newVit, oldVit));

        StartCoroutine(LoadingTrojan());

        if (GetComponent<PhotonView>().isMine)
        {
            myView.RPC("trojan", PhotonTargets.OthersBuffered, viewID);
        }
    }

    IEnumerator Infect(GameObject cible, float newVit, float oldVit)
    {
        cible.SendMessage("UpdateVit", newVit);
        yield return new WaitForSeconds(3);
        cible.SendMessage("UpdateVIt", oldVit);
    }

    IEnumerator LoadingTrojan()
    {
        trojan_load = false;
        yield return new WaitForSeconds(10);
        trojan_load = true;
    }
    #endregion
}
