using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentThree
{
    interface IAppointment<AptList>
    {
        int Count { get; }
        void AddAppointments(AptList item);
        AptList GetAppointments(int index);
        void RemoveUnbookedAppointments(int index);
        void SortBySizeOfLot();
    }
}
