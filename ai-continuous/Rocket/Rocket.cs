using AIContinuous.Rocket;

public class Rocket
{
    public double Mass { get; private set; } = 750.0;
    public double Fuel { get; private set; } = 3500.0;
    public double Diameter { get; private set; } = 0.6;
    public double Cd { get; private set; } = 0.8;
    public double Ve { get; private set; } = 1916.0;
    public double Height { get; set; } = 0;
    public double Velocity { get; set; } = 0;

    public double GetAcceleration(double T, double D, double W)
    {
        var currentM = this.Mass + this.Fuel;
        return (T + D + W) / currentM;
    }

    public double GetThrust(double me) => me * Ve;

    public double GetDrag(double height, double velocity, double diameter)
    {
        var vector = velocity > 0 ? 1 : -1;
        var currentCd = Drag.Coefficient(velocity, Atmosphere.Temperature(height), Cd);
        return -0.5
            * currentCd
            * Atmosphere.Density(height)
            * Utils.CrossSection(diameter)
            * velocity
            * velocity
            * vector;
    }

    public double GetWeight(double mass, double height) => -mass * Gravity.GetGravity(height);

    public double GetVelocity(double a, double time) => a * time;

    public double Launch(double[] me)
    {
        double oldH = this.Height;
        double newH = this.Height;
        int time = 0;
        while (oldH > newH)
        {
            var currentMe = this.Fuel <= 0 ? 0 : me[time];
            oldH = newH;
            var t = GetThrust(currentMe);
            var d = GetDrag(this.Height, this.Velocity, this.Diameter);
            var w = GetWeight(this.Mass + this.Fuel, this.Height);

            var a = GetAcceleration(t, d, w);

            var v = GetVelocity(a, time);

            this.Velocity += v;
            newH = oldH + v * time;
            this.Fuel -= currentMe;
            this.Height = newH;
            time++;
        }
        return this.Height;
    }
}
