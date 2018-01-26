namespace Tieptuyen
{
    partial class SummaryRowForm
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
            this.radButton2 = new Telerik.WinControls.UI.RadButton();
            this.radButton3 = new Telerik.WinControls.UI.RadButton();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.radComboBoxRow = new Telerik.WinControls.UI.RadDropDownList();
            this.radComboBoxLocation = new Telerik.WinControls.UI.RadDropDownList();
            this.radComboBoxColumn = new Telerik.WinControls.UI.RadDropDownList();
            this.radComboBoxFunction = new Telerik.WinControls.UI.RadDropDownList();
            this.radCheckBox1 = new Telerik.WinControls.UI.RadCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radComboBoxRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radComboBoxLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radComboBoxColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radComboBoxFunction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCheckBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radButton2
            // 
            this.radButton2.Location = new System.Drawing.Point(199, 179);
            this.radButton2.Name = "radButton2";
            this.radButton2.Size = new System.Drawing.Size(62, 23);
            this.radButton2.TabIndex = 10;
            this.radButton2.Text = "Đóng";
            this.radButton2.ThemeName = "Breeze";
            this.radButton2.Click += new System.EventHandler(this.radButton2_Click);
            // 
            // radButton3
            // 
            this.radButton3.Location = new System.Drawing.Point(105, 179);
            this.radButton3.Name = "radButton3";
            this.radButton3.Size = new System.Drawing.Size(75, 23);
            this.radButton3.TabIndex = 9;
            this.radButton3.Text = "Xóa Tất Cả";
            this.radButton3.ThemeName = "Breeze";
            this.radButton3.Click += new System.EventHandler(this.radButton3_Click);
            // 
            // radButton1
            // 
            this.radButton1.Location = new System.Drawing.Point(14, 179);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(75, 23);
            this.radButton1.TabIndex = 8;
            this.radButton1.Text = "Đồng ý";
            this.radButton1.ThemeName = "Breeze";
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // radLabel4
            // 
            this.radLabel4.Location = new System.Drawing.Point(52, 147);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(39, 18);
            this.radLabel4.TabIndex = 6;
            this.radLabel4.Text = "Dòng :";
            // 
            // radLabel3
            // 
            this.radLabel3.Location = new System.Drawing.Point(52, 113);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(37, 18);
            this.radLabel3.TabIndex = 4;
            this.radLabel3.Text = "Vị Trí :";
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(61, 76);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(29, 18);
            this.radLabel2.TabIndex = 2;
            this.radLabel2.Text = "Cột :";
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(24, 40);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(65, 14);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "Chức năng :";
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox1.Controls.Add(this.radComboBoxRow);
            this.radGroupBox1.Controls.Add(this.radComboBoxLocation);
            this.radGroupBox1.Controls.Add(this.radComboBoxColumn);
            this.radGroupBox1.Controls.Add(this.radComboBoxFunction);
            this.radGroupBox1.Controls.Add(this.radButton2);
            this.radGroupBox1.Controls.Add(this.radButton3);
            this.radGroupBox1.Controls.Add(this.radButton1);
            this.radGroupBox1.Controls.Add(this.radLabel4);
            this.radGroupBox1.Controls.Add(this.radLabel3);
            this.radGroupBox1.Controls.Add(this.radLabel2);
            this.radGroupBox1.Controls.Add(this.radLabel1);
            this.radGroupBox1.HeaderText = "Bảng chọn cột";
            this.radGroupBox1.Location = new System.Drawing.Point(12, 52);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.radGroupBox1.Size = new System.Drawing.Size(274, 215);
            this.radGroupBox1.TabIndex = 3;
            this.radGroupBox1.Text = "Bảng chọn cột";
            // 
            // radComboBoxRow
            // 
            this.radComboBoxRow.Location = new System.Drawing.Point(105, 147);
            this.radComboBoxRow.Name = "radComboBoxRow";
            this.radComboBoxRow.Size = new System.Drawing.Size(145, 20);
            this.radComboBoxRow.TabIndex = 15;
            this.radComboBoxRow.Text = "radDropDownList1";
            // 
            // radComboBoxLocation
            // 
            this.radComboBoxLocation.Location = new System.Drawing.Point(105, 113);
            this.radComboBoxLocation.Name = "radComboBoxLocation";
            this.radComboBoxLocation.Size = new System.Drawing.Size(145, 20);
            this.radComboBoxLocation.TabIndex = 14;
            this.radComboBoxLocation.Text = "radDropDownList1";
            this.radComboBoxLocation.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.radComboBoxLocation_SelectedIndexChanged);
            // 
            // radComboBoxColumn
            // 
            this.radComboBoxColumn.Location = new System.Drawing.Point(105, 76);
            this.radComboBoxColumn.Name = "radComboBoxColumn";
            this.radComboBoxColumn.Size = new System.Drawing.Size(145, 20);
            this.radComboBoxColumn.TabIndex = 13;
            this.radComboBoxColumn.Text = "radDropDownList1";
            // 
            // radComboBoxFunction
            // 
            this.radComboBoxFunction.Location = new System.Drawing.Point(105, 40);
            this.radComboBoxFunction.Name = "radComboBoxFunction";
            this.radComboBoxFunction.Size = new System.Drawing.Size(145, 20);
            this.radComboBoxFunction.TabIndex = 12;
            this.radComboBoxFunction.Text = "radDropDownList1";
            // 
            // radCheckBox1
            // 
            this.radCheckBox1.Location = new System.Drawing.Point(12, 12);
            this.radCheckBox1.Name = "radCheckBox1";
            this.radCheckBox1.Size = new System.Drawing.Size(180, 18);
            this.radCheckBox1.TabIndex = 2;
            this.radCheckBox1.Text = "Bỏ chức năng chọn cột";
            this.radCheckBox1.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.radCheckBox1_ToggleStateChanged);
            // 
            // SummaryRowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 279);
            this.Controls.Add(this.radGroupBox1);
            this.Controls.Add(this.radCheckBox1);
            this.Name = "SummaryRowForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "SummaryRowForm";
            this.ThemeName = "ControlDefault";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SummaryRowForm_FormClosing);
            this.Load += new System.EventHandler(this.SummaryRowForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            this.radGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radComboBoxRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radComboBoxLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radComboBoxColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radComboBoxFunction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCheckBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton radButton2;
        private Telerik.WinControls.UI.RadButton radButton3;
        private Telerik.WinControls.UI.RadButton radButton1;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadCheckBox radCheckBox1;
        private Telerik.WinControls.UI.RadDropDownList radComboBoxRow;
        private Telerik.WinControls.UI.RadDropDownList radComboBoxLocation;
        private Telerik.WinControls.UI.RadDropDownList radComboBoxColumn;
        private Telerik.WinControls.UI.RadDropDownList radComboBoxFunction;
    }
}
