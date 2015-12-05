using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetClass
{
    class Laptop : Personnage
    {
        public Laptop() { }
        public Laptop(string name) : base(name)
        {
            m_vieMax *= 0.9f; m_vie = m_vieMax;
            m_vitesseAtt *= 1.3f;
            m_attaque *= 0.9f;
        }

        public void turboBoost()
        {
            float oldVit = m_vitesseAtt;
            float newVit = m_vitesseAtt * 2;

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(5000);
                m_vitesseAtt = oldVit;
            }).Start();

            m_vitesseAtt = newVit;
        }
    }
}
