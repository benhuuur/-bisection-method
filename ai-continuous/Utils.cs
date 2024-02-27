using System;

public static class Utils
{
    public static double CrossSection(double D) => Math.PI * D * D / 4;

    public static double Rescale(double x, double min, double max) => (max - min) * x + min;
}
