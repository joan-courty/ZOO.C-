using System;
using System.Collections.Generic;
using System.Linq;

public enum TypeHabitat
{
    Tigre,
    Aigle,
    Poule // Le coq est dans l'enclos de la poule en toute logique
}

public class Habitat
{
    public TypeHabitat Type { get; private set; }
    public int CapaciteMax { get; private set; }
    
    public List<Animal> Pensionnaires { get; private set; }

    public Habitat(TypeHabitat type, int capaciteMax)
    {
        Type = type;
        CapaciteMax = capaciteMax;
        Pensionnaires = new List<Animal>();
    }

    public bool AjouterAnimal(Animal nouvelAnimal)
    {
        if (Pensionnaires.Count >= CapaciteMax)
        {
            Console.WriteLine($"[Habitat] Refusé : L'enclos des {Type} est plein à craquer !");
            return false;
        }

        Pensionnaires.Add(nouvelAnimal);
        return true;
    }

    public void RetirerAnimal(Animal animal)
    {
        Pensionnaires.Remove(animal);
    }

    public Animal GererReproduction()
    {
        if (Pensionnaires.Count >= CapaciteMax) return null;
        bool aMaleAdulte = Pensionnaires.Any(a => a.SexeAnimal == Sexe.Male && a.AgeMois >= 12);
        bool aFemelleAdulte = Pensionnaires.Any(a => a.SexeAnimal == Sexe.Femelle && a.AgeMois >= 12);

        if (aMaleAdulte && aFemelleAdulte)
        {
            // 20% de chance d'avoir un bébé
            Random de = new Random();
            if (de.Next(1, 101) <= 20)
            {
                Console.WriteLine($"Un bébé {Type} vient de naître !");

                return null; // TODO
            }
        }
        
        return null;
    }
}