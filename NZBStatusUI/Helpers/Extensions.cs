using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using JsonDataManipulator.DTOs;

namespace NZBStatusUI.Helpers
{
    public static class Extensions
    {
        public static bool ContainsValue(this DataGridView dgv, string key, string value)
        {
            if (!dgv.Columns.Contains(key))
            {
                return false;
            }
            return dgv.Rows.Cast<DataGridViewRow>().Any(r => r.Cells[key].Value.ToString().Equals(value));

        }

        public static int GetRowID(this DataGridView dgv, string key, string value)
        {
            // return dgv.Rows.Cast<DataGridViewRow>().FirstOrDefault(r => r.Cells[key].Value.ToString().Equals(value)).;
            var dataGridViewRow = dgv.Rows.Cast<DataGridViewRow>().FirstOrDefault(r => r.Cells[key].Value.ToString().Equals(value));
            if (dataGridViewRow != null) return dataGridViewRow.Index;
            return -1;

            //bool result = false;
            //foreach (DataGridViewRow row in dgv.Rows)
            //{
            //    if (row.Cells[key].Value.ToString().Equals(value))
            //    {
            //        result = true;
            //        break;
            //    }
            //}
            //return result;
        }

        public static HashSet<string> GetHashSet(this DataGridView dgv, string key)
        {
            // return dgv.Rows.Cast<DataGridViewRow>().FirstOrDefault(r => r.Cells[key].Value.ToString().Equals(value)).;

            if (!dgv.Columns.Contains(key))
            {
                return new HashSet<string>();
            }

            var result = new HashSet<string>();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                result.Add(row.Cells[key].Value.ToString());
            }
            return result;
        }
    }
}