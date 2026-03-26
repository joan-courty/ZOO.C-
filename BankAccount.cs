using System;

public class Bank
{
    public decimal Solde { get; private set; }

    public Bank()
    {
        Solde = 80000m; 
    }

    public void Crediter(decimal montant)
    {
        Solde += montant;
    }

    public bool Debiter(decimal montant)
    {
        if (Solde >= montant)
        {
            Solde -= montant; 
            return true;      
        }
        
        decimal manque = montant - Solde;
        Console.WriteLine($"[Banque] Paiement refusé ! Fonds insuffisants. Il vous manque {manque}€.");
        
        return false; 
    }
}