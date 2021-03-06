using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using Telerik.ReportViewer;
using Telerik.Reporting;
using Telerik.WinControls.UI;
using System.Drawing.Printing;
using Telerik.ReportViewer.WinForms;
namespace Tieptuyen
{
    public class RadGridReport
    {

        private Form reportForm;
        private Report report;
        private DataSet reportDataSet;
        private string reportName;
        public int pageWidth;
        private Boolean reportGroup;
        /// <summary>
        /// Get report name
        /// </summary>
        /// 

        public Boolean ReportGroup
        {
            set { reportGroup = value; }
            get { return reportGroup; }
        }

        public string ReportName
        {
            get { return reportName; }
        }

        private PageHeaderSection pageHeader;
        private ReportHeaderSection reportHeader;
        private DetailSection detail;
        private PageFooterSection pageFooter;

        //dimension
        private int tableHeaderHeight;
        private int tableRowsHeight;

        #region Page Settings API

        private bool useGridColors = false;

        public bool UseGridColors
        {
            get{return useGridColors;}
            set{useGridColors = value;}
        }

        private bool pageLandScape = false;

        /// <summary>
        /// Get or set the page landscape layout 
        /// </summary>
        public bool PageLandScape
        {
            get{return pageLandScape;}
            set{pageLandScape = value;}
        }

        private System.Drawing.Printing.PaperKind paperKind =
            System.Drawing.Printing.PaperKind.Custom;

        /// <summary>
        /// Get or set paper kind (A3, A4 etc.)
        /// </summary>
        public System.Drawing.Printing.PaperKind PaperKind
        {
            get { return paperKind; }
            set { paperKind = value; }
        }

        private Telerik.Reporting.Drawing.Unit leftMargin = Telerik.Reporting.Drawing.Unit.Pixel(0);

        /// <summary>
        /// Set report's left margin in milimeters
        /// </summary>
        public int LeftMargin
        {
            set{leftMargin = Telerik.Reporting.Drawing.Unit.Pixel(value);}
        }

        private Telerik.Reporting.Drawing.Unit rightMargin = Telerik.Reporting.Drawing.Unit.Pixel(0);


        /// <summary>
        /// Set report's right margin in milimeters
        /// </summary>
        public int RightMargin
        {
            set{rightMargin = Telerik.Reporting.Drawing.Unit.Pixel(value);}
        }

        private Telerik.Reporting.Drawing.Unit topMargin = Telerik.Reporting.Drawing.Unit.Pixel(0);

        /// <summary>
        /// Set report's top margin in milimeters
        /// </summary>
        public int TopMargin
        {
            set{topMargin = Telerik.Reporting.Drawing.Unit.Pixel(value);}
        }

        private Telerik.Reporting.Drawing.Unit bottomMargin = Telerik.Reporting.Drawing.Unit.Pixel(0);

        /// <summary>
        /// Set report's bottom margin in milimeters
        /// </summary>
        public int BottomMargin
        {
            set{bottomMargin = Telerik.Reporting.Drawing.Unit.Pixel(value);}
        }

        /// <summary>
        /// Set report's all margins in milimeters
        /// </summary>
        public int AllMargins
        {
            set
            {
                topMargin = Telerik.Reporting.Drawing.Unit.Pixel(value);
                bottomMargin = Telerik.Reporting.Drawing.Unit.Pixel(value);
                leftMargin = Telerik.Reporting.Drawing.Unit.Pixel(value);
                rightMargin = Telerik.Reporting.Drawing.Unit.Pixel(value);
            }
        }

        private bool fitToPageSize = false;

        /// <summary>
        /// Set report to fit to paper size page
        /// </summary>
        public bool FitToPageSize
        {
            set
            {
                fitToPageSize = value;
            }
        }

        private FormWindowState windowState;

        public FormWindowState ReportWindowState
        {
            set
            {
                windowState = value;
            }
        }

        private Size reportSize = new Size(1024, 768);

        public Size ReportSize
        {
            set { reportSize = value; }
        }

        private bool repeatTableHeader = false;

        /// <summary>
        /// Repeat Table header on all pages
        /// </summary>
        public bool RepeatTableHeader
        {
            set { repeatTableHeader = value; }
        }

        #endregion

        public RadGridReport(string reportName)
        {
            this.reportName = reportName;
            reportDataSet = new DataSet();
        }

        private void CreateReportForm(string formTitle)
        {
            this.reportForm = new Form();
            this.reportForm.Size = reportSize;
            this.reportForm.WindowState = windowState;
            this.reportForm.StartPosition = FormStartPosition.CenterParent;
            this.reportForm.FormBorderStyle = FormBorderStyle.Sizable;
            this.reportForm.MinimizeBox = false;
            this.reportForm.Text = formTitle;
            this.reportForm.ShowInTaskbar = false;
            this.reportForm.ShowIcon = false;
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.Dock = DockStyle.Fill;
            reportViewer.Name = "ReportViewer";
            this.reportForm.Controls.Add(reportViewer);
            this.reportForm.FormClosed += new FormClosedEventHandler(reportForm_FormClosed);
        }

        void reportForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            pageHeader = null;
            reportHeader = null;
            detail = null;
            pageFooter = null;
            reportDataSet = null;
            report.Dispose();
            reportForm.Dispose();
        }

        /// <summary>
        /// Create and show a form containig report
        /// </summary>
        /// <param name="owner">The owner of report form</param>
        /// <param name="radGridView">The grid which will be present in report</param>
        public void ReportFormShow(IWin32Window owner, RadGridView radGridView)
        {
            GetGridDimensions(radGridView);
            CreateReportForm(reportName);
            CreateTelerikReport();
            CreateDataSet(radGridView);
            AddReportData(radGridView);
            report.PageSettings.PaperSize =
                new Telerik.Reporting.Drawing.SizeU(new Size(768,
                    pageWidth + (int) (leftMargin.Value + rightMargin.Value + 10)));
            ((ReportViewer) reportForm.Controls["ReportViewer"]).Report = this.report;
            ((ReportViewer) reportForm.Controls["ReportViewer"]).RefreshReport();
            reportForm.ShowDialog(owner);
        }

        /// <summary>
        /// Create report instance and its attributes
        /// </summary>
        private void CreateTelerikReport()
        {
            this.report = new Report();

            pageHeader = new PageHeaderSection();
            detail = new DetailSection();
            pageFooter = new PageFooterSection();
            Telerik.Reporting.TextBox reportTitleTextBox = new Telerik.Reporting.TextBox();
            Telerik.Reporting.TextBox currentTimeTextBox = new Telerik.Reporting.TextBox();
            Telerik.Reporting.TextBox pageInfoTextBox = new Telerik.Reporting.TextBox();

            ((System.ComponentModel.ISupportInitialize)report).BeginInit();

            // 
            // reportTitleTextBox
            //
            reportTitleTextBox.Name = "reportTitleTextBox";
            reportTitleTextBox.Dock = DockStyle.Fill;
            reportTitleTextBox.Style.Font.Style = FontStyle.Bold | FontStyle.Italic;
            reportTitleTextBox.Style.Font.Size =
                new Telerik.Reporting.Drawing.Unit(14, Telerik.Reporting.Drawing.UnitType.Pixel);
            reportTitleTextBox.Value = this.reportName;
            // 
            //pageHeader
            //

            this.pageHeader.Height =
                new Telerik.Reporting.Drawing.Unit(25, Telerik.Reporting.Drawing.UnitType.Pixel);
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
                    reportTitleTextBox
                    });
            // 
            // detail
            // 
            detail.Height = new Telerik.Reporting.Drawing.Unit(this.tableRowsHeight, Telerik.Reporting.Drawing.UnitType.Pixel);
            detail.Name = "detail";
            // 
            // pageFooter
            // 
            pageFooter.Height = new Telerik.Reporting.Drawing.Unit(20, Telerik.Reporting.Drawing.UnitType.Pixel);
            pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
                pageInfoTextBox});
            pageFooter.Name = "pageFooter";
            //
            //pageInfoTextBox
            //
            pageInfoTextBox.Name = "pageInfoTextBox";
            pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(
                new Telerik.Reporting.Drawing.Unit(100, Telerik.Reporting.Drawing.UnitType.Pixel),
                new Telerik.Reporting.Drawing.Unit(20, Telerik.Reporting.Drawing.UnitType.Pixel));
            pageInfoTextBox.Dock = DockStyle.Left;
            pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            pageInfoTextBox.Value = "=PageNumber + ' of '+ PageCount";
            // 
            // report
            // 
            this.report.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
                pageHeader,
                detail,
                pageFooter});
            this.report.PageSettings.Landscape = pageLandScape;
            this.report.PageSettings.Margins.Bottom = bottomMargin;
            this.report.PageSettings.Margins.Left = leftMargin;
            this.report.PageSettings.Margins.Right = rightMargin;
            this.report.PageSettings.Margins.Top = topMargin;
            this.report.PageSettings.PaperKind = paperKind;


            if (!repeatTableHeader)
            {
                reportHeader = new ReportHeaderSection();

                //
                //reportHeader
                //
                reportHeader.Height =
                    new Telerik.Reporting.Drawing.Unit(
                    this.tableHeaderHeight, Telerik.Reporting.Drawing.UnitType.Pixel);
                reportHeader.Name = "reportHeader";

                this.report.Items.Add(reportHeader);
            }

            ((System.ComponentModel.ISupportInitialize)report).EndInit();
        }

        /// <summary>
        /// Genarate a DataSet from RadGridView data
        /// </summary>
        /// <param name="grid">Source RadGridView</param>
        private void CreateDataSet(RadGridView grid)
        {
            DataTable masterTable = CreateTableFromTemplate(grid.MasterGridViewTemplate, "MasterReportTable");
            this.reportDataSet.Tables.Add(masterTable);
        }

        /// <summary>
        /// Create DataTable form GridViewTemplate
        /// </summary>
        /// <param name="gridViewTemplate">GridViewTemplate which contains data</param>
        /// <param name="tableName">Table name</param>
        /// <returns>DataTable which contains data from GridViewTemplate</returns>
        private DataTable CreateTableFromTemplate(GridViewTemplate gridViewTemplate, string tableName)
        {
            if ((gridViewTemplate != null) && (tableName != ""))
            {
                DataTable table = new DataTable(tableName);

                for (int col = 0; col < gridViewTemplate.ColumnCount; col++)
                {

                    table.Columns.Add(gridViewTemplate.Columns[col].HeaderText);
                }
                for (int row = 0; row < gridViewTemplate.RowCount; row++)
                {
                    DataRow tableRow = table.NewRow();
                    int tam = 0;
                    for (int cell = 0; cell < gridViewTemplate.ColumnCount; cell++)
                    {

                        tableRow[tam] = gridViewTemplate.Rows[row].Cells[cell].Value;
                        tam++;
                    }
                    table.Rows.Add(tableRow);
                }

                return table;
            }
            else
            {
                return null;
            }
        }


        private void AddReportData(RadGridView grid)
        {
            if ((grid != null) && (this.reportDataSet.Tables["MasterReportTable"] != null))
            {
                #region CALCULATE REPORT WIDTH

                this.report.DataSource = this.reportDataSet.Tables["MasterReportTable"];

                Telerik.Reporting.Drawing.Unit reportWidth = Telerik.Reporting.Drawing.Unit.Pixel(0);
                double columnWidthSum = 0;

                if (fitToPageSize)
                {
                    foreach (GridViewDataColumn column in grid.Columns)
                    {
                        if (!column.IsGrouped)
                        {
                            columnWidthSum += (double) column.Width;
                        }
                    }
                }

                #endregion

                Group unboundGroup = new Group(true);
                unboundGroup.GroupHeader.Height =
                    Telerik.Reporting.Drawing.Unit.Pixel(tableHeaderHeight);
                unboundGroup.GroupHeader.PrintOnEveryPage = true;
                unboundGroup.GroupFooter.Visible = false;

                if (repeatTableHeader)
                {
                    report.Groups.Add(unboundGroup);
                }

                for (int i = 0; i < grid.ColumnCount; i++)
                {

                    if (!grid.Columns[i].IsGrouped && grid.Columns[i].IsVisible)
                    {

                        #region REPORT HEADER ROW

                        GridViewCellInfo headerCell =
                            (GridViewCellInfo) grid.MasterView.TableHeaderRow.Cells[i];

                        Telerik.Reporting.TextBox captionTextBox = new Telerik.Reporting.TextBox();
                        captionTextBox.Name = headerCell.ColumnInfo.HeaderText + "TextBox";
                        Telerik.Reporting.Drawing.Unit reportTableSize;

                        if (pageLandScape)
                        {
                            reportTableSize =
                                report.PageSettings.PaperSize.Height.Subtract(leftMargin).Subtract(rightMargin);
                        }
                        else
                        {
                            reportTableSize =
                                report.PageSettings.PaperSize.Width.Subtract(leftMargin).Subtract(rightMargin);
                        }
                        if (fitToPageSize)
                        {
                            captionTextBox.Width = reportTableSize.Multiply(
                                ((double) headerCell.ColumnInfo.Width/columnWidthSum));
                        }
                        else
                        {
                            captionTextBox.Width = Telerik.Reporting.Drawing.Unit.Pixel(headerCell.ColumnInfo.Width);
                        }
                        pageWidth += (int) (captionTextBox.Width.Value + 5);
                        captionTextBox.Value = headerCell.ColumnInfo.HeaderText;
                        captionTextBox.Dock = DockStyle.Left;
                        if (headerCell.Style.Font != null)
                        {
                            captionTextBox.Style.Font.Style = headerCell.Style.Font.Style;
                        }
                        captionTextBox.Style.Font.Name = "Times New Roman";
                        captionTextBox.Style.BorderStyle.Default =
                            Telerik.Reporting.Drawing.BorderType.Solid;

                        reportWidth = reportWidth.Add(captionTextBox.Width);

                        #region REPORT HEADER CELLS COLORS

                        if (useGridColors)
                        {
                            captionTextBox.Style.Color = headerCell.Style.ForeColor;
                            captionTextBox.Style.BackgroundColor = ColorMixer(
                                headerCell.Style.BackColor,
                                headerCell.Style.BackColor2,
                                headerCell.Style.BackColor3,
                                headerCell.Style.BackColor4);
                        }
                        else
                        {
                            captionTextBox.Style.BackgroundColor = Color.Gray;
                            captionTextBox.Style.Color = Color.White;
                        }

                        #endregion
                        //anhtran
                        //captionTextBox.Format = headerCell.Format;
                        captionTextBox.Format = grid.Columns[i].FormatString;

                        captionTextBox.Multiline = true;
                        captionTextBox.TextWrap = true;
                        captionTextBox.Style.TextAlign = GetHorisontalAlign(headerCell.ColumnInfo.TextAlignment);
                        captionTextBox.Style.VerticalAlign = GetVerticalAlign(headerCell.ColumnInfo.TextAlignment);

                        if (repeatTableHeader)
                        {
                            unboundGroup.GroupHeader.Items.Add(captionTextBox);
                        }
                        else
                        {
                            this.reportHeader.Items.Add(captionTextBox);
                        }

                        #endregion

                        #region REPORT TABLE ROWS

                        if (ReportGroup == false)
                        {
                            if (grid.Columns[i].GetType() == typeof (GridViewDateTimeColumn))
                            {
                                Telerik.Reporting.TextBox dataTextBox = new Telerik.Reporting.TextBox();
                                dataTextBox.Name = grid.Columns[i].HeaderText + "FieldTextBox";
                                dataTextBox.Size = captionTextBox.Size;
                                dataTextBox.Dock = DockStyle.Left;
                                dataTextBox.Value = "= CDate(Fields.[" + grid.Columns[i].HeaderText + "])";
                                dataTextBox.Format = "{0:dd/MM/yyyy}";
                                dataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
                                this.detail.Items.Add(dataTextBox);
                            }
                            else if (grid.Columns[i].GetType() == typeof (GridViewImageColumn))
                            {

                                Telerik.Reporting.TextBox dataTextBox = new Telerik.Reporting.TextBox();
                                dataTextBox.Name = grid.Columns[i].HeaderText + "FieldTextBox";
                                dataTextBox.Size = captionTextBox.Size;
                                dataTextBox.Dock = DockStyle.Left;
                                dataTextBox.Value = "=Fields.[" + grid.Columns[i].HeaderText + "]";
                                dataTextBox.Format = grid.Columns[i].FormatString;
                                dataTextBox.Style.Font.Name = "Times New Roman";
                                dataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
                                this.detail.Items.Add(dataTextBox);
                            }
                            else
                            {
                                Telerik.Reporting.TextBox dataTextBox = new Telerik.Reporting.TextBox();
                                dataTextBox.Name = grid.Columns[i].HeaderText + "FieldTextBox";
                                dataTextBox.Size = captionTextBox.Size;
                                dataTextBox.Dock = DockStyle.Left;
                                dataTextBox.Value = "=Fields.[" + grid.Columns[i].HeaderText + "]";
                                dataTextBox.Format = grid.Columns[i].FormatString;
                                dataTextBox.Style.Font.Name = "Times New Roman";
                                dataTextBox.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
                                this.detail.Items.Add(dataTextBox);
                            }
                        }
                        else
                        {
                            detail.Height = Telerik.Reporting.Drawing.Unit.Mm(0);
                        }

                        #endregion
                    }
                }

                this.report.Width = reportWidth;

                #region GROUPS

                //anh tran
                //for (int i = 0; i < grid.MasterTemplate.GroupDescriptors.Count; i++)
                //{
                //    for (int j = 0; j < grid.MasterTemplate.GroupDescriptors[i].GroupNames.Count; j++)
                //    {
                //        Group group = new Group(true);

                //        Telerik.Reporting.TextBox groupTitleTextBox = new Telerik.Reporting.TextBox();
                //        groupTitleTextBox.Dock = DockStyle.Fill;
                //        groupTitleTextBox.Width = reportWidth;
                //        groupTitleTextBox.Value =
                //            String.Format(
                //                "=\"{0}: \" + CStr(IsNull(Fields.[{1}],\"\") + \" : \" + Count(Fields.[{1}]) + \" Người\" )",
                //                grid.MasterTemplate.GroupDescriptors[i].GroupNames[j].PropertyName,
                //                grid.MasterTemplate.GroupDescriptors[i].GroupNames[j].PropertyName);
                //        groupTitleTextBox.Style.Font.Style = FontStyle.Bold;
                //        GridViewGroupRowInfo groupCell = grid.MasterTemplate.Groups[0].GroupRow;
                //        groupTitleTextBox.Style.BackgroundColor = ColorMixer(
                //            groupCell.BackColor,
                //            groupCell.BackColor2,
                //            groupCell.BackColor3,
                //            groupCell.BackColor4);
                //        groupTitleTextBox.Style.Color = groupCell.ForeColor;
                //        groupTitleTextBox.Style.Font.Style = groupCell.Font.Style;
                //        groupTitleTextBox.Style.Font.Name = "Times New Roman";
                //        groupTitleTextBox.Style.BorderStyle.Default =
                //            Telerik.Reporting.Drawing.BorderType.Solid;
                //        groupTitleTextBox.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
                //        group.GroupHeader.Items.Add(groupTitleTextBox);
                //        group.GroupHeader.Height = Telerik.Reporting.Drawing.Unit.Pixel(tableHeaderHeight - 10);
                //        group.GroupHeader.KeepTogether = true;
                //        group.GroupFooter.Height = Telerik.Reporng.Drawing.Unit.Pixel(1);
                //        group.GroupFooter.KeepTogether = true;
                //        group.GroupKeepTogether = GroupKeepTogether.FirstDetail;
                //        group.Grouping.Add(String.Format("=Fields.[{0}]",
                //            grid.MasterGridViewTemplate.GroupByExpressions[i].SelectFields[j].HeaderText));

                //        report.Groups.Add(group);
                //    }
                //}

                #endregion

                #region GroupSummary

                if (grid.MasterTemplate.SummaryRowsTop.Count != 0)
                {
                    foreach (GridViewSummaryRowItem item in grid.MasterTemplate.SummaryRowsTop)
                    {
                        Group groupunbound = new Group(true);
                        int value = 0;
                        if (item.Count != 0)
                        {
                            for (int i = 0; i < grid.ColumnCount; i++)
                            {
                                if (!grid.Columns[i].IsGrouped && grid.Columns[i].IsVisible)
                                {
                                    GridViewColumn headerCell =
                                        grid.MasterView.TableHeaderRow.Cells[i].ColumnInfo;
                                    Telerik.Reporting.TextBox captionTextBox = new Telerik.Reporting.TextBox();
                                    captionTextBox.Width = Telerik.Reporting.Drawing.Unit.Pixel(headerCell.Width);
                                    Telerik.Reporting.TextBox groupTitleTextBoxUnbound = new Telerik.Reporting.TextBox();
                                    groupTitleTextBoxUnbound.Width =
                                        new Telerik.Reporting.Drawing.Unit(
                                            grid.MasterTemplate.Columns[i].Width,
                                            Telerik.Reporting.Drawing.UnitType.Pixel);
                                    groupTitleTextBoxUnbound.Value += "";
                                    groupTitleTextBoxUnbound.Style.Font.Style = FontStyle.Bold;
                                    groupTitleTextBoxUnbound.Style.Font.Name = "Times New Roman";
                                    groupTitleTextBoxUnbound.Style.VerticalAlign =
                                        Telerik.Reporting.Drawing.VerticalAlign.Bottom;
                                    groupTitleTextBoxUnbound.Dock = DockStyle.Left;
                                    groupTitleTextBoxUnbound.Size = captionTextBox.Size;
                                    foreach (GridViewSummaryItem detail in item)
                                    {
                                        if (grid.MasterTemplate.Columns[i].FieldName == detail.Name)
                                        {
                                            if (detail.Aggregate == GridAggregateFunction.Count)
                                            {
                                                groupTitleTextBoxUnbound.Value +=
                                                    String.Format("=\"Số lượng: \" + Count(Fields.[{0}])",
                                                        grid.MasterTemplate.Columns[detail.Name].HeaderText);
                                            }
                                            else if (detail.Aggregate == GridAggregateFunction.Avg)
                                            {
                                                groupTitleTextBoxUnbound.Value +=
                                                    String.Format("=\"Trung bình: \" + Avg(CInt(Fields.[{0}]))",
                                                        grid.MasterTemplate.Columns[detail.Name].HeaderText);
                                            }
                                            else if (detail.Aggregate == GridAggregateFunction.Last)
                                            {
                                                groupTitleTextBoxUnbound.Value +=
                                                    String.Format("=\"Cuối cùng: \" + Last(Fields.[{0}])",
                                                        grid.MasterTemplate.Columns[detail.Name].HeaderText);
                                            }
                                            else if (detail.Aggregate == GridAggregateFunction.Max)
                                            {
                                                groupTitleTextBoxUnbound.Value +=
                                                    String.Format("=\"Lớn nhất: \" + Max(Fields.[{0}])",
                                                        grid.MasterTemplate.Columns[detail.Name].HeaderText);
                                            }
                                            else if (detail.Aggregate == GridAggregateFunction.Min)
                                            {
                                                groupTitleTextBoxUnbound.Value +=
                                                    String.Format("=\"Nhỏ nhất: \" + Min(Fields.[{0}])",
                                                        grid.MasterTemplate.Columns[detail.Name].HeaderText);
                                            }
                                            else if (detail.Aggregate == GridAggregateFunction.Sum)
                                            {
                                                try
                                                {
                                                    groupTitleTextBoxUnbound.Value +=
                                                        String.Format("=\"Tổng: \" + Sum(CInt(Fields.[{0}]))",
                                                            grid.MasterTemplate.Columns[detail.FieldName]
                                                                .HeaderText);
                                                }
                                                catch
                                                {
                                                }
                                            }
                                        }
                                    }
                                    groupunbound.GroupHeader.Items.Insert(value, groupTitleTextBoxUnbound);
                                    groupunbound.GroupHeader.Height =
                                        Telerik.Reporting.Drawing.Unit.Pixel(tableHeaderHeight);
                                    groupunbound.GroupHeader.KeepTogether = true;
                                    groupunbound.GroupFooter.Height = Telerik.Reporting.Drawing.Unit.Pixel(1);
                                    groupunbound.GroupFooter.KeepTogether = true;
                                    value++;
                                }
                            }
                        }
                        report.Groups.Add(groupunbound);
                    }
                }

                if (grid.MasterTemplate.SummaryRowsBottom.Count != 0)
                {

                    foreach (GridViewSummaryRowItem item in grid.MasterTemplate.SummaryRowsBottom)
                    {
                        Group groupunbound = new Group(true);
                        int value = 0;
                        if (item.Count != 0)
                        {
                            for (int i = 0; i < grid.ColumnCount; i++)
                            {
                                if (!grid.Columns[i].IsGrouped && grid.Columns[i].IsVisible)
                                {
                                    GridViewColumn headerCell =
                                        grid.MasterView.TableHeaderRow.Cells[i].ColumnInfo;
                                    Telerik.Reporting.TextBox captionTextBox = new Telerik.Reporting.TextBox();
                                    captionTextBox.Width = Telerik.Reporting.Drawing.Unit.Pixel(headerCell.Width);
                                    Telerik.Reporting.TextBox groupTitleTextBoxUnbound = new Telerik.Reporting.TextBox();
                                    groupTitleTextBoxUnbound.Width =
                                        new Telerik.Reporting.Drawing.Unit(
                                            grid.MasterTemplate.Columns[i].Width,
                                            Telerik.Reporting.Drawing.UnitType.Pixel);
                                    groupTitleTextBoxUnbound.Value += "";
                                    groupTitleTextBoxUnbound.Style.Font.Style = FontStyle.Bold;
                                    groupTitleTextBoxUnbound.Style.VerticalAlign =
                                        Telerik.Reporting.Drawing.VerticalAlign.Bottom;
                                    groupTitleTextBoxUnbound.Dock = DockStyle.Left;
                                    groupTitleTextBoxUnbound.Size = captionTextBox.Size;
                                    foreach (GridViewSummaryItem detail in item)
                                    {
                                        if (grid.MasterTemplate.Columns[i].FieldName == detail.Name)
                                        {
                                            if (detail.Aggregate == GridAggregateFunction.Count)
                                            {
                                                groupTitleTextBoxUnbound.Value +=
                                                    String.Format("=\"Số lượng: \" + Count(Fields.[{0}])",
                                                        grid.MasterTemplate.Columns[detail.Name].HeaderText);
                                            }
                                            else if (detail.Aggregate == GridAggregateFunction.Avg)
                                            {
                                                groupTitleTextBoxUnbound.Value +=
                                                    String.Format("=\"Trung bình: \" + Avg(CInt(Fields.[{0}]))",
                                                        grid.MasterTemplate.Columns[detail.Name].HeaderText);
                                            }
                                            else if (detail.Aggregate == GridAggregateFunction.Last)
                                            {
                                                groupTitleTextBoxUnbound.Value +=
                                                    String.Format("=\"Cuối cùng: \" + Last(Fields.[{0}])",
                                                        grid.MasterTemplate.Columns[detail.Name].HeaderText);
                                            }
                                            else if (detail.Aggregate == GridAggregateFunction.Max)
                                            {
                                                groupTitleTextBoxUnbound.Value +=
                                                    String.Format("=\"Lớn nhất: \" + Max(Fields.[{0}])",
                                                        grid.MasterTemplate.Columns[detail.Name].HeaderText);
                                            }
                                            else if (detail.Aggregate == GridAggregateFunction.Min)
                                            {
                                                groupTitleTextBoxUnbound.Value +=
                                                    String.Format("=\"Nhỏ nhất: \" + Min(Fields.[{0}])",
                                                        grid.MasterTemplate.Columns[detail.Name].HeaderText);
                                            }
                                            else if (detail.Aggregate == GridAggregateFunction.Sum)
                                            {
                                                groupTitleTextBoxUnbound.Value +=
                                                    String.Format("=\"Tổng: \" + Sum(CInt(Fields.[{0}]))",
                                                        grid.MasterTemplate.Columns[detail.Name].HeaderText);
                                            }
                                        }
                                    }
                                    groupunbound.GroupFooter.Items.Insert(value, groupTitleTextBoxUnbound);
                                    groupunbound.GroupFooter.Height =
                                        Telerik.Reporting.Drawing.Unit.Pixel(tableHeaderHeight);
                                    groupunbound.GroupFooter.KeepTogether = true;
                                    groupunbound.GroupHeader.Height = Telerik.Reporting.Drawing.Unit.Pixel(1);
                                    groupunbound.GroupHeader.KeepTogether = true;
                                    value++;
                                }
                            }
                        }
                        report.Groups.Add(groupunbound);
                    }
                }

                #endregion
            }
        }



        /// <summary>
        /// Get Header and Row sizes for Report from provided RadGridView
        /// </summary>
        /// <param name="grid">Source RadGridView</param>
        private void GetGridDimensions(RadGridView grid)
        {
            this.tableHeaderHeight = grid.RootElement.Children[0].Children[0].Children[0].Size.Height;

            this.tableRowsHeight = GetRowHeight(grid);
        }


        /// <summary>
        /// Get average DataRows height
        /// </summary>
        /// <param name="grid">Source RadGridView</param>
        /// <returns>Average height</returns>
        private int GetRowHeight(RadGridView grid)
        {
            int rowHeightSum = 0;
            int rowSum = 0;

            for (int i = 0; i < grid.RowCount; i++)
            {
                rowHeightSum += grid.Rows[i].GetActualHeight(grid.GridElement);
                rowSum++;
            }

            int childTemplatesCount = grid.MasterGridViewTemplate.ChildGridViewTemplates.Count;

            for (int i = 0; i < childTemplatesCount; i++)
            {
                int rowCount = grid.MasterGridViewTemplate.ChildGridViewTemplates[i].RowCount;
                for (int j = 0; j < rowCount; j++)
                {
                    rowHeightSum += grid.MasterGridViewTemplate.ChildGridViewTemplates[i].Rows[j].GetActualHeight(grid.GridElement);
                    rowSum++;
                }
            }

            int averageHeight = rowHeightSum / rowSum;

            int height = 20;

            if (averageHeight > 0)
            {
                height = averageHeight;
            }

            return height;
        }

        private Telerik.Reporting.Drawing.HorizontalAlign GetHorisontalAlign(ContentAlignment align)
        {
            if ((align == ContentAlignment.BottomRight) || (align == ContentAlignment.MiddleRight) ||
                (align == ContentAlignment.TopRight))
            {
                return Telerik.Reporting.Drawing.HorizontalAlign.Right;
            }
            else if ((align == ContentAlignment.BottomLeft) || (align == ContentAlignment.MiddleLeft) ||
                (align == ContentAlignment.TopLeft))
            {
                return Telerik.Reporting.Drawing.HorizontalAlign.Left;
            }
            else
            {
                return Telerik.Reporting.Drawing.HorizontalAlign.Center;
            }
        }

        private Telerik.Reporting.Drawing.VerticalAlign GetVerticalAlign(ContentAlignment align)
        {
            if ((align == ContentAlignment.BottomCenter) || (align == ContentAlignment.BottomLeft) ||
                            (align == ContentAlignment.BottomRight))
            {
                return Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            }
            else if ((align == ContentAlignment.TopCenter) || (align == ContentAlignment.TopLeft) ||
                            (align == ContentAlignment.TopRight))
            {
                return Telerik.Reporting.Drawing.VerticalAlign.Top;
            }
            else
            {
                return Telerik.Reporting.Drawing.VerticalAlign.Middle;
            }
        }

        /// <summary>
        /// Color blend fuction to mix several colors
        /// </summary>
        /// <param name="colors">Color to include in mixer</param>
        /// <returns>Blended colors</returns>
        private Color ColorMixer(params Color[] colors)
        {
            int colorR = 0;
            int colorG = 0;
            int colorB = 0;

            for (int i = 0; i < colors.Length; i++)
            {
                colorR += colors[i].R;
                colorG += colors[i].G;
                colorB += colors[i].B;
            }

            colorR = colorR / colors.Length;
            colorG = colorG / colors.Length;
            colorB = colorB / colors.Length;
            Color mixedColor = Color.FromArgb(colorR, colorG, colorB);

            return mixedColor;
        }
    }
}
