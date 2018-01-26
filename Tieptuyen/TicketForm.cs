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

namespace Tieptuyen
{
    public partial class TicketForm : Telerik.WinControls.UI.RadForm
    {
        private readonly bool IsEdit;
        private readonly IList<Ticket> _tickets;
        public TicketForm(bool isEdit, IList<Ticket> tickets)
        {
            this.IsEdit = isEdit;
            this._tickets = tickets;
            InitializeComponent();
            if (this.IsEdit)
            {
                btAddNew.Visible = false;
                btUpdate.Visible = true;
                btUpdate.Location = btAddNew.Location;
                grvTicket.AllowAddNewRow = false;
                grvTicket.AllowDeleteRow = false;
                if (tickets != null)
                {
                    grvTicket.DataSource = tickets;
                }
            }
            else
            {
                btAddNew.Visible = true;
                btUpdate.Visible = false;
                grvTicket.AllowAddNewRow = true;
                grvTicket.AllowDeleteRow = true;
            }
        }

        private void ImportTicket_Load(object sender, EventArgs e)
        {
            grvTicket.MasterTemplate.ShowFilterCellOperatorText = false;
        }                               
    }
}

