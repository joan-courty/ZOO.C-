using System;
using System.Collections.Generic;
using System.Linq;

public class Zoo
{
    // --- TES Briques ---
    public Bank MaBanque = new Bank();
    public StockNourriture MonStock = new StockNourriture();
    public Seller MonVendeur = new Seller();
    public Billetterie MaBilletterie = new Billetterie();
    public ZooEvent MesEvenements = new ZooEvent();
    
    public List<Habitat> HabitatsZoo = new List<Habitat>();
    
    public int MoisActuel = 1;
    public int Annee = 1;

    public void LancerJeu()
    {
        bool enCours = true;
        while (enCours)
        {
            Console.WriteLine("\n==================================================");
            Console.WriteLine($"Année {Annee} - Mois {MoisActuel} | Budget : {MaBanque.Solde}€");
            Console.WriteLine($"Viande : {MonStock.ViandeKg}kg | Graines : {MonStock.GrainesKg}kg");
            Console.WriteLine("==================================================");
            Console.WriteLine("1. Etat du Zoo");
            Console.WriteLine("2. Aller au Marchand");
            Console.WriteLine("3. Passer au tour suivant");
            Console.WriteLine("4. Quitter");
            Console.Write("Choix : ");

            string choix = Console.ReadLine();
            switch (choix)
            {
                case "1": AfficherZoo(); break;
                case "2": MenuMagasin(); break;
                case "3": PasserUnMois(); break;
                case "4": enCours = false; break;
                default: Console.WriteLine("Choix invalide."); break;
            }
        }
    }

    private void MenuMagasin()
    {
        Console.WriteLine("--- MAGASIN ---");
        Console.WriteLine("1. Acheter Viande (5€/kg)");
        Console.WriteLine("2. Acheter Graines (2.5€/kg)");
        Console.WriteLine("3. Acheter un Enclos");
        Console.WriteLine("4. Acheter un Animal");
        Console.Write("Choix : ");
        string choix = Console.ReadLine();

        if (choix == "1")
        {
            Console.Write("Combien de kg de viande ? : ");
            if (float.TryParse(Console.ReadLine(), out float kg))
                MonVendeur.AcheterNourriture(TypeAliment.Viande, kg, MonStock, MaBanque);
        }
        else if (choix == "2")
        {
            Console.Write("Combien de kg de graines ? : ");
            if (float.TryParse(Console.ReadLine(), out float kg))
                MonVendeur.AcheterNourriture(TypeAliment.Graine, kg, MonStock, MaBanque);
        }
        else if (choix == "3")
        {
            Console.WriteLine("1. Enclos Tigre | 2. Volière Aigle | 3. Poulailler");
            string type = Console.ReadLine();
            if (type == "1" && MonVendeur.AcheterHabitat(TypeHabitat.EnclosTigre, MaBanque)) HabitatsZoo.Add(new Habitat(TypeHabitat.EnclosTigre));
            if (type == "2" && MonVendeur.AcheterHabitat(TypeHabitat.VoliereAigle, MaBanque)) HabitatsZoo.Add(new Habitat(TypeHabitat.VoliereAigle));
            if (type == "3" && MonVendeur.AcheterHabitat(TypeHabitat.Poulailler, MaBanque)) HabitatsZoo.Add(new Habitat(TypeHabitat.Poulailler));
        }
        else if (choix == "4")
        {
            if (HabitatsZoo.Count == 0) { Console.WriteLine("Achète un enclos d'abord !"); return; }

            Console.WriteLine("Espèce : 1. Tigre | 2. Aigle | 3. Poule | 4. Coq");
            string especeStr = Console.ReadLine();
            Console.Write("Âge en mois (ex: 6) : ");
            int.TryParse(Console.ReadLine(), out int age);

            TypeAnimal typeVendeur = TypeAnimal.Tigre;
            if (especeStr == "2") typeVendeur = TypeAnimal.Aigle;
            if (especeStr == "3") typeVendeur = TypeAnimal.Poule;
            if (especeStr == "4") typeVendeur = TypeAnimal.Coq;

            if (MonVendeur.AcheterAnimal(typeVendeur, age, MaBanque))
            {
                Sexe sexe = especeStr == "4" ? Sexe.Male : (especeStr == "3" ? Sexe.Femelle : Sexe.Male);
                if (especeStr == "1" || especeStr == "2")
                {
                    Console.Write("Sexe (1: Mâle, 2: Femelle) : ");
                    sexe = Console.ReadLine() == "2" ? Sexe.Femelle : Sexe.Male;
                }

                Animal a = null;
                if (especeStr == "1") a = new Tigre(sexe, age);
                if (especeStr == "2") a = new Aigle(sexe, age);
                if (especeStr == "3" || especeStr == "4") a = new Poule(sexe, age);

                // On cherche un enclos dispo
                bool place = false;
                foreach (var h in HabitatsZoo)
                {
                    if (h.Animaux.Count < h.CapaciteMax) 
                    { 
                        h.Animaux.Add(a); 
                        place = true; 
                        Console.WriteLine("Animal placé dans l'enclos !"); 
                        break; 
                    }
                }
                if (!place) Console.WriteLine("Pas de place dans tes enclos ! L'animal s'est enfui.");
            }
        }
    }
    private void PasserUnMois()
    {
        Console.WriteLine("Vous avez passé un mois");
        
        MesEvenements.DeclencherEvenementAleatoire(this);

        List<Animal> animauxVivants = new List<Animal>();

        foreach (var habitat in HabitatsZoo.ToList())
        {
            habitat.NettoyerMorts();
            habitat.GererReproduction(MoisActuel);
            habitat.GererSurpopulation();

            foreach (var animal in habitat.Animaux.ToList())
            {
                if (animal.EstMort) continue;

                animal.VieillirUnMois();
                if (MoisActuel == 1) animal.TesterMaladieAnnuelle();
                animal.GererMaladie();
                int joursDansUnMois = 30;
                float besoinMensuel = (float)animal.ConsommationJourKg * joursDansUnMois;

                if (animal.Alimentation == TypeAlimentation.Viande)
                {
                    if (MonStock.ViandeKg >= besoinMensuel)
                    {
                        MonStock.Consommer(TypeAliment.Viande, besoinMensuel);
                    }
                    else
                    {
                        MonStock.Consommer(TypeAliment.Viande, MonStock.ViandeKg); 
                        animal.EstMort = true;
                        Console.WriteLine($"[Famine] Un {animal.GetType().Name} est mort de faim ! (Manque de viande)");
                    }
                }
                else if (animal.Alimentation == TypeAlimentation.Graines)
                {
                    if (MonStock.GrainesKg >= besoinMensuel)
                    {
                        MonStock.Consommer(TypeAliment.Graine, besoinMensuel);
                    }
                    else
                    {
                        MonStock.Consommer(TypeAliment.Graine, MonStock.GrainesKg); 
                        animal.EstMort = true;
                        Console.WriteLine($"[Famine] Un {animal.GetType().Name} est mort de faim ! (Manque de graines)");
                    }
                }

                if (!animal.EstMort) animauxVivants.Add(animal);
            }
        }

        decimal revenus = MaBilletterie.CalculerRevenusMensuels(animauxVivants, MoisActuel);
        MaBanque.Crediter(revenus);

        MoisActuel++;
        if (MoisActuel > 12)
        {
            MoisActuel = 1;
            Annee++;
            Console.WriteLine($"Vous êtes dans une nouvelle année ({Annee}) !");
        }
    }

    private void AfficherZoo()
    {
        Console.WriteLine("\n--- ÉTAT DU ZOO ---");
        if (HabitatsZoo.Count == 0) Console.WriteLine("Zoo vide.");
        foreach (var h in HabitatsZoo)
        {
            Console.WriteLine($"- {h.Type} : {h.Animaux.Count}/{h.CapaciteMax} animaux.");
        }
    }
}