using System;
using AIContinuos;
using static System.Console;

double Rosenbrock(double[] x)
{
    var n = x.Length - 1;
    double sum = 0;

    for (int i = 0; i < n; i++)
        sum += 100 * (x[i + 1] - x[i] * x[i]) * (x[i + 1] - x[i] * x[i]) + (1 - x[i]) * (1 - x[i]);

    return sum;
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

// dateTime = DateTime.Now;
// double[] min = { 10, 10 };
// var sol = Optimize.GradientDescent(myFunction, min);
// WriteLine($"{sol[0]},{sol[1]}");
// WriteLine($"Duration: {(DateTime.Now - dateTime).TotalMilliseconds}");

dateTime = DateTime.Now;
double[] min = { 1, 1 };
var sol = Optimize.GradientDescent(Rosenbrock, min, lr: 1e-5, atol: 1e-9);
WriteLine($"{sol[0]} : {sol[1]}");
WriteLine($"Duration: {(DateTime.Now - dateTime).TotalMilliseconds}");
