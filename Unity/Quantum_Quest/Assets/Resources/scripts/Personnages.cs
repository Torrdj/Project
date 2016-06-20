using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Personnages : MonoBehaviour {

    PhotonView myView;

    [SerializeField]
    protected float m_vie, m_vieMax,
        m_mana, m_manaMax,
        m_attaque, m_defense,
        m_vitesseAtt;

    protected string m_name;
    protected bool m_isParalyzed = false;
    protected bool load = true;
    public bool dead = false;
    

    // Use this for initialization
    void Start ()
    {
        myView = gameObject.GetComponent<PhotonView>();

        m_vieMax = 1000; m_vie = m_vieMax;
        m_manaMax = 1000; m_mana = m_manaMax;
        m_attaque = 50; m_defense = 20;
        m_vitesseAtt = 1.0f;

        if(tag == "Player")
        {
            if(myView.isMine)
            {
                tag = "Player";//Just in case...
            }
            else
            {
                tag = "Ennemis";
            }
        }
    }
	
	// Update is called once per frame
	void Update () {

        if(m_vie == 0)
        {
            dead = true;
        }
	    
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.transform.CompareTag("Ennemis"))
                {

                }
                else
                {

                }
            }
        }
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
}
