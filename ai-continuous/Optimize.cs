using System;

namespace AIContinuos;

// metodos de inflecção
public static class Optimize
{
    public static double Newton(
        Func<double, double> function,
        double x0,
        double h = 1e-2,
        double atol = 1e-4,
        int maxIter = 1_000
    )
    {
        Func<double, double> diffFunction = x => Diff.Differentiate5P(function, x, h);
        Func<double, double> diffSecondFunction = x => Diff.Differentiate5P(diffFunction, x, h);
        return Root.Newton(diffFunction, diffSecondFunction, x0, atol, maxIter);
    }

    public static double GradientDescent(
        Func<double, double> function,
        double x0,
        double lr = 1e-2,
        double atol = 1e-4
    )
    {
        double xp = x0;
        double diff = Diff.Differentiate5P(function, xp);
        
        while (Math.Abs(diff) > atol)
        {
            diff = Diff.Differentiate5P(function, xp);
            xp -= diff * lr;
        }

        return xp;
    }

    public static double Min(Func<double, double> function, double x0)
    {
        double min = double.MaxValue;

        for (int i = (int)x0 - 10_000; i < (int)x0 + 10_000; i++)
        {
            var c = Newton(function, i);
            var fc = function(c);
            var fm = function(min);

            if (fc < fm || fm is double.NaN)
                min = c;
        }

        return min;
    }
}
