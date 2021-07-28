[System.Serializable]
public struct Lint
{
    public long value;

    public Lint (long v)
    {
        value = v;
    }

    //To convert a Lint into a long
    public static implicit operator long(Lint l) => l.value;

    //To convert a long into a Lint
    public static implicit operator Lint(long l) => new Lint(l);

    //We updated this operator so that 20000 * 20000 = 40000.
    //This is because '20000' is seen as 2.0000 by our system
    //By using this 'smart' system, we also avoid insanely high values in e.g. Math.Pow
    public static Lint operator *(Lint l, Lint l2) => l.value * l2.value / LintMath.Float2Lint;

    //We updated this operator so that 1000 / 500 = 200
    //This is because 10.00 / 5.00 = 2.00
    public static Lint operator /(Lint l, Lint l2) => l.value * LintMath.Float2Lint / l2.value;

    public override string ToString()
    {
        return value.ToString();
    }
}
