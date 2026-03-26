public class Tigre : Animal
{
    public override TypeAlimentation Alimentation => TypeAlimentation.Viande;
    public override decimal ConsommationJourKg => SexeAnimal == Sexe.Male ? 12m : 10m;
    public override int JoursAvantFaimMax => 2; 
    public override int AgeMaturiteMois => SexeAnimal == Sexe.Male ? 72 : 48; //Dans cette ligne on regroupe les femelles et les males, si c'est un male alors son age de maturité sera de 72 mois sinon 48 mois
    public override int FinReproductionMois => 168; //en mois
    public override int EsperanceVieMois => 300;
    public override int DureeGestationJours => 90; 
    public override int MortaliteInfantilePourcentage => 33;
    public override int ProbabiliteMaladieAnnuelle => 30;
    public override int DureeMaladieBase => 15;
    public override decimal SubventionAnnuelle => 43800m; 

    public Tigre(Sexe sexe, int ageMois) : base(sexe, ageMois) {}
}
