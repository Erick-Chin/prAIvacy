using prAIvacy.Core.Filters;

public class AdversarialNoiseFilter : IImageFilter
{
    private readonly float _intensity;
    private readonly Random _random;

    public string Name => $"Adversarial Noise ({_intensity})";

    public AdversarialNoiseFilter(float intensity = 0.05f)
    {
        _intensity = intensity;
        _random = new Random();
    }

    public void Apply(Image<Rgba32> image)
    {
        for (int y = 0; y < image.Height; y++)
        {
            for (int x = 0; x < image.Width; x++)
            {
                if ((x + y) % 7 == 0 && _random.NextDouble() < _intensity)
                {
                    var pixel = image[x, y];
                    pixel.R = (byte)Math.Clamp(pixel.R + _random.Next(-20, 20), 0, 255);
                    pixel.G = (byte)Math.Clamp(pixel.G + _random.Next(-20, 20), 0, 255);
                    pixel.B = (byte)Math.Clamp(pixel.B + _random.Next(-20, 20), 0, 255);
                    image[x, y] = pixel;
                    //image[x, y] = new Rgba32(0, 255, 0);
                }
            }
        }
    }
}