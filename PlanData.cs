using System;
using System.Collections.Generic;


namespace MyCalender
{
    [Serializable]
    public class PlanData
    {
        private bool notify;
        private List<PlanItem> job;

        public List<PlanItem> Job { get => job; set => job = value; }
        public bool Notify { get => notify; set => notify = value; }
    }
}
