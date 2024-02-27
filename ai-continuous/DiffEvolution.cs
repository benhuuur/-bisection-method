using System;
using System.Collections.Generic;
using System.Linq;

namespace AIContinuous;

public class DiffEvolution
{
    // todos os indivduos sempre tem a quantidade de propiedades iguais
    protected List<double[]> Individuals { get; set; }
    protected Func<double[], double> Fitness { get; }
    protected int Npop { get; set; }
    protected Func<double[], double> Restriction { get; set; }
    protected double MutationMin { get; set; }
    protected double MutationMax { get; set; }
    protected int Dimension { get; set; }
    protected int BestIndividualIndex { get; set; }
    protected List<double[]> Bounds { get; set; }
    private double[] IndividualsRestrictions { get; set; }
    private double[] IndividualsFitness { get; set; }
    public double Recombination { get; set; }

    public DiffEvolution(
        Func<double[], double> fitness,
        Func<double[], double> restriction,
        int npop,
        List<double[]> bounds,
        double recombination = 0.8,
        double mutationMin = 0.5,
        double mutationMax = 0.9
    )
    {
        this.Fitness = fitness;
        this.Restriction = restriction;
        this.Npop = npop;
        this.Individuals = new(npop);
        this.Dimension = bounds.Count;
        this.Bounds = bounds.ToList();
        this.Recombination = recombination;
        this.MutationMax = mutationMax;
        this.MutationMin = mutationMin;
        this.IndividualsRestrictions = new double[npop];
        this.IndividualsFitness = new double[npop];
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

            IndividualsRestrictions[i] = Restriction(Individuals[i]);
            IndividualsFitness[i] =
                IndividualsRestrictions[i] <= 0.0 ? Fitness(Individuals[i]) : double.MaxValue;
        }
        FindBestIndividual();
    }

    private void FindBestIndividual()
    {
        var fitnessBest = this.IndividualsFitness[BestIndividualIndex];
        for (int i = 0; i < Npop; i++)
        {
            var fitnessCurrent = this.Fitness(this.Individuals[this.BestIndividualIndex]);

            if (fitnessCurrent < fitnessBest)
            {
                BestIndividualIndex = i;
                fitnessBest = fitnessCurrent;
            }
        }
        this.IndividualsFitness[BestIndividualIndex] = fitnessBest;
    }

    private double[] Mutate(int index)
    {
        int individualRand1;
        int individualRand2;

        do individualRand1 = Random.Shared.Next(Npop);
        while (individualRand1 == index);

        do individualRand2 = Random.Shared.Next(Npop);
        while (individualRand1 == individualRand2);

        var newIndivdual = (double[])Individuals[BestIndividualIndex].Clone();
        for (int i = 0; i < Dimension; i++)
        {
            newIndivdual[i] +=
                Utils.Rescale(Random.Shared.NextDouble(), MutationMin, MutationMax)
                * (this.Individuals[individualRand1][i] - this.Individuals[individualRand2][i]);
        }

        return newIndivdual;
    }

    protected double[] Crossover(int index)
    {
        var trial = Mutate(index);
        var trial2 = (double[])Individuals[index].Clone();

        for (int i = 0; i < Dimension; i++)
        {
            if (
                !(
                    (Random.Shared.NextDouble() < this.Recombination)
                    || (i == Random.Shared.Next(Dimension))
                )
            )
                trial2[i] = trial[i];
        }

        return trial2;
    }

    protected void Iterate()
    {
        for (int i = 0; i < Npop; i++)
        {
            var current = Individuals[i];
            var trial = Crossover(i);

            var restTrial = this.Restriction(trial);
            var fitnessTrial = restTrial <= 0.0 ? this.Fitness(trial) : double.MaxValue;
            var restIndividual = this.IndividualsRestrictions[i];

            if (
                (restTrial < restIndividual && restIndividual > 0.0)
                || (restTrial <= 0.0 && restIndividual > 0.0)
                || (restTrial <= 0.0 && fitnessTrial < IndividualsFitness[i])
            )
            {
                Individuals[i] = trial;
                IndividualsFitness[i] = fitnessTrial;
                IndividualsRestrictions[i] = restTrial;
            }
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
