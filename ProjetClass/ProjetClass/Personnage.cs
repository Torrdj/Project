using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetClass
{
    class Personnage
    {
        protected bool load = true;
        protected string m_name;
        protected float m_vie, m_vieMax,
            m_mana, m_manaMax,
            m_attaque, m_defense,
            m_vitesseAtt;
        protected bool m_isParalyzed = false;

        public Personnage() { }

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
            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(Convert.ToInt32(1000 * m_vitesseAtt));
                load = true;
            }).Start();
            load = false;
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
        public bool isAlive()
        {
            return m_vie > 0;
        }

        #region Getter/Setter
        public bool IsLoad
        {
            get { return load; }
        }

        public bool IsParalysed
        {
            get { return m_isParalyzed; }
        }

        public float coupDeMolette//attaque de base ET getter
        {
           get { return m_attaque; }
        }

        public float Defense
        {
            get { return m_defense; }
            set { m_defense = value; }
        }


        public float Vie
        {
            get { return m_vie; }
            set { m_vie = value; }
        }

        public float Mana
        {
            get { return m_mana; }
            set { m_mana = value; }
        }

        public float VitAtt
        {
            get { return m_vitesseAtt; }
            set { m_vitesseAtt = value; }
        }
        #endregion
        
    }
}
