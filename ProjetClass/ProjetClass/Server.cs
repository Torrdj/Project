using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetClass
{
    class Server : Personnage
    {
        protected bool ddos_load = true;
        protected bool firewall_load = true;
        public Server() { }

        public Server(string name) : base(name)
        {
            m_vieMax *= 1.5f; m_vie = m_vieMax;
            m_defense *= 1.2f;
            m_vitesseAtt *= 1.25f;
        }

        #region Attaque DDOS
        public bool ddos_Isload()
        {
            return ddos_load;
        }

        public void DDOS(Personnage cible)//20s de recharge
        {
            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(3000);
                m_isParalyzed = false;
            }).Start();

            m_isParalyzed = true;
            ddos_load = false;

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(Convert.ToInt32(20000));
                ddos_load = true;
            }).Start();
        }
        #endregion

        #region Attaque Firewall
        public bool firewall_Isload()
        {
            return firewall_load;
        }

        public void firewall()//20s de recharge
        {
            float oldDef = m_defense;
            float newDef = m_defense * 2;
            
            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(5000);
                m_defense = oldDef;
            }).Start();

            m_defense = newDef;
            firewall_load = false;
            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(Convert.ToInt32(20000));
                firewall_load = true;
            }).Start();
        }
        #endregion
    }
}
