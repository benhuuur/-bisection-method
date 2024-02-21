using System;
using AIContinuos;
using static System.Console;

// double myFunction(double x)
// {
//     return x + 1;
// }

double myFunction(double x)
{
    return (x - 1) * (x - 1) + Math.Sin(x * x * x);
}

// double myFunction(double x)
// {
//     return (Math.Sqrt(Math.Abs(x)) - 5) * x + 10;
// }
// double myDer(double x)
// {
//     return 1 / 2 * Math.Sqrt(Math.Abs(x)) * x + (Math.Sqrt(Math.Abs(x)) - 5);
// }

DateTime dateTime = DateTime.Now;

double sol;

// dateTime = DateTime.Now;
// sol = Root.Bisection(myFunction, -11, +10);
// WriteLine(sol);
// WriteLine($"Duration: {(DateTime.Now - dateTime).TotalMilliseconds}");

// dateTime = DateTime.Now;
// sol = Root.FalsePosition(myFunction, -10, +10);
// WriteLine(sol);
// WriteLine($"Duration: {(DateTime.Now - dateTime).TotalMilliseconds}");

// dateTime = DateTime.Now;
// sol = Root.Newton(myFunction, myDer, 10.0);
// WriteLine(sol);
// WriteLine($"Duration: {(DateTime.Now - dateTime).TotalMilliseconds}");

// dateTime = DateTime.Now;
// sol = Root.Newton(myFunction, double (double x) => Diff.Differentiate3P(myFunction, x), 10.0);
// WriteLine(sol);
// WriteLine($"Duration: {(DateTime.Now - dateTime).TotalMilliseconds}");

// dateTime = DateTime.Now;
// sol = Optimize.Newton(myFunction, 1);
// WriteLine(sol);
// WriteLine($"Duration: {(DateTime.Now - dateTime).TotalMilliseconds}");

dateTime = DateTime.Now;
sol = Optimize.Min(myFunction, 10000);
WriteLine(sol);
WriteLine($"Duration: {(DateTime.Now - dateTime).TotalMilliseconds}");
