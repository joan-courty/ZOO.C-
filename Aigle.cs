public class Aigle : Animal
{
    public override TypeAlimentation Alimentation => TypeAlimentation.Viande;
    public override decimal ConsommationJourKg => SexeAnimal == Sexe.Male ? 0.25m : 0.3m;
    public override int JoursAvantFaimMax => 10;
    public override int AgeMaturiteMois => 48; // en mois
    public override int FinReproductionMois => 168; // en mois
    public override int EsperanceVieMois => 300;
    public override int DureeGestationJours => 45; 
    public override int MortaliteInfantilePourcentage => 50;
    public override int ProbabiliteMaladieAnnuelle => 10;
    public override int DureeMaladieBase => 30;
    public override decimal SubventionAnnuelle => 2190m;

    public Aigle(Sexe sexe, int ageMois) : base(sexe, ageMois) {}
}