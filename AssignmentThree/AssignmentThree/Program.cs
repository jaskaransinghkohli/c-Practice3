using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentThree
{
    //DELEGATE
    public delegate void CustomerTasks();
    class Program
    {
     //ENUM
        enum Properties
        {
            RuralProperty, StandAloneProperty, TownHouseProperty
        }
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Go();
            Console.ReadKey();
            
        }
        public void Go()
        {
            //APPOINTMENT SCHEDULE
            string[] schedule = new string[5] { "7:00 am", "8:00 am", "9:00 am", "10:00 am", "11:00 am" };
            Hashtable scheduleCollection = new Hashtable();
            //CREATING INTERFACE FOR CALLLING METNODS OF APPOINTMENT AND CUSTOMER
            //GIVING REFERENCE OF INTERFACE CLASS AND CREATED OBJECT OF INTERFACE SUB CLASS
            IAppointment<AppointmentBase> appObj = new Appointment<AppointmentBase>();
            //CREATING OBJECT OF INTERFACE FROM  ONE OF THCHILD CLASS AS CUSTOMER BASE CLASS IS ABSTRACT
            //SO CANOT CREATE AN INSTANCE OF BASE CLASS AND CHILD CLASS INHERITS ALL THE PROPERTY OF PARENT CLASS
            ICustomer cust = new RuralProperty();
            //CREATING SCHEDULE HASHTABLE 
            for (int i = 0; i < schedule.Length; i++)
            {
                string scheduleTime = (string)schedule[i];
                int scheduleIndex = i + 1;
                //ADDING TIME AND INDEX TO HASHTABLE
                scheduleCollection.Add(scheduleIndex, scheduleTime);
                //ADDING TIME TO APPOINTMENT BASE
                AppointmentBase aptBaseObj = new AppointmentBase();
                aptBaseObj.Time = scheduleTime;
                //CALLING ADD METHOD FROM IAPPOINTMENT INTERFACE
                appObj.AddAppointments(aptBaseObj);
            }
            bool isAppointmentNeeded = false;
            do {
                string userRes = string.Empty;
                //ASK APPOINTMENT WITH VALIDATION
                do
                {
                    Console.Write("Do you need an appointment? (y/n) [press n to terminate]: ");
                    userRes = Console.ReadLine();
                } while ((userRes.ToUpper() != "Y") && (userRes.ToUpper() != "N"));
                //END WHEN USER SELECTS NO
                if ((userRes.ToUpper() == "N"))
                {
                    isAppointmentNeeded = false;
                    continue;
                }
                //DISPLAY APPOINTMENTS SCHEDULE
                for (int i = 0; i <= schedule.Length; i++)
                {
                    if (scheduleCollection.ContainsKey(i))
                    {
                        Console.WriteLine($"{i}. {scheduleCollection[i]}");
                    }
                }
                //USER SLOT SELECTION WITH VALIDATION
                string userSelectionString = string.Empty;
                int userSelection = 0;
                do
                {
                    Console.Write("Please choose available slot : ");
                    userSelectionString = Console.ReadLine();
                } while (!int.TryParse(userSelectionString, out userSelection) || (!scheduleCollection.ContainsKey(userSelection))) ;
                //PROPERTY
                Console.WriteLine("Here are the properties we accept:");
                var propertiesEnum = Enum.GetValues(typeof(Properties));
                int index = 0;
                //DISPLAY PROPERTY FROM ENUM
                foreach (var property in propertiesEnum)
                {
                    Console.WriteLine($"{++index}. {property}");
                }
                //PROPERTY
                string propertyChoice = string.Empty;
                int choice = 0;
                //VALIDATE PROPERTY AND CAPTURE PROPERTY SELECTED
                do
                {
                    Console.Write("Which Type Of Properity Landscaping You Prefer? ");
                    propertyChoice = Console.ReadLine();
                } while ((!int.TryParse(propertyChoice, out choice)) || ((choice <= 0) || (choice > propertiesEnum.Length)));
                //USER PROPERTY SELECTION
                if (choice > 0)
                {
                    //-----------------------------NAME OF CUSTOMER----------------------------
                    do
                    {
                        Console.Write("Please Enter Your Name: [eg. john or john doe]: ");
                        cust.CustName = Console.ReadLine();
                    } while (!cust.ValidateCustomerName());
                    string custNameString = cust.CustName;
                    //-----------------------------SIZE OF LOT---------------------------------
                    string szOfLotString = string.Empty;
                    decimal szOfLotDecimal = 0;
                    do
                    {
                        Console.Write("How Much acres land You Own? [Should be less than 100 acre] ");
                        // cust.SizeOfLot = Console.ReadLine();
                        szOfLotString = Console.ReadLine();
                        decimal.TryParse(szOfLotString, out szOfLotDecimal);
                        cust.SizeOfLot = szOfLotDecimal;
                    } while (!cust.ValidateSizeOfLot());
                    decimal sizeOfLotString = cust.SizeOfLot;
                    //--------------------------------SIZE OF WORK-----------------------------
                    do
                    {
                        Console.Write($"How Much acre land is available for the Landscaping Work? " +
                            $"[Should be less than or equal to land you own ({sizeOfLotString})] ");
                        cust.SizeOfWorkArea = Console.ReadLine();
                    } while (!cust.ValidateSizeOfWork());
                    string sizeOfWorkAreaString = cust.SizeOfWorkArea;
                    //--------------------------------CREDIT CARD-----------------------------               
                    do
                    {
                        Console.Write("Enter Credit Card Details [Format : 0000 0000 0000 0000]: ");
                        cust.CreditCard = Console.ReadLine();
                    } while (!cust.ValidCreditCardNumber());
                    string ccNumMaskString = cust.MaskCreditCardNum;
                    //STORE CUSTOMER PROPERTY SELECTION INFO
                    Customer property = null;
                    //CALLING PROPERTY TYPE SELECTED BY USER
                    switch (choice - 1)
                    {
                        case (int)Properties.RuralProperty:
                            do
                            {
                                Console.Write("Is Working Area Clean? (y/n): ");
                                userRes = Console.ReadLine();
                            } while ((userRes.ToUpper() != "Y") && (userRes.ToUpper() != "N"));
                            property = new RuralProperty(custNameString, sizeOfLotString, sizeOfWorkAreaString, ccNumMaskString, (userRes.ToUpper() == "Y") ? true : false);
                            break;
                        case (int)Properties.StandAloneProperty:
                            do
                            {
                                Console.Write("Is enterance to the lot cleared (y/n) ");
                                userRes = Console.ReadLine();
                            } while ((userRes.ToUpper() != "Y") && (userRes.ToUpper() != "N"));
                               property = new StandAloneProperty(custNameString, sizeOfLotString, sizeOfWorkAreaString, ccNumMaskString, (userRes.ToUpper() == "Y") ? true : false);
                            break;
                        case (int)Properties.TownHouseProperty:
                            do
                            {
                                Console.Write("Did Neighbors gave consent? (y/n) ");
                                userRes = Console.ReadLine();
                            } while ((userRes.ToUpper() != "Y") && (userRes.ToUpper() != "N"));
                             property = new TownHouseProperty(custNameString, sizeOfLotString, sizeOfWorkAreaString, ccNumMaskString, (userRes.ToUpper() == "Y") ? true : false);
                            break;

                        default:
                            Console.WriteLine("Please select the service which is mentioned in the list.");
                            break;
                    }
                    //ADD SELECTED PROPERTY DATA TO APPOINTMENT OBJECT
                    appObj.GetAppointments(userSelection - 1).PropertyType = property;
                    //SETTING APPOINTMENT TRUE
                    isAppointmentNeeded = true;
                    //REMOVE BOOKED SLOT FROM SCHEDULE
                    scheduleCollection.Remove(userSelection);
                }
            } while ((isAppointmentNeeded) && (scheduleCollection.Keys.Count > 0));
            //REMOVE UNSELECTED APPOINTMENTS BY USER FOR SORTING ARRAYLIST
            for (int i = appObj.Count - 1; i >= 0; i--)
            {
                if (appObj.GetAppointments(i).PropertyType == null)
                {
                    //CALLING REMOVE METHOD FROM IAPPOINTMENT INTERFACE
                    appObj.RemoveUnbookedAppointments(i);
                }
            }
            //CALLING SORT METHOD FROM IAPPOINTMENT INTERFACE
            appObj.SortBySizeOfLot();
            //DISPLAY OUTPUT
            int customerCount = 1;
            Console.WriteLine("\n\t***** APPOINTMENT SUMMARY FOR CUSTOMERS BY SIZE OF LOT *****");
            for (int i = 0; i < appObj.Count; i++)
            {
                Console.Write($"\n\t\t\t\t--- CUSTOMER #{customerCount} ---" +
                        $"\n\nTime Slot booked at {appObj.GetAppointments(i).Time} for customer {appObj.GetAppointments(i).PropertyType}. " +
                        $"All the Customers performs following tasks : ");
                //DELEGATE FOR USER'S TASKS
                CustomerTasks custTaskDelegate = appObj.GetAppointments(i).PropertyType.CreateDesign;
                custTaskDelegate += appObj.GetAppointments(i).PropertyType.EstimateWork;
                custTaskDelegate += appObj.GetAppointments(i).PropertyType.ArrangeWorkers;
                custTaskDelegate();
                customerCount += 1;
            }
            //WHEN USER TERMINATES WITHOUT SELECTING SLOT
            if (customerCount == 1)
            {
                Console.WriteLine("\nSorry No appointments were booked");
            }
            Console.WriteLine("\nPlease press any key twice to exit");
            Console.ReadKey();
        }
    }
}
