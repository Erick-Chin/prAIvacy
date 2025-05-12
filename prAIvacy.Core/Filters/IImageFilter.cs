using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace prAIvacy.Core.Filters
{
    public interface IImageFilter
    {
        void Apply(Image<Rgba32> image);
        string Name { get; }
    }
}