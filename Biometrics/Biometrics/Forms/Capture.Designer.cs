namespace Biometrics
{
    partial class Capture
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.clock = new System.Windows.Forms.Timer(this.components);
            this.lbl_date = new System.Windows.Forms.Label();
            this.lbl_time = new System.Windows.Forms.Label();
            this.lbl_shift_insert = new System.Windows.Forms.Label();
            this.lbl_shift = new System.Windows.Forms.Label();
            this.lbl_position_insert = new System.Windows.Forms.Label();
            this.lbl_position = new System.Windows.Forms.Label();
            this.lbl_id_insert = new System.Windows.Forms.Label();
            this.lbl_id = new System.Windows.Forms.Label();
            this.lbl_name_insert = new System.Windows.Forms.Label();
            this.lbl_name = new System.Windows.Forms.Label();
            this.pic_fingerprint = new System.Windows.Forms.PictureBox();
            this.lbl_status = new System.Windows.Forms.Label();
            this.pic_profile = new System.Windows.Forms.PictureBox();
            this.dgv_datashow = new System.Windows.Forms.DataGridView();
            this.scan_interval = new System.Windows.Forms.Timer(this.components);
            this.btn_register = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pic_fingerprint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_profile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_datashow)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(289, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(476, 58);
            this.label1.TabIndex = 0;
            this.label1.Text = "Attendance System";
            // 
            // clock
            // 
            this.clock.Enabled = true;
            this.clock.Tick += new System.EventHandler(this.Clock_Tick);
            // 
            // lbl_date
            // 
            this.lbl_date.AutoSize = true;
            this.lbl_date.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_date.Location = new System.Drawing.Point(287, 111);
            this.lbl_date.Name = "lbl_date";
            this.lbl_date.Size = new System.Drawing.Size(86, 38);
            this.lbl_date.TabIndex = 1;
            this.lbl_date.Text = "Date";
            // 
            // lbl_time
            // 
            this.lbl_time.AutoSize = true;
            this.lbl_time.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_time.Location = new System.Drawing.Point(401, 170);
            this.lbl_time.Name = "lbl_time";
            this.lbl_time.Size = new System.Drawing.Size(118, 48);
            this.lbl_time.TabIndex = 2;
            this.lbl_time.Text = "Time";
            // 
            // lbl_shift_insert
            // 
            this.lbl_shift_insert.AutoSize = true;
            this.lbl_shift_insert.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_shift_insert.Location = new System.Drawing.Point(1295, 481);
            this.lbl_shift_insert.Name = "lbl_shift_insert";
            this.lbl_shift_insert.Size = new System.Drawing.Size(52, 25);
            this.lbl_shift_insert.TabIndex = 40;
            this.lbl_shift_insert.Text = "*****";
            // 
            // lbl_shift
            // 
            this.lbl_shift.AutoSize = true;
            this.lbl_shift.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_shift.Location = new System.Drawing.Point(1132, 481);
            this.lbl_shift.Name = "lbl_shift";
            this.lbl_shift.Size = new System.Drawing.Size(57, 25);
            this.lbl_shift.TabIndex = 39;
            this.lbl_shift.Text = "Shift:";
            // 
            // lbl_position_insert
            // 
            this.lbl_position_insert.AutoSize = true;
            this.lbl_position_insert.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_position_insert.Location = new System.Drawing.Point(1295, 423);
            this.lbl_position_insert.Name = "lbl_position_insert";
            this.lbl_position_insert.Size = new System.Drawing.Size(52, 25);
            this.lbl_position_insert.TabIndex = 38;
            this.lbl_position_insert.Text = "*****";
            // 
            // lbl_position
            // 
            this.lbl_position.AutoSize = true;
            this.lbl_position.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_position.Location = new System.Drawing.Point(1132, 423);
            this.lbl_position.Name = "lbl_position";
            this.lbl_position.Size = new System.Drawing.Size(119, 25);
            this.lbl_position.TabIndex = 37;
            this.lbl_position.Text = "Department:";
            // 
            // lbl_id_insert
            // 
            this.lbl_id_insert.AutoSize = true;
            this.lbl_id_insert.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_id_insert.Location = new System.Drawing.Point(1295, 313);
            this.lbl_id_insert.Name = "lbl_id_insert";
            this.lbl_id_insert.Size = new System.Drawing.Size(52, 25);
            this.lbl_id_insert.TabIndex = 36;
            this.lbl_id_insert.Text = "*****";
            // 
            // lbl_id
            // 
            this.lbl_id.AutoSize = true;
            this.lbl_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_id.Location = new System.Drawing.Point(1132, 313);
            this.lbl_id.Name = "lbl_id";
            this.lbl_id.Size = new System.Drawing.Size(37, 25);
            this.lbl_id.TabIndex = 35;
            this.lbl_id.Text = "ID:";
            // 
            // lbl_name_insert
            // 
            this.lbl_name_insert.AutoSize = true;
            this.lbl_name_insert.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_name_insert.Location = new System.Drawing.Point(1295, 366);
            this.lbl_name_insert.Name = "lbl_name_insert";
            this.lbl_name_insert.Size = new System.Drawing.Size(52, 25);
            this.lbl_name_insert.TabIndex = 34;
            this.lbl_name_insert.Text = "*****";
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_name.Location = new System.Drawing.Point(1132, 366);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(70, 25);
            this.lbl_name.TabIndex = 33;
            this.lbl_name.Text = "Name:";
            // 
            // pic_fingerprint
            // 
            this.pic_fingerprint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_fingerprint.Location = new System.Drawing.Point(1125, 559);
            this.pic_fingerprint.Name = "pic_fingerprint";
            this.pic_fingerprint.Size = new System.Drawing.Size(355, 230);
            this.pic_fingerprint.TabIndex = 45;
            this.pic_fingerprint.TabStop = false;
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_status.ForeColor = System.Drawing.Color.Red;
            this.lbl_status.Location = new System.Drawing.Point(33, 780);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(68, 25);
            this.lbl_status.TabIndex = 46;
            this.lbl_status.Text = "Status";
            // 
            // pic_profile
            // 
            this.pic_profile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_profile.Location = new System.Drawing.Point(1125, 28);
            this.pic_profile.Name = "pic_profile";
            this.pic_profile.Size = new System.Drawing.Size(355, 230);
            this.pic_profile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_profile.TabIndex = 47;
            this.pic_profile.TabStop = false;
            // 
            // dgv_datashow
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_datashow.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_datashow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_datashow.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_datashow.Location = new System.Drawing.Point(38, 267);
            this.dgv_datashow.Name = "dgv_datashow";
            this.dgv_datashow.RowHeadersVisible = false;
            this.dgv_datashow.RowHeadersWidth = 51;
            this.dgv_datashow.RowTemplate.Height = 24;
            this.dgv_datashow.Size = new System.Drawing.Size(1044, 466);
            this.dgv_datashow.TabIndex = 48;
            // 
            // scan_interval
            // 
            this.scan_interval.Tick += new System.EventHandler(this.scan_interval_Tick);
            // 
            // btn_register
            // 
            this.btn_register.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_register.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_register.Location = new System.Drawing.Point(952, 748);
            this.btn_register.Name = "btn_register";
            this.btn_register.Size = new System.Drawing.Size(130, 41);
            this.btn_register.TabIndex = 49;
            this.btn_register.Text = "Register";
            this.btn_register.UseVisualStyleBackColor = true;
            this.btn_register.Click += new System.EventHandler(this.btn_register_Click);
            // 
            // Capture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1557, 850);
            this.Controls.Add(this.btn_register);
            this.Controls.Add(this.dgv_datashow);
            this.Controls.Add(this.pic_profile);
            this.Controls.Add(this.lbl_status);
            this.Controls.Add(this.pic_fingerprint);
            this.Controls.Add(this.lbl_shift_insert);
            this.Controls.Add(this.lbl_shift);
            this.Controls.Add(this.lbl_position_insert);
            this.Controls.Add(this.lbl_position);
            this.Controls.Add(this.lbl_id_insert);
            this.Controls.Add(this.lbl_id);
            this.Controls.Add(this.lbl_name_insert);
            this.Controls.Add(this.lbl_name);
            this.Controls.Add(this.lbl_time);
            this.Controls.Add(this.lbl_date);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Capture";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Attendance System";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Capture_FormClosing);
            this.Load += new System.EventHandler(this.Capture_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic_fingerprint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_profile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_datashow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer clock;
        private System.Windows.Forms.Label lbl_date;
        private System.Windows.Forms.Label lbl_time;
        private System.Windows.Forms.Label lbl_shift_insert;
        private System.Windows.Forms.Label lbl_shift;
        private System.Windows.Forms.Label lbl_position_insert;
        private System.Windows.Forms.Label lbl_position;
        private System.Windows.Forms.Label lbl_id_insert;
        private System.Windows.Forms.Label lbl_id;
        private System.Windows.Forms.Label lbl_name_insert;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.PictureBox pic_fingerprint;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.PictureBox pic_profile;
        private System.Windows.Forms.DataGridView dgv_datashow;
        private System.Windows.Forms.Timer scan_interval;
        private System.Windows.Forms.Button btn_register;
    }
}

