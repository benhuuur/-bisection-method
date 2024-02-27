using System;
using System.Collections.Generic;
using System.Diagnostics;
using AIContinuous;
using AIContinuous.Rocket;
using static System.Console;

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

// dateTime = DateTime.Now;
// double[] min = { 1, 1 };
// var sol = Optimize.GradientDescent(Rosenbrock, min, lr: 1e-5, atol: 1e-9);
// WriteLine($"{sol[0]} : {sol[1]}");
// WriteLine($"Duration: {(DateTime.Now - dateTime).TotalMilliseconds}");

var sw = new Stopwatch();
var Rocket = new Rocket();
sw.Start();
List<double[]> bounds = new();


for (int i = 0; i < 20; i++)
{
    var bound = new double[] { 0, Utils.Rescale(Random.Shared.NextDouble(), 0.0, 3500.0) };
    bounds.Add(bound);
}

double Restriction(double[] x)
{
    return -1.0;
}



var diffEvolution = new DiffEvolution(Rocket.Launch, Restriction, 200, bounds);
var sol = diffEvolution.Optimize(4_000);
sw.Stop();
WriteLine($"{sol[0]} : {sol[1]}");
WriteLine($"Duration: {sw.Elapsed}");
