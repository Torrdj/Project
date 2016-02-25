using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetClass
{
    class Laptop : Personnage
    {
        protected bool turbo_load = true;
        protected bool spyware_load = true;

        public Laptop() { }
        public Laptop(string name) : base(name)
        {
            m_vieMax *= 0.9f; m_vie = m_vieMax;
            m_vitesseAtt *= 0.5f;
            m_attaque *= 0.9f;
        }

        #region Attaque TurboBoost
        public bool turboBoost_isLoad()
        {
            return turbo_load;
        }

        public void turboBoost()//20 s de recharge
        {
            float oldVit = m_vitesseAtt;
            float newVit = m_vitesseAtt * 0.5f;
            turbo_load = false;

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(5000);
                m_vitesseAtt = oldVit;
            }).Start();

            m_vitesseAtt = newVit;

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(20000);
                turbo_load = true;
            }).Start();
        }
        #endregion

        #region Attaque Spyware
        public bool spyware_isLoad()
        {
            return spyware_load;
        }

        public void spyware(Personnage cible)
        {
            new System.Threading.Thread(() =>
            {
                for (int i = 0; i < 30; i++)
                {
                    System.Threading.Thread.Sleep(167);//inflige 150 de dégâts sur 5s
                    attaque(cible, 150 / 30);
                }
            }).Start();

            float oldDef = cible.Defense;
            float newDef = oldDef *= 0.9f;

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(5000);
                cible.Defense = oldDef;
                spyware_load = true;
            }).Start();
            cible.Defense = newDef;
            spyware_load = false;
        }
        #endregion
    }
}
