using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
//using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Rendering;
using KR_POP.Common.ControlEX;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Data;


//*******************************************************************************
//프로그램명    Methods.cs
//메뉴ID        
//설명          공통 메서드
//작성일        2013.01.08
//개발자        남인호
//*******************************************************************************
// 변경일자     변경자      요청자      요구사항ID          요청 및 작업내용
//*******************************************************************************
//
//
//*******************************************************************************

namespace KR_POP.Common
{
	public class Methods
	{
		public static SoundPlayer Player;

		public static bool ConvertToBool(string value)
		{
			if (string.IsNullOrEmpty(value) == true)
			{
				return false;
			}
			else
			{
				try
				{
					if (value.Equals("1") == true)
					{
						return true;
					}
					else if (value.ToUpper().Equals("Y") == true)
					{
						return true;
					}

					else if (value.Equals("On") == true)
					{
						return true;
					}
					else if (value.Equals("Checked") == true)
					{
						return true;
					}
					else
					{
						return Convert.ToBoolean(value);
					}
				}
				catch (InvalidCastException ex)
				{
					System.Diagnostics.Debug.Print(ex.ToString());
					return false;
				}
			}
		}

		public static string ConvertToString(object value)
		{
			if (value.GetType() == typeof(string) && string.IsNullOrEmpty((string)value) == true)
			{
				return string.Empty;
			}
			else
			{
				return Convert.ToString(value);
			}
		}

		/// <summary>
		/// 문자열에서 숫자, decimal point 추출 .윤창호
		/// </summary>
		/// <param name="_str">문자열</param>
		/// <returns>0 또는 숫자형태 문자열</returns>
		public static string ConvertToDigitTypeString(string _str)
		{
			if (_str == null || _str == "") return "0";

			string rtnString = "";

			for (int i = 0; i < _str.Length; i++)
			{
				if (Char.IsDigit(_str, i)) rtnString += _str.Substring(i, 1);
				if (_str.Substring(i, 1) == ".") rtnString += _str.Substring(i, 1);
			}
			return rtnString == "" ? "0" : rtnString;
		}

		/// <summary>
		/// BarCode -> ProdID
		/// </summary>
		/// <param name="_strBarCode"></param>
		/// <returns></returns>
		public static string ConvertToProdID(string _strBarCode)
		{
			string prodID = string.Empty;

			string[] codeSplit = _strBarCode.Split('-');
			if (codeSplit.Length == 1)
			{
				//귀뚜라미
				prodID = codeSplit[0];
			}
			else
			{
				//나노켐
				prodID = codeSplit[1].Trim() + codeSplit[2].Trim() + codeSplit[3].Trim() + codeSplit[4].Trim();
			}
			return prodID;
		}



		/// <summary>
		/// OK버튼 눌렀을 경우 사운드소리
		/// Resources-이미지에서 wav파일 추가후 파일이름변경해서 사용가능
		/// </summary>
		public static void OKWavPlayer()
		{
			Player = new SoundPlayer(

										Properties.Resources.se_alert);
			Player.Play();
		}

		/// <summary>
		/// Cancel버튼 눌렀을 경우 사운드소리
		/// Resources-이미지에서 wav파일 추가후 파일이름변경해서 사용가능
		/// </summary>
		public static void ErrorWavPlayer(string divCode)
		{
			switch (divCode)
			{
				case "31":
					Player = new SoundPlayer(Properties.Resources.Error31);
					break;
				default:
					Player = new SoundPlayer(Properties.Resources.Error41);
					break;
			}
			Player.Play();
			//Player = new SoundPlayer(

			//                            Properties.Resources.bad);
			//Player.Play();
		}
		public static void OKWavPlayer(string divCode)
		{
			switch (divCode)
			{
				case "31":
					Player = new SoundPlayer(Properties.Resources.OK31);
					break;
				default:
					Player = new SoundPlayer(Properties.Resources.OK41);
					break;
			}
			Player.Play();
		}
		public static void SimpleScanWavPlayer(string divCode)
		{
			switch (divCode)
			{
				case "31":
					Player = new SoundPlayer(Properties.Resources.OK31);
					Player.Play();
					break;
			}
		}
		public static void FillItemWavPlayer(string divCode)
		{
			switch (divCode)
			{
				case "31":
					Player = new SoundPlayer(Properties.Resources.Scan31);
					break;
				default:
					Player = new SoundPlayer(Properties.Resources.Scan41);
					break;
			}
			Player.Play();
		}
		public static void ErrorCountWavPlayer(string divCode)
		{
			switch (divCode)
			{
				case "31":
					break;
				default:
					Player = new SoundPlayer(Properties.Resources.CountError41);
					Player.Play();
					break;
			}
        }

        #region 엑셀 사용안함. 참고용
        /// <summary>
		/// 엑셀내보내기. 특정 컬럼에 셀서식 지정, 특정 컬럼 제외해서 내보내기
		/// 현재 사용하는곳 없음
		/// </summary>
		/// <param name="captions"></param>
		/// <param name="dataGridView"></param>
		/// <param name="saveFileDialog"></param>
		/// <param name="filename"></param>
		/// <param name="cellTypes"></param>
        //public static void ExportExcel3(bool captions, DataGridView dataGridView, SaveFileDialog saveFileDialog, string filename, string[] cellTypes, int[] exceptCells)
        //{
        //    saveFileDialog.FileName = filename;       //파일명
        //    saveFileDialog.DefaultExt = "xls";          //확장자
        //    saveFileDialog.Filter = "Excel files (*.xls)|*.xls";
        //    saveFileDialog.InitialDirectory = Environment.SpecialFolder.Recent.ToString();
        //    //saveFileDialog.InitialDirectory = "c:\\";       //경로

        //    DialogResult result = saveFileDialog.ShowDialog();      //저장다이얼로그

        //    if (result == DialogResult.OK)      //다이얼로그 OK일 경우
        //    {
        //        int num = 0;
        //        object missingType = Type.Missing;

        //        Excel.Application objApp = null;
        //        Excel._Workbook objBook = null;
        //        Excel.Workbooks objBooks = null;
        //        Excel.Sheets objSheets = null;
        //        Excel._Worksheet objSheet = null;
        //        Excel.Range range = null;

        //        string[] headers = new string[dataGridView.ColumnCount - exceptCells.Length];
        //        string[] columns = new string[dataGridView.ColumnCount - exceptCells.Length];

        //        int colIndex = 0;
        //        //headers[colIndex] = "거래처";
        //        //num = colIndex + 65;
        //        //columns[colIndex] = Convert.ToString((char)num);

        //        //colIndex++;

        //        for (int c = 0; c < dataGridView.ColumnCount; c++)
        //        {
        //            bool breakIndex = false;
        //            for (int k = 0; k < exceptCells.Length; k++)
        //            {
        //                if (c == exceptCells[k])    // 제외할 컬럼
        //                {
        //                    breakIndex = true;
        //                }
        //            }

        //            if (breakIndex) continue;

        //            headers[colIndex] = dataGridView.Rows[0].Cells[c].OwningColumn.HeaderText.ToString();
        //            num = colIndex + 65;
        //            columns[colIndex] = Convert.ToString((char)num);

        //            colIndex++;
        //        }

        //        try
        //        {
        //            objApp = new Excel.Application();
        //            objBooks = objApp.Workbooks;
        //            objBook = objBooks.Add(Missing.Value);
        //            objSheets = objBook.Worksheets;
        //            objSheet = (Excel._Worksheet)objSheets.get_Item(1);

        //            if (captions)   //Captions가 True일때
        //            {
        //                colIndex = 0;

        //                for (int c = 0; c < dataGridView.ColumnCount; c++) //캡션명
        //                {
        //                    bool breakIndex = false;
        //                    for (int k = 0; k < exceptCells.Length; k++)
        //                    {
        //                        if (c == exceptCells[k])    // 제외할 컬럼
        //                        {
        //                            breakIndex = true;
        //                        }
        //                    }

        //                    if (breakIndex) continue;

        //                    range = objSheet.get_Range(columns[colIndex] + "1", Missing.Value);
        //                    range.set_Value(Missing.Value, headers[colIndex]);
        //                    range.Interior.Color = ColorTranslator.ToOle(Color.LightGray);
        //                    range.Borders.Color = ColorTranslator.ToOle(Color.Black);

        //                    colIndex++;
        //                }
        //            }


        //            for (int i = 0; i < dataGridView.RowCount; i++)    //DataGirdView에 Row값
        //            {
        //                colIndex = 0;

        //                for (int j = 0; j < dataGridView.ColumnCount; j++)
        //                {
        //                    bool breakIndex = false;
        //                    for (int k = 0; k < exceptCells.Length; k++)
        //                    {
        //                        if (j == exceptCells[k])    // 제외할 컬럼
        //                        {
        //                            breakIndex = true;
        //                        }
        //                    }

        //                    if (breakIndex) continue;


        //                    if (dataGridView.Rows[i].Cells[j].Value != null)
        //                    {
        //                        range = objSheet.get_Range(columns[colIndex] + Convert.ToString(i + 2), Missing.Value);

        //                        //range.NumberFormat = @"@"; //셀서식 - 텍스트
        //                        //range.NumberFormat = @"mm-dd-yyyy"; //셀서식 - 날짜형식
        //                        //range.NumberFormat = @"###.#0"; //셀서식 - 숫자형식
        //                        if ("TEXT".Equals(cellTypes[j]))
        //                            range.NumberFormatLocal = @"@";
        //                        range.set_Value(Missing.Value, dataGridView.Rows[i].Cells[j].FormattedValue.ToString());
        //                        SetColorAndFontStyle(range, dataGridView.Rows[i].Cells[j]);
        //                    }

        //                    colIndex++;
        //                }
        //            }

        //            objApp.Visible = false;
        //            objApp.UserControl = false;

        //            objBook.SaveAs(@saveFileDialog.FileName,
        //                       Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal,
        //                       missingType, missingType, missingType, missingType,
        //                       Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
        //                       missingType, missingType, missingType, missingType, missingType);
        //            objBook.Close(false, missingType, missingType);

        //            Cursor.Current = Cursors.Default;

        //            MessageBox.Show("저장했습니다!!!");     //성공했을경우 메세지박스

        //            //엑셀프로세스 종료 
        //            objBooks.Close();
        //            objApp.Quit();

        //            objSheets = null;
        //            objBooks = null;
        //            objApp = null;
        //        }
        //        catch (Exception theException)
        //        {
        //            String errorMessage;
        //            errorMessage = "Error: ";
        //            errorMessage = String.Concat(errorMessage, theException.Message);
        //            errorMessage = String.Concat(errorMessage, " Line: ");
        //            errorMessage = String.Concat(errorMessage, theException.Source);

        //            MessageBox.Show(errorMessage, "Error");
        //        }
        //        finally
        //        {
        //            // Clean up
        //            ReleaseExcelObject(objSheets);
        //            ReleaseExcelObject(objBooks);
        //            ReleaseExcelObject(objApp);
        //        }

        //        Process.Start(saveFileDialog.FileName);
        //    }
        //}


        //현재 사용안함.
		/// <summary>
		/// 엑셀출력. 특정 컬럼에 셀서식 지정
        /// 2013.06.20   그리드뷰에 Row가 없어도 HeaderText만 뿌려지도록 수정.
		/// </summary>
		/// <param name="captions"></param>
		/// <param name="dataGridView"></param>
		/// <param name="saveFileDialog"></param>
		/// <param name="filename">파일명</param>
		/// <param name="cellTypes">필드수량에 맞춰줘야됨</param>
        //public static void ExportExcel2(bool captions, DataGridView dataGridView, SaveFileDialog saveFileDialog, string filename, string[] cellTypes)
        //{
        //    //if (dataGridView.RowCount == 0)
        //    //{
        //    //    MessageBox.Show("조회된 데이터가 없어서 엑셀내보내기를 할 수 없습니다", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    //    return;
        //    //}


        //    saveFileDialog.FileName = filename;       //파일명
        //    saveFileDialog.DefaultExt = "xls";          //확장자
        //    saveFileDialog.Filter = "Excel files (*.xls)|*.xls";
        //    saveFileDialog.InitialDirectory = Environment.SpecialFolder.Recent.ToString();
        //    //saveFileDialog.InitialDirectory = "c:\\";       //경로

        //    DialogResult result = saveFileDialog.ShowDialog();      //저장다이얼로그

        //    if (result == DialogResult.OK)      //다이얼로그 OK일 경우
        //    {
        //        int num = 0;
        //        object missingType = Type.Missing;

        //        Excel.Application objApp = null;
        //        Excel._Workbook objBook = null;
        //        Excel.Workbooks objBooks = null;
        //        Excel.Sheets objSheets = null;
        //        Excel._Worksheet objSheet = null;
        //        Excel.Range range = null;

        //        string[] headers = new string[dataGridView.ColumnCount];
        //        string[] columns = new string[dataGridView.ColumnCount];

        //        //Excel파일에 헤더 뿌리기
        //        for (int c = 0; c < dataGridView.ColumnCount; c++)
        //        {
        //            if (dataGridView.Rows.Count == 0)           //그리드뷰에 Row가 없을 경우
        //            {
        //                headers[c] = dataGridView.Columns[c].HeaderText.ToString();         //컬럼에 있는 HeaderText값을 headers에 넣어줌
        //                num = c + 65;                                                   // 순차적으로 65=A, 66=B 엑셀의 위치를 Columns위치를 잡아줌
        //                columns[c] = Convert.ToString((char)num);                       // columns에 차례대로 넣음.
        //            }
        //            else            //Row가 없는데 else로 들어오면 Row[0]을 찾지못해서 에러남.
        //            {
        //                headers[c] = dataGridView.Rows[0].Cells[c].OwningColumn.HeaderText.ToString();  //컬럼에 있는 HeaderText값을 headers에 넣어줌
        //                num = c + 65;                                                                   // 순차적으로 65=A, 66=B 엑셀의 위치를 Columns위치를 잡아줌
        //                columns[c] = Convert.ToString((char)num);                                       // columns에 차례대로 넣음.
        //            }
        //        }

        //        try
        //        {
        //            objApp = new Excel.Application();
        //            objBooks = objApp.Workbooks;
        //            objBook = objBooks.Add(Missing.Value);
        //            objSheets = objBook.Worksheets;
        //            objSheet = (Excel._Worksheet)objSheets.get_Item(1);

        //            if (captions)   //Captions가 True일때
        //            {
        //                for (int c = 0; c < dataGridView.ColumnCount; c++) //캡션명
        //                {
        //                    range = objSheet.get_Range(columns[c] + "1", Missing.Value);
        //                    range.set_Value(Missing.Value, headers[c]);
        //                    range.Interior.Color = ColorTranslator.ToOle(Color.LightGray);
        //                    range.Borders.Color = ColorTranslator.ToOle(Color.Black);
        //                }
        //            }

        //            for (int i = 0; i < dataGridView.RowCount; i++)    //DataGirdView에 Row값
        //            {
        //                for (int j = 0; j < dataGridView.ColumnCount; j++)
        //                {
        //                    // Cell Merge가 있는지 확인
        //                    DataGridViewTextBoxCellEx cell = dataGridView.Rows[i].Cells[j] as DataGridViewTextBoxCellEx;
        //                    if (cell != null && cell.ColumnSpan > 1)                    //Merge가 있을 경우
        //                    {
        //                        string from = columns[j] + Convert.ToString(i + 2);
        //                        string to = Convert.ToString((char)((int)columns[j][columns[j].Length - 1] + cell.ColumnSpan - 1)) + Convert.ToString(i + 2);
        //                        range = objSheet.get_Range(from, to);
        //                        range.Merge(true);
        //                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

        //                        if (dataGridView.Rows[i].Cells[j].Value != null)
        //                        {
        //                            range.set_Value(Missing.Value, dataGridView.Rows[i].Cells[j].FormattedValue.ToString());    //엑셀에 값 세팅
        //                            SetColorAndFontStyle(range, dataGridView.Rows[i].Cells[j]);
        //                        }
        //                        j += cell.ColumnSpan - 1;
        //                    }
        //                    else        //Merge가 없을경우
        //                    {
        //                        if (dataGridView.Rows[i].Cells[j].Value != null)        
        //                        {
        //                            range = objSheet.get_Range(columns[j] + Convert.ToString(i + 2), Missing.Value);

        //                            //range.NumberFormat = @"@"; //셀서식 - 텍스트
        //                            //range.NumberFormat = @"mm-dd-yyyy"; //셀서식 - 날짜형식
        //                            //range.NumberFormat = @"###.#0"; //셀서식 - 숫자형식
        //                            if ("TEXT".Equals(cellTypes[j]))
        //                                range.NumberFormatLocal = @"@";
        //                            range.set_Value(Missing.Value, dataGridView.Rows[i].Cells[j].FormattedValue.ToString());        //엑셀에 값 세팅
        //                            SetColorAndFontStyle(range, dataGridView.Rows[i].Cells[j]);
        //                        }
        //                    }
        //                }
        //            }

        //            objApp.Visible = false;
        //            objApp.UserControl = false;

        //            objBook.SaveAs(@saveFileDialog.FileName,
        //                       Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal,
        //                       missingType, missingType, missingType, missingType,
        //                       Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
        //                       missingType, missingType, missingType, missingType, missingType);
        //            objBook.Close(false, missingType, missingType);

        //            Cursor.Current = Cursors.Default;

        //            MessageBox.Show("저장했습니다!!!");     //성공했을경우 메세지박스

        //            //엑셀프로세스 종료 
        //            objBooks.Close();
        //            objApp.Quit();

        //            objSheets = null;
        //            objBooks = null;
        //            objApp = null;
        //        }
        //        catch (Exception theException)
        //        {
        //            String errorMessage;
        //            errorMessage = "Error: ";
        //            errorMessage = String.Concat(errorMessage, theException.Message);
        //            errorMessage = String.Concat(errorMessage, " Line: ");
        //            errorMessage = String.Concat(errorMessage, theException.Source);

        //            MessageBox.Show(errorMessage, "Error");
        //        }
        //        finally
        //        {
        //            // Clean up
        //            ReleaseExcelObject(objSheets);
        //            ReleaseExcelObject(objBooks);
        //            ReleaseExcelObject(objApp);
        //        }

        //        Process.Start(saveFileDialog.FileName);
        //    }
        //}


        //현재사용안함
        /// <summary>
        /// 엑셀출력. 특정 컬럼에 셀서식 지정
        /// 2013.06.20   그리드뷰에 Row가 없어도 HeaderText만 뿌려지도록 수정.
        /// 2013.07.01   그리드뷰에 Row가 없으면 상단 그리드의 지시번호 등등 입력되게 수정
        /// </summary>
        /// <param name="captions"></param>
        /// <param name="dataGridView"></param>
        /// <param name="saveFileDialog"></param>
        /// <param name="filename">파일명</param>
        /// <param name="cellTypes">필드수량에 맞춰줘야됨</param>
        /// <param name="Item">엑셀에 값 세팅을 위한 리스트./param>
        //public static void ExportExcel2(bool captions, DataGridView dataGridView, SaveFileDialog saveFileDialog, string filename, string[] cellTypes, List<string> Item)
        //{
        //    saveFileDialog.FileName = filename;       //파일명
        //    saveFileDialog.DefaultExt = "xls";          //확장자
        //    saveFileDialog.Filter = "Excel files (*.xls)|*.xls";
        //    saveFileDialog.InitialDirectory = Environment.SpecialFolder.Recent.ToString();
        //    //saveFileDialog.InitialDirectory = "c:\\";       //경로

        //    DialogResult result = saveFileDialog.ShowDialog();      //저장다이얼로그

        //    if (result == DialogResult.OK)      //다이얼로그 OK일 경우
        //    {
        //        int num = 0;
        //        object missingType = Type.Missing;

        //        Excel.Application objApp = null;
        //        Excel._Workbook objBook = null;
        //        Excel.Workbooks objBooks = null;
        //        Excel.Sheets objSheets = null;
        //        Excel._Worksheet objSheet = null;
        //        Excel.Range range = null;

        //        string[] headers = new string[dataGridView.ColumnCount];
        //        string[] columns = new string[dataGridView.ColumnCount];

        //        //Excel파일에 헤더 뿌리기
        //        for (int c = 0; c < dataGridView.ColumnCount; c++)
        //        {
        //            if (dataGridView.Rows.Count == 0)           //그리드뷰에 Row가 없을 경우
        //            {
        //                headers[c] = dataGridView.Columns[c].HeaderText.ToString();         //컬럼에 있는 HeaderText값을 headers에 넣어줌
        //                num = c + 65;                                                   // 순차적으로 65=A, 66=B 엑셀의 위치를 Columns위치를 잡아줌
        //                columns[c] = Convert.ToString((char)num);                       // columns에 차례대로 넣음.
        //            }
        //            else            //Row가 없는데 else로 들어오면 Row[0]을 찾지못해서 에러남.
        //            {
        //                headers[c] = dataGridView.Rows[0].Cells[c].OwningColumn.HeaderText.ToString();  //컬럼에 있는 HeaderText값을 headers에 넣어줌
        //                num = c + 65;                                                                   // 순차적으로 65=A, 66=B 엑셀의 위치를 Columns위치를 잡아줌
        //                columns[c] = Convert.ToString((char)num);                                       // columns에 차례대로 넣음.
        //            }
        //        }

        //        try
        //        {
        //            objApp = new Excel.Application();
        //            objBooks = objApp.Workbooks;
        //            objBook = objBooks.Add(Missing.Value);
        //            objSheets = objBook.Worksheets;
        //            objSheet = (Excel._Worksheet)objSheets.get_Item(1);

        //            if (captions)   //Captions가 True일때
        //            {
        //                for (int c = 0; c < dataGridView.ColumnCount; c++) //캡션명
        //                {
        //                    range = objSheet.get_Range(columns[c] + "1", Missing.Value);
        //                    range.set_Value(Missing.Value, headers[c]);
        //                    range.Interior.Color = ColorTranslator.ToOle(Color.LightGray);
        //                    range.Borders.Color = ColorTranslator.ToOle(Color.Black);
        //                }
        //            }

        //            for (int i = 0; i < dataGridView.RowCount; i++)    //DataGirdView에 Row값
        //            {
        //                for (int j = 0; j < dataGridView.ColumnCount; j++)
        //                {
        //                    // Cell Merge가 있는지 확인
        //                    DataGridViewTextBoxCellEx cell = dataGridView.Rows[i].Cells[j] as DataGridViewTextBoxCellEx;
        //                    if (cell != null && cell.ColumnSpan > 1)                    //Merge가 있을 경우
        //                    {
        //                        string from = columns[j] + Convert.ToString(i + 2);
        //                        string to = Convert.ToString((char)((int)columns[j][columns[j].Length - 1] + cell.ColumnSpan - 1)) + Convert.ToString(i + 2);
        //                        range = objSheet.get_Range(from, to);
        //                        range.Merge(true);
        //                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

        //                        if (dataGridView.Rows[i].Cells[j].Value != null)
        //                        {
        //                            range.set_Value(Missing.Value, dataGridView.Rows[i].Cells[j].FormattedValue.ToString());    //엑셀에 값 세팅
        //                            SetColorAndFontStyle(range, dataGridView.Rows[i].Cells[j]);
        //                        }
        //                        j += cell.ColumnSpan - 1;
        //                    }
        //                    else
        //                    {
        //                        if (dataGridView.Rows[i].Cells[j].Value != null)                //Merge가 없을경우
        //                        {
        //                            range = objSheet.get_Range(columns[j] + Convert.ToString(i + 2), Missing.Value);

        //                            //range.NumberFormat = @"@"; //셀서식 - 텍스트
        //                            //range.NumberFormat = @"mm-dd-yyyy"; //셀서식 - 날짜형식
        //                            //range.NumberFormat = @"###.#0"; //셀서식 - 숫자형식
        //                            if ("TEXT".Equals(cellTypes[j]))
        //                                range.NumberFormatLocal = @"@";
        //                            range.set_Value(Missing.Value, dataGridView.Rows[i].Cells[j].FormattedValue.ToString());        //엑셀에 값 세팅
        //                            SetColorAndFontStyle(range, dataGridView.Rows[i].Cells[j]);
        //                        }
        //                    }
        //                }
        //            }

        //            if (dataGridView.RowCount == 0)
        //            {
        //                for (int j = 0; j < dataGridView.ColumnCount; j++)
        //                {
        //                    range = objSheet.get_Range(columns[j] + Convert.ToString(0 + 2), Missing.Value);

        //                    //range.NumberFormat = @"@"; //셀서식 - 텍스트
        //                    //range.NumberFormat = @"mm-dd-yyyy"; //셀서식 - 날짜형식
        //                    //range.NumberFormat = @"###.#0"; //셀서식 - 숫자형식
        //                    if ("TEXT".Equals(cellTypes[j]))
        //                        range.NumberFormatLocal = @"@";
        //                    range.set_Value(Missing.Value, Item[j]);        //엑셀에 값 세팅
        //                    //SetColorAndFontStyle(range, dataGridView.Rows[0].Cells[j]);
        //                }
        //            }



        //            objApp.Visible = false;
        //            objApp.UserControl = false;

        //            objBook.SaveAs(@saveFileDialog.FileName,
        //                       Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal,
        //                       missingType, missingType, missingType, missingType,
        //                       Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
        //                       missingType, missingType, missingType, missingType, missingType);
        //            objBook.Close(false, missingType, missingType);

        //            Cursor.Current = Cursors.Default;

        //            MessageBox.Show("저장했습니다!!!");     //성공했을경우 메세지박스

        //            //엑셀프로세스 종료 
        //            objBooks.Close();
        //            objApp.Quit();

        //            objSheets = null;
        //            objBooks = null;
        //            objApp = null;
        //        }
        //        catch (Exception theException)
        //        {
        //            String errorMessage;
        //            errorMessage = "Error: ";
        //            errorMessage = String.Concat(errorMessage, theException.Message);
        //            errorMessage = String.Concat(errorMessage, " Line: ");
        //            errorMessage = String.Concat(errorMessage, theException.Source);

        //            MessageBox.Show(errorMessage, "Error");
        //        }
        //        finally
        //        {
        //            // Clean up
        //            ReleaseExcelObject(objSheets);
        //            ReleaseExcelObject(objBooks);
        //            ReleaseExcelObject(objApp);
        //        }

        //        Process.Start(saveFileDialog.FileName);
        //    }
        //}
        #endregion


        /// <summary>
        /// 엑셀출력. 특정 컬럼에 셀서식 지정
        /// 2013.06.20   그리드뷰에 Row가 없어도 HeaderText만 뿌려지도록 수정.
        /// 2013.07.01   그리드뷰에 Row가 없으면 상단 그리드의 지시번호 등등 입력되게 수정
        /// 2013.07.04   엑셀 속도개선. 루프돌아서 하나씩 값을 입력하는방식에서 한번에 배열로 넣는방식으로 변경.
        /// </summary>
        /// <param name="captions"></param>
        /// <param name="dataGridView"></param>
        /// <param name="saveFileDialog"></param>
        /// <param name="filename">파일명</param>
        /// <param name="cellTypes">필드수량에 맞춰줘야됨</param>
        /// <param name="Item">엑셀에 값 세팅을 위한 리스트./param>
        public static void ExportExcel4(bool captions, DataGridView dataGridView, SaveFileDialog saveFileDialog, string filename, string[] cellTypes, List<string> Item)
        {
            saveFileDialog.FileName = filename;       //파일명
            saveFileDialog.DefaultExt = "xls";          //확장자
            saveFileDialog.Filter = "Excel files (*.xls)|*.xls";
            saveFileDialog.InitialDirectory = Environment.SpecialFolder.Recent.ToString();
            //saveFileDialog.InitialDirectory = "c:\\";       //경로

            DialogResult result = saveFileDialog.ShowDialog();      //저장다이얼로그

            if (result == DialogResult.OK)      //다이얼로그 OK일 경우
            {
                int num = 0;
                object missingType = Type.Missing;

                Excel.Application objApp = null;
                Excel._Workbook objBook = null;
                Excel.Workbooks objBooks = null;
                Excel.Sheets objSheets = null;
                Excel._Worksheet objSheet = null;
                Excel.Range range = null;

                string[] headers = new string[dataGridView.ColumnCount];
                string[] columns = new string[dataGridView.ColumnCount];

                //Excel파일에 헤더 뿌리기
                for (int c = 0; c < dataGridView.ColumnCount; c++)
                {
                    if (dataGridView.Rows.Count == 0)           //그리드뷰에 Row가 없을 경우
                    {
                        headers[c] = dataGridView.Columns[c].HeaderText.ToString();         //컬럼에 있는 HeaderText값을 headers에 넣어줌
                        num = c + 65;                                                   // 순차적으로 65=A, 66=B 엑셀의 위치를 Columns위치를 잡아줌
                        columns[c] = Convert.ToString((char)num);                       // columns에 차례대로 넣음.
                    }
                    else if (c > 25)        //Columns가 엑셀에서 25자리가 A~Z까지고 26부터는 AA~AZ라서
                    {
                        headers[c] = dataGridView.Rows[0].Cells[c].OwningColumn.HeaderText.ToString();
                        columns[c] = Convert.ToString((char)(Convert.ToInt32(c / 26) - 1 + 65)) + Convert.ToString((char)(c % 26 + 65));
                    }
                    else            //Row가 없는데 else로 들어오면 Row[0]을 찾지못해서 에러남.
                    {
                        headers[c] = dataGridView.Rows[0].Cells[c].OwningColumn.HeaderText.ToString();  //컬럼에 있는 HeaderText값을 headers에 넣어줌
                        num = c + 65;                                                                   // 순차적으로 65=A, 66=B 엑셀의 위치를 Columns위치를 잡아줌
                        columns[c] = Convert.ToString((char)num);                                       // columns에 차례대로 넣음.
                    }
                }
                
                

                try
                {
                    objApp = new Excel.Application();
                    objBooks = objApp.Workbooks;
                    objBook = objBooks.Add(Missing.Value);
                    objSheets = objBook.Worksheets;
                    objSheet = (Excel._Worksheet)objSheets.get_Item(1);

                    if (captions)   //Captions가 True일때
                    {
                        for (int c = 0; c < dataGridView.ColumnCount; c++) //캡션명
                        {
                            range = objSheet.get_Range(columns[c] + "1", Missing.Value);
                            range.set_Value(Missing.Value, headers[c]);
                            range.Interior.Color = ColorTranslator.ToOle(Color.LightGray);
                            range.Borders.Color = ColorTranslator.ToOle(Color.Black);
                        }
                    }

                    //엑셀에 넣을 배열 생성 **** 추가
                    object[,] saNames = new object[dataGridView.RowCount, dataGridView.ColumnCount];

                    //DataGridView의 타입저장**** 추가
                    string tp;
                    for (int i = 0; i < dataGridView.RowCount; i++)    //DataGirdView에 Row값
                    {
                        for (int j = 0; j < dataGridView.ColumnCount; j++)
                        {

                            //Cell 타입 가져오기**** 추가
                            tp = dataGridView.Rows[i].Cells[j].ValueType.Name;
                            // Cell Merge가 있는지 확인
                            DataGridViewTextBoxCellEx cell = dataGridView.Rows[i].Cells[j] as DataGridViewTextBoxCellEx;
                            if (cell != null && cell.ColumnSpan > 1)                    //Merge가 있을 경우
                            {
                                string from = columns[j] + Convert.ToString(i + 2);
                                string to = Convert.ToString((char)((int)columns[j][columns[j].Length - 1] + cell.ColumnSpan - 1)) + Convert.ToString(i + 2);
                                range = objSheet.get_Range(from, to);
                                range.Merge(true);
                                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                if (dataGridView.Rows[i].Cells[j].Value != null)
                                {
                                    //배열에 그리드값 가져와 대입**** 추가
                                    if (tp == "String") //2000-01-01 형태의 날짜 필터하기 위함(숫자로 변환 방지)
                                        saNames[i, j] = "'" + dataGridView.Rows[i].Cells[j].FormattedValue.ToString();
                                    else
                                        saNames[i, j] = dataGridView.Rows[i].Cells[j].Value;
                                }
                                j += cell.ColumnSpan - 1;
                            }
                            else        //Merge가 없을경우
                            {
                                //배열에 그리드값 가져와 대입**** 추가
                                if (tp == "String") //2000-01-01 형태의 날짜 필터하기 위함(숫자로 변환 방지)
                                    saNames[i, j] = "'" + dataGridView.Rows[i].Cells[j].Value.ToString();
                                else
                                    saNames[i, j] = dataGridView.Rows[i].Cells[j].Value;
                            }
                        }
                    }

                    //그리드뷰에 Row가 없을경우 원하는 값 세팅
                    if (dataGridView.RowCount == 0)
                    {
                        for (int j = 0; j < dataGridView.ColumnCount; j++)
                        {
                            range = objSheet.get_Range(columns[j] + Convert.ToString(0 + 2), Missing.Value);

                            //range.NumberFormat = @"@"; //셀서식 - 텍스트
                            //range.NumberFormat = @"mm-dd-yyyy"; //셀서식 - 날짜형식
                            //range.NumberFormat = @"###.#0"; //셀서식 - 숫자형식
                            if ("TEXT".Equals(cellTypes[j]))
                                range.NumberFormatLocal = @"@";
                            range.set_Value(Missing.Value, Item[j]);        //엑셀에 값 세팅
                        }
                    }

                    //해당 시트에 Range를 가져 와서 Value2에 한번에 밀어 넣는다   **** 추가
                    //2번째줄부터, 가로길이 + 세로길이 .Value2 = 배열에 담긴 값 한번에 밀어넣기
                    objSheet.get_Range(columns[0] + "2", columns[columns.Length - 1] + (dataGridView.RowCount + 1)).Value2 = saNames;
                                                   
                    objApp.Visible = false;
                    objApp.UserControl = false;

                    objBook.SaveAs(@saveFileDialog.FileName,
                               Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal,
                               missingType, missingType, missingType, missingType,
                               Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                               missingType, missingType, missingType, missingType, missingType);
                    objBook.Close(false, missingType, missingType);

                    Cursor.Current = Cursors.Default;

                    MessageBox.Show("저장했습니다!!!");     //성공했을경우 메세지박스

                    //엑셀프로세스 종료 
                    objBooks.Close();
                    objApp.Quit();

                    objSheets = null;
                    objBooks = null;
                    objApp = null;
                }
                catch (Exception theException)
                {
                    String errorMessage;
                    errorMessage = "Error: ";
                    errorMessage = String.Concat(errorMessage, theException.Message);
                    errorMessage = String.Concat(errorMessage, " Line: ");
                    errorMessage = String.Concat(errorMessage, theException.Source);

                    MessageBox.Show(errorMessage, "Error");
                }
                finally
                {
                    // Clean up
                    ReleaseExcelObject(objSheets);
                    ReleaseExcelObject(objBooks);
                    ReleaseExcelObject(objApp);
                }

                Process.Start(saveFileDialog.FileName);
            }
        }



        #region 엑셀 사용안함. 참고용
        //일반 조회 그리드에서 사용하던 엑셀 내보내기 셀을 하나씩 대입하는 함수. 느림.(현재 사용안함)참고용.
		/// <summary>
		/// 참조: Microsoft.Office.Interop.Excel 추가
		/// Methods.cs 소스 최상단에 Excel = Microsoft.Office.Interop.Excel 추가
		/// 사용법 : 그리드뷰에 태그이름으로 ListBox가 입력됨
		/// List<string> list = new List<string>();
		///    list.Add(Convert.ToString(this.p_dgv_Notice.Tag));
		/// Frm_GridViewSelect mSub = new Frm_GridViewSelect(list);
		/// DialogResult result = mSub.ShowDialog();
		/// if (result == DialogResult.OK) 
		///         mSub.Value으로 리턴 됨.
		/// </summary>
		/// <param name="captions">캡션명 true일때 Excel에 찍힘</param>
		/// <param name="dataGridView">선택 혹은 특정 DataGridView를 넘겨주면 그 DataGridView 출력</param>
		/// <param name="saveFileDialog">저장SaveFileDialog</param>
        //public static void ExportExcel(bool captions, DataGridView dataGridView, SaveFileDialog saveFileDialog, string filename)
        //{
        //    saveFileDialog.FileName = filename;       //파일명
        //    saveFileDialog.DefaultExt = "xls";          //확장자
        //    saveFileDialog.Filter = "Excel files (*.xls)|*.xls";
        //    saveFileDialog.InitialDirectory = Environment.SpecialFolder.Recent.ToString();
        //    //saveFileDialog.InitialDirectory = "c:\\";       //경로

        //    DialogResult result = saveFileDialog.ShowDialog();      //저장다이얼로그

        //    if (result == DialogResult.OK)      //다이얼로그 OK일 경우
        //    {
        //        int num = 0;
        //        object missingType = Type.Missing;

        //        Excel.Application objApp = null;
        //        Excel._Workbook objBook = null;
        //        Excel.Workbooks objBooks = null;
        //        Excel.Sheets objSheets = null;
        //        Excel._Worksheet objSheet = null;
        //        Excel.Range range = null;

        //        string[] headers = new string[dataGridView.ColumnCount];
        //        string[] columns = new string[dataGridView.ColumnCount];

        //        for (int c = 0; c < dataGridView.ColumnCount; c++)
        //        {
        //            if (dataGridView.Rows.Count == 0)           //그리드뷰에 Row가 없을 경우
        //            {
        //                headers[c] = dataGridView.Columns[c].HeaderText.ToString();         //컬럼에 있는 HeaderText값을 headers에 넣어줌
        //                num = c + 65;                                                   // 순차적으로 65=A, 66=B 엑셀의 위치를 Columns위치를 잡아줌
        //                columns[c] = Convert.ToString((char)num);                       // columns에 차례대로 넣음.
        //            }
        //            else            //Row가 없는데 else로 들어오면 Row[0]을 찾지못해서 에러남.
        //            {
        //                headers[c] = dataGridView.Rows[0].Cells[c].OwningColumn.HeaderText.ToString();
        //                num = c + 65;
        //                columns[c] = Convert.ToString((char)num);
        //            }
        //        }

        //        try
        //        {
        //            objApp = new Excel.Application();
        //            objBooks = objApp.Workbooks;
        //            objBook = objBooks.Add(Missing.Value);
        //            objSheets = objBook.Worksheets;
        //            objSheet = (Excel._Worksheet)objSheets.get_Item(1);

        //            if (captions)   //Captions가 True일때
        //            {
        //                for (int c = 0; c < dataGridView.ColumnCount; c++) //캡션명
        //                {
        //                    range = objSheet.get_Range(columns[c] + "1", Missing.Value);
        //                    range.set_Value(Missing.Value, headers[c]);
        //                    range.Interior.Color = ColorTranslator.ToOle(Color.LightGray);
        //                    range.Borders.Color = ColorTranslator.ToOle(Color.Black);
        //                }
        //            }


        //            for (int i = 0; i < dataGridView.RowCount; i++)    //DataGirdView에 Row값
        //            {
        //                for (int j = 0; j < dataGridView.ColumnCount; j++)
        //                {
        //                    // Cell Merge가 있는지 확인
        //                    DataGridViewTextBoxCellEx cell = dataGridView.Rows[i].Cells[j] as DataGridViewTextBoxCellEx;
        //                    if (cell != null && cell.ColumnSpan > 1)
        //                    {
        //                        string from = columns[j] + Convert.ToString(i + 2);
        //                        string to = Convert.ToString((char)((int)columns[j][columns[j].Length - 1] + cell.ColumnSpan - 1)) + Convert.ToString(i + 2);
        //                        range = objSheet.get_Range(from, to);
        //                        range.Merge(true);
        //                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

        //                        if (dataGridView.Rows[i].Cells[j].Value != null)
        //                        {
        //                            range.set_Value(Missing.Value, dataGridView.Rows[i].Cells[j].FormattedValue.ToString());
        //                            SetColorAndFontStyle(range, dataGridView.Rows[i].Cells[j]);
        //                        }
        //                        j += cell.ColumnSpan - 1;
        //                    }
        //                    else
        //                    {
        //                        if (dataGridView.Rows[i].Cells[j].Value != null)
        //                        {
        //                            range = objSheet.get_Range(columns[j] + Convert.ToString(i + 2), Missing.Value);
        //                            range.set_Value(Missing.Value, dataGridView.Rows[i].Cells[j].FormattedValue.ToString());
        //                            SetColorAndFontStyle(range, dataGridView.Rows[i].Cells[j]);
        //                        }
        //                    }
        //                }
        //            }

        //            objApp.Visible = false;
        //            objApp.UserControl = false;

        //            objBook.SaveAs(@saveFileDialog.FileName,
        //                       Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal,
        //                       missingType, missingType, missingType, missingType,
        //                       Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
        //                       missingType, missingType, missingType, missingType, missingType);
        //            objBook.Close(false, missingType, missingType);

        //            Cursor.Current = Cursors.Default;

        //            MessageBox.Show("Save Success!!!");     //성공했을경우 메세지박스

        //            //엑셀프로세스 종료 
        //            objBooks.Close();
        //            objApp.Quit();

        //            objSheets = null;
        //            objBooks = null;
        //            objApp = null;
        //        }
        //        catch (Exception theException)
        //        {
        //            String errorMessage;
        //            errorMessage = "Error: ";
        //            errorMessage = String.Concat(errorMessage, theException.Message);
        //            errorMessage = String.Concat(errorMessage, " Line: ");
        //            errorMessage = String.Concat(errorMessage, theException.Source);

        //            MessageBox.Show(errorMessage, "Error");
        //        }
        //        finally
        //        {
        //            // Clean up
        //            ReleaseExcelObject(objSheets);
        //            ReleaseExcelObject(objBooks);
        //            ReleaseExcelObject(objApp);
        //        }

        //        Process.Start(saveFileDialog.FileName);
        //    }
        //}
        #endregion


        /// <summary>
        /// 참조: Microsoft.Office.Interop.Excel 추가
        /// Methods.cs 소스 최상단에 Excel = Microsoft.Office.Interop.Excel 추가
        /// 사용법 : 그리드뷰에 태그이름으로 ListBox가 입력됨
        /// List<string> list = new List<string>();
        ///    list.Add(Convert.ToString(this.p_dgv_Notice.Tag));
        /// Frm_GridViewSelect mSub = new Frm_GridViewSelect(list);
        /// DialogResult result = mSub.ShowDialog();
        /// if (result == DialogResult.OK) 
        ///         mSub.Value으로 리턴 됨.
        /// </summary>
        /// <param name="captions">캡션명 true일때 Excel에 찍힘</param>
        /// <param name="dataGridView">선택 혹은 특정 DataGridView를 넘겨주면 그 DataGridView 출력</param>
        /// <param name="saveFileDialog">저장SaveFileDialog</param>
        /// //기존 셀하나씩 값을 대입하는방식에서 한번에 넣는 방식으로 속도개선.
        public static void ExportExcel(bool captions, DataGridView dataGridView, SaveFileDialog saveFileDialog, string filename)
        {
            saveFileDialog.FileName = filename;       //파일명
            saveFileDialog.DefaultExt = "xls";          //확장자
            saveFileDialog.Filter = "Excel files (*.xls)|*.xls";
            saveFileDialog.InitialDirectory = Environment.SpecialFolder.Recent.ToString();
            //saveFileDialog.InitialDirectory = "c:\\";       //경로

            DialogResult result = saveFileDialog.ShowDialog();      //저장다이얼로그

            if (result == DialogResult.OK)      //다이얼로그 OK일 경우
            {
                int num = 0;
                object missingType = Type.Missing;

                Excel.Application objApp = null;
                Excel._Workbook objBook = null;
                Excel.Workbooks objBooks = null;
                Excel.Sheets objSheets = null;
                Excel._Worksheet objSheet = null;
                Excel.Range range = null;

                string[] headers = new string[dataGridView.ColumnCount];
                string[] columns = new string[dataGridView.ColumnCount];

                for (int c = 0; c < dataGridView.ColumnCount; c++)
                {
                    if (dataGridView.Rows.Count == 0)           //그리드뷰에 Row가 없을 경우
                    {
                        headers[c] = dataGridView.Columns[c].HeaderText.ToString();         //컬럼에 있는 HeaderText값을 headers에 넣어줌
                        num = c + 65;                                                   // 순차적으로 65=A, 66=B 엑셀의 위치를 Columns위치를 잡아줌
                        columns[c] = Convert.ToString((char)num);                       // columns에 차례대로 넣음.
                    }
                    else if (c > 25)        //Columns가 엑셀에서 25자리가 A~Z까지고 26부터는 AA~AZ라서
                    {
                        headers[c] = dataGridView.Rows[0].Cells[c].OwningColumn.HeaderText.ToString();
                        columns[c] = Convert.ToString((char)(Convert.ToInt32(c / 26) - 1 + 65)) + Convert.ToString((char)(c % 26 + 65));
                    }
                    else            //Row가 없는데 else로 들어오면 Row[0]을 찾지못해서 에러남.
                    {
                        headers[c] = dataGridView.Rows[0].Cells[c].OwningColumn.HeaderText.ToString();
                        num = c + 65;
                        columns[c] = Convert.ToString((char)num);
                    }
                }

                try
                {
                    objApp = new Excel.Application();
                    objBooks = objApp.Workbooks;
                    objBook = objBooks.Add(Missing.Value);
                    objSheets = objBook.Worksheets;
                    objSheet = (Excel._Worksheet)objSheets.get_Item(1);

                    if (captions)   //Captions가 True일때
                    {
                        for (int c = 0; c < dataGridView.ColumnCount; c++) //캡션명
                        {
                            range = objSheet.get_Range(columns[c] + "1", Missing.Value);
                            range.set_Value(Missing.Value, headers[c]);
                            range.Interior.Color = ColorTranslator.ToOle(Color.LightGray);
                            range.Borders.Color = ColorTranslator.ToOle(Color.Black);
                        }
                    }


                    //엑셀에 넣을 배열 생성 **** 추가
                    object[,] saNames = new object[dataGridView.RowCount, dataGridView.ColumnCount];

                    //DataGridView의 타입저장**** 추가
                    string tp;
                    for (int i = 0; i < dataGridView.RowCount; i++)    //DataGirdView에 Row값
                    {
                        for (int j = 0; j < dataGridView.ColumnCount; j++)
                        {

                            //Cell 타입 가져오기**** 추가
                            tp = dataGridView.Rows[i].Cells[j].ValueType.Name;
                             //Cell Merge가 있는지 확인
                            DataGridViewTextBoxCellEx cell = dataGridView.Rows[i].Cells[j] as DataGridViewTextBoxCellEx;
                            if (cell != null && cell.ColumnSpan > 1)                    //Merge가 있을 경우
                            {
                                string from = columns[j] + Convert.ToString(i + 2);
                                string to = Convert.ToString((char)((int)columns[j][columns[j].Length - 1] + cell.ColumnSpan - 1)) + Convert.ToString(i + 2);
                                range = objSheet.get_Range(from, to);
                                range.Merge(true);
                                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                if (dataGridView.Rows[i].Cells[j].Value != null)
                                {
                                    if (tp == "String") //2000-01-01 형태의 날짜 필터하기 위함(숫자로 변환 방지)
                                     saNames[i, j] = "'" + dataGridView.Rows[i].Cells[j].FormattedValue.ToString();
                                    else
                                        saNames[i, j] = "'" + dataGridView.Rows[i].Cells[j].Value;
                                    //range.set_Value(Missing.Value, dataGridView.Rows[i].Cells[j].FormattedValue.ToString());    //엑셀에 값 세팅
                                    //SetColorAndFontStyle(range, dataGridView.Rows[i].Cells[j]);
                                }
                                j += cell.ColumnSpan - 1;
                            }
                            else        //Merge가 없을경우
                            {
                                //배열에 그리드값 가져와 대입**** 추가
                                if (tp == "String") //2000-01-01 형태의 날짜 필터하기 위함(숫자로 변환 방지)
                                    saNames[i, j] = "'" + dataGridView.Rows[i].Cells[j].Value.ToString();
                                else
                                    saNames[i, j] = dataGridView.Rows[i].Cells[j].Value;
                            }
                        }
                    }

                    //해당 시트에 Range를 가져 와서 Value2에 한번에 밀어 넣는다   **** 추가
                    //2번째줄부터, 가로길이 + 세로길이 .Value2 = 배열에 담긴 값 한번에 밀어넣기
                    objSheet.get_Range(columns[0] + "2", columns[columns.Length - 1] + (dataGridView.RowCount + 1)).Value2 = saNames;


                    objApp.Visible = false;
                    objApp.UserControl = false;

                    objBook.SaveAs(@saveFileDialog.FileName,
                               Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal,
                               missingType, missingType, missingType, missingType,
                               Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                               missingType, missingType, missingType, missingType, missingType);
                    objBook.Close(false, missingType, missingType);

                    Cursor.Current = Cursors.Default;

                    MessageBox.Show("Save Success!!!");     //성공했을경우 메세지박스

                    //엑셀프로세스 종료 
                    objBooks.Close();
                    objApp.Quit();

                    objSheets = null;
                    objBooks = null;
                    objApp = null;
                }
                catch (Exception theException)
                {
                    String errorMessage;
                    errorMessage = "Error: ";
                    errorMessage = String.Concat(errorMessage, theException.Message);
                    errorMessage = String.Concat(errorMessage, " Line: ");
                    errorMessage = String.Concat(errorMessage, theException.Source);

                    MessageBox.Show(errorMessage, "Error");
                }
                finally
                {
                    // Clean up
                    ReleaseExcelObject(objSheets);
                    ReleaseExcelObject(objBooks);
                    ReleaseExcelObject(objApp);
                }

                Process.Start(saveFileDialog.FileName);
            }
        }

		/// <summary>
		/// Excel 파일을 읽어들인다.
		/// </summary>
		/// <param name="openFileDialog"></param>
		/// <returns></returns>
		public static List<Array> ImportExcel(OpenFileDialog openFileDialog)
		{
#if false //x64에서 정상동작하지 않음
            // OLEDB를 이용한 엑셀 연결
            DataTable dt = null;

            openFileDialog.FileName = "Excel";       //파일명
            openFileDialog.DefaultExt = "xls";          //확장자
            openFileDialog.Filter = "Excel files (*.xls)|*.xls";
            openFileDialog.InitialDirectory = Environment.SpecialFolder.Recent.ToString();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string szConn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=No'", openFileDialog.FileName); ;
                OleDbConnection conn = new OleDbConnection(szConn);
                conn.Open();

                // 엑셀로부터 데이타 읽기
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", conn);
                OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);

                dt = ds.Tables[0];
            }

            return dt;
#else
			List<Array> result = new List<Array>();

			Excel.Application excelApp = null;
			Excel.Workbook theWorkbook = null;
			Excel.Worksheet worksheet = null;

			openFileDialog.FileName = "Excel";       //파일명
			openFileDialog.DefaultExt = "xls";          //확장자
			openFileDialog.Filter = "Excel files (*.xls)|*.xls";
			openFileDialog.InitialDirectory = Environment.SpecialFolder.Recent.ToString();


			try
			{
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					excelApp = new Excel.Application();

					theWorkbook = excelApp.Workbooks.Open(openFileDialog.FileName, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, false, Excel.XlCorruptLoad.xlNormalLoad);
					Excel.Sheets sheets = theWorkbook.Worksheets;
					worksheet = (Excel.Worksheet)sheets.get_Item(1);

					//데이터가 있는 칼럼을 찾는다.
					Excel.Range range = worksheet.get_Range("A1", "BZ1");
					System.Array myvalues = (System.Array)range.Cells.Value2;

					int colCount = 0;

					foreach (string item in myvalues)
					{
						if (string.IsNullOrEmpty(item) == false)
						{
							colCount++;
						}
						else
						{
							break;
						}
					}

					string toString = string.Empty;

					if ((colCount - 1) / 26 > 0)
					{
						toString = ((char)('A' + ((colCount - 1) / 26 - 1))).ToString();
					}
					toString += ((char)('A' + ((colCount - 1) % 26))).ToString();

					
                    // Data를 읽어와서 담는다.
                    // 엑셀에서 65535줄 만큼 읽어올수 있다.
					for (int i = 2; i <= 65535; i++)
					{
						range = worksheet.get_Range("A" + i.ToString(), toString + i.ToString());
						myvalues = (System.Array)range.Cells.Value2;

						bool isAllItemNull = true;

						foreach (object obj in myvalues)
						{
							if (obj != null)
							{
								isAllItemNull = false;
								break;
							}
						}

						if (isAllItemNull == true)
						{
							break;
						}
						else
						{
							result.Add(myvalues);
						}
					}

					excelApp.Quit();
				}

				return result;
			}
			finally
			{
				// Clean up
				ReleaseExcelObject(worksheet);
				ReleaseExcelObject(theWorkbook);
				ReleaseExcelObject(excelApp);
			}
#endif
		}

		/// <summary>
		/// Excel 처리후 정리
		/// </summary>
		/// <param name="obj"></param>
		private static void ReleaseExcelObject(object obj)
		{
			try
			{
				if (obj != null)
				{
					Marshal.ReleaseComObject(obj);
					obj = null;
				}
			}
			catch (Exception ex)
			{
				obj = null;
				throw ex;
			}
			finally
			{
				GC.Collect();
			}
		}

		//Cell Color 및 Font Color, Font Bold 여부 지정
		private static void SetColorAndFontStyle(Excel.Range range, DataGridViewCell cell)
		{
			Color oldColor = Color.White;

			if (cell.Style.BackColor.A > 0)
			{
				oldColor = cell.Style.BackColor;
			}

			Color newColor = Color.FromArgb(oldColor.A, oldColor.R, oldColor.G, oldColor.B);
			range.Interior.Color = ColorTranslator.ToOle(newColor);

			oldColor = Color.Black;
			if (cell.Style.ForeColor.A > 0)
			{
				oldColor = cell.Style.ForeColor;
			}

			newColor = Color.FromArgb(oldColor.A, oldColor.R, oldColor.G, oldColor.B);
			range.Font.Color = System.Drawing.ColorTranslator.ToOle(newColor);

			range.Borders.Color = ColorTranslator.ToOle(Color.Black);

			if (cell.Style.Font != null)
			{
				range.Font.Bold = cell.Style.Font.Bold;
			}
		}

		/// <summary>
		/// 사용자 Password를 암호화하는데 사용한다.
		/// </summary>
		/// <param name="Password"></param>
		/// <param name="UserName"></param>
		/// <returns></returns>
		public static string EncryptString(string dataToEncrypt, string key)
		{
			byte[] Results;
			System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

			// Step 1. We hash the passphrase using MD5
			// We use the MD5 hash generator as the result is a 128 bit byte array
			// which is a valid length for the TripleDES encoder we use below

			MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
			byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(key));

			// Step 2. Create a new TripleDESCryptoServiceProvider object
			TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

			// Step 3. Setup the encoder
			TDESAlgorithm.Key = TDESKey;
			TDESAlgorithm.Mode = CipherMode.ECB;
			TDESAlgorithm.Padding = PaddingMode.PKCS7;

			// Step 4. Convert the input string to a byte[]
			byte[] DataToEncrypt = UTF8.GetBytes(dataToEncrypt);

			// Step 5. Attempt to encrypt the string
			try
			{
				ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
				Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
			}
			finally
			{
				// Clear the TripleDes and Hashprovider services of any sensitive information
				TDESAlgorithm.Clear();
				HashProvider.Clear();
			}

			// Step 6. Return the encrypted string as a base64 encoded string
			return Convert.ToBase64String(Results);
		}

		/// <summary>
		/// 사용자 Password를 복호화하는데 사용한다.
		/// </summary>
		/// <param name="EncryptedString"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string DecryptString(string EncryptedString, string key)
		{
			byte[] Results;
			System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

			// Step 1. We hash the passphrase using MD5
			// We use the MD5 hash generator as the result is a 128 bit byte array
			// which is a valid length for the TripleDES encoder we use below

			MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
			byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(key));

			// Step 2. Create a new TripleDESCryptoServiceProvider object
			TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

			// Step 3. Setup the decoder
			TDESAlgorithm.Key = TDESKey;
			TDESAlgorithm.Mode = CipherMode.ECB;
			TDESAlgorithm.Padding = PaddingMode.PKCS7;

			// Step 4. Convert the input string to a byte[]
			byte[] DataToDecrypt = Convert.FromBase64String(EncryptedString);

			// Step 5. Attempt to decrypt the string
			try
			{
				ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
				Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
			}
			finally
			{
				// Clear the TripleDes and Hashprovider services of any sensitive information
				TDESAlgorithm.Clear();
				HashProvider.Clear();
			}

			// Step 6. Return the decrypted string in UTF8 format
			return UTF8.GetString(Results);
		}

		private const int LEN_PRODID_NNK = 24;
		private const string DIVCODE_NNK = "N01";

		/// <summary>
		/// 나노켐 ProdID인지 확인한다.
		/// </summary>
		/// <param name="prodID"></param>
		/// <returns></returns>
		public static bool IsNanokemProdID(string prodID)
		{
			bool result = false;

			if (prodID.Length == LEN_PRODID_NNK)
			{
				if (prodID.Substring(16, 3).Equals(DIVCODE_NNK) == true)
				{
					result = true;
				}
			}
			else if (prodID.Length > LEN_PRODID_NNK)
			{
				if (prodID.Substring(27, 3).Equals(DIVCODE_NNK) == true)
				{
					result = true;
				}
			}

			return result;
		}

		/// <summary>
		/// 나노켐 ProdIDFull을 ProdID로 변경한다.
		/// </summary>
		/// <param name="prodIDFull"></param>
		/// <returns></returns>
		public static string ConvertNanokemProdIDFullToProdID(string prodIDFull)
		{
			string[] tempProdID = prodIDFull.Split('-');
			StringBuilder sbProdIDNNK = new StringBuilder();

			for (int i = 1; i < 5; i++)
			{
				sbProdIDNNK.Append(tempProdID[i].Trim());
			}

			return sbProdIDNNK.ToString();
		}

		/// <summary>
		/// DataGridViewRow에 해당되는 Binding된 DataRow를 구한다.
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		public static System.Data.DataRow GetDataRow(DataGridViewRow row)
		{
			if (row == null || row.DataBoundItem == null)
			{
				return null;
			}
			else
			{
				return ((System.Data.DataRowView)(row.DataBoundItem)).Row;
			}
		}

		/// <summary>
		/// DataRow에 해당되는 DataGridViewRow를 반환한다.
		/// </summary>
		/// <param name="dr"></param>
		/// <param name="dgv"></param>
		/// <returns></returns>
		public static DataGridViewRow GetDataGridViewRowOfDataRow(System.Data.DataRow dr, DataGridView dgv)
		{
			DataGridViewRow theRow = null;

			foreach (DataGridViewRow row in dgv.Rows)
			{
				if (dr.Equals(GetDataRow(row)) == true)
				{
					theRow = row;
				}
			}

			return theRow;
		}

		/// <summary>
		/// DataTable의 Data를 String으로 변환
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static string GetDataString(object data)
		{
			if (data == System.DBNull.Value)
			{
				return string.Empty;
			}
			else
			{
				if (data.GetType() == typeof(string))
				{
					return data as string;
				}
				else
				{
					return data.ToString();
				}
			}
		}

		/// <summary>
		/// DataTable의 Data를 YN으로 변환
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static string GetDataStringYN(object data)
		{
			if (data == System.DBNull.Value)
			{
				return "N";
			}
			else
			{
				if (data.GetType() == typeof(string))
				{
					return data as string;
				}
				else
				{
					return "N";
				}
			}
		}

		/// <summary>
		/// DataTable의 Data를 int로 변환
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static int GetDataInt(object data)
		{
			if (data == null)
			{
				return 0;
			}

			if (data == System.DBNull.Value)
			{
				return 0;
			}

			if (data.GetType() == typeof(int) || data.GetType() == typeof(short) || data.GetType() == typeof(decimal))
			{
				return Convert.ToInt32(data.ToString());
			}
			else if (data.GetType() == typeof(string) && string.IsNullOrEmpty(data as string) == false)
			{
				return Convert.ToInt32(data.ToString());
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		/// DataTable의 Data를 short로 변환
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static short GetDataShort(object data)
		{
			if (data == System.DBNull.Value)
			{
				return 0;
			}
			else
			{
				if (data.GetType() == typeof(short))
				{
					return Convert.ToInt16(data);
				}
				else
				{
					return 0;
				}
			}
		}

		/// <summary>
		/// DataTable의 Data를 float로 변환
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static float GetDataFloat(object data)
		{
			if (data == System.DBNull.Value)
			{
				return 0;
			}
			else
			{
				if (data.GetType() == typeof(float))
				{
					return Convert.ToSingle(data);
				}
				else
				{
					return 0;
				}
			}
		}

		/// <summary>
		/// DataTable의 Data를 double로 변환
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static double GetDataDouble(object data)
		{
			if (data == System.DBNull.Value)
			{
				return 0;
			}
			else
			{
				if (data.GetType() == typeof(double))
				{
					return Convert.ToDouble(data);
				}
				else
				{
					return 0;
				}
			}
		}

        /// <summary>
        /// 인자로 들어 문자에 특수 문자가 존재 하는지 여부를 검사 한다.
        /// ., <- 2개추가
        /// IsMatch(String) - Regex 생성자에 지정된 정규식이 입력 문자열에서 일치하는 항목을 찾을 것인지 여부를 나타냅니다.
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static bool CheckingSpecialText(string txt)
        {
            string str = @"[~!@\#$%^&*\()\=+|\\/:;?""<>'.,]";
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(str);
            return rex.IsMatch(txt);
        }

		/// <summary>
		/// Key_Down 이벤트
		/// 숫자만 입력받기
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <param name="MaxLength"></param>
		public static void NumberKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyValue < 48 || e.KeyValue > 57)
			{
				e.SuppressKeyPress = true;
				if (e.KeyData == Keys.Back
					|| e.KeyData == Keys.Delete
					|| (e.KeyData >= Keys.NumPad0 && e.KeyData <= Keys.NumPad9)
					|| e.KeyData == Keys.Left
					|| e.KeyData == Keys.Right
					)
				{
					e.SuppressKeyPress = false;
				}
			}
		}

		/// <summary>
		/// Key_Press이벤트
		/// 영어 +  숫자만 입력받기
		/// </summary>
		/// <param name="e"></param>
		public static void KeyPressEnglishNumber(KeyPressEventArgs e)
		{
			if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') ||
				(e.KeyChar >= 'A' && e.KeyChar <= 'Z') ||
				(e.KeyChar >= '0' && e.KeyChar <= '9') ||
				(e.KeyChar == Convert.ToChar(Keys.Delete)) ||
				(e.KeyChar == Convert.ToChar(Keys.Back)))
			{

			}
			else
			{
				e.Handled = true;
			}
		}

		/// <summary>
		/// 숫자8자리 입력할경우 유효한 날짜인지 확인. 성공ture 실패false
		/// 예)Methods.IsDate("20130214");
		/// </summary>
		/// <param name="Date"></param>
		/// <returns></returns>
		public static bool IsDate(string Date)
		{
			try
			{
				if (Date.Length < 8)
				{
					return false;
				}
				string ConvertDate = Date.Substring(0, 4) + "/" + Date.Substring(4, 2) + "/" + Date.Substring(6, 2);

				string d = Convert.ToDateTime(ConvertDate).ToString("yyyy/MM/dd");
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// MMyy string값 날짜체크. 예)03월13년 : 0313
		/// </summary>
		/// <param name="MMyy"></param>
		/// <returns></returns>
		public static bool MMyyDateCheck(string MMyy)
		{
			try
			{
				if (MMyy.Length == 4)
				{
				}
				string d = Convert.ToDateTime("20" + MMyy.Substring(2, 2) + "/" + MMyy.Substring(0, 2)).ToString("yyyy/MM");
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// 신 제조번호 체크 Validation
		/// C31(3) + 품목코드(10) + 일련번호(5) + 날짜MMyy(4)
		/// 0.총 22자리 자리수 체크
		/// 1.C31+품목코드가 From, TO 둘다 동일한지 체크.
		/// 2.일련번호가 From보다 To가 같거나 더 큰지 체크
		/// 3.날짜 MMyy타입이 유효한 날짜타입인지 체크.
		/// </summary>
		/// <param name="FromProdID"></param>
		/// <param name="ToProdID"></param>
		/// <returns></returns>
		public static string[] ProdIDValidation(string FromProdID, string ToProdID)
		{
			try
			{
				//제조번호가 22자리인지 확인        신 제조번호
				if (FromProdID.Length == 22 && ToProdID.Length == 22)
				{
					//제조번호 앞 13자리 C31(3)+품목코드(10) 똑같은지 확인
					if (FromProdID.Substring(0, 13).Equals(ToProdID.Substring(0, 13)))
					{
						////제조번호 뒤 4자리 확인 (월년)  -   구제조번호때문에 사용안함.
						//if (Convert.ToInt32(FromProdID.Substring(18, 4)) == Convert.ToInt32(ToProdID.Substring(18, 4)))
						//{
						//월년이 유효한 날짜인지 확인 -   구제조번호때문에 사용안함.
						//if (MMyyDateCheck(FromProdID.Substring(18, 4)) && MMyyDateCheck(ToProdID.Substring(18, 4)))
						//{
						//일련번호가 From보다 To가 같거나 더 큰지 체크.
						if (Convert.ToInt32(FromProdID.Substring(13, 5)) <= Convert.ToInt32(ToProdID.Substring(13, 5)))
						{
							return new string[] { "success", "0,OK" };
						}
						else
						{
							return new string[] { "failure", "1,일련번호가 From보다 To가 더 커야 합니다" };
						}
						//}
						//else
						//{
						//    return new string[] { "failure", "2,제조번호에 날짜타입이 유효한 날짜가 아닙니다" };
						//}
						//}
						////제조번호 가운데 4자리 같은지 확인 (월년)    구 제조번호
						//else if (Convert.ToInt32(FromProdID.Substring(13, 4)) == Convert.ToInt32(ToProdID.Substring(13, 4)))
						//{
						//    //월년이 유효한 날짜인지 확인
						//    if (MMyyDateCheck(FromProdID.Substring(13, 4)) && MMyyDateCheck(ToProdID.Substring(13, 4)))
						//    {
						//        return true;    //Old제조번호
						//    }
						//    else
						//    {
						//        return false;
						//    }
						//}
						//else
						//{
						//    return new string[] { "failure", "3,동일한 날짜가 아닙니다." };
						//}
					}
					else
					{
						return new string[] { "failure", "4,제조번호앞 13자리가 동일하지 않습니다" };
					}
				}
                else if (FromProdID.Length == 24 && ToProdID.Length == 24)
                {
                    return new string[] { "success", "0,OK" };
                }
                else if (FromProdID.Length == 26 && ToProdID.Length == 26)  //나노켐 제조번호는 26자리....
                {
                    return new string[] { "success", "0,OK" };
                }
                else
				{
					return new string[] { "failure", "5,제조번호 자리수가 맞지 않습니다" };
				}
			}
			catch
			{
				return new string[] { "failure", "6,제조번호를 확인하세요" };
			}
		}

		/// <summary>
		/// 신 BoxID 체크 Validation
		/// C31(3) + (IB/OB) + 품목코드(10) + 일련번호(5) + 날짜MMyy(4)
		/// 0.총 24자리 자리수 체크
		/// 1.C31+품목코드가 From, TO 둘다 동일한지 체크.
		/// 2.일련번호가 From보다 To가 같거나 더 큰지 체크
		/// 3.날짜 MMyy타입이 유효한 날짜타입인지 체크.
		/// </summary>
		/// <param name="FromProdID"></param>
		/// <param name="ToProdID"></param>
		/// <returns></returns>
		public static string[] BoxIDValidation(string FromBoxID, string ToBoxID)
		{
			List<string> lstType = new List<string>();
			lstType.Add("IB");
			lstType.Add("OB");

			try
			{
				//제조번호가 22자리인지 확인        신 제조번호
				if (FromBoxID.Length == 24 && ToBoxID.Length == 24)
				{
					//제조번호 앞 13자리 C31(3)+품목코드(10) 똑같은지 확인
					if (FromBoxID.Substring(0, 15).Equals(ToBoxID.Substring(0, 15)))
					{
						if (lstType.Contains(FromBoxID.Substring(3, 2)) == true && lstType.Contains(ToBoxID.Substring(3, 2)) == true)
						{
							//제조번호 뒤 4자리 확인 (월년)
							if (Convert.ToInt32(FromBoxID.Substring(20, 4)) == Convert.ToInt32(ToBoxID.Substring(20, 4)))
							{
								//월년이 유효한 날짜인지 확인
								if (MMyyDateCheck(FromBoxID.Substring(20, 4)) && MMyyDateCheck(ToBoxID.Substring(20, 4)))
								{
									//일련번호가 From보다 To가 같거나 더 큰지 체크.
									if (Convert.ToInt32(FromBoxID.Substring(15, 5)) <= Convert.ToInt32(ToBoxID.Substring(15, 5)))
									{
										return new string[] { "success", "0,OK" };
									}
									else
									{
										return new string[] { "failure", "1,일련번호가 From보다 To가 더 커야 합니다" };
									}
								}
								else
								{
									return new string[] { "failure", "2,BoxID에 날짜타입이 유효한 날짜가 아닙니다" };
								}
							}
							else
							{
								return new string[] { "failure", "3,동일한 날짜가 아닙니다." };
							}
						}
						else
						{
							return new string[] { "failure", "4,올바른 BoxID가 아닙니다." };
						}
					}
					else
					{
						return new string[] { "failure", "5,Box앞 15자리가 동일하지 않습니다" };
					}
				}
				else if (FromBoxID.Length == 24 && ToBoxID.Length == 24)
				{
					return new string[] { "success", "0,성공" };
				}
				else
				{
					return new string[] { "failure", "6,BoxID 자리수가 맞지 않습니다" };
				}
			}
			catch
			{
				return new string[] { "failure", "7,BoxID를 확인하세요" };
			}
		}

		/// <summary>
		/// 신 PalletID 체크 Validation
		/// C31(3) + (IP/OP) + 거래처/부서/작업장(8) + 일련번호(5) + 날짜MMyy(4)
		/// 0.총 22자리 자리수 체크
		/// 1.C31+출하처코드가 From, TO 둘다 동일한지 체크.
		/// 2.일련번호가 From보다 To가 같거나 더 큰지 체크
		/// 3.날짜 MMyy타입이 유효한 날짜타입인지 체크.
		/// </summary>
		/// <param name="FromProdID"></param>
		/// <param name="ToProdID"></param>
		/// <returns></returns>
		public static string[] PalletIDValidation(string FromPalletID, string ToPalletID)
		{
			List<string> lstType = new List<string>();
			lstType.Add("IP");
			lstType.Add("OP");

			try
			{
				//제조번호가 22자리인지 확인        신 제조번호
				if (FromPalletID.Length == 22 && ToPalletID.Length == 22)
				{
					//제조번호 앞 13자리 C31(3)+품목코드(10) 똑같은지 확인
					if (FromPalletID.Substring(0, 13).Equals(ToPalletID.Substring(0, 13)))
					{
						if (lstType.Contains(FromPalletID.Substring(3, 2)) == true && lstType.Contains(ToPalletID.Substring(3, 2)) == true)
						{
							//제조번호 뒤 4자리 확인 (월년)
							if (Convert.ToInt32(FromPalletID.Substring(18, 4)) == Convert.ToInt32(ToPalletID.Substring(18, 4)))
							{
								//월년이 유효한 날짜인지 확인
								if (MMyyDateCheck(FromPalletID.Substring(18, 4)) && MMyyDateCheck(ToPalletID.Substring(18, 4)))
								{
									//일련번호가 From보다 To가 같거나 더 큰지 체크.
									if (Convert.ToInt32(FromPalletID.Substring(13, 5)) <= Convert.ToInt32(ToPalletID.Substring(13, 5)))
									{
										return new string[] { "success", "0,OK" };
									}
									else
									{
										return new string[] { "failure", "1,일련번호가 From보다 To가 더 커야 합니다" };
									}
								}
								else
								{
									return new string[] { "failure", "2,PalletID에 날짜타입이 유효한 날짜가 아닙니다" };
								}
							}
							////제조번호 가운데 4자리 같은지 확인 (월년)    구 제조번호
							//else if (Convert.ToInt32(FromProdID.Substring(13, 4)) == Convert.ToInt32(ToProdID.Substring(13, 4)))
							//{
							//    //월년이 유효한 날짜인지 확인
							//    if (MMyyDateCheck(FromProdID.Substring(13, 4)) && MMyyDateCheck(ToProdID.Substring(13, 4)))
							//    {
							//        return true;    //Old제조번호
							//    }
							//    else
							//    {
							//        return false;
							//    }
							//}
							else
							{
								return new string[] { "failure", "3,동일한 날짜가 아닙니다." };
							}
						}
						else
						{
							return new string[] { "failure", "4,올바른 PalletID가 아닙니다." };
						}
					}
					else
					{
						return new string[] { "failure", "5,PalletID앞 13자리가 동일하지 않습니다" };
					}
				}
				else if (FromPalletID.Length == 24 && ToPalletID.Length == 24)
				{
					return new string[] { "success", "0,성공" };
				}
				else
				{
					return new string[] { "failure", "6,PalletID 자리수가 맞지 않습니다" };
				}
			}
			catch
			{
				return new string[] { "failure", "7,PalletID를 확인하세요" };
			}
		}

		//********************************************* 황호열 추가 시작  ------------ 2013.0206. 

		// 콤마세팅. float 에 콤마 세팅
		public static string setComma2(string str)
		{

			if (str == null || "".Equals(str))
				return "";
			if ("0".Equals(str))
				return "0";

			int iSosujumIdx = -1;

			for (int i = 0; i < str.Length; i++)
			{
				if (str[i].Equals('.'))
				{
					iSosujumIdx = i;
					if (i == 0)     // 0.1 등 0.x 는 .x 로 넘어온다. 그래서 앞에 '0' 붙여준다.
					{
						str = "0" + str;
						iSosujumIdx = i + 1;
					}
					break;
				}

			}

			string sosujum = "";
			if (iSosujumIdx > -1)
			{
				sosujum = str.Substring(iSosujumIdx);  // 소수점 아래 숫자. 소숫점 포함.
				str = str.Substring(0, iSosujumIdx);
			}

			string strReturn = "";
			string strTmp = str;
			string minusFlag = "";
			if (strTmp.Substring(0, 1).Equals("-"))
			{
				minusFlag = "-";
				strTmp = strTmp.Substring(1);
			}

			char[] charArr = strTmp.ToCharArray();

			int j = 0;
			for (int i = charArr.Length - 1; i >= 0; i--)
			{
				j++;
				if ((j % 3) == 0)
				{
					strReturn = "," + charArr[i].ToString() + strReturn;
				}
				else
				{
					strReturn = charArr[i].ToString() + strReturn;
				}
			}
			if ((strTmp.Length % 3) == 0)
			{
				strReturn = strReturn.Substring(1);
			}

			strReturn = minusFlag + strReturn;
			strReturn += sosujum;

			return strReturn;
		}

		// 콤마세팅
		public static string setComma(string str)
		{
			if (str == null || "".Equals(str))
				return "";

			string strReturn = "";
			string strTmp = str;
			string minusFlag = "";
			if (strTmp.Substring(0, 1).Equals("-"))
			{
				minusFlag = "-";
				strTmp = strTmp.Substring(1);
			}

			char[] charArr = strTmp.ToCharArray();

			int j = 0;
			for (int i = charArr.Length - 1; i >= 0; i--)
			{
				j++;
				if ((j % 3) == 0)
				{
					strReturn = "," + charArr[i].ToString() + strReturn;
				}
				else
				{
					strReturn = charArr[i].ToString() + strReturn;
				}
			}
			if ((strTmp.Length % 3) == 0)
			{
				strReturn = strReturn.Substring(1);
			}

			strReturn = minusFlag + strReturn;

			return strReturn;
		}

		// 숫자 체크
		public static bool checkNum(string str)
		{
			if (str == null || "".Equals(str))
				return false;

			bool isNumber = true;
			foreach (char c in str)
			{
				switch (c)
				{
					//case ',':
					//case '-':
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
						break;
					default:
						isNumber = false;
						break;

				}
			}

			return isNumber;
		}

		//********************************************* 황호열 추가 끝

		//public static byte[] GetQRCode(string code)
		//{
		//	QrEncoder encoder = new QrEncoder(ErrorCorrectionLevel.L);
		//	Renderer renderer = new Renderer(30);
		//	QrCode qrCode = encoder.Encode(code);
		//	Size size = renderer.Measure(qrCode.Matrix.Width);
		//	Bitmap bitmap = new Bitmap(size.Width, size.Height);
		//	bitmap.SetResolution(300f, 300f);
		//	Graphics graphics = Graphics.FromImage(bitmap);

		//	graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
		//	graphics.FillRectangle(Brushes.White, 0, 0, size.Width, size.Height);

		//	renderer.Draw(graphics, qrCode.Matrix);

		//	Bitmap pBitmap = new Bitmap(118, 118);
		//	pBitmap.SetResolution(300f, 300f);
		//	Graphics pGriphics = Graphics.FromImage(pBitmap);

		//	pGriphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
		//	pGriphics.DrawImage(bitmap, 0, 0, 118, 118);

		//	//renderer.CreateImageFile(qrCode.Matrix, string.Format("d:\\{0}.png", code), System.Drawing.Imaging.ImageFormat.Png);
		//	ImageConverter imageConverter = new ImageConverter();
		//	byte[] result = (byte[])imageConverter.ConvertTo(pBitmap, typeof(byte[]));



		//	//ImageConverter imageConverter = new ImageConverter();
		//	//byte[] result = (byte[])imageConverter.ConvertTo(bitmap, typeof(byte[]));

		//	return result;
		//}

        //검사성적서 Excel 출력
        public static void ExportExcel_TestReport(bool captions, SaveFileDialog saveFileDialog, string filename, string sInspectDate, string sInspectUser, string sInspectWorkShop, string sModel, string sNBNo, string sLotID, string sTempeature, string sControl, string sDIP, string sNozzle, DataGridView dgvDimsList)
        //public static void ExportExcel_TestReport(bool captions, DataGridView dataGridView, SaveFileDialog saveFileDialog, string filename, string[] cellTypes, List<string> Item)
        {
            string sSymptomName = string.Empty;
            string sComments    = string.Empty;

            saveFileDialog.FileName = filename;       //파일명
            saveFileDialog.DefaultExt = "xls";          //확장자
            saveFileDialog.Filter = "Excel files (*.xls)|*.xls";
            saveFileDialog.InitialDirectory = Environment.SpecialFolder.Recent.ToString();
            //saveFileDialog.InitialDirectory = "c:\\";       //경로

            DialogResult result = saveFileDialog.ShowDialog();      //저장다이얼로그

            if (result == DialogResult.OK)      //다이얼로그 OK일 경우
            {
                object missingType = Type.Missing;

                Excel.Application objApp = null;
                Excel._Workbook objBook = null;
                Excel.Workbooks objBooks = null;
                Excel.Sheets objSheets = null;
                Excel._Worksheet objSheet = null;
                Excel.Range range = null;

                //objApp.Interactive = false; //사용자 수정 불가능

                try
                {
                    objApp = new Excel.Application();
                    objBooks = objApp.Workbooks;
                    objBook = objBooks.Add(Missing.Value);
                    objSheets = objBook.Worksheets;
                    objSheet = (Excel._Worksheet)objSheets.get_Item(1);

                    objSheet.Columns.ColumnWidth = 2;

                    range = objSheet.get_Range("A1", "AD1");//셀 범위 지정
                    range.MergeCells = true;
                    range.Value2 = "(수출/내수)  보일러 검사 성적서";
                    //range.HorizontalAlignment = 
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    //range.Font.Bold = true;
                    range.Font.Size = 18;

                    range = objSheet.get_Range("A2", "AD2");//셀 범위 지정
                    range.MergeCells = true;
                    range.Value2 = "수출 및 내수 공용 성적서 이므로 모델 별로 해당 항목에 한하여 검사 진행함.";
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.Font.Size = 12;

                    range = objSheet.get_Range("A4", "N4");
                    range.MergeCells = true;
                    range.Value2 = "검사일자: " + sInspectDate;

                    range.Font.Size = 11;

                    range = objSheet.get_Range("P4", "AD4");
                    range.MergeCells = true;
                    range.Value2 = "모    델: " + sModel;
                    range.Font.Size = 11;

                    range = objSheet.get_Range("A5", "N5");
                    range.MergeCells = true;
                    range.Value2 = "검 사 자: " + sInspectUser;
                    range.Font.Size = 11;

                    range = objSheet.get_Range("P5", "AD5");
                    range.MergeCells = true;
                    range.Value2 = "N/B 번호: " + sNBNo;
                    range.Font.Size = 11;

                    range = objSheet.get_Range("A6", "N6");
                    range.MergeCells = true;
                    range.Value2 = "검사라인: " + sInspectWorkShop;
                    range.Font.Size = 11;

                    range = objSheet.get_Range("P6", "AD6");
                    range.MergeCells = true;
                    range.Value2 = "제조번호: " + sLotID;
                    range.Font.Size = 11;


                    // 1.중요 체크항목
                    range = objSheet.get_Range("A8", "F8");
                    range.MergeCells = true;
                    range.Value2 = "1. 중요 체크항목";
                    range.Font.Size = 11;

                    range = objSheet.get_Range("A9", "D10");
                    range.MergeCells = true;
                    range.Value2 = "온도조절기 버전";
                    range.WrapText = true;
                    range.Borders.Weight = 2;
                    //range.Borders.LineStyle = 3;
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.Font.Size = 11;

                    range = objSheet.get_Range("A11", "D12");
                    range.MergeCells = true;
                    range.Value2 = sTempeature;
                    range.WrapText = true;
                    range.Borders.Weight = 2;
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.Font.Size = 11;


                    range = objSheet.get_Range("E9", "G10");
                    range.MergeCells = true;
                    range.Value2 = "콘트롤 버전";
                    range.WrapText = true;
                    range.Borders.Weight = 2;
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.Font.Size = 11;

                    range = objSheet.get_Range("E11", "G12");
                    range.MergeCells = true;
                    range.Value2 = sControl;
                    range.WrapText = true;
                    range.Borders.Weight = 2;
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.Font.Size = 11;

                    range = objSheet.get_Range("H9", "K10");
                    range.MergeCells = true;
                    range.Value2 = "딥스위치 설정번호";
                    range.WrapText = true;
                    range.Borders.Weight = 2;
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.Font.Size = 11;

                    range = objSheet.get_Range("H11", "K12");
                    range.MergeCells = true;
                    range.Value2 = sDIP;
                    range.WrapText = true;
                    range.Borders.Weight = 2;
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.Font.Size = 11;

                    range = objSheet.get_Range("L9", "M10");
                    range.MergeCells = true;
                    range.Value2 = "노즐 사양";
                    range.WrapText = true;
                    range.Borders.Weight = 2;
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.Font.Size = 11;

                    range = objSheet.get_Range("L11", "M12");
                    range.MergeCells = true;
                    range.Value2 = sNozzle;
                    range.WrapText = true;
                    range.Borders.Weight = 2;
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.Font.Size = 11;

                    range = objSheet.get_Range("N9", "O10");
                    range.MergeCells = true;
                    range.Value2 = "마킹 확인";
                    range.WrapText = true;
                    range.Borders.Weight = 2;
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.Font.Size = 11;

                    range = objSheet.get_Range("N11", "O12");
                    range.MergeCells = true;
                    //range.Value2 = "";
                    range.WrapText = true;
                    range.Borders.Weight = 2;
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.Font.Size = 11;

                    range = objSheet.get_Range("P9", "S10");
                    range.MergeCells = true;
                    range.Value2 = "삼방벨브 가동확인";
                    range.WrapText = true;
                    range.Borders.Weight = 2;
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.Font.Size = 11;

                    range = objSheet.get_Range("P11", "S12");
                    range.MergeCells = true;
                    //range.Value2 = "";
                    range.WrapText = true;
                    range.Borders.Weight = 2;
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.Font.Size = 11;

                    range = objSheet.get_Range("T9", "W10");
                    range.MergeCells = true;
                    range.Value2 = "급, 배기    폐쇄확인";
                    range.WrapText = true;
                    range.Borders.Weight = 2;
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.Font.Size = 11;

                    range = objSheet.get_Range("T11", "W12");
                    range.MergeCells = true;
                    //range.Value2 = "";
                    range.WrapText = true;
                    range.Borders.Weight = 2;
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.Font.Size = 11;

                    range = objSheet.get_Range("X9", "Y10");
                    range.MergeCells = true;
                    range.Value2 = "배선 확인";
                    range.WrapText = true;
                    range.Borders.Weight = 2;
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.Font.Size = 11;

                    range = objSheet.get_Range("X11", "Y12");
                    range.MergeCells = true;
                    //range.Value2 = "";
                    range.WrapText = true;
                    range.Borders.Weight = 2;
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.Font.Size = 11;

                    range = objSheet.get_Range("Z9", "AA10");
                    range.MergeCells = true;
                    range.Value2 = "누수 확인";
                    range.WrapText = true;
                    range.Borders.Weight = 2;
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.Font.Size = 11;

                    range = objSheet.get_Range("Z11", "AA12");
                    range.MergeCells = true;
                    //range.Value2 = "";
                    range.WrapText = true;
                    range.Borders.Weight = 2;
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.Font.Size = 11;

                    range = objSheet.get_Range("AB9", "AD10");
                    range.MergeCells = true;
                    range.Value2 = "소음    확인";
                    range.WrapText = true;
                    range.Borders.Weight = 2;
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.Font.Size = 11;

                    range = objSheet.get_Range("AB11", "AD12");
                    range.MergeCells = true;
                    //range.Value2 = "";
                    range.WrapText = true;
                    range.Borders.Weight = 2;
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.Font.Size = 11;

                    //2.검사항목별 검사값
                    range = objSheet.get_Range("A14", "G14");
                    range.MergeCells = true;
                    range.Value2 = "2.검사항목별 검사값";
                    range.Font.Size = 11;

                    range = objSheet.get_Range("A15", "G15");
                    range.MergeCells = true;
                    range.Value2 = "검사항목";
                    range.WrapText = true;
                    range.Borders.Weight = 2;
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.Font.Size = 11;

                    range = objSheet.get_Range("H15", "S15");
                    range.MergeCells = true;
                    range.Value2 = "SPEC";
                    range.WrapText = true;
                    range.Borders.Weight = 2;
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.Font.Size = 11;

                    range = objSheet.get_Range("T15", "Y15");
                    range.MergeCells = true;
                    range.Value2 = "측정값";
                    range.WrapText = true;
                    range.Borders.Weight = 2;
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.Font.Size = 11;

                    range = objSheet.get_Range("Z15", "AD15");
                    range.MergeCells = true;
                    range.Value2 = "적합 여부";
                    range.WrapText = true;
                    range.Borders.Weight = 2;
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    range.Font.Size = 11;


                    // 검사항목별 검사값 Data 총14개 
                    // Data Grid 항목 만큼 Data를 인쇄하고 나머지는 빈칸으로 인쇄한다. 
                    // 정량적 검사 Data Get

                    int j = 0;
                    if (dgvDimsList.Rows.Count > 0) 
                    {
                        int iRowIndex = dgvDimsList.CurrentCell.RowIndex;
                        DataTable dt = dgvDimsList.DataSource as DataTable;
                        

                        for (int i = 0; i < dgvDimsList.Rows.Count; i++)
                        {
                            j = 16 + i;

                            //검사항목
                            range = objSheet.get_Range("A" + j.ToString(), "G" + j.ToString());
                            range.MergeCells = true;
                            range.Value2 = dt.Rows[iRowIndex+i][0] as string;
                            range.WrapText = true;
                            range.Borders.Weight = 2;
                            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            range.Font.Size = 11;

                            //SPEC
                            range = objSheet.get_Range("H" + j.ToString(), "S" + j.ToString());
                            range.MergeCells = true;
                            range.Value2 = dt.Rows[iRowIndex+i][2] as string;
                            range.WrapText = true;
                            range.Borders.Weight = 2;
                            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            range.Font.Size = 11;

                            //측정값
                            range = objSheet.get_Range("T" + j.ToString(), "Y" + j.ToString());
                            range.MergeCells = true;
                            range.Value2 = dt.Rows[iRowIndex+i][6] as string;
                            range.WrapText = true;
                            range.Borders.Weight = 2;
                            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            range.Font.Size = 11;

                            //적합여부
                            range = objSheet.get_Range("Z" + j.ToString(), "AD" + j.ToString());
                            range.MergeCells = true;
                            range.Value2 = dt.Rows[iRowIndex+i][7] as string;
                            range.WrapText = true;
                            range.Borders.Weight = 2;
                            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            range.Font.Size = 11;

                            //부적합내역(Data)
                            if (dt.Rows[iRowIndex + i][8] as string != "")
                            {
                                sSymptomName = dt.Rows[iRowIndex + i][8] as string;
                            }

                            //조치내역(Data)
                            if (dt.Rows[iRowIndex + i][9] as string != "")
                            {
                                sComments = dt.Rows[iRowIndex + i][9] as string;
                            }

                        }

                    }

                    for (int i = 1; i <= (14 - dgvDimsList.Rows.Count); i++)
                    {
                        j = 15 + i + dgvDimsList.Rows.Count;

                        range = objSheet.get_Range("A" + j.ToString(), "G" + j.ToString());
                        range.MergeCells = true;
                        range.WrapText = true;
                        range.Borders.Weight = 2;
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        range.Font.Size = 11;

                        range = objSheet.get_Range("H" + j.ToString(), "S" + j.ToString());
                        range.MergeCells = true;
                        range.WrapText = true;
                        range.Borders.Weight = 2;
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        range.Font.Size = 11;

                        range = objSheet.get_Range("T" + j.ToString(), "Y" + j.ToString());
                        range.MergeCells = true;
                        range.WrapText = true;
                        range.Borders.Weight = 2;
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        range.Font.Size = 11;

                        range = objSheet.get_Range("Z" + j.ToString(), "AD" + j.ToString());
                        range.MergeCells = true;
                        range.WrapText = true;
                        range.Borders.Weight = 2;
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        range.Font.Size = 11;
                    }



                    //3. 내전압 시험
                    range = objSheet.get_Range("A31", "A31");
                    range.MergeCells = true;
                    //2016.12.20, 고객 요청에 의해 수출/내수 모두 사용하도록 내전압/절연, 저항 시험으로 변경
                    range.Value2 = "3. 내전압/ 절연,저항 시험(내전압 시험시1500V 1분 인가하여 이상이 없을 것) (               )";
                    //range.Value2 = "3. 내전압 시험(1500V 1분 인가하여 이상이 없을 것) (               )";
                    range.Font.Size = 11;

                    //4.부적합내역
                    range = objSheet.get_Range("A33", "E33");
                    range.MergeCells = true;
                    range.Value2 = "4.부적합내역";
                    range.Font.Size = 11;

                    range = objSheet.get_Range("A34", "O39");
                    range.MergeCells = true;
                    range.Value2 = sSymptomName;
                    range.Borders.Weight = 2;
                    range.Font.Size = 11;


                    //5.조치내역
                    range = objSheet.get_Range("P33", "P33");
                    range.MergeCells = true;
                    range.Value2 = "5.조치내역";
                    range.Font.Size = 11;

                    range = objSheet.get_Range("P34", "AD39");
                    range.MergeCells = true;
                    range.Value2 = sComments;
                    range.Borders.Weight = 2;
                    range.Font.Size = 11;


                    range = objSheet.get_Range("A40", "A40");
                    range.MergeCells = true;
                    range.Value2 = "* 연소기능 용량별 사양서에 준할 것 ";
                    range.Font.Size = 11;

                    range = objSheet.get_Range("A41", "AD41");
                    range.MergeCells = true;
                    range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;

                    range = objSheet.get_Range("A42", "A42");
                    //range.MergeCells = true;
                    range.Value2 = "Kiturami Co,Ltd,.";
                    range.Font.Size = 11;


                    //Properties.Resources.se_alert
                    //string sPicture =  "\\resources\excel_logo.jpg";
                    //Image Logo = "\\resources\page_bg.jpg"; //KR_POP.Common.Properties.Resources. ;
                    //range = objSheet.get_Range("X42", "AD42");
                    //Image oImage = Image.FromFile("excel_logo.jpg");
                    ////Image oImage = Image.FromFile("\\resources\page_bg.jpg");
                    //range.set_Item(1,1,oImage);

                    objApp.Visible = false;
                    objApp.UserControl = false;

                    objBook.SaveAs(@saveFileDialog.FileName,
                               Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal,
                               missingType, missingType, missingType, missingType,
                               Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                               missingType, missingType, missingType, missingType, missingType);
                    objBook.Close(false, missingType, missingType);

                    Cursor.Current = Cursors.Default;


                    MessageBox.Show("저장했습니다!!!");     //성공했을경우 메세지박스

                    //엑셀프로세스 종료 
                    objBooks.Close();
                    objApp.Quit();

                    objSheets = null;
                    objBooks = null;
                    objApp = null;

                }
                catch (Exception theException)
                {
                    String errorMessage;
                    errorMessage = "Error: ";
                    errorMessage = String.Concat(errorMessage, theException.Message);
                    errorMessage = String.Concat(errorMessage, " Line: ");
                    errorMessage = String.Concat(errorMessage, theException.Source);

                    MessageBox.Show(errorMessage, "Error");
                }
                finally
                {
                    // Clean up
                    ReleaseExcelObject(objSheets);
                    ReleaseExcelObject(objBooks);
                    ReleaseExcelObject(objApp);
                }



            }

        }
	}
}
