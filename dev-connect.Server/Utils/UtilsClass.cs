using System.ComponentModel;
using dev_connect.Server.Data.Dto;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace dev_connect.Server.Utils
{
    public class UtilsClass
    {
        public static PaginationResponse<T> PaginationWrapper<T>(long totalCount, int totalPages, List<T>? data)
        {
            return new PaginationResponse<T>
            {
                Data = data,
                TotalCount = totalCount,
                PageSize = totalPages
            };
        }
        public class EnumDescriptionConverter<TEnum> : ValueConverter<TEnum, string>
            where TEnum : struct, Enum
        {
            public EnumDescriptionConverter()
                : base(
                      v => GetEnumDescription(v),
                      v => GetEnumValue(v))
            {

            }
            private static string GetEnumDescription(TEnum value)
            {
                var fieldInfo = value.GetType().GetField(value.ToString());
                var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));
                return attribute?.Description ?? value.ToString();
            }
            public static TEnum GetEnumValue(string description)
            {
                foreach (var field in typeof(TEnum).GetFields())
                {
                    var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                    if(attribute != null && attribute.Description == description)
                    {
                        return (TEnum)field.GetValue(null);
                    }
                }
                throw new ArgumentException($"No enum value found for description '{description}'.");
            }
        }
    }
}
