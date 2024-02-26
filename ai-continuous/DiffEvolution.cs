using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Microsoft.VisualBasic;

namespace AIContinuos;

public class DiffEvolution
{
    // todos os indivduos sempre tem a quantidade de propiedades iguais
    protected List<double[]> Individuals { get; set; }
    protected Func<double[], double> Fitness { get; }
    protected int Npop { get; set; }
    protected double Mutation { get; set; }
    protected int Dimension { get; set; }
    protected double BestIndividualFitness { get; set; } = double.MaxValue;
    protected int BestIndividualIndex { get; set; }
    protected List<double[]> Bounds { get; set; }

    public DiffEvolution(
        Func<double[], double> fitness,
        int npop,
        List<double[]> bounds,
        double mutation = 0.7
    )
    {
        this.Individuals = new(npop);
        this.Fitness = fitness;
        this.Npop = npop;
        this.Dimension = bounds.Count;
        this.Bounds = bounds.ToList();
        this.Mutation = mutation;
    }

    private void GeneratePopulation()
    {
        var dimension = this.Dimension;
        for (int i = 0; i < Npop; i++)
        {
            this.Individuals.Add(new double[dimension]);
            for (int j = 0; j < dimension; j++)
                this.Individuals[i][j] = Utils.Rescale(
                    Random.Shared.NextDouble(),
                    this.Bounds[j][0],
                    this.Bounds[j][1]
                );
        }
        FindBestIndividual();
    }

    private void FindBestIndividual()
    {
        var fitnessBest = this.BestIndividualFitness;
        for (int i = 0; i < Npop; i++)
        {
            var fitnessCurrent = this.Fitness(this.Individuals[this.BestIndividualIndex]);

            if (fitnessCurrent < fitnessBest)
            {
                BestIndividualIndex = i;
                fitnessBest = fitnessCurrent;
            }
        }
        this.BestIndividualFitness = fitnessBest;
    }

    private double[] Mutate(double[] individual)
    {
        var newIndivdual = new double[this.Dimension];

        newIndivdual = Individuals[this.BestIndividualIndex];

        var individualRand1 = Random.Shared.Next(Npop);
        var individualRand2 = Random.Shared.Next(Npop);

        for (int i = 0; i < Dimension; i++)
        {
            newIndivdual[i] +=
                this.Mutation
                * (this.Individuals[individualRand1][i] - this.Individuals[individualRand2][i]);
        }

        return newIndivdual;
    }

    protected void Iterate()
    {
        for (int i = 0; i < Npop; i++)
        {
            var current = Individuals[i];
            var trial = Mutate(current);
            if(Fitness(trial) < Fitness(current))
                Individuals[i] = trial;
        }

        FindBestIndividual();
    }

    public double[] Optimize(int n)
    {
        GeneratePopulation();

        for (int i = 0; i < n; i++)
            Iterate();

        return this.Individuals[this.BestIndividualIndex];
    }
}
