using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetClass
{
    class Server : Personnage
    {
        public Server(string name) : base(name)
        {
            m_vieMax *= 1.5f;
            m_defense *= 1.2f;
            m_vitesseAtt *= 0.75f;
        }
    }
}
