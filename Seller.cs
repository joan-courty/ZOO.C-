using System;

// Énumération pour que le vendeur sache de quel animal on parle
public enum TypeAnimal
{
    Tigre,
    Aigle,
    Poule,
    Coq
}

public class Seller
{
    // Acheter Nourriture
    public bool AcheterNourriture(TypeAliment type, float quantite, StockNourriture stock, ref decimal budgetDuZoo)
    {
        decimal prixTotal = 0m;
        
       
        if (type == TypeAliment.Viande)
            prixTotal = Catalog.PrixViande * (decimal)quantite; 
        else if (type == TypeAliment.Graine)
            prixTotal = Catalog.PrixGraine * (decimal)quantite; 

        if (budgetDuZoo >= prixTotal)
        {
            budgetDuZoo -= prixTotal;
            stock.Ajouter(type, quantite); 
            Console.WriteLine($"Achat réussi : {quantite}kg de {type} ! Budget restant : {budgetDuZoo}€");
            return true;
        }
        
        Console.WriteLine("Achat refusé : Pas assez d'argent pour cette nourriture !");
        return false;
    }

    // Acheter Habitat
    public bool AcheterHabitat(TypeHabitat type, ref decimal budgetDuZoo)
    {
        decimal prixAchat = 0m;

        if (type == TypeHabitat.Tigre) prixAchat = Catalog.AchatHabitatTigre;
        else if (type == TypeHabitat.Aigle) prixAchat = Catalog.AchatHabitatAigle;
        else if (type == TypeHabitat.Poule) prixAchat = Catalog.AchatHabitatPoule;

        if (budgetDuZoo >= prixAchat)
        {
            budgetDuZoo -= prixAchat;
            Console.WriteLine($"Nouvel habitat pour {type} acheté ! Budget restant : {budgetDuZoo}€");
            return true;
        }

        Console.WriteLine("Achat refusé : Pas assez d'argent pour cet habitat !");
        return false;
    }

    // Vendre Habitat
    public void VendreHabitat(TypeHabitat type, ref decimal budgetDuZoo)
    {
        decimal prixVente = 0m;

        if (type == TypeHabitat.Tigre) prixVente = Catalog.VenteHabitatTigre;
        else if (type == TypeHabitat.Aigle) prixVente = Catalog.VenteHabitatAigle;
        else if (type == TypeHabitat.Poule) prixVente = Catalog.VenteHabitatPoule;

        budgetDuZoo += prixVente;
        Console.WriteLine($"Habitat pour {type} vendu ! Vous gagnez {prixVente}€. Nouveau budget : {budgetDuZoo}€");
    }

    // Acheter Animaux
    public bool AcheterAnimal(TypeAnimal espece, int ageMois, ref decimal budgetDuZoo)
    {
        decimal prixAchat = 0m;
        if (espece == TypeAnimal.Tigre)
        {
            if (ageMois <= 6) prixAchat = Catalog.AchatTigre6Mois;
            else if (ageMois <= 48) prixAchat = Catalog.AchatTigre4Ans;
            else prixAchat = Catalog.AchatTigre14Ans;
        }
        else if (espece == TypeAnimal.Aigle)
        {
            if (ageMois <= 6) prixAchat = Catalog.AchatAigle6Mois;
            else if (ageMois <= 48) prixAchat = Catalog.AchatAigle4Ans;
            else prixAchat = Catalog.AchatAigle14Ans;
        }
        else if (espece == TypeAnimal.Poule)
        {
            prixAchat = Catalog.AchatPoule6Mois;
        }
        else if (espece == TypeAnimal.Coq)
        {
            prixAchat = Catalog.AchatCoq6Mois;
        }

        // Paiement
        if (budgetDuZoo >= prixAchat)
        {
            budgetDuZoo -= prixAchat; 
            Console.WriteLine($"Achat réussi : 1 {espece} ({ageMois} mois) acheté ! Budget restant : {budgetDuZoo}€");
            return true; 
        }

        Console.WriteLine($"Achat refusé : Pas assez d'argent pour ce {espece} !");
        return false;
    }

    // Vendre Animaux
    public void VendreAnimal(TypeAnimal espece, int ageMois, ref decimal budgetDuZoo)
    {
        decimal prixVente = 0m;

        if (espece == TypeAnimal.Tigre)
        {
            if (ageMois <= 6) prixVente = Catalog.VenteTigre6Mois;
            else if (ageMois <= 48) prixVente = Catalog.VenteTigre4Ans;
            else prixVente = Catalog.VenteTigre14Ans;
        }
        else if (espece == TypeAnimal.Aigle)
        {
            if (ageMois <= 6) prixVente = Catalog.VenteAigle6Mois;
            else if (ageMois <= 48) prixVente = Catalog.VenteAigle4Ans;
            else prixVente = Catalog.VenteAigle14Ans;
        }
        else if (espece == TypeAnimal.Poule)
        {
            prixVente = Catalog.VentePoule6Mois;
        }
        else if (espece == TypeAnimal.Coq)
        {
            prixVente = Catalog.VenteCoq6Mois;
        }

        budgetDuZoo += prixVente; 
        Console.WriteLine($"Vente réussie : 1 {espece} vendu(e) pour {prixVente}€ ! Nouveau budget : {budgetDuZoo}€");
    }
}