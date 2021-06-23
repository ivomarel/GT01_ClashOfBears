using UnityEngine;

[System.Serializable]
public struct IntVector3
{
    #region shorthands

    public static IntVector3 forward
    {
        get
        {
            return new IntVector3(0,0,1);
        }
    }


    public static IntVector3 backward
    {
        get
        {
            return new IntVector3(0,0,-1);
        }
    }

    public static IntVector3 left
    {
        get
        {
            return new IntVector3(-1, 0, 0);
        }
    }

    
    public static IntVector3 right
    {
        get
        {
            return new IntVector3(1, 0, 0);
        }
    }

    public static IntVector3 zero
    {
        get
        {
            return new IntVector3(0, 0, 0);
        }
    }

    #endregion

    
    public int sqrMagnitude
    {
        get
        {
            return x * x + y * y + z * z;
        }
    }

    public int x;
    public int y;
    public int z;
        
    public IntVector3(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static IntVector3 operator +(IntVector3 a, IntVector3 b)
        => new IntVector3(a.x + b.x, a.y + b.y, a.z + b.z);

    public static IntVector3 operator *(IntVector3 a, float b)
     => new IntVector3((int)(a.x * b), (int)(a.y * b), (int)(a.z * b));

    public static IntVector3 operator -(IntVector3 a, IntVector3 b)
        => new IntVector3(a.x - b.x, a.y - b.y, a.z - b.z);

    /// <summary>
    /// Negating a Vector is the same as negating each individual component of that Vector
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static IntVector3 operator -(IntVector3 a)
        => new IntVector3(-a.x, -a.y, -a.z);


    public static bool operator ==(IntVector3 a, IntVector3 b)
        => a.x == b.x && a.y == b.y && a.z == b.z;
    
    public static bool operator !=(IntVector3 a, IntVector3 b)
    => a.x != b.x || a.y != b.y || a.z != b.z;

    public override bool Equals(object obj)
    {
        if (!GetType().Equals(obj.GetType()))
        {
            return false;
        }
        IntVector3 a = (IntVector3)obj;
        return this.x == a.x && this.y == a.y && this.z == a.z;
    }

    //This generates a hashcode based on the x & y value
    public override int GetHashCode()
    {
        return (x << 2) ^ z;
    }

    //This should ONLY be used in Edit mode
    public static explicit operator IntVector3(Vector3 a) => new IntVector3((int)(a.x * IntMath.Float2Int), (int)(a.y * IntMath.Float2Int), (int)(a.z * IntMath.Float2Int));

    public static implicit operator Vector3(IntVector3 a) => new Vector3(a.x * IntMath.Int2Float, a.y * IntMath.Int2Float, a.z * IntMath.Int2Float);


}
