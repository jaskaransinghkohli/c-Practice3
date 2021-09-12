using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentThree
{
    //APPOINTMENT IMPLEMENTS IAPPOINTMENT AND IDISPOSAL(.NET INTERFACE)
    class Appointment<AptList> : IAppointment<AptList>, IDisposable
    {
        private List<AptList> appointments = null;
        public Appointment()
        {
            appointments = new List<AptList>();
        }
        //GETTER AND SETTER FOR ARRAY LIST
        public List<AptList> AppointmentList
        {
            get => this.appointments; set => this.appointments = value;
        }
        //INDEXING THE LIST FOR ADD, GET, AND REMOVE APPOINTMENT
        public AptList this[int i]
        {
            get => this.appointments[i]; set => this.appointments[i] = value;
        }
        //--------------------------------------INTERFACE METHODS IMPLEMENTATION----------------------------
        //FOR LOOPING ARRAY LIST
        public int Count
        {
            get => this.appointments.Count;
        }
        //ADDING APPOINTMENT OBJECT TO LIST
        public void AddAppointments(AptList aptObj)
        {
            this.appointments.Add(aptObj);
        }
        //GET APPOINTMENT FROM LIST
        public AptList GetAppointments(int index)
        {
            return this.appointments[index];
        }
        //REMOVING ITEM FROM FOR SORTING
        public void RemoveUnbookedAppointments(int index)
        {
            this.appointments.RemoveAt(index);
        }
        //SORTING LIST BY SIZE OF LOT PASSING USING COMPARETO METHOD FROM <ICOMPARABLE> INTERFACE  
        public void SortBySizeOfLot()
        {
            this.appointments.Sort();
        }
        //GARBAGE COLLECTOR FOR DISPLSING UNUSED OBJECTS
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
