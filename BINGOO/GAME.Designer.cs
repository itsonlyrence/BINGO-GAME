using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BINGO
{
    partial class GAME
    {
        private System.ComponentModel.IContainer components = null;

        private TableLayoutPanel masterScreenLayout;
        private TableLayoutPanel rightContentSplitter;
        private TableLayoutPanel bingoGridEngine;
        private TableLayoutPanel sidebarStack;
        private TableLayoutPanel matrixGridEngine;

        private Panel leftSidebarPanel;
        private Panel topDisplayPanel;
        private Panel gridContainer;

        private Label lblTotalCallsCount;
        private Label lblCurrentBigBall;

        private ComboBox cmbSavedPatterns;
        private TextBox txtPatternName;
        private Button btnSavePattern;
        private Button btnDeletePattern;

        private List<Label> historyBallLabels = new List<Label>();
        private List<Button> numberButtons = new List<Button>();

        // Refined Hospital Branding Color Palette (Matching image_8ed04a.png)
        private readonly Color logoDarkTeal = Color.FromArgb(12, 38, 37);
        private readonly Color logoHospitalBlue = Color.FromArgb(20, 60, 58);
        private readonly Color logoBrightGreen = Color.FromArgb(57, 181, 74);
        private readonly Color logoPureWhite = Color.White;

        // Custom High-Contrast Text Settings for the White Grid Panel
        private readonly Color textUnselectedColor = Color.FromArgb(12, 38, 37); // Dark Teal Text on White
        private readonly Color textSelectedColor = Color.White;                   // White Text inside Green Pop Circle
        private readonly Color sidebarLightText = Color.FromArgb(240, 245, 245);

        private Image logoBackground = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null) components.Dispose();
                if (logoBackground != null) logoBackground.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GAME));
            masterScreenLayout = new TableLayoutPanel();
            leftSidebarPanel = new Panel();
            rightContentSplitter = new TableLayoutPanel();
            gridContainer = new Panel();
            topDisplayPanel = new Panel();
            masterScreenLayout.SuspendLayout();
            rightContentSplitter.SuspendLayout();
            SuspendLayout();
            // 
            // masterScreenLayout
            // 
            masterScreenLayout.ColumnCount = 2;
            masterScreenLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 260F));
            masterScreenLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            masterScreenLayout.Controls.Add(leftSidebarPanel, 0, 0);
            masterScreenLayout.Controls.Add(rightContentSplitter, 1, 0);
            masterScreenLayout.Dock = DockStyle.Fill;
            masterScreenLayout.Location = new Point(0, 0);
            masterScreenLayout.Name = "masterScreenLayout";
            masterScreenLayout.RowCount = 1;
            masterScreenLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            masterScreenLayout.Size = new Size(1680, 995);
            masterScreenLayout.TabIndex = 0;
            // 
            // leftSidebarPanel
            // 
            leftSidebarPanel.BackColor = logoDarkTeal;
            leftSidebarPanel.Dock = DockStyle.Fill;
            leftSidebarPanel.Location = new Point(0, 0);
            leftSidebarPanel.Name = "leftSidebarPanel";
            leftSidebarPanel.Size = new Size(260, 995);
            leftSidebarPanel.TabIndex = 0;
            // 
            // rightContentSplitter
            // 
            rightContentSplitter.BackColor = logoDarkTeal;
            rightContentSplitter.ColumnCount = 1;
            rightContentSplitter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rightContentSplitter.Controls.Add(gridContainer, 0, 0);
            rightContentSplitter.Controls.Add(topDisplayPanel, 0, 1);
            rightContentSplitter.Dock = DockStyle.Fill;
            rightContentSplitter.Location = new Point(260, 0);
            rightContentSplitter.Name = "rightContentSplitter";
            rightContentSplitter.Padding = new Padding(15);
            rightContentSplitter.RowCount = 2;
            rightContentSplitter.RowStyles.Add(new RowStyle(SizeType.Percent, 74F));
            rightContentSplitter.RowStyles.Add(new RowStyle(SizeType.Percent, 26F));
            rightContentSplitter.Size = new Size(1420, 995);
            rightContentSplitter.TabIndex = 1;
            // 
            // gridContainer
            // 
            gridContainer.BackColor = logoPureWhite; // Explicit White Board Background
            gridContainer.Dock = DockStyle.Fill;
            gridContainer.Location = new Point(18, 18);
            gridContainer.Name = "gridContainer";
            gridContainer.Size = new Size(1384, 699);
            gridContainer.TabIndex = 0;
            // 
            // topDisplayPanel
            // 
            topDisplayPanel.BackColor = logoHospitalBlue;
            topDisplayPanel.Dock = DockStyle.Fill;
            topDisplayPanel.Location = new Point(15, 735);
            topDisplayPanel.Margin = new Padding(0, 15, 0, 0);
            topDisplayPanel.Name = "topDisplayPanel";
            topDisplayPanel.Size = new Size(1390, 230);
            topDisplayPanel.TabIndex = 1;
            // 
            // GAME
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = logoDarkTeal;
            ClientSize = new Size(1680, 995);
            Controls.Add(masterScreenLayout);

            // Safe System Icon Injection Handler
            try
            {
                string absoluteIconPath = @"C:\Users\AMOU\Desktop\BINGOO\BINGOO\APP_LOGO_BINGO_LMGHDC.ico";
                if (System.IO.File.Exists(absoluteIconPath))
                {
                    this.Icon = new Icon(absoluteIconPath);
                }
                else if (System.IO.File.Exists("APP_LOGO_BINGO_LMGHDC.ico"))
                {
                    this.Icon = new Icon("APP_LOGO_BINGO_LMGHDC.ico");
                }
            }
            catch { }

            Name = "GAME";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LMGHDC Premium Bingo System";
            WindowState = FormWindowState.Maximized;
            Load += GAME_Load;
            masterScreenLayout.ResumeLayout(false);
            rightContentSplitter.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void BuildLeftSidebarLayout()
        {
            leftSidebarPanel.Controls.Clear();

            this.sidebarStack = new TableLayoutPanel();
            this.sidebarStack.Dock = DockStyle.Fill;
            this.sidebarStack.ColumnCount = 1;
            this.sidebarStack.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.sidebarStack.Padding = new Padding(15);

            this.sidebarStack.RowCount = 9;
            this.sidebarStack.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            this.sidebarStack.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            this.sidebarStack.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            this.sidebarStack.RowStyles.Add(new RowStyle(SizeType.Absolute, 220F));
            this.sidebarStack.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            this.sidebarStack.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            this.sidebarStack.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            this.sidebarStack.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.sidebarStack.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            leftSidebarPanel.Controls.Add(this.sidebarStack);

            // 1. Branding Header
            Label lblLogo = new Label();
            lblLogo.Text = "LMGHDC\nBINGO!";
            lblLogo.Font = new Font("Arial Black", 20, FontStyle.Bold | FontStyle.Italic);
            lblLogo.ForeColor = logoBrightGreen;
            lblLogo.Dock = DockStyle.Fill;
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            this.sidebarStack.Controls.Add(lblLogo, 0, 0);

            // 2. Call Counter Panel
            Panel pnlTotalCalls = new Panel();
            pnlTotalCalls.Dock = DockStyle.Fill;
            pnlTotalCalls.BackColor = logoHospitalBlue;
            pnlTotalCalls.Padding = new Padding(5);
            this.sidebarStack.Controls.Add(pnlTotalCalls, 0, 1);

            lblTotalCallsCount = new Label();
            lblTotalCallsCount.Text = "00";
            lblTotalCallsCount.Font = new Font("Impact", 32, FontStyle.Regular);
            lblTotalCallsCount.ForeColor = logoBrightGreen;
            lblTotalCallsCount.Dock = DockStyle.Fill;
            lblTotalCallsCount.TextAlign = ContentAlignment.MiddleCenter;
            pnlTotalCalls.Controls.Add(lblTotalCallsCount);

            Label lblTotalCallsTitle = new Label();
            lblTotalCallsTitle.Text = "TOTAL CALLS";
            lblTotalCallsTitle.Font = new Font("Arial Black", 8, FontStyle.Bold);
            lblTotalCallsTitle.ForeColor = sidebarLightText;
            lblTotalCallsTitle.Dock = DockStyle.Bottom;
            lblTotalCallsTitle.Height = 18;
            lblTotalCallsTitle.TextAlign = ContentAlignment.MiddleCenter;
            pnlTotalCalls.Controls.Add(lblTotalCallsTitle);

            // 3. Section Title
            Label lblPatternTitle = new Label();
            lblPatternTitle.Text = "PATTERN";
            lblPatternTitle.Font = new Font("Arial Black", 9, FontStyle.Bold);
            lblPatternTitle.ForeColor = logoPureWhite;
            lblPatternTitle.Dock = DockStyle.Fill;
            lblPatternTitle.TextAlign = ContentAlignment.BottomCenter;
            this.sidebarStack.Controls.Add(lblPatternTitle, 0, 2);

            // 4. Matrix Canvas
            Panel pnlPatternMatrix = new Panel();
            pnlPatternMatrix.Size = new Size(210, 210);
            pnlPatternMatrix.Anchor = AnchorStyles.Top | AnchorStyles.None;
            pnlPatternMatrix.BackColor = logoHospitalBlue;
            this.sidebarStack.Controls.Add(pnlPatternMatrix, 0, 3);

            this.matrixGridEngine = new TableLayoutPanel();
            this.matrixGridEngine.Dock = DockStyle.Fill;
            this.matrixGridEngine.Padding = new Padding(8);
            this.matrixGridEngine.RowCount = 6;
            this.matrixGridEngine.ColumnCount = 5;

            for (int i = 0; i < 5; i++)
                this.matrixGridEngine.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));

            this.matrixGridEngine.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
            for (int i = 0; i < 5; i++)
                this.matrixGridEngine.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));

            pnlPatternMatrix.Controls.Add(this.matrixGridEngine);

            string[] bngo = { "B", "I", "N", "G", "O" };
            for (int c = 0; c < 5; c++)
            {
                Label lblColHeader = new Label();
                lblColHeader.Text = bngo[c];
                lblColHeader.Font = new Font("Arial Black", 11, FontStyle.Bold);
                lblColHeader.ForeColor = logoBrightGreen;
                lblColHeader.Dock = DockStyle.Fill;
                lblColHeader.Margin = new Padding(0);
                lblColHeader.TextAlign = ContentAlignment.MiddleCenter;
                this.matrixGridEngine.Controls.Add(lblColHeader, c, 0);
            }

            int circleDiameter = 22;
            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 5; c++)
                {
                    CheckBox chkCell = new CheckBox();
                    chkCell.Size = new Size(circleDiameter, circleDiameter);
                    chkCell.Anchor = AnchorStyles.None;
                    chkCell.Margin = new Padding(0);
                    chkCell.Appearance = Appearance.Button;
                    chkCell.FlatStyle = FlatStyle.Flat;
                    chkCell.FlatAppearance.BorderSize = 0;
                    chkCell.FlatAppearance.MouseDownBackColor = Color.Transparent;
                    chkCell.FlatAppearance.MouseOverBackColor = Color.Transparent;
                    chkCell.BackColor = Color.Transparent;

                    int rowIdx = r, colIdx = c;

                    chkCell.CheckedChanged += (s, e) =>
                    {
                        patternGrid[rowIdx, colIdx] = chkCell.Checked;
                        chkCell.Invalidate();
                    };

                    chkCell.Paint += (s, pe) =>
                    {
                        CheckBox box = (CheckBox)s;
                        pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                        Color currentBg = box.Checked ? logoBrightGreen : Color.FromArgb(14, 44, 42);
                        Color currentBorder = box.Checked ? logoBrightGreen : Color.FromArgb(40, 95, 92);

                        using (SolidBrush brush = new SolidBrush(currentBg))
                        using (Pen pen = new Pen(currentBorder, 1.5f))
                        {
                            pe.Graphics.Clear(pnlPatternMatrix.BackColor);
                            Rectangle circleBounds = new Rectangle(1, 1, box.Width - 3, box.Height - 3);
                            pe.Graphics.FillEllipse(brush, circleBounds);
                            pe.Graphics.DrawEllipse(pen, circleBounds);
                        }
                    };

                    this.matrixGridEngine.Controls.Add(chkCell, c, r + 1);
                }
            }

            // 5. Shared Dropdown Controls
            TableLayoutPanel dropDownRowContainer = new TableLayoutPanel();
            dropDownRowContainer.Dock = DockStyle.Fill;
            dropDownRowContainer.Margin = new Padding(0);
            dropDownRowContainer.ColumnCount = 2;
            dropDownRowContainer.RowCount = 1;
            dropDownRowContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            dropDownRowContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            dropDownRowContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.sidebarStack.Controls.Add(dropDownRowContainer, 0, 4);

            cmbSavedPatterns = new ComboBox();
            cmbSavedPatterns.Dock = DockStyle.Fill;
            cmbSavedPatterns.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSavedPatterns.BackColor = logoPureWhite;
            cmbSavedPatterns.ForeColor = logoDarkTeal;
            cmbSavedPatterns.Font = new Font("Arial", 10, FontStyle.Regular);
            cmbSavedPatterns.SelectedIndexChanged += new EventHandler(this.cmbSavedPatterns_SelectedIndexChanged);
            dropDownRowContainer.Controls.Add(cmbSavedPatterns, 0, 0);

            btnDeletePattern = new Button();
            btnDeletePattern.Text = "DEL";
            btnDeletePattern.Font = new Font("Arial Black", 8F, FontStyle.Bold);
            btnDeletePattern.ForeColor = logoPureWhite;
            btnDeletePattern.BackColor = Color.FromArgb(176, 58, 46);
            btnDeletePattern.Dock = DockStyle.Fill;
            btnDeletePattern.Margin = new Padding(4, 0, 0, 2);
            btnDeletePattern.FlatStyle = FlatStyle.Flat;
            btnDeletePattern.FlatAppearance.BorderSize = 0;
            btnDeletePattern.Click += new EventHandler(this.btnDeletePattern_Click);
            dropDownRowContainer.Controls.Add(btnDeletePattern, 1, 0);

            // 6. Name Textbox
            txtPatternName = new TextBox();
            txtPatternName.Dock = DockStyle.Fill;
            txtPatternName.Text = "Custom Pattern Name";
            txtPatternName.Font = new Font("Arial", 10, FontStyle.Italic);
            txtPatternName.BackColor = logoPureWhite;
            txtPatternName.ForeColor = Color.Gray;
            txtPatternName.Enter += (s, e) => { if (txtPatternName.Text == "Custom Pattern Name") { txtPatternName.Text = ""; txtPatternName.Font = new Font("Arial", 10, FontStyle.Regular); txtPatternName.ForeColor = Color.Black; } };
            txtPatternName.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txtPatternName.Text)) { txtPatternName.Text = "Custom Pattern Name"; txtPatternName.Font = new Font("Arial", 10, FontStyle.Italic); txtPatternName.ForeColor = Color.Gray; } };
            this.sidebarStack.Controls.Add(txtPatternName, 0, 5);

            // 7. Save Button
            btnSavePattern = new Button();
            btnSavePattern.Text = "SAVE PATTERN";
            btnSavePattern.Font = new Font("Arial Black", 9, FontStyle.Bold);
            btnSavePattern.ForeColor = logoPureWhite;
            btnSavePattern.BackColor = logoBrightGreen;
            btnSavePattern.Dock = DockStyle.Fill;
            btnSavePattern.FlatStyle = FlatStyle.Flat;
            btnSavePattern.FlatAppearance.BorderSize = 0;
            btnSavePattern.Click += new EventHandler(this.btnSavePattern_Click);
            this.sidebarStack.Controls.Add(btnSavePattern, 0, 6);

            // 8. Spacer Item
            Panel pnlSpacer = new Panel();
            pnlSpacer.Dock = DockStyle.Fill;
            pnlSpacer.BackColor = Color.Transparent;
            this.sidebarStack.Controls.Add(pnlSpacer, 0, 7);

            // 9. Board Reset Control
            Button btnReset = new Button();
            btnReset.Text = "RESET BOARD";
            btnReset.Font = new Font("Arial Black", 10, FontStyle.Bold);
            btnReset.ForeColor = logoPureWhite;
            btnReset.BackColor = Color.Transparent;
            btnReset.Dock = DockStyle.Bottom;
            btnReset.Height = 45;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.FlatAppearance.BorderColor = logoBrightGreen;
            btnReset.FlatAppearance.BorderSize = 1;
            btnReset.Click += new EventHandler(this.btnReset_Click);
            this.sidebarStack.Controls.Add(btnReset, 0, 8);
        }

        private void BuildBottomTrayDashboard()
        {
            topDisplayPanel.Controls.Clear();
            historyBallLabels.Clear();

            TableLayoutPanel bottomDashboardLayout = new TableLayoutPanel();
            bottomDashboardLayout.Dock = DockStyle.Fill;
            bottomDashboardLayout.Padding = new Padding(15);
            bottomDashboardLayout.ColumnCount = 2;
            bottomDashboardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220F));
            bottomDashboardLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            bottomDashboardLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            topDisplayPanel.Controls.Add(bottomDashboardLayout);

            Panel pnlCurrentWrap = new Panel();
            pnlCurrentWrap.Dock = DockStyle.Fill;
            bottomDashboardLayout.Controls.Add(pnlCurrentWrap, 0, 0);

            Panel pnlCurrentBox = new Panel();
            pnlCurrentBox.BackColor = logoBrightGreen;
            pnlCurrentBox.Dock = DockStyle.Top;
            pnlCurrentBox.Height = 120;
            pnlCurrentWrap.Controls.Add(pnlCurrentBox);

            lblCurrentBigBall = new Label();
            lblCurrentBigBall.Text = "--";
            lblCurrentBigBall.Font = new Font("Impact", 42, FontStyle.Regular);
            lblCurrentBigBall.ForeColor = logoDarkTeal;
            lblCurrentBigBall.Dock = DockStyle.Fill;
            lblCurrentBigBall.TextAlign = ContentAlignment.MiddleCenter;
            pnlCurrentBox.Controls.Add(lblCurrentBigBall);

            Label lblCurrentCallText = new Label();
            lblCurrentCallText.Text = "CURRENT CALL";
            lblCurrentCallText.ForeColor = sidebarLightText;
            lblCurrentCallText.Font = new Font("Arial Black", 9, FontStyle.Bold);
            lblCurrentCallText.Dock = DockStyle.Bottom;
            lblCurrentCallText.Height = 25;
            lblCurrentCallText.TextAlign = ContentAlignment.MiddleCenter;
            pnlCurrentWrap.Controls.Add(lblCurrentCallText);

            TableLayoutPanel historyGridEngine = new TableLayoutPanel();
            historyGridEngine.Dock = DockStyle.Fill;
            historyGridEngine.Margin = new Padding(25, 0, 0, 0);
            historyGridEngine.ColumnCount = 4;
            historyGridEngine.RowCount = 2;

            for (int i = 0; i < 4; i++)
                historyGridEngine.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

            historyGridEngine.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            historyGridEngine.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            bottomDashboardLayout.Controls.Add(historyGridEngine, 1, 0);

            for (int i = 0; i < 4; i++)
            {
                Label sqBox = new Label();
                sqBox.Dock = DockStyle.Fill;
                sqBox.Margin = new Padding(6);
                sqBox.Font = new Font("Impact", 22, FontStyle.Regular);
                sqBox.BackColor = logoDarkTeal;
                sqBox.ForeColor = logoBrightGreen;
                sqBox.TextAlign = ContentAlignment.MiddleCenter;
                sqBox.BorderStyle = BorderStyle.None;

                historyGridEngine.Controls.Add(sqBox, i, 0);
                historyBallLabels.Add(sqBox);
            }

            Label lblHistoryText = new Label();
            lblHistoryText.Text = "PREVIOUS CALL LOG HISTORY";
            lblHistoryText.ForeColor = sidebarLightText;
            lblHistoryText.Font = new Font("Arial Black", 9, FontStyle.Bold);
            lblHistoryText.Dock = DockStyle.Fill;
            lblHistoryText.TextAlign = ContentAlignment.MiddleLeft;
            historyGridEngine.Controls.Add(lblHistoryText, 0, 1);
            historyGridEngine.SetColumnSpan(lblHistoryText, 4);
        }

        private void BuildBingoGridDesign()
        {
            gridContainer.Controls.Clear();
            numberButtons.Clear();

            // Safe absolute desktop target retrieval of image logo data 
            try
            {
                string absoluteLogoPath = @"C:\Users\AMOU\Desktop\BINGOO\BINGOO\LMGHDC_LOGO.png";
                if (System.IO.File.Exists(absoluteLogoPath) && logoBackground == null)
                {
                    logoBackground = Image.FromFile(absoluteLogoPath);
                }
                else if (System.IO.File.Exists("LMGHDC_LOGO.png") && logoBackground == null)
                {
                    logoBackground = Image.FromFile("LMGHDC_LOGO.png");
                }
            }
            catch { }

            // Watermark background rendering handler
            this.gridContainer.Paint += (s, pe) =>
            {
                pe.Graphics.Clear(logoPureWhite);

                if (logoBackground != null)
                {
                    pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    pe.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    int minDimension = Math.Min(gridContainer.Width, gridContainer.Height);
                    int targetSize = (int)(minDimension * 0.78);

                    int posX = (gridContainer.Width - targetSize) / 2;
                    int posY = (gridContainer.Height - targetSize) / 2;

                    // 25% transparent alpha blending behind numbers matrix
                    float[][] colorMatrixElements = {
                        new float[] {1,  0,  0,  0,  0},
                        new float[] {0,  1,  0,  0,  0},
                        new float[] {0,  0,  1,  0,  0},
                        new float[] {0,  0,  0,  0.25f, 0},
                        new float[] {0,  0,  0,  0,  1}
                    };

                    ColorMatrix matrix = new ColorMatrix(colorMatrixElements);
                    using (ImageAttributes attributes = new ImageAttributes())
                    {
                        attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                        pe.Graphics.DrawImage(logoBackground,
                            new Rectangle(posX, posY, targetSize, targetSize),
                            0, 0, logoBackground.Width, logoBackground.Height,
                            GraphicsUnit.Pixel, attributes);
                    }
                }
            };

            this.bingoGridEngine = new TableLayoutPanel();
            this.bingoGridEngine.Dock = DockStyle.Fill;
            this.bingoGridEngine.Padding = new Padding(15);
            this.bingoGridEngine.ColumnCount = 16;
            this.bingoGridEngine.RowCount = 5;
            this.bingoGridEngine.BackColor = Color.Transparent;

            this.bingoGridEngine.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            for (int i = 0; i < 15; i++)
                this.bingoGridEngine.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 6.33F));

            for (int i = 0; i < 5; i++)
                this.bingoGridEngine.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));

            gridContainer.Controls.Add(this.bingoGridEngine);

            string[] letters = { "B", "I", "N", "G", "O" };
            for (int r = 0; r < 5; r++)
            {
                Label lblLetter = new Label();
                lblLetter.Text = letters[r];
                lblLetter.Font = new Font("Arial Black", 28, FontStyle.Bold);
                lblLetter.ForeColor = logoPureWhite;
                lblLetter.BackColor = logoDarkTeal;
                lblLetter.Dock = DockStyle.Fill;
                lblLetter.Margin = new Padding(3, 4, 12, 4);
                lblLetter.TextAlign = ContentAlignment.MiddleCenter;
                this.bingoGridEngine.Controls.Add(lblLetter, 0, r);
            }

            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 15; col++)
                {
                    int number = (row * 15) + col + 1;

                    Button numBtn = new Button();
                    numBtn.Dock = DockStyle.Fill;
                    numBtn.Margin = new Padding(2);

                    numBtn.FlatStyle = FlatStyle.Flat;
                    numBtn.FlatAppearance.BorderSize = 0;
                    numBtn.FlatAppearance.MouseDownBackColor = Color.Transparent;
                    numBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;
                    numBtn.BackColor = Color.Transparent;
                    numBtn.Font = new Font("Arial Black", 20, FontStyle.Bold);

                    numBtn.Text = number.ToString();
                    numBtn.Tag = number;

                    numBtn.Paint += (s, pe) =>
                    {
                        Button btn = (Button)s;
                        pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                        bool isSelected = this.IsNumberSelected((int)btn.Tag);
                        Color currentTextColor = isSelected ? textSelectedColor : textUnselectedColor;

                        if (isSelected)
                        {
                            using (SolidBrush selectBrush = new SolidBrush(logoBrightGreen))
                            {
                                int circleSize = Math.Min(btn.Width, btn.Height) - 2;
                                int cx = (btn.Width - circleSize) / 2;
                                int cy = (btn.Height - circleSize) / 2;
                                pe.Graphics.FillEllipse(selectBrush, new Rectangle(cx, cy, circleSize, circleSize));
                            }
                        }

                        TextFormatFlags flags = TextFormatFlags.HorizontalCenter |
                                                TextFormatFlags.VerticalCenter |
                                                TextFormatFlags.SingleLine |
                                                TextFormatFlags.NoPadding;

                        TextRenderer.DrawText(pe.Graphics, btn.Text, btn.Font, btn.ClientRectangle, currentTextColor, flags);
                    };

                    numberButtons.Add(numBtn);
                    this.bingoGridEngine.Controls.Add(numBtn, col + 1, row);
                }
            }
        }

        #endregion
    }
}