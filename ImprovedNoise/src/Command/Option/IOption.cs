namespace ImprovedNoise.Command.Option
{
    public interface IOption
    {
        int Width { get; }
        int Height { get; }
        double Increment { get; }
        string Type { get; }
    }
}