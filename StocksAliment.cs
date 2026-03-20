using System;

public class StockNourriture
{
    public float ViandeKg { get; private set; } = 0f;
    public float GrainesKg { get; private set; } = 0f;

    public const decimal PrixKgViande = 5.0m; 
    public const decimal PrixKgGraine = 2.5m; 

    public void Ajouter(TypeAliment type, float quantite)
    {
        if (type == TypeAliment.Viande) 
            ViandeKg += quantite;
        else 
            GrainesKg += quantite;
    }
    public void Consommer(TypeAliment type, float quantite)
    {
        if (type == TypeAliment.Viande) 
            ViandeKg = Math.Max(0, ViandeKg - quantite);
        else 
            GrainesKg = Math.Max(0, GrainesKg - quantite);
    }
    public void PerteViandeAvariee()
    {
        ViandeKg *= 0.80f;
    }

    public void PerteNuisibles()
    {
        GrainesKg *= 0.90f;
    }
}