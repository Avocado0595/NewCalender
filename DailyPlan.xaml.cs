using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MyCalender
{
    /// <summary>
    /// Interaction logic for DailyPlan.xaml
    /// </summary>
    public partial class DailyPlan : Window
    {
        private DateTime date;
        private PlanData job;
        public DateTime Date { get => date; set => date = value; }
        public PlanData Job { get => job; set => job = value; }
        public List<Ajob> listJob;

        public DailyPlan(DateTime date, PlanData job)
        {
            InitializeComponent();
            this.Date = date;
            this.Job = job;
            dtpSelectionDate.SelectedDate = Date;
            cbxFilter.ItemsSource = PlanItem.listStatus;
            
        }
        #region method
        //show all job at selected date
        private void getStplToList()
        {
            if (spnlPlans.Children.Count == 0)
                return;
            listJob = new List<Ajob>();
            foreach (Ajob i in spnlPlans.Children)
            {
                listJob.Add(i);
            }
        }
        private void getListToStpn()
        {
            if (listJob.Count == 0)
                return;
            spnlPlans.Children.Clear();
            foreach (Ajob i in listJob)
            {
                spnlPlans.Children.Add(i);
            }
        }

        private void ShowJobByDate(DateTime date)
        {
            spnlPlans.Children.Clear();
            if (Job != null && Job.Job != null)
            {
                for (int i = 0; i < Job.Job.Count; i++)
                {
                    if (Job.Job[i].Date.Date == date.Date)
                    {
                        Addjob(Job.Job[i]);
                    }
                }
            }
            countDoneJob();
            getStplToList();
        }
        //Count Done Job and add to textbox progress
        void countDoneJob()
        {
            MyConst.doneJob = 0;
            for (int i=0; i<spnlPlans.Children.Count; i++)
            {
                if (((Ajob)spnlPlans.Children[i]).Job.Status == EPlanItem.DONE.ToString())
                    MyConst.doneJob++;
            }
            int percentProcess =0;
            if (spnlPlans.Children.Count !=0)
                   percentProcess = MyConst.doneJob * 100 / spnlPlans.Children.Count;
            tbxProgress.Text = String.Format("{0}%",percentProcess);
            prbProgress.Maximum = spnlPlans.Children.Count;
            prbProgress.Value = MyConst.doneJob;
        }
        //add a job to stackpanel
        void Addjob(PlanItem job)
        {
            Ajob ajob = new Ajob(job);
            ajob.Edit += Ajob_Edit;
            ajob.Delete += Ajob_Delete;
            spnlPlans.Children.Add(ajob);
        }
        #endregion

        #region event
        //delete a job 
        private void Ajob_Delete(object sender, EventArgs e)
        {
            Ajob uc = sender as Ajob;
            PlanItem job = uc.Job;
            
            spnlPlans.Children.Remove(uc);
            Job.Job.Remove(job);
            countDoneJob();
        }
        //edit a job event
        private void Ajob_Edit(object sender, EventArgs e)
        {
            countDoneJob();
        }

        //add a job
        private void MenuItemAddJob_Click(object sender, RoutedEventArgs e)
        {

            PlanItem item = new PlanItem() { Date = dtpSelectionDate.SelectedDate.Value, FromTime = new Point(0, 0), ToTime = new Point(0, 0), Job = "", Status = "COMING" };
            Job.Job.Add(item);
            Addjob(item);
            countDoneJob();
        }

        //change date
        private void dtpSelectionDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowJobByDate((sender as DatePicker).SelectedDate.Value);
        }
        //go back prev day
        private void btnPrevday_Click(object sender, RoutedEventArgs e)
        {
            dtpSelectionDate.SelectedDate = dtpSelectionDate.SelectedDate.Value.AddDays(-1);
        }
        //go to next day
        private void btnNextday_Click(object sender, RoutedEventArgs e)
        {
            dtpSelectionDate.SelectedDate = dtpSelectionDate.SelectedDate.Value.AddDays(1);
        }
        //go to today
        private void MenuItemToday_Click(object sender, RoutedEventArgs e)
        {
            dtpSelectionDate.SelectedDate = DateTime.Now;
        }
        #endregion

        private void fromTimeAcs(object sender, RoutedEventArgs e)
        {
            listJob.Sort(new AjobFromTimeCompareAcs());
            getListToStpn();
        }

        private void fromTimeDecs(object sender, RoutedEventArgs e)
        {
            listJob.Sort(new AjobFromTimeCompareDecs());
            getListToStpn();
        }
        private void toTimeDecs(object sender, RoutedEventArgs e)
        {
            listJob.Sort(new AjobToTimeCompareDecs());
            getListToStpn();
        }
        private void toTimeAcs(object sender, RoutedEventArgs e)
        {
            listJob.Sort(new AjobToTimeCompareAcs());
            getListToStpn();
        }

        private void cbxFilter_Selected(object sender, RoutedEventArgs e)
        {
            if (cbxFilter.SelectedItem == null)
                return;
            string selection = cbxFilter.SelectedItem.ToString();
            for(int i=0; i<spnlPlans.Children.Count; i++)
            {
                if (((Ajob)spnlPlans.Children[i]).Job.Status != selection)
                    ((Ajob)spnlPlans.Children[i]).Visibility = Visibility.Collapsed;
                else
                    ((Ajob)spnlPlans.Children[i]).Visibility = Visibility.Visible;
            }
           // getListToStpn();
        }

        private void ckxFilter_Click(object sender, RoutedEventArgs e)
        {
            if (ckxFilter.IsChecked == false)
                for (int i = 0; i < spnlPlans.Children.Count; i++)
                {
                     ((Ajob)spnlPlans.Children[i]).Visibility = Visibility.Visible;
                }
            else
            {
                if (cbxFilter.SelectedItem == null)
                    return;
                string selection = cbxFilter.SelectedItem.ToString();
                for (int i = 0; i < spnlPlans.Children.Count; i++)
                {
                    if (((Ajob)spnlPlans.Children[i]).Job.Status != selection)
                        ((Ajob)spnlPlans.Children[i]).Visibility = Visibility.Collapsed;
                    else
                        ((Ajob)spnlPlans.Children[i]).Visibility = Visibility.Visible;
                }

            }

        }
    }
}
