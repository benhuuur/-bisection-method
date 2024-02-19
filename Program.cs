using AIContinuos;
using static System.Console;

double myFunction(double x)
{
    return x + 5;
}

var sol = Root.Bisection(myFunction, -10, +10);
WriteLine(sol);
using AIContinuos;
using static System.Console;

double myFunction(double x)
{
    return x + 5;
}

var sol = Root.Bisection(myFunction, -10, +10);
WriteLine(sol);
