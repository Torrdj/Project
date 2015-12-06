using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetClass
{
    class Personnage
    {
        protected string m_name;
        protected float m_vie, m_vieMax,
            m_mana, m_manaMax,
            m_attaque, m_defense,
            m_vitesseAtt;
        protected bool m_isParalyzed = false;

        public Personnage(){}

        public Personnage(string name)
        {
            this.m_name = name;
            m_vieMax = 1000; m_vie = m_vieMax;
            m_manaMax = 1000; m_mana = m_manaMax;
            m_attaque = 50; m_defense = 20;
            m_vitesseAtt = 1.0f;
        }

        public void attaque(Personnage cible, float damages)
        {
            cible.receiveDamages(damages);
        }

        public void receiveDamages(float damages)
        {
            if (damages > m_defense)
                m_vie -= damages - m_defense;

            if (m_vie <= 0)
                m_vie = 0;

            if (m_vie >= m_vieMax)
                m_vie = m_vieMax;
        }

        public float getDamage()
        {
            return m_attaque;
        }

        public float coupDeMolette()
        {
            return m_attaque;
        }

        public bool isAlive()
        {
            return m_vie > 0;
        }

        public float getVie()
        {
            return m_vie;
        }

        public float getMana()
        {
            return m_mana;
        }

        public float getVitAtt()
        {
            return m_vitesseAtt;
        }

        public void setVie(float vie)
        {
            m_vie = vie;
        }

        public void setMana(float mana)
        {
            m_mana = mana;
        }
    }
}
