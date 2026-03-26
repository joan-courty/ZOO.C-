using System;
using System.Linq;

public class ZooEvent
{
    private Random de = new Random();
    
    public void DeclencherEvenementAleatoire(Zoo monZoo)
    {
        bool unEvenementAEuLieu = false;

        if (de.Next(1, 101) <= 1)
        {
            if (monZoo.HabitatsZoo.Count > 0)
            {
                int indexHabitat = de.Next(monZoo.HabitatsZoo.Count);
                monZoo.HabitatsZoo.RemoveAt(indexHabitat);
                Console.WriteLine("[EVENT] Incendie ! Vous avez perdu 1 habitat.");
                unEvenementAEuLieu = true;
            }
        }

        if (de.Next(1, 101) <= 1)
        {
            if (monZoo.HabitatsZoo.Count > 0)
            {
                var habitatCible = monZoo.HabitatsZoo[de.Next(monZoo.HabitatsZoo.Count)];
                if (habitatCible.Animaux.Count > 0)
                {
                    int indexAnimal = de.Next(habitatCible.Animaux.Count);
                    var animalVole = habitatCible.Animaux[indexAnimal];
                    habitatCible.Animaux.RemoveAt(indexAnimal);
                    Console.WriteLine("[EVENT] Vol ! Un de vos animaux a disparu. C'est un/une " + animalVole.GetType().Name + " !");
                    unEvenementAEuLieu = true;
                }
            }
        }

        if (de.Next(1, 101) <= 20)
        {
            Console.WriteLine("[EVENT] Nuisibles ! Les rats ont mangé 10% de votre stock de graines.");
            monZoo.MonStock.PerteNuisibles(); 
            unEvenementAEuLieu = true;
        }

        if (de.Next(1, 101) <= 10)
        {
            Console.WriteLine("[EVENT] Viande avariée ! Vous perdez 20% de votre stock de viande.");
            monZoo.MonStock.PerteViandeAvariee();
            unEvenementAEuLieu = true;
        }

        if (!unEvenementAEuLieu)
        {
            Console.WriteLine("[EVENT] Mois calme, aucun événement exceptionnel.");
        }
    }
}