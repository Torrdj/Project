using System;

class Personnage
{
	/*
        Vie Max = 1000
        Mana Max = 1000
        Attaque = 50
        Vitesse d'Attaque = 1000
        Defence = 20
    */
}
class Server /// : Personnage
{
    /*
        Vie max *= 1.5 -> 1500
        Defence *= 1.2 -> 24
        Vitesse d'Attaque *= 1.25 -> 1250

        Attaque :
            - DDOS
            - Firewall
    */
}
class Computer /// : Personnage
{
    /*
        Attaque *= 1,2 -> 60

        Attaque : 
            - FailureSystem
    */
}
class Laptop /// : Personnage
{
    /*
        Vie Max *= 0,9 -> 900
        Vitesse d'Attaque *= 0,5 -> 500
        Attaque *= 0,9 -> 45

        Attaque : 
            - Turbo Boost
    */
}
