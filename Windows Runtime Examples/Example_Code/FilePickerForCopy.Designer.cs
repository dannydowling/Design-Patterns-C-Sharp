namespace Windows_Runtime_Examples.Example_Code
{
    partial class FilePickerForCopy
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.desitinationFolder = new System.Windows.Forms.Label();
            this.errorDisplay = new System.Windows.Forms.Label();
            this.cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(556, 127);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 39);
            this.button1.TabIndex = 2;
            this.button1.Text = "Source 1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Source_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(555, 189);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(114, 37);
            this.button2.TabIndex = 3;
            this.button2.Text = "Source 2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Source_2);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(555, 246);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(114, 38);
            this.button3.TabIndex = 4;
            this.button3.Text = "Source 3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Source_3);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(563, 45);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(151, 38);
            this.button4.TabIndex = 5;
            this.button4.Text = "Copy";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Copy);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(556, 370);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(136, 29);
            this.button5.TabIndex = 6;
            this.button5.Text = "Set Destination";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.Set_Destination);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(281, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Add the sources to the list and push copy";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 10;
            // 
            // desitinationFolder
            // 
            this.desitinationFolder.AutoSize = true;
            this.desitinationFolder.Location = new System.Drawing.Point(35, 394);
            this.desitinationFolder.Name = "desitinationFolder";
            this.desitinationFolder.Size = new System.Drawing.Size(152, 20);
            this.desitinationFolder.TabIndex = 11;
            this.desitinationFolder.Text = "destinationFolderText";
            // 
            // errorDisplay
            // 
            this.errorDisplay.AutoSize = true;
            this.errorDisplay.Location = new System.Drawing.Point(35, 118);
            this.errorDisplay.Name = "errorDisplay";
            this.errorDisplay.Size = new System.Drawing.Size(94, 20);
            this.errorDisplay.TabIndex = 12;
            this.errorDisplay.Text = "Error Display";
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(558, 306);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(111, 29);
            this.cancel.TabIndex = 13;
            this.cancel.Text = "cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // FilePickerForCopy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.errorDisplay);
            this.Controls.Add(this.desitinationFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "FilePickerForCopy";
            this.Text = "FilePickerForCopy";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Label label1;
        private Label label2;
        private Label desitinationFolder;
        private Label errorDisplay;
        private Button cancel;
    }
}