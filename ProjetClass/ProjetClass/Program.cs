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
            bool attaque1 = true;
            bool attaque2 = true;

            #region Serveur/Computer
            while (serveur.isAlive() && computer.isAlive())
            {
                if (attaque1)
                {
                    attaque1 = false;
                    new System.Threading.Thread(() =>
                    {
                        System.Threading.Thread.Sleep(Convert.ToInt32(1000f * serveur.getVitAtt()));
                        serveur.attaque(computer, serveur.getDamage());
                        attaque1 = true;
                    }).Start();
                }

                if (attaque2)
                {
                    attaque2 = false;
                    new System.Threading.Thread(() =>
                    {
                        System.Threading.Thread.Sleep(Convert.ToInt32(1000f * computer.getVitAtt()));
                        computer.attaque(serveur, computer.getDamage());
                        attaque2 = true;
                    }).Start();
                }
            }
            Console.WriteLine("serveur : " + serveur.getVie());
            Console.WriteLine("Computer : " + computer.getVie());
            serveur = new Server("Serveur");
            computer = new Computer("Computer");

            #endregion
            Console.WriteLine();

            #region Server/laptop
            while (serveur.isAlive() && laptop.isAlive())
            {
                if (attaque1)
                {
                    attaque1 = false;
                    new System.Threading.Thread(() =>
                    {
                        System.Threading.Thread.Sleep(Convert.ToInt32(1000f * serveur.getVitAtt()));
                        serveur.attaque(laptop, serveur.getDamage());
                        attaque1 = true;
                    }).Start();
                }

                if (attaque2)
                {
                    attaque2 = false;
                    new System.Threading.Thread(() =>
                    {
                        System.Threading.Thread.Sleep(Convert.ToInt32(1000f * laptop.getVitAtt()));
                        laptop.attaque(serveur, laptop.getDamage());
                        attaque2 = true;
                    }).Start();
                }
            }
            Console.WriteLine("serveur : " + serveur.getVie());
            Console.WriteLine("laptop : " + laptop.getVie());
            serveur = new Server("Serveur");
            laptop = new Laptop("laptop");
            #endregion
            Console.WriteLine();

            #region Computer/laptop
            while (laptop.isAlive() && computer.isAlive())
            {
                if (attaque1)
                {
                    attaque1 = false;
                    new System.Threading.Thread(() =>
                    {
                        System.Threading.Thread.Sleep(Convert.ToInt32(1000f * laptop.getVitAtt()));
                        laptop.attaque(computer, laptop.getDamage());
                        attaque1 = true;
                    }).Start();
                }

                if (attaque2)
                {
                    attaque2 = false;
                    new System.Threading.Thread(() =>
                    {
                        System.Threading.Thread.Sleep(Convert.ToInt32(1000f * computer.getVitAtt()));
                        computer.attaque(laptop, computer.getDamage());
                        attaque2 = true;
                    }).Start();
                }
            }
            Console.WriteLine("computer : " + computer.getVie());
            Console.WriteLine("laptop : " + laptop.getVie());
            #endregion
            Console.Read();

        }
    }
}
