using System;
using System.IO;
using prAIvacy.Core.Filters;
using prAIvacy.Core;


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

        string[] imageFiles = Directory.GetFiles(inputFolder, "*.jpg"); // you can extend to *.png
        if (imageFiles.Length == 0)
        {
            Console.WriteLine("⚠️ No image files found in input folder.");
            return;
        }

        var processor = new ImageProcessor();
        processor.AddFilter(new NoiseFilter(0.3f));
        processor.AddFilter(new AdversarialNoiseFilter(0.9f));
        processor.AddFilter(new AdversarialWaveFilter(8f, 0.05f));

        // Make sure this matches the file extension above
        processor.AddFilter(new FawkesFilter(
            fawkesDir: "/Users/sathwikj/Documents/GitHub/fawkes",
            mode: "low",
            format: "jpg"
        ));

        foreach (var imagePath in imageFiles)
        {
            Console.WriteLine($"\n▶️ Processing: {Path.GetFileName(imagePath)}");

            using var image = Image.Load<Rgba32>(imagePath);
            processor.ApplyAll(image);

            string fileName = Path.GetFileNameWithoutExtension(imagePath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_filtered.jpg");
            image.Save(outputPath);

            Console.WriteLine($"✅ Saved: {outputPath}");
        }

        Console.WriteLine("\n✅ Batch filtering complete.");
    }
}
