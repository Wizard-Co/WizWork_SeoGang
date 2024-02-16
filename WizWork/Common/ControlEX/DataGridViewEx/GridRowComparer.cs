using System;
using System.Collections.Generic;
using System.Windows.Forms;


//*******************************************************************************
//프로그램명    GridRowComparer.cs
//메뉴ID        
//설명          Grid Sort에 사용할 Comparer
//작성일        2013.01.08
//개발자        남인호
//*******************************************************************************
// 변경일자     변경자      요청자      요구사항ID          요청 및 작업내용
//*******************************************************************************
//
//
//*******************************************************************************

namespace KR_POP.Common.ControlEX
{
    public class GridRowComparer : System.Collections.IComparer
    {
        private List<KeyValuePair<DataGridViewColumn, bool>> p_Lst_SortInformation;

        public GridRowComparer(List<KeyValuePair<DataGridViewColumn, bool>> columnList)
        {
            p_Lst_SortInformation = columnList;
        }

        public int Compare(object x, object y)
        {
            DataGridViewRow DataGridViewRow1 = ((DataGridViewRow)(x));
            DataGridViewRow DataGridViewRow2 = ((DataGridViewRow)(y));

            int CompareResult = compareResult(DataGridViewRow1, DataGridViewRow2, 0);

            return CompareResult;
        }


        public int compareResult(DataGridViewRow DataGridViewRow1, DataGridViewRow DataGridViewRow2, int i)
        {
            int CompareResult = 0;

            if (p_Lst_SortInformation.Count > 0)
            {
                DataGridViewColumn dgvColumn = p_Lst_SortInformation[i].Key;

                int sortOrderModifier = 0;
                if (p_Lst_SortInformation[i].Value)
                    sortOrderModifier = 1;
                else
                    sortOrderModifier = -1;


                object value1 = DataGridViewRow1.Cells[dgvColumn.Index].Value;
                object value2 = DataGridViewRow2.Cells[dgvColumn.Index].Value;

                //Sort Images Together if images are in datagrid view
                if ((value1 is System.Drawing.Bitmap) && !(value2 is System.Drawing.Bitmap))
                    return -1 * sortOrderModifier;
                else if (!(value1 is System.Drawing.Bitmap) && (value2 is System.Drawing.Bitmap))
                    return 1 * sortOrderModifier;
                else if (value1 is System.Drawing.Bitmap && value2 is System.Drawing.Bitmap)
                    return 0;

                string cellValue1 = Convert.ToString(DataGridViewRow1.Cells[dgvColumn.Index].Value);
                string cellValue2 = Convert.ToString(DataGridViewRow2.Cells[dgvColumn.Index].Value);

                //When Cell value is null or empty
                if ((cellValue1 == null || cellValue1 == string.Empty) && (cellValue2 != null || cellValue2 != string.Empty))
                    return -1 * sortOrderModifier;
                else if ((cellValue1 != null || cellValue1 != string.Empty) && (cellValue2 == null || cellValue2 == string.Empty))
                    return 1 * sortOrderModifier;
                else if ((cellValue1 == null || cellValue1 == string.Empty) && (cellValue2 == null || cellValue2 != string.Empty))
                    return 0;

                //compare Numeric values
                if (dgvColumn.ValueType == typeof(Double))
                {
                    double numVal1 = Convert.ToDouble(cellValue1);
                    double numVal2 = Convert.ToDouble(cellValue2);

                    if (numVal1 > numVal2)
                        CompareResult = 1;
                    else if (numVal1 < numVal2)
                        CompareResult = -1;
                    else
                        CompareResult = 0;
                }
                //compare date values
                else if (dgvColumn.ValueType == typeof(DateTime))
                {
                    DateTime cellValueDt1;
                    DateTime cellValueDt2;

                    if ((DateTime.TryParse(cellValue1, out cellValueDt1)) && (DateTime.TryParse(cellValue2, out cellValueDt2)))
                    {
                        if (cellValueDt1 > cellValueDt2)
                            CompareResult = 1;
                        else if (cellValueDt1 < cellValueDt2)
                            CompareResult = -1;
                        else
                            CompareResult = 0;

                    }
                }
                else //compare string values
                {
                    CompareResult = System.String.Compare(cellValue1, cellValue2);
                }

                CompareResult = CompareResult * sortOrderModifier;

                //if same values, perform this routine again
                if (CompareResult == 0)
                {
                    if (i != p_Lst_SortInformation.Count - 1)
                    {
                        i++;
                        CompareResult = compareResult(DataGridViewRow1, DataGridViewRow2, i);
                    }
                }
            }

            return CompareResult;
        }
    }
}
