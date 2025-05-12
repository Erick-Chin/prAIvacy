using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace prAIvacy.Core.Filters
{
    public class NoiseFilter : IImageFilter
    {
        private readonly float _intensity;
        private readonly Random _random;

        public string Name => $"Noise ({_intensity})";

        public NoiseFilter(float intensity = 0.2f)
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
                    if (_random.NextDouble() < _intensity)
                    {
                        image[x, y] = new Rgba32(
                            (byte)_random.Next(256),
                            (byte)_random.Next(256),
                            (byte)_random.Next(256)
                        );
                    }
                }
            }
        }
    }
}