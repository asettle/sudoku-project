using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PuzzleSolver
{
    public partial class MainWindow : Form
    {
        public List<char> PuzzleEntryFieldValues {get; set;}

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowSolution(List<char> solutions)
        {
            if (solutions == null || solutions.Count != 16) return; 
            for (var i = 0; ++i < 17;)
            {
                var solutionField = Controls["puzzleGroupBox"].Controls["puzzleSolutionGroupBox"].Controls[$"puzzleSolutionField{i}"] as RichTextBox;
                solutionField.Text = solutions[i - 1].ToString();
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
            // ToDo: Temporary initialization
            ShowSolution(new List<char> { 'E', 'C', 'H', 'O', 'H', 'O', 'E', 'C', 'C', 'H', 'O', 'E', 'O', 'E', 'C', 'H' });
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
        #endregion    
    }
}
