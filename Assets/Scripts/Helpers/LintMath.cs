using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LintMath 
{
    
    //These need to match!
    public const long Float2Lint = 10000;
    public const float Lint2Float = 0.0001f;

    public const long HALF_PI = 15708; 
    public const long PI = 31416;
    public const long PI_2 = 62832;

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

    public static Lint Abs (Lint l)
    {
        if (l < 0) return -l;
        return l;
    }

    /// <summary>
    /// Deterministic method to calculate Sin for a certain Lint angle
    /// </summary>
    /// <param name="angle in RADIANS"></param>
    /// <returns></returns>
    public static Lint Sin (Lint angle)
    {
        angle = ClampAngle(angle);        

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

    /// <summary>
    /// Clamps any radian angle between -PI and PI (while retaining the actual angle)
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    private static Lint ClampAngle (Lint angle)
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

        //Now we have an angle between -PI and PI
        if (angle > PI) angle -= PI_2;
        if (angle <= -PI) angle += PI_2;

        return angle;
    }

    public static Lint Cos(Lint angle)
    {
        angle = ClampAngle(angle);

        //Increase number of loops for higher precision
        int nLoops = 5;

        Lint result = 0;

        int flip = -1;

        for (int i = 0; i < nLoops; i++)
        {
            //flips back between 1 and -1 each loop
            flip = -flip;// (int)Mathf.Pow(-1, i);

            int k = 2 * i;

            int fact = Factorial(k);

            long pow = LintMath.Pow(angle, k);

            result += flip * pow / fact;
        }

        return result;
    }

    /// <summary>
    /// The Atan2 method is used so we can define in which quadrant the angle should be
    /// </summary>
    /// <param name="y"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Lint Atan2 (Lint y, Lint x)
    {
        Lint ay = Abs(y);
        Lint ax = Abs(x);

        //When y is greater than x, y/x is greater than 1
        bool invert = ay > ax;

        Lint z = invert ? ax / ay : ay / ax;

        Lint angle = Atan(z);

        if (invert) angle = HALF_PI - angle;
        //These adjustments are why we use the Atan2 method, as opposed to the Atan method
        if (x < 0) angle = PI - angle;
        if (y > 0) angle = -angle;

        return angle + HALF_PI;
    }


    public static Lint Atan(Lint value)
    {
        //Increase number of loops for higher precision
        int nLoops = 5;

        Lint result = 0;

        int flip = -1;

        for (int i = 0; i < nLoops; i++)
        {
            //flips back between 1 and -1 each loop
            flip = -flip;// (int)Mathf.Pow(-1, i);

            int k = 2 * i + 1;

            long pow = LintMath.Pow(value, k);

            result += flip * pow / k;
        }

        return result;
    }
}
