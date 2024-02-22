using System;
using AIContinuos;
using static System.Console;

// double myFunction(double x)
// {
//     return x + 1;
// }

// double myFunction(double x)
// {
//     return (x - 1) * (x - 1) + Math.Sin(x * x * x);
// }

// double myFunction(double x)
// {
//     return (Math.Sqrt(Math.Abs(x)) - 5) * x + 10;
// }
// double myDer(double x)
// {
//     return 1 / 2 * Math.Sqrt(Math.Abs(x)) * x + (Math.Sqrt(Math.Abs(x)) - 5);
// }

// double myFunction(double[] x)
// {
//     return x[0] * x[0] + x[1] * x[1];
// }

double myFunction(double[] x)
{
    return Math.Pow((x[0] + x[1] * 2 - 7), 2) + Math.Pow((x[0] * 2 + x[1] - 5), 2);
}

DateTime dateTime = DateTime.Now;

// dateTime = DateTime.Now;
//  var sol = Root.Bisection(myFunction, -11, +10);
// WriteLine(sol);
// WriteLine($"Duration: {(DateTime.Now - dateTime).TotalMilliseconds}");

// dateTime = DateTime.Now;
//  var sol = Root.FalsePosition(myFunction, -10, +10);
// WriteLine(sol);
// WriteLine($"Duration: {(DateTime.Now - dateTime).TotalMilliseconds}");

// dateTime = DateTime.Now;
//  var sol = Root.Newton(myFunction, myDer, 10.0);
// WriteLine(sol);
// WriteLine($"Duration: {(DateTime.Now - dateTime).TotalMilliseconds}");

// dateTime = DateTime.Now;
//  var sol = Root.Newton(myFunction, double (double x) => Diff.Differentiate3P(myFunction, x), 10.0);
// WriteLine(sol);
// WriteLine($"Duration: {(DateTime.Now - dateTime).TotalMilliseconds}");

// dateTime = DateTime.Now;
//  var sol = Optimize.Newton(myFunction, 1);
// WriteLine(sol);
// WriteLine($"Duration: {(DateTime.Now - dateTime).TotalMilliseconds}");

// dateTime = DateTime.Now;
//  var sol = Optimize.Min(myFunction, 10000);
// WriteLine(sol);
// WriteLine($"Duration: {(DateTime.Now - dateTime).TotalMilliseconds}");

dateTime = DateTime.Now;

double[] min = { 10, 10 };
var sol = Optimize.GradientDescent(myFunction, min);
WriteLine($"{sol[0]},{sol[1]}");
WriteLine($"Duration: {(DateTime.Now - dateTime).TotalMilliseconds}");
