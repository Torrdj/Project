using System;
using System.Collections;

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

        #region Attaque TurboBoost
        public bool turboBoost_isLoad()
        {
            return turbo_isLoad;
        }

        public void turboBoost()//20 s de recharge
        {
            float oldVit = m_vitesseAtt;
            float newVit = m_vitesseAtt * 0.5f;
            turbo_isLoad = false;

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(5000);
                m_vitesseAtt = oldVit;
            }).Start();

            m_vitesseAtt = newVit;
            
            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(Convert.ToInt32(20000));
                turbo_isLoad = true;
            }).Start();
        }
        #endregion
    }
}
