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
        string inputFolder = "input_images";
        string outputFolder = "output_images";

        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"❌ Input folder not found: {inputFolder}");
            return;
        }

        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        string[] imageFiles = Directory.GetFiles(inputFolder, "*.jpg"); // You can extend to *.png, etc.

        if (imageFiles.Length == 0)
        {
            Console.WriteLine("⚠️ No image files found in input folder.");
            return;
        }

        // Normalize slider value (example: assume slider value is 5)
        double sliderValue = 5; // Replace this with the actual slider value from your UI
        float normalizedIntensity = (float)(sliderValue / 10.0); // Normalize to 0–1

        var processor = new ImageProcessor();
        //processor.AddFilter(new NoiseFilter(normalizedIntensity));
        processor.AddFilter(new AdversarialNoiseFilter(normalizedIntensity));
        processor.AddFilter(new AdversarialWaveFilter(8f, normalizedIntensity));

        Console.WriteLine($"🔄 Processing {imageFiles.Length} images...\n");

        foreach (var imagePath in imageFiles)
        {
            using Image<Rgba32> image = Image.Load<Rgba32>(imagePath);

            processor.ApplyAll(image);

            string fileName = Path.GetFileNameWithoutExtension(imagePath);
            string extension = Path.GetExtension(imagePath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_filtered{extension}");

            image.Save(outputPath);
            Console.WriteLine($"✅ Saved: {outputPath}");
        }

        Console.WriteLine("\n🎉 Batch filtering complete.");
    }
}