using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Console.Abstract
{
    public interface IEnumOperation
    {
        T GetEnumFromDescription<T>(string description) where T : Enum;
        int GetEnumItemsCount<T>();
        string GetEnumDescription<T>(Enum item) where T : Enum;
    }
}
