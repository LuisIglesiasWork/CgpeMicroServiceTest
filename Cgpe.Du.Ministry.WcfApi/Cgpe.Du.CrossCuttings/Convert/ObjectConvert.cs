using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Cgpe.Du.CrossCuttings
{
    public class DtoToDataTableConverter<T>
    {
        public DataTable Map(List<T> sourceList)
        {
            DataTable objToDataTableAdded = null;
            var propertyInfos = typeof(T).GetProperties().Where(x => !x.PropertyType.ToString().Contains("System.Collections."));
            foreach (var name in propertyInfos.Select(x => x.Name))
            {
                DataTable objToDataTableMerged = null;
                if (sourceList != null)
                    foreach (T procuratorListDto in sourceList)
                    {
                        var objToDataTable = this.ConvertObjectToDataTable((Object) typeof(T).GetProperty(name).GetValue(procuratorListDto), name);
                        objToDataTableMerged?.Merge(objToDataTable);
                        if (objToDataTableMerged == null)
                        {
                            objToDataTableMerged = objToDataTable;
                        }
                    }

                if (objToDataTableAdded == null)
                {
                    objToDataTableAdded = objToDataTableMerged;
                }
                else
                {
                    objToDataTableAdded.Columns.Add(objToDataTableMerged?.Columns[objToDataTableMerged.Columns.Count - 1]
                        .ColumnName);
                    for (var i = 0; i < objToDataTableMerged?.Rows.Count; i++)
                    {
                        objToDataTableAdded.Rows[i][objToDataTableMerged.Columns[objToDataTableMerged.Columns.Count - 1]
                            .ColumnName] = objToDataTableMerged.Rows[i][0];

                    }

                }
            }

            return objToDataTableAdded;
        }

        private DataTable ConvertObjectToDataTable(Object obj, string columnName)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(columnName);
            //foreach (PropertyInfo info in typeof(T).GetProperties())
            //    dt.Rows.Add(info.Name);
            dt.Rows.Add(obj ?? " ");

            dt.AcceptChanges();
            return dt;
        }
    }
}
