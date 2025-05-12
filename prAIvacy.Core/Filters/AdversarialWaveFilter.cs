using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace prAIvacy.Core.Filters
{
    public class AdversarialWaveFilter : IImageFilter
    {
        public string Name => "Adversarial Wave Filter";

        private readonly float _amplitude;
        private readonly float _frequency;

        public AdversarialWaveFilter(float amplitude = 8f, float frequency = 0.1f)
        {
            _amplitude = amplitude;
            _frequency = frequency;
        }

        public void Apply(Image<Rgba32> image)
        {
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    // Sine wave perturbation based on x and y
                    float wave = (float)(Math.Sin(x * _frequency) + Math.Cos(y * _frequency)) * _amplitude;

                    var pixel = image[x, y];
                    pixel.R = (byte)Math.Clamp(pixel.R + wave, 0, 255);
                    pixel.G = (byte)Math.Clamp(pixel.G + wave, 0, 255);
                    pixel.B = (byte)Math.Clamp(pixel.B + wave, 0, 255);
                    image[x, y] = pixel;
                }
            }
        }
    }
}