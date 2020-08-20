using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MyCalender
{
    /// <summary>
    /// Interaction logic for Ajob.xaml
    /// </summary>
    public partial class Ajob : UserControl
    {

        private PlanItem job;
        public Ajob(PlanItem job)
        {
            InitializeComponent();
            this.Job = job;
            cbxStatus.ItemsSource = PlanItem.listStatus;
            showData();
        }

        public PlanItem Job { get => job; set => job = value; }

        private event EventHandler edit;
        public event EventHandler Edit
        {
            add { edit += value; }
            remove { edit -= value; }
        }

        private event EventHandler delete;
        public event EventHandler Delete
        {
            add { delete += value; }
            remove { delete -= value; }
        }


        void showData()
        {
            tbxJob.Text = Job.Job;
            tpkStart.SelectedTime = new DateTime(1,1,1,(int)Job.FromTime.X,(int)Job.FromTime.Y,0);
            tpkEnd.SelectedTime = new DateTime(1, 1, 1,(int) Job.ToTime.X, (int)Job.ToTime.Y, 0);
            cbxStatus.SelectedIndex = PlanItem.listStatus.IndexOf(Job.Status);
            ckbDone.IsChecked = PlanItem.listStatus.IndexOf(Job.Status) == (int)EPlanItem.DONE;
        }

        private void btnEdit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Job.Job = tbxJob.Text;
            Job.FromTime = new Point(tpkStart.SelectedTime.Value.Hour, tpkStart.SelectedTime.Value.Minute);
            Job.ToTime = new Point(tpkEnd.SelectedTime.Value.Hour, tpkEnd.SelectedTime.Value.Minute);
            Job.Status = cbxStatus.SelectedItem == null ? "DOING": cbxStatus.SelectedItem.ToString();

            if (edit != null)
                edit(this, new EventArgs());
        }

        private void btnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (delete != null)
                delete(this, new EventArgs());
        }

        private void ckbDone_Click(object sender, RoutedEventArgs e)
        {
            cbxStatus.SelectedIndex = (bool)ckbDone.IsChecked ? (int)EPlanItem.DONE : (int)EPlanItem.DOING;
        }

        private void cbxStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ckbDone.IsChecked = cbxStatus.SelectedIndex == (int)EPlanItem.DONE;
        }

    }
    public class AjobFromTimeCompareAcs : IComparer<Ajob>
    {
        public int Compare(Ajob x, Ajob y)
        {
            DateTime xTime = new DateTime(1, 1, 1, (int)x.Job.FromTime.X, (int)x.Job.FromTime.Y, 0);
            DateTime yTime = new DateTime(1, 1, 1, (int)y.Job.FromTime.X, (int)y.Job.FromTime.Y, 0);
            return xTime.CompareTo(yTime);
        }
    }
    public class AjobFromTimeCompareDecs : IComparer<Ajob>
    {
        public int Compare(Ajob x, Ajob y)
        {
            DateTime xTime = new DateTime(1, 1, 1, (int)x.Job.FromTime.X, (int)x.Job.FromTime.Y, 0);
            DateTime yTime = new DateTime(1, 1, 1, (int)y.Job.FromTime.X, (int)y.Job.FromTime.Y, 0);
            return yTime.CompareTo(xTime);
        }
    }
    public class AjobToTimeCompareAcs : IComparer<Ajob>
    {
        public int Compare(Ajob x, Ajob y)
        {
            DateTime xTime = new DateTime(1, 1, 1, (int)x.Job.ToTime.X, (int)x.Job.ToTime.Y, 0);
            DateTime yTime = new DateTime(1, 1, 1, (int)y.Job.ToTime.X, (int)y.Job.ToTime.Y, 0);
            return xTime.CompareTo(yTime);
        }
    }
    public class AjobToTimeCompareDecs : IComparer<Ajob>
    {
        public int Compare(Ajob x, Ajob y)
        {
            DateTime xTime = new DateTime(1, 1, 1, (int)x.Job.ToTime.X, (int)x.Job.ToTime.Y, 0);
            DateTime yTime = new DateTime(1, 1, 1, (int)y.Job.ToTime.X, (int)y.Job.ToTime.Y, 0);
            return yTime.CompareTo(xTime);
        }
    }
}
