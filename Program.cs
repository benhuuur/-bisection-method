using AIContinuos;
using static System.Console;

double myFunction(double x)
{
    return x + 1;
}

var sol = Root.Bisection(myFunction, -10, +10);
WriteLine(sol);
sol = Root.FalsePosition(myFunction, -10, +10);
WriteLine(sol);