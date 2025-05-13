using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using prAIvacy.Core;
using prAIvacy.Core.Filters;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        private BitmapImage? originalImage; // Made nullable to fix CS8618 and CS0649
        private List<string> selectedImages = new List<string>();
        private int currentImageIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseInputFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Select Images",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
                Multiselect = true // Enable multi-selection
            };

            if (dialog.ShowDialog() == true)
            {
                selectedImages = dialog.FileNames.ToList();
                currentImageIndex = 0;

                if (selectedImages.Count > 0)
                {
                    InputFolderTextBox.Text = string.Join(", ", selectedImages);
                    UpdatePreview(); // Show the first image
                }
                else
                {
                    MessageBox.Show("No files selected.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void BrowseOutputFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Select a Folder",
                Filter = "Folders|*.none", // This filter is a workaround to allow folder selection
                CheckFileExists = false,
                FileName = "Select Folder"
            };

            if (dialog.ShowDialog() == true)
            {
                string? folderPath = Path.GetDirectoryName(dialog.FileName);
                if (folderPath != null)
                {
                    OutputFolderTextBox.Text = folderPath;
                }
                else
                {
                    MessageBox.Show("Invalid folder path selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void FilterIntensitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (FilterIntensityLabel != null)
            {
                FilterIntensityLabel.Content = $"Filter Intensity: {FilterIntensitySlider.Value:F1}";
            }

            if (originalImage != null)
            {
                // Normalize the slider value to a range of 0 to 1
                double normalizedIntensity = FilterIntensitySlider.Value / 10.0;

                // Apply the filter with the normalized intensity
                var filteredImage = ApplyFilter(originalImage, normalizedIntensity);
                FilteredPreviewImage.Source = filteredImage;
            }
        }

        private BitmapImage ApplyFilter(BitmapImage image, double intensity)
        {
            // Convert BitmapImage to a format compatible with ImageSharp
            using var memoryStream = new MemoryStream();
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            encoder.Save(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            using Image<Rgba32> imageSharpImage = Image.Load<Rgba32>(memoryStream);

            // Apply filters using ImageProcessor
            var processor = new ImageProcessor();
            //processor.AddFilter(new NoiseFilter((float)intensity));
            processor.AddFilter(new AdversarialNoiseFilter((float)intensity));
            processor.AddFilter(new AdversarialWaveFilter(8f, (float)intensity));

            processor.ApplyAll(imageSharpImage);

            // Convert the processed ImageSharp image back to BitmapImage
            using var outputStream = new MemoryStream();
            imageSharpImage.SaveAsPng(outputStream);
            outputStream.Seek(0, SeekOrigin.Begin);

            var filteredImage = new BitmapImage();
            filteredImage.BeginInit();
            filteredImage.CacheOption = BitmapCacheOption.OnLoad;
            filteredImage.StreamSource = outputStream;
            filteredImage.EndInit();

            return filteredImage;
        }

        private void UpdatePreview()
        {
            if (selectedImages.Count == 0 || currentImageIndex < 0 || currentImageIndex >= selectedImages.Count)
            {
                MessageBox.Show("No images to preview.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Load the current image
            string currentImagePath = selectedImages[currentImageIndex];
            originalImage = new BitmapImage(new Uri(currentImagePath));
            PreviewImage.Source = originalImage;

            // Apply the filter to the current image
            if (originalImage != null)
            {
                var filteredImage = ApplyFilter(originalImage, FilterIntensitySlider.Value);
                FilteredPreviewImage.Source = filteredImage;
            }
        }

        private void PreviousImage_Click(object sender, RoutedEventArgs e)
        {
            if (currentImageIndex > 0)
            {
                currentImageIndex--;
                UpdatePreview();
            }
            else
            {
                MessageBox.Show("This is the first image.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void PreviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedImages.Count == 0)
            {
                MessageBox.Show("No images selected for preview.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Reset to the first image and update the preview
            currentImageIndex = 0;
            UpdatePreview();
        }

        private void NextImage_Click(object sender, RoutedEventArgs e)
        {
            if (currentImageIndex < selectedImages.Count - 1)
            {
                currentImageIndex++;
                UpdatePreview();
            }
            else
            {
                MessageBox.Show("This is the last image.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ProcessImages_Click(object sender, RoutedEventArgs e)
        {
            string[] selectedFiles = InputFolderTextBox.Text.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            string outputFolder = OutputFolderTextBox.Text;

            if (selectedFiles.Length == 0)
            {
                MessageBox.Show("No input files selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Get the filter intensity from the slider
            float filterIntensity = (float)FilterIntensitySlider.Value;

            var processor = new ImageProcessor();
            //processor.AddFilter(new NoiseFilter(filterIntensity));
            processor.AddFilter(new AdversarialNoiseFilter(filterIntensity));
            processor.AddFilter(new AdversarialWaveFilter(8f, filterIntensity));

            LogListBox.Items.Add($"Processing {selectedFiles.Length} images with filter intensity {filterIntensity}...\n");

            foreach (var imagePath in selectedFiles)
            {
                try
                {
                    using Image<Rgba32> image = Image.Load<Rgba32>(imagePath);

                    processor.ApplyAll(image);

                    string fileName = Path.GetFileNameWithoutExtension(imagePath);
                    string extension = Path.GetExtension(imagePath);
                    string outputPath = Path.Combine(outputFolder, $"{fileName}_filtered{extension}");

                    image.Save(outputPath);
                    LogListBox.Items.Add($"✅ Saved: {outputPath}");
                }
                catch (Exception ex)
                {
                    LogListBox.Items.Add($"❌ Error processing {imagePath}: {ex.Message}");
                }
            }

            LogListBox.Items.Add("\n🎉 Batch filtering complete.");
        }
    }
}