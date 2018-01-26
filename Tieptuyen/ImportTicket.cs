using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Telerik.Reporting;
using Telerik.WinControls;
using Tieptuyen.Components.Core.Api.BusinessObjects;
using Tieptuyen.Components.Core.Api.Controllers;
using Tieptuyen.Components.Core.Api.Util;
using System.Linq;

namespace Tieptuyen
{
    public partial class ImportTicket : Telerik.WinControls.UI.RadForm
    {
        public ImportTicket()
        {
            InitializeComponent();
        }

        private void ImportTicket_Load(object sender, EventArgs e)
        {
            grvTicket.MasterTemplate.ShowFilterCellOperatorText = false;
        }

        private void radBtChonFile_Click(object sender, EventArgs e)
        {
            openFileDialog_ImportFile.ShowDialog();
            txtFile.Text = openFileDialog_ImportFile.FileName;
            string filePath = txtFile.Text.Trim();
            CheckFileValid(filePath);
        }

        private bool CheckFileValid(string filepath)
        {
            if (string.IsNullOrEmpty(filepath))
            {
                MessageBox.Show(@"Bạn chưa chọn file cần nhập", @"Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            FileInfo file = new FileInfo(filepath);
            string extention = file.Extension;
            if (!string.IsNullOrEmpty(extention))
            {
                extention = extention.ToUpper();
            }

            if (!extention.Equals(".XLS") && !extention.Equals(".XLSX") && !string.IsNullOrEmpty(extention))
            {
                string msg = "Chỉ hỗ trỡ file Excel!";
                string err = "Lỗi";
                MessageBox.Show(msg, err, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFile.Text = string.Empty;
                return false;
            }

            return true;
        }


        private void radBtNhapTuFile_Click(object sender, EventArgs e)
        {
            string filePath = txtFile.Text.Trim();
            string sheetName = txtSheetName.Text;
            DataTable ticketsTable = new DataTable();
            if (string.IsNullOrEmpty(sheetName))
            {
                MessageBox.Show(@"Bạn phải nhập Tên SHEET cần import", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<Ticket> tickets = new List<Ticket>();

            try
            {
                if (CheckFileValid(filePath))
                {
                    ticketsTable = ExelHelper.ReadDataTableFromSheet(filePath, sheetName);
                    int i = 1;
                    foreach (DataRow row in ticketsTable.Rows)
                    {
                        var newTicket = new Ticket();
                        try
                        {
                            if (!string.IsNullOrEmpty(row["TicketNo"].ToString()))
                            {

                                if (!string.IsNullOrEmpty(row["Imported"].ToString()))
                                {
                                    newTicket.Imported = Boolean.Parse(row["Imported"].ToString());
                                }

                                if (!string.IsNullOrEmpty(row["IssueDate"].ToString()))
                                {
                                    newTicket.IssueDate = DateTime.ParseExact(row["IssueDate"].ToString(), "d/M/yyyy",
                                        new CultureInfo("vi-vn"));
                                }
                                newTicket.TicketNo = row["TicketNo"].ToString();
                                if (!string.IsNullOrEmpty(row["NoSeats"].ToString()))
                                {
                                    newTicket.NoSeats = Int16.Parse(row["NoSeats"].ToString());
                                }
                                newTicket.FlyNo = row["FlyNo"].ToString();
                                newTicket.Brand = row["Brand"].ToString();
                                newTicket.Class = row["Class"].ToString();
                                newTicket.Journey = row["Journey"].ToString();
                                if (!string.IsNullOrEmpty(row["FlyDate"].ToString()))
                                {
                                    newTicket.FlyDate = DateTime.ParseExact(row["FlyDate"].ToString(), "d/M/yyyy", new CultureInfo("vi-vn"));
                                }
                                newTicket.FlyTime = row["FlyTime"].ToString();
                                newTicket.FlyTime1 = row["FlyTime1"].ToString();
                                newTicket.Passenger = row["Passenger"].ToString();
                                if (!string.IsNullOrEmpty(row["DoB"].ToString()))
                                {
                                    newTicket.DoB = DateTime.ParseExact(row["DoB"].ToString(), "d/M/yyyy", new CultureInfo("vi-vn"));
                                }
                                newTicket.Title = row["Title"].ToString();

                                if (!string.IsNullOrEmpty(row["NetPrice"].ToString()))
                                {
                                    newTicket.NetPrice = Decimal.Parse(row["NetPrice"].ToString());
                                }

                                if (!string.IsNullOrEmpty(row["CommissionL2"].ToString()))
                                {
                                    newTicket.CommissionL2 = Decimal.Parse(row["CommissionL2"].ToString());
                                }

                                if (!string.IsNullOrEmpty(row["ServiceFee"].ToString()))
                                {
                                    newTicket.ServiceFee = Decimal.Parse(row["ServiceFee"].ToString());
                                }

                                if (!string.IsNullOrEmpty(row["PackageFee"].ToString()))
                                {
                                    newTicket.PackageFee = Decimal.Parse(row["PackageFee"].ToString());
                                }

                                if (!string.IsNullOrEmpty(row["Diffirence"].ToString()))
                                {
                                    newTicket.Diffirence = Decimal.Parse(row["Diffirence"].ToString());
                                }

                                if (!string.IsNullOrEmpty(row["Paid"].ToString()))
                                {
                                    newTicket.Paid = Decimal.Parse(row["Paid"].ToString());
                                }

                                if (!string.IsNullOrEmpty(row["Remain"].ToString()))
                                {
                                    newTicket.Remain = Decimal.Parse(row["Remain"].ToString());
                                }

                                if (!string.IsNullOrEmpty(row["TotalPrice"].ToString()))
                                {
                                    newTicket.TotalPrice = Decimal.Parse(row["TotalPrice"].ToString());
                                }

                                if (!string.IsNullOrEmpty(row["CancelFee"].ToString()))
                                {
                                    newTicket.CancelFee = Decimal.Parse(row["CancelFee"].ToString());
                                }

                                if (!string.IsNullOrEmpty(row["Refund"].ToString()))
                                {
                                    newTicket.Refund = Decimal.Parse(row["Refund"].ToString());
                                }

                                if (!string.IsNullOrEmpty(row["ChangeFee"].ToString()))
                                {
                                    newTicket.ChangeFee = Decimal.Parse(row["ChangeFee"].ToString());
                                }

                                if (!string.IsNullOrEmpty(row["Penalty"].ToString()))
                                {
                                    newTicket.Penalty = Decimal.Parse(row["Penalty"].ToString());
                                }

                                if (!string.IsNullOrEmpty(row["ExtraFee"].ToString()))
                                {
                                    newTicket.ExtraFee = Decimal.Parse(row["ExtraFee"].ToString());
                                }

                                if (!string.IsNullOrEmpty(row["CustomerId"].ToString()))
                                {
                                    newTicket.CustomerID = Int16.Parse(row["CustomerId"].ToString());
                                }

                                newTicket.Note = row["Note"].ToString();

                                tickets.Add(newTicket);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(string.Format("Lỗi ở dòng {0} : {1} ", i, ex.Message), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                        finally
                        {
                            i++;
                        }
                    }
                }
                var notImportedTickets = tickets.Where(x => x.Imported == false).ToList();
                grvTicket.DataSource = notImportedTickets;
                lbRealRecords.Text = tickets.Count.ToString();
                lbValidRecords.Text = notImportedTickets.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            grvTicket.DataSource = null;
            this.DialogResult = DialogResult.Cancel;
        }

        private void txtFile_Click(object sender, EventArgs e)
        {
            openFileDialog_ImportFile.ShowDialog();
            txtFile.Text = openFileDialog_ImportFile.FileName;
        }

        private void radImportFromFile_Click(object sender, EventArgs e)
        {
            List<Ticket> ticketsts = grvTicket.DataSource as List<Ticket>;            
            if (ticketsts ==null || ticketsts.Count == 0)
            {
                MessageBox.Show("Không có vé để lưu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SystemItemController controller = new SystemItemController();
            controller.AddTickets(ticketsts);
            MessageBox.Show(@"Import thông tin thành công");
            grvTicket.DataSource = null;
        }       
    }
}
