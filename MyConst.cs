using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyCalender
{

    public class MyConst

    {
        public static int doneJob = 0;
        public static  int weeksofmonth = 6;
        public static int daysofweek = 7;
        public static DateTime today = DateTime.Now;
        public static int notiTime = 5*60;
        public static List<int> InitNotifytime = new List<int>() { 5, 10, 15, 20, 30, 60 };

    public static int dayOfMonth(int month, int year)
        {
            int result = 30;
            switch(month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    result = 31;
                    break;
                case 2:
                    if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
                        result = 29;
                    else
                        result = 28;
                    break;
            }
            return result;
        }
    }
}
