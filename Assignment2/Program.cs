using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assignment_2
{
    public class Item
    {
        public int Id;
        public string Name;
        public double Price;
        public int Quantity;
        public Item(int Id, string Name, double Price, int Quantity)
        {
            this.Id = Id;
            this.Name = Name;
            this.Price = Price;
            this.Quantity = Quantity;
        }
        public override string ToString()
        {
            return $"Id : {Id}, name: {Name}, price : {Price}, quantity : {Quantity}";
        }
    }
    public class Program
    {
        public static List<Item> products = new List<Item> { };
        static void Main(string[] args)
        {
            Console.WriteLine("1. Add Item");
            Console.WriteLine("2. Update Item");
            Console.WriteLine("3. Delete Item");
            Console.WriteLine("4. Display Items");
            Console.WriteLine("5. Find items");
            Console.WriteLine("6. Exit");
            Boolean flag = true;
            while (flag) 
            {
                Console.WriteLine("Select an option from 1 - 6 :");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        addItem();
                        break;
                    case 2:
                        updateItem();
                        break;
                    case 3:
                        deleteItem();
                        break;
                    case 4:
                        displayItem();
                        break;
                    case 5:
                        findItem();
                        break;
                    case 6:
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Invalid, Enter the choice within the range only :");
                        break;
                }
            }
            Console.WriteLine("Exting the application");
            Console.ReadLine();
        }
      public static void addItem() 
        {
            Console.WriteLine("Add items into the list :");
            Console.WriteLine("Enter the item id from 1 to 999");
            int Id = Convert.ToInt32(Console.ReadLine());
            if(Id > 0 && Id <= 999)
            {
                Console.WriteLine("Enter the item name");
                string Name = Console.ReadLine();
                Console.WriteLine("Enter the item price");
                double Price = double.Parse(Console.ReadLine());
                Console.WriteLine("Enter the item quantity");
                int Quantity = Convert.ToInt32(Console.ReadLine());
                Item item1 = new Item(Id, Name, Price, Quantity);
                products.Add(item1);
                Console.WriteLine("Product details added succesfully");
            }
            else
            {
                Console.WriteLine("Enter the valid id from 1 - 999");
            }
           
        }
      public  static void updateItem()
        {
            if(products.Count == 0)
            {
                Console.WriteLine("The List is Empty, First add the items into the List");
            }
            else
            {
                Console.WriteLine("Enter item index to update");
                int index = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the new item id");
                int newId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the new item name");
                string newName = Console.ReadLine();
                Console.WriteLine("Enter the item newprice");
                double newPrice = double.Parse(Console.ReadLine());
                Console.WriteLine("Enter the item newquantity");
                int newQuantity = Convert.ToInt32(Console.ReadLine());
                products[index].Id = newId;
                products[index].Name = newName;
                products[index].Price = newPrice;
                products[index].Quantity = newQuantity;
                Console.WriteLine("new product details added succesfully");
            }
        }
      public static void deleteItem()
        {
            if(products.Count == 0)
            {
                Console.WriteLine("The List is Emplty, First add the items into the List");
            }
            else
            {
                Console.WriteLine("You can delete an item by index number or id number");
                Console.WriteLine("If u want to delete an item by its index choose : 0 ");
                Console.WriteLine("If u want to delete an item by its id choose : 1 ");
                int deleteChoice = Convert.ToInt32(Console.ReadLine());
                if (deleteChoice == 0)
                {
                    Console.WriteLine("Enter the item index to be deleted");
                    int index = Convert.ToInt32(Console.ReadLine());
                    if (index <= 0 || index >= products.Count)
                    {
                        Console.WriteLine("Enter the valid item index");
                    }
                    else
                    {
                        products.RemoveAt(index);
                        Console.WriteLine("Item deleted succesfully");
                    }
                }
                else
                {
                    Console.WriteLine("Enter the item id to be deleted");
                    int id = Convert.ToInt32(Console.ReadLine());
                    var item = products.Find(x => x.Id == id);
                    if (item != null)
                    {
                        products.Remove(item);
                        Console.WriteLine("Item deleted succesfully");
                    }
                    else
                    {
                        Console.Write("Item not found");
                    }
                }
            } 
        }
      public static void displayItem()
        {
            Console.WriteLine("Number of Products  : " + $"{products.Count}");
            if (products.Count == 0)
            {
                Console.WriteLine("The  List is Empty, First add the items into the List");
            }
            else
            {
                Console.WriteLine("Tasks in the list:");
                for (int i = 0; i < products.Count; i++)
                {
                    Console.WriteLine($"index : {i}, {products[i]}");
                }
                //we can also use foreach loop
               /* foreach (var item in products)
                {
                    Console.WriteLine(item);
                }*/
            }
        }
      public static void findItem() 
        {
            if (products.Count == 0)
            {
                Console.WriteLine("The List is Empty, So first add items into the List");
            }
            else
            {
                Console.WriteLine("Enter the id to find an item ");
                int id = Convert.ToInt32(Console.ReadLine());
                foreach (var item in products)
                {
                    if (item.Id == id)
                    {
                        Console.WriteLine($"Id : {item.Id}, name: {item.Name}, price : {item.Price}, quantity : {item.Quantity}");
                    }
                }
            }
        }
    }
    
}
