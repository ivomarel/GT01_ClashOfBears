using UnityEngine;

[System.Serializable]
public struct LintVector3
{
    #region shorthands

    public static LintVector3 forward
    {
        get
        {
            return new LintVector3(0,0,LintMath.Float2Lint);
        }
    }


    public static LintVector3 backward
    {
        get
        {
            return new LintVector3(0,0,-LintMath.Float2Lint);
        }
    }

    public static LintVector3 left
    {
        get
        {
            return new LintVector3(-LintMath.Float2Lint, 0, 0);
        }
    }

    
    public static LintVector3 right
    {
        get
        {
            return new LintVector3(LintMath.Float2Lint, 0, 0);
        }
    }

    public static LintVector3 zero
    {
        get
        {
            return new LintVector3(0, 0, 0);
        }
    }

    #endregion

    
    public Lint sqrMagnitude
    {
        get
        {
            return x * x + y * y + z * z;
        }
    }

    public Lint x;
    public Lint y;
    public Lint z;
        
    public LintVector3(Lint x, Lint y, Lint z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static LintVector3 operator +(LintVector3 a, LintVector3 b)
        => new LintVector3(a.x + b.x, a.y + b.y, a.z + b.z);

    public static LintVector3 operator *(LintVector3 a, float b)
     => new LintVector3((int)(a.x * b), (int)(a.y * b), (int)(a.z * b));

    public static LintVector3 operator -(LintVector3 a, LintVector3 b)
        => new LintVector3(a.x - b.x, a.y - b.y, a.z - b.z);

    /// <summary>
    /// Negating a Vector is the same as negating each individual component of that Vector
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static LintVector3 operator -(LintVector3 a)
        => new LintVector3(-a.x, -a.y, -a.z);


    public static bool operator ==(LintVector3 a, LintVector3 b)
        => a.x == b.x && a.y == b.y && a.z == b.z;
    
    public static bool operator !=(LintVector3 a, LintVector3 b)
    => a.x != b.x || a.y != b.y || a.z != b.z;

    public override bool Equals(object obj)
    {
        if (!GetType().Equals(obj.GetType()))
        {
            return false;
        }
        LintVector3 a = (LintVector3)obj;
        return this.x == a.x && this.y == a.y && this.z == a.z;
    }

    //This generates a hashcode based on the x & y value
    public override int GetHashCode()
    {
        return (int)((x << 2) ^ z);
    }

    //This should ONLY be used in Edit mode
    public static explicit operator LintVector3(Vector3 a) => new LintVector3((Lint)(a.x * LintMath.Float2Lint), (Lint)(a.y * LintMath.Float2Lint), (Lint)(a.z * LintMath.Float2Lint));

    public static implicit operator Vector3(LintVector3 a) => new Vector3(a.x * LintMath.Lint2Float, a.y * LintMath.Lint2Float, a.z * LintMath.Lint2Float);


}
