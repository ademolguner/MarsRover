using MarsRover.Console.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Console.Manager
{
    public class EnumOperation : IEnumOperation
    {
        public T GetEnumFromDescription<T>(string value) where T : Enum   // type safety
        {
            var type = typeof(T);
            // This control can collect with T condition where you already can afford, but, added to check for availability
            if (!type.IsEnum) throw new InvalidOperationException(message: $"The object of type (T) sent is not of ENUM type. T must be an enum type object"); ;
            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == value)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == value)
                        return (T)field.GetValue(null);
                }
            }
            throw new InvalidOperationException(message: $"Incorrect direction entered !  no type {value} direction.");
        }

        public int GetEnumItemsCount<T>()
        {
            var myEnumMemberCount = Enum.GetValues(typeof(T)).Length;
            return myEnumMemberCount;
        }

        public string GetEnumDescription<T>(Enum item) where T : Enum
        {
            return ((DescriptionAttribute) (item.GetType().GetMember(item.ToString())[0]
                .GetCustomAttributes(typeof(DescriptionAttribute), false).ElementAt(0))).Description;
        }
    }
}
