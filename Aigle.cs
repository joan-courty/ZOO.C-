public class Aigle : Animal
{
    public override TypeAlimentation Alimentation => TypeAlimentation.Viande;
    public override decimal ConsommationJourKg => SexeAnimal == Sexe.Male ? 0.25m : 0.3m;
    public override int JoursAvantFaimMax => 10;
    public override int AgeMaturiteMois => 48; // en mois
    public override int FinReproductionMois => 168; // en mois
    public override int EsperanceVieMois => 300; // en mois

    public Aigle(Sexe sexe, int ageMois) : base(sexe, ageMois) {}
}