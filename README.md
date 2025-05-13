# prAIvacy – Anti-AI Image Protection Tool

## Developers
- Eric Chin  
- Sathwik Jupalli

---

## Overview

prAIvacy is a Windows desktop application designed to help users protect their personal images from being scraped, classified, or misused by AI systems. The application applies privacy-preserving filters, including adversarial noise and wave-based perturbations, which disrupt how machine learning models interpret visual data. These filters are designed to reduce the effectiveness of AI detection while maintaining human viewability.

This project was inspired by research efforts such as **Fawkes** and **Nightshade**, developed at the University of Chicago. While prAIvacy does not use these tools directly, it draws on their concepts to create a simplified, fast, and local alternative built entirely in C#.

---

## Features

### Current Functionality
- Load and display user-selected images
- Preview filters in real-time using sliders
- Apply adversarial filters such as noise and wave-based perturbations
- Apply filters individually or sequentially
- Save processed images with filter-based filenames
- Batch process folders of images
- Log successful and failed filter operations

### Planned Functionality
- Simulate AI detection scores (mock classifier)
- Add filter presets (e.g., "Light", "Strong", "Max Shield")
- Export logs of processing activity for auditing

---

## Architecture

### Core (Cross-platform, written in C# .NET)
Handles all business logic and image transformations. This can be run and tested outside of the UI on macOS, Linux, or Windows.

**Key Files:**
- `ImageProcessor.cs`: Manages chaining and execution of filters
- `Filters/`: Directory containing filters like `NoiseFilter.cs`, `AdversarialWaveFilter.cs`
- `BatchProcessor.cs`: Automates multi-image processing with uniform settings
- `Logger.cs`: Captures errors and successes during filter operations

### UI (Windows-only, WPF)
The WPF front-end presents a user-friendly interface that supports:
- Image previews before and after filtering
- Real-time slider-based filter adjustments
- File/folder browsing for input and output
- Logging display for user feedback

---

## Development Timeline

| Week | Milestone                                        |
|------|--------------------------------------------------|
| 1    | Research adversarial filters and create repo     |
| 2    | Build WPF UI shell and implement image loader    |
| 3    | Add basic filters and image saving functionality |
| 4    | Add live filter previews and slider controls     |
| 5    | Implement batch image processing                 |
| 6    | Integrate adversarial filters                    |
| 7    | Add mock AI classifier (planned)                 |
| 8    | UI polish, filter presets, and log exporting     |
| 9    | Testing and bug fixing                           |
| 10   | Record demo, finalize documentation, submit      |

---

## Getting Started

### Prerequisites
- Windows OS (required for the WPF front-end)
- .NET 6 or later
- Visual Studio 2022 or newer

### How to Run the Project
1. Clone the repository:
- git clone https://github.com/your-username/prAIvacy.git
- cd prAIvacy
2. Open `prAIvacy.sln` in Visual Studio.
3. Press `F5` or go to `Build > Start Debugging`.

---

## Usage Instructions

- Use the **"Browse Input Folder"** button to load your images.
- Use the **"Browse Output Folder"** button to select where to save filtered results.
- Select from available filters or choose to apply all at once.
- Adjust filter intensity using the slider.
- Click **Apply** to begin processing.
- Processed images will be saved with their filter name in the filename (e.g., `output-AdversarialWaveFilter.jpg`).

---

## Programming Techniques Used
This project demonstrates the following required programming techniques:

- Loops (image traversal and batch operations)
- Methods (filter logic, utility functions)
- Classes (modular architecture for filters and processing)
- Inheritance (`IImageFilter` interface for extensibility)
- Strings and Lists (image path handling, log messages)
- Model-View-Controller (separation of core and UI layers)
- Exception Handling (logging failures during processing)
- LINQ (optional in batch operations and file filtering)

---

## Runtime & Testing

- The application compiles and runs without any runtime errors.
- All features are thoroughly tested through console tests and UI validation.
- Test outputs are documented in the console and through saved images.
- Batch processing and individual filter tests have been completed successfully.

---

## Documentation

- This README provides complete project context, usage, and technical architecture.
- All core C# files include inline comments to explain functionality.
- Code structure follows clean modular conventions with meaningful class and method names.

---

## Graphical User Interface (GUI)

- The WPF UI is fully functional and responsive.
- Components are laid out in a clean, logical, and professional manner.
- Filters are applied interactively, and the UI provides feedback on all actions.
- Design follows a minimal aesthetic with clear alignment, spacing, and usability in mind.

---

## Acknowledgments

This project was inspired by academic research from the University of Chicago on adversarial privacy, specifically:

- [Fawkes](https://sandlab.cs.uchicago.edu/fawkes/) – a tool to cloak images against facial recognition
- [Nightshade](https://nightshade.cs.uchicago.edu/) – a dataset poisoning method to disrupt AI training (conceptual inspiration only)

---

## License

This project is licensed under the MIT License. See the LICENSE file for details.
