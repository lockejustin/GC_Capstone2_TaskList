using System;
using System.Collections.Generic;
using System.Text;

namespace GC_Capstone2_TaskList
{
    class Task
    {
        #region fields
        private string _assignedName;
        private DateTime _dueDate;
        private string _description;
        private bool _complete;
        #endregion

        #region properties
        public string AssignedName
        {
            get { return _assignedName; }
            set { _assignedName = value; }
        }
        public DateTime DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public bool Complete
        {
            get { return _complete; }
            set { _complete = value; }
        }
        #endregion

        #region Constructors
        public Task() { }

        public Task(string AssignedName, DateTime DueDate, string Description, bool Complete = false)
        {
            _assignedName = AssignedName;
            _dueDate = DueDate;
            _description = Description;
            _complete = Complete;
        }
        #endregion

        #region Methods
        public void PrintTask()
        {
            string completeYN = "";
            if (Complete == true)
            {
                completeYN = "Yes";
            }
            else
            {
                completeYN = "No";
            }
            Console.WriteLine($"\t{AssignedName}\t{DueDate.ToString("d")}\t{completeYN}\t\t{Description}");
        }
        #endregion
    }
}
