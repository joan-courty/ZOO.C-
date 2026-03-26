using System;
using System.Collections.Generic;
using System.Linq;

public class Billetterie
{
    public decimal PrixAdulte { get; private set; } = 17m;
    public decimal PrixEnfant { get; private set; } = 13m; 

    private Random de = new Random();

    public decimal CalculerRevenusMensuels(List<Animal> animaux, int moisActuel)
    {
        if (animaux.Count == 0) return 0m;

        bool estSaisonHaute = moisActuel >= 5 && moisActuel <= 9;
        decimal visiteursBaseTotal = 0m;

        foreach (var animal in animaux)
        {
            if (animal.SexeAnimal == Sexe.Femelle && animal.EstEnGestation)
            {
                continue;
            }

            if (animal is Tigre)
            {
                visiteursBaseTotal += estSaisonHaute ? 30m : 5m;
            }
            else if (animal is Aigle)
            {
                visiteursBaseTotal += estSaisonHaute ? 15m : 7m;
            }
            else if (animal is Poule)
            {
                visiteursBaseTotal += estSaisonHaute ? 2m : 0.5m;
            }
        }

        int visiteursBase = (int)Math.Round(visiteursBaseTotal);

        int variance = (int)(visiteursBase * 0.20);
        int visiteursReels = visiteursBase + de.Next(-variance, variance + 1);

        if (visiteursReels <= 0) return 0m;

        int nombreAdultes = visiteursReels / 2;
        int nombreEnfants = visiteursReels - nombreAdultes; 

        decimal gainsAdultes = nombreAdultes * PrixAdulte;
        decimal gainsEnfants = nombreEnfants * PrixEnfant;
        decimal totalGains = gainsAdultes + gainsEnfants;

        if (estSaisonHaute) Console.WriteLine("Haute saison en cours !");
        Console.WriteLine($"- Visiteurs : {visiteursReels} ({nombreAdultes} adultes, {nombreEnfants} enfants)");
        Console.WriteLine($"- Revenus : {totalGains} €");

        return totalGains;
    }
}