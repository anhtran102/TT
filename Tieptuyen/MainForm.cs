using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Tieptuyen.Components.Core.Api.BusinessObjects;
using Tieptuyen.Components.Core.Api.Controllers;
using System.Linq.Dynamic;

namespace Tieptuyen
{
    public partial class MainForm : Telerik.WinControls.UI.RadForm
    {
        private UserSettings userSetting;
        private SystemItemController systemItemController;
        MenuContext MenuContext = null;

        public MainForm()
        {
            InitializeComponent();
            Login login = new Login();
            login.ShowDialog();            
            userSetting = login.User;
            login.Close();
            if (userSetting == null)
            {
                Environment.Exit(0);                
            }

            systemItemController = new SystemItemController();
            LoadData();
        }

        private void LoadData()
        {
            dpSearchFrom.Value = DateTime.Now.AddMonths(-2);
            dpSearchTo.Value = DateTime.Now.AddMonths(2);
            LoadAllAccount();
            LoadCashInfo();
            LoadAllCustomer();
            LoadAllTicket();
        }

        private void btAccAdd_Click(object sender, EventArgs e)
        {            
            if (string.IsNullOrEmpty(txtAccNo.Text) || string.IsNullOrEmpty(txtAccName.Text) || string.IsNullOrEmpty(txtAccBank.Text))
            {
                RadMessageBox.Show("Số tài khoản, tên tài khoản, ngân hàng phải nhập");
                return;
            }

            var account = new Account
            {
                AccountNo = txtAccNo.Text,
                AccountName = txtAccName.Text,
                BankName = txtAccBank.Text,
                Phone = txtAccPhone.Text,
                IsOwn = chbIsOwn.Checked
            };

            try
            {
                this.systemItemController.AddAccount(account);
                ResetAccForm();
                LoadAllAccount();
            }
            catch (Exception)
            {
                RadMessageBox.Show("Có lỗi xảy ra trong quá trình thao tác");
            }
        }

        private void ResetAccForm()
        {
            txtAccNo.Text = string.Empty;
            txtAccName.Text = String.Empty;
            txtAccBank.Text = String.Empty;
            txtAccPhone.Text = string.Empty;
            chbIsOwn.Checked = false;
        }

        private void LoadAllAccount()
        {
            var accounts = systemItemController.GetAccounts(null);

            grvAcc.DataSource = accounts;
        }    

        private void grvAcc_Click(object sender, EventArgs e)
        {
            var accout = (Account)grvAcc.SelectedRows[0].DataBoundItem;
            txtAccNo.Text = accout.AccountNo;
            txtAccName.Text = accout.AccountName;
            txtAccBank.Text = accout.BankName;
            txtAccPhone.Text = accout.Phone;
            chbIsOwn.Checked = accout.IsOwn;
        }

        private void btAccUpdate_Click(object sender, EventArgs e)
        {
            var accout = (Account)grvAcc.SelectedRows[0].DataBoundItem;
            if (string.IsNullOrEmpty(txtAccNo.Text) || string.IsNullOrEmpty(txtAccName.Text) || string.IsNullOrEmpty(txtAccBank.Text))
            {
                RadMessageBox.Show("Số tài khoản, tên tài khoản, ngân hàng phải nhập");
                return;
            }

            accout.AccountNo = txtAccNo.Text;
            accout.AccountName = txtAccName.Text;
            accout.BankName = txtAccBank.Text;
            accout.Phone = txtAccPhone.Text;
            accout.IsOwn = chbIsOwn.Checked;
            systemItemController.UpdateAccount(accout);
            ResetAccForm();
            LoadAllAccount();
        }


        private void LoadCashInfo()
        {
            dpkPayDate.Value = DateTime.Now;
            ddCashAccFrom.DataSource = this.systemItemController.GetAccounts(true);
            ddCashAccTo.DataSource = this.systemItemController.GetAccounts(false);
            LoadAllInputCash();
        }

        private void btCashAdd_Click(object sender, EventArgs e)
        {
            if (numCash.Value == 0)
            {
                RadMessageBox.Show("Nhập số tiền hợp lệ");
                return;
            }

            if (string.IsNullOrEmpty(txtCashPayer.Text))
            {
                RadMessageBox.Show("Bạn phải nhập người chuyển");
                return;
            }

            var cash = new InputCash
            {     
                FromAccount = ddCashAccFrom.SelectedValue.ToString(),
                ToAccount = ddCashAccTo.SelectedValue.ToString(),
                Cash = numCash.Value,
                Payer = txtCashPayer.Text,
                PayDate = dpkPayDate.Value

            };

            try
            {
                this.systemItemController.AddInputCash(cash);
                txtCashPayer.Text = string.Empty;
                numCash.Value = 0;
            }
            catch (Exception ex)
            {
                RadMessageBox.Show("Có lỗi xảy ra trong quá trình thao tác");
            }
        }

        private void LoadAllInputCash()
        {
            grvCash.DataSource = this.systemItemController.GetAllInputCash();
            BindCustomerToControl();
        }

        private void BindCustomerToControl()
        {
            var empty = new Customer { Id = 0, CustomerName = string.Empty };
            var customers = this.systemItemController.GetAllCustomers() ?? new List<Customer>();
            customers.Insert(0, empty);
            ddCustomer.DataSource = customers;
            ddCustomerSearch.DataSource = customers;
        }

        private void btCustomerAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomerName.Text))
            {
                RadMessageBox.Show("Tên khách hàng phải nhập");
                return;
            }

            var customer = new Customer
            {
                CustomerName = txtCustomerName.Text,
                Address = txtCustomerAddr.Text,
                Phone = txtCustomerPhone.Text,
            };

            try
            {
                this.systemItemController.AddCustomer(customer);
                LoadAllCustomer();
                ResetCustomerForm();
            }
            catch (Exception ex)
            {
                RadMessageBox.Show("Có lỗi xảy ra trong quá trình thao tác");
            }
        }

        private void LoadAllCustomer()
        {
            grvCustomer.MasterTemplate.ShowFilterCellOperatorText = false;
            grvCustomer.DataSource = this.systemItemController.GetAllCustomers();
        }

        private void btCustomerUpdate_Click(object sender, EventArgs e)
        {
            var customer = (Customer)grvCustomer.SelectedRows[0].DataBoundItem;
            if (string.IsNullOrEmpty(txtCustomerName.Text))
            {
                RadMessageBox.Show("Tên khách hàng phải nhập");
                return;
            }

            customer.CustomerName = txtCustomerName.Text;
            customer.Address = txtCustomerAddr.Text;
            customer.Phone = txtCustomerPhone.Text;                           

            try
            {
                this.systemItemController.UpdateCustomer(customer);
                LoadAllCustomer();
            }
            catch (Exception)
            {
                RadMessageBox.Show("Có lỗi xảy ra trong quá trình thao tác");
            }
        }

        private void ResetCustomerForm()
        {
            txtCustomerName.Text = string.Empty;
            txtCustomerAddr.Text = string.Empty;
            txtCustomerPhone.Text = string.Empty;
        }

        private void grvCustomer_Click(object sender, EventArgs e)
        {
            if (grvCustomer.SelectedRows.Count >0)
            {
                var customer = (Customer) grvCustomer.SelectedRows[0].DataBoundItem;
                txtCustomerName.Text = customer.CustomerName;
                txtCustomerAddr.Text = customer.Address;
                txtCustomerPhone.Text = customer.Phone;
            }
        }

        private void btCashUpdate_Click(object sender, EventArgs e)
        {
            var cash = (InputCash)grvCash.SelectedRows[0].DataBoundItem;
            if (numCash.Value == 0)
            {
                RadMessageBox.Show("Nhập số tiền hợp lệ");
                return;
            }

            if (string.IsNullOrEmpty(txtCashPayer.Text))
            {
                RadMessageBox.Show("Bạn phải nhập người chuyển");
                return;
            }

            cash.FromAccount = ddCashAccFrom.SelectedValue.ToString();
            cash.ToAccount = ddCashAccTo.SelectedValue.ToString();
            cash.Cash = numCash.Value;
            cash.Payer = txtCashPayer.Text;
            cash.PayDate = dpkPayDate.Value;

            try
            {
                this.systemItemController.UpdateInputCash(cash);
                txtCashPayer.Text = string.Empty;
                numCash.Value = 0;
            }
            catch (Exception ex)
            {
                RadMessageBox.Show("Có lỗi xảy ra trong quá trình thao tác");
            }
        }

        private void RemoveGroupEvaluateEvent(bool isRemove)
        {
            if (isRemove)
            {
                this.grvTicket.GroupSummaryEvaluate -= gridView_GroupSumaryEvaluate;
                this.grvCustomer.GroupSummaryEvaluate -= gridView_GroupSumaryEvaluate;
            }
            else
            {
                this.grvTicket.GroupSummaryEvaluate += gridView_GroupSumaryEvaluate;
                this.grvCustomer.GroupSummaryEvaluate += gridView_GroupSumaryEvaluate;    
            }
        }

        private void gridView_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            RadGridView grid = sender as RadGridView;
            try
            {
                if (grid.CurrentCell == null || grid.Groups.Count > 0)
                {
                    return;
                }

                MenuContext = null;
                MenuContext = new MenuContext(grid);
                MenuContext.removeGroupEvaluateEvent += new MenuContext.RemoveGroupEvaluateEvent(RemoveGroupEvaluateEvent);
                MenuContext.IsColumnChooser = true;
                MenuContext.istongcot = true;
                MenuContext.IsSummaryRow = true;
                MenuContext.IsReport = true;
                MenuContext.GetReportMenu(e.ContextMenu);
            }
            catch (InvalidOperationException ee)
            {
                MessageBox.Show(ee.Message);
            }
            catch (Exception)
            {
                Telerik.WinControls.RadMessageBox.Show(this, "Unknow Exception: có lỗi xảy ra trong quá trình thao tác", "Lỗi", MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Exclamation);
            }
        }

        private void gridView_GroupSumaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            RadGridView grvSender = (RadGridView)sender;
            if (e.Value == null)
            {
                e.FormatString = "Dữ liệu sai";
            }
            else
            {
                if (grvSender.Columns.GetColumnByFieldName(e.SummaryItem.Name).GetType() == typeof(GridViewDateTimeColumn))
                {
                    e.FormatString = "" + string.Format("{0:dd/MM/yyyy}", e.Value) + " : " + e.Group.ItemCount;
                }
                else
                {
                    e.FormatString = "" + e.Value + " : " + e.Group.ItemCount;
                }
            }
        }

        private void LoadAllTicket()
        {
            grvTicket.MasterTemplate.ShowFilterCellOperatorText = false;
            grvTicket.DataSource = this.systemItemController.GetAllTickets();
        }

        private void btNhapTuFile_Click(object sender, EventArgs e)
        {
            ImportTicket importTicket = new ImportTicket();
            importTicket.ShowDialog();
        }

        private void btRefreshTicket_Click(object sender, EventArgs e)
        {
            LoadAllTicket();
        }       

        private void grvTicket_RowValidated(object sender, RowValidatedEventArgs e)
        {

        }       

        private void grvTicket_FilterChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            var tickets = GetFilteredDataSource();
            SetSummaryInfo(tickets);                        
            
        }

        private void SetSummaryInfo(IEnumerable<Ticket> tickets)
        {
            if (tickets == null)
                return;
            decimal SumNet = 0, SumGross = 0, SumHH = 0, SumService = 0, SumPenalty = 0, SumExtra = 0, SumPaid = 0, SumRemain = 0, SumProfit = 0;
            foreach (var ticket in tickets)
            {
                SumNet += (ticket.NetPrice + ticket.PackageFee + ticket.ChangeFee);
                SumGross += ticket.TotalPrice;
                SumHH += ticket.CommissionL2;
                SumService += ticket.ServiceFee;
                SumPenalty += ticket.Penalty;
                SumExtra += ticket.ExtraFee;
                SumPaid += ticket.Paid;
                SumRemain += ticket.Remain;
            }

            SumProfit = SumGross - SumNet - SumService - SumHH - SumPenalty + SumExtra;
            lbSumNet.Text = SumNet.ToString("#,0");
            lbSumGross.Text = SumGross.ToString("#,0");
            lbSumHH.Text = SumHH.ToString("#,0");
            lbSumService.Text = SumService.ToString("#,0");
            lbSumPenalty.Text = SumPenalty.ToString("#,0");
            lbSumExtra.Text = SumExtra.ToString("#,0");
            lbSumPaid.Text = SumPaid.ToString("#,0");
            lbSumRemain.Text = SumRemain.ToString("#,0");
            lbProfit.Text = SumProfit.ToString("#,0");
        }
        private List<Ticket> GetFilteredDataSource()
        {
            List<Ticket> ticketsts = new List<Ticket>();
            foreach(var row in grvTicket.MasterView.Rows){
                var ticket = row.DataBoundItem as Ticket;
                if (ticket != null) 
                {
                    ticketsts.Add(ticket);
                }
            }
            return ticketsts;
            
        }

        private void grvTicket_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            var tickets = grvTicket.DataSource as IEnumerable<Ticket>;
            SetSummaryInfo(tickets);
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            Ticket selected = grvTicket.SelectedRows[0].DataBoundItem as Ticket;
            IEnumerable<Ticket> tickets = grvTicket.DataSource as IEnumerable<Ticket>;
            var sameNoTickets = new List<Ticket>();
            if (selected != null)
            {
                sameNoTickets = tickets.Where(x => x.TicketNo.Equals(selected.TicketNo)).ToList();
            }

            if (sameNoTickets.Count > 0)
            {
                TicketForm ticketForm = new TicketForm(true, sameNoTickets);
                ticketForm.ShowDialog();
            }
        }              
    }
}
