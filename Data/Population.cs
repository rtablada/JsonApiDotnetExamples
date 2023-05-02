namespace JsonApiExample.Data;

public class Population
{
    public decimal Pop { get; set; }
    public int EstimatePop { get; set; }
    public decimal Value
    {
        get
        {
            if (EstimatePop == 0)
                return Pop;
            return Pop / EstimatePop;
        }
    }

    public Population()
    {
        Pop = 0M;
        EstimatePop = 1;
    }

    public override string ToString()
    {
        if (EstimatePop > 1)
            return string.Format("{0} / {1}", EstimatePop, Pop.ToString("F"));
        return Pop.ToString("F");
    }
}
