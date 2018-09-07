using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Common
{
    public class MapperHelper<T>where T:new()
    {
        public T MapperModel(DataRow dr)
        {
            if(dr == null)
            {
                return default(T);
            }
            T model = new T();
            for(int i=0;i < dr.Table.Columns.Count; i++)
            {
                PropertyInfo propertyInfo = model.GetType().GetProperty(dr.Table.Columns[i].ColumnName);
                if(propertyInfo != null && dr[i] != DBNull.Value)
                {
                    propertyInfo.SetValue(model, dr[i], null);
                }
            }
            return model;
        }

        public List<T> MapperList(DataTable dt)
        {
            if (!Utility.TableHelper(dt))
            {
                return null;
            }
            List<T> list = new List<T>();
            foreach(DataRow dr in dt.Rows)
            {
                list.Add(MapperModel(dr));
            }
            return list;
        }
    }
}
