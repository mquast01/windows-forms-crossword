# CrossWord

CrossWord is a C# Windows Forms application that helps create, get, and provide a GUI for solving crosswords.

## Dependencies
Requires:
Microsoft.AspNet.WebApi.Client (5.2.8)
Newtonsoft.Json (13.0.1)

## Usage
Separated into three forms
1. Open a crossword saved locally on your device. Provides a layout to edit cells and check for the correct answers.

* takes json

2. Create a crossword given specific parameters. Enter specific dimensions and use it to build your own crossword.

* struggles to deal with dimensions that are not a square. I must have messed up the indexing somewhere

* use periods to represent the black squares used in a regular crossword.
3. Open a new crossword taken from a GitHub repository hosting most crosswords from the NYT crossword puzzles.