using System;
using System.Diagnostics;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace prAIvacy.Core.Filters
{
    public class FawkesFilter : IImageFilter
    {
        public string Name => "Fawkes Cloaking Filter";

        private readonly string _fawkesDir;
        private readonly string _pythonPath;
        private readonly string _mode;
        private readonly string _format;

        public FawkesFilter(string fawkesDir, string mode = "low", string format = "png")
        {
            _fawkesDir = fawkesDir;
            _pythonPath = Path.Combine(_fawkesDir, "venv", "bin", "python");
            _mode = mode;
            _format = format;
        }

        public void Apply(Image<Rgba32> image)
        {
            string tempInputDir = Path.Combine(Path.GetTempPath(), "fawkes_input");
            string cloakedDir = Path.Combine(tempInputDir, "cloaked");

            Directory.CreateDirectory(tempInputDir);
            Directory.CreateDirectory(cloakedDir);

            string inputPath = Path.Combine(tempInputDir, "input." + _format);
            image.Save(inputPath);

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = _pythonPath,
                    Arguments = $"fawkes/protection.py -d \"{tempInputDir}\" --mode {_mode} --format {_format}",
                    WorkingDirectory = _fawkesDir,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.OutputDataReceived += (sender, e) => { if (e.Data != null) Console.WriteLine(e.Data); };
            process.ErrorDataReceived += (sender, e) => { if (e.Data != null) Console.WriteLine("FAWKES STDERR: " + e.Data); };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            string baseName = Path.GetFileNameWithoutExtension(inputPath);
            string cloakedImagePath = Path.Combine(cloakedDir, baseName + "." + _format);

            if (File.Exists(cloakedImagePath))
            {
                using var cloaked = Image.Load<Rgba32>(cloakedImagePath);
                image.Mutate(x => x.DrawImage(cloaked, 1f));
                Console.WriteLine("✅ Applied Fawkes cloaked image.");
            }
            else
            {
                Console.WriteLine("❌ Fawkes failed to generate cloaked image.");
            }

            Directory.Delete(tempInputDir, true); // clean up
        }
    }
}
