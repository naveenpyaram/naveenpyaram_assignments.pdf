using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Assignment1
{
    public class Tasks 
    {
        public string title;
        public string description;
        public Tasks() { }
        public Tasks(string title, string description) 
        {
            this.title = title;
            this.description = description;
        }
        public override string ToString()
        {
            return $"title : {title}, description : {description}";
        }

    }
    public class Program 
    {
       static  List<Tasks> values = new List<Tasks>();
        static void Main(string[] args)
        {
            Console.WriteLine("1. Addtask ");
            Console.WriteLine("2. Updatetask ");
            Console.WriteLine("3.Deletetask ");
            Console.WriteLine("4. Readtask ");
            Console.WriteLine("5. Exit");
            bool exit = true;
            while (exit)
            {
                Console.WriteLine("Choose an option: ");
                int choice = Convert.ToInt32(Console.ReadLine());
              
                switch (choice)
                {
                    case 1:
                        create();
                        break;
                    case 2:
                        update();
                        break;
                    case 3:
                        delete();
                        break;
                    case 4:
                        read();
                        break;
                    case 5:
                        exit = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input, please try again");
                        break;
                }
            }
            Console.WriteLine("Exititng the application");
            Console.ReadLine();
        }
        public static void create()
        {
            Console.WriteLine("enter title :");
            string Title = Console.ReadLine();
            Console.WriteLine("enter description :");
            string Description = Console.ReadLine();
            Tasks t1 = new Tasks(Title, Description);
            values.Add(t1);
            Console.WriteLine("task and desciption added succesfully");
        }
        public static void update()
        {
            Console.WriteLine("Enter the index of task to be updated");
            int index = Convert.ToInt32(Console.ReadLine());

            if (index <= 0 && index >= values.Count)
            {
                Console.WriteLine("Invalid index");
                return;
            }
            else
            {
                Console.WriteLine("Enter new title");
                string newTitle = Console.ReadLine();
                Console.WriteLine("Enter new description");
                string newDescription = Console.ReadLine();
                values[index].title = newTitle;
                values[index].description = newDescription;
                Console.WriteLine("task and description updated succesfully");
            }
        }
        public static void delete()
        {
            Console.WriteLine("Enter the index of task to be deleted");
            int index = Convert.ToInt32(Console.ReadLine());

            if (index <= 0 && index >= values.Count)
            {
                Console.WriteLine("Invalid index");
                return;
            }
            else
            {
                values.RemoveAt(index);
                Console.WriteLine("task and description deleted succesfully");
            }
           
        }
        public static void read()
        {
            if(values.Count == 0)
            {
                Console.WriteLine("This list is empty");
            }
            else
            {
                Console.WriteLine("task and description in the list");
                for(int i = 0; i < values.Count; i++)
                {
                    Console.WriteLine($"{i}.{values[i]}");
                }
            }
        }
  
    }
}
