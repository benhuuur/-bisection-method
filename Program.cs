using System;
using AIContinuos;
using static System.Console;

// double myFunction(double x)
// {
//     return x + 1;
// }
double myFunction(double x)
{
    return x * x;
}
double myDer(double x)
{
    return 2 * x;
}

DateTime dateTime = DateTime.Now;

// dateTime = DateTime.Now;
// var sol = Root.Bisection(myFunction, -10, +10);
// WriteLine(sol);
// WriteLine($"Duration: {(DateTime.Now - dateTime).TotalMilliseconds}");

// dateTime = DateTime.Now;
// var sol = Root.FalsePosition(myFunction, -10, +10);
// WriteLine(sol);
// WriteLine($"Duration: {(DateTime.Now - dateTime).TotalMilliseconds}");

dateTime = DateTime.Now;
var sol = Root.Newton(myFunction, myDer, 10.0);
WriteLine(sol);
WriteLine($"Duration: {(DateTime.Now - dateTime).TotalMilliseconds}");
