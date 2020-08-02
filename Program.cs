using System;
using System.Collections.Generic;
using System.Linq;

namespace GC_Capstone2_TaskList
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] menuOptions = { "List Tasks", "Add Task", "Edit Existing Task", "Delete Task", "Mark Task Complete", "Quit" };
            Console.WriteLine("Welcome to the Task Manager\n");
            List<Task> taskList = PopulateTaskList();
            bool continueProgram = true;
            int menuChoice = 0;

            while (continueProgram)
            {
                PrintMainMenu(menuOptions);
                menuChoice = MenuChoice(menuOptions);
                
                //calls appropriate method based on user input
                if (menuChoice == 1) //List Tasks
                {
                    ListTasks(taskList);
                }
                else if (menuChoice == 2) //Add Task
                {
                    taskList = AddTask(taskList);
                }
                else if (menuChoice == 3) //Edit Task
                {
                    taskList = EditTask(taskList);
                }
                else if (menuChoice == 4) //Delete Task
                {
                    taskList = DeleteTask(taskList);
                }
                else if (menuChoice == 5) //Complete Task
                {
                    taskList = CompleteTask(taskList);
                }
                else //Quit program
                {
                    Console.WriteLine("Thanks for using the task manager.  Goodbye!");
                    continueProgram = false;
                }
            }
        }

        public static int MenuChoice (string[] menuOptions)
        {
            string userEntry = "";
            bool validEntry = false;
            int menuSelection = 0;

            while (!validEntry)
            {
                Console.Write("What would you like to do? ");

                userEntry = Console.ReadLine();  //receives input from the user

                if (int.TryParse(userEntry, out int n))  //tests to see if input is an int and within range
                {
                    if (n >= 1 && n <= menuOptions.Length)
                    {
                        validEntry = true;
                        menuSelection = n;
                    }
                    else
                    {
                        Console.WriteLine("Your entry was invalid.  Please try again.");
                        validEntry = false;
                    }
                }
                else  //triggers if alpha characters are entered
                {
                    Console.WriteLine("Your entry was invalid.  Please try again.");
                    validEntry = false;
                }
            }

            return menuSelection;

        }
    
        public static void ListTasks(List<Task> taskList)
        {
            Console.Clear();
            Console.WriteLine("List of all tasks");

            Console.WriteLine($"Task\tTeam Member\tDue Date\tComplete?\tDescription");

            for (int i = 0; i < taskList.Count; i++)  //prints out each entry in the list
            {
                Console.Write($"{i + 1}");
                taskList[i].PrintTask();
            }

            Console.WriteLine("\nPlease press any key to return to the main menu");
            Console.ReadKey();
            Console.Clear();
        }

        public static List<Task> AddTask(List<Task> taskList)
        {
            string name = "";
            string description = "";
            string date = "";
            DateTime dueDate = new DateTime();

            Console.Clear();
            Console.WriteLine("New Task Entry");

            //Prompts for name entry and validates it
            Console.WriteLine("Please enter the assigned team member's name");
            name = Console.ReadLine();
            
            while (name.Trim() == "")
            {
                Console.WriteLine("No input detected.  Please enter a name.");
                name = Console.ReadLine();
            }

            //Prompts for a description of the task
            Console.WriteLine("Please enter a description for the task");
            description = Console.ReadLine();

            while (description.Trim() == "")
            {
                Console.WriteLine("No input detected.  Please enter a description.");
                description = Console.ReadLine();
            }

            //Prompts for due date
            Console.WriteLine("Please enter a due date for the task (mm/dd/yyyy)");
            date = Console.ReadLine();

            while (!DateTime.TryParse(date, out DateTime result))
            {
                Console.WriteLine("Invalid date entered.  Please try again.");
                date = Console.ReadLine();

            }

            dueDate = DateTime.Parse(date);

            //adds new task with data entered by user above
            taskList.Add(new Task(name, dueDate, description));

            Console.WriteLine("Task added! Press any key to return to the main menu.");
            Console.ReadKey();
            Console.Clear();

            return taskList;
        }

        public static List<Task> DeleteTask(List<Task> taskList)
        {
            Console.Write("\nWhich task would you like to delete? ");
            string input = ""; 
            int taskNumber = 0;
            bool validEntry = false;
            bool confirmDelete = false;

            while (!validEntry)  //verifies that a valid task number is entered
            {
                input = Console.ReadLine();
                while (!int.TryParse(input, out int result))
                {
                    Console.WriteLine("Please enter a valid task number.");
                    input = Console.ReadLine();
                }
                
                taskNumber = int.Parse(input);

                if (taskNumber > 0 && taskNumber <= taskList.Count)
                {
                    validEntry = true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid number from the list.");
                }
            }

            int index = taskNumber - 1;  //calculates index based on user entry

            
            //prints information related to task that user chose
            Console.WriteLine($"Task\tTeam Member\tDue Date\tComplete?\tDescription");
            Console.Write($"{taskNumber}");
            taskList[index].PrintTask();
            Console.Write($"Are you sure you want to delete the task above? (y/n) ");

            confirmDelete = ConfirmChoice();  //confirms and verifies if user wants to delete the task

            //deletes task if user confirms, otherwise indicates to user that task has not been deleted
            if (confirmDelete)
            {
                taskList.RemoveAt(index);
                Console.WriteLine($"Task number {taskNumber} has been deleted.  Please press any key to return to the main menu.");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine($"Task number {taskNumber} has NOT been deleted.  Please press any key to return to the main menu.");
                Console.ReadKey();
                Console.Clear();
            }

            return taskList;
        }

        public static List<Task> EditTask(List<Task> taskList)
        {
            Console.Write("\nWhich task would you like to edit? ");
            string input = "";
            int taskNumber = 0;
            bool validEntry = false;
            bool confirmEdit = false;

            while (!validEntry)  //verifies a valid choice is entered
            {
                input = Console.ReadLine();
                while (!int.TryParse(input, out int result))
                {
                    Console.WriteLine("Please enter a valid task number.");
                    input = Console.ReadLine();
                }

                taskNumber = int.Parse(input);

                if (taskNumber > 0 && taskNumber <= taskList.Count)
                {
                    validEntry = true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid number from the list.");
                }
            }

            int index = taskNumber - 1;  //calculates index based on user choice above

            //Lists task and asks user to confirm edit
            Console.WriteLine($"Task\tTeam Member\tDue Date\tComplete?\tDescription");
            Console.Write($"{taskNumber}");
            taskList[index].PrintTask();
            Console.Write($"Are you sure you want to edit the task above? (y/n) ");

            confirmEdit = ConfirmChoice();

            if (confirmEdit)
            {
                taskList = EditInput(taskList, index);  //calls edit task method if user chooses to edit

                Console.WriteLine($"Task number {taskNumber} has been edited.  Please press any key to return to the main menu.");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine($"Task number {taskNumber} has NOT been changed.  Please press any key to return to the main menu.");
                Console.ReadKey();
                Console.Clear();
            }

            return taskList;
        }

        public static List<Task> EditInput(List<Task> taskList, int index)
        {
            string name = "";
            string description = "";
            string date = "";
            DateTime dueDate = new DateTime();
            string newCompleteStatus = "";

            //Prompts for name entry and validates it
            Console.WriteLine($"This task is currently assigned to {taskList[index].AssignedName}.  Please enter who you'd like to reassign it to. ");
            name = Console.ReadLine();

            while (name.Trim() == "")
            {
                Console.WriteLine("No input detected.  Please enter a name.");
                name = Console.ReadLine();
            }

            //Prompts for a description of the task
            Console.WriteLine($"This task description is currently {taskList[index].Description}.  Please enter a new description.");
            description = Console.ReadLine();

            while (description.Trim() == "")
            {
                Console.WriteLine("No input detected.  Please enter a description.");
                description = Console.ReadLine();
            }

            //Prompts for due date
            Console.WriteLine($"The current due date is {taskList[index].DueDate}.  Please enter a new due date for the task (mm/dd/yyyy)");
            date = Console.ReadLine();

            while (!DateTime.TryParse(date, out DateTime result))
            {
                Console.WriteLine("Invalid date entered.  Please try again.");
                date = Console.ReadLine();

            }

            string completeYN = "";

            if (taskList[index].Complete == true)
            {
                completeYN = "Yes";
            }
            else
            {
                completeYN = "No";
            }

            //Prompt for completion change
            Console.WriteLine($"The current completion status is {completeYN}.  Please enter a new status, either yes or no.");
            newCompleteStatus = Console.ReadLine().ToLower();

            while (newCompleteStatus != "yes" && newCompleteStatus != "no")
            {
                Console.WriteLine("Invalid status entered.  Please try again.");
                newCompleteStatus = Console.ReadLine().ToLower();
            }

            if (newCompleteStatus == "yes")
            {
                taskList[index].Complete = true;
            }
            else
            {
                taskList[index].Complete = false;
            }

            dueDate = DateTime.Parse(date);

            taskList[index].AssignedName = name;
            taskList[index].Description = description;
            taskList[index].DueDate = dueDate;

            return taskList;
        }

        public static bool ConfirmChoice()
        {
            string response = Console.ReadLine().ToLower();

            while (response != "y" && response != "n")  //checks to make sure the user enters either y or n
            {
                Console.Write("Your entry was invalid.  Please respond (y/n) ");
                response = Console.ReadLine().ToLower();
            }

            if (response == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<Task> CompleteTask(List<Task> taskList)
        {
            Console.Write("\nWhich task would you like to mark complete? ");
            string input = "";
            int taskNumber = 0;
            bool validEntry = false;
            bool confirmComplete = false;

            while (!validEntry)  //verifies proper entry of task number
            {
                input = Console.ReadLine();
                while (!int.TryParse(input, out int result))
                {
                    Console.WriteLine("Please enter a valid task number.");
                    input = Console.ReadLine();
                }

                taskNumber = int.Parse(input);

                if (taskNumber > 0 && taskNumber <= taskList.Count)
                {
                    validEntry = true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid number from the list.");
                }
            }

            int index = taskNumber - 1;

            Console.WriteLine($"Task\tTeam Member\tDue Date\tComplete?\tDescription");
            Console.Write($"{taskNumber}");
            taskList[index].PrintTask();
            Console.Write($"Are you sure you want to mark the task above as complete? (y/n) ");

            confirmComplete = ConfirmChoice();

            if (confirmComplete)  //changes  completion status as necessary based on user input
            {
                taskList[index].Complete = true;
                Console.WriteLine($"Task number {taskNumber} has been marked complete.  Please press any key to return to the main menu.");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine($"Task number {taskNumber} has NOT been changed.  Please press any key to return to the main menu.");
                Console.ReadKey();
                Console.Clear();
            }

            return taskList;
        }

        public static void PrintMainMenu(string[] menuOptions)
        {
            for (int i = 0; i < menuOptions.Length; i++)
            {
                Console.WriteLine($"{i+1}. {menuOptions[i]}");
            }
        }

        public static List<Task> PopulateTaskList()
        {
            List<Task> taskList = new List<Task>() { };
            taskList.Add(new Task("Steve Rogers", new DateTime(2020, 11, 20), "Polish shield", false));
            taskList.Add(new Task("Tony Stark", new DateTime(2020, 8, 1), "Go to sleep", false));
            taskList.Add(new Task("Natasha Romanov", new DateTime(2020, 8, 14), "Learn new language", false));
            taskList.Add(new Task("Steve Rogers", new DateTime(2021, 01, 01), "Catch up on recent history", false));

            return taskList;
        }
    }
}
