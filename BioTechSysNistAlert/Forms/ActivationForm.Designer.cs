namespace BioTechSysNistAlert.Forms
{
    partial class ActivationForm
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
            this.txtKey = new System.Windows.Forms.TextBox();
            this.txtSerial = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.btnActivate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtKey
            // 
            this.txtKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKey.Location = new System.Drawing.Point(225, 48);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(304, 20);
            this.txtKey.TabIndex = 6;
            // 
            // txtSerial
            // 
            this.txtSerial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSerial.Location = new System.Drawing.Point(225, 22);
            this.txtSerial.Name = "txtSerial";
            this.txtSerial.ReadOnly = true;
            this.txtSerial.Size = new System.Drawing.Size(304, 20);
            this.txtSerial.TabIndex = 5;
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(148, 22);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(71, 13);
            this.Label1.TabIndex = 4;
            this.Label1.Text = "Serial number";
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(146, 51);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(74, 13);
            this.Label2.TabIndex = 7;
            this.Label2.Text = "Activation key";
            // 
            // btnActivate
            // 
            this.btnActivate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActivate.Location = new System.Drawing.Point(369, 88);
            this.btnActivate.Name = "btnActivate";
            this.btnActivate.Size = new System.Drawing.Size(75, 23);
            this.btnActivate.TabIndex = 8;
            this.btnActivate.Text = "Activate";
            this.btnActivate.UseVisualStyleBackColor = true;
            // 
            // ActivationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 162);
            this.Controls.Add(this.btnActivate);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.txtSerial);
            this.Controls.Add(this.Label1);
            this.Name = "ActivationForm";
            this.Text = "ActivationForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtKey;
        internal System.Windows.Forms.TextBox txtSerial;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button btnActivate;
    }
}