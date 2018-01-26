using System;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Tieptuyen.Common;

namespace Tieptuyen
{
    public partial class SummaryRowForm : Telerik.WinControls.UI.RadForm
    {
        public delegate void RemoveGroupEvaluateEvent(bool isRemove);
        public RemoveGroupEvaluateEvent removeGroupEvaluateEvent;
        RadGridView grid;
        public SummaryRowForm(RadGridView grid)
        {
            InitializeComponent();
            this.grid = grid;
            //    InitComboBoxes();

        }
        private void InitComboBoxes()
        {
            radComboBoxFunction.DataSource = MyGridAggregateFunction.GetList();
            radComboBoxFunction.ValueMember = "AggregateValue";
            radComboBoxFunction.DisplayMember = "AggregateName";

            for (int i = 0; i < grid.Columns.Count; i++)
            {
                RadListDataItem item = new RadListDataItem(grid.Columns[i].HeaderText, grid.Columns[i].FieldName);
                radComboBoxColumn.Items.Add(item);
            }
            radComboBoxLocation.Items.Add(new RadListDataItem("Trên"));
            radComboBoxLocation.Items.Add(new RadListDataItem("Dưới"));

            radComboBoxLocation.SelectedIndex = 0;
            radComboBoxFunction.SelectedIndex = 0;
            radComboBoxColumn.SelectedIndex = 0;
        }

        private void RebuildComboBoxItems(RadDropDownList dropDownList, int count)
        {
            dropDownList.BeginUpdate();
            dropDownList.Items.Clear();
            for (int i = 0; i < count; i++)
            {
                dropDownList.Items.Add(new RadListDataItem(string.Format("Dòng {0}", i + 1)));
            }
            dropDownList.Items.Add(new RadListDataItem("Dòng mới"));
            dropDownList.SelectedIndex = count - 1;
            dropDownList.EndUpdate();
        }
        private GridViewSummaryRowItem CollectionItem(GridViewSummaryRowItemCollection collection, int rowIndex, out bool updated)
        {
            updated = false;
            if (collection == null || rowIndex < 0) return null;
            while (rowIndex >= collection.Count)
            {
                updated = true; collection.Add(new GridViewSummaryRowItem());
            }
            return collection[rowIndex];
        }       

        private void radCheckBox1_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                removeGroupEvaluateEvent(true);
                this.radGroupBox1.Enabled = true;
            }
            else if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.Off)
            {
                removeGroupEvaluateEvent(false);
                this.radGroupBox1.Enabled = false;
                grid.MasterTemplate.SummaryRowsBottom.Clear();
                grid.MasterTemplate.SummaryRowsTop.Clear();
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            GridViewSummaryRowItem item = null;
            string formatString = string.Empty;
            bool updated = false;
            switch (this.radComboBoxLocation.SelectedIndex)
            {

                case 0:
                    item = CollectionItem(this.grid.MasterTemplate.SummaryRowsTop, this.radComboBoxRow.SelectedIndex, out updated);
                    if (updated)
                        RebuildComboBoxItems((RadDropDownList)radComboBoxRow, this.grid.MasterTemplate.SummaryRowsTop.Count);
                    formatString = string.Format("{0}: {1}; ", radComboBoxFunction.Text, "{0:0,0.##}");
                    break;
                case 1:
                    item = CollectionItem(this.grid.MasterTemplate.SummaryRowsBottom, this.radComboBoxRow.SelectedIndex, out updated);
                    if (updated)
                        RebuildComboBoxItems(this.radComboBoxRow, this.grid.MasterTemplate.SummaryRowsBottom.Count);
                    formatString = string.Format("{0}: {1}; ", radComboBoxFunction.Text, "{0:0,0.##}");
                    break;
            }
            if (item == null) return;
            string fieldName = ((RadListDataItem)radComboBoxColumn.SelectedItem).Value.ToString();
            item.Add(new GridViewSummaryItem(fieldName, formatString, (GridAggregateFunction)radComboBoxFunction.SelectedValue));
        }
     
        private void radButton3_Click(object sender, EventArgs e)
        {
            this.grid.MasterTemplate.SummaryRowsBottom.Clear();
            this.grid.MasterTemplate.SummaryRowsTop.Clear();
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SummaryRowForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (grid.MasterTemplate.SummaryRowsTop.Count == 0 && grid.MasterTemplate.SummaryRowsBottom.Count == 0)
                removeGroupEvaluateEvent(false);
        }            

        private void SummaryRowForm_Load(object sender, EventArgs e)
        {
            this.InitComboBoxes();
            this.radGroupBox1.Enabled = false;

            this.radComboBoxRow.SelectedIndex = 0;

            if (grid.MasterTemplate.SummaryRowsBottom.Count != 0 || grid.MasterTemplate.SummaryRowsTop.Count != 0)
            {
                this.radCheckBox1.Checked = true;
            }
        }                           

        private void radComboBoxLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (radComboBoxLocation.SelectedIndex)
            {
                case 0:
                    RebuildComboBoxItems(radComboBoxRow, this.grid.MasterTemplate.SummaryRowsTop.Count);
                    break;
                case 1:
                    RebuildComboBoxItems(radComboBoxRow, this.grid.MasterTemplate.SummaryRowsBottom.Count);
                    break;
            }
        }      
    }
}
