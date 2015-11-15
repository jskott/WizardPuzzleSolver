namespace WizardPuzzleSolver
{
    partial class SolverForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolverForm));
            this.m_solveButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.piecesFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.m_checkAllButton = new System.Windows.Forms.Button();
            this.m_clearAllButton = new System.Windows.Forms.Button();
            this.solverBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.countLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_solveButton
            // 
            this.m_solveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_solveButton.Location = new System.Drawing.Point(885, 399);
            this.m_solveButton.Name = "m_solveButton";
            this.m_solveButton.Size = new System.Drawing.Size(75, 23);
            this.m_solveButton.TabIndex = 0;
            this.m_solveButton.Text = "Solve";
            this.m_solveButton.UseVisualStyleBackColor = true;
            this.m_solveButton.Click += new System.EventHandler(this.m_solveButton_Click);
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.Location = new System.Drawing.Point(12, 151);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(948, 242);
            this.flowLayoutPanel.TabIndex = 1;
            // 
            // piecesFlowLayoutPanel
            // 
            this.piecesFlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.piecesFlowLayoutPanel.AutoScroll = true;
            this.piecesFlowLayoutPanel.Location = new System.Drawing.Point(13, 13);
            this.piecesFlowLayoutPanel.Name = "piecesFlowLayoutPanel";
            this.piecesFlowLayoutPanel.Size = new System.Drawing.Size(947, 116);
            this.piecesFlowLayoutPanel.TabIndex = 2;
            // 
            // m_checkAllButton
            // 
            this.m_checkAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_checkAllButton.Location = new System.Drawing.Point(12, 399);
            this.m_checkAllButton.Name = "m_checkAllButton";
            this.m_checkAllButton.Size = new System.Drawing.Size(75, 23);
            this.m_checkAllButton.TabIndex = 3;
            this.m_checkAllButton.Text = "Check All";
            this.m_checkAllButton.UseVisualStyleBackColor = true;
            this.m_checkAllButton.Click += new System.EventHandler(this.m_selectAllButton_Click);
            // 
            // m_clearAllButton
            // 
            this.m_clearAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_clearAllButton.Location = new System.Drawing.Point(93, 399);
            this.m_clearAllButton.Name = "m_clearAllButton";
            this.m_clearAllButton.Size = new System.Drawing.Size(75, 23);
            this.m_clearAllButton.TabIndex = 4;
            this.m_clearAllButton.Text = "Clear All";
            this.m_clearAllButton.UseVisualStyleBackColor = true;
            this.m_clearAllButton.Click += new System.EventHandler(this.m_clearAllButton_Click);
            // 
            // solverBackgroundWorker
            // 
            this.solverBackgroundWorker.WorkerReportsProgress = true;
            this.solverBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DoSolveWork);
            this.solverBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.solverBackgroundWorker_ProgressChanged);
            this.solverBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.solverBackgroundWorker_RunWorkerCompleted);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(184, 399);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(695, 23);
            this.progressBar.Step = 5;
            this.progressBar.TabIndex = 5;
            // 
            // countLabel
            // 
            this.countLabel.AutoSize = true;
            this.countLabel.Location = new System.Drawing.Point(12, 132);
            this.countLabel.Name = "countLabel";
            this.countLabel.Size = new System.Drawing.Size(107, 13);
            this.countLabel.TabIndex = 6;
            this.countLabel.Text = "Number of iterations: ";
            // 
            // SolverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(972, 434);
            this.Controls.Add(this.countLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.m_clearAllButton);
            this.Controls.Add(this.m_checkAllButton);
            this.Controls.Add(this.piecesFlowLayoutPanel);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.m_solveButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SolverForm";
            this.Text = "Solver";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_solveButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel piecesFlowLayoutPanel;
        private System.Windows.Forms.Button m_checkAllButton;
        private System.Windows.Forms.Button m_clearAllButton;
        private System.ComponentModel.BackgroundWorker solverBackgroundWorker;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label countLabel;
    }
}

