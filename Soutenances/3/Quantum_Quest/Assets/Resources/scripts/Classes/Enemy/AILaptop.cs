using UnityEngine;
using System.Collections;

public class AILaptop : AIPersonnages
{
    protected bool turbo_load = true;
    protected bool spyware_load = true;

    // Use this for initialization
    new void Start () {
        base.Start();

        type = PlayerInfo.TYPES.Laptop;

        m_name = "Laptop ennemi";
        m_vieMax *= 0.9f; m_vie = m_vieMax;
        m_vitesseAtt *= 0.5f;
        m_attaque *= 0.9f;
    }
	
	// Update is called once per frame
	new void FixedUpdate () {
        base.FixedUpdate();
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
        turbo_boost_isactiavte = true;
        yield return new WaitForSeconds(time);
        m_vitesseAtt = oldSpeed;
        turbo_boost_isactiavte = false;
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

        float damages = ((150 - oldDef) / 30) + oldDef;
        StartCoroutine(ApplyDamages(cible, damages));

        StartCoroutine(Spying(cible, newDef, oldDef));

        StartCoroutine(LoadingSpyware());

        if (GetComponent<PhotonView>().isMine)
        {
            myView.RPC("spyware", PhotonTargets.OthersBuffered, viewID);
        }
    }

    IEnumerator ApplyDamages(GameObject cible, float damages)
    {
        cible.SendMessage("Update_DoT");
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(0.334f);
            cible.SendMessage("receiveDamages", damages);
        }//30 frappes sur 10s
        cible.SendMessage("Update_DoT");
    }

    IEnumerator Spying(GameObject cible, float newDef, float oldDef)
    {
        cible.SendMessage("UpdateDef", newDef);
        yield return new WaitForSeconds(5);
        cible.SendMessage("UpdateDef", oldDef);
    }

    IEnumerator LoadingSpyware()
    {
        spyware_load = false;
        yield return new WaitForSeconds(15);
        spyware_load = true;
    }
    #endregion

}
