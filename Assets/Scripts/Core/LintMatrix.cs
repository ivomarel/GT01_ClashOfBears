using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public struct LintMatrix
{
    public Lint[,] data
    {
        get
        {
            if (_data == null)
            {
                _data = new Lint[3, 3];
            }
            return _data;
        }
    }

    private Lint[,] _data;


    public Lint this[int i, int j]
    {
        get { return data[i, j]; }
        set { data[i, j] = value; }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < data.GetLength(0); i++)
        {
            for (int j = 0; j < data.GetLength(1); j++)
            {
                sb.Append(data[i, j]);
                sb.Append("\t");
            }
            sb.Append("\n");
        }
        return sb.ToString();
    }

    public static LintVector3 operator *(LintMatrix mx, LintVector3 v)
    {
        LintVector3 r = new LintVector3();
        r.x = v.x * mx[0, 0] + v.y * mx[0, 1] + v.z * mx[0, 2];
        r.y = v.x * mx[1, 0] + v.y * mx[1, 1] + v.z * mx[1, 2];
        r.z = v.x * mx[2, 0] + v.y * mx[2, 1] + v.z * mx[2, 2];
        return r;
    }

  
    public static LintMatrix CreateFromEuler (LintVector3 euler)
    {
        Lint cx = LintMath.Cos(euler.x);
        Lint cy = LintMath.Cos(euler.y);
        Lint cz = LintMath.Cos(euler.z);

        Lint sx = LintMath.Sin(euler.x);
        Lint sy = LintMath.Sin(euler.y);
        Lint sz = LintMath.Sin(euler.z);

        LintMatrix lmx = new LintMatrix();

        lmx[0, 0] = cz * cy + sz * sx * sy;
        lmx[1, 0] = sz * cx;
        lmx[2, 0] = -cz * sy + cy * sz * sx;
        lmx[0, 1] = -sz * cy + cz * sx * sy;
        lmx[1, 1] = cz * cx;
        lmx[2, 1] = sz * sy + cz * sx * cy;
        lmx[0, 2] = cx * sy;
        lmx[1, 2] = -sx;
        lmx[2, 2] = cx * cy;

        return lmx;
    }
}
