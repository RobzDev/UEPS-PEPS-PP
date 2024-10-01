namespace CostosEmpresariales
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dtviewMI = new DataGridView();
            cbOptions = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dtviewMI).BeginInit();
            SuspendLayout();
            // 
            // dtviewMI
            // 
            dtviewMI.AccessibleRole = AccessibleRole.None;
            dtviewMI.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtviewMI.Location = new Point(66, 57);
            dtviewMI.Name = "dtviewMI";
            dtviewMI.Size = new Size(631, 381);
            dtviewMI.TabIndex = 0;
            dtviewMI.CellBeginEdit += dtviewMI_CellBeginEdit;
            dtviewMI.CellEndEdit += dtviewMI_CellEndEdit;
            // 
            // cbOptions
            // 
            cbOptions.FormattingEnabled = true;
            cbOptions.Items.AddRange(new object[] { "PEPS", "UEPS", "PP" });
            cbOptions.Location = new Point(576, 28);
            cbOptions.Name = "cbOptions";
            cbOptions.Size = new Size(121, 23);
            cbOptions.TabIndex = 1;
            cbOptions.SelectedIndexChanged += cbOptions_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(cbOptions);
            Controls.Add(dtviewMI);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dtviewMI).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dtviewMI;
        private ComboBox cbOptions;
    }
}
