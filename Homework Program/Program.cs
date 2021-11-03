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
        }
        static void Main(string[] args)
        {
            homework[] hmwk = new homework[20];
            int homeworkcount = 0;
            gethomework(ref hmwk, ref homeworkcount);
            userselection(ref hmwk, ref homeworkcount);
            
        }

        static void gethomework(ref homework[] hmwk, ref int homeworkcount)
        {
            StreamReader work = new StreamReader("homework.txt");
            while (!work.EndOfStream)
            {
                
                string[] homework = work.ReadLine().Split(',');
                homework thishomework;
                thishomework.subject = homework[0];
                thishomework.description = homework[1];
                thishomework.dueDate = DateTime.Parse(homework[2]);
                thishomework.complete = bool.Parse(homework[3]);
                hmwk[homeworkcount] = thishomework;
                homeworkcount++;


            }
            work.Close();
            Console.WriteLine("{0} homework(s) loaded.\nPress any key to Continue.", homeworkcount);
            Console.ReadKey();
            Console.Clear();
        }

        static void userselection(ref homework[] hmwk, ref int homeworkcount)
        {
            
            string choice = "";
            while (choice == "")
            {
                Console.WriteLine("What would you like to do?\n" +
                    "1: View Homework\n" +
                    "2: Complete Homework\n" +
                    "3: Add Homework\n" +
                    "4: Quit");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        viewhmwk(hmwk, homeworkcount);
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
        static void viewhmwk(homework[] hmwk, int homeworkcount)
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
            
        }

        static void completehmwk(ref homework[] hmwk, ref int homeworkcount)
        {
            viewhmwk(hmwk, homeworkcount);
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

        }

        static void addhmwk(ref homework[] hmwk, ref int homeworkcount)
        {
            if(homeworkcount >= 20)
            {
                Console.WriteLine("The max limit of homework has been added.\nPlease complete some homework before adding more.");
                userselection(ref hmwk, ref homeworkcount);
            }

            
            
            Console.Write("Enter the subject of the homework: ");
            hmwk[homeworkcount].subject = Console.ReadLine();
            Console.Write("Enter a description of what the homework is about: ");
            hmwk[homeworkcount].description = Console.ReadLine();
            DateTime date = DateTime.Today; 
            validateDate(ref date);
            hmwk[homeworkcount].dueDate = date;
            hmwk[homeworkcount].complete = false;
            homeworkcount++;
            userselection(ref hmwk, ref homeworkcount);
            


        }

        static void validateDate(ref DateTime date)
        {
            while(date == DateTime.Today)
            {
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
        }
    }
}
