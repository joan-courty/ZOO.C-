public class Poule : Animal
{
    public override TypeAlimentation Alimentation => TypeAlimentation.Graines;
    public override decimal ConsommationJourKg => SexeAnimal == Sexe.Male ? 0.18m : 0.15m;
    public override int JoursAvantFaimMax => SexeAnimal == Sexe.Male ? 2 : 1;
    public override int AgeMaturiteMois => 6; //en mois
    public override int FinReproductionMois => 96;
    public override int EsperanceVieMois => 180;
    public override int DureeGestationJours => 42; 
    public override int MortaliteInfantilePourcentage => 50;
    public override int ProbabiliteMaladieAnnuelle => 5;
    public override int DureeMaladieBase => 5;
    public override decimal SubventionAnnuelle => 0m;

    public Poule(Sexe sexe, int ageMois) : base(sexe, ageMois) {}
}

