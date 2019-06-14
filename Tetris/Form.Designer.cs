using System.Reflection;
using System.Windows.Forms;

namespace Tetris
{
    partial class Form
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
            this.panelTetrisBoard = new Tetris.TetrisPanel();
            this.labelScore = new System.Windows.Forms.Label();
            this.labelScoreActually = new System.Windows.Forms.Label();
            this.buttonStartGame = new Tetris.NonSelectableButton();
            this.labelPaused = new System.Windows.Forms.Label();
            this.panelTetrisBoard.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTetrisBoard
            // 
            this.panelTetrisBoard.BackColor = System.Drawing.Color.Transparent;
            this.panelTetrisBoard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTetrisBoard.Controls.Add(this.labelPaused);
            this.panelTetrisBoard.Location = new System.Drawing.Point(12, 12);
            this.panelTetrisBoard.Name = "panelTetrisBoard";
            this.panelTetrisBoard.Size = new System.Drawing.Size(320, 440);
            this.panelTetrisBoard.TabIndex = 0;
            this.panelTetrisBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelTetrisBoard_Paint);
            // 
            // labelScore
            // 
            this.labelScore.AutoSize = true;
            this.labelScore.Font = new System.Drawing.Font("MV Boli", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScore.Location = new System.Drawing.Point(338, 12);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(111, 41);
            this.labelScore.TabIndex = 2;
            this.labelScore.Text = "Score:";
            // 
            // labelScoreActually
            // 
            this.labelScoreActually.AutoSize = true;
            this.labelScoreActually.Font = new System.Drawing.Font("MV Boli", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScoreActually.Location = new System.Drawing.Point(453, 12);
            this.labelScoreActually.Name = "labelScoreActually";
            this.labelScoreActually.Size = new System.Drawing.Size(40, 41);
            this.labelScoreActually.TabIndex = 3;
            this.labelScoreActually.Text = "0";
            // 
            // buttonStartGame
            // 
            this.buttonStartGame.Font = new System.Drawing.Font("MV Boli", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStartGame.Location = new System.Drawing.Point(448, 166);
            this.buttonStartGame.Margin = new System.Windows.Forms.Padding(0);
            this.buttonStartGame.Name = "buttonStartGame";
            this.buttonStartGame.Size = new System.Drawing.Size(224, 68);
            this.buttonStartGame.TabIndex = 1;
            this.buttonStartGame.TabStop = false;
            this.buttonStartGame.Text = "Start Game";
            this.buttonStartGame.UseVisualStyleBackColor = true;
            this.buttonStartGame.Click += new System.EventHandler(this.ButtonStartGame_Click);
            // 
            // labelPaused
            // 
            this.labelPaused.BackColor = System.Drawing.Color.Transparent;
            this.labelPaused.Font = new System.Drawing.Font("MV Boli", 36F);
            this.labelPaused.Location = new System.Drawing.Point(3, 153);
            this.labelPaused.Name = "labelPaused";
            this.labelPaused.Size = new System.Drawing.Size(312, 68);
            this.labelPaused.TabIndex = 0;
            this.labelPaused.Text = "Paused";
            this.labelPaused.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelPaused.Visible = false;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 459);
            this.Controls.Add(this.labelScoreActually);
            this.Controls.Add(this.labelScore);
            this.Controls.Add(this.buttonStartGame);
            this.Controls.Add(this.panelTetrisBoard);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "Form";
            this.Text = "Simple Tetris";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.panelTetrisBoard.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelScore;
        private System.Windows.Forms.Label labelScoreActually;
        private NonSelectableButton buttonStartGame;
        private TetrisPanel panelTetrisBoard;
        private Label labelPaused;
    }
}

