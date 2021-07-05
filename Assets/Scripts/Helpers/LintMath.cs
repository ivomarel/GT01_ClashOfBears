using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LintMath 
{
    
    //These need to match!
    public const long Float2Lint = 10000;
    public const float Lint2Float = 0.0001f;

    public const long PI = 31416; 

    public static int Factorial (int v)
    {
        if (v <= 1)
        {
            return 1;
        }
        return v * Factorial(v-1);
    }

    public static Lint Pow (Lint l, int p)
    {
        if (p == 0)
        {
            return LintMath.Float2Lint;
        } 

        if (p == 1)
        {
            return l;
        }

        return l * Pow (l, p-1);
       
    }

    /// <summary>
    /// Deterministic method to calculate Sin for a certain Lint angle
    /// </summary>
    /// <param name="angle in RADIANS"></param>
    /// <returns></returns>
    public static Lint Sin (Lint angle)
    {
        //9 % 4 = 1
        //860 % 360 = 140

        /* The modulo operator reduces value by another value until there is only a 'remainder'
         * This is essentially the same as:
        int value = 860;
        while (value > 360)
        {
            value -= 360;
        }
        */

        //Now we have an angle between -2PI and 2PI
        angle %= PI * 2;


        //Increase number of loops for higher precision
        int nLoops = 5;

        Lint result = 0;

        int flip = -1;

        for (int i = 0; i < nLoops; i++)
        {
            //flips back between 1 and -1 each loop
            flip = -flip;// (int)Mathf.Pow(-1, i);

            int k = 2 * i + 1;

            int fact = Factorial(k);

            long pow = LintMath.Pow(angle, k);

            result += flip * pow / fact;
        }

        return result;
    }
}
