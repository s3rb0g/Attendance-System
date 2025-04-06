using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Biometrics.Classes
{
    internal class DatabaseClass
    {
        private string connectionstring = "datasource=localhost;username=root;password=;database=camanco";
        private string command = "";

        private MySqlConnection mysqlConnect;
        private MySqlCommand mysqlCommand;
        private MySqlDataReader mysqlReader;
        //private MySqlDataAdapter mysqlAdapter;


        // Verify Login
        public bool verify_login(int id, string password)
        {
            mysqlConnect = new MySqlConnection(connectionstring);
            mysqlConnect.Open();

            command = "SELECT name FROM officers WHERE id = @id AND password = @password AND position = 'HR'";

            mysqlCommand = new MySqlCommand(command, mysqlConnect);

            mysqlCommand.Parameters.AddWithValue("@id", id);
            mysqlCommand.Parameters.AddWithValue("@password", password);

            mysqlReader = mysqlCommand.ExecuteReader();

            if (mysqlReader.Read())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Check if fingerprint exist
        public bool existing_fingerprint(long id)
        {
            mysqlConnect = new MySqlConnection(connectionstring);
            mysqlConnect.Open();

            command = "SELECT fingerprint FROM employees WHERE id = @id";

            mysqlCommand = new MySqlCommand(command, mysqlConnect);

            mysqlCommand.Parameters.AddWithValue("@id", id);

            mysqlReader = mysqlCommand.ExecuteReader();

            if (mysqlReader.Read())
            {
                if (!mysqlReader.IsDBNull(0))
                {
                    return true;
                }

            }

            return false;

        }

        // Insert morning time in
        public void insert_morning_timein(long id, string shift, string date, string timein, int late, double latepay, string remarks)
        {
            int regularHour = 0, regular_night_Hour = 0;
            double regularPay = 0, regular_night_Pay = 0;
            int overtimeHour = 0, overtime_night_Hour = 0;
            double overtimePay = 0, overtime_night_Pay = 0;

            int sundayHour = 0, sunday_night_Hour = 0;
            double sundayPay = 0, sunday_night_Pay = 0;
            int sunday_overtimeHour = 0, sunday_overtime_night_Hour = 0;
            double sunday_overtimePay = 0, sunday_overtime_night_Pay = 0;

            int regular_holiday_Hour = 0, regular_holiday_night_Hour = 0;
            double regular_holiday_Pay = 0, regular_holiday_night_Pay = 0;
            int regular_holiday_overtime_Hour = 0, regular_holiday_overtime_night_Hour = 0;
            double regular_holiday_overtime_Pay = 0, regular_holiday_overtime_night_Pay = 0;

            int special_holiday_Hour = 0, special_holiday_night_Hour = 0;
            double special_holiday_Pay = 0, special_holiday_night_Pay = 0;
            int special_holiday_overtime_Hour = 0, special_holiday_overtime_night_Hour = 0;
            double special_holiday_overtime_Pay = 0, special_holiday_overtime_night_Pay = 0;
            
            double totalPay = 0;

            mysqlConnect = new MySqlConnection(connectionstring);
            mysqlConnect.Open();

            command = "INSERT INTO attendance (employeeid, shift, date, timein, latehour, latededuct," +
                "regularhour, regularpay, overtimehour, overtimepay, nighthour, nightpay, nightothour, nightotpay," +
                "sundayhour, sundaypay, sundayothour, sundayotpay, sundaynighthour, sundaynightpay, sundaynightothour, sundaynightotpay," +
                "regholhour, regholpay, regholothour, regholotpay, regholnighthour, regholnightpay, regholnightothour, regholnightotpay," +
                "specialholhour, specialholpay, specialholothour, specialholotpay, specialholnighthour, specialholnightpay, specialholnightothour, specialholnightotpay, totalpay, remarkslate) " +
                "VALUES (@id, @shift, @date, @timein, @late, @latepay," +
                "@regularHour, @regularPay, @overtimeHour, @overtimePay, @regular_night_Hour, @regular_night_Pay, @overtime_night_Hour, @overtime_night_Pay," +
                "@sundayHour, @sundayPay, @sunday_overtimeHour, @sunday_overtimePay, @sunday_night_Hour, @sunday_night_Pay, @sunday_overtime_night_Hour, @sunday_overtime_night_Pay," +
                "@regular_holiday_Hour, @regular_holiday_Pay, @regular_holiday_overtime_Hour, @regular_holiday_overtime_Pay," +
                "@regular_holiday_night_Hour, @regular_holiday_night_Pay, @regular_holiday_overtime_night_Hour, @regular_holiday_overtime_night_Pay," +
                "@special_holiday_Hour, @special_holiday_pay, @special_holiday_overtime_Hour, @special_holiday_overtime_Pay," +
                "@special_holiday_night_Hour, @special_holiday_night_Pay, @special_holiday_overtime_night_Hour, @special_holiday_overtime_night_Pay, @totalPay, @remarks)";

            mysqlCommand = new MySqlCommand(command, mysqlConnect);

            mysqlCommand.Parameters.AddWithValue("@id", id);
            mysqlCommand.Parameters.AddWithValue("@shift", shift);
            mysqlCommand.Parameters.AddWithValue("@date", date);
            mysqlCommand.Parameters.AddWithValue("@timein", timein);
            mysqlCommand.Parameters.AddWithValue("@late", late);
            mysqlCommand.Parameters.AddWithValue("@latepay", latepay);

            mysqlCommand.Parameters.AddWithValue("@regularHour", regularHour);
            mysqlCommand.Parameters.AddWithValue("@regularPay", regularPay);
            mysqlCommand.Parameters.AddWithValue("@overtimeHour", overtimeHour);
            mysqlCommand.Parameters.AddWithValue("@overtimePay", overtimePay);

            mysqlCommand.Parameters.AddWithValue("@regular_night_Hour", regular_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@regular_night_Pay", regular_night_Pay);
            mysqlCommand.Parameters.AddWithValue("@overtime_night_Hour", overtime_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@overtime_night_Pay", overtime_night_Pay);

            mysqlCommand.Parameters.AddWithValue("@sundayHour", sundayHour);
            mysqlCommand.Parameters.AddWithValue("@sundayPay", sundayPay);
            mysqlCommand.Parameters.AddWithValue("@sunday_overtimeHour", sunday_overtimeHour);
            mysqlCommand.Parameters.AddWithValue("@sunday_overtimePay", sunday_overtimePay);

            mysqlCommand.Parameters.AddWithValue("@sunday_night_Hour", sunday_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@sunday_night_Pay", sunday_night_Pay);
            mysqlCommand.Parameters.AddWithValue("@sunday_overtime_night_Hour", sunday_overtime_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@sunday_overtime_night_Pay", sunday_overtime_night_Pay);

            mysqlCommand.Parameters.AddWithValue("@regular_holiday_Hour", regular_holiday_Hour);
            mysqlCommand.Parameters.AddWithValue("@regular_holiday_Pay", regular_holiday_Pay);
            mysqlCommand.Parameters.AddWithValue("@regular_holiday_overtime_Hour", regular_holiday_overtime_Hour);
            mysqlCommand.Parameters.AddWithValue("@regular_holiday_overtime_Pay", regular_holiday_overtime_Pay);

            mysqlCommand.Parameters.AddWithValue("@regular_holiday_night_Hour", regular_holiday_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@regular_holiday_night_Pay", regular_holiday_night_Pay);
            mysqlCommand.Parameters.AddWithValue("@regular_holiday_overtime_night_Hour", regular_holiday_overtime_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@regular_holiday_overtime_night_Pay", regular_holiday_overtime_night_Pay);

            mysqlCommand.Parameters.AddWithValue("@special_holiday_Hour", special_holiday_Hour);
            mysqlCommand.Parameters.AddWithValue("@special_holiday_pay", special_holiday_Pay);
            mysqlCommand.Parameters.AddWithValue("@special_holiday_overtime_Hour", special_holiday_overtime_Hour);
            mysqlCommand.Parameters.AddWithValue("@special_holiday_overtime_Pay", special_holiday_overtime_Pay);

            mysqlCommand.Parameters.AddWithValue("@special_holiday_night_Hour", special_holiday_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@special_holiday_night_Pay", special_holiday_night_Pay);
            mysqlCommand.Parameters.AddWithValue("@special_holiday_overtime_night_Hour", special_holiday_overtime_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@special_holiday_overtime_night_Pay", special_holiday_overtime_night_Pay);

            mysqlCommand.Parameters.AddWithValue("@totalPay", totalPay);
            mysqlCommand.Parameters.AddWithValue("@remarks", remarks);

            mysqlReader = mysqlCommand.ExecuteReader();
            mysqlConnect.Close();
        }

        // Insert morning time out
        public void insert_morning_timeout(long id, string date, string timeout, int undertime, double undertimepay, string remarks, int basicday, double basicpay)
        {
            mysqlConnect = new MySqlConnection(connectionstring);
            mysqlConnect.Open();

            command = "UPDATE attendance SET timeout = @timeout, undertimehour = @undertime, undertimededuct = @undertimepay, remarksundertime = @remarks," +
                                                        " basicday = @basicday, basicpay = @basicpay WHERE employeeid = @id AND date = @date";

            mysqlCommand = new MySqlCommand(command, mysqlConnect);

            mysqlCommand.Parameters.AddWithValue("@timeout", timeout);
            mysqlCommand.Parameters.AddWithValue("@undertime", undertime);
            mysqlCommand.Parameters.AddWithValue("@undertimepay", undertimepay);
            mysqlCommand.Parameters.AddWithValue("@remarks", remarks);
            mysqlCommand.Parameters.AddWithValue("@basicday", basicday);
            mysqlCommand.Parameters.AddWithValue("@basicpay", basicpay);

            mysqlCommand.Parameters.AddWithValue("@id", id);
            mysqlCommand.Parameters.AddWithValue("@date", date);

            mysqlReader = mysqlCommand.ExecuteReader();
            mysqlConnect.Close();
        }

        // Check if morning attendance exist
        public bool existing_morning_attendance(long id, string date)
        {
            mysqlConnect = new MySqlConnection(connectionstring);
            mysqlConnect.Open();

            command = "SELECT id FROM attendance WHERE employeeid = @id AND date = @date";

            mysqlCommand = new MySqlCommand(command, mysqlConnect);

            mysqlCommand.Parameters.AddWithValue("@id", id);
            mysqlCommand.Parameters.AddWithValue("@date", date);

            mysqlReader = mysqlCommand.ExecuteReader();

            if (mysqlReader.Read())
            {
                return true;
            }

            return false;
        }

        // Check if morning attendance time out exist
        public bool existing_morning_timeout(long id, string date)
        {
            mysqlConnect = new MySqlConnection(connectionstring);
            mysqlConnect.Open();

            command = "SELECT timeout FROM attendance WHERE employeeid = @id AND date = @date";

            mysqlCommand = new MySqlCommand(command, mysqlConnect);

            mysqlCommand.Parameters.AddWithValue("@id", id);
            mysqlCommand.Parameters.AddWithValue("@date", date);

            mysqlReader = mysqlCommand.ExecuteReader();

            if (mysqlReader.Read())
            {
                if (!mysqlReader.IsDBNull(0))
                {
                    return true;
                }
            }

            return false;
        }

        // Update morning time in
        public void update_morning_timein(long id, string date, string timein)
        {
            mysqlConnect = new MySqlConnection(connectionstring);
            mysqlConnect.Open();

            command = "UPDATE attendance SET timein = @timein WHERE employeeid = @id AND date = @date";
            mysqlCommand = new MySqlCommand(command, mysqlConnect);

            mysqlCommand.Parameters.AddWithValue("@id", id);
            mysqlCommand.Parameters.AddWithValue("@date", date);
            mysqlCommand.Parameters.AddWithValue("@timein", timein);

            mysqlReader = mysqlCommand.ExecuteReader();
            mysqlConnect.Close();
        }

        // Insert computation per day
        public void insert_computation_perDay(long id, string date, int regularHour, double regularPay, int overtimeHour, double overtimePay,
              int regular_night_Hour, double regular_night_Pay, int overtime_night_Hour, double overtime_night_Pay,
              int sundayHour, double sundayPay, int sunday_overtimeHour, double sunday_overtimePay, int sunday_night_Hour, double sunday_night_Pay,
              int sunday_overtime_night_Hour, double sunday_overtime_night_Pay, int regular_holiday_Hour, double regular_holiday_Pay,
              int regular_holiday_overtime_Hour, double regular_holiday_overtime_Pay, int regular_holiday_night_Hour, double regular_holiday_night_Pay,
              int regular_holiday_overtime_night_Hour, double regular_holiday_overtime_night_Pay, int special_holiday_Hour,
              double special_holiday_Pay, int special_holiday_overtime_Hour, double special_holiday_overtime_Pay, int special_holiday_night_Hour,
              double special_holiday_night_Pay, int special_holiday_overtime_night_Hour, double special_holiday_overtime_night_Pay, double totalPay)
        {
            mysqlConnect = new MySqlConnection(connectionstring);
            mysqlConnect.Open();

            command = "UPDATE attendance SET regularhour = @regularHour, regularpay = @regularPay, overtimehour = @overtimeHour, overtimepay = @overtimePay," +
                "nighthour = @regular_night_Hour, nightpay = @regular_night_Pay, nightothour = @overtime_night_Hour, nightotpay = @overtime_night_Pay," +
                "sundayhour = @sundayHour, sundaypay = @sundayPay, sundayothour = @sunday_overtimeHour, sundayotpay = @sunday_overtimePay, " +
                "sundaynighthour  = @sunday_night_Hour, sundaynightpay = @sunday_night_Pay, sundaynightothour = @sunday_overtime_night_Hour," +
                "sundaynightotpay = @sunday_overtime_night_Pay, regholhour = @regular_holiday_Hour, regholpay = @regular_holiday_Pay," +
                "regholothour = @regular_holiday_overtime_Hour, regholotpay = @regular_holiday_overtime_Pay, regholnighthour = @regular_holiday_night_Hour, " +
                "regholnightpay = @regular_holiday_night_Pay, regholnightothour = @regular_holiday_overtime_night_Hour, regholnightotpay = @regular_holiday_overtime_night_Pay," +
                "specialholhour = @special_holiday_Hour, specialholpay = @special_holiday_Pay, specialholothour = @special_holiday_overtime_Hour," +
                "specialholotpay = @special_holiday_overtime_Pay, specialholnighthour = @special_holiday_night_Hour, specialholnightpay = @special_holiday_night_Pay," +
                "specialholnightothour = @special_holiday_overtime_night_Hour, specialholnightotpay = @special_holiday_overtime_night_Pay, totalpay = @totalPay WHERE employeeid = @id AND date = @date";

            mysqlCommand = new MySqlCommand(command, mysqlConnect);

            mysqlCommand.Parameters.AddWithValue("@regularHour", regularHour);
            mysqlCommand.Parameters.AddWithValue("@regularPay", regularPay);
            mysqlCommand.Parameters.AddWithValue("@overtimeHour", overtimeHour);
            mysqlCommand.Parameters.AddWithValue("@overtimePay", overtimePay);

            mysqlCommand.Parameters.AddWithValue("@regular_night_Hour", regular_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@regular_night_Pay", regular_night_Pay);
            mysqlCommand.Parameters.AddWithValue("@overtime_night_Hour", overtime_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@overtime_night_Pay", overtime_night_Pay);

            mysqlCommand.Parameters.AddWithValue("@sundayHour", sundayHour);
            mysqlCommand.Parameters.AddWithValue("@sundayPay", sundayPay);
            mysqlCommand.Parameters.AddWithValue("@sunday_overtimeHour", sunday_overtimeHour);
            mysqlCommand.Parameters.AddWithValue("@sunday_overtimePay", sunday_overtimePay);

            mysqlCommand.Parameters.AddWithValue("@sunday_night_Hour", sunday_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@sunday_night_Pay", sunday_night_Pay);
            mysqlCommand.Parameters.AddWithValue("@sunday_overtime_night_Hour", sunday_overtime_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@sunday_overtime_night_Pay", sunday_overtime_night_Pay);

            mysqlCommand.Parameters.AddWithValue("@regular_holiday_Hour", regular_holiday_Hour);
            mysqlCommand.Parameters.AddWithValue("@regular_holiday_Pay", regular_holiday_Pay);
            mysqlCommand.Parameters.AddWithValue("@regular_holiday_overtime_Hour", regular_holiday_overtime_Hour);
            mysqlCommand.Parameters.AddWithValue("@regular_holiday_overtime_Pay", regular_holiday_overtime_Pay);

            mysqlCommand.Parameters.AddWithValue("@regular_holiday_night_Hour", regular_holiday_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@regular_holiday_night_Pay", regular_holiday_night_Pay);
            mysqlCommand.Parameters.AddWithValue("@regular_holiday_overtime_night_Hour", regular_holiday_overtime_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@regular_holiday_overtime_night_Pay", regular_holiday_overtime_night_Pay);

            mysqlCommand.Parameters.AddWithValue("@special_holiday_Hour", special_holiday_Hour);
            mysqlCommand.Parameters.AddWithValue("@special_holiday_pay", special_holiday_Pay);
            mysqlCommand.Parameters.AddWithValue("@special_holiday_overtime_Hour", special_holiday_overtime_Hour);
            mysqlCommand.Parameters.AddWithValue("@special_holiday_overtime_Pay", special_holiday_overtime_Pay);

            mysqlCommand.Parameters.AddWithValue("@special_holiday_night_Hour", special_holiday_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@special_holiday_night_Pay", special_holiday_night_Pay);
            mysqlCommand.Parameters.AddWithValue("@special_holiday_overtime_night_Hour", special_holiday_overtime_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@special_holiday_overtime_night_Pay", special_holiday_overtime_night_Pay);

            mysqlCommand.Parameters.AddWithValue("@totalPay", totalPay);
            mysqlCommand.Parameters.AddWithValue("@id", id);
            mysqlCommand.Parameters.AddWithValue("@date", date);

            mysqlReader = mysqlCommand.ExecuteReader();
            mysqlConnect.Close();
        }

        // Get Position
        public string getPosition(long id)
        {
            string position = null;

            mysqlConnect = new MySqlConnection(connectionstring);
            mysqlConnect.Open();

            command = "SELECT position FROM emp_workdetails WHERE employeeid = @id";

            mysqlCommand = new MySqlCommand(command, mysqlConnect);

            mysqlCommand.Parameters.AddWithValue("@id", id);

            mysqlReader = mysqlCommand.ExecuteReader();

            if (mysqlReader.Read())
            {
                position = mysqlReader.GetValue(0).ToString();
            }

            mysqlConnect.Close();
            return position;
        }

        // Get Shift
        public string getShift(long id)
        {
            string shift = null;

            mysqlConnect = new MySqlConnection(connectionstring);
            mysqlConnect.Open();

            command = "SELECT shift FROM emp_workdetails WHERE employeeid = @id";

            mysqlCommand = new MySqlCommand(command, mysqlConnect);

            mysqlCommand.Parameters.AddWithValue("@id", id);

            mysqlReader = mysqlCommand.ExecuteReader();

            if (mysqlReader.Read())
            {
                shift = mysqlReader.GetValue(0).ToString();
            }

            mysqlConnect.Close();
            return shift;
        }

        // Get Rate
        public double getRate(long id)
        {
            double rate = 0;

            mysqlConnect = new MySqlConnection(connectionstring);
            mysqlConnect.Open();

            command = "SELECT rate FROM emp_workdetails WHERE employeeid = @id";
            mysqlCommand = new MySqlCommand(command, mysqlConnect);

            mysqlCommand.Parameters.AddWithValue("@id", id);

            mysqlReader = mysqlCommand.ExecuteReader();

            if (mysqlReader.Read())
            {
                rate = double.Parse(mysqlReader.GetValue(0).ToString());
            }

            return rate;
        }

        // Get time in
        public string getTimein(long id, string date)
        {
            string timein = null;

            mysqlConnect = new MySqlConnection(connectionstring);
            mysqlConnect.Open();

            command = "SELECT timein FROM attendance WHERE employeeid = @id AND date = @date";
            mysqlCommand = new MySqlCommand(command, mysqlConnect);

            mysqlCommand.Parameters.AddWithValue("@id", id);
            mysqlCommand.Parameters.AddWithValue("@date", date);

            mysqlReader = mysqlCommand.ExecuteReader();

            if (mysqlReader.Read())
            {
                timein = mysqlReader.GetValue(0).ToString();
            }

            mysqlConnect.Close();
            return timein;
        }

        // Get time out
        public string getTimeout(long id, string date)
        {
            string timeout = null;

            mysqlConnect = new MySqlConnection(connectionstring);
            mysqlConnect.Open();

            command = "SELECT timeout FROM attendance WHERE employeeid = @id AND date = @date";
            mysqlCommand = new MySqlCommand(command, mysqlConnect);

            mysqlCommand.Parameters.AddWithValue("@id", id);
            mysqlCommand.Parameters.AddWithValue("@date", date);

            mysqlReader = mysqlCommand.ExecuteReader();

            if (mysqlReader.Read())
            {
                timeout = mysqlReader.GetValue(0).ToString();
            }

            mysqlConnect.Close();
            return timeout;
        }


        /* ********** Night Shift ********** */


        // Check if night attendance exist yesterday
        public bool existing_night_attendance_yesterday(long id, string date, string shift)
        {
            mysqlConnect = new MySqlConnection(connectionstring);
            mysqlConnect.Open();

            command = "SELECT id FROM attendance WHERE employeeid = @id AND date = @date AND shift = @shift";

            mysqlCommand = new MySqlCommand(command, mysqlConnect);

            mysqlCommand.Parameters.AddWithValue("@id", id);
            mysqlCommand.Parameters.AddWithValue("@date", date);
            mysqlCommand.Parameters.AddWithValue("@shift", shift);

            mysqlReader = mysqlCommand.ExecuteReader();

            if (mysqlReader.Read())
            {
                return true;
            }

            return false;
        }

        // Check if night attendance exist
        public bool existing_night_attendance(long id, string date, string shift)
        {
            mysqlConnect = new MySqlConnection(connectionstring);
            mysqlConnect.Open();

            command = "SELECT id FROM attendance WHERE employeeid = @id AND date = @date AND shift = @shift";

            mysqlCommand = new MySqlCommand(command, mysqlConnect);

            mysqlCommand.Parameters.AddWithValue("@id", id);
            mysqlCommand.Parameters.AddWithValue("@date", date);
            mysqlCommand.Parameters.AddWithValue("@shift", shift);

            mysqlReader = mysqlCommand.ExecuteReader();

            if (mysqlReader.Read())
            {
                return true;
            }

            return false;
        }

        // Check if night attendance time out exist
        public bool existing_night_timeout(long id, string date, string shift)
        {
            mysqlConnect = new MySqlConnection(connectionstring);
            mysqlConnect.Open();

            command = "SELECT timeout FROM attendance WHERE employeeid = @id AND date = @date AND shift = @shift";

            mysqlCommand = new MySqlCommand(command, mysqlConnect);

            mysqlCommand.Parameters.AddWithValue("@id", id);
            mysqlCommand.Parameters.AddWithValue("@date", date);
            mysqlCommand.Parameters.AddWithValue("@shift", shift);

            mysqlReader = mysqlCommand.ExecuteReader();

            if (mysqlReader.Read())
            {
                if (!mysqlReader.IsDBNull(0))
                {
                    return true;
                }
            }

            return false;
        }

        // Get night time out
        public string getTimeout_night(long id, string date, string shift)
        {
            string timeout = null;

            mysqlConnect = new MySqlConnection(connectionstring);
            mysqlConnect.Open();

            command = "SELECT timeout FROM attendance WHERE employeeid = @id AND date = @date AND shift = @shift";
            mysqlCommand = new MySqlCommand(command, mysqlConnect);

            mysqlCommand.Parameters.AddWithValue("@id", id);
            mysqlCommand.Parameters.AddWithValue("@date", date);
            mysqlCommand.Parameters.AddWithValue("@shift", shift);

            mysqlReader = mysqlCommand.ExecuteReader();

            if (mysqlReader.Read())
            {
                timeout = mysqlReader.GetValue(0).ToString();
            }

            mysqlConnect.Close();
            return timeout;
        }

        // Get night time in 
        public string getTimein_night(long id, string date, string shift)
        {
            string timein = null;

            mysqlConnect = new MySqlConnection(connectionstring);
            mysqlConnect.Open();

            command = "SELECT timein FROM attendance WHERE employeeid = @id AND date = @date AND shift = @shift";
            mysqlCommand = new MySqlCommand(command, mysqlConnect);

            mysqlCommand.Parameters.AddWithValue("@id", id);
            mysqlCommand.Parameters.AddWithValue("@date", date);
            mysqlCommand.Parameters.AddWithValue("@shift", shift);

            mysqlReader = mysqlCommand.ExecuteReader();

            if (mysqlReader.Read())
            {
                timein = mysqlReader.GetValue(0).ToString();
            }

            mysqlConnect.Close();
            return timein;
        }

        // Insert night time out
        public void insert_night_timeout(long id, string date, string timeout, string shift)
        {
            mysqlConnect = new MySqlConnection(connectionstring);
            mysqlConnect.Open();

            command = "UPDATE attendance SET timeout = @timeout WHERE employeeid = @id AND date = @date AND shift = @shift";
            mysqlCommand = new MySqlCommand(command, mysqlConnect);

            mysqlCommand.Parameters.AddWithValue("@timeout", timeout);
            mysqlCommand.Parameters.AddWithValue("@id", id);
            mysqlCommand.Parameters.AddWithValue("@date", date);
            mysqlCommand.Parameters.AddWithValue("@shift", shift);

            mysqlReader = mysqlCommand.ExecuteReader();
            mysqlConnect.Close();
        }

        // Insert computation per day night
        public void insert_computation_perDay_night(long id, string date, int regularHour, double regularPay, int overtimeHour, double overtimePay,
              int regular_night_Hour, double regular_night_Pay, int overtime_night_Hour, double overtime_night_Pay,
              int sundayHour, double sundayPay, int sunday_overtimeHour, double sunday_overtimePay, int sunday_night_Hour, double sunday_night_Pay,
              int sunday_overtime_night_Hour, double sunday_overtime_night_Pay, int regular_holiday_Hour, double regular_holiday_Pay,
              int regular_holiday_overtime_Hour, double regular_holiday_overtime_Pay, int regular_holiday_night_Hour, double regular_holiday_night_Pay,
              int regular_holiday_overtime_night_Hour, double regular_holiday_overtime_night_Pay, int special_holiday_Hour,
              double special_holiday_Pay, int special_holiday_overtime_Hour, double special_holiday_overtime_Pay, int special_holiday_night_Hour,
              double special_holiday_night_Pay, int special_holiday_overtime_night_Hour, double special_holiday_overtime_night_Pay, double totalPay)
        {   

            mysqlConnect = new MySqlConnection(connectionstring);
            mysqlConnect.Open();

            command = "UPDATE attendance SET regularhour = @regularHour, regularpay = @regularPay, overtimehour = @overtimeHour, overtimepay = @overtimePay," +
                "nighthour = @regular_night_Hour, nightpay = @regular_night_Pay, nightothour = @overtime_night_Hour, nightotpay = @overtime_night_Pay," +
                "sundayhour = @sundayHour, sundaypay = @sundayPay, sundayothour = @sunday_overtimeHour, sundayotpay = @sunday_overtimePay, " +
                "sundaynighthour  = @sunday_night_Hour, sundaynightpay = @sunday_night_Pay, sundaynightothour = @sunday_overtime_night_Hour," +
                "sundaynightotpay = @sunday_overtime_night_Pay, regholhour = @regular_holiday_Hour, regholpay = @regular_holiday_Pay," +
                "regholothour = @regular_holiday_overtime_Hour, regholotpay = @regular_holiday_overtime_Pay, regholnighthour = @regular_holiday_night_Hour, " +
                "regholnightpay = @regular_holiday_night_Pay, regholnightothour = @regular_holiday_overtime_night_Hour, regholnightotpay = @regular_holiday_overtime_night_Pay," +
                "specialholhour = @special_holiday_Hour, specialholpay = @special_holiday_Pay, specialholothour = @special_holiday_overtime_Hour," +
                "specialholotpay = @special_holiday_overtime_Pay, specialholnighthour = @special_holiday_night_Hour, specialholnightpay = @special_holiday_night_Pay," +
                "specialholnightothour = @special_holiday_overtime_night_Hour, specialholnightotpay = @special_holiday_overtime_night_Pay, totalpay = @totalPay WHERE employeeid = @id AND date = @date";

            mysqlCommand = new MySqlCommand(command, mysqlConnect);

            mysqlCommand.Parameters.AddWithValue("@regularHour", regularHour);
            mysqlCommand.Parameters.AddWithValue("@regularPay", regularPay);
            mysqlCommand.Parameters.AddWithValue("@overtimeHour", overtimeHour);
            mysqlCommand.Parameters.AddWithValue("@overtimePay", overtimePay);

            mysqlCommand.Parameters.AddWithValue("@regular_night_Hour", regular_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@regular_night_Pay", regular_night_Pay);
            mysqlCommand.Parameters.AddWithValue("@overtime_night_Hour", overtime_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@overtime_night_Pay", overtime_night_Pay);

            mysqlCommand.Parameters.AddWithValue("@sundayHour", sundayHour);
            mysqlCommand.Parameters.AddWithValue("@sundayPay", sundayPay);
            mysqlCommand.Parameters.AddWithValue("@sunday_overtimeHour", sunday_overtimeHour);
            mysqlCommand.Parameters.AddWithValue("@sunday_overtimePay", sunday_overtimePay);

            mysqlCommand.Parameters.AddWithValue("@sunday_night_Hour", sunday_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@sunday_night_Pay", sunday_night_Pay);
            mysqlCommand.Parameters.AddWithValue("@sunday_overtime_night_Hour", sunday_overtime_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@sunday_overtime_night_Pay", sunday_overtime_night_Pay);

            mysqlCommand.Parameters.AddWithValue("@regular_holiday_Hour", regular_holiday_Hour);
            mysqlCommand.Parameters.AddWithValue("@regular_holiday_Pay", regular_holiday_Pay);
            mysqlCommand.Parameters.AddWithValue("@regular_holiday_overtime_Hour", regular_holiday_overtime_Hour);
            mysqlCommand.Parameters.AddWithValue("@regular_holiday_overtime_Pay", regular_holiday_overtime_Pay);

            mysqlCommand.Parameters.AddWithValue("@regular_holiday_night_Hour", regular_holiday_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@regular_holiday_night_Pay", regular_holiday_night_Pay);
            mysqlCommand.Parameters.AddWithValue("@regular_holiday_overtime_night_Hour", regular_holiday_overtime_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@regular_holiday_overtime_night_Pay", regular_holiday_overtime_night_Pay);

            mysqlCommand.Parameters.AddWithValue("@special_holiday_Hour", special_holiday_Hour);
            mysqlCommand.Parameters.AddWithValue("@special_holiday_pay", special_holiday_Pay);
            mysqlCommand.Parameters.AddWithValue("@special_holiday_overtime_Hour", special_holiday_overtime_Hour);
            mysqlCommand.Parameters.AddWithValue("@special_holiday_overtime_Pay", special_holiday_overtime_Pay);

            mysqlCommand.Parameters.AddWithValue("@special_holiday_night_Hour", special_holiday_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@special_holiday_night_Pay", special_holiday_night_Pay);
            mysqlCommand.Parameters.AddWithValue("@special_holiday_overtime_night_Hour", special_holiday_overtime_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@special_holiday_overtime_night_Pay", special_holiday_overtime_night_Pay);

            mysqlCommand.Parameters.AddWithValue("@totalPay", totalPay);
            mysqlCommand.Parameters.AddWithValue("@id", id);
            mysqlCommand.Parameters.AddWithValue("@date", date);

            mysqlReader = mysqlCommand.ExecuteReader();
            mysqlConnect.Close();
        }

        // Update morning time in
        public void update_night_timein(long id, string date, string timein, string shift)
        {
            mysqlConnect = new MySqlConnection(connectionstring);
            mysqlConnect.Open();

            command = "UPDATE attendance SET timein = @timein WHERE employeeid = @id AND date = @date AND shift = @shift";
            mysqlCommand = new MySqlCommand(command, mysqlConnect);

            mysqlCommand.Parameters.AddWithValue("@id", id);
            mysqlCommand.Parameters.AddWithValue("@date", date);
            mysqlCommand.Parameters.AddWithValue("@timein", timein);
            mysqlCommand.Parameters.AddWithValue("@shift", shift);

            mysqlReader = mysqlCommand.ExecuteReader();
            mysqlConnect.Close();
        }

        // Insert night time in
        public void insert_night_timein(long id, string shift, string date, string timein)
        {
            int regularHour = 0, regular_night_Hour = 0;
            double regularPay = 0, regular_night_Pay = 0;
            int overtimeHour = 0, overtime_night_Hour = 0;
            double overtimePay = 0, overtime_night_Pay = 0;

            int sundayHour = 0, sunday_night_Hour = 0;
            double sundayPay = 0, sunday_night_Pay = 0;
            int sunday_overtimeHour = 0, sunday_overtime_night_Hour = 0;
            double sunday_overtimePay = 0, sunday_overtime_night_Pay = 0;

            int regular_holiday_Hour = 0, regular_holiday_night_Hour = 0;
            double regular_holiday_Pay = 0, regular_holiday_night_Pay = 0;
            int regular_holiday_overtime_Hour = 0, regular_holiday_overtime_night_Hour = 0;
            double regular_holiday_overtime_Pay = 0, regular_holiday_overtime_night_Pay = 0;

            int special_holiday_Hour = 0, special_holiday_night_Hour = 0;
            double special_holiday_Pay = 0, special_holiday_night_Pay = 0;
            int special_holiday_overtime_Hour = 0, special_holiday_overtime_night_Hour = 0;
            double special_holiday_overtime_Pay = 0, special_holiday_overtime_night_Pay = 0;

            double totalPay = 0;

            mysqlConnect = new MySqlConnection(connectionstring);
            mysqlConnect.Open();

            command = "INSERT INTO attendance (employeeid, shift, date, timein, " +
                "regularhour, regularpay, overtimehour, overtimepay, nighthour, nightpay, nightothour, nightotpay," +
                "sundayhour, sundaypay, sundayothour, sundayotpay, sundaynighthour, sundaynightpay, sundaynightothour, sundaynightotpay," +
                "regholhour, regholpay, regholothour, regholotpay, regholnighthour, regholnightpay, regholnightothour, regholnightotpay," +
                "specialholhour, specialholpay, specialholothour, specialholotpay, specialholnighthour, specialholnightpay, specialholnightothour, specialholnightotpay, totalpay) " +
                "VALUES (@id, @shift, @date, @timein, " +
                "@regularHour, @regularPay, @overtimeHour, @overtimePay, @regular_night_Hour, @regular_night_Pay, @overtime_night_Hour, @overtime_night_Pay," +
                "@sundayHour, @sundayPay, @sunday_overtimeHour, @sunday_overtimePay, @sunday_night_Hour, @sunday_night_Pay, @sunday_overtime_night_Hour, @sunday_overtime_night_Pay," +
                "@regular_holiday_Hour, @regular_holiday_Pay, @regular_holiday_overtime_Hour, @regular_holiday_overtime_Pay," +
                "@regular_holiday_night_Hour, @regular_holiday_night_Pay, @regular_holiday_overtime_night_Hour, @regular_holiday_overtime_night_Pay," +
                "@special_holiday_Hour, @special_holiday_pay, @special_holiday_overtime_Hour, @special_holiday_overtime_Pay," +
                "@special_holiday_night_Hour, @special_holiday_night_Pay, @special_holiday_overtime_night_Hour, @special_holiday_overtime_night_Pay, @totalPay)";

            mysqlCommand = new MySqlCommand(command, mysqlConnect);

            mysqlCommand.Parameters.AddWithValue("@id", id);
            mysqlCommand.Parameters.AddWithValue("@shift", shift);
            mysqlCommand.Parameters.AddWithValue("@date", date);
            mysqlCommand.Parameters.AddWithValue("@timein", timein);

            mysqlCommand.Parameters.AddWithValue("@regularHour", regularHour);
            mysqlCommand.Parameters.AddWithValue("@regularPay", regularPay);
            mysqlCommand.Parameters.AddWithValue("@overtimeHour", overtimeHour);
            mysqlCommand.Parameters.AddWithValue("@overtimePay", overtimePay);

            mysqlCommand.Parameters.AddWithValue("@regular_night_Hour", regular_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@regular_night_Pay", regular_night_Pay);
            mysqlCommand.Parameters.AddWithValue("@overtime_night_Hour", overtime_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@overtime_night_Pay", overtime_night_Pay);

            mysqlCommand.Parameters.AddWithValue("@sundayHour", sundayHour);
            mysqlCommand.Parameters.AddWithValue("@sundayPay", sundayPay);
            mysqlCommand.Parameters.AddWithValue("@sunday_overtimeHour", sunday_overtimeHour);
            mysqlCommand.Parameters.AddWithValue("@sunday_overtimePay", sunday_overtimePay);

            mysqlCommand.Parameters.AddWithValue("@sunday_night_Hour", sunday_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@sunday_night_Pay", sunday_night_Pay);
            mysqlCommand.Parameters.AddWithValue("@sunday_overtime_night_Hour", sunday_overtime_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@sunday_overtime_night_Pay", sunday_overtime_night_Pay);

            mysqlCommand.Parameters.AddWithValue("@regular_holiday_Hour", regular_holiday_Hour);
            mysqlCommand.Parameters.AddWithValue("@regular_holiday_Pay", regular_holiday_Pay);
            mysqlCommand.Parameters.AddWithValue("@regular_holiday_overtime_Hour", regular_holiday_overtime_Hour);
            mysqlCommand.Parameters.AddWithValue("@regular_holiday_overtime_Pay", regular_holiday_overtime_Pay);

            mysqlCommand.Parameters.AddWithValue("@regular_holiday_night_Hour", regular_holiday_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@regular_holiday_night_Pay", regular_holiday_night_Pay);
            mysqlCommand.Parameters.AddWithValue("@regular_holiday_overtime_night_Hour", regular_holiday_overtime_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@regular_holiday_overtime_night_Pay", regular_holiday_overtime_night_Pay);

            mysqlCommand.Parameters.AddWithValue("@special_holiday_Hour", special_holiday_Hour);
            mysqlCommand.Parameters.AddWithValue("@special_holiday_pay", special_holiday_Pay);
            mysqlCommand.Parameters.AddWithValue("@special_holiday_overtime_Hour", special_holiday_overtime_Hour);
            mysqlCommand.Parameters.AddWithValue("@special_holiday_overtime_Pay", special_holiday_overtime_Pay);

            mysqlCommand.Parameters.AddWithValue("@special_holiday_night_Hour", special_holiday_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@special_holiday_night_Pay", special_holiday_night_Pay);
            mysqlCommand.Parameters.AddWithValue("@special_holiday_overtime_night_Hour", special_holiday_overtime_night_Hour);
            mysqlCommand.Parameters.AddWithValue("@special_holiday_overtime_night_Pay", special_holiday_overtime_night_Pay);

            mysqlCommand.Parameters.AddWithValue("@totalPay", totalPay);

            mysqlReader = mysqlCommand.ExecuteReader();
            mysqlConnect.Close();
        }



        public string get_holiday(string date)
        {
            string get_holiday = null;

            mysqlConnect = new MySqlConnection(connectionstring);
            mysqlConnect.Open();

            command = "SELECT holidaytype FROM holidays WHERE date = @date ";
            mysqlCommand = new MySqlCommand(command, mysqlConnect);

            mysqlCommand.Parameters.AddWithValue("@date", date);

            mysqlReader = mysqlCommand.ExecuteReader();

            if (mysqlReader.Read())
            {
                get_holiday = mysqlReader.GetValue(0).ToString();
            }

            mysqlConnect.Close();

            return get_holiday;
        }

        // Check Half day
        public bool check_halday(long id, string date)
        {
            mysqlConnect = new MySqlConnection(connectionstring);
            mysqlConnect.Open();

            command = "SELECT id FROM halfdayleave WHERE employeeid = @id AND date = @date AND leavetype = 'Leave With pay'";

            mysqlCommand = new MySqlCommand(command, mysqlConnect);

            mysqlCommand.Parameters.AddWithValue("@id", id);
            mysqlCommand.Parameters.AddWithValue("@date", date);

            mysqlReader = mysqlCommand.ExecuteReader();

            if (mysqlReader.Read())
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
