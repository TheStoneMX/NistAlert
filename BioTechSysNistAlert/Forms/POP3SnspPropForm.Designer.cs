namespace BioTechSysNistAlert.Forms
{
    partial class POP3SnspPropForm
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
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.UltraWinToolTip.UltraToolTipInfo ultraToolTipInfo4 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Contraseña del Usuario", Infragistics.Win.ToolTipImage.Default, null, Infragistics.Win.DefaultableBoolean.Default);
            Infragistics.Win.UltraWinToolTip.UltraToolTipInfo ultraToolTipInfo2 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Usuario del cuenta de Correo que envia", Infragistics.Win.ToolTipImage.Default, null, Infragistics.Win.DefaultableBoolean.Default);
            Infragistics.Win.UltraWinToolTip.UltraToolTipInfo ultraToolTipInfo3 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("donde se van a mandar los Nist", Infragistics.Win.ToolTipImage.Default, null, Infragistics.Win.DefaultableBoolean.Default);
            Infragistics.Win.UltraWinToolTip.UltraToolTipInfo ultraToolTipInfo1 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Puerto de Conexcion de mioafissnsp", Infragistics.Win.ToolTipImage.Default, null, Infragistics.Win.DefaultableBoolean.Default);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POP3SnspPropForm));
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OK = new System.Windows.Forms.Button();
            this.snsp_Password = new System.Windows.Forms.TextBox();
            this.snsp_userName = new System.Windows.Forms.TextBox();
            this.snsp_popMail = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.snsp_popPort = new System.Windows.Forms.TextBox();
            this.ultraToolTipManager = new Infragistics.Win.UltraWinToolTip.UltraToolTipManager(this.components);
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 13);
            this.label4.TabIndex = 120;
            this.label4.Text = "Servidor de Correo Entrante:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(139, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 121;
            this.label3.Text = "Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 13);
            this.label2.TabIndex = 122;
            this.label2.Text = "Cuenta de correo del Usuario:";
            // 
            // OK
            // 
            this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OK.Location = new System.Drawing.Point(207, 149);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(145, 23);
            this.OK.TabIndex = 117;
            this.OK.Text = "Guardar Cambios";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // snsp_Password
            // 
            this.snsp_Password.Location = new System.Drawing.Point(210, 57);
            this.snsp_Password.Name = "snsp_Password";
            this.snsp_Password.PasswordChar = '#';
            this.snsp_Password.Size = new System.Drawing.Size(192, 20);
            this.snsp_Password.TabIndex = 111;
            this.snsp_Password.Text = "a12345678";
            this.snsp_Password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            ultraToolTipInfo4.ToolTipText = "Contraseña del Usuario";
            this.ultraToolTipManager.SetUltraToolTip(this.snsp_Password, ultraToolTipInfo4);
            // 
            // snsp_userName
            // 
            this.snsp_userName.Location = new System.Drawing.Point(210, 31);
            this.snsp_userName.Name = "snsp_userName";
            this.snsp_userName.Size = new System.Drawing.Size(192, 20);
            this.snsp_userName.TabIndex = 114;
            this.snsp_userName.Text = "ansi_nist@biometrics-solutions.com";
            this.snsp_userName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            ultraToolTipInfo2.ToolTipText = "Usuario del cuenta de Correo que envia";
            this.ultraToolTipManager.SetUltraToolTip(this.snsp_userName, ultraToolTipInfo2);
            // 
            // snsp_popMail
            // 
            this.snsp_popMail.Location = new System.Drawing.Point(210, 83);
            this.snsp_popMail.Name = "snsp_popMail";
            this.snsp_popMail.Size = new System.Drawing.Size(192, 20);
            this.snsp_popMail.TabIndex = 112;
            this.snsp_popMail.Text = "mail.biometrics-solutions.com";
            this.snsp_popMail.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            ultraToolTipInfo3.ToolTipText = "donde se van a mandar los Nist";
            this.ultraToolTipManager.SetUltraToolTip(this.snsp_popMail, ultraToolTipInfo3);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(123, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 123;
            this.label5.Text = "Puerto POP3:";
            // 
            // snsp_popPort
            // 
            this.snsp_popPort.Location = new System.Drawing.Point(210, 109);
            this.snsp_popPort.Name = "snsp_popPort";
            this.snsp_popPort.Size = new System.Drawing.Size(192, 20);
            this.snsp_popPort.TabIndex = 119;
            this.snsp_popPort.Text = "110";
            this.snsp_popPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            ultraToolTipInfo1.ToolTipText = "Puerto de Conexcion de mioafissnsp";
            this.ultraToolTipManager.SetUltraToolTip(this.snsp_popPort, ultraToolTipInfo1);
            // 
            // ultraToolTipManager
            // 
            this.ultraToolTipManager.ContainingControl = this;
            // 
            // SnspPropForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(525, 211);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.snsp_popPort);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.snsp_Password);
            this.Controls.Add(this.snsp_userName);
            this.Controls.Add(this.snsp_popMail);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SnspPropForm";
            this.Text = "Ventana de Propiedades de Emails";
            this.Load += new System.EventHandler(this.SnspPropForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.TextBox snsp_Password;
        private System.Windows.Forms.TextBox snsp_userName;
        private System.Windows.Forms.TextBox snsp_popMail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox snsp_popPort;
        private Infragistics.Win.UltraWinToolTip.UltraToolTipManager ultraToolTipManager;

    }
}