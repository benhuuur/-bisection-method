using System;
using System.Collections.Generic;
using System.Linq;
using AulasAI.Nuenv;
using AulasAI.Optimize;
using AulasAI.Rocket;

int numberOfPoints = 51;
var timeData = Space.Geometric(1.0, 1001.0, numberOfPoints).Select(x => x - 1.0).ToArray(); // gasta mais no inicio do lançamento do foguete foguete

double Simulate(double[] massFlowData)
{
    var rocket = new Rocket(750.0, 0.6, 1916.0, 0.8, timeData, massFlowData);

    return rocket.LaunchUntilMax(1e-1);
}

double Fitness(double[] x) => -1.0 * Simulate(x);

double Restriction(double[] x) => Integrate.Romberg(timeData, x) - 3500.0;

var bounds = new List<double[]>(numberOfPoints);
for (int i = 0; i < numberOfPoints; i++)
    bounds.Add(new double[] { 0.0, 1000.0 });

var diffEvol = new DiffEvolution(Fitness, bounds, 15 * numberOfPoints, Restriction);
var sol = diffEvol.Optimize(10000);

foreach (var s in sol)
    Console.WriteLine(s);

Console.WriteLine($"Altitude maxima: {Simulate(sol)}");
