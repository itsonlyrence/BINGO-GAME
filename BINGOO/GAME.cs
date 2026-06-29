using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BINGO
{
    public partial class GAME : Form
    {
        private List<int> selectedHistory = new List<int>();
        private bool[,] patternGrid = new bool[5, 5];
        private Dictionary<string, bool[,]> loadedPatternsMap;

        public GAME()
        {
            InitializeComponent();

            WipeGhostDesignerControls();

            BuildLeftSidebarLayout();
            BuildBottomTrayDashboard();
            BuildBingoGridDesign();

            AttachFunctionalEvents();
            this.AutoScroll = true;

            InitializePatternDataOnly();

        }

        private void GAME_Load(object sender, EventArgs e)
        {
            SyncPatternDropdownUI();
            ExecuteFullSystemReset();
        }

        private void InitializePatternDataOnly()
        {
            loadedPatternsMap = PatternManager.LoadAllPatterns();

            if (!loadedPatternsMap.ContainsKey("[None]"))
            {
                loadedPatternsMap["[None]"] = new bool[5, 5];
            }

            if (!loadedPatternsMap.ContainsKey("[Custom Workspace]"))
            {
                loadedPatternsMap["[Custom Workspace]"] = new bool[5, 5];
            }
        }

        private void SyncPatternDropdownUI()
        {
            // Temporarily unhook layout event handlers to dodge cycle bugs while inserting items
            cmbSavedPatterns.SelectedIndexChanged -= cmbSavedPatterns_SelectedIndexChanged;

            cmbSavedPatterns.Items.Clear();
            cmbSavedPatterns.Items.Add("None");
            cmbSavedPatterns.Items.Add("Custom Workspace");

            foreach (string name in loadedPatternsMap.Keys)
            {
                if (name == "[Custom Workspace]" || name == "[None]") continue;
                cmbSavedPatterns.Items.Add(name);
            }

            cmbSavedPatterns.SelectedIndex = 0; // Modern default behavior: Highlights "None" first

            cmbSavedPatterns.SelectedIndexChanged += cmbSavedPatterns_SelectedIndexChanged;
        }

        private void cmbSavedPatterns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSavedPatterns.SelectedItem == null) return;

            // Map the modern UI display strings back to internal tracking bracket notation keys
            string displayItem = cmbSavedPatterns.SelectedItem.ToString();
            string selectedKey = displayItem;
            if (displayItem == "None") selectedKey = "[None]";
            else if (displayItem == "Custom Workspace") selectedKey = "[Custom Workspace]";

            // UI Visibility Rules based on modernized selections
            if (selectedKey == "[Custom Workspace]")
            {
                txtPatternName.Visible = true;
                btnSavePattern.Visible = true;
                btnDeletePattern.Enabled = false;
            }
            else if (selectedKey == "[None]")
            {
                txtPatternName.Visible = false;
                btnSavePattern.Visible = false;
                btnDeletePattern.Enabled = false; // Strictly blocked from deletion
            }
            else
            {
                txtPatternName.Visible = false;
                btnSavePattern.Visible = false;
                btnDeletePattern.Enabled = true;
            }

            if (loadedPatternsMap.ContainsKey(selectedKey))
            {
                bool[,] targetedPattern = loadedPatternsMap[selectedKey];
                Array.Copy(targetedPattern, patternGrid, targetedPattern.Length);

                if (this.matrixGridEngine != null)
                {
                    int gridCellIndex = 0;
                    foreach (Control ctrl in this.matrixGridEngine.Controls)
                    {
                        if (ctrl is CheckBox chkCell)
                        {
                            int r = gridCellIndex / 5;
                            int c = gridCellIndex % 5;

                            chkCell.Checked = patternGrid[r, c];
                            chkCell.Invalidate();

                            gridCellIndex++;
                            if (gridCellIndex >= 25) break;
                        }
                    }
                }
            }

            // Automatically wipe called numbers baseline history when a board is changed
            selectedHistory.Clear();
            UpdateTopDashboardDisplay();
        }

        private void btnSavePattern_Click(object sender, EventArgs e)
        {
            string enteredName = txtPatternName.Text.Trim();

            if (string.IsNullOrEmpty(enteredName) || enteredName == "Custom Pattern Name")
            {
                MessageBox.Show("Please type an identification name for your custom pattern layout workspace.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Guard against users trying to overwrite reserved system tracking strings
            if (enteredName.Equals("None", StringComparison.OrdinalIgnoreCase) ||
                enteredName.Equals("Custom Workspace", StringComparison.OrdinalIgnoreCase) ||
                enteredName.StartsWith("["))
            {
                MessageBox.Show("That name is reserved for system layout definitions. Please pick another unique pattern name.", "Name Reserved", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PatternManager.SavePattern(enteredName, patternGrid);

            InitializePatternDataOnly();
            SyncPatternDropdownUI();

            txtPatternName.Text = "Custom Pattern Name";
            txtPatternName.Font = new Font("Arial", 10, FontStyle.Italic);
            txtPatternName.ForeColor = Color.Gray;

            cmbSavedPatterns.SelectedItem = enteredName;

            MessageBox.Show($"Pattern alignment '{enteredName}' stored successfully!", "Storage Engine", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDeletePattern_Click(object sender, EventArgs e)
        {
            if (cmbSavedPatterns.SelectedItem == null) return;
            string targetToDelete = cmbSavedPatterns.SelectedItem.ToString();

            // Double safety barrier protecting Core Layout structures
            if (targetToDelete == "None" || targetToDelete == "Custom Workspace" ||
                targetToDelete == "[None]" || targetToDelete == "[Custom Workspace]")
            {
                return;
            }

            var confirmResult = MessageBox.Show($"Are you sure you want to permanently delete the pattern layout '{targetToDelete}'?", "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                PatternManager.DeletePattern(targetToDelete);
                InitializePatternDataOnly();
                SyncPatternDropdownUI();
                ExecuteFullSystemReset();
                MessageBox.Show("Pattern layout removed from disk configuration.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ExecuteFullSystemReset();
        }

        /// <summary>
        /// Clears called history while restoring template defaults to None.
        /// </summary>
        private void ExecuteFullSystemReset()
        {
            // 1. Flush game call loops (Now: None Selected)
            selectedHistory.Clear();

            // 2. Safely swap dropdown active item index back to modern "None" (Index 0)
            if (cmbSavedPatterns.Items.Count > 0 && cmbSavedPatterns.Items[0].ToString() == "None")
            {
                if (cmbSavedPatterns.SelectedIndex == 0)
                {
                    // Force refresh grid states if already resting at None position index
                    cmbSavedPatterns_SelectedIndexChanged(cmbSavedPatterns, EventArgs.Empty);
                }
                else
                {
                    cmbSavedPatterns.SelectedIndex = 0;
                }
            }
            else
            {
                // Fallback safe cleanup if items context index collection missing entirely
                Array.Clear(patternGrid, 0, patternGrid.Length);
                if (this.matrixGridEngine != null)
                {
                    foreach (Control ctrl in this.matrixGridEngine.Controls)
                    {
                        if (ctrl is CheckBox chkCell)
                        {
                            chkCell.Checked = false;
                            chkCell.Invalidate();
                        }
                    }
                }
            }

            // 3. Redraw screen layout items (-- and 00 states)
            UpdateTopDashboardDisplay();
        }

        private void WipeGhostDesignerControls()
        {
            List<Control> toRemove = new List<Control>();
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl != masterScreenLayout)
                {
                    toRemove.Add(ctrl);
                }
            }

            foreach (Control ctrl in toRemove)
            {
                this.Controls.Remove(ctrl);
                ctrl.Dispose();
            }
        }

        public bool IsNumberSelected(int number)
        {
            return selectedHistory.Contains(number);
        }

        private void AttachFunctionalEvents()
        {
            foreach (Button btn in numberButtons)
            {
                btn.Click += (sender, e) =>
                {
                    Button clickedButton = (Button)sender;
                    int rolledNumber = (int)clickedButton.Tag;

                    if (selectedHistory.Contains(rolledNumber))
                        selectedHistory.Remove(rolledNumber);
                    else
                        selectedHistory.Add(rolledNumber);

                    clickedButton.Invalidate();
                    UpdateTopDashboardDisplay();
                };
            }
        }

        private void UpdateTopDashboardDisplay()
        {
            if (lblTotalCallsCount != null)
            {
                lblTotalCallsCount.Text = selectedHistory.Count.ToString("D2");
            }

            if (lblCurrentBigBall != null)
            {
                if (selectedHistory.Count == 0)
                {
                    lblCurrentBigBall.Text = "--";
                }
                else
                {
                    int currentNum = selectedHistory[selectedHistory.Count - 1];
                    string letter = "BINGO"[(currentNum - 1) / 15].ToString();
                    lblCurrentBigBall.Text = $"{letter} {currentNum}";
                }
                lblCurrentBigBall.Invalidate();
            }

            for (int i = 0; i < 4; i++)
            {
                if (i >= historyBallLabels.Count) break;

                int historyIndex = selectedHistory.Count - 5 + i;
                if (historyIndex >= 0 && historyIndex < selectedHistory.Count - 1)
                {
                    int num = selectedHistory[historyIndex];
                    string letter = "BINGO"[(num - 1) / 15].ToString();
                    historyBallLabels[i].Text = $"{letter}\n{num}";
                }
                else
                {
                    historyBallLabels[i].Text = "";
                }
                historyBallLabels[i].Invalidate();
            }

            if (gridContainer != null) gridContainer.Invalidate(true);
        }
    }
}