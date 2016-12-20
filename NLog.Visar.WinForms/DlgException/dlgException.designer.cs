namespace NLog.Visar.WinForms
{
    partial class DlgException
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgException));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BtnDetails = new System.Windows.Forms.Button();
            this.imglstForButton = new System.Windows.Forms.ImageList(this.components);
            this.BtnContinue = new System.Windows.Forms.Button();
            this.LblExceptionMessage = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnCopy = new System.Windows.Forms.Button();
            this.TbxExceptionDescription = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = NLog.Visar.WinForms.Properties.Resources.WindowsError;
            this.pictureBox1.Location = new System.Drawing.Point(17, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(33, 33);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // BtnDetails
            // 
            this.BtnDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnDetails.ImageIndex = 0;
            this.BtnDetails.ImageList = this.imglstForButton;
            this.BtnDetails.Location = new System.Drawing.Point(11, 7);
            this.BtnDetails.Name = "BtnDetails";
            this.BtnDetails.Size = new System.Drawing.Size(42, 23);
            this.BtnDetails.TabIndex = 1;
            this.BtnDetails.UseVisualStyleBackColor = true;
            this.BtnDetails.Click += new System.EventHandler(this.BtnDetails_Click);
            // 
            // imglstForButton
            // 
            this.imglstForButton.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstForButton.ImageStream")));
            this.imglstForButton.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstForButton.Images.SetKeyName(0, "error_report_down.png");
            this.imglstForButton.Images.SetKeyName(1, "error_report_up.png");
            // 
            // BtnContinue
            // 
            this.BtnContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnContinue.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnContinue.Location = new System.Drawing.Point(314, 7);
            this.BtnContinue.Name = "BtnContinue";
            this.BtnContinue.Size = new System.Drawing.Size(95, 23);
            this.BtnContinue.TabIndex = 0;
            this.BtnContinue.Text = "Продолжить";
            this.BtnContinue.UseVisualStyleBackColor = true;
            this.BtnContinue.Click += new System.EventHandler(this.BtnContinue_Click);
            // 
            // LblExceptionMessage
            // 
            this.LblExceptionMessage.AutoSize = true;
            this.LblExceptionMessage.Location = new System.Drawing.Point(62, 11);
            this.LblExceptionMessage.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
            this.LblExceptionMessage.MaximumSize = new System.Drawing.Size(360, 0);
            this.LblExceptionMessage.Name = "LblExceptionMessage";
            this.LblExceptionMessage.Size = new System.Drawing.Size(358, 26);
            this.LblExceptionMessage.TabIndex = 0;
            this.LblExceptionMessage.Text = "Ошибка соединения с сервисом. Для дальнейшей работы требуется успешное прохождени" +
    "е авторизации";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.BtnCopy);
            this.panel1.Controls.Add(this.BtnContinue);
            this.panel1.Controls.Add(this.BtnDetails);
            this.panel1.Location = new System.Drawing.Point(0, 68);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(421, 38);
            this.panel1.TabIndex = 1;
            // 
            // BtnCopy
            // 
            this.BtnCopy.Image = NLog.Visar.WinForms.Properties.Resources.page_copy;
            this.BtnCopy.Location = new System.Drawing.Point(57, 7);
            this.BtnCopy.Name = "BtnCopy";
            this.BtnCopy.Size = new System.Drawing.Size(25, 23);
            this.BtnCopy.TabIndex = 2;
            this.BtnCopy.UseVisualStyleBackColor = true;
            this.BtnCopy.Click += new System.EventHandler(this.button1_Click);
            // 
            // TbxExceptionDescription
            // 
            this.TbxExceptionDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TbxExceptionDescription.Location = new System.Drawing.Point(11, 112);
            this.TbxExceptionDescription.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
            this.TbxExceptionDescription.Multiline = true;
            this.TbxExceptionDescription.Name = "TbxExceptionDescription";
            this.TbxExceptionDescription.ReadOnly = true;
            this.TbxExceptionDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TbxExceptionDescription.Size = new System.Drawing.Size(399, 193);
            this.TbxExceptionDescription.TabIndex = 2;
            this.TbxExceptionDescription.Text = "Подробная информация об ошибке:";
            this.TbxExceptionDescription.Visible = false;
            this.TbxExceptionDescription.WordWrap = false;
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 400;
            // 
            // dlgException
            // 
            this.AcceptButton = this.BtnContinue;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.BtnContinue;
            this.ClientSize = new System.Drawing.Size(421, 311);
            this.Controls.Add(this.TbxExceptionDescription);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.LblExceptionMessage);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgException";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ошибка";
            this.Load += new System.EventHandler(this.dlgException_Load);
            this.Shown += new System.EventHandler(this.dlgException_Shown);
            this.Resize += new System.EventHandler(this.dlgException_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button BtnDetails;
        private System.Windows.Forms.Button BtnContinue;
        private System.Windows.Forms.Label LblExceptionMessage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TbxExceptionDescription;
        private System.Windows.Forms.ImageList imglstForButton;
        private System.Windows.Forms.Button BtnCopy;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}