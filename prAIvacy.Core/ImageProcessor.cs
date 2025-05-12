using System;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using prAIvacy.Core.Filters;

namespace prAIvacy.Core
{
    public class ImageProcessor
    {
        private readonly List<IImageFilter> _filters = new();

        public void AddFilter(IImageFilter filter)
        {
            _filters.Add(filter);
        }

        public void ListFilters()
        {
            Console.WriteLine("Available filters:");
            for (int i = 0; i < _filters.Count; i++)
            {
                Console.WriteLine($"{i}: {_filters[i].Name}");
            }
        }

        public void ApplyAll(Image<Rgba32> image)
        {
            foreach (var filter in _filters)
            {
                Console.WriteLine($"Applying: {filter.Name}");
                filter.Apply(image);
            }
        }

        public string? ApplyFilterByIndex(Image<Rgba32> image, int index)
        {
            if (index < 0 || index >= _filters.Count)
            {
                Console.WriteLine("❌ Invalid filter index.");
                return null;
            }

            var filter = _filters[index];
            Console.WriteLine($"Applying: {filter.Name}");
            filter.Apply(image);
            return filter.Name;
        }

        public void ApplyFilterByName(Image<Rgba32> image, string name)
        {
            var filter = _filters.Find(f => f.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (filter == null)
            {
                Console.WriteLine("❌ Filter not found.");
                return;
            }

            Console.WriteLine($"Applying: {filter.Name}");
            filter.Apply(image);
        }
    }
}