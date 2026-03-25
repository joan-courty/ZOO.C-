using System;

public class ZooEvent
{
    private Random de = new Random();
    
    public void DeclencherEvenementAleatoire(Zoo monZoo)
    {
        bool unEvenementAEuLieu = false;

        if (de.Next(1, 101) <= 1)
        {
            Console.WriteLine("[EVENT] Incendie ! Vous avez perdu 1 habitat.");
            // TODO : monZoo.RetirerHabitat();
            unEvenementAEuLieu = true;
        }

        if (de.Next(1, 101) <= 1)
        {
            Console.WriteLine("[EVENT] Vol ! Un de vos animaux a disparu.");
            // TODO : monZoo.RetirerAnimal();
            unEvenementAEuLieu = true;
        }

        if (de.Next(1, 101) <= 20)
        {
            Console.WriteLine("[EVENT] Nuisibles ! Les rats ont mangé 10% de votre stock de graines.");
            // TODO : monZoo.StockGraines = monZoo.StockGraines * 0.9m;
            unEvenementAEuLieu = true;
        }

        if (de.Next(1, 101) <= 10)
        {
            Console.WriteLine("[EVENT] Viande avariée ! Vous perdez 20% de votre stock de viande.");
            // TODO : monZoo.StockViande = monZoo.StockViande * 0.8m;
            unEvenementAEuLieu = true;
        }

        if (!unEvenementAEuLieu)
        {
            Console.WriteLine("[EVENT] Mois calme, aucun événement exceptionnel.");
        }
    }
}