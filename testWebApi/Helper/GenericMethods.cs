
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Dynamic;
using System.Reflection;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;

using System.Text.Json;

namespace testWebApi.Helper
{
    public static class GenericMethods
    {
        public static object Exception(Exception ex)
        {
            return (new { isValid = false, message = "Error Occure Please Contact to Administrator !", status = ErrorCode.Exception, Result = ex.Message });
        }



        public static object CreateResponce(ObjectResponce objectResponce, object result = null, Exception ex = null, string Hash = null, string Message = null)
        {
            try
            {
                if (ObjectResponce.Accepted == objectResponce)
                {
                    if (result == null)
                    {
                        return (new { isValid = true, Message = Message, ResponseCode = Helper.ReponseCode.Success, Result = result });
                    }
                    else
                    {
                        return (new { isValid = true, Message = "Accepted", ResponseCode = Helper.ReponseCode.Success, Result = result });
                    }
                }
                else if (ObjectResponce.Invalid_Operation == objectResponce)
                {
                    return (new { isValid = true, Message = Message, ResponseCode = Helper.ReponseCode.InvalidOperation, Result = "" });
                }
                else
                {
                    return (new { isValid = false, Message = "No Data Found", ResponseCode = Helper.ReponseCode.InvalidIBAN, Result = "Invalid IBAN" });
                }
            }
            catch (Exception ex1)
            {
                throw new Exception(ex1.Message);
            }
        }

        public static DataTable ToDataTable<T>(this List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {

                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        public static string DataTableToString(this DataTable dt)
        {
            if (dt.Rows.Count < 1)
            {
                return "";
            }
            else
            {
                string finalString = "";
                foreach (DataRow dtRow in dt.Rows)
                {
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (dtRow[dc].ToString() != "" && dtRow[dc].ToString() != null && dtRow[dc].ToString() != string.Empty)
                        {
                            finalString += dtRow[dc].ToString() + ";";
                        }
                    }
                    finalString += "|";
                }
                return finalString.TrimEnd('|');
            }
        }

        public static List<T> ConvertDataTableToList<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }


        //SAH Genaric method

        public static List<dynamic> ConvertDynamicListToDataTable<dynamic>(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    var result = GetDatatableClass(dt);

                    var jsonStr = JsonSerializer.Serialize(result);


                    var ReturnResult = JsonSerializer.Deserialize<List<dynamic>>(jsonStr);

                    return ReturnResult;


                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {

            }
        }

        private static List<dynamic> GetDatatableClass(DataTable dt)
        {

            List<dynamic> expandoList = new List<dynamic>();

            foreach (DataRow row in dt.Rows)
            {
                //create a new ExpandoObject() at each row
                var expandoDict = new ExpandoObject() as IDictionary<String, Object>;
                foreach (DataColumn col in dt.Columns)
                {
                    //put every column of this row into the new dictionary
                    expandoDict.Add(col.ToString(), row[col.ColumnName].ToString());
                }

                //add this "row" to the list
                expandoList.Add(expandoDict);
            }
            return expandoList;
        }

        public static IEnumerable<T> ToEnumerable<T>(this T input)
        {
            yield return input;
        }










    }

    public class TravelResponceValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value.ToString() != "0")
            {
                return new ValidationResult("Only Successful Case are acceptable !");
            }
            return ValidationResult.Success;
        }
    }

   

}