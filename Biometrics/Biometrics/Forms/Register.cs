using Biometrics.Classes;
using DPFP;
using DPFP.Capture;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biometrics.Forms
{
    public partial class Register : Form, DPFP.Capture.EventHandler
    {
        private DPFP.Capture.Capture Capturer;

        private DPFP.Template Template;

        private DPFP.Processing.Enrollment Enroller;

        private bool recordfound = false;

        private string connectionstring = "datasource=localhost;username=root;password=;database=camanco";
        private string command = "";

        private MySqlConnection mysqlConnect;
        private MySqlCommand mysqlCommand;
        private MySqlDataReader mysqlReader;

        private long employeeId = 0;

        private DatabaseClass databaseClass = new DatabaseClass();
    
        public Register()
        {
            InitializeComponent();
        }

        private void Register_Load(object sender, EventArgs e)
        {
            label_visible_false();
            btn_capture_cancel.Visible = false;
            lbl_fingerneeded.Visible = false;

            MakeReport("Please find a record to register fingerprint.");

            Enroller = new DPFP.Processing.Enrollment();

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

        private void txt_id_TextChanged(object sender, EventArgs e)
        {
            if (long.TryParse(txt_id.Text, out long id))
            {
                mysqlConnect = new MySqlConnection(connectionstring);
                mysqlConnect.Open();

                command = "SELECT firstname, middlename, lastname, birthday, picture FROM employees WHERE id = @id";

                mysqlCommand = new MySqlCommand(command, mysqlConnect);
                mysqlCommand.Parameters.AddWithValue("@id", id);

                mysqlReader = mysqlCommand.ExecuteReader();

                recordfound = false;

                while (mysqlReader.Read())
                {
                    recordfound = true;

                    lbl_fname_insert.Text = mysqlReader.GetValue(0).ToString();
                    lbl_mname_insert.Text = mysqlReader.GetValue(1).ToString();
                    lbl_lname_insert.Text = mysqlReader.GetValue(2).ToString();

                    DateTime birthday = mysqlReader.GetDateTime(3);
                    lbl_birthday_insert.Text = birthday.ToString("dd/MM/yyyy");

                    string pic_path = mysqlReader.GetValue(4).ToString();
                    show_picture(pic_path);

                    label_visible_true();
                }
                mysqlConnect.Close();

                if (!recordfound)
                {
                    label_visible_false();
                    MakeReport("Record not found.");
                }
                else
                {
                    MakeReport("Record found. You can now register fingerprint.");
                }
            }
            else
            {
                MakeReport("Please find a record to register fingerprint.");
                recordfound = false;
                label_visible_false();
            }
        }

        // Button Capture
        private void btn_capture_Click(object sender, EventArgs e)
        {
            if (recordfound)
            {
                btn_capture.Visible = false;
                btn_capture_cancel.Visible = true;
                txt_id.Enabled = false;

                lbl_fingerneeded.Visible = true;
                FingerNeededUpdate();

                employeeId = long.Parse(txt_id.Text);

                bool checked_fingerprint = databaseClass.existing_fingerprint(employeeId);

                if (checked_fingerprint)
                {
                    DialogResult result = MessageBox.Show("Fingerprint is already registered \nDo you want to register again??", "Fingerprint Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        Capturer.StartCapture();
                        MakeReport("Fingerprint scanner is ready.");
                        btn_close.Enabled = false;
                    }
                    else
                    {
                        btn_capture.Visible = true;
                        btn_capture_cancel.Visible = false;
                        txt_id.Enabled = true;
                        lbl_fingerneeded.Visible = false;

                        MakeReport("Record found. You can now register fingerprint.");
                    }

                }
                else
                {
                    Capturer.StartCapture();
                    MakeReport("Fingerprint scanner is ready.");
                    btn_close.Enabled = false;
                }

            }
            else
            {
                btn_capture.Visible = true;
                btn_capture_cancel.Visible = false;
                txt_id.Enabled = true;
                lbl_fingerneeded.Visible = false;

                MakeReport("Please find a record first to register fingerprint.");
            }
        }

        private void finish()
        {
            this.Invoke(new Action(delegate ()
            {
                btn_capture.Visible = true;
                btn_capture_cancel.Visible = false;
                txt_id.Enabled = true;
                btn_close.Enabled = true;
                pic_fingerprint.Image = null;
                lbl_fingerneeded.Visible = false;
                Enroller.Clear();
                MakeReport("Record found. Fingerprint registration completed.");
            }));
        }

        // Button Cancel
        private void btn_capture_cancel_Click(object sender, EventArgs e)
        {
            btn_capture.Visible = true;
            btn_capture_cancel.Visible = false;
            txt_id.Enabled = true;
            btn_close.Enabled = true;
            pic_fingerprint.Image = null;
            lbl_fingerneeded.Visible = false;
            Enroller.Clear();

            if (Capturer != null)
            {
                try
                {
                    Capturer.StopCapture();
                    if (recordfound)
                    {
                        MakeReport("Record found. You can now register fingerprint.");
                    }
                    else
                    {
                        MakeReport("Please find a record to register fingerprint.");
                    }
                }
                catch
                {
                    MakeReport("Can't terminate capture");
                }
            }
        }

        // Button Close
        private void btn_close_Click(object sender, EventArgs e)
        {
            Capturer.StopCapture();

            Capture capture = new Capture();
            this.Hide();
            capture.Show();
        }

        // MakeReport
        private void MakeReport(string message)
        {
            this.Invoke(new Action(delegate ()
            {
                lbl_status.Text = message;
            }));
        }

        // SetStatus
        protected void SetStatus(string status)
        {
            this.Invoke(new Action(delegate ()
            {
                lbl_fingerneeded.Text = status;
            }));
        }

        private void FingerNeededUpdate()
        {
            SetStatus(String.Format("Finger sample needed: {0}", Enroller.FeaturesNeeded));
        }

        public void label_visible_false()
        {
            lbl_fname.Visible = false;
            lbl_mname.Visible = false;
            lbl_lname.Visible = false;
            lbl_birthday.Visible = false;

            lbl_fname_insert.Visible = false;
            lbl_mname_insert.Visible = false;
            lbl_lname_insert.Visible = false;
            lbl_birthday_insert.Visible = false;

            pic_profile.Visible = false;
        }

        public void label_visible_true()
        {
            lbl_fname.Visible = true;
            lbl_mname.Visible = true;
            lbl_lname.Visible = true;
            lbl_birthday.Visible = true;

            lbl_fname_insert.Visible = true;
            lbl_mname_insert.Visible = true;
            lbl_lname_insert.Visible = true;
            lbl_birthday_insert.Visible = true;

            pic_profile.Visible = true;
        }

        //Process
        protected virtual void Process(DPFP.Sample Sample)
        {
            DrawPicture(ConvertSampleBitmap(Sample));

            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Enrollment);

            if (features != null)
            {
                try
                {
                    Enroller.AddFeatures(features);
                }
                catch
                {
                    MakeReport("The fingerprint you register is poor. Please try again.");
                }
                finally
                {
                    FingerNeededUpdate();

                    switch (Enroller.TemplateStatus)
                    {

                        case DPFP.Processing.Enrollment.Status.Ready:
                            {
                                Ontemplate(Enroller.Template);

                                MemoryStream fingerprintData = new MemoryStream();
                                Enroller.Template.Serialize(fingerprintData);
                                fingerprintData.Position = 0;
                                BinaryReader br = new BinaryReader(fingerprintData);

                                byte[] bytes = br.ReadBytes((Int32)fingerprintData.Length);

                                try
                                {
                                    mysqlConnect = new MySqlConnection(connectionstring);
                                    mysqlConnect.Open();

                                    command = "UPDATE employees SET fingerprint = @finger WHERE id = @employeeid";

                                    mysqlCommand = new MySqlCommand(command, mysqlConnect);

                                    mysqlCommand.Parameters.AddWithValue("@finger", bytes).DbType = DbType.Binary;
                                    mysqlCommand.Parameters.AddWithValue("@employeeid", employeeId);

                                    mysqlReader = mysqlCommand.ExecuteReader();

                                    mysqlReader.Close();

                                    MessageBox.Show("Fingerprint is registered.", "Fingerprint Registration");
                                    finish();

                                }
                                catch (Exception e)
                                {
                                    MessageBox.Show("Error: " + e.Message, "Finger Registration");
                                }
                                break;
                            }

                        case DPFP.Processing.Enrollment.Status.Failed:
                            {
                                Enroller.Clear();
                                Capturer.StopCapture();
                                FingerNeededUpdate();
                                Ontemplate(null);
                                Capturer.StartCapture();

                                break;
                            }

                    }
                }
            }
        }

        private Bitmap ConvertSampleBitmap(DPFP.Sample Sample)
        {
            Bitmap bitmap = null;

            DPFP.Capture.SampleConversion Convertor = new DPFP.Capture.SampleConversion();
            Convertor.ConvertToPicture(Sample, ref bitmap);

            return bitmap;
        }

        private void DrawPicture(Bitmap bitmap)
        {
            pic_fingerprint.Image = new Bitmap(bitmap, pic_fingerprint.Size);
        }

        private void Ontemplate(DPFP.Template template) 
        {
            this.Invoke(new Action(delegate ()
            {
                Template = template;
            }));
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











        public void OnComplete(object Capture, string ReaderSerialNumber, Sample Sample)
        {
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
