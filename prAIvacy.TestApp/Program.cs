using System;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using prAIvacy.Core;
using prAIvacy.Core.Filters;

class Program
{
    static void Main()
    {
        string inputPath = "input.jpg";

        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"❌ File not found: {inputPath}");
            return;
        }

        using Image<Rgba32> image = Image.Load<Rgba32>(inputPath);

        var processor = new ImageProcessor();

        // Add your filters here
        //processor.AddFilter(new NoiseFilter(0.3f));
        processor.AddFilter(new AdversarialNoiseFilter(0.9f));
        processor.AddFilter(new AdversarialWaveFilter(8f, 0.05f));

        processor.ListFilters();

        Console.Write("Enter filter number to apply (or press Enter to apply ALL): ");
        string? input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            // Apply all filters
            processor.ApplyAll(image);

            string fileName = "output-AllFilters.jpg";
            image.Save(fileName);
            Console.WriteLine($"✅ Saved as: {fileName}");
        }
        else if (int.TryParse(input, out int index))
        {
            string? filterName = processor.ApplyFilterByIndex(image, index);

            if (!string.IsNullOrEmpty(filterName))
            {
                string outputPath = $"output-{SanitizeFileName(filterName)}.jpg";
                image.Save(outputPath);
                Console.WriteLine($"✅ Saved as: {outputPath}");
            }
        }
        else
        {
            Console.WriteLine("❌ Invalid input.");
        }
    }

    // Helper to make filter names filename-safe
    static string SanitizeFileName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return name.Replace(" ", "_");
    }
}