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
            m_vitesseAtt *= 0.75f;
        }

        public void DDOS(Personnage cible)
        {

        }

        public void firewall()
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
