namespace VSDCRequestSubmitter
{
    partial class VSDCRequestSubmitter
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
            this.txtRequest = new System.Windows.Forms.TextBox();
            this.btnSubmittRequest = new System.Windows.Forms.Button();
            this.txtVSDCAddress = new System.Windows.Forms.TextBox();
            this.lblRequest = new System.Windows.Forms.Label();
            this.lblVSDCAddress = new System.Windows.Forms.Label();
            this.lvlCertificateSubjectName = new System.Windows.Forms.Label();
            this.txtCertificateName = new System.Windows.Forms.TextBox();
            this.txtPAC = new System.Windows.Forms.TextBox();
            this.lblPAC = new System.Windows.Forms.Label();
            this.comboBoxAcceptLanguage = new System.Windows.Forms.ComboBox();
            this.lblAcceptLanguage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtRequest
            // 
            this.txtRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRequest.Location = new System.Drawing.Point(12, 157);
            this.txtRequest.Multiline = true;
            this.txtRequest.Name = "txtRequest";
            this.txtRequest.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRequest.Size = new System.Drawing.Size(645, 228);
            this.txtRequest.TabIndex = 0;
            // 
            // btnSubmittRequest
            // 
            this.btnSubmittRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmittRequest.Location = new System.Drawing.Point(673, 405);
            this.btnSubmittRequest.Name = "btnSubmittRequest";
            this.btnSubmittRequest.Size = new System.Drawing.Size(124, 43);
            this.btnSubmittRequest.TabIndex = 1;
            this.btnSubmittRequest.Text = "Submitt";
            this.btnSubmittRequest.UseVisualStyleBackColor = true;
            this.btnSubmittRequest.Click += new System.EventHandler(this.btnSubmittRequest_Click);
            // 
            // txtVSDCAddress
            // 
            this.txtVSDCAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVSDCAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVSDCAddress.Location = new System.Drawing.Point(12, 28);
            this.txtVSDCAddress.Name = "txtVSDCAddress";
            this.txtVSDCAddress.Size = new System.Drawing.Size(645, 30);
            this.txtVSDCAddress.TabIndex = 2;
            // 
            // lblRequest
            // 
            this.lblRequest.AutoSize = true;
            this.lblRequest.Location = new System.Drawing.Point(9, 137);
            this.lblRequest.Name = "lblRequest";
            this.lblRequest.Size = new System.Drawing.Size(65, 17);
            this.lblRequest.TabIndex = 3;
            this.lblRequest.Text = "Request:";
            // 
            // lblVSDCAddress
            // 
            this.lblVSDCAddress.AutoSize = true;
            this.lblVSDCAddress.Location = new System.Drawing.Point(9, 8);
            this.lblVSDCAddress.Name = "lblVSDCAddress";
            this.lblVSDCAddress.Size = new System.Drawing.Size(147, 17);
            this.lblVSDCAddress.TabIndex = 4;
            this.lblVSDCAddress.Text = "VSDC Target Address";
            // 
            // lvlCertificateSubjectName
            // 
            this.lvlCertificateSubjectName.AutoSize = true;
            this.lvlCertificateSubjectName.Location = new System.Drawing.Point(9, 71);
            this.lvlCertificateSubjectName.Name = "lvlCertificateSubjectName";
            this.lvlCertificateSubjectName.Size = new System.Drawing.Size(200, 17);
            this.lvlCertificateSubjectName.TabIndex = 6;
            this.lvlCertificateSubjectName.Text = "Certificate Name (Subject CN):";
            // 
            // txtCertificateName
            // 
            this.txtCertificateName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCertificateName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCertificateName.Location = new System.Drawing.Point(12, 91);
            this.txtCertificateName.Name = "txtCertificateName";
            this.txtCertificateName.Size = new System.Drawing.Size(645, 30);
            this.txtCertificateName.TabIndex = 5;
            // 
            // txtPAC
            // 
            this.txtPAC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPAC.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPAC.Location = new System.Drawing.Point(12, 408);
            this.txtPAC.Name = "txtPAC";
            this.txtPAC.Size = new System.Drawing.Size(645, 30);
            this.txtPAC.TabIndex = 7;
            // 
            // lblPAC
            // 
            this.lblPAC.AutoSize = true;
            this.lblPAC.Location = new System.Drawing.Point(12, 388);
            this.lblPAC.Name = "lblPAC";
            this.lblPAC.Size = new System.Drawing.Size(39, 17);
            this.lblPAC.TabIndex = 8;
            this.lblPAC.Text = "PAC:";
            // 
            // comboBoxAcceptLanguage
            // 
            this.comboBoxAcceptLanguage.FormattingEnabled = true;
            this.comboBoxAcceptLanguage.Items.AddRange(new object[] {
            "default",
            "en-US",
            "sr-Cyrl-RS"});
            this.comboBoxAcceptLanguage.Location = new System.Drawing.Point(673, 331);
            this.comboBoxAcceptLanguage.Name = "comboBoxAcceptLanguage";
            this.comboBoxAcceptLanguage.Size = new System.Drawing.Size(121, 24);
            this.comboBoxAcceptLanguage.TabIndex = 9;
            // 
            // lblAcceptLanguage
            // 
            this.lblAcceptLanguage.AutoSize = true;
            this.lblAcceptLanguage.Location = new System.Drawing.Point(670, 302);
            this.lblAcceptLanguage.Name = "lblAcceptLanguage";
            this.lblAcceptLanguage.Size = new System.Drawing.Size(120, 17);
            this.lblAcceptLanguage.TabIndex = 10;
            this.lblAcceptLanguage.Text = "Accept-Language\r\n";
            // 
            // VSDCRequestSubmitter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblAcceptLanguage);
            this.Controls.Add(this.comboBoxAcceptLanguage);
            this.Controls.Add(this.lblPAC);
            this.Controls.Add(this.txtPAC);
            this.Controls.Add(this.lvlCertificateSubjectName);
            this.Controls.Add(this.txtCertificateName);
            this.Controls.Add(this.lblVSDCAddress);
            this.Controls.Add(this.lblRequest);
            this.Controls.Add(this.txtVSDCAddress);
            this.Controls.Add(this.btnSubmittRequest);
            this.Controls.Add(this.txtRequest);
            this.Name = "VSDCRequestSubmitter";
            this.Text = "VSDC Request Submitter";
            this.Load += new System.EventHandler(this.VSDCRequestSubmitter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRequest;
        private System.Windows.Forms.Button btnSubmittRequest;
        private System.Windows.Forms.TextBox txtVSDCAddress;
        private System.Windows.Forms.Label lblRequest;
        private System.Windows.Forms.Label lblVSDCAddress;
        private System.Windows.Forms.Label lvlCertificateSubjectName;
        private System.Windows.Forms.TextBox txtCertificateName;
        private System.Windows.Forms.TextBox txtPAC;
        private System.Windows.Forms.Label lblPAC;
        private System.Windows.Forms.ComboBox comboBoxAcceptLanguage;
        private System.Windows.Forms.Label lblAcceptLanguage;
    }
}

