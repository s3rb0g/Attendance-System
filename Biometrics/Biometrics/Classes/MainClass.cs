using DPFP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biometrics.Classes
{
    internal class MainClass
    {
        DatabaseClass database = new DatabaseClass();

        public int regularHour = 0, regular_night_Hour = 0;
        public double regularPay = 0, regular_night_Pay = 0;
        public int overtimeHour = 0, overtime_night_Hour = 0;
        public double overtimePay = 0, overtime_night_Pay = 0;

        public int sundayHour = 0, sunday_night_Hour = 0;
        public double sundayPay = 0, sunday_night_Pay = 0;
        public int sunday_overtimeHour = 0, sunday_overtime_night_Hour = 0;
        public double sunday_overtimePay = 0, sunday_overtime_night_Pay = 0;

        public int regular_holiday_Hour = 0, regular_holiday_night_Hour = 0;
        public double regular_holiday_Pay = 0, regular_holiday_night_Pay = 0;
        public int regular_holiday_overtime_Hour = 0, regular_holiday_overtime_night_Hour = 0;
        public double regular_holiday_overtime_Pay = 0, regular_holiday_overtime_night_Pay = 0;

        public int special_holiday_Hour = 0, special_holiday_night_Hour = 0;
        public double special_holiday_Pay = 0, special_holiday_night_Pay = 0;
        public int special_holiday_overtime_Hour = 0, special_holiday_overtime_night_Hour = 0;
        public double special_holiday_overtime_Pay = 0, special_holiday_overtime_night_Pay = 0;

        public double totalPay;
        public int basicday = 0;
        public double basicpay = 0;

        public string day_rate = "";

        public void morning_calculation(int hoursWorked, double ratePerDay, string day, string date)
        {
            reset_variables();

            double ratePerHour = ratePerDay / 8;

            // check first if sunday
            if (day == "Sunday")
            {
                ratePerHour *= 1.30;
                day_rate = "Sunday";
            }

            // check if holiday today
            string holiday = database.get_holiday(date);


            if (holiday == "Regular")
            {
                ratePerHour *= 2;
                day_rate = "Regular";
            }
            else if (holiday == "Special")
            {
                ratePerHour *= 1.30;
                day_rate = "Special"; 
            }


            double overtimePerHour = ratePerHour * 1.25;

            int hour = 0;
            double pay = 0;

            int othour = 0;
            double otpay = 0;

            if (hoursWorked > 8)
            {
                othour = hoursWorked - 8;
                otpay = othour * overtimePerHour;
                hour = 8;
                pay = ratePerHour * 8;

                save_hour_pay(hour, pay, othour, otpay, ratePerDay);
                totalPay = pay + otpay;
            }
            else
            {
                hour = 8;
                pay = 8 * ratePerHour;
                othour = 0;
                otpay = 0;

                save_hour_pay(hour, pay, othour, otpay, ratePerDay);
                totalPay = pay + otpay;
            }

        }

        public void officestaff_calculation(long id, int hoursWorked, double ratePerDay, string day, string date)
        {
            reset_variables();

            double ratePerHour = ratePerDay / 8;


            bool check = database.check_halday(id, date);

            if (check)
            {
                hoursWorked += 4;
            }

            hoursWorked -= 1;

            // check first if sunday
            if (day == "Sunday")
            {
                ratePerHour *= 1.30;
                day_rate = "Sunday";
            }

            // check if holiday today
            string holiday = database.get_holiday(date);


            if (holiday == "Regular")
            {
                ratePerHour *= 2;
                day_rate = "Regular";
            }
            else if (holiday == "Special")
            {
                ratePerHour *= 1.30;
                day_rate = "Special";
            }


            double overtimePerHour = ratePerHour * 1.25;

            int hour = 0;
            double pay = 0;

            int othour = 0;
            double otpay = 0;

            if (hoursWorked > 8)
            {
                othour = hoursWorked - 8;
                otpay = othour * overtimePerHour;
                hour = 8;
                pay = ratePerHour * 8;

                save_hour_pay(hour, pay, othour, otpay, ratePerDay);
                totalPay = pay + otpay;
            }
            else
            {
                hour = 8;
                pay = 8 * ratePerHour;
                othour = 0;
                otpay = 0;

                save_hour_pay(hour, pay, othour, otpay, ratePerDay);
                totalPay = pay + otpay;
            }

        }

        public void night_calculation(int hoursWorked, double ratePerDay, string day, string date)
        {
            reset_variables();

            double ratePerHour = ratePerDay / 8;
            ratePerHour *= 1.1;

            // check first if sunday
            if (day == "Sunday")
            {
                ratePerHour *= 1.30;
                day_rate = "Sunday";
            }

            // check if holiday today
            string holiday = database.get_holiday(date);

            if (holiday == "Regular")
            {
                ratePerHour *= 2;
                day_rate = "Regular";
            }
            else if (holiday == "Special")
            {
                ratePerHour *= 1.30;
                day_rate = "Special";
            }

            int hour = 0;
            double pay = 0;

            int othour = 0;
            double otpay = 0;

            double overtimePerHour = ratePerHour * 1.25;

            if (hoursWorked > 8)
            {
                othour = hoursWorked - 8;
                otpay = othour * overtimePerHour;
                hour = 8;
                pay = ratePerHour * 8;

                save_hour_pay_night(hour, pay, othour, otpay);
                totalPay = pay + otpay;
            }
            else
            {
                hour = hoursWorked;
                pay = hoursWorked * ratePerHour;
                othour = 0;
                otpay = 0;

                save_hour_pay_night(hour, pay, othour, otpay);
                totalPay = pay + otpay;
            }

        }



        public void save_hour_pay(int hour, double pay, int othour, double otpay, double rate)
        {
            if (day_rate == "Sunday")
            {
                sundayHour = hour;
                sundayPay = pay;
                sunday_overtimeHour = othour;
                sunday_overtimePay = otpay;
            }
            else if (day_rate == "Regular")
            {
                regular_holiday_Hour = hour;
                regular_holiday_Pay = pay;
                regular_holiday_overtime_Hour = othour;
                regular_holiday_overtime_Pay = otpay;
            }
            else if (day_rate == "Special")
            {
                special_holiday_Hour = hour;
                special_holiday_Pay = pay;
                special_holiday_overtime_Hour = othour;
                special_holiday_overtime_Pay = otpay;
            }
            else
            {
                regularHour = hour;
                regularPay = pay;
                overtimeHour = othour;
                overtimePay = otpay;

                basicpay = rate;
                basicday = 1;
            }
        }

        public void save_hour_pay_night(int hour, double pay, int othour, double otpay)
        {
            if (day_rate == "Sunday")
            {
                sunday_night_Hour = hour;
                sunday_night_Pay = pay;
                sunday_overtime_night_Hour = othour;
                sunday_overtime_night_Pay = otpay;
            }
            else if (day_rate == "Regular")
            {
                regular_holiday_night_Hour = hour;
                regular_holiday_night_Pay = pay;
                regular_holiday_overtime_night_Hour = othour;
                regular_holiday_overtime_night_Pay = otpay;
            }
            else if (day_rate == "Special")
            {
                special_holiday_night_Hour = hour;
                special_holiday_night_Pay = pay;
                special_holiday_overtime_night_Hour = othour;
                special_holiday_overtime_night_Pay = otpay;
            }
            else
            {
                regular_night_Hour = hour;
                regular_night_Pay = pay;
                overtime_night_Hour = othour;
                overtime_night_Pay = otpay;
            }
        }

        public void reset_variables()
        {
            day_rate = "";

            regularHour = 0; 
            regular_night_Hour = 0;
            regularPay = 0;
            regular_night_Pay = 0;
            overtimeHour = 0;
            overtime_night_Hour = 0;
            overtimePay = 0;
            overtime_night_Pay = 0;

            sundayHour = 0;
            sunday_night_Hour = 0;
            sundayPay = 0;
            sunday_night_Pay = 0;
            sunday_overtimeHour = 0;
            sunday_overtime_night_Hour = 0;
            sunday_overtimePay = 0;
            sunday_overtime_night_Pay = 0;

            regular_holiday_Hour = 0;
            regular_holiday_night_Hour = 0;
            regular_holiday_Pay = 0;
            regular_holiday_night_Pay = 0;
            regular_holiday_overtime_Hour = 0;
            regular_holiday_overtime_night_Hour = 0;
            regular_holiday_overtime_Pay = 0;
            regular_holiday_overtime_night_Pay = 0;

            special_holiday_Hour = 0;
            special_holiday_night_Hour = 0;
            special_holiday_Pay = 0;
            special_holiday_night_Pay = 0;
            special_holiday_overtime_Hour = 0;
            special_holiday_overtime_night_Hour = 0;
            special_holiday_overtime_Pay = 0;
            special_holiday_overtime_night_Pay = 0;

            basicday = 0;
            basicpay = 0;

        }





    }
}
