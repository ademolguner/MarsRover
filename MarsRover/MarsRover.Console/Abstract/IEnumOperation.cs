using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Console.Abstract
{
    public interface IEnumOperation
    {
        T GetValueFromDescription<T>(string value);
        int GetEnumItemCount<T>();
        string GetEnumDescription<T>(Enum value);
    }
}
