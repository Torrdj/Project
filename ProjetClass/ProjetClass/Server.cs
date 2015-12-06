using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetClass
{
    class Server : Personnage
    {
        public Server(){}

        public Server(string name) : base(name)
        {
            m_vieMax *= 1.5f; m_vie = m_vieMax;
            m_defense *= 1.2f;
            m_vitesseAtt *= 1.25f;
        }

        public void DDOS(Personnage cible)//20s de recharge
        {
            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(3000);
                m_isParalyzed = false;
            }).Start();
            m_isParalyzed = true;
        }

        public void firewall()//20s de recharge
        {
            float oldDef = m_defense;
            float newDef = m_defense * 2;

            //"tâche de fond" pour pas bloquer le programme
            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(5000);
                m_defense = oldDef;//On retrouve une def normale à la fin du buff
            }).Start();

            m_defense = newDef;
        }
    }
}
