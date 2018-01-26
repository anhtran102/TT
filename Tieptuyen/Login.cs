using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tieptuyen.Api.Security.Crypto;
using Tieptuyen.Components.Core.Api.BusinessObjects;
using Tieptuyen.Components.Core.Api.Controllers;
using System.Web.Http;
using Microsoft.Practices.Unity;
using Tieptuyen.App_Start;
using Tieptuyen.Components.Core.Api.DataAccess.Repository;
using Tieptuyen.Components.Core.Api.Services;
using Tieptuyen.Components.Globalization.Api;
using Tieptuyen.Components.Globalization.Api.Controllers;
using Tieptuyen.Components.Globalization.Api.Default;

namespace Tieptuyen
{
    public partial class Login : Form
    {
        //private AccessControlController accessControlController;
        private readonly IAccessControlService accessControlService;
        private readonly AccessControlController accessControlController;
        private readonly LanguageController languageController;
        private readonly IUnityContainer container;
        public UserSettings User { get; set; }
        public Login()
        {
            InitializeComponent();           
             container = UnityConfig.GetConfiguredContainer();
            accessControlController = container.Resolve<AccessControlController>();
            LanguageManager l = new LanguageManager();
            languageController = new LanguageController(l);
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show(@"Please enter User Name and Password"); ;
                return;
            }
           
            var username = txtUserName.Text;
            var password = txtPassword.Text;
            try
            {
                var userSettings = accessControlController.GetUserSettings(username, password);
                if (userSettings != null)
                {
                   this.User = userSettings;
                    this.DialogResult = DialogResult.OK;                    
                }
                else
                {
                    MessageBox.Show(@"UserName or password is not correct");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            txtUserName.SelectAll();
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            txtPassword.SelectAll();
        }             
    }
}