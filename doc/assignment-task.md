# Task

Create a software application that solves a puzzle game similar to Sudoku using genetic algorithms.
The objective is to fill a grid of size 4x4 with four different letters so that each column, each row,
and each of the four sub-grids of size 2x2 contain each of the letters only once

As an example, this is a successfully solved grid:

W 	D 	R 	O

O 	R 	W 	D

R 	O 	D 	W

D 	W 	O 	R

At the start of the game, a partially completed grid is provided by the user. Depending on
the initial configuration, the puzzle might have no solution, one single solution or more
than one solution. You decide the initial placement on the grid.

The conditions for letter placement on the 4x4 grid are as follows:

* the four letters are all different from each other; you decide what letters to use as
  long as they are all different
* the same single letter may not appear twice in the same row, column, or any of the four
  subgrids of size 2x2
