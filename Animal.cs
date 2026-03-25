using System;

public enum Sexe { Male, Femelle }
public enum TypeAlimentation { Viande, Graines }

public abstract class Animal
{
    public Sexe SexeAnimal { get; protected set; }
    public int AgeMois { get; protected set; }
    
    public int JoursSansManger { get; protected set; }
    public bool AEuFaim => JoursSansManger >= JoursAvantFaimMax; 
    
    // Santé
    public bool EstMalade { get; protected set; }
    public int JoursMaladieRestants { get; protected set; }
    public bool EstMort { get; protected set; } 
    
    public bool EstEnGestation { get; protected set; }
    public int JoursGestationRestants { get; protected set; }

    public abstract TypeAlimentation Alimentation { get; }
    public abstract decimal ConsommationJourKg { get; }
    public abstract int JoursAvantFaimMax { get; } 
    public abstract int AgeMaturiteMois { get; }
    public abstract int FinReproductionMois { get; } 
    public abstract int EsperanceVieMois { get; } 

    private Random de = new Random();

    public Animal(Sexe sexe, int ageMois)
    {
        SexeAnimal = sexe;
        AgeMois = ageMois;
        EstMort = false;
        JoursSansManger = 0;
    }

    public virtual void Manger(decimal quantiteDisponible)
    {
        decimal besoin = ConsommationJourKg;
        
        if (SexeAnimal == Sexe.Femelle && EstEnGestation)
        {
            besoin *= 2;
        }

        if (quantiteDisponible >= besoin)
        {
            JoursSansManger = 0; 
        }
        else
        {
            JoursSansManger++; 
            
            if (EstEnGestation && AEuFaim)
            {
                Console.WriteLine("Fausse couche : une femelle a eu trop faim.");
                EstEnGestation = false;
                JoursGestationRestants = 0;
            }
        }
    }

    public void GererMaladie()
    {
        if (EstMalade)
        {
            JoursMaladieRestants--;

            if (de.Next(1, 101) <= 10) 
            {
                EstMort = true;
                Console.WriteLine("Un animal n'a pas survécu à sa maladie...");
            }
            else if (JoursMaladieRestants <= 0)
            {
                EstMalade = false;
                Console.WriteLine("Un animal a guéri de sa maladie !");
            }
        }
    }

    public void VieillirUnMois()
    {
        AgeMois++;
        if (AgeMois >= EsperanceVieMois)
        {
            EstMort = true;
            Console.WriteLine("Un animal est mort de vieillesse.");
        }
    }

    public bool PeutSeReproduire()
    {
        if (AEuFaim || EstMalade || AgeMois < AgeMaturiteMois || AgeMois > FinReproductionMois || EstEnGestation)
        {
            return false;
        }
        return true;
    }
}