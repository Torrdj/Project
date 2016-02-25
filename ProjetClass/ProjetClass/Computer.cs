using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetClass
{
    class Computer : Personnage
    {
        protected bool failuresystem_load = true;
        protected bool trojan_load = true;
        public Computer() { }
        public Computer(string name) : base(name)
        {
            m_attaque *= 1.2f;
        }
        
        #region Attaque FailureSystem
        public bool failureSytem_Isload()
        {
            return failuresystem_load;
        }

        public void failureSystem(Personnage cible)//15s de recharge
        {
            attaque(cible, 200 + m_attaque);
            failuresystem_load = false;

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(15000);
                failuresystem_load = true;
            }).Start();
        }
        #endregion

        #region Attaque Trojan
        public bool trojan_isLoad()
        {
            return trojan_load;
        }

        public void trojan(Personnage cible)
        {
            attaque(cible, 100 + m_attaque);
            trojan_load = false;

            float oldVit = cible.VitAtt;
            float newVit = oldVit * 1.5f;

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(3000);
                cible.VitAtt = oldVit;
            }).Start();
            cible.VitAtt = newVit;//On réduit la vitesse de la cible

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(10000);//10s de recharge
                trojan_load = true;
            }).Start();
            trojan_load = false;
        }
        #endregion
    }
}
