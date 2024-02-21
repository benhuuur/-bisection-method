using System;
using static System.Console;

namespace AIContinuos
{
    public static class Root
    {
        //usar double pq tem mais precisão, pois o float tem um erro maior que double
        public static double FalsePosition(
            Func<double, double> function,
            double a,
            double b,
            double tol = 1e-4,
            int maxIter = 1_000
        )
        {
            double currA = a;
            double currB = b;
            double fcurrA = function(a);
            double fcurrB = function(b);
            double c = 0;

            for (int i = 0; i < maxIter; i++)
            {
                c = -fcurrA * ((currB - currA) / (fcurrB - fcurrA)) + currA;

                if (currB - currA < tol * 2.0)
                    break;

                var fc = function(c);

                if (Math.Abs(fc) < tol)
                    break;

                if (Math.Sign(fc) == Math.Sign(fcurrB))
                    currB = c;
                else
                    currA = c;
            }
            return c;
        }

        public static double Bisection(
            Func<double, double> function,
            double a,
            double b,
            double atol = 1e-4,
            double rtol = 1e-4,
            int maxIter = 1_000
        )
        {
            double currA = a;
            double currB = b;
            double c = 0;

            for (int i = 0; i < maxIter; i++)
            {
                var diff = currB - currA;
                var half = diff / 2.0;
                c = a + half;

                var fc = function(c);
                var fb = function(b);

                if (Math.Sign(fc) == Math.Sign(fb))
                    currB = c;
                else
                    currA = c;

                if (Math.Abs(fc) < atol)
                    break;

                if (currB - currA < rtol * 2.0)
                    break;
            }
            return c;
        }

        public static double Newton(
            Func<double, double> function,
            Func<double, double> der,
            double x0,
            double rtol = 1e-4,
            double atol = 1e-4,
            int maxIter = 10_000
        )
        {
            double c = x0;

            for (int i = 0; i < maxIter; i++)
            {
                var fc = function(c);
                c -= fc / der(c);

                if (Math.Abs(fc) < atol)
                    break;
            }

            return c;
        }
    }
}
