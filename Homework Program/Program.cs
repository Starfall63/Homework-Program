using System;
using System.IO;
namespace Homework_Program
{
    class Program
    {
        struct homework
        {
            public string subject;
            public string description;
            public DateTime dueDate;
            public bool complete;
            //Structure for homework
        }
        static void Main(string[] args)
        {
            homework[] hmwk = new homework[20];//Declares array for homework to be stored
            int homeworkcount = 0;//Counter for how much homework is stored on the file
            gethomework(ref hmwk, ref homeworkcount);//Retrieves homework from homework file and stores into the hmwk array
            userselection(ref hmwk, ref homeworkcount);//Goes to user selection where user decides what they want to do
            
        }

        static void gethomework(ref homework[] hmwk, ref int homeworkcount)
        {
            StreamReader work = new StreamReader("homework.txt");
            while (!work.EndOfStream)//Reads the homework from file and adds to array
            {
                
                string[] homework = work.ReadLine().Split(',');
                homework thishomework;
                thishomework.subject = homework[0];
                thishomework.description = homework[1];
                thishomework.dueDate = DateTime.Parse(homework[2]);
                thishomework.complete = bool.Parse(homework[3]);
                hmwk[homeworkcount] = thishomework;
                homeworkcount++;//Counter increments everytime a homework is added to the array


            }
            work.Close();
            Console.WriteLine("{0} homework(s) loaded.", homeworkcount);
            for (int i = 0; i < homeworkcount; i++)//Checks how many homeworks are overdue and alerts the user
            {
                if(DateTime.Today > hmwk[i].dueDate)
                {
                    int overdue = 0;
                    overdue++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("There are {0} homework(s) overdue.", overdue);
                }
            }
            Console.ResetColor();
            Console.WriteLine("Press any key to Continue.");
            Console.ReadKey();
            Console.Clear();
        }

        static void userselection(ref homework[] hmwk, ref int homeworkcount)
        {
            //User selects what they want to do
            string choice = "";
            while (choice == "")
            {
                Console.WriteLine("What would you like to do?\n" +
                    "1: View All Homework\n" +
                    "2: Complete Homework\n" +
                    "3: Add Homework\n" +
                    "4: Quit");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        viewallhmwk(hmwk, homeworkcount);
                        userselection(ref hmwk, ref homeworkcount);
                        break;
                    case "2":
                        completehmwk(ref hmwk, ref homeworkcount);
                        break;
                    case "3":
                        addhmwk(ref hmwk, ref homeworkcount);
                        break;
                    case "4":
                        outfile(hmwk, homeworkcount);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Please enter a valid choice.");
                        choice = "";
                        break;
                }
            }
        }
        static void viewallhmwk(homework[] hmwk, int homeworkcount)
        {
            Console.Clear();
            Console.WriteLine("=================================");
            for (int i = 0; i < homeworkcount; i++)
            {
                TimeSpan daysleft = hmwk[i].dueDate - DateTime.Today;
                if (daysleft.Days <= 3)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                if (hmwk[i].complete == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine("Homework {0}:", i);
                Console.WriteLine(hmwk[i].subject);
                Console.WriteLine(hmwk[i].description);
                Console.WriteLine(hmwk[i].dueDate.ToString("dd/MM/yyyy")+ "\n");
                Console.ResetColor();
                Console.WriteLine("=================================");

            }
            //Displays all homework. It will mark completed homeworks as green and homeworks that are overdue or less than 3 days away as red
        }
        
        static void hmwktocomplete(homework[] hmwk, int homeworkcount)
        {
            Console.Clear();
            Console.WriteLine("=================================");
            for (int i = 0; i < homeworkcount; i++)
            {
                if(hmwk[i].complete == false)
                {
                    TimeSpan daysleft = hmwk[i].dueDate - DateTime.Today;
                    if (daysleft.Days <= 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.WriteLine("Homework {0}:", i);
                    Console.WriteLine(hmwk[i].subject);
                    Console.WriteLine(hmwk[i].description);
                    Console.WriteLine(hmwk[i].dueDate.ToString("dd/MM/yyyy") + "\n");
                    Console.ResetColor();
                    Console.WriteLine("=================================");
                }
            }
            //Displays only the homework that needs to be completed, marking those overdue or less than 3 days away from dues date as red

        }

        static void completehmwk(ref homework[] hmwk, ref int homeworkcount)
        {
            hmwktocomplete(hmwk, homeworkcount);
            Console.Write("What homework number would you like to complete: ");
            int choice = int.Parse(Console.ReadLine());
            if (hmwk[choice].complete == true)
            {
                Console.WriteLine("This homework has already been completed.");
                userselection(ref hmwk, ref homeworkcount);
            }
            hmwk[choice].complete = true;
            Console.Clear();
            userselection(ref hmwk, ref homeworkcount);
            //User selects homework that they would like to be marked complete 
        }

        static void addhmwk(ref homework[] hmwk, ref int homeworkcount)
        {
            if(homeworkcount >= 20)
            {
                Console.WriteLine("The max limit of homework has been added.\nPlease complete some homework before adding more.");
                userselection(ref hmwk, ref homeworkcount);
            }
            //If there is already 20 homeworks in the array, it will not let the user add anymore.
            
            //User inputs the information about the homework that they want to add
            Console.Write("Enter the subject of the homework: ");
            hmwk[homeworkcount].subject = Console.ReadLine();
            Console.Write("Enter a description of what the homework is about: ");
            hmwk[homeworkcount].description = Console.ReadLine();
            DateTime date = DateTime.Today; 
            validateDate(ref date);//Makes sure that the due date is valid
            hmwk[homeworkcount].dueDate = date;
            hmwk[homeworkcount].complete = false;
            homeworkcount++;//Increments homeworkcount after the user adds a homework to the array
            userselection(ref hmwk, ref homeworkcount);
            


        }

        static void validateDate(ref DateTime date)
        {
            while(date == DateTime.Today)
            {
                //Validates the date that the user inputs to make sure that the due date is at a sate later than the current day
                Console.Write("Enter the due date (dd/mm/yyyy): ");
                try
                {
                    date = DateTime.Parse(Console.ReadLine());
                    if (date.Date <= DateTime.Today)
                    {
                        Console.WriteLine("Please enter a valid date.");
                        date = DateTime.Today;
                    }
                }
                catch
                {
                    Console.WriteLine("Please enter a valid date.");
                    date = DateTime.Today;
                }
            }
            
        }

        static void outfile(homework[] hmwk, int homeworkcount)
        {
            StreamWriter writefile = new StreamWriter("homework.txt"); 
            for (int i = 0; i < homeworkcount; i++)
            {
                writefile.WriteLine("{0},{1},{2},{3}", hmwk[i].subject, hmwk[i].description, hmwk[i].dueDate.ToString("dd/MM/yyyy"),hmwk[i].complete.ToString());

            }
            writefile.Close();
            //Overwrites the homework file with the new information that is stored in the array
        }
    }
}
