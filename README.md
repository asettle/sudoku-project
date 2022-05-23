# Sudoku Group Project

This project emulates the production of a genetic software system.
The purpose of this assignment was to create a software application
that solves a puzzle game similar to Sudoku using genetic algorithms. 

For more information on the task at hand, see `assignment-task.md`.

## How do I get started?

The implementation of this project was done using Windows Forms is
which is Windows-native. This means that this project is not compatible
with Unix-based systems, such as Linux or Mac. The implementation of the 
algorithmic engine was inspired by the SudokuGA project (https://github.com/erwin-beckers/SudokuGA).

To understand the program and how it runs, see the `/.pseudocode/` file
in the `/doc/` folder for the pseudo code implementation.

Extract the PuzzleSolver.zip File from the `/bin/` direcotry.
Download the puzzle solver zip file and extract it into your preferred directory. 


Once you've downloaded the program, run the `.exe` file.
The GUI is an easy-to-use genetic algorithm solver compiled for the Windows
operating system only, in other to use the GUI see the steps below: 

1. Run PuzzleSolver.exe: Open the extracted folder and double click on the
   PuzzleSolver.exe file to run the GUI application, this will open a GUI
   interface and a command-line tool. The GUI interface is for inputting
   computational parameters and seeing the output result, while the command-line
   will show the working steps. 
   
2. Set the Puzzle Solver Settings: The puzzle solver settings comprise 7
   input variables. 

- Population Capacity: A non-zero Integer with a maximum value of 999. 
- Elitism Quotient: A number from 0 - 100 in percentage. 
- Mutation Probability: A number from 0 - 100 in percentage. 
- Diversity Quotient: A number from 0 - 100 in percentage. 
- Crossover Probability: A is a number from 0 - 100 in percentage. 
- Tournament Size: A non-zero Integer with a Maximum Value of 100. 
- Maximal Iterations:  A non-zero Integer with a Maximum Value of 9999. 

4. Set the Puzzle to the Solved: The puzzle section contains 4 by 4 grid
   boxes, that must have 4 unique/distinct letters in other for it to be
   solvable. The system is designed to disable the “Solve!” button if the
   unique letters are less than or greater than 4. 

5. Click on Solve: If steps I – iv are executed correctly, the “Solve!” button
   will be visible and compute the answers based on the designed genetic algorithm. 

6. Check Output Steps: The command line will show the computed steps based on the
   generation until the termination criteria have been met. 

7. Check Solution Grid: The GUI solution section will display the result gotten
   from the best fit solution in green letters. If the system does not have an
   accurate answer the solution boxes will be replaced with an “X” letter all
   through, this represents a failure to find the solution.  

You may have to press the solve button multiple times to get an answer. 


## Contributing

I have defined some basic contributing guidelines in the `contributing.md`
in `/doc/`. Please see for more details.

Do not submit directly to the master branch under any circumstances.
Your pull request will be reverted in this case.

## License

Copyright 2021 Group A

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
