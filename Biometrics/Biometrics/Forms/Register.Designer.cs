namespace Biometrics.Forms
{
    partial class Register
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
            this.lbl_fingerneeded = new System.Windows.Forms.Label();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_capture = new System.Windows.Forms.Button();
            this.btn_capture_cancel = new System.Windows.Forms.Button();
            this.txt_id = new System.Windows.Forms.TextBox();
            this.lbl_birthday_insert = new System.Windows.Forms.Label();
            this.lbl_lname_insert = new System.Windows.Forms.Label();
            this.lbl_mname_insert = new System.Windows.Forms.Label();
            this.lbl_fname_insert = new System.Windows.Forms.Label();
            this.lbl_birthday = new System.Windows.Forms.Label();
            this.lbl_lname = new System.Windows.Forms.Label();
            this.lbl_mname = new System.Windows.Forms.Label();
            this.lbl_fname = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_status = new System.Windows.Forms.Label();
            this.pic_fingerprint = new System.Windows.Forms.PictureBox();
            this.pic_profile = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pic_fingerprint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_profile)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_fingerneeded
            // 
            this.lbl_fingerneeded.AutoSize = true;
            this.lbl_fingerneeded.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_fingerneeded.Location = new System.Drawing.Point(45, 453);
            this.lbl_fingerneeded.Name = "lbl_fingerneeded";
            this.lbl_fingerneeded.Size = new System.Drawing.Size(70, 25);
            this.lbl_fingerneeded.TabIndex = 33;
            this.lbl_fingerneeded.Text = "label1";
            // 
            // btn_close
            // 
            this.btn_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_close.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.Location = new System.Drawing.Point(1051, 518);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(125, 40);
            this.btn_close.TabIndex = 32;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_capture
            // 
            this.btn_capture.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_capture.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_capture.Location = new System.Drawing.Point(901, 518);
            this.btn_capture.Name = "btn_capture";
            this.btn_capture.Size = new System.Drawing.Size(125, 40);
            this.btn_capture.TabIndex = 31;
            this.btn_capture.Text = "Capture";
            this.btn_capture.UseVisualStyleBackColor = true;
            this.btn_capture.Click += new System.EventHandler(this.btn_capture_Click);
            // 
            // btn_capture_cancel
            // 
            this.btn_capture_cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_capture_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_capture_cancel.Location = new System.Drawing.Point(901, 518);
            this.btn_capture_cancel.Name = "btn_capture_cancel";
            this.btn_capture_cancel.Size = new System.Drawing.Size(125, 40);
            this.btn_capture_cancel.TabIndex = 30;
            this.btn_capture_cancel.Text = "Cancel";
            this.btn_capture_cancel.UseVisualStyleBackColor = true;
            this.btn_capture_cancel.Click += new System.EventHandler(this.btn_capture_cancel_Click);
            // 
            // txt_id
            // 
            this.txt_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_id.Location = new System.Drawing.Point(513, 105);
            this.txt_id.Name = "txt_id";
            this.txt_id.Size = new System.Drawing.Size(275, 30);
            this.txt_id.TabIndex = 29;
            this.txt_id.TextChanged += new System.EventHandler(this.txt_id_TextChanged);
            // 
            // lbl_birthday_insert
            // 
            this.lbl_birthday_insert.AutoSize = true;
            this.lbl_birthday_insert.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_birthday_insert.Location = new System.Drawing.Point(508, 382);
            this.lbl_birthday_insert.Name = "lbl_birthday_insert";
            this.lbl_birthday_insert.Size = new System.Drawing.Size(52, 25);
            this.lbl_birthday_insert.TabIndex = 28;
            this.lbl_birthday_insert.Text = "*****";
            // 
            // lbl_lname_insert
            // 
            this.lbl_lname_insert.AutoSize = true;
            this.lbl_lname_insert.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_lname_insert.Location = new System.Drawing.Point(508, 313);
            this.lbl_lname_insert.Name = "lbl_lname_insert";
            this.lbl_lname_insert.Size = new System.Drawing.Size(52, 25);
            this.lbl_lname_insert.TabIndex = 27;
            this.lbl_lname_insert.Text = "*****";
            // 
            // lbl_mname_insert
            // 
            this.lbl_mname_insert.AutoSize = true;
            this.lbl_mname_insert.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_mname_insert.Location = new System.Drawing.Point(508, 245);
            this.lbl_mname_insert.Name = "lbl_mname_insert";
            this.lbl_mname_insert.Size = new System.Drawing.Size(52, 25);
            this.lbl_mname_insert.TabIndex = 26;
            this.lbl_mname_insert.Text = "*****";
            // 
            // lbl_fname_insert
            // 
            this.lbl_fname_insert.AutoSize = true;
            this.lbl_fname_insert.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_fname_insert.Location = new System.Drawing.Point(508, 172);
            this.lbl_fname_insert.Name = "lbl_fname_insert";
            this.lbl_fname_insert.Size = new System.Drawing.Size(52, 25);
            this.lbl_fname_insert.TabIndex = 25;
            this.lbl_fname_insert.Text = "*****";
            // 
            // lbl_birthday
            // 
            this.lbl_birthday.AutoSize = true;
            this.lbl_birthday.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_birthday.Location = new System.Drawing.Point(354, 382);
            this.lbl_birthday.Name = "lbl_birthday";
            this.lbl_birthday.Size = new System.Drawing.Size(89, 25);
            this.lbl_birthday.TabIndex = 24;
            this.lbl_birthday.Text = "Birthday:";
            // 
            // lbl_lname
            // 
            this.lbl_lname.AutoSize = true;
            this.lbl_lname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_lname.Location = new System.Drawing.Point(354, 313);
            this.lbl_lname.Name = "lbl_lname";
            this.lbl_lname.Size = new System.Drawing.Size(103, 25);
            this.lbl_lname.TabIndex = 23;
            this.lbl_lname.Text = "Last name";
            // 
            // lbl_mname
            // 
            this.lbl_mname.AutoSize = true;
            this.lbl_mname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_mname.Location = new System.Drawing.Point(354, 245);
            this.lbl_mname.Name = "lbl_mname";
            this.lbl_mname.Size = new System.Drawing.Size(130, 25);
            this.lbl_mname.TabIndex = 22;
            this.lbl_mname.Text = "Middle name:";
            // 
            // lbl_fname
            // 
            this.lbl_fname.AutoSize = true;
            this.lbl_fname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_fname.Location = new System.Drawing.Point(354, 172);
            this.lbl_fname.Name = "lbl_fname";
            this.lbl_fname.Size = new System.Drawing.Size(109, 25);
            this.lbl_fname.TabIndex = 21;
            this.lbl_fname.Text = "First name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(354, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 25);
            this.label2.TabIndex = 20;
            this.label2.Text = "ID:";
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_status.Location = new System.Drawing.Point(25, 533);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(68, 25);
            this.lbl_status.TabIndex = 19;
            this.lbl_status.Text = "Status";
            // 
            // pic_fingerprint
            // 
            this.pic_fingerprint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_fingerprint.Location = new System.Drawing.Point(25, 94);
            this.pic_fingerprint.Name = "pic_fingerprint";
            this.pic_fingerprint.Size = new System.Drawing.Size(323, 346);
            this.pic_fingerprint.TabIndex = 18;
            this.pic_fingerprint.TabStop = false;
            // 
            // pic_profile
            // 
            this.pic_profile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_profile.Location = new System.Drawing.Point(813, 94);
            this.pic_profile.Name = "pic_profile";
            this.pic_profile.Size = new System.Drawing.Size(363, 346);
            this.pic_profile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_profile.TabIndex = 34;
            this.pic_profile.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(379, 38);
            this.label1.TabIndex = 35;
            this.label1.Text = "Fingerprint Registration";
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1211, 589);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pic_profile);
            this.Controls.Add(this.lbl_fingerneeded);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_capture);
            this.Controls.Add(this.btn_capture_cancel);
            this.Controls.Add(this.txt_id);
            this.Controls.Add(this.lbl_birthday_insert);
            this.Controls.Add(this.lbl_lname_insert);
            this.Controls.Add(this.lbl_mname_insert);
            this.Controls.Add(this.lbl_fname_insert);
            this.Controls.Add(this.lbl_birthday);
            this.Controls.Add(this.lbl_lname);
            this.Controls.Add(this.lbl_mname);
            this.Controls.Add(this.lbl_fname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_status);
            this.Controls.Add(this.pic_fingerprint);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Register";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Register";
            this.Load += new System.EventHandler(this.Register_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic_fingerprint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_profile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_fingerneeded;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_capture;
        private System.Windows.Forms.Button btn_capture_cancel;
        private System.Windows.Forms.TextBox txt_id;
        private System.Windows.Forms.Label lbl_birthday_insert;
        private System.Windows.Forms.Label lbl_lname_insert;
        private System.Windows.Forms.Label lbl_mname_insert;
        private System.Windows.Forms.Label lbl_fname_insert;
        private System.Windows.Forms.Label lbl_birthday;
        private System.Windows.Forms.Label lbl_lname;
        private System.Windows.Forms.Label lbl_mname;
        private System.Windows.Forms.Label lbl_fname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.PictureBox pic_fingerprint;
        private System.Windows.Forms.PictureBox pic_profile;
        private System.Windows.Forms.Label label1;
    }
}