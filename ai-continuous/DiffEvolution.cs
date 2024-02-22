using System;
using System.Collections.Generic;
using System.Linq;

namespace AIContinuos;

public class DiffEvolution
{
    // todos os indivduos sempre tem a quantidade de propiedades iguais
    protected List<double[]> Individuals { get; set; }
    protected Func<double[], double> Fitness { get; }
    protected int Npop { get; set; }
    protected int Dimension { get; set; }
    protected double BestIndividualFitness { get; set; } = double.MaxValue;
    protected int BestIndividualIndex { get; set; }
    protected List<double[]> Bounds { get; set; }

    public DiffEvolution(Func<double[], double> fitness, int npop, List<double[]> bounds)
    {
        this.Individuals = new(npop);
        this.Fitness = fitness;
        this.Npop = npop;
        this.Dimension = bounds.Count;
        this.Bounds = bounds.ToList();
    }

    private void GeneratePopulation()
    {
        var dimension = Dimension;
        for (int i = 0; i < Npop; i++)
        {
            Individuals[i] = new double[dimension];
            for (int j = 0; j < dimension; j++)
                Individuals[i][j] = Utils.Rescale(
                    Random.Shared.NextDouble(),
                    Bounds[j][0],
                    Bounds[j][1]
                );
        }
    }

    private void FindBestIndividual()
    {
        var fitnessBest = BestIndividualFitness;
        for (int i = 0; i < Npop; i++)
        {
            var fitnessCurrent = Fitness(Individuals[BestIndividualIndex]);

            if (fitnessCurrent < fitnessBest)
            {
                BestIndividualIndex = i;
                fitnessBest = fitnessCurrent;
            }
        }
        BestIndividualFitness = fitnessBest;
    }

    private double[] Mutate(double[] individual)
    {
        var newIndivdual = new double[Dimension];

        newIndivdual = Individuals[BestIndividualIndex];
        for (int i = 0; i < Dimension; i++)
        {
            newIndivdual[i] +=
                Individuals[Random.Shared.Next(Npop)][i] - Individuals[Random.Shared.Next(Npop)][i];
        }

        return newIndivdual;
    }

    public double[] Optimize()
    {
        GeneratePopulation();
        FindBestIndividual();
        return Individuals[BestIndividualIndex];
    }
}
