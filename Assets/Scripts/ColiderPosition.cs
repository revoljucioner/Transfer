public class ColiderPosition
{
    public double Left { get; private set; }
    public double Right { get; private set; }
    public double Center { get; private set; }
    public double Size { get; private set; }

    public ColiderPosition(double left, double right, double center, double size)
    {
        Left = left;
        Right = right;
        Center = center;
        Size = size;
    }
}