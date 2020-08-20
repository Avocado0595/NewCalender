using MaterialDesignThemes.Wpf;
using System;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml.Serialization;
using Button = System.Windows.Controls.Button;
using System.Drawing;
using Brushes = System.Windows.Media.Brushes;
using Point = System.Windows.Point;
using Application = System.Windows.Application;
using Microsoft.Win32;

namespace MyCalender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        public List<Grid> gridDays { get; set; }
        public PlanData Job { get => job; set => job = value; }

        private PlanData job;
        private string filePath = "data.xml";
        NotifyIcon notify;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public MainWindow()
        {
                       
            InitializeComponent();
            DefaultDate();
            gridDays = new List<Grid>() { gridSun, gridMon, gridTue, gridWed, gridThu, gridFri, gridSat };
            LoadDays(dtpChosenDate.SelectedDate.Value.Month, dtpChosenDate.SelectedDate.Value.Year);
            dtpChosenDate.SelectedDateChanged += DtpChosenDate_SelectedDateChanged;
            cbxTime.ItemsSource = MyConst.InitNotifytime;
            try
            {
                Job = DeserializeFromXML(filePath) as PlanData;
            }
            catch
            {
                DefaultJob();
            }
            /*  RegistryKey regkey = Registry.CurrentUser.CreateSubKey("Software\\LapLich");
              //mo registry khoi dong cung win
              RegistryKey regstart = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
              string keyvalue = "1";
              //string subkey = "Software\\ManhQuyen";
              try
              {
                  //chen gia tri key
                  regkey.SetValue("Index", keyvalue);
                  //regstart.SetValue("taoregistrytronghethong", "E:\\Studing\\Bai Tap\\CSharp\\Channel 4\\bai temp\\tao registry trong he thong\\tao registry trong he thong\\bin\\Debug\\tao registry trong he thong.exe");
                  regstart.SetValue("LapLich", Environment.CurrentDirectory + "\\MyCalender.exe");
                  ////dong tien trinh ghi key
                  //regkey.Close();
              }
              catch (System.Exception ex)
              {
              }*/

            //set notifyIcon at taskbar
            notify = new NotifyIcon();
            notify.Icon = new System.Drawing.Icon(Environment.CurrentDirectory + @"\calender.ico");
            //set time to show balloonTip
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, MyConst.notiTime);
            notify.BalloonTipClicked += Notify_Click;
        }


        #region Method
        //set defaultJob for using first time 
        void DefaultJob()
        {
            Job = new PlanData();
            Job.Job = new List<PlanItem>();
            //Job.Job.Add(new PlanItem() { Date = new DateTime(2020,07,25), Job = "Release Day...", FromTime = new Point(12, 0), ToTime = new Point(12, 0), Status = PlanItem.listStatus[(int)EPlanItem.DONE] });
            // Job.Job.Add(new PlanItem() { Date = DateTime.Now, Job = "Testing", FromTime = new Point(18, 22), ToTime = new Point(20, 10), Status = PlanItem.listStatus[(int)EPlanItem.DONE] });
        }

        void DefaultDate()
        {
            dtpChosenDate.SelectedDate = DateTime.Today;
        }
     
        void LoadDays(int month, int year)
        {
            clearBtn();
           
            int week = 1;
            for (int i = 1; i <= MyConst.dayOfMonth( month, year); i++)
            {
                DateTime currentDate;
                currentDate = new DateTime(year, month, i);
                Button btn = new Button
                {
                    Height = double.NaN,
                    Padding = new Thickness(0),
                    Margin = new Thickness(2, 2, 1.365, 2.096),
                    Background = Brushes.AliceBlue,
                    Content = i.ToString(),
                    Tag = string.Format("{0:0#}{1:0#}{2:####}", currentDate.Day, currentDate.Month, currentDate.Year)
                };
                btn.Click += Btn_Click;

                var getGrid = (Grid)gridDays[(int)currentDate.DayOfWeek].FindName("gridW"+ week.ToString() + ((int)currentDate.DayOfWeek).ToString());
                getGrid.Children.Clear();
                getGrid.Children.Add(btn);
                if (dtpChosenDate.SelectedDate.Value.Date.Equals(currentDate.Date))
                    btn.Background = Brushes.CadetBlue;
                if (currentDate.Date.Equals(DateTime.Now.Date))
                    btn.Background = Brushes.LightBlue;
                if (currentDate.DayOfWeek == 0)
                    week++;
            }
            
        }

        void clearBtn()
        {            
            for (int i = 0; i < 7 ; i++)
            {
                for (int j=1; j<7; j++  )
                {
                    var getGrid = (Grid)gridDays[i].FindName("gridW" + j.ToString() + i.ToString());
                    getGrid.Children.Clear();
                }
            }
        }

        private void SerializeToXML(object data, string filePath)
        {
            File.WriteAllText(filePath, string.Empty);//clear all content in xml file before override on it
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);          
            XmlSerializer xml = new XmlSerializer(typeof(PlanData));
            xml.Serialize(fs, data);
            fs.Close();           
        }

        private object DeserializeFromXML(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            XmlSerializer xml = new XmlSerializer(typeof(PlanData));
            object result = xml.Deserialize(fs);
            fs.Close();
            return result;
        }

        #endregion

        #region Event
        //move to next month
        private void btnNextMonth_Click(object sender, RoutedEventArgs e)
        {
            dtpChosenDate.SelectedDate = dtpChosenDate.SelectedDate.Value.AddMonths(1);
        }
        //move to prev month
        private void btnPrevMonth_Click(object sender, RoutedEventArgs e)
        {
            dtpChosenDate.SelectedDate = dtpChosenDate.SelectedDate.Value.AddMonths(-1);
        }
        //move to today
        private void btnToDay_Click(object sender, RoutedEventArgs e)
        {
            DefaultDate();
        }
        //event click on days button
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.BorderBrush = Brushes.CadetBlue;
            dtpChosenDate.SelectedDate = new DateTime(int.Parse(btn.Tag.ToString().Substring(4, 4)), int.Parse(btn.Tag.ToString().Substring(2, 2)), int.Parse(btn.Tag.ToString().Substring(0, 2)));
            DailyPlan dailyPlan = new DailyPlan(new DateTime(int.Parse(btn.Tag.ToString().Substring(4, 4)), int.Parse(btn.Tag.ToString().Substring(2, 2)), int.Parse(btn.Tag.ToString().Substring(0, 2))),Job);
            dailyPlan.ShowDialog();
        }

        private void DtpChosenDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker selectDate = (DatePicker)sender;
            LoadDays(selectDate.SelectedDate.Value.Month, selectDate.SelectedDate.Value.Year);
        }
        //Serialize To XML when window closed
        private void Window_Closed(object sender, EventArgs e)
        {
            SerializeToXML(Job, filePath);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (Job == null || Job.Job == null)
                return;
            List<PlanItem> job = Job.Job.Where(p => p.Date.Date == DateTime.Now.Date).ToList();

            notify.Visible = true;
            notify.ShowBalloonTip(10000, "Lịch Công Việc", string.Format($"Bạn có {job.Count} công việc hôm nay"), ToolTipIcon.Info);

        }
        //show ToDay Plan when click to balloon tip
        private void Notify_Click(object sender, EventArgs e)
        {
            DailyPlan dailyPlan = new DailyPlan(DateTime.Now, Job);
            dailyPlan.ShowDialog();
        }

        private void ckbNoti_Checked(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Start();         
        }

        private void ckbNoti_Unchecked(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
        }

        private void cbxTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyConst.notiTime = (int)cbxTime.SelectedItem*60;
            dispatcherTimer.Interval = new TimeSpan(0, 0, MyConst.notiTime);
        }
        #endregion
    }
}
