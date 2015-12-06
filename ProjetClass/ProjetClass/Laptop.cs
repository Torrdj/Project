using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetClass
{
    class Laptop : Personnage
    {
        protected bool turbo_isLoad = true;

        public Laptop() { }
        public Laptop(string name) : base(name)
        {
            m_vieMax *= 0.9f; m_vie = m_vieMax;
            m_vitesseAtt *= 0.5f;
            m_attaque *= 0.9f;
        }

        public bool turboBoost_isLoad()
        {
            return turbo_isLoad;
        }

        public void turboBoost()//20 s de recharge
        {
            float oldVit = m_vitesseAtt;
            float newVit = m_vitesseAtt * 0.5f;

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(5000);
                m_vitesseAtt = oldVit;
            }).Start();

            m_vitesseAtt = newVit;
        }
    }
}
