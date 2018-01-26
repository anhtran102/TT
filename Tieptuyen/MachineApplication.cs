using System;
using Telerik.WinControls.UI;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;
using System.IO;
using System.Drawing;

namespace Tieptuyen
{
    public static class MachineApplication
    {
        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            if (imageIn == null)
                return null;
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);

            return ms.ToArray();
        }

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }


        public static void ReportViewer(RadGridView radgrid)
        {
            FormOptions form = new FormOptions();
            form.ShowDialog(null);
            if (form.DialogResult == DialogResult.OK)
            {
                RadGridReport report = new RadGridReport(form.HeaderText);
                report.UseGridColors = form.UseGridColors;
                report.AllMargins = form.SetAllMargins;
                report.PaperKind = form.PaperKind;
                report.ReportGroup = form.ReportGroup;
                report.PageLandScape = form.IsLandScape;
                report.ReportWindowState = FormWindowState.Maximized;
                report.ReportSize = radgrid.Size;
                report.RepeatTableHeader = form.RepeatHeaderRow;
                report.ReportFormShow(null, radgrid);
            }
        }       

        public static DateTime StripDateTime(DateTime dateTime)
        {
            DateTime date = dateTime;
            dateTime = date.Add(new TimeSpan(-date.Hour, -date.Minute, -date.Second));
            return dateTime;
        }      

        /// <summary>
        /// xóa filter và condition formatting của gridview
        /// </summary>
        /// <param name="gridview"></param>
        public static void ClearGridSetting(Telerik.WinControls.UI.RadGridView gridview)
        {
            gridview.MasterTemplate.FilterDescriptors.Clear();
            for (int i = 0; i < gridview.Columns.Count; i++)
            {
                gridview.Columns[i].ConditionalFormattingObjectList.Clear();
            }
        }       

        public static string GetHeaderText(string fieldName)
        {
            ResourceManager rs = new ResourceManager("HumanResourceGarmex.Resource.TableColumn",
                Assembly.GetExecutingAssembly());

            if (fieldName.EndsWith("GroupByMonth"))
                return rs.GetString(fieldName.Substring(0, fieldName.IndexOf("GroupByMonth"))) + " theo tháng";
            else if (fieldName.EndsWith("GroupByYear"))
            {
                return rs.GetString(fieldName.Substring(0, fieldName.IndexOf("GroupByYear"))) + " theo năm";
            }
            return rs.GetString(fieldName);
        }              
    }
}
