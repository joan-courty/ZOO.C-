using System;
using System.Collections.Generic;
using System.Linq;

public enum TypeHabitat {
    EnclosTigre, 
    VoliereAigle, 
    Poulailler 
}

public class Habitat
{
    public TypeHabitat Type { get; private set; }
    public List<Animal> Animaux { get; private set; } = new List<Animal>();
    
    public int CapaciteMax { get; private set; }
    public decimal PrixAchat { get; private set; }
    public decimal PrixVente { get; private set; }
    public int PerteSurpopulationMois { get; private set; }

    private Random de = new Random();

    public Habitat(TypeHabitat type)
    {
        Type = type;
        
        if (type == TypeHabitat.EnclosTigre)
        {
            CapaciteMax = 2;
            PrixAchat = 2000m;
            PrixVente = 500m;
            PerteSurpopulationMois = 1;
        }
        else if (type == TypeHabitat.VoliereAigle)
        {
            CapaciteMax = 4;
            PrixAchat = 2000m;
            PrixVente = 500m;
            PerteSurpopulationMois = 1;
        }
        else if (type == TypeHabitat.Poulailler)
        {
            CapaciteMax = 10;
            PrixAchat = 300m;
            PrixVente = 50m;
            PerteSurpopulationMois = 4;
        }
    }

    public void AjouterAnimal(Animal animal) { Animaux.Add(animal); }

    public void NettoyerMorts()
    {
        int morts = Animaux.RemoveAll(a => a.EstMort);
        if (morts > 0) Console.WriteLine($"{morts} animal(aux) retiré(s) de l'habitat {Type}.");
    }

    public bool ResteDeLaPlacePourBebe(int nombreBebesAttendus)
    {
        return (Animaux.Count + nombreBebesAttendus) <= CapaciteMax;
    }

    public void GererSurpopulation()
    {
        if (Animaux.Count > CapaciteMax)
        {
            if (de.Next(1, 101) <= 50) 
            {
                Console.WriteLine($"Surpopulation ({Type}) : des animaux n'ont pas survécu.");
                for (int i = 0; i < PerteSurpopulationMois; i++)
                {
                    if (Animaux.Count > 0) Animaux.RemoveAt(0);
                }
            }
        }
    }

    public void GererReproduction(int moisActuelJeu)
    {
        int taillePortee = 1;
        if (Type == TypeHabitat.EnclosTigre) taillePortee = 3;
        if (Type == TypeHabitat.VoliereAigle) taillePortee = 2;
        if (Type == TypeHabitat.Poulailler) taillePortee = 16; 

        foreach (var animal in Animaux.ToList())
        {
            if (animal.SexeAnimal == Sexe.Femelle && animal.EstEnGestation && animal.JoursGestationRestants <= 0)
            {
                animal.TerminerGestation(); 
                animal.MoisDernierePortee = 0; 

                for (int i = 0; i < taillePortee; i++)
                {
                    if (de.Next(1, 101) > animal.MortaliteInfantilePourcentage)
                    {
                        Animal bebe = CreerBebeAleatoire();
                        if (bebe != null && Animaux.Count < CapaciteMax) 
                        {
                            Animaux.Add(bebe);
                            Console.WriteLine($"Un bébé {Type} est né et a survécu !");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Un bébé {Type} n'a pas survécu à la naissance.");
                    }
                }
            }
        }

        if (Type == TypeHabitat.VoliereAigle && moisActuelJeu != 3) return; 

        if (!ResteDeLaPlacePourBebe(taillePortee)) return; 

        var males = Animaux.Where(a => a.SexeAnimal == Sexe.Male && a.PeutSeReproduire()).ToList();
        var femelles = Animaux.Where(a => a.SexeAnimal == Sexe.Femelle && a.PeutSeReproduire()).ToList();

        foreach (var femelle in femelles)
        {
            if (Type == TypeHabitat.EnclosTigre && femelle.MoisDernierePortee < 20) continue; 

            Animal maleChoisi = null;

            if (Type == TypeHabitat.VoliereAigle)
            {
                if (femelle.Partenaire != null && !femelle.Partenaire.EstMort && Animaux.Contains(femelle.Partenaire))
                {
                    maleChoisi = femelle.Partenaire;
                }
                else
                {
                    maleChoisi = males.FirstOrDefault(m => m.Partenaire == null);
                    if (maleChoisi != null)
                    {
                        femelle.Partenaire = maleChoisi;
                        maleChoisi.Partenaire = femelle;
                        Console.WriteLine("Deux aigles se sont mis en couple pour la vie !");
                    }
                }
            }
            else
            {
                maleChoisi = males.FirstOrDefault();
            }

            if (maleChoisi != null)
            {
                if (de.Next(1, 101) <= 20)
                {
                    femelle.LancerGestation();
                    Console.WriteLine($"Accouplement réussi ({Type}) !");
                    break; 
                }
            }
        }
    }

    private Animal CreerBebeAleatoire()
    {
        Sexe sexeBebe = de.Next(0, 2) == 0 ? Sexe.Male : Sexe.Femelle;
        if (Type == TypeHabitat.EnclosTigre) return new Tigre(sexeBebe, 0);
        if (Type == TypeHabitat.VoliereAigle) return new Aigle(sexeBebe, 0);
        if (Type == TypeHabitat.Poulailler) return new Poule(sexeBebe, 0);
        return null;
    }
}