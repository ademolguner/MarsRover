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
        public T GetEnumFromDescription<T>(string value) where T : Enum
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException(message: $"Gönderilen (T) tipindeki nesne ENUM tipinde değildir. T bir enum tipinde object olmalıdır"); ;
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
            throw new InvalidOperationException(message: $"Girilen yön hatalı ! {value} tipinde bir yön bulunmamaktadır.");
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
