namespace DesktopMatcherDemo
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.patternTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.stringTextBox = new System.Windows.Forms.TextBox();
            this.matchButton = new System.Windows.Forms.Button();
            this.fullPatternTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.resultLable = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(344, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pattern";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // patternTextBox
            // 
            this.patternTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.patternTextBox.Location = new System.Drawing.Point(17, 41);
            this.patternTextBox.Multiline = true;
            this.patternTextBox.Name = "patternTextBox";
            this.patternTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.patternTextBox.Size = new System.Drawing.Size(764, 127);
            this.patternTextBox.TabIndex = 1;
            this.patternTextBox.Text = "(кореллы | волнистые) & летят";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(270, 171);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(262, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "String to match with pattern";
            // 
            // stringTextBox
            // 
            this.stringTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.stringTextBox.Location = new System.Drawing.Point(17, 198);
            this.stringTextBox.Multiline = true;
            this.stringTextBox.Name = "stringTextBox";
            this.stringTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.stringTextBox.Size = new System.Drawing.Size(764, 189);
            this.stringTextBox.TabIndex = 3;
            this.stringTextBox.Text = "Волнистые попугаи летят на Юг";
            // 
            // matchButton
            // 
            this.matchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.matchButton.Location = new System.Drawing.Point(678, 393);
            this.matchButton.Name = "matchButton";
            this.matchButton.Size = new System.Drawing.Size(103, 34);
            this.matchButton.TabIndex = 4;
            this.matchButton.Text = "MATCH";
            this.matchButton.UseVisualStyleBackColor = true;
            this.matchButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // fullPatternTextBox
            // 
            this.fullPatternTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fullPatternTextBox.Location = new System.Drawing.Point(17, 433);
            this.fullPatternTextBox.Multiline = true;
            this.fullPatternTextBox.Name = "fullPatternTextBox";
            this.fullPatternTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.fullPatternTextBox.Size = new System.Drawing.Size(764, 267);
            this.fullPatternTextBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(13, 398);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 24);
            this.label3.TabIndex = 6;
            this.label3.Text = "Full pattern";
            // 
            // resultLable
            // 
            this.resultLable.AutoSize = true;
            this.resultLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resultLable.Location = new System.Drawing.Point(270, 398);
            this.resultLable.Name = "resultLable";
            this.resultLable.Size = new System.Drawing.Size(211, 24);
            this.resultLable.TabIndex = 7;
            this.resultLable.Text = "No matching result yet...";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 711);
            this.Controls.Add(this.resultLable);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.fullPatternTextBox);
            this.Controls.Add(this.matchButton);
            this.Controls.Add(this.stringTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.patternTextBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Matcher DEMO";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox patternTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox stringTextBox;
        private System.Windows.Forms.Button matchButton;
        private System.Windows.Forms.TextBox fullPatternTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label resultLable;
    }
}

