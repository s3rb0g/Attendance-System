using Biometrics.Classes;
using Biometrics.Forms;
using DPFP;
using DPFP.Capture;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biometrics
{
    public partial class Capture : Form, DPFP.Capture.EventHandler
    {
        private DPFP.Capture.Capture Capturer;

        private DPFP.Template Template;

        private DPFP.Verification.Verification Verificator;

        private DatabaseClass DatabaseClass = new DatabaseClass();
        private MainClass MainClass = new MainClass();

        private string employee_name = " ";
        private long employee_id = 0;
        private string shift = " ";
        private string department = " ";
        private int late = 0;
        private int undertime = 0;
        private string remarks = " ";

        public Capture()
        {
            InitializeComponent();
        }

        private void Capture_Load(object sender, EventArgs e)
        {
            btn_register.Visible = false;
            MakeReport("");
            label_visible_false();

            Verificator = new DPFP.Verification.Verification();
            Template = new DPFP.Template();

            try
            {
                Capturer = new DPFP.Capture.Capture();
                if (Capturer != null)
                {
                    Capturer.EventHandler = this;
                }
                else
                {
                    MessageBox.Show("Can't initiate capture operation");
                }
            }
            catch
            {
                MessageBox.Show("Can't initiate capture operation");
            }

            dgv_datashow.Enabled = false;

            dgv_datashow.Columns.Add("id", "ID");
            dgv_datashow.Columns.Add("name", "NAME");
            dgv_datashow.Columns.Add("shift", "SHIFT");
            dgv_datashow.Columns.Add("timein", "TIME IN");
            dgv_datashow.Columns.Add("timeout", "TIME OUT");

            dgv_datashow.Columns["id"].Width = 130;
            dgv_datashow.Columns["name"].Width = 200;
            dgv_datashow.Columns["shift"].Width = 150;
            dgv_datashow.Columns["timein"].Width = 150;
            dgv_datashow.Columns["timeout"].Width = 150;

            scan_interval.Interval = 1000;
            scan_interval.Start();

            clock.Start();
        }

        private void MakeReport(string message)
        {
            this.Invoke(new Action(delegate ()
            {
                lbl_status.Text = message;
            }));
        }

        private void show_details(string id, string name, string position, string shift)
        {
            this.BeginInvoke(new Action(delegate ()
            {
                lbl_id_insert.Text = id;
                lbl_name_insert.Text = name;
                lbl_position_insert.Text = position;
                lbl_shift_insert.Text = shift;
            }));
        }

        private void show_picture(string path)
        {
            this.Invoke(new Action(delegate ()
            {
                string formatted_path = path.Replace("/", "\\");
                string file_path = "C:\\xampp\\htdocs\\THESIS\\" + formatted_path;

                pic_profile.Image = Image.FromFile(file_path);

            }));
        }

        private void label_visible_false()
        {
            this.Invoke(new Action(delegate ()
            {
                lbl_id_insert.Visible = false;
                lbl_name_insert.Visible = false;
                lbl_position_insert.Visible = false;
                lbl_shift_insert.Visible = false;
                pic_profile.Image = null;
            }));
        }

        private void label_visible_true()
        {
            this.Invoke(new Action(delegate ()
            {
                lbl_id_insert.Visible = true;
                lbl_name_insert.Visible = true;
                lbl_position_insert.Visible = true;
                lbl_shift_insert.Visible = true;
            }));
        }

        private void display_dgv_timeIn(string timein)
        {
            delete_row_datagridview();
            this.Invoke(new Action(delegate ()
            {
                dgv_datashow.Rows.Insert(0, employee_id, employee_name, shift, timein);
            }));
        }

        private void display_dgv_timeInOut(string timein, string timeout)
        {
            delete_row_datagridview();
            this.Invoke(new Action(delegate ()
            {
                dgv_datashow.Rows.Insert(0, employee_id, employee_name, shift, timein, timeout);
            }));
        }

        private void delete_row_datagridview()
        {
            this.Invoke(new Action(delegate ()
            {
                for (int i = dgv_datashow.Rows.Count - 1; i >= 0; i--)
                {
                    DataGridViewRow row = dgv_datashow.Rows[i];
                    if (row.Cells["id"].Value != null && row.Cells["id"].Value.ToString() == employee_id.ToString())
                    {
                        dgv_datashow.Rows.RemoveAt(i);
                    }
                }

            }));
        }

        private void Clock_Tick(object sender, EventArgs e)
        {
            lbl_date.Text = DateTime.Now.ToLongDateString();
            lbl_time.Text = DateTime.Now.ToLongTimeString();
        }

        private void scan_interval_Tick(object sender, EventArgs e)
        {
            start_scan();
        }

        private void Capture_FormClosing(object sender, FormClosingEventArgs e)
        {
            stop_scan();
        }

        private void start_scan()
        {
            try
            {
                if (Capturer != null)
                {
                    Capturer.StartCapture();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message, "Finger Verification");
            }
        }

        private void stop_scan()
        {
            try
            {
                if (Capturer != null)
                {
                    Capturer.StopCapture();
                    scan_interval.Stop();
                    clock.Stop();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message, "Finger Verification");
            }
        }

        private double compute_late(double rate, int late)
        {
            double perHour = rate / 8;
            double total_late = perHour * late;

            return total_late;
        }
        private double compute_undertime(double rate, int undertime)
        {
            double perHour = rate / 8;
            double total_undertime = perHour * undertime;

            return total_undertime;
        }

        protected virtual void Process(DPFP.Sample Sample)
        {
            DrawPicture(ConvertSampleBitmap(Sample));

            try
            {
                string connectionstring = "datasource=localhost;username=root;password=;database=camanco";
                string command = "SELECT * FROM employees";
                bool fingerprint_verified = false;

                MySqlConnection mysqlConnect = new MySqlConnection(connectionstring);
                MySqlCommand mysqlCommand = new MySqlCommand(command, mysqlConnect);

                MySqlDataAdapter mysqlAdapter = new MySqlDataAdapter();
                mysqlAdapter.SelectCommand = mysqlCommand;

                DataTable dataTable = new DataTable();
                mysqlAdapter.Fill(dataTable);

                mysqlConnect.Open();
                MySqlDataReader mysqlReader;
                mysqlReader = mysqlCommand.ExecuteReader();

                foreach (DataRow row in dataTable.Rows)
                {
                    if (row["fingerprint"] != DBNull.Value)
                    {
                        byte[] finger = (byte[])row["fingerprint"];
                        MemoryStream fingerprintData = new MemoryStream(finger);
                        Template.DeSerialize(fingerprintData);

                        DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);

                        if (features != null)
                        {
                            DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();
                            Verificator.Verify(features, Template, ref result);

                            if (result.Verified)
                            {
                                fingerprint_verified = true;
                                label_visible_true();

                                employee_id = long.Parse(row["id"].ToString());
                                string id_string = row["id"].ToString();

                                string fname = row["firstname"].ToString();
                                string mname = row["middlename"].ToString();
                                string lname = row["lastname"].ToString();

                                string pic_path = row["picture"].ToString();

                                string m_initial = mname.Substring(0, 1);

                                employee_name = fname + " " + m_initial + " " + lname;

                                department = DatabaseClass.getPosition(employee_id);
                                shift = DatabaseClass.getShift(employee_id);

                                show_details(id_string, employee_name, department, shift);
                                show_picture(pic_path);

                                break;
                            }

                            mysqlConnect.Close();
                        }
                    }
                }

                if (fingerprint_verified && department != "Office Staff")
                {
                    DateTime dateTime_now = DateTime.Now;

                    string date_now = dateTime_now.ToString("MM:dd:yyyy");
                    string time_now = dateTime_now.ToString("hh:mm:ss tt");
                    string day_now = dateTime_now.DayOfWeek.ToString();

                    switch (shift)
                    {

                        case "Morning":
                            {
                                bool exist_attendance = DatabaseClass.existing_morning_attendance(employee_id, date_now);

                                if (exist_attendance)
                                {
                                    bool exist_timeout = DatabaseClass.existing_morning_timeout(employee_id, date_now);

                                    if (!exist_timeout)
                                    {
                                        // no time out exist

                                        DateTime timein;
                                        string timein_string = DatabaseClass.getTimein(employee_id, date_now);

                                        if (DateTime.TryParseExact(timein_string, "hh:mm:ss tt",
                                                                    CultureInfo.InvariantCulture,
                                                                    DateTimeStyles.None, out timein))
                                        {

                                            if (timein.TimeOfDay < new TimeSpan(6, 15, 0))
                                            {
                                                // early time in

                                                DateTime early_timein = new DateTime(timein.Year, timein.Month, timein.Day, 6, 0, 0);
                                                TimeSpan early_timein_duration = dateTime_now - early_timein;

                                                if (early_timein_duration.Hours >= 1)
                                                {
                                                    // time out
                                                    // also the computation of total pay per day

                                                    double rate = DatabaseClass.getRate(employee_id);

                                                    MainClass.morning_calculation(early_timein_duration.Hours, rate, day_now, date_now);

                                                    int regularHour = MainClass.regularHour;
                                                    double regularPay = MainClass.regularPay;
                                                    int overtimeHour = MainClass.overtimeHour;
                                                    double overtimePay = MainClass.overtimePay;
                                                    int regular_night_Hour = MainClass.regular_night_Hour;
                                                    double regular_night_Pay = MainClass.regular_night_Pay;
                                                    int overtime_night_Hour = MainClass.overtime_night_Hour;
                                                    double overtime_night_Pay = MainClass.overtime_night_Pay;

                                                    int sundayHour = MainClass.sundayHour;
                                                    double sundayPay = MainClass.sundayPay;
                                                    int sunday_overtimeHour = MainClass.sunday_overtimeHour;
                                                    double sunday_overtimePay = MainClass.sunday_overtimePay;

                                                    int sunday_night_Hour = MainClass.sunday_night_Hour;
                                                    double sunday_night_Pay = MainClass.sunday_night_Pay;
                                                    int sunday_overtime_night_Hour = MainClass.sunday_overtime_night_Hour;
                                                    double sunday_overtime_night_Pay = MainClass.sunday_overtime_night_Pay;

                                                    int regular_holiday_Hour = MainClass.regular_holiday_Hour;
                                                    double regular_holiday_Pay = MainClass.regular_holiday_Pay;
                                                    int regular_holiday_overtime_Hour = MainClass.regular_holiday_overtime_Hour;
                                                    double regular_holiday_overtime_Pay = MainClass.regular_holiday_overtime_Pay;
                                                    int regular_holiday_night_Hour = MainClass.regular_holiday_night_Hour;
                                                    double regular_holiday_night_Pay = MainClass.regular_holiday_night_Pay;
                                                    int regular_holiday_overtime_night_Hour = MainClass.regular_holiday_overtime_night_Hour;
                                                    double regular_holiday_overtime_night_Pay = MainClass.regular_holiday_overtime_night_Pay;

                                                    int special_holiday_Hour = MainClass.special_holiday_Hour;
                                                    double special_holiday_Pay = MainClass.special_holiday_Pay;
                                                    int special_holiday_overtime_Hour = MainClass.special_holiday_overtime_Hour;
                                                    double special_holiday_overtime_Pay = MainClass.special_holiday_overtime_Pay;
                                                    int special_holiday_night_Hour = MainClass.special_holiday_night_Hour;
                                                    double special_holiday_night_Pay = MainClass.special_holiday_night_Pay;
                                                    int special_holiday_overtime_night_Hour = MainClass.special_holiday_overtime_night_Hour;
                                                    double special_holiday_overtime_night_Pay = MainClass.special_holiday_overtime_night_Pay;

                                                    int basicDay = MainClass.basicday;
                                                    double basicPay = MainClass.basicpay; 
                                                    
                                                    double totalPay = MainClass.totalPay;

                                                    DateTime shiftEnd = DateTime.Today.AddHours(14); // 2:00 PM
                                                    DateTime timeOnly = DateTime.ParseExact(time_now, "hh:mm:ss tt", null);

                                                    // No undertime if time-out is 2:00 PM or later
                                                    if (timeOnly >= shiftEnd)
                                                    {
                                                        undertime = 0;
                                                        remarks = "-";
                                                    }
                                                    else{
                                                        // Compute undertime duration
                                                        TimeSpan undertimeDuration = shiftEnd - timeOnly;

                                                        // Convert to total hours and round up any remaining minutes
                                                        undertime = (int)Math.Ceiling(undertimeDuration.TotalHours);
                                                        
                                                        remarks = "Undertime";
                                                    }

                                                    double undertimepay = compute_undertime(rate, undertime);

                                                    DatabaseClass.insert_morning_timeout(employee_id, date_now, time_now, undertime, undertimepay, remarks, basicDay, basicPay);

                                                    DatabaseClass.insert_computation_perDay(employee_id, date_now, regularHour, regularPay, overtimeHour, overtimePay,
                                                        regular_night_Hour, regular_night_Pay, overtime_night_Hour, overtime_night_Pay, sundayHour, sundayPay,
                                                        sunday_overtimeHour, sunday_overtimePay, sunday_night_Hour, sunday_night_Pay, sunday_overtime_night_Hour,
                                                        sunday_overtime_night_Pay, regular_holiday_Hour, regular_holiday_Pay, regular_holiday_overtime_Hour,
                                                        regular_holiday_overtime_Pay, regular_holiday_night_Hour, regular_holiday_night_Pay, regular_holiday_overtime_night_Hour,
                                                        regular_holiday_overtime_night_Pay, special_holiday_Hour, special_holiday_Pay, special_holiday_overtime_Hour,
                                                        special_holiday_overtime_Pay, special_holiday_night_Hour, special_holiday_night_Pay, special_holiday_overtime_night_Hour,
                                                        special_holiday_overtime_night_Pay, totalPay);

                                                    display_dgv_timeInOut(timein_string, time_now);

                                                }
                                                else
                                                {
                                                    // this where the second finger not morethan an hour
                                                    // update only

                                                    //DatabaseClass.update_morning_timein(employee_id, date_now, time_now);
                                                    //display_dgv_timeIn(time_now);

                                                    MakeReport("You have already signed in. Thank you.");

                                                }

                                            }
                                            else
                                            {
                                                //late time in

                                                DateTime late_timein = timein;

                                                if (timein.Minute > 0 || timein.Second > 0)
                                                {
                                                    late_timein = timein.AddMinutes(60 - timein.Minute).AddSeconds(-timein.Second);
                                                }

                                                TimeSpan late_timein_duration = dateTime_now - late_timein;

                                                if (late_timein_duration.Hours >= 1)
                                                {
                                                    // time out
                                                    // also the computation of total pay per day

                                                    double rate = DatabaseClass.getRate(employee_id);

                                                    MainClass.morning_calculation(late_timein_duration.Hours, rate, day_now, date_now);

                                                    int regularHour = MainClass.regularHour;
                                                    double regularPay = MainClass.regularPay;
                                                    int overtimeHour = MainClass.overtimeHour;
                                                    double overtimePay = MainClass.overtimePay;
                                                    int regular_night_Hour = MainClass.regular_night_Hour;
                                                    double regular_night_Pay = MainClass.regular_night_Pay;
                                                    int overtime_night_Hour = MainClass.overtime_night_Hour;
                                                    double overtime_night_Pay = MainClass.overtime_night_Pay;

                                                    int sundayHour = MainClass.sundayHour;
                                                    double sundayPay = MainClass.sundayPay;
                                                    int sunday_overtimeHour = MainClass.sunday_overtimeHour;
                                                    double sunday_overtimePay = MainClass.sunday_overtimePay;
                                                    int sunday_night_Hour = MainClass.sunday_night_Hour;
                                                    double sunday_night_Pay = MainClass.sunday_night_Pay;
                                                    int sunday_overtime_night_Hour = MainClass.sunday_overtime_night_Hour;
                                                    double sunday_overtime_night_Pay = MainClass.sunday_overtime_night_Pay;

                                                    int regular_holiday_Hour = MainClass.regular_holiday_Hour;
                                                    double regular_holiday_Pay = MainClass.regular_holiday_Pay;
                                                    int regular_holiday_overtime_Hour = MainClass.regular_holiday_overtime_Hour;
                                                    double regular_holiday_overtime_Pay = MainClass.regular_holiday_overtime_Pay;
                                                    int regular_holiday_night_Hour = MainClass.regular_holiday_night_Hour;
                                                    double regular_holiday_night_Pay = MainClass.regular_holiday_night_Pay;
                                                    int regular_holiday_overtime_night_Hour = MainClass.regular_holiday_overtime_night_Hour;
                                                    double regular_holiday_overtime_night_Pay = MainClass.regular_holiday_overtime_night_Pay;

                                                    int special_holiday_Hour = MainClass.special_holiday_Hour;
                                                    double special_holiday_Pay = MainClass.special_holiday_Pay;
                                                    int special_holiday_overtime_Hour = MainClass.special_holiday_overtime_Hour;
                                                    double special_holiday_overtime_Pay = MainClass.special_holiday_overtime_Pay;
                                                    int special_holiday_night_Hour = MainClass.special_holiday_night_Hour;
                                                    double special_holiday_night_Pay = MainClass.special_holiday_night_Pay;
                                                    int special_holiday_overtime_night_Hour = MainClass.special_holiday_overtime_night_Hour;
                                                    double special_holiday_overtime_night_Pay = MainClass.special_holiday_overtime_night_Pay;

                                                    int basicDay = MainClass.basicday;
                                                    double basicPay = MainClass.basicpay;

                                                    double totalPay = MainClass.totalPay;

                                                    DateTime shiftEnd = DateTime.Today.AddHours(14); // 2:00 PM
                                                    DateTime timeOnly = DateTime.ParseExact(time_now, "hh:mm:ss tt", null);

                                                    // No undertime if time-out is 2:00 PM or later
                                                    if (timeOnly >= shiftEnd)
                                                    {
                                                        undertime = 0;
                                                        remarks = "-";
                                                    }
                                                    else
                                                    {
                                                        // Compute undertime duration
                                                        TimeSpan undertimeDuration = shiftEnd - timeOnly;

                                                        // Convert to total hours and round up any remaining minutes
                                                        undertime = (int)Math.Ceiling(undertimeDuration.TotalHours);
                                                        remarks = "Undertime";
                                                    }

                                                    double undertimepay = compute_undertime(rate, undertime);

                                                    DatabaseClass.insert_morning_timeout(employee_id, date_now, time_now, undertime, undertimepay, remarks, basicDay, basicPay);

                                                    DatabaseClass.insert_computation_perDay(employee_id, date_now, regularHour, regularPay, overtimeHour, overtimePay,
                                                        regular_night_Hour, regular_night_Pay, overtime_night_Hour, overtime_night_Pay, sundayHour, sundayPay,
                                                        sunday_overtimeHour, sunday_overtimePay, sunday_night_Hour, sunday_night_Pay, sunday_overtime_night_Hour,
                                                        sunday_overtime_night_Pay, regular_holiday_Hour, regular_holiday_Pay, regular_holiday_overtime_Hour,
                                                        regular_holiday_overtime_Pay, regular_holiday_night_Hour, regular_holiday_night_Pay, regular_holiday_overtime_night_Hour,
                                                        regular_holiday_overtime_night_Pay, special_holiday_Hour, special_holiday_Pay, special_holiday_overtime_Hour,
                                                        special_holiday_overtime_Pay, special_holiday_night_Hour, special_holiday_night_Pay, special_holiday_overtime_night_Hour,
                                                        special_holiday_overtime_night_Pay, totalPay);

                                                    display_dgv_timeInOut(timein_string, time_now);

                                                }
                                                else
                                                {
                                                    // this where the second finger not morethan an hour
                                                    // update only

                                                    //DatabaseClass.update_morning_timein(employee_id, date_now, time_now);
                                                    //display_dgv_timeIn(time_now);

                                                    MakeReport("You have already signed in. Thank you.");

                                                }

                                            }

                                        }
                                        else
                                        {
                                            MessageBox.Show("Invalid time format", "Fngerprint Verification");
                                        }

                                    }
                                    else
                                    {
                                        // Display only records

                                        string timein = DatabaseClass.getTimein(employee_id, date_now);
                                        string timeout = DatabaseClass.getTimeout(employee_id, date_now);

                                        display_dgv_timeInOut(timein, timeout);

                                        MakeReport("You have already signed out. Thank you.");

                                    }

                                }
                                else
                                {
                                    // insert time in

                                    DateTime timeOnly = DateTime.ParseExact(time_now, "hh:mm:ss tt", null);

                                    DateTime shiftStart = DateTime.Today.AddHours(6); // 6:00 AM
                                    DateTime graceEnd = shiftStart.AddMinutes(15); // 6:15 AM

                                    // No late if within the grace period
                                    if (timeOnly <= graceEnd)
                                    {
                                        late = 0;
                                        remarks = "-";
                                    }
                                    // Compute late hours if after 8:15 AM
                                    else
                                    {
                                        TimeSpan lateDuration = timeOnly - shiftStart; // Get duration from 6:00 AM

                                        // Correct rounding to ensure each second past the hour is counted
                                        late = (int)Math.Ceiling(lateDuration.TotalMinutes / 60.0);

                                        // Ensure minimum late is at least 1 hour if past 6:15 AM
                                        late = Math.Max(late, 1);

                                        remarks = "Late";
                                    }

                                    double rate = DatabaseClass.getRate(employee_id);
                                    double latepay = compute_late(rate, late);

                                    DatabaseClass.insert_morning_timein(employee_id, shift, date_now, time_now, late, latepay, remarks);

                                    display_dgv_timeIn(time_now);
                                }

                                break;
                            }


                        case "Night":
                            {

                                string amPm = dateTime_now.ToString("tt");

                                if (amPm == "pm")
                                {
                                    bool exist_attendance = DatabaseClass.existing_night_attendance(employee_id, date_now, shift);

                                    if (exist_attendance)
                                    {
                                        // exist attendance 
                                        // check the time out

                                        bool exist_timeout = DatabaseClass.existing_night_timeout(employee_id, date_now, shift);

                                        if (!exist_timeout)
                                        {
                                            // no time out exist

                                            DateTime timein_night;
                                            string timein_string_night = DatabaseClass.getTimein_night(employee_id, date_now, shift);

                                            if (DateTime.TryParseExact(timein_string_night, "hh:mm:ss tt",
                                                                        CultureInfo.InvariantCulture,
                                                                        DateTimeStyles.None, out timein_night))
                                            {

                                                if (timein_night.TimeOfDay < new TimeSpan(18, 15, 0))
                                                {
                                                    // early time in

                                                    DateTime early_timein_night = new DateTime(timein_night.Year, timein_night.Month, timein_night.Day, 18, 0, 0);
                                                    TimeSpan early_timein_night_duration = dateTime_now - early_timein_night;

                                                    if (early_timein_night_duration.Hours >= 1)
                                                    {
                                                        // time out
                                                        // also the computation of total pay per day

                                                        double rate = DatabaseClass.getRate(employee_id);

                                                        MainClass.night_calculation(early_timein_night_duration.Hours, rate, day_now, date_now);

                                                        int regularHour = MainClass.regularHour;
                                                        double regularPay = MainClass.regularPay;
                                                        int overtimeHour = MainClass.overtimeHour;
                                                        double overtimePay = MainClass.overtimePay;
                                                        int regular_night_Hour = MainClass.regular_night_Hour;
                                                        double regular_night_Pay = MainClass.regular_night_Pay;
                                                        int overtime_night_Hour = MainClass.overtime_night_Hour;
                                                        double overtime_night_Pay = MainClass.overtime_night_Pay;

                                                        int sundayHour = MainClass.sundayHour;
                                                        double sundayPay = MainClass.sundayPay;
                                                        int sunday_overtimeHour = MainClass.sunday_overtimeHour;
                                                        double sunday_overtimePay = MainClass.sunday_overtimePay;

                                                        int sunday_night_Hour = MainClass.sunday_night_Hour;
                                                        double sunday_night_Pay = MainClass.sunday_night_Pay;
                                                        int sunday_overtime_night_Hour = MainClass.sunday_overtime_night_Hour;
                                                        double sunday_overtime_night_Pay = MainClass.sunday_overtime_night_Pay;

                                                        int regular_holiday_Hour = MainClass.regular_holiday_Hour;
                                                        double regular_holiday_Pay = MainClass.regular_holiday_Pay;
                                                        int regular_holiday_overtime_Hour = MainClass.regular_holiday_overtime_Hour;
                                                        double regular_holiday_overtime_Pay = MainClass.regular_holiday_overtime_Pay;
                                                        int regular_holiday_night_Hour = MainClass.regular_holiday_night_Hour;
                                                        double regular_holiday_night_Pay = MainClass.regular_holiday_night_Pay;
                                                        int regular_holiday_overtime_night_Hour = MainClass.regular_holiday_overtime_night_Hour;
                                                        double regular_holiday_overtime_night_Pay = MainClass.regular_holiday_overtime_night_Pay;

                                                        int special_holiday_Hour = MainClass.special_holiday_Hour;
                                                        double special_holiday_Pay = MainClass.special_holiday_Pay;
                                                        int special_holiday_overtime_Hour = MainClass.special_holiday_overtime_Hour;
                                                        double special_holiday_overtime_Pay = MainClass.special_holiday_overtime_Pay;
                                                        int special_holiday_night_Hour = MainClass.special_holiday_night_Hour;
                                                        double special_holiday_night_Pay = MainClass.special_holiday_night_Pay;
                                                        int special_holiday_overtime_night_Hour = MainClass.special_holiday_overtime_night_Hour;
                                                        double special_holiday_overtime_night_Pay = MainClass.special_holiday_overtime_night_Pay;

                                                        double totalPay = MainClass.totalPay;

                                                        DatabaseClass.insert_night_timeout(employee_id, date_now, time_now, shift);

                                                        DatabaseClass.insert_computation_perDay_night(employee_id, date_now, regularHour, regularPay, overtimeHour, overtimePay,
                                                        regular_night_Hour, regular_night_Pay, overtime_night_Hour, overtime_night_Pay, sundayHour, sundayPay,
                                                        sunday_overtimeHour, sunday_overtimePay, sunday_night_Hour, sunday_night_Pay, sunday_overtime_night_Hour,
                                                        sunday_overtime_night_Pay, regular_holiday_Hour, regular_holiday_Pay, regular_holiday_overtime_Hour,
                                                        regular_holiday_overtime_Pay, regular_holiday_night_Hour, regular_holiday_night_Pay, regular_holiday_overtime_night_Hour,
                                                        regular_holiday_overtime_night_Pay, special_holiday_Hour, special_holiday_Pay, special_holiday_overtime_Hour,
                                                        special_holiday_overtime_Pay, special_holiday_night_Hour, special_holiday_night_Pay, special_holiday_overtime_night_Hour,
                                                        special_holiday_overtime_night_Pay, totalPay);

                                                        display_dgv_timeInOut(timein_string_night, time_now);
                                                    }
                                                    else
                                                    {
                                                        // this where the second finger not morethan an hour
                                                        // update only

                                                        //DatabaseClass.update_night_timein(employee_id, date_now, time_now, shift);
                                                        //display_dgv_timeIn(time_now);

                                                        MakeReport("You have already signed in. Thank you.");

                                                    }

                                                }
                                                else
                                                {
                                                    // late time in

                                                    DateTime late_timein_night = timein_night;

                                                    if (timein_night.Minute > 0 || timein_night.Second > 0)
                                                    {
                                                        late_timein_night = timein_night.AddMinutes(60 - timein_night.Minute).AddSeconds(-timein_night.Second);
                                                    }

                                                    TimeSpan late_timein_duration_night = dateTime_now - late_timein_night;

                                                    if (late_timein_duration_night.Hours >= 1)
                                                    {
                                                        // time out
                                                        // also the computation of total pay per day

                                                        double rate = DatabaseClass.getRate(employee_id);

                                                        MainClass.night_calculation(late_timein_duration_night.Hours, rate, day_now, date_now);

                                                        int regularHour = MainClass.regularHour;
                                                        double regularPay = MainClass.regularPay;
                                                        int overtimeHour = MainClass.overtimeHour;
                                                        double overtimePay = MainClass.overtimePay;
                                                        int regular_night_Hour = MainClass.regular_night_Hour;
                                                        double regular_night_Pay = MainClass.regular_night_Pay;
                                                        int overtime_night_Hour = MainClass.overtime_night_Hour;
                                                        double overtime_night_Pay = MainClass.overtime_night_Pay;

                                                        int sundayHour = MainClass.sundayHour;
                                                        double sundayPay = MainClass.sundayPay;
                                                        int sunday_overtimeHour = MainClass.sunday_overtimeHour;
                                                        double sunday_overtimePay = MainClass.sunday_overtimePay;
                                                        int sunday_night_Hour = MainClass.sunday_night_Hour;
                                                        double sunday_night_Pay = MainClass.sunday_night_Pay;
                                                        int sunday_overtime_night_Hour = MainClass.sunday_overtime_night_Hour;
                                                        double sunday_overtime_night_Pay = MainClass.sunday_overtime_night_Pay;

                                                        int regular_holiday_Hour = MainClass.regular_holiday_Hour;
                                                        double regular_holiday_Pay = MainClass.regular_holiday_Pay;
                                                        int regular_holiday_overtime_Hour = MainClass.regular_holiday_overtime_Hour;
                                                        double regular_holiday_overtime_Pay = MainClass.regular_holiday_overtime_Pay;
                                                        int regular_holiday_night_Hour = MainClass.regular_holiday_night_Hour;
                                                        double regular_holiday_night_Pay = MainClass.regular_holiday_night_Pay;
                                                        int regular_holiday_overtime_night_Hour = MainClass.regular_holiday_overtime_night_Hour;
                                                        double regular_holiday_overtime_night_Pay = MainClass.regular_holiday_overtime_night_Pay;

                                                        int special_holiday_Hour = MainClass.special_holiday_Hour;
                                                        double special_holiday_Pay = MainClass.special_holiday_Pay;
                                                        int special_holiday_overtime_Hour = MainClass.special_holiday_overtime_Hour;
                                                        double special_holiday_overtime_Pay = MainClass.special_holiday_overtime_Pay;
                                                        int special_holiday_night_Hour = MainClass.special_holiday_night_Hour;
                                                        double special_holiday_night_Pay = MainClass.special_holiday_night_Pay;
                                                        int special_holiday_overtime_night_Hour = MainClass.special_holiday_overtime_night_Hour;
                                                        double special_holiday_overtime_night_Pay = MainClass.special_holiday_overtime_night_Pay;

                                                        double totalPay = MainClass.totalPay; ;

                                                        DatabaseClass.insert_night_timeout(employee_id, date_now, time_now, shift);

                                                        DatabaseClass.insert_computation_perDay_night(employee_id, date_now, regularHour, regularPay, overtimeHour, overtimePay,
                                                        regular_night_Hour, regular_night_Pay, overtime_night_Hour, overtime_night_Pay, sundayHour, sundayPay,
                                                        sunday_overtimeHour, sunday_overtimePay, sunday_night_Hour, sunday_night_Pay, sunday_overtime_night_Hour,
                                                        sunday_overtime_night_Pay, regular_holiday_Hour, regular_holiday_Pay, regular_holiday_overtime_Hour,
                                                        regular_holiday_overtime_Pay, regular_holiday_night_Hour, regular_holiday_night_Pay, regular_holiday_overtime_night_Hour,
                                                        regular_holiday_overtime_night_Pay, special_holiday_Hour, special_holiday_Pay, special_holiday_overtime_Hour,
                                                        special_holiday_overtime_Pay, special_holiday_night_Hour, special_holiday_night_Pay, special_holiday_overtime_night_Hour,
                                                        special_holiday_overtime_night_Pay, totalPay);

                                                        display_dgv_timeInOut(timein_string_night, time_now);
                                                    }
                                                    else
                                                    {
                                                        // this where the second finger not morethan an hour
                                                        // update only

                                                        //DatabaseClass.update_night_timein(employee_id, date_now, time_now, shift);
                                                        //display_dgv_timeIn(time_now);

                                                        MakeReport("You have already signed in. Thank you.");

                                                    }
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Invalid time format", "Fngerprint Verification");
                                            }
                                        }

                                        else
                                        {
                                            // Display only records

                                            string timein = DatabaseClass.getTimein_night(employee_id, date_now, shift);
                                            string timeout = DatabaseClass.getTimeout_night(employee_id, date_now, shift);

                                            display_dgv_timeInOut(timein, timeout);

                                            MakeReport("You have already signed out. Thank you.");
                                        }

                                    }
                                    else
                                    {
                                        // insert time in

                                        DatabaseClass.insert_night_timein(employee_id, shift, date_now, time_now);
                                        display_dgv_timeIn(time_now);
                                    }

                                }
                                else
                                {
                                    // Time now is AM

                                    string date_yesterday = DateTime.Today.AddDays(-1).ToString("MM:dd:yyyy");
                                    bool exist_attendance_yesterday = DatabaseClass.existing_night_attendance_yesterday(employee_id, date_yesterday, shift);

                                    if (exist_attendance_yesterday)
                                    {
                                        // exist attendance yesterday

                                        bool exist_timeout_night = DatabaseClass.existing_night_timeout(employee_id, date_yesterday, shift);

                                        if (!exist_timeout_night)
                                        {
                                            // no time out exist

                                            DateTime timein_night;
                                            string timein_string_night = DatabaseClass.getTimein_night(employee_id, date_yesterday, shift);

                                            if (DateTime.TryParseExact(timein_string_night, "hh:mm:ss tt",
                                                                        CultureInfo.InvariantCulture,
                                                                        DateTimeStyles.None, out timein_night))
                                            {

                                                timein_night = timein_night.AddDays(-1);

                                                if (timein_night.TimeOfDay < new TimeSpan(18, 15, 0))
                                                {
                                                    // early time in night

                                                    DateTime early_timein_night = new DateTime(timein_night.Year, timein_night.Month, timein_night.Day, 18, 0, 0);
                                                    TimeSpan early_timein_night_duration = dateTime_now - early_timein_night;

                                                    double rate = DatabaseClass.getRate(employee_id);

                                                    MainClass.night_calculation(early_timein_night_duration.Hours, rate, day_now, date_now);

                                                    int regularHour = MainClass.regularHour;
                                                    double regularPay = MainClass.regularPay;
                                                    int overtimeHour = MainClass.overtimeHour;
                                                    double overtimePay = MainClass.overtimePay;
                                                    int regular_night_Hour = MainClass.regular_night_Hour;
                                                    double regular_night_Pay = MainClass.regular_night_Pay;
                                                    int overtime_night_Hour = MainClass.overtime_night_Hour;
                                                    double overtime_night_Pay = MainClass.overtime_night_Pay;

                                                    int sundayHour = MainClass.sundayHour;
                                                    double sundayPay = MainClass.sundayPay;
                                                    int sunday_overtimeHour = MainClass.sunday_overtimeHour;
                                                    double sunday_overtimePay = MainClass.sunday_overtimePay;
                                                    int sunday_night_Hour = MainClass.sunday_night_Hour;
                                                    double sunday_night_Pay = MainClass.sunday_night_Pay;
                                                    int sunday_overtime_night_Hour = MainClass.sunday_overtime_night_Hour;
                                                    double sunday_overtime_night_Pay = MainClass.sunday_overtime_night_Pay;

                                                    int regular_holiday_Hour = MainClass.regular_holiday_Hour;
                                                    double regular_holiday_Pay = MainClass.regular_holiday_Pay;
                                                    int regular_holiday_overtime_Hour = MainClass.regular_holiday_overtime_Hour;
                                                    double regular_holiday_overtime_Pay = MainClass.regular_holiday_overtime_Pay;
                                                    int regular_holiday_night_Hour = MainClass.regular_holiday_night_Hour;
                                                    double regular_holiday_night_Pay = MainClass.regular_holiday_night_Pay;
                                                    int regular_holiday_overtime_night_Hour = MainClass.regular_holiday_overtime_night_Hour;
                                                    double regular_holiday_overtime_night_Pay = MainClass.regular_holiday_overtime_night_Pay;

                                                    int special_holiday_Hour = MainClass.special_holiday_Hour;
                                                    double special_holiday_Pay = MainClass.special_holiday_Pay;
                                                    int special_holiday_overtime_Hour = MainClass.special_holiday_overtime_Hour;
                                                    double special_holiday_overtime_Pay = MainClass.special_holiday_overtime_Pay;
                                                    int special_holiday_night_Hour = MainClass.special_holiday_night_Hour;
                                                    double special_holiday_night_Pay = MainClass.special_holiday_night_Pay;
                                                    int special_holiday_overtime_night_Hour = MainClass.special_holiday_overtime_night_Hour;
                                                    double special_holiday_overtime_night_Pay = MainClass.special_holiday_overtime_night_Pay;

                                                    double totalPay = MainClass.totalPay;

                                                    DatabaseClass.insert_night_timeout(employee_id, date_yesterday, time_now, shift);

                                                    DatabaseClass.insert_computation_perDay_night(employee_id, date_yesterday, regularHour, regularPay, overtimeHour, overtimePay,
                                                        regular_night_Hour, regular_night_Pay, overtime_night_Hour, overtime_night_Pay, sundayHour, sundayPay,
                                                        sunday_overtimeHour, sunday_overtimePay, sunday_night_Hour, sunday_night_Pay, sunday_overtime_night_Hour,
                                                        sunday_overtime_night_Pay, regular_holiday_Hour, regular_holiday_Pay, regular_holiday_overtime_Hour,
                                                        regular_holiday_overtime_Pay, regular_holiday_night_Hour, regular_holiday_night_Pay, regular_holiday_overtime_night_Hour,
                                                        regular_holiday_overtime_night_Pay, special_holiday_Hour, special_holiday_Pay, special_holiday_overtime_Hour,
                                                        special_holiday_overtime_Pay, special_holiday_night_Hour, special_holiday_night_Pay, special_holiday_overtime_night_Hour,
                                                        special_holiday_overtime_night_Pay, totalPay);

                                                    display_dgv_timeInOut(timein_string_night, time_now);

                                                }
                                                else
                                                {
                                                    //late time in

                                                    DateTime late_timein_night = timein_night;

                                                    if (timein_night.Minute > 0 || timein_night.Second > 0)
                                                    {
                                                        late_timein_night = timein_night.AddMinutes(60 - timein_night.Minute).AddSeconds(-timein_night.Second);
                                                    }

                                                    TimeSpan late_timein_duration_night = dateTime_now - late_timein_night;

                                                    double rate = DatabaseClass.getRate(employee_id);

                                                    MainClass.night_calculation(late_timein_duration_night.Hours, rate, day_now, date_now);

                                                    int regularHour = MainClass.regularHour;
                                                    double regularPay = MainClass.regularPay;
                                                    int overtimeHour = MainClass.overtimeHour;
                                                    double overtimePay = MainClass.overtimePay;
                                                    int regular_night_Hour = MainClass.regular_night_Hour;
                                                    double regular_night_Pay = MainClass.regular_night_Pay;
                                                    int overtime_night_Hour = MainClass.overtime_night_Hour;
                                                    double overtime_night_Pay = MainClass.overtime_night_Pay;

                                                    int sundayHour = MainClass.sundayHour;
                                                    double sundayPay = MainClass.sundayPay;
                                                    int sunday_overtimeHour = MainClass.sunday_overtimeHour;
                                                    double sunday_overtimePay = MainClass.sunday_overtimePay;
                                                    int sunday_night_Hour = MainClass.sunday_night_Hour;
                                                    double sunday_night_Pay = MainClass.sunday_night_Pay;
                                                    int sunday_overtime_night_Hour = MainClass.sunday_overtime_night_Hour;
                                                    double sunday_overtime_night_Pay = MainClass.sunday_overtime_night_Pay;

                                                    int regular_holiday_Hour = MainClass.regular_holiday_Hour;
                                                    double regular_holiday_Pay = MainClass.regular_holiday_Pay;
                                                    int regular_holiday_overtime_Hour = MainClass.regular_holiday_overtime_Hour;
                                                    double regular_holiday_overtime_Pay = MainClass.regular_holiday_overtime_Pay;
                                                    int regular_holiday_night_Hour = MainClass.regular_holiday_night_Hour;
                                                    double regular_holiday_night_Pay = MainClass.regular_holiday_night_Pay;
                                                    int regular_holiday_overtime_night_Hour = MainClass.regular_holiday_overtime_night_Hour;
                                                    double regular_holiday_overtime_night_Pay = MainClass.regular_holiday_overtime_night_Pay;

                                                    int special_holiday_Hour = MainClass.special_holiday_Hour;
                                                    double special_holiday_Pay = MainClass.special_holiday_Pay;
                                                    int special_holiday_overtime_Hour = MainClass.special_holiday_overtime_Hour;
                                                    double special_holiday_overtime_Pay = MainClass.special_holiday_overtime_Pay;
                                                    int special_holiday_night_Hour = MainClass.special_holiday_night_Hour;
                                                    double special_holiday_night_Pay = MainClass.special_holiday_night_Pay;
                                                    int special_holiday_overtime_night_Hour = MainClass.special_holiday_overtime_night_Hour;
                                                    double special_holiday_overtime_night_Pay = MainClass.special_holiday_overtime_night_Pay;

                                                    double totalPay = MainClass.totalPay;

                                                    DatabaseClass.insert_night_timeout(employee_id, date_yesterday, time_now, shift);

                                                    DatabaseClass.insert_computation_perDay_night(employee_id, date_yesterday, regularHour, regularPay, overtimeHour, overtimePay,
                                                        regular_night_Hour, regular_night_Pay, overtime_night_Hour, overtime_night_Pay, sundayHour, sundayPay,
                                                        sunday_overtimeHour, sunday_overtimePay, sunday_night_Hour, sunday_night_Pay, sunday_overtime_night_Hour,
                                                        sunday_overtime_night_Pay, regular_holiday_Hour, regular_holiday_Pay, regular_holiday_overtime_Hour,
                                                        regular_holiday_overtime_Pay, regular_holiday_night_Hour, regular_holiday_night_Pay, regular_holiday_overtime_night_Hour,
                                                        regular_holiday_overtime_night_Pay, special_holiday_Hour, special_holiday_Pay, special_holiday_overtime_Hour,
                                                        special_holiday_overtime_Pay, special_holiday_night_Hour, special_holiday_night_Pay, special_holiday_overtime_night_Hour,
                                                        special_holiday_overtime_night_Pay, totalPay);

                                                    display_dgv_timeInOut(timein_string_night, time_now);

                                                }


                                            }
                                            else
                                            {
                                                MessageBox.Show("Invalid time format", "Fngerprint Verification");
                                            }

                                        }
                                        else
                                        {
                                            // Display only records

                                            string timein = DatabaseClass.getTimein_night(employee_id, date_yesterday, shift);
                                            string timeout = DatabaseClass.getTimeout_night(employee_id, date_yesterday, shift);

                                            display_dgv_timeInOut(timein, timeout);

                                            MakeReport("You have already signed out. Thank you.");

                                        }

                                    }
                                    else
                                    {
                                        MakeReport("Night shift: Too early to sign in.");
                                    }
                                }

                                break;
                            }
                    }
                }

                // This is for office staff

                else if (fingerprint_verified && department == "Office Staff")
                {
                    DateTime dateTime_now = DateTime.Now;

                    string date_now = dateTime_now.ToString("MM:dd:yyyy");
                    string time_now = dateTime_now.ToString("hh:mm:ss tt");
                    string day_now = dateTime_now.DayOfWeek.ToString();


                    bool exist_attendance = DatabaseClass.existing_morning_attendance(employee_id, date_now);

                    if (exist_attendance)
                    {
                        bool exist_timeout = DatabaseClass.existing_morning_timeout(employee_id, date_now);

                        if (!exist_timeout)
                        {
                            // no time out exist

                            DateTime timein;
                            string timein_string = DatabaseClass.getTimein(employee_id, date_now);

                            if (DateTime.TryParseExact(timein_string, "hh:mm:ss tt",
                                                        CultureInfo.InvariantCulture,
                                                        DateTimeStyles.None, out timein))
                            {

                                if (timein.TimeOfDay < new TimeSpan(8, 15, 0))
                                {
                                    // early time in

                                    DateTime early_timein = new DateTime(timein.Year, timein.Month, timein.Day, 8, 0, 0);
                                    TimeSpan early_timein_duration = dateTime_now - early_timein;

                                    if (early_timein_duration.Hours >= 1)
                                    {
                                        // time out
                                        // also the computation of total pay per day

                                        double rate = DatabaseClass.getRate(employee_id);

                                        MainClass.officestaff_calculation(employee_id, early_timein_duration.Hours, rate, day_now, date_now);

                                        int regularHour = MainClass.regularHour;
                                        double regularPay = MainClass.regularPay;
                                        int overtimeHour = MainClass.overtimeHour;
                                        double overtimePay = MainClass.overtimePay;
                                        int regular_night_Hour = MainClass.regular_night_Hour;
                                        double regular_night_Pay = MainClass.regular_night_Pay;
                                        int overtime_night_Hour = MainClass.overtime_night_Hour;
                                        double overtime_night_Pay = MainClass.overtime_night_Pay;

                                        int sundayHour = MainClass.sundayHour;
                                        double sundayPay = MainClass.sundayPay;
                                        int sunday_overtimeHour = MainClass.sunday_overtimeHour;
                                        double sunday_overtimePay = MainClass.sunday_overtimePay;

                                        int sunday_night_Hour = MainClass.sunday_night_Hour;
                                        double sunday_night_Pay = MainClass.sunday_night_Pay;
                                        int sunday_overtime_night_Hour = MainClass.sunday_overtime_night_Hour;
                                        double sunday_overtime_night_Pay = MainClass.sunday_overtime_night_Pay;

                                        int regular_holiday_Hour = MainClass.regular_holiday_Hour;
                                        double regular_holiday_Pay = MainClass.regular_holiday_Pay;
                                        int regular_holiday_overtime_Hour = MainClass.regular_holiday_overtime_Hour;
                                        double regular_holiday_overtime_Pay = MainClass.regular_holiday_overtime_Pay;
                                        int regular_holiday_night_Hour = MainClass.regular_holiday_night_Hour;
                                        double regular_holiday_night_Pay = MainClass.regular_holiday_night_Pay;
                                        int regular_holiday_overtime_night_Hour = MainClass.regular_holiday_overtime_night_Hour;
                                        double regular_holiday_overtime_night_Pay = MainClass.regular_holiday_overtime_night_Pay;

                                        int special_holiday_Hour = MainClass.special_holiday_Hour;
                                        double special_holiday_Pay = MainClass.special_holiday_Pay;
                                        int special_holiday_overtime_Hour = MainClass.special_holiday_overtime_Hour;
                                        double special_holiday_overtime_Pay = MainClass.special_holiday_overtime_Pay;
                                        int special_holiday_night_Hour = MainClass.special_holiday_night_Hour;
                                        double special_holiday_night_Pay = MainClass.special_holiday_night_Pay;
                                        int special_holiday_overtime_night_Hour = MainClass.special_holiday_overtime_night_Hour;
                                        double special_holiday_overtime_night_Pay = MainClass.special_holiday_overtime_night_Pay;

                                        int basicDay = MainClass.basicday;
                                        double basicPay = MainClass.basicpay;

                                        double totalPay = MainClass.totalPay;

                                        DateTime shiftEnd = DateTime.Today.AddHours(17); // 5:00 PM
                                        DateTime timeOnly = DateTime.ParseExact(time_now, "hh:mm:ss tt", null);

                                        // No undertime if time-out is 5:00 PM or later
                                        if (timeOnly >= shiftEnd)
                                        {
                                            undertime = 0;
                                            remarks = "-";
                                        }
                                        else
                                        {
                                            // Compute undertime duration
                                            TimeSpan undertimeDuration = shiftEnd - timeOnly;

                                            // Convert to total hours and round up any remaining minutes
                                            undertime = (int)Math.Ceiling(undertimeDuration.TotalHours);
                                            remarks = "Undertime";
                                        }

                                        double undertimepay = compute_undertime(rate, undertime);

                                        DatabaseClass.insert_morning_timeout(employee_id, date_now, time_now, undertime, undertimepay, remarks, basicDay, basicPay);

                                        DatabaseClass.insert_computation_perDay(employee_id, date_now, regularHour, regularPay, overtimeHour, overtimePay,
                                            regular_night_Hour, regular_night_Pay, overtime_night_Hour, overtime_night_Pay, sundayHour, sundayPay,
                                            sunday_overtimeHour, sunday_overtimePay, sunday_night_Hour, sunday_night_Pay, sunday_overtime_night_Hour,
                                            sunday_overtime_night_Pay, regular_holiday_Hour, regular_holiday_Pay, regular_holiday_overtime_Hour,
                                            regular_holiday_overtime_Pay, regular_holiday_night_Hour, regular_holiday_night_Pay, regular_holiday_overtime_night_Hour,
                                            regular_holiday_overtime_night_Pay, special_holiday_Hour, special_holiday_Pay, special_holiday_overtime_Hour,
                                            special_holiday_overtime_Pay, special_holiday_night_Hour, special_holiday_night_Pay, special_holiday_overtime_night_Hour,
                                            special_holiday_overtime_night_Pay, totalPay);

                                        display_dgv_timeInOut(timein_string, time_now);

                                    }
                                    else
                                    {
                                        // this where the second finger not morethan an hour
                                        // update only

                                        //DatabaseClass.update_morning_timein(employee_id, date_now, time_now);
                                        //display_dgv_timeIn(time_now);

                                        MakeReport("You have already signed in. Thank you.");

                                    }

                                }
                                else
                                {
                                    //late time in

                                    DateTime late_timein = timein;

                                    if (timein.Minute > 0 || timein.Second > 0)
                                    {
                                        late_timein = timein.AddMinutes(60 - timein.Minute).AddSeconds(-timein.Second);
                                    }

                                    TimeSpan late_timein_duration = dateTime_now - late_timein;

                                    if (late_timein_duration.Hours >= 1)
                                    {
                                        // time out
                                        // also the computation of total pay per day

                                        double rate = DatabaseClass.getRate(employee_id);

                                        MainClass.officestaff_calculation(employee_id, late_timein_duration.Hours, rate, day_now, date_now);

                                        int regularHour = MainClass.regularHour;
                                        double regularPay = MainClass.regularPay;
                                        int overtimeHour = MainClass.overtimeHour;
                                        double overtimePay = MainClass.overtimePay;
                                        int regular_night_Hour = MainClass.regular_night_Hour;
                                        double regular_night_Pay = MainClass.regular_night_Pay;
                                        int overtime_night_Hour = MainClass.overtime_night_Hour;
                                        double overtime_night_Pay = MainClass.overtime_night_Pay;

                                        int sundayHour = MainClass.sundayHour;
                                        double sundayPay = MainClass.sundayPay;
                                        int sunday_overtimeHour = MainClass.sunday_overtimeHour;
                                        double sunday_overtimePay = MainClass.sunday_overtimePay;
                                        int sunday_night_Hour = MainClass.sunday_night_Hour;
                                        double sunday_night_Pay = MainClass.sunday_night_Pay;
                                        int sunday_overtime_night_Hour = MainClass.sunday_overtime_night_Hour;
                                        double sunday_overtime_night_Pay = MainClass.sunday_overtime_night_Pay;

                                        int regular_holiday_Hour = MainClass.regular_holiday_Hour;
                                        double regular_holiday_Pay = MainClass.regular_holiday_Pay;
                                        int regular_holiday_overtime_Hour = MainClass.regular_holiday_overtime_Hour;
                                        double regular_holiday_overtime_Pay = MainClass.regular_holiday_overtime_Pay;
                                        int regular_holiday_night_Hour = MainClass.regular_holiday_night_Hour;
                                        double regular_holiday_night_Pay = MainClass.regular_holiday_night_Pay;
                                        int regular_holiday_overtime_night_Hour = MainClass.regular_holiday_overtime_night_Hour;
                                        double regular_holiday_overtime_night_Pay = MainClass.regular_holiday_overtime_night_Pay;

                                        int special_holiday_Hour = MainClass.special_holiday_Hour;
                                        double special_holiday_Pay = MainClass.special_holiday_Pay;
                                        int special_holiday_overtime_Hour = MainClass.special_holiday_overtime_Hour;
                                        double special_holiday_overtime_Pay = MainClass.special_holiday_overtime_Pay;
                                        int special_holiday_night_Hour = MainClass.special_holiday_night_Hour;
                                        double special_holiday_night_Pay = MainClass.special_holiday_night_Pay;
                                        int special_holiday_overtime_night_Hour = MainClass.special_holiday_overtime_night_Hour;
                                        double special_holiday_overtime_night_Pay = MainClass.special_holiday_overtime_night_Pay;

                                        int basicDay = MainClass.basicday;
                                        double basicPay = MainClass.basicpay;

                                        double totalPay = MainClass.totalPay;

                                        DateTime shiftEnd = DateTime.Today.AddHours(17); // 5:00 PM
                                        DateTime timeOnly = DateTime.ParseExact(time_now, "hh:mm:ss tt", null);

                                        // No undertime if time-out is 5:00 PM or later
                                        if (timeOnly >= shiftEnd)
                                        {
                                            undertime = 0;
                                            remarks = "-";
                                        }
                                        else
                                        {
                                            // Compute undertime duration
                                            TimeSpan undertimeDuration = shiftEnd - timeOnly;

                                            // Convert to total hours and round up any remaining minutes
                                            undertime = (int)Math.Ceiling(undertimeDuration.TotalHours);
                                            remarks = "Undertime";
                                        }

                                        double undertimepay = compute_undertime(rate, undertime);

                                        DatabaseClass.insert_morning_timeout(employee_id, date_now, time_now, undertime, undertimepay, remarks, basicDay, basicPay);

                                        DatabaseClass.insert_computation_perDay(employee_id, date_now, regularHour, regularPay, overtimeHour, overtimePay,
                                            regular_night_Hour, regular_night_Pay, overtime_night_Hour, overtime_night_Pay, sundayHour, sundayPay,
                                            sunday_overtimeHour, sunday_overtimePay, sunday_night_Hour, sunday_night_Pay, sunday_overtime_night_Hour,
                                            sunday_overtime_night_Pay, regular_holiday_Hour, regular_holiday_Pay, regular_holiday_overtime_Hour,
                                            regular_holiday_overtime_Pay, regular_holiday_night_Hour, regular_holiday_night_Pay, regular_holiday_overtime_night_Hour,
                                            regular_holiday_overtime_night_Pay, special_holiday_Hour, special_holiday_Pay, special_holiday_overtime_Hour,
                                            special_holiday_overtime_Pay, special_holiday_night_Hour, special_holiday_night_Pay, special_holiday_overtime_night_Hour,
                                            special_holiday_overtime_night_Pay, totalPay);

                                        display_dgv_timeInOut(timein_string, time_now);

                                    }
                                    else
                                    {
                                        // this where the second finger not morethan an hour
                                        // update only

                                        //DatabaseClass.update_morning_timein(employee_id, date_now, time_now);
                                        //display_dgv_timeIn(time_now);

                                        MakeReport("You have already signed in. Thank you.");

                                    }

                                }

                            }
                            else
                            {
                                MessageBox.Show("Invalid time format", "Fngerprint Verification");
                            }

                        }
                        else
                        {
                            // Display only records

                            string timein = DatabaseClass.getTimein(employee_id, date_now);
                            string timeout = DatabaseClass.getTimeout(employee_id, date_now);

                            display_dgv_timeInOut(timein, timeout);

                            MakeReport("You have already signed out. Thank you.");

                        }

                    }
                    else
                    {
                        // insert time in

                        DateTime timeOnly = DateTime.ParseExact(time_now, "hh:mm:ss tt", null);

                        DateTime shiftStart = DateTime.Today.AddHours(8); // 8:00 AM
                        DateTime graceEnd = shiftStart.AddMinutes(15); // 8:15 AM

                        // No late if within the grace period
                        if (timeOnly <= graceEnd)
                        {
                            late = 0;
                            remarks = "-";
                        }
                        // Compute late hours if after 8:15 AM
                        else
                        {
                            TimeSpan lateDuration = timeOnly - shiftStart; // Get duration from 8:00 AM

                            // Correct rounding to ensure each second past the hour is counted
                            late = (int)Math.Ceiling(lateDuration.TotalMinutes / 60.0);

                            // Ensure minimum late is at least 1 hour if past 8:15 AM
                            late = Math.Max(late, 1);

                            remarks = "Late";
                        }

                        double rate = DatabaseClass.getRate(employee_id);
                        double latepay = compute_late(rate, late);

                        DatabaseClass.insert_morning_timein(employee_id, shift, date_now, time_now, late, latepay, remarks);

                        display_dgv_timeIn(time_now);
                    }

                }
                else
                {
                    MakeReport("Figerprint is not registered");
                }
                

            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message, "Finger Verification");
            }

        }

        private Bitmap ConvertSampleBitmap(DPFP.Sample Sample)
        {
            Bitmap bitmap = null;

            DPFP.Capture.SampleConversion convertor = new DPFP.Capture.SampleConversion();
            convertor.ConvertToPicture(Sample, ref bitmap);

            return bitmap;
        }

        private void DrawPicture(Bitmap bitmap)
        {
            pic_fingerprint.Image = new Bitmap(bitmap, pic_fingerprint.Size);
        }

        protected DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
        {
            DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction();
            DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
            DPFP.FeatureSet feature = new DPFP.FeatureSet();

            Extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref feature);

            if (feedback == DPFP.Capture.CaptureFeedback.Good)
            {
                return feature;
            }
            else
            {
                return null;
            }
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            stop_scan();
            clock.Stop();
            Login login = new Login();
            this.Hide();
            login.Show();
        }


        public void OnComplete(object Capture, string ReaderSerialNumber, Sample Sample)
        {
            remarks = "";
            undertime = 0;
            late = 0;
            MakeReport("");
            label_visible_false();
            Process(Sample);
            //throw new NotImplementedException();
        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            //throw new NotImplementedException();
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            //throw new NotImplementedException();
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            //throw new NotImplementedException();
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            //throw new NotImplementedException();
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, CaptureFeedback CaptureFeedback)
        {
            //throw new NotImplementedException();
        }

        
    }
}
