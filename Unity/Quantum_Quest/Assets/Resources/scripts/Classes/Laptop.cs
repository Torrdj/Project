using UnityEngine;
using System.Collections;

public class Laptop : Personnages
{
    public bool turbo_load = true;
    public bool spyware_load = true;

    new void Start()
    {
        base.Start();

        if (tag == "Player")
        {
            if (myView.isMine)
            {
                m_vieMax *= 0.9f; m_vie = m_vieMax;
                m_vitesseAtt *= 0.5f;
                m_attaque *= 0.9f;

                info.Vie = m_vie; info.VieMax = m_vieMax;
            }
        }
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();

        if (!isDead && cible != -1)
        {
            if (isLoad)
            {
                if (turbo_load && Input.GetKey(KeyCode.Alpha2))
                {
                    Debug.Log("2");
                    turboBoostRPC(cible);
                    StartCoroutine(Loading());
                }
                else if (spyware_load && Input.GetKey(KeyCode.Alpha3))
                {
                    Debug.Log("3");
                    spyware(cible);
                    StartCoroutine(Loading());
                }
            }
        }
    }

    #region TurboBoost
    [PunRPC]
    public void turboBoostRPC(int viewID)
    {
        SendMessage("turboBoost");

        if (GetComponent<PhotonView>().isMine)
        {
            myView.RPC("turboBoostRPC", PhotonTargets.OthersBuffered, viewID);
        }
    }

    void turboBoost()
    {
        float oldVit = m_vitesseAtt;
        float newVit = m_vitesseAtt * 0.5f;
        turbo_load = false;

        StartCoroutine(UpdateSpeed(newVit, oldVit, 5));

        StartCoroutine(LoadingTurbo(20));
    }

    IEnumerator UpdateSpeed(float newSpeed, float oldSpeed, float time)
    {
        m_vitesseAtt = newSpeed;
        yield return new WaitForSeconds(time);
        m_vitesseAtt = oldSpeed;
    }

    IEnumerator LoadingTurbo(float time)
    {
        turbo_load = false;
        yield return new WaitForSeconds(time);
        turbo_load = true;
    }
    #endregion

    #region Spyware
    [PunRPC]
    public void spyware(int viewID)
    {
        GameObject cible = PhotonView.Find(viewID).gameObject;

        float oldDef = cible.GetComponent<Personnages>().Defense;
        float newDef = oldDef * 0.9f;

        float damages = (150 / 30) + oldDef;
        StartCoroutine(ApplyDamages(cible, damages));
        
        StartCoroutine(Spying(cible, newDef, oldDef, 5));

        if (GetComponent<PhotonView>().isMine)
        {
            myView.RPC("spyware", PhotonTargets.OthersBuffered, viewID);
        }
    }

    IEnumerator ApplyDamages(GameObject cible, float damages)
    {
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(0.167f);
            cible.SendMessage("receiveDamages", damages);
        }
    }

    IEnumerator Spying(GameObject cible, float newDef, float oldDef, float time)
    {
        cible.SendMessage("UpdateDef", newDef);
        spyware_load = false;
        yield return new WaitForSeconds(time);
        cible.SendMessage("UpdateDef", oldDef);
        spyware_load = true;
    }
    #endregion

}
