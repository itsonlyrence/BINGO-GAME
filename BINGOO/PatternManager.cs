using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace BINGO
{
    public static class PatternManager
    {
        private static readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "patterns.txt");

        public static Dictionary<string, bool[,]> LoadAllPatterns()
        {
            var patternDictionary = new Dictionary<string, bool[,]>();

            if (!File.Exists(filePath))
            {
                WriteDefaultPatternsFile();
            }

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line) || !line.Contains("=")) continue;

                    string[] pieces = line.Split('=');
                    string name = pieces[0].Trim();
                    string matrixString = pieces[1].Trim();

                    if (matrixString.Length == 25)
                    {
                        bool[,] grid = new bool[5, 5];
                        int stringIndex = 0;
                        for (int r = 0; r < 5; r++)
                        {
                            for (int c = 0; c < 5; c++)
                            {
                                grid[r, c] = (matrixString[stringIndex++] == '1');
                            }
                        }
                        patternDictionary[name] = grid;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load configuration matrices: {ex.Message}");
            }

            return patternDictionary;
        }

        public static void SavePattern(string name, bool[,] grid)
        {
            try
            {
                var allLines = new List<string>();
                if (File.Exists(filePath))
                {
                    allLines.AddRange(File.ReadAllLines(filePath));
                }

                char[] states = new char[25];
                int idx = 0;
                for (int r = 0; r < 5; r++)
                {
                    for (int c = 0; c < 5; c++)
                    {
                        states[idx++] = grid[r, c] ? '1' : '0';
                    }
                }
                string matrixString = new string(states);
                string entryLine = $"{name}={matrixString}";

                int existingIndex = -1;
                for (int i = 0; i < allLines.Count; i++)
                {
                    if (allLines[i].StartsWith(name + "="))
                    {
                        existingIndex = i;
                        break;
                    }
                }

                if (existingIndex >= 0)
                {
                    allLines[existingIndex] = entryLine;
                }
                else
                {
                    allLines.Add(entryLine);
                }

                File.WriteAllLines(filePath, allLines);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to commit matrix data to storage: {ex.Message}");
            }
        }

        public static void DeletePattern(string name)
        {
            try
            {
                if (!File.Exists(filePath)) return;

                string[] lines = File.ReadAllLines(filePath);
                var preservedLines = new List<string>();

                foreach (string line in lines)
                {
                    if (!line.StartsWith(name + "="))
                    {
                        preservedLines.Add(line);
                    }
                }

                File.WriteAllLines(filePath, preservedLines);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to remove pattern from disk: {ex.Message}");
            }
        }

        private static void WriteDefaultPatternsFile()
        {
            try
            {
                string[] defaults = {
                    "[Custom Workspace]=0000000000000000000000000",
                    "Blackout=1111111111111111111111111",
                    "Pattern L=1000010000100001000011111",
                    "Pattern M=1000111011101011000110001",
                    "Pattern G=1111110000101111000111111",
                    "Pattern H=1000110001111111000110001",
                    "Pattern D=1111010001100011000111110",
                    "Pattern C=1111110000100001000011111"
                };
                File.WriteAllLines(filePath, defaults);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to generate initial default system patterns: {ex.Message}");
            }
        }
    }
}