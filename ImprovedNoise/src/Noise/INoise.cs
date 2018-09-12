namespace ImprovedNoise.Noise
{
    public interface INoise
    {
        double Noise(double x, double y);

        double Noise(double x, double y, double z);
    }
}