using System;

public enum Sexe {
    Male, 
    Femelle 
}
public enum EtatSante { 
    BonneSante, 
    Affame, 
    Malade, 
    Mort 
}

public abstract class Animal
{
    public TypeAnimal Espece { get; protected set; }
    public Sexe SexeAnimal { get; protected set; }
    public int AgeMois { get; protected set; }
    public float JaugeFaim { get; protected set; } 
    public EtatSante Sante { get; protected set; }

    public Animal(TypeAnimal espece, Sexe sexe, int ageInitial)
    {
        Espece = espece;
        SexeAnimal = sexe;
        AgeMois = ageInitial;
        JaugeFaim = 100f; 
        Sante = EtatSante.BonneSante;
    }

    public void Grandir()
    {
        AgeMois++;
    }

    public void AvoirFaim(float quantite)
    {
        JaugeFaim -= quantite;
        if (JaugeFaim <= 0)
        {
            JaugeFaim = 0;
            Sante = EtatSante.Affame;
        }
    }

    public void Manger(float quantite)
    {
        if (Sante == EtatSante.Mort) return;

        JaugeFaim += quantite;
        if (JaugeFaim > 100f) JaugeFaim = 100f; 
        
        if (Sante == EtatSante.Affame && JaugeFaim > 20f)
        {
            Sante = EtatSante.BonneSante;
        }
    }
}