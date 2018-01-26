﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Tieptuyen
{
    public partial class FormOptions : Telerik.WinControls.UI.RadForm
    {
        bool fitPage;
        public bool FitToPage
        {
            get { return fitPage; }
        }

        bool gridColors;
        public bool UseGridColors
        {
            get { return gridColors; }
        }

        int margins;
        public int SetAllMargins
        {
            get { return margins; }
        }

        System.Drawing.Printing.PaperKind paperKind;
        public System.Drawing.Printing.PaperKind PaperKind
        {
            get { return paperKind; }
        }

        bool isLandScape;
        public bool IsLandScape
        {
            get { return isLandScape; }
        }

        bool repeatHeader;
        public bool RepeatHeaderRow
        {
            get { return repeatHeader; }
        }
        string headerText;

        public string HeaderText
        {
            get { return headerText; }
        }
        bool reportGroup;

        public bool ReportGroup
        {
            get { return reportGroup; }
        }
        public FormOptions()
        {
            InitializeComponent();
            FillPageSizesCombo();

            this.DialogResult = DialogResult.No;
        }



        private void FillPageSizesCombo()
        {
            foreach (System.Drawing.Printing.PaperKind kind in
                Enum.GetValues(typeof(System.Drawing.Printing.PaperKind)))
            {
                RadListDataItem item = new RadListDataItem();
                item.Text = kind.ToString();
                item.Value = kind;
                this.ddPaperSize.Items.Add(item);
            }

            this.ddPaperSize.SelectedIndex = 0;
        }

        private void radButtonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.gridColors = this.radCheckBoxColors.IsChecked;
            this.repeatHeader = this.radCheckBoxHeaderRow.IsChecked;
            this.margins = (int)this.radSpinEditorMargins.Value;
            this.headerText = this.radTextBox1.Text;
            this.paperKind = (System.Drawing.Printing.PaperKind)this.ddPaperSize.SelectedValue;
            this.reportGroup = this.radCheckBoxGroup.IsChecked;
            if (this.radRadioButtonLandscape.IsChecked)
            {
                this.isLandScape = true;
            }
            else if (this.radRadioButtonPortrait.IsChecked)
            {
                this.isLandScape = false;
            }

            this.Close();
        }

        private void radButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }           
    }
}
