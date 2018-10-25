namespace EHRSoftware
{
    partial class addPatientForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cboSection = new System.Windows.Forms.ComboBox();
            this.cboAdmitted = new System.Windows.Forms.ComboBox();
            this.txtLast = new System.Windows.Forms.TextBox();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.txtFirst = new System.Windows.Forms.TextBox();
            this.lblSection = new System.Windows.Forms.Label();
            this.lblAdmitted = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.lblLast = new System.Windows.Forms.Label();
            this.lblFirst = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Silver;
            this.btnCancel.Location = new System.Drawing.Point(142, 242);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(81, 39);
            this.btnCancel.TabIndex = 28;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Silver;
            this.btnAdd.Location = new System.Drawing.Point(39, 242);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(81, 39);
            this.btnAdd.TabIndex = 27;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cboSection
            // 
            this.cboSection.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cboSection.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSection.FormattingEnabled = true;
            this.cboSection.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D"});
            this.cboSection.Location = new System.Drawing.Point(102, 191);
            this.cboSection.Name = "cboSection";
            this.cboSection.Size = new System.Drawing.Size(121, 21);
            this.cboSection.TabIndex = 26;
            // 
            // cboAdmitted
            // 
            this.cboAdmitted.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cboAdmitted.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboAdmitted.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAdmitted.FormattingEnabled = true;
            this.cboAdmitted.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cboAdmitted.Location = new System.Drawing.Point(102, 150);
            this.cboAdmitted.Name = "cboAdmitted";
            this.cboAdmitted.Size = new System.Drawing.Size(121, 21);
            this.cboAdmitted.TabIndex = 25;
            // 
            // txtLast
            // 
            this.txtLast.Location = new System.Drawing.Point(102, 70);
            this.txtLast.Name = "txtLast";
            this.txtLast.Size = new System.Drawing.Size(100, 20);
            this.txtLast.TabIndex = 23;
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(102, 110);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(100, 20);
            this.txtAge.TabIndex = 24;
            // 
            // txtFirst
            // 
            this.txtFirst.Location = new System.Drawing.Point(102, 31);
            this.txtFirst.Name = "txtFirst";
            this.txtFirst.Size = new System.Drawing.Size(100, 20);
            this.txtFirst.TabIndex = 22;
            // 
            // lblSection
            // 
            this.lblSection.AutoSize = true;
            this.lblSection.Location = new System.Drawing.Point(36, 194);
            this.lblSection.Name = "lblSection";
            this.lblSection.Size = new System.Drawing.Size(46, 13);
            this.lblSection.TabIndex = 19;
            this.lblSection.Text = "Section:";
            // 
            // lblAdmitted
            // 
            this.lblAdmitted.AutoSize = true;
            this.lblAdmitted.Location = new System.Drawing.Point(36, 153);
            this.lblAdmitted.Name = "lblAdmitted";
            this.lblAdmitted.Size = new System.Drawing.Size(51, 13);
            this.lblAdmitted.TabIndex = 18;
            this.lblAdmitted.Text = "Admitted:";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Location = new System.Drawing.Point(36, 113);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(29, 13);
            this.lblAge.TabIndex = 17;
            this.lblAge.Text = "Age:";
            // 
            // lblLast
            // 
            this.lblLast.AutoSize = true;
            this.lblLast.Location = new System.Drawing.Point(36, 73);
            this.lblLast.Name = "lblLast";
            this.lblLast.Size = new System.Drawing.Size(61, 13);
            this.lblLast.TabIndex = 16;
            this.lblLast.Text = "Last Name:";
            // 
            // lblFirst
            // 
            this.lblFirst.AutoSize = true;
            this.lblFirst.Location = new System.Drawing.Point(36, 34);
            this.lblFirst.Name = "lblFirst";
            this.lblFirst.Size = new System.Drawing.Size(60, 13);
            this.lblFirst.TabIndex = 15;
            this.lblFirst.Text = "First Name:";
            // 
            // addPatientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(283, 312);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cboSection);
            this.Controls.Add(this.cboAdmitted);
            this.Controls.Add(this.txtLast);
            this.Controls.Add(this.txtAge);
            this.Controls.Add(this.txtFirst);
            this.Controls.Add(this.lblSection);
            this.Controls.Add(this.lblAdmitted);
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.lblLast);
            this.Controls.Add(this.lblFirst);
            this.Name = "addPatientForm";
            this.Text = "addPatientForm";
            this.Load += new System.EventHandler(this.addPatientForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.ComboBox cboSection;
        public System.Windows.Forms.ComboBox cboAdmitted;
        public System.Windows.Forms.TextBox txtLast;
        public System.Windows.Forms.TextBox txtAge;
        public System.Windows.Forms.TextBox txtFirst;
        private System.Windows.Forms.Label lblSection;
        private System.Windows.Forms.Label lblAdmitted;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label lblLast;
        private System.Windows.Forms.Label lblFirst;
    }
}