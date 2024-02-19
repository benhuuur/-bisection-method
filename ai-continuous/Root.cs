namespace AIContinuos
{
    public static class Root
    {
        //usar double pq tem mais precisão, pois o float tem um erro maior que double
        public static double Bisection(
            Func<double, double> function,
            double a,
            double b,
            double tol = 1e-4,
            int maxIter = 1_000
        )
        {
            double currA = a;
            double currB = b;
            double c = 0;

            for (int i = 0; i < maxIter; i++)
            {
                var diff = currB - currA;
                var half = diff / 2;
                c = a + half;

                if(currA - currA < tol * 2)
                    return c;
                
                var fc = function(c);
                var fb = function(b);

                if (Math.Sign(fc) == Math.Sign(fb))
                    currB = c;
                else
                    currA = c;
            }
            return c;
        }
    }
}
