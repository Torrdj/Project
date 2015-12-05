using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetClass
{
    class Program
    {
        static void Main(string[] args)
        {
            Server serveur = new Server("Serveur");
            Computer computer = new Computer("Computer");
            Laptop laptop = new Laptop("laptop");

            while(serveur.isAlive() && computer.isAlive())
            {
                //attaque du serveur
                new System.Threading.Thread(() =>
                {
                    System.Threading.Thread.Sleep(1000);
                    
                }).Start();





            }
            
            
        }
    }
}
