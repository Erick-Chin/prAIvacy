using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using prAIvacy.Core.Filters;

class Program
{
    static void Main()
    {
        using Image<Rgba32> image = Image.Load<Rgba32>("input.jpg");

        var noiseFilter = new NoiseFilter(0.3f);
        noiseFilter.Apply(image);

        image.Save("output.jpg");

        Console.WriteLine("Filter applied and image saved.");
    }
}