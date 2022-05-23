using PuzzleSolver.ApplicationLogic;
using PuzzleSolver.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PuzzleSolver
{
    public partial class MainWindow : Form
    {
        internal int _populationCapacity;
        internal decimal _elitismQuotient;
        internal decimal _mutationProbability;
        internal double _diversityQuotient;
        internal decimal _crossoverProbability;
        internal int _tournamentSize;
        internal int _maxIterations;
        internal int _originallySetPositions;

        internal List<char> _solution;

        public List<char> PuzzleEntryFieldValues {get; set;}
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowSolution(List<char> solution)
        {
            var solutionStatus = SolutionStatus.Found;

            if (solution == null)
            {
                solutionStatus = SolutionStatus.InProgress;
            }
            else if (solution.Count == 0)
            {
                solutionStatus = SolutionStatus.NotFound;
            }
           
            for (var i = 0; ++i < 17;)
            {
                var solutionField = Controls["puzzleGroupBox"].Controls["puzzleSolutionGroupBox"].Controls[$"puzzleSolutionField{i}"] as RichTextBox;
                if (solutionStatus == SolutionStatus.Found)
                {
                    solutionField.ForeColor = System.Drawing.Color.LimeGreen;
                    solutionField.Text = solution[i - 1].ToString();
                }
                else if (solutionStatus == SolutionStatus.NotFound)
                {
                    solutionField.ForeColor = System.Drawing.Color.Red;
                    solutionField.Text = "X";
                }
                else
                {
                    solutionField.ForeColor = System.Drawing.Color.OrangeRed;
                    solutionField.Text = "?";
                }
            } 
        }
        
        private int GetRankingNumber(string controlName)
        {
            int.TryParse(Regex.Replace(controlName, "[A-Za-z]", string.Empty), out var rankingNumber);
            return rankingNumber;
        }

        #region Event Handlers
        private void MainWindow_Load(object sender, EventArgs e)
        {
            PuzzleEntryFieldValues = new List<char>();
            Enumerable.Range(1, 16).ToList().ForEach(r => PuzzleEntryFieldValues.Add('\0'));
        }

        private void PuzzleEntryField_TextChanged(object sender, EventArgs e)
        {
            var puzzleEntryField = sender as RichTextBox;
            var puzzleEntryFieldLetter = !string.IsNullOrEmpty(puzzleEntryField.Text) ? puzzleEntryField.Text.First() : '\0';
            PuzzleEntryFieldValues[GetRankingNumber(puzzleEntryField.Name) - 1] = puzzleEntryFieldLetter;
            if (PuzzleEntryFieldValues.Distinct().Count() != 5)
            {
                solvePuzzleButton.Enabled = false;
                return;
            }
            if (!Enumerable.Range(1, 15).Contains(PuzzleEntryFieldValues.Count(r => r == '\0')))
            {
                solvePuzzleButton.Enabled = false;
                return;
            }
            solvePuzzleButton.Enabled = true;
        }

        private void PuzzleEntryField_KeyPress(object sender, KeyPressEventArgs e)
        {
            var puzzleEntryField = sender as RichTextBox;
            if (e.KeyChar != '\b')
            {
                if (char.IsLetter(e.KeyChar))
                    puzzleEntryField.Text = e.KeyChar.ToString().ToUpper();
                e.Handled = true;
            }
        }

        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            var currentEntryField = sender as NumericUpDown;
            if (currentEntryField.Value > currentEntryField.Maximum)
            {
                currentEntryField.Value = currentEntryField.Maximum;
            }
        }

        private void SolvePuzzleButton_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker.IsBusy)
            {
                solvePuzzleButton.Enabled = false;
                ShowSolution(null);
                _populationCapacity = Convert.ToInt32(populationCapacityEntryField.Value);
                _maxIterations = Convert.ToInt32(maximalIterationsEntryField.Value);
                _elitismQuotient = elitismQuotientEntryField.Value / 100;
                _tournamentSize = Convert.ToInt32(tournamentSizeEntryField.Value);
                _crossoverProbability = crossoverProbabilityEntryField.Value / 100;
                _mutationProbability = mutationProbabilityEntryField.Value / 100;
                Console.Clear();
                backgroundWorker.RunWorkerAsync();
            }
        }

        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var geneticAlgorithm = new GeneticAlgorithm(_populationCapacity, _elitismQuotient, _mutationProbability, 0, _crossoverProbability, _tournamentSize, _maxIterations);
            _solution = geneticAlgorithm.Solve(PuzzleEntryFieldValues);
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            solvePuzzleButton.Enabled = true;
            ShowSolution(_solution);
        }
        #endregion
    }
}
