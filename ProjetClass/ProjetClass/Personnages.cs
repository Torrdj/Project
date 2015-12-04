using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetClass
{
    class Personnages
    {
        protected string m_name;
        protected int m_vie, m_vieMax,
            m_mana, m_manaMax,
            m_puissance, m_puissMagic,
            m_defense, m_defMagic;
		//c'est tropo mrant

        public Personnages(string name)
        {
            this.m_name = name;
            m_vieMax = 1000; m_vie = m_vieMax;
            m_manaMax = 1000; m_mana = m_manaMax;
            m_puissance = 50; m_puissMagic = 50;
            m_defense = 50; m_defMagic = 50;
        }

        public void attaque(Personnages cible)
        {
            cible.m_vie -= cible.m_puissance;
        }
    }
}
