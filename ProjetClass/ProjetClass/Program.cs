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

            #region Serveur/Computer

            while (serveur.isAlive() && computer.isAlive())
            {
                bool[] ServerLoading = { serveur.firewall_Isload(), serveur.ddos_Isload() };
                bool[] ComputerLoading = { computer.failureSytem_Isload() };

                if (serveur.IsLoad() && !serveur.IsParalysed())
                {
                    int i = 0;
                    bool hadAttaque = false;
                    while (i < ServerLoading.Length)
                    {
                        if (!ServerLoading[i])
                            i++;
                        else
                        {
                            switch (i)
                            {
                                case 0:
                                    serveur.firewall();
                                    hadAttaque = true;
                                    break;
                                case 1:
                                    serveur.DDOS(computer);
                                    hadAttaque = true;
                                    break;
                            }
                            i++;
                        }
                    }
                    if (!hadAttaque)
                        serveur.attaque(computer, serveur.coupDeMolette());
                }

                if (computer.IsLoad() && !computer.IsParalysed())
                {
                    int i = 0;
                    bool hadAttaque = false;
                    while (i < ComputerLoading.Length)
                    {
                        if (!ComputerLoading[i])
                            i++;
                        else
                        {
                            switch (i)
                            {
                                case 0:
                                    computer.failureSystem(serveur);
                                    hadAttaque = true;
                                    break;
                            }
                            i++;
                        }
                    }
                    if (!hadAttaque)
                        computer.attaque(serveur, computer.coupDeMolette());
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
                bool[] ServerLoading = { serveur.firewall_Isload(), serveur.ddos_Isload() };
                bool[] LaptopLoading = { laptop.turboBoost_isLoad() };

                if (serveur.IsLoad() && !serveur.IsParalysed())
                {
                    int i = 0;
                    bool hadAttaque = false;
                    while (i < ServerLoading.Length)
                    {
                        if (!ServerLoading[i])
                            i++;
                        else
                        {
                            switch (i)
                            {
                                case 0:
                                    serveur.firewall();
                                    hadAttaque = true;
                                    break;
                                case 1:
                                    serveur.DDOS(laptop);
                                    hadAttaque = true;
                                    break;
                            }
                            i++;
                        }
                    }
                    if (!hadAttaque)
                        serveur.attaque(laptop, serveur.coupDeMolette());
                }

                if (laptop.IsLoad() && !laptop.IsParalysed())
                {
                    int i = 0;
                    bool hadAttaque = false;
                    while (i < LaptopLoading.Length)
                    {
                        if (!LaptopLoading[i])
                            i++;
                        else
                        {
                            switch (i)
                            {
                                case 0:
                                    laptop.turboBoost();
                                    hadAttaque = true;
                                    break;
                            }
                            i++;
                        }
                    }
                    if (!hadAttaque)
                        laptop.attaque(serveur, laptop.coupDeMolette());
                }
            }

            Console.WriteLine("serveur : " + serveur.getVie());
            Console.WriteLine("Laptop : " + laptop.getVie());
            serveur = new Server("Serveur");
            laptop = new Laptop("Laptop");

            #endregion
            Console.WriteLine();

            #region Computer/laptop
            while (laptop.isAlive() && computer.isAlive())
            {
                bool[] LaptopLoading = { laptop.turboBoost_isLoad() };
                bool[] ComputerLoading = { computer.failureSytem_Isload() };

                if (laptop.IsLoad() && !laptop.IsParalysed())
                {
                    int i = 0;
                    bool hadAttaque = false;
                    while (i < LaptopLoading.Length)
                    {
                        if (!LaptopLoading[i])
                            i++;
                        else
                        {
                            switch (i)
                            {
                                case 0:
                                    laptop.turboBoost();
                                    hadAttaque = true;
                                    break;
                            }
                            i++;
                        }
                    }
                    if (!hadAttaque)
                        laptop.attaque(computer, laptop.coupDeMolette());
                }

                if (computer.IsLoad() && !computer.IsParalysed())
                {
                    int i = 0;
                    bool hadAttaque = false;
                    while (i < ComputerLoading.Length)
                    {
                        if (!ComputerLoading[i])
                            i++;
                        else
                        {
                            switch (i)
                            {
                                case 0:
                                    computer.failureSystem(laptop);
                                    hadAttaque = true;
                                    break;
                            }
                            i++;
                        }
                    }
                    if (!hadAttaque)
                        computer.attaque(laptop, computer.coupDeMolette());
                }
            }

            Console.WriteLine("Laptop : " + laptop.getVie());
            Console.WriteLine("Computer : " + computer.getVie());
            laptop = new Laptop("Laptop");
            computer = new Computer("Computer");
            #endregion
            Console.Read();

        }
    }
}
