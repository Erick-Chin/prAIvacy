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

        var processor = new ImageProcessor();
        processor.AddFilter(new NoiseFilter(0.3f));
        processor.AddFilter(new AdversarialNoiseFilter(0.9f));
        processor.AddFilter(new AdversarialWaveFilter(8f, 0.05f));

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
