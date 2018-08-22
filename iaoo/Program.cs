using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iaoo
{
    /// <summary>
    /// Basic outputs for customers callable through an interface
    /// </summary>
    public interface ICustomer
    {
        // interface cannot contain fields
        void print(); // public by default
    }

    /// <summary>
    /// special treatments for big volume customers
    /// </summary>
    public interface IBigCustomer : ICustomer // inheritance of ICustomer
    {
        void addSpecialTreatment();
    }

    /// <summary>
    /// Basic implementation of a customer
    /// </summary>
    public abstract class BaseCustomer
    {
        public string name { get; set; }
        public double importanceIndex { get; set; }
        public abstract void raiseImportance(); // can have access modifier (public) and has to be implemented in derived classes
        public void raiseImportance(double multiplier) // overloaded function
        {
            importanceIndex *= multiplier;
        }
    }

    /// <summary>
    /// Customer with lower volume
    /// </summary>
    public class NormalCustomer : BaseCustomer, ICustomer // inherits the base abstract class which is not instantiable
    {
        public override void raiseImportance() // override abstract function
        {
            importanceIndex *= 1.1;
        }

        public void print() // can have access modifier (public) (interfaces cannot)
        {
            Console.WriteLine($"Hello {name}, you have a Ranking of {importanceIndex}.");
            Console.WriteLine("----------");
        }
    }

    /// <summary>
    /// Customer with higher importance
    /// </summary>
    public class PayingCustomer : BaseCustomer, IBigCustomer // abstract class can inherit from an abstract class and/or from an Interface but not from multiple abstract classes
    {
        public int buyingVolume { get; set; }

        public override void raiseImportance()
        {
            importanceIndex *= 1.3;
        }

        public void print()
        {
            Console.WriteLine($"Hello {name}, you have a Ranking of {importanceIndex}. Your buying volume is {buyingVolume} Euros.");
            Console.WriteLine("----------");
        }

        public void addSpecialTreatment() // must implement the function because we inherit the secondary Interface IBigCustomer
        {
            Console.WriteLine($"You look good today!");
        }
    }

    class Program
    {
        /// <summary>
        /// Calls the print function of a customer instance which inherits from ICustomer interface
        /// </summary>
        /// <param name="customer">customer instance which inherits from ICustomer interface</param>
        static void printCustomers(ICustomer customer)
        {
            customer.print();
        }

        static void Main(string[] args)
        {
            NormalCustomer c = new NormalCustomer()
            {
                name = "John Doe",
                importanceIndex = 1
            };

            PayingCustomer pc = new PayingCustomer()
            {
                name = "Max Monterey",
                importanceIndex = 1,
                buyingVolume = 20000
            };
            c.raiseImportance(1.3); // calling overloaded function
            pc.raiseImportance(2.0);
            c.raiseImportance(); // calling basic function
            pc.raiseImportance();

            // using the same method to call different classes but the same inheritance of Interface
            printCustomers(c);
            pc.addSpecialTreatment(); // calling function of secondary inherited Interface
            printCustomers(pc);
            

            Console.WriteLine("Press any key to continue...");

            Console.ReadKey(true);
        }

        
    }
}
