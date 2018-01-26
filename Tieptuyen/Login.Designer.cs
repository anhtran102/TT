namespace Tieptuyen
{
    partial class Login
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
            this.btLogin = new Telerik.WinControls.UI.RadButton();
            this.btExit = new Telerik.WinControls.UI.RadButton();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbError = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btExit)).BeginInit();
            this.SuspendLayout();
            // 
            // btLogin
            // 
            this.btLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btLogin.Location = new System.Drawing.Point(122, 153);
            this.btLogin.Name = "btLogin";
            this.btLogin.Size = new System.Drawing.Size(89, 34);
            this.btLogin.TabIndex = 3;
            this.btLogin.Text = "Login";
            this.btLogin.Click += new System.EventHandler(this.btLogin_Click);
            // 
            // btExit
            // 
            this.btExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btExit.Location = new System.Drawing.Point(234, 153);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(86, 34);
            this.btExit.TabIndex = 4;
            this.btExit.Text = "Exit";
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(122, 62);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(198, 20);
            this.txtUserName.TabIndex = 1;
            this.txtUserName.Text = "Administrator";
            this.txtUserName.Enter += new System.EventHandler(this.txtUserName_Enter);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(122, 100);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = 'x';
            this.txtPassword.Size = new System.Drawing.Size(198, 20);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.Text = "letmein123";
            this.txtPassword.Enter += new System.EventHandler(this.txtPassword_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "User Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(36, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // lbError
            // 
            this.lbError.AutoSize = true;
            this.lbError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbError.ForeColor = System.Drawing.Color.Red;
            this.lbError.Location = new System.Drawing.Point(36, 202);
            this.lbError.Name = "lbError";
            this.lbError.Size = new System.Drawing.Size(0, 15);
            this.lbError.TabIndex = 5;
            // 
            // Login
            // 
            this.AcceptButton = this.btLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btExit;
            this.ClientSize = new System.Drawing.Size(383, 261);
            this.ControlBox = false;
            this.Controls.Add(this.lbError);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.btLogin);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LOGIN";
            ((System.ComponentModel.ISupportInitialize)(this.btLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btExit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton btLogin;
        private Telerik.WinControls.UI.RadButton btExit;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbError;
    }
}

