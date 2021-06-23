using UnityEngine;

public struct IntVector2
{
    #region shorthands
    /// <summary>
    /// Shorthand for writing new IntVector2(0,1)
    /// </summary>
    public static IntVector2 forward
    {
        get
        {
            return new IntVector2(0, 1);
        }
    }

    /// <summary>
    /// Shorthand for writing new IntVector2(0,-1)
    /// </summary>
    public static IntVector2 backward
    {
        get
        {
            return new IntVector2(0, -1);
        }
    }

    /// <summary>
    /// Shorthand for writing new IntVector2(-1,0)
    /// </summary>
    public static IntVector2 left
    {
        get
        {
            return new IntVector2(-1, 0);
        }
    }

    /// <summary>
    /// Shorthand for writing new IntVector2(1,0)
    /// </summary>
    public static IntVector2 right
    {
        get
        {
            return new IntVector2(1, 0);
        }
    }

    /// <summary>
    /// Shorthand for writing new IntVector2(0,0)
    /// </summary>
    public static IntVector2 zero
    {
        get
        {
            return new IntVector2(0, 0);
        }
    }

    #endregion

    public int x;
    public int y;

    /// <summary>
    /// A nice helper class for grid positions and directions
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public IntVector2(int x, int y)
    {
        this.x = x;
        this.y = y;        
    }

    public static IntVector2 operator +(IntVector2 a, IntVector2 b)
        => new IntVector2(a.x + b.x, a.y + b.y);

    public static IntVector2 operator *(IntVector2 a, float b)
     => new IntVector2((int)(a.x * b), (int)(a.y * b));

    public static IntVector2 operator -(IntVector2 a, IntVector2 b)
        => new IntVector2(a.x - b.x, a.y - b.y);

    /// <summary>
    /// Negating a Vector is the same as negating each individual component of that Vector
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static IntVector2 operator -(IntVector2 a)
        => new IntVector2(-a.x, -a.y);


    public static bool operator ==(IntVector2 a, IntVector2 b)
        => a.x == b.x && a.y == b.y;
    
    public static bool operator !=(IntVector2 a, IntVector2 b)
    => a.x != b.x || a.y != b.y;

    public override bool Equals(object obj)
    {
        //If the object we're comparing with is not an IntVector2, it's definitely not the same
        if (!GetType().Equals(obj.GetType()))
        {
            return false;
        }
        IntVector2 a = (IntVector2)obj;
        return this.x == a.x && this.y == a.y;
    }

    //This generates a hashcode based on the x & y value
    public override int GetHashCode()
    {
        return (x << 2) ^ y;
    }

    public static implicit operator IntVector2(Vector3 a) => new IntVector2((int)a.x, (int)a.z);

    public static implicit operator Vector3(IntVector2 a) => new Vector3(a.x, 0, a.y);


}
