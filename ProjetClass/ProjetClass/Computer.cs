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
                System.Threading.Thread.Sleep(Convert.ToInt32(15000));
                failuresystem_load = true;
            }).Start();
        }
        #endregion
    }
}
