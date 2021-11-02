using System;
using System.IO;
using System.Collections.Generic;
namespace Homework_Program
{
    class Program
    {
        struct homework
        {
            public string subject;
            public string description;
            public DateTime dueDate;
        }
        static void Main(string[] args)
        {
            List<homework> hmwk = new List<homework>();
            gethomework(ref hmwk);
            userselection(ref hmwk);
            
        }

        static void gethomework(ref List<homework> hmwk)
        {
            StreamReader work = new StreamReader("homework.txt");
            while (!work.EndOfStream)
            {
                
                string[] homework = work.ReadLine().Split(',');
                homework thishomework;
                thishomework.subject = homework[0];
                thishomework.description = homework[1];
                thishomework.dueDate = DateTime.Parse(homework[2]);

                hmwk.Add(thishomework);
                
               
                
            }
            work.Close();
        }

        static void userselection(ref List<homework> hmwk)
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
                        viewhmwk(hmwk);
                        userselection(ref hmwk);
                        break;
                    case "2":
                        completehmwk(ref hmwk);
                        break;
                    case "3":
                        addhmwk(ref hmwk);
                        break;
                    case "4":
                        outfile(hmwk);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Please enter a valid choice.");
                        choice = "";
                        break;
                }
            }
        }
        static void viewhmwk(List<homework> hmwk)
        {
            Console.Clear();
            Console.WriteLine("=================================");
            for (int i = 0; i < hmwk.Count; i++)
            {
                TimeSpan daysleft = hmwk[i].dueDate - DateTime.Today;
                if (daysleft.Days <= 3)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.WriteLine("Homework {0}:", i);
                Console.WriteLine(hmwk[i].subject);
                Console.WriteLine(hmwk[i].description);
                Console.WriteLine(hmwk[i].dueDate.ToString("dd/MM/yyyy")+ "\n");
                Console.ResetColor();
                Console.WriteLine("=================================");

            }
            
        }

        static void completehmwk(ref List<homework> hmwk)
        {
            viewhmwk(hmwk);
            Console.Write("What homework number would you like to complete: ");
            int choice = int.Parse(Console.ReadLine());
            hmwk.RemoveAt(choice);
            Console.Clear();
            userselection(ref hmwk);

        }

        static void addhmwk(ref List<homework> hmwk)
        {
            if(hmwk.Count >= 20)
            {
                Console.WriteLine("The max limit of homework has been added.\nPlease complete some homework before adding more.");
                userselection(ref hmwk);
            }
            
            
            homework thisHomework;
            Console.Write("Enter the subject of the homework: ");
            thisHomework.subject = Console.ReadLine();
            Console.Write("Enter a description of what the homework is about: ");
            thisHomework.description = Console.ReadLine();
            DateTime date = DateTime.Today; 
            validateDate(ref date);
            thisHomework.dueDate = date;
            hmwk.Add(thisHomework);
            userselection(ref hmwk);
            


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

        static void outfile(List<homework> hmwk)
        {
            StreamWriter writefile = new StreamWriter("homework.txt"); 
            for (int i = 0; i < hmwk.Count; i++)
            {
                writefile.WriteLine("{0},{1},{2}", hmwk[i].subject, hmwk[i].description, hmwk[i].dueDate.ToString("dd/MM/yyyy"));

            }
            writefile.Close();
        }
    }
}
