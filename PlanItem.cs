using System;
using System.Collections.Generic;
using System.Windows;
using System.Xml;

namespace MyCalender
{
    public class PlanItem
    {
        private string job;
        private DateTime date;
        public string Job { get => job; set => job = value; }
        public Point FromTime { get => fromTime; set => fromTime = value; }
        public Point ToTime { get => toTime; set => toTime = value; }
        public string Status { get => status; set => status = value; }
        public DateTime Date { get => date; set => date = value; }

        private Point fromTime;
        private Point toTime;
        private string status;

        public static List<string> listStatus = new List<string>() { "COMING", "DONE", "DOING",  "MISSED" };


    }



    public enum EPlanItem
    {
        COMING,DONE, DOING , MISSED
    }
}
