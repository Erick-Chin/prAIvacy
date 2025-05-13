2025SP-INFOTC-4400
# prAIvacy – Anti-AI Image Protection Tool
	•	Eric Chin
	•	Sathwik Jupalli

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



prAIvacy – Anti-AI Image Protection Tool
Developers:
•	Eric Chin
•	Sathwik Jupalli
---
Overview
prAIvacy is a Windows desktop application designed to help users protect their personal images from being scraped or misused by AI systems. By applying privacy-preserving filters, such as adversarial noise and anti-aliasing disruption, the tool misleads AI classifiers while ensuring the images remain viewable by humans.
The project is inspired by cutting-edge adversarial defense research, including Nightshade and Fawkes, developed at the University of Chicago.
---
Features
Current Features
•	Load and Display Images: Users can select and preview images for processing.
•	Live Filter Previews: Adjustable sliders allow users to preview filter effects in real-time.
•	Adversarial Filters: Includes advanced filters like adversarial noise and wave-based disruptions.
•	Batch Processing: Process multiple images at once with consistent filter settings.
•	Export Filtered Images: Save processed images to a specified output folder.
•	Error Logging: Displays logs for successful and failed image processing tasks.
Planned Features
•	Mock AI Classifier: Simulate AI detection scores before and after applying filters.
•	Filter Presets: Provide predefined privacy modes for quick application.
•	Log Exporting: Save logs of processed images and applied filters.
---
Architecture
The application is divided into two main components:
Core (Cross-platform, C# .NET)
Handles all image processing and logic. This component is designed to be platform-independent and can be tested on macOS or Linux.
•	ImageProcessor.cs: Manages the application of filters in a chain.
•	Filters/: Implements IImageFilter for filters like Adversarial Noise and Wave Disruption.
•	BatchProcessor.cs: Handles batch processing of image folders.
•	Logger.cs: Logs filter usage and processing results.
UI (Windows-only, WPF)
The frontend is built with WPF to provide a user-friendly interface for previewing, adjusting, and exporting filtered images.
•	Live Previews: Displays both the original and filtered images side-by-side.
•	Interactive Sliders: Adjust filter intensity in real-time.
•	Batch Controls: Select multiple images and specify output folders.
---
Development Plan
| Week | Milestone                                   | |------|--------------------------------------------| | 1    | Research adversarial filters, create repo  | | 2    | Build basic WPF UI shell, implement image loader | | 3    | Add basic filters and image saving         | | 4    | Add live filter preview and sliders        | | 5    | Implement batch image processing           | | 6    | Integrate adversarial filters              | | 7    | Add mock AI detection and classifier       | | 8    | UI polish, filter presets, export logs     | | 9    | Testing and bug fixes                      | | 10   | Record demo, finalize README, submit project |
---
Running the Project
Prerequisites
•	Windows OS: The UI is built using WPF and requires Windows to run.
•	.NET 6+: Ensure you have the .NET SDK installed.
Steps to Run
1.	Clone the repository:
   git clone https://github.com/your-repo/prAIvacy.git
   cd prAIvacy
   2.	Open the solution in Visual Studio:
•	Double-click the prAIvacy.sln file to open the project in Visual Studio.
3.	Build and run the project:
•	Press F5 or go to Build > Start Debugging in Visual Studio.
4.	Use the application:
•	Load images, adjust filter settings, and export processed images.
---
Usage Notes
•	Input Folder: Use the "Browse Input Folder" button to select images for processing.
•	Output Folder: Specify where the processed images will be saved.
•	Filter Intensity: Adjust the slider to control the strength of the applied filters.
•	Batch Processing: Select multiple images for simultaneous processing.
---
Acknowledgments
This project is inspired by adversarial privacy research, including:
•	Nightshade
•	Fawkes
Special thanks to the University of Chicago for their contributions to adversarial defense techniques.

