using System;

public enum Sexe { Male, Femelle }
public enum TypeAlimentation { Viande, Graines }

public abstract class Animal
{
    public Sexe SexeAnimal { get; protected set; }
    public int AgeMois { get; protected set; }
    public int MoisDansZoo { get; set; }
    public int MoisDernierePortee { get; set; } = 99;
    
    // Faim
    public int JoursSansManger { get; protected set; }
    public bool AEuFaim => JoursSansManger >= JoursAvantFaimMax; 
    
    // Maladie
    public bool EstMalade { get; protected set; }
    public int JoursMaladieRestants { get; protected set; }
    public bool EstMort { get; set; } 
    
    // Gestation
    public bool EstEnGestation { get; protected set; }
    public int JoursGestationRestants { get; protected set; }

    public Animal Partenaire { get; set; }
    public abstract TypeAlimentation Alimentation { get; }
    public abstract decimal ConsommationJourKg { get; }
    public abstract int JoursAvantFaimMax { get; } 
    public abstract int AgeMaturiteMois { get; }
    public abstract int FinReproductionMois { get; } 
    public abstract int EsperanceVieMois { get; } 
    public abstract int DureeGestationJours { get; } 
    public abstract int MortaliteInfantilePourcentage { get; }
    public abstract int ProbabiliteMaladieAnnuelle { get; }
    public abstract int DureeMaladieBase { get; }
    public abstract decimal SubventionAnnuelle { get; }

    protected Random de = new Random();
    
    public Animal(Sexe sexe, int ageMois)
    {
        SexeAnimal = sexe;
        AgeMois = ageMois;
        EstMort = false;
        JoursSansManger = 0;
        EstEnGestation = false;
        MoisDansZoo = 0;
    }

    public virtual void Manger(decimal quantiteDisponible)
    {
        decimal besoin = ConsommationJourKg;
        if (SexeAnimal == Sexe.Femelle && EstEnGestation) besoin *= 2;

        if (quantiteDisponible >= besoin)
        {
            JoursSansManger = 0; 
        }
        else
        {
            JoursSansManger++; 
            if (EstEnGestation && AEuFaim)
            {
                Console.WriteLine("Un animal à fait une fausse couche : une femelle a eu faim.");
                TerminerGestation();
            }
        }
    }

    public void LancerGestation()
    {
        EstEnGestation = true;
        JoursGestationRestants = DureeGestationJours;
    }

    public void TerminerGestation()
    {
        EstEnGestation = false;
        JoursGestationRestants = 0;
    }

    public void AvancerGestationUnJour()
    {
        if (EstEnGestation) JoursGestationRestants--;
    }

    public void TesterMaladieAnnuelle()
    {
        if (!EstMalade && de.Next(1, 101) <= ProbabiliteMaladieAnnuelle)
        {
            EstMalade = true;
            int variance = (int)(DureeMaladieBase * 0.20); 
            JoursMaladieRestants = DureeMaladieBase + de.Next(-variance, variance + 1);
            Console.WriteLine($"Un animal est tombé malade pour {JoursMaladieRestants} jours !");
        }
    }

    public void GererMaladie()
    {
        if (EstMalade)
        {
            JoursMaladieRestants--;
            if (de.Next(1, 101) <= 10) // Chaque jour l'animal à 10% de mourir pour cause de maladie
            {
                EstMort = true;
                Console.WriteLine("Un animal n'a pas survécu à sa maladie.");
            }
            else if (JoursMaladieRestants <= 0)
            {
                EstMalade = false;
            }
        }
    }

    public void VieillirUnMois()
    {
        AgeMois++;
        MoisDansZoo++;
        MoisDernierePortee++;
        if (AgeMois >= EsperanceVieMois) EstMort = true;
    }

    public bool PeutSeReproduire()
    {
        if (AEuFaim || EstMalade || AgeMois < AgeMaturiteMois || AgeMois >= FinReproductionMois || EstEnGestation || MoisDansZoo == 0)
            return false;
        
        return true;
    }
}