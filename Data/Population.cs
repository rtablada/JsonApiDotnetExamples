namespace JsonApiExample.Data;

public class Statistics
{
    public decimal Population { get; set; }
    public long Gdp { get; set; }
    public decimal PerCapita
    {
        get
        {
            if (Gdp == 0)
                return Population;
            return Population / Gdp;
        }
    }

    public Statistics()
    {
        Population = 0M;
        Gdp = 1;
    }

    public override string ToString()
    {
        if (Gdp > 1)
            return string.Format("{0} / {1}", Gdp, Population.ToString("F"));
        return Population.ToString("F");
    }
}
