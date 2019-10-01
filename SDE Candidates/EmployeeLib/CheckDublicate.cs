using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeLib
{
    class CheckDublicate : IEqualityComparer<Employees>
    {
        //Check and remove dublicates from employee csv
      
            public bool Equals(Employees x, Employees y)
            {
                return x.Id.Trim().Equals(y.Id) || (x.ManagerId.Trim().Equals("") && y.ManagerId.Trim().Equals(""));
            }
            public int GetHashCode(Employees obj)
            {
                return obj.Id.GetHashCode();
            }
        }
    }

