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
            this.panelTetrisBoard = new System.Windows.Forms.Panel();
            //Mój przycisk którego nie da się zaznaczyć
            this.buttonStartGame = new NonSelectableButton();
            this.labelScore = new System.Windows.Forms.Label();
            this.labelScoreActually = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panelTetrisBoard
            // 
            this.panelTetrisBoard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTetrisBoard.Location = new System.Drawing.Point(12, 12);
            this.panelTetrisBoard.Name = "panelTetrisBoard";
            this.panelTetrisBoard.Size = new System.Drawing.Size(320, 440);
            this.panelTetrisBoard.TabIndex = 0;
            this.panelTetrisBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelTetrisBoard_Paint);
            // 
            // buttonStartGame
            // 
            this.buttonStartGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonStartGame.Location = new System.Drawing.Point(480, 166);
            this.buttonStartGame.Margin = new System.Windows.Forms.Padding(0);
            this.buttonStartGame.Name = "buttonStartGame";
            this.buttonStartGame.Size = new System.Drawing.Size(192, 68);
            this.buttonStartGame.TabIndex = 1;
            this.buttonStartGame.TabStop = false;
            this.buttonStartGame.Text = "Start Game";
            this.buttonStartGame.UseVisualStyleBackColor = true;
            this.buttonStartGame.Click += new System.EventHandler(this.ButtonStartGame_Click);
            
            //Usunięcie flickeringu
            typeof(Panel).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, panelTetrisBoard, new object[] { true });
            // 
            // labelScore
            // 
            this.labelScore.AutoSize = true;
            this.labelScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelScore.Location = new System.Drawing.Point(338, 12);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(109, 37);
            this.labelScore.TabIndex = 2;
            this.labelScore.Text = "Score:";
            // 
            // labelScoreActually
            // 
            this.labelScoreActually.AutoSize = true;
            this.labelScoreActually.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelScoreActually.Location = new System.Drawing.Point(453, 12);
            this.labelScoreActually.Name = "labelScoreActually";
            this.labelScoreActually.Size = new System.Drawing.Size(35, 37);
            this.labelScoreActually.TabIndex = 3;
            this.labelScoreActually.Text = "0";
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
            this.Text = "Tetris";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTetrisBoard;
        private System.Windows.Forms.Button buttonStartGame;
        private System.Windows.Forms.Label labelScore;
        private System.Windows.Forms.Label labelScoreActually;
    }
}

