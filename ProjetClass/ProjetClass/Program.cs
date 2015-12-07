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
            Server serveur1 = new Server("Serveur1");
            Computer computer = new Computer("Computer");
            Computer computer1 = new Computer("Computer1");
            Laptop laptop = new Laptop("laptop");
            Laptop laptop1 = new Laptop("laptop1");

            #region Laptop/Latop

            while (laptop.isAlive() && laptop1.isAlive())
            {
                bool[] LaptopLoading = { laptop.turboBoost_isLoad() };
                bool[] LaptopLoading1 = { laptop1.turboBoost_isLoad() };

                if (laptop1.IsLoad() && !laptop1.IsParalysed())
                {
                    int i = 0;
                    bool hadAttaque = false;
                    while (i < LaptopLoading1.Length)
                    {
                        if (!LaptopLoading1[i])
                            i++;
                        else
                        {
                            switch (i)
                            {
                                case 0:
                                    laptop1.turboBoost();
                                    hadAttaque = true;
                                    break;
                            }
                            i++;
                        }
                    }
                    if (!hadAttaque)
                        laptop1.attaque(laptop, laptop1.coupDeMolette());
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
                        laptop.attaque(laptop1, laptop.coupDeMolette());
                }


            }


            Console.WriteLine("Laptop : " + laptop.getVie());
            Console.WriteLine("Laptop1 : " + laptop1.getVie());

            laptop = new Laptop("laptop");
            #endregion
            Console.WriteLine();

            #region Computer/Computer

            while (computer.isAlive() && computer1.isAlive())
            {
                bool[] ComputerLoading = { computer.failureSytem_Isload() };
                bool[] ComputerLoading1 = { computer.failureSytem_Isload() };

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
                                    computer.failureSystem(computer1);
                                    hadAttaque = true;
                                    break;
                            }
                            i++;
                        }
                    }
                    if (!hadAttaque)
                        computer.attaque(computer1, computer.coupDeMolette());
                }

                if (computer1.IsLoad() && !computer1.IsParalysed())
                {
                    int i = 0;
                    bool hadAttaque = false;
                    while (i < ComputerLoading1.Length)
                    {
                        if (!ComputerLoading1[i])
                            i++;
                        else
                        {
                            switch (i)
                            {
                                case 0:
                                    computer1.failureSystem(computer);
                                    hadAttaque = true;
                                    break;
                            }
                            i++;
                        }
                    }
                    if (!hadAttaque)
                        computer1.attaque(computer, computer1.coupDeMolette());
                }
            }


            Console.WriteLine("computer : " + computer.getVie());
            Console.WriteLine("Computer1 : " + computer1.getVie());

            computer = new Computer("Computer");
            #endregion
            Console.WriteLine();

            #region Serveur/Serveur

            while (serveur.isAlive() && serveur1.isAlive())
            {
                bool[] ServerLoading = { serveur.firewall_Isload(), serveur.ddos_Isload() };
                bool[] ServerLoading1 = { serveur1.firewall_Isload(), serveur1.ddos_Isload() };

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
                                    serveur.DDOS(serveur1);
                                    hadAttaque = true;
                                    break;
                            }
                            i++;
                        }
                    }
                    if (!hadAttaque)
                        serveur.attaque(serveur1, serveur.coupDeMolette());
                }


                if (serveur1.IsLoad() && !serveur1.IsParalysed())
                {
                    int i = 0;
                    bool hadAttaque = false;
                    while (i < ServerLoading1.Length)
                    {
                        if (!ServerLoading1[i])
                            i++;
                        else
                        {
                            switch (i)
                            {
                                case 0:
                                    serveur1.firewall();
                                    hadAttaque = true;
                                    break;
                                case 1:
                                    serveur1.DDOS(serveur);
                                    hadAttaque = true;
                                    break;
                            }
                            i++;
                        }
                    }
                    if (!hadAttaque)
                        serveur1.attaque(serveur, serveur1.coupDeMolette());
                }



            }

            Console.WriteLine("Seveur : " + serveur.getVie());
            Console.WriteLine("Serveur1 : " + serveur1.getVie());

            serveur = new Server("Serveur");
            #endregion
            Console.WriteLine();


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
