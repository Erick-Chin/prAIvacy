2025SP-INFOTC-4400
# prAIvacy – Anti-AI Image Protection Tool

## Overview

**prAIvacy** is a Windows desktop application that helps users protect their personal images from being scraped or misused by AI systems. It applies privacy-preserving filters, including adversarial noise and anti-aliasing disruption, to mislead AI classifiers while keeping images viewable by humans.

The project draws inspiration from modern adversarial defense research, including [Nightshade](https://nightshade.cs.uchicago.edu/) and [Fawkes](https://sandlab.cs.uchicago.edu/fawkes/), developed at the University of Chicago.

---

## Features

### Minimum (Week 2-3)
- Load and display user images
- Apply basic filters (blur, noise, color shift)
- Save/export modified images

### Target (Week 4-6)
- Apply adversarial filters
- Batch process folders
- Live filter previews with adjustable settings

### Stretch (Week 7+)
- Advanced adversarial filter inspired by Nightshade
- Mock AI classifier with before/after detection scores
- Export logs and provide privacy mode presets

---

## Architecture

The app is split into two parts:

### Core (Cross-platform, C# .NET)
Handles image processing and logic. This can be developed and tested on macOS.

- `ImageLoader.cs` – Load/save images
- `ImageProcessor.cs` – Apply filters in a chain
- `Filters/` – Implements `IImageFilter` with filters like Noise, Blur, etc.
- `BatchProcessor.cs` – Process image folders
- `Logger.cs` – Optional export of filter usage logs

## UI (Windows-only, WPF)
The frontend built with WPF to preview, adjust, and export filtered images.

---

## Development Plan

| Week | Milestone |
|------|-----------|
| 1    | Research adversarial filters (Nightshade, Fawkes), create repo |
| 2    | Build basic WPF UI shell, implement image loader |
| 3    | Add basic filters + image saving |
| 4    | Add live filter preview, sliders |
| 5    | Add batch image processing |
| 6    | Integrate adversarial filters |
| 7    | Add mock AI detection & classifier |
| 8    | UI polish, filter presets, export logs |
| 9    | Testing, bug fixes |
| 10   | Record demo, finalize README, submit project |

---

## Running the Project

```bash
cd Core
dotnet run
