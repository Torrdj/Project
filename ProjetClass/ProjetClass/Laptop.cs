using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetClass
{
    class Laptop : Personnage
    {
        public Laptop(string name) : base(name)
        {
            m_vieMax *= 0.9f;
            m_vitesseAtt *= 1.3f;
            m_attaque *= 0.9f;
        }
    }
}
