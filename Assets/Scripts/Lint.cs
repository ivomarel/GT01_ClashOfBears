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
}
