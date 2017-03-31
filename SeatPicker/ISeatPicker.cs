using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewQuestion
{
    public interface ISeatPicker
    {
        string DeveloperName { get; }
        int GetSeat(List<bool> currentSeats);
    }
}
