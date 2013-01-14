namespace TrelloKinectControl
{
    partial class BrowserForm
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
            this.browserContainer = new System.Windows.Forms.ToolStripContainer();
            this.onOffButton = new System.Windows.Forms.Button();
            this.browserContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // browserContainer
            // 
            // 
            // browserContainer.ContentPanel
            // 
            this.browserContainer.ContentPanel.Size = new System.Drawing.Size(1280, 999);
            this.browserContainer.Location = new System.Drawing.Point(3, 3);
            this.browserContainer.Name = "browserContainer";
            this.browserContainer.Size = new System.Drawing.Size(1280, 1024);
            this.browserContainer.TabIndex = 0;
            this.browserContainer.Text = "toolStripContainer1";
            // 
            // onOffButton
            // 
            this.onOffButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.onOffButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.onOffButton.Location = new System.Drawing.Point(1450, 15);
            this.onOffButton.Name = "onOffButton";
            this.onOffButton.Size = new System.Drawing.Size(96, 32);
            this.onOffButton.TabIndex = 1;
            this.onOffButton.Text = "Start";
            this.onOffButton.UseVisualStyleBackColor = false;
            this.onOffButton.Click += new System.EventHandler(this.onOffButton_Click);
            // 
            // BrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1634, 862);
            this.Controls.Add(this.onOffButton);
            this.Controls.Add(this.browserContainer);
            this.Name = "BrowserForm";
            this.Text = "Form1";
            this.browserContainer.ResumeLayout(false);
            this.browserContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer browserContainer;
        private System.Windows.Forms.Button onOffButton;
    }
}

