using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetClass
{
    class Computer : Personnage
    {
        public Computer(string name) : base(name)
        {
            m_attaque *= 1.2f;
        }
    }
}
