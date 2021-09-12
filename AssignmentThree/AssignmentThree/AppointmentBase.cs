using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentThree
{
    //APPOINTMENT BASE IMPLEMENTS ICOMPARABLE(.NET INTERFACE)
    class AppointmentBase : IComparable<AppointmentBase>
    {
        public string time;
        public Customer property;
        public string Time { get => time; set => time = value; }
        internal Customer PropertyType { get => property; set => property = value; }
        //COMPARING SIZE OF LOT FOR SORTING FROM .NET INTERFACE ICOMPARABLE
        public int CompareTo(AppointmentBase appointmentObj)
        {
            return this.property.SizeOfLot.CompareTo(appointmentObj.property.SizeOfLot);
        }
    }
}
