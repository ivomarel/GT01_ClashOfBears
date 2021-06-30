using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LintMath 
{
    //These need to match!
    public const long Float2Lint = 100;
    public const float Lint2Float = 0.01f;


    public static int Factorial (int v)
    {
        if (v <= 1)
        {
            return 1;
        }
        return v * Factorial(v-1);
    }

    /// <summary>
    /// Deterministic method to calculate Sin for a certain Lint angle
    /// </summary>
    /// <param name="angle in RADIANS"></param>
    /// <returns></returns>
    public static Lint Sin (Lint angle)
    {
        //Increase number of loops for higher precision
        int nLoops = 5;

        Lint result = 0;

        for (int i = 0; i < nLoops; i++)
        {
            //flips back between 1 and -1 each loop
            int flip = (int)Mathf.Pow(-1, i);

            int k = 2 * i + 1;

            int fact = Factorial(k);

            int pow = (int)Mathf.Pow(angle, k);

            result += flip * pow / fact;
        }

        return result;
    }
}
