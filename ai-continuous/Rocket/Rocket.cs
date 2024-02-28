using System.Runtime.InteropServices;
using AIContinuous.Nuenv;
using AIContinuous.Rocket;

public class Rocket
{
    public double DryMass { get; set; }
    public double Fuel { get; set; }
    public double CrossSection { get; set; }
    public double Cd0 { get; set; }
    public double Ve { get; set; }
    public double Height { get; private set; } = 0;
    public double Velocity { get; private set; } = 0;
    public double[] TimeData { get; set; }
    public double[] MassFlowData { get; set; }
    public double Time { get; set; }

    public Rocket(
        double dryMass,
        double diameter,
        double ve,
        double cd0,
        double[] timeData,
        double[] massFlowData
    )
    {
        this.DryMass = dryMass;
        this.CrossSection = Utils.CrossSection(diameter);
        this.Ve = ve;
        this.Cd0 = cd0;
        this.TimeData = (double[])timeData.Clone();
        this.MassFlowData = (double[])massFlowData.Clone();
        this.Fuel = Integrate.Romberg(TimeData, MassFlowData);
        this.Time = 0.0;
    }

    private double GetAcceleration(double T, double D, double W)
    {
        var currentM = GetTotalMass();
        return (T + D + W) / currentM;
    }

    private double GetMassFlow(double t) => Interp1D.Linear(TimeData, MassFlowData, t);

    private double GetTotalMass() => this.DryMass + this.Fuel;

    private double GetThrust(double me) => me * Ve;

    private double GetDrag(double height, double velocity)
    {
        // velocity/Math.Abs(velocity)
        var vector = velocity > 0 ? 1 : -1;
        var currentCd = Drag.Coefficient(velocity, Atmosphere.Temperature(height), Cd0);
        return -0.5
            * currentCd
            * Atmosphere.Density(height)
            * CrossSection
            * velocity
            * velocity
            * vector;
    }

    private static double GetWeight(double mass, double height) =>
        -1.0 * mass * Gravity.GetGravity(height);

    private static double GetVelocity(double a, double dt) => a * dt;

    private void UpdateHeight(double dt) => Height += Velocity * dt;

    private void UpdateFuel(double t, double dt) =>
        this.Fuel -= 0.5 * (dt * GetMassFlow(t) + GetMassFlow(t + dt));

    public double Launch(double time, double dt = 1e-1)
    {
        for (double i = 0.0; i < time; i += dt)
            FlyALittleBit(dt);

        return this.Height;
    }

    public double LaunchUntilMax(double dt = 1e-1)
    {
        do FlyALittleBit(dt);
        while (Velocity > 0.0);

        return this.Height;
    }

    public double LaunchUntilGround(double dt = 1e-1)
    {
        do FlyALittleBit(dt);
        while (Height > 0.0);

        return this.Height;
    }

    public void FlyALittleBit(double dt)
    {
        var currentMe = GetMassFlow(this.Time);

        var t = GetThrust(currentMe);
        var d = GetDrag(this.Height, this.Velocity);
        var w = GetWeight(GetTotalMass(), this.Height);

        var a = GetAcceleration(t, d, w);
        var v = GetVelocity(a, dt);

        this.Velocity += v;

        UpdateHeight(dt);

        UpdateFuel(this.Time, dt);

        this.Time += dt;
    }
}
