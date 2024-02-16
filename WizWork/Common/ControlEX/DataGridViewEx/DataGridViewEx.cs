using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text;


//*******************************************************************************
//프로그램명    DataGridViewEx.cs
//메뉴ID        
//설명          DataGridView의 확장 클래스
//작성일        2012.12.06
//개발자        남인호
//*******************************************************************************
// 변경일자     변경자      요청자      요구사항ID          요청 및 작업내용
//*******************************************************************************
//
//
//*******************************************************************************

namespace KR_POP.Common.ControlEX
{
    public enum DataGridViewRowState
    {
        Added,
        Deleted,
        Detached,
        Modified,
        Unchanged
    }

    public partial class DataGridViewEx : DataGridView
    {
		private bool _isInSort = false;		//정렬 시 RowEnter EventHandler로 들어갔을 경우 처리를 막기 위해 사용
		private bool _isInQuery = false;	//조회 결과를 그리드에 적용할 때 RowEnter EventHandler로 들어갔을 경우 처리를 막기 위해 사용

		//private Dictionary<string, KeyValuePair<DataGridViewColumn, bool>> _dicSortInformation = new Dictionary<string, KeyValuePair<DataGridViewColumn, bool>>();
		//private Dictionary<int, int> _dicRealIndex = new Dictionary<int, int>();

		private List<string> _listMantatoryColumnName = null;
		private List<string> _listPKColumnName = null;
		public List<string> MandatoryColumns
		{
			get { return _listMantatoryColumnName; }
		}

		private Dictionary<string, List<string>> _dicModifiedCellPosition = new Dictionary<string, List<string>>();
		private string _strPrevCellValue = string.Empty;

		private static Color ColorModified = Color.FromArgb(10, 96, 177);
        private static Font FontModified = new Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
        private static Font FontNormal = new Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));

		private List<int> _lstWidth = new List<int>();




        #region Properties

		/// <summary>
		/// 그리드 처리속도를 높여준다
		/// 하지만 셀 머지가 있는 경우 머지한 셀이 까만색으로 아무것도 보이지 않게되는 문제가 있다.
		/// </summary>
        public bool DoubleBufferSet
        {
            set { this.DoubleBuffered = value; }
        }

		public bool IsInSort
		{
			get { return _isInSort; }
			set { _isInSort = value; }
		}

        public bool IsInQuery
        {
            get { return _isInQuery; }
            set {

                if (value == true)
                {
                    _dicModifiedCellPosition.Clear();
                }

                _isInQuery = value; }
        }

		public new DataGridViewAutoSizeColumnsMode AutoSizeColumnsMode
		{
			get { return base.AutoSizeColumnsMode; }
			set
			{
				base.AutoSizeColumnsMode = value;

				if (base.AutoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.AllCells)
				{
					foreach (DataGridViewColumn col in Columns)
					{
						_lstWidth.Add(col.Width);
					}
				}
				else if (base.AutoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.None)
				{
					for (int i = 0; i < _lstWidth.Count; i++)
					{
						Columns[i].Width = _lstWidth[i];
					}

					_lstWidth.Clear();
				}
			}
		}

        #endregion



        #region Creator

        public DataGridViewEx()
        {
            InitializeComponent();
            this.AllowUserToResizeRows = false;
        }

        #endregion



        #region 초기설정

        /// <summary>
        /// PKCell만 별도로 지정할 때 사용한다.
        /// </summary>
        /// <param name="names"></param>
        public void SetPkColumnNames(List<string> pkCellList)
        {
			_listPKColumnName = pkCellList;

			// PKCell BGColor
            foreach (string pkCell in pkCellList)
            {
				Columns[pkCell].DefaultCellStyle.BackColor = Color.FromArgb(237, 226, 109);
            }
        }

        /// <summary>
        /// 필수입력항목을 지정한다. - ColumnHeaderText의 Font Color도 함께 적용된다.
        /// </summary>
        /// <param name="names"></param>
        public void SetMandatoryColumnNames(List<string> names)
        {
            _listMantatoryColumnName = names;

            foreach (string name in _listMantatoryColumnName)
            {
				Columns[name].HeaderCell.Style.ForeColor = Color.Blue;
            }
        }

        /// <summary>
        /// ReadOnly 항목을 지정한다. - 배경색도 함께 적용된다.
        /// </summary>
        /// <param name="indexes"></param>
        public void SetReadOnlyIndex(List<string> names)
        {
            foreach (string name in names)
            {
                Columns[name].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 191);
                Columns[name].ReadOnly = true;
            }
        }

        #endregion



        #region 상태확인 등 후처리 시 확인용

        /// <summary>
        /// 클래스 구분용
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "DataGridViewEx";
        }

        /// <summary>
        /// 모든 필수입력항목이 입력되었는지 확인
        /// </summary>
        /// <returns>true - 모두 입력되어 있는 경우</returns>
        public bool IsAllMandatoryItemsFilled()
        {
			DataTable dt = DataSource as DataTable;

			if (dt == null)
			{
				return true;
			}

			foreach (DataRow row in dt.Rows)
			{
				switch (row.RowState)
				{
					case DataRowState.Added:
					case DataRowState.Modified:
						{
							foreach (string name in _listMantatoryColumnName)
							{
                                // 공백제거 추가. 20151216 by 황호열
                                //if (row[name] == null || string.IsNullOrEmpty(row[name].ToString()) == true)
                                if (row[name] == null || string.IsNullOrEmpty(row[name].ToString().Trim()) == true)
								{
									return false;
								}
							}
						}
						break;
					default:
						{
						}
						break;
				}

			}

            return true;
        }

		public bool IsContainsChanges()
		{
			if (DataSource == null)
			{
				return false;
			}

			DataTable dt = DataSource as DataTable;
			return dt.DataSet.HasChanges();
		}

        #endregion



        #region 사용자의 선택/추가/삭제 시 _dicRealIndex, _dicRowStates, listRemovedRowsPKValue 변경, 폰트 색 변경

		/// <summary>
		/// cell 값이 변경되면 폰트를 변경한다.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void DataGridViewEx_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
			if (_isInQuery == true
				|| _isInSort == true
				|| CurrentCell == null
				|| CurrentCell.Value == null
				)
			{
				return;
			}

			if (CurrentCell.Value != null)
			{
				_strPrevCellValue = CurrentCell.Value.ToString();
			}
			else
			{
				_strPrevCellValue = string.Empty;
			}

			CurrentCell.Style.ForeColor = ColorModified;
			CurrentCell.Style.Font = FontModified;
		}

		/// <summary>
		/// cell 값이 변경되면 폰트를 변경한다.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void DataGridViewEx_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
			if (_ctrlCombo != null)
			{
				_ctrlCombo.KeyPress -= DataGridViewEx_KeyPress;
				_ctrlCombo = null;
			}

			if (_isInQuery == true
				|| _isInSort == true
				|| CurrentCell == null
				|| CurrentCell.Value == null
				|| _listPKColumnName == null
				)
			{
				return;
			}

			string pkValues = GetPkValues(e.RowIndex);
			string columnName = Columns[e.ColumnIndex].Name;

			if (CurrentCell.Value.ToString().Equals(_strPrevCellValue) == true)
			{
				if (_dicModifiedCellPosition.ContainsKey(pkValues) == false
					|| _dicModifiedCellPosition[pkValues].Contains(columnName) == false)
				{
					CurrentCell.Style.ForeColor = Color.Black;
					CurrentCell.Style.Font = FontNormal;
				}
			}
			else
			{
				if (_dicModifiedCellPosition.ContainsKey(pkValues) == false)
				{
					List<string> listColumnNames = new List<string>();
					listColumnNames.Add(columnName);

					_dicModifiedCellPosition.Add(pkValues, listColumnNames);
				}
				else if (_dicModifiedCellPosition[pkValues].Contains(columnName) == false)
				{
					_dicModifiedCellPosition[pkValues].Add(columnName);
				}
			}

			_strPrevCellValue = string.Empty;
		}

		private string GetPkValues(int rowIndex)
		{
			string pkValues = string.Empty;

			foreach (string pkColumnName in _listPKColumnName)
			{
				pkValues += Rows[rowIndex].Cells[pkColumnName].Value as string;
				pkValues += "|";
			}

			if (pkValues.Length > 0)
			{
				pkValues = pkValues.Substring(0, pkValues.Length - 1);
			}

			return pkValues;
		}

		/// <summary>
		/// 정렬 후 수정이 있는 셀의 폰트를 다시 적용한다.
		/// </summary>
		public void UpdateModifiedStyle()
		{
			foreach (DataGridViewRow row in Rows)
			{
				string pkValues = string.Empty;

				foreach (string pkColumnName in _listPKColumnName)
				{
					pkValues += row.Cells[pkColumnName].Value as string;
					pkValues += "|";
				}

				if (pkValues.Length > 0)
				{
					pkValues = pkValues.Substring(0, pkValues.Length - 1);
				}

				if (_dicModifiedCellPosition.ContainsKey(pkValues) == true)
				{
					foreach(string columnName in _dicModifiedCellPosition[pkValues])
					{
						row.Cells[columnName].Selected = true;
						CurrentCell = row.Cells[columnName];

						CurrentCell.Style.ForeColor = Color.Black;
						CurrentCell.Style.Font = FontNormal;
					}
				}
			}

			CurrentCell.Selected = false;
			CurrentCell = null;

			//Refresh();
		}

		/// <summary>
		/// 수정된 셀 위치정보를 초기화한다.
		/// </summary>
		public void ClearModifiedCellPosition()
		{
			_dicModifiedCellPosition.Clear();
		}

		public bool IsRowModified(int rowIndex)
		{
			bool result = false;
			string pkValue = GetPkValues(rowIndex);
			
			if (_dicModifiedCellPosition.ContainsKey(pkValue) == true)
			{
				result = true;
			}

			return result;
		}

        /// <summary>
        /// CellValue가 변경되면 해당 p_Dic_RowStates를 Modified로 변경한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewEx_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0
				|| e.ColumnIndex < 0
				|| _isInQuery == true
				|| _isInSort == true)
            {
                return;
            }

			SetParentChanges(true);

			if (_listPKColumnName != null)
			{
				string pkValues = GetPkValues(e.RowIndex);
				string columnName = Columns[e.ColumnIndex].Name;

				if (_dicModifiedCellPosition.ContainsKey(pkValues) == false)
				{
					List<string> listColumnNames = new List<string>();
					listColumnNames.Add(columnName);

					_dicModifiedCellPosition.Add(pkValues, listColumnNames);
				}
				else if (_dicModifiedCellPosition[pkValues].Contains(columnName) == false)
				{
					_dicModifiedCellPosition[pkValues].Add(columnName);
				}
			}
        }

        #endregion



        #region DataGridViewTextBoxButtonCell이 있는 Row를 지우는 처리

        /// <summary>
        /// DataGridViewTextBoxButtonCell이 포함된 한 행을 지운다.
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="textBoxButtonCellIndex"></param>
		public void RemoveButtonFromTextBoxButtonCell(int rowIndex, string textBoxButtonCellName)
        {
            DataGridViewTextBoxButtonCell cell = Rows[rowIndex].Cells[textBoxButtonCellName] as DataGridViewTextBoxButtonCell;
           
            if (cell != null)
            {
                cell.RemoveButton();
            }
        }

        #endregion



		#region Copy&Paste

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if ((keyData == (Keys.Control | Keys.Shift | Keys.C)))
			{
				DataObject data = GetClipboardContent();

				if (data == null)
				{
					return true;
				}

				string dgvMainHtmlString = (string)data.GetData(DataFormats.Html);
				MemoryStream str = new MemoryStream(Encoding.UTF8.GetBytes(dgvMainHtmlString));
				data.SetData(DataFormats.Html, str);
				Clipboard.Clear();
				Clipboard.SetDataObject(data, true);
				str.Close();
				str.Dispose();

				return true;
			}
			else if(keyData == (Keys.Control | Keys.C))
			{
				if (CurrentCell == null)
				{
					return true;
				}

				DataObject data = new DataObject(CurrentCell.FormattedValue);
				Clipboard.SetDataObject(data);
				
				return true;
			}
			else if (keyData == (Keys.Control | Keys.V))
			{
				if (CurrentCell == null)
				{
					return true;
				}

				string strText = Clipboard.GetText();
				string[] lines = strText.Split('\n');
				int iRowIndex = CurrentCell.RowIndex;
				int iColumnIndex = CurrentCell.ColumnIndex;
				foreach (string line in lines)
				{
					if (iRowIndex < RowCount && line.Length > 0)
					{
						string[] cells = line.Split('\t');

						for (int i = 0; i < cells.GetLength(0); ++i)
						{
							if (iColumnIndex + i < ColumnCount)
							{
								// ReadOnly인 경우 값을 넣지 않는다.
								if (Rows[iRowIndex].Cells[iColumnIndex + i].ReadOnly == false)
								{
									Rows[iRowIndex].Cells[iColumnIndex + i].Value = Convert.ChangeType(cells[i], Rows[iRowIndex].Cells[iColumnIndex + i].ValueType);
								}
							}
							else
							{
								break;
							}
						}

						iRowIndex++;
					}
					else
					{
						break;
					}
				}

				DataGridViewCell cell = CurrentCell;
				CurrentCell = null;
				CurrentCell = cell;

				//this.EndEdit();
				//this.Refresh();

				return true;
			}

			return base.ProcessCmdKey(ref msg, keyData);
		}

		#endregion



		#region DataGridView Check/Uncheck All

		private List<string> _lstCheckColumnNames = null;

		/// <summary>
		/// 전체 체크/해제할 칼럼명들을 설정한다.
		/// </summary>
		/// <param name="lstColumns"></param>
		public void SetCheckColumns(List<string> lstColumns)
		{
			_lstCheckColumnNames = lstColumns;

			foreach (string name in _lstCheckColumnNames)
			{
				if (_dicSort.ContainsKey(name) == true)
				{
					_dicSort.Remove(name);
				}
			}
		}

		/// <summary>
		/// DataGridViewCheckBoxColumn의 ColumnHeader를 MouseClick 했을 때 전체 선택/해제 처리한다.
		/// 데이터가 많으면 느려짐.
		/// </summary>
		/// <param name="gridView"></param>
		/// <param name="columnName"></param>
		private void GridViewCheckToggle(string columnName)
		{
			DataGridViewCheckBoxColumn col = Columns[columnName] as DataGridViewCheckBoxColumn;
			object trueValue = col.TrueValue == null ? "True" : col.TrueValue;
			object falseValue = col.FalseValue == null ? "False" : col.FalseValue;
			object setValue = null;

			Cursor = Cursors.WaitCursor;

			if (RowCount > 0)
			{
				DataTable dt = DataSource as DataTable;
				var query = dt.Select(columnName + " = '" + trueValue + "'");
				int iTrueCount = query.Count();


				if (iTrueCount == 0)
				{
					setValue = trueValue;
				}
				else if (iTrueCount == RowCount)
				{
					setValue = falseValue;
				}

				dt.AsEnumerable().All(p => { p[columnName] = setValue; return true; });
			}

			Cursor = Cursors.Default;
		}

		#endregion

		

		#region DataSet을 이용한 다중정렬(칼럼헤더 클릭) - 정렬초기화(칼럼헤더 더블클릭)

		private Dictionary<string, string> _dicSort = new Dictionary<string, string>();

		/// <summary>
		/// DataGridView를 정렬하는데 사용할 기준정보를 만든다
		/// </summary>
		public void PrepareSort()
		{
			_dicSort = GetSortKey();
		}

		/// <summary>
		/// DataGridView를 정렬하는데 사용할 기준정보를 만든다
		/// excptList에 해당하는 열은 제외한다.
		/// </summary>
		/// <param name="dataGridView"></param>
		/// <param name="excptList"></param>
		public void PrepareSort(List<string> excptList)
		{
			_dicSort = GetSortKey(excptList);
		}

		/// <summary>
		/// Column Head를 클릭한 것에 따라 정렬한다.
		/// 1.prepareSort를 실행하여 sort기준정보를 만든다.
		/// 2.Sort를 실행한다.
		/// 3.초기화는 InitSort를 실행한다.
		/// </summary>
		/// <param name="dataGridView">정렬할 DataGridView</param>
		/// <param name="columnIndex">정렬할 Column - 이번에 Click한 Column</param>
		/// <param name="dataSet">정렬할 데이터</param>
		/// <param name="sort">정렬 기준정보</param>
		private void Sort(string columnName)
		{
			// 수정된 내용이 있으면 정렬금지
			DataTable dt = DataSource as DataTable;

			if (dt == null || dt.DataSet == null || dt.DataSet.HasChanges() == true)
			{
				return;
			}

			if (_dicSort.ContainsKey(columnName) == false)
			{
				return;
			}

			//컬럼명,(클릭순서dateTime,정렬상태A/D/N, DataPropertyName) 컬럼명을 key로 Dictionary<,>
			//생성시간 순 정렬 후 List에 넣고, sort text 생성 
			//더블클릭시 해제
			DataGridViewColumn column = Columns[columnName];

			if (string.IsNullOrEmpty(_dicSort[columnName]))
			{
				_dicSort[columnName] = DateTime.Now.Ticks.ToString() + "/" + column.DataPropertyName + " " + "ASC";
				column.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Ascending;
			}
			else
			{
				string[] keys = _dicSort[columnName].Split('/');
				string[] values = keys[1].Split(' ');

				switch (values[1].ToUpper())
				{
					case "ASC":
						{
							_dicSort[columnName] = keys[0] + "/" + column.DataPropertyName + " " + "DESC";
						}
						break;
					case "DESC":
						{
							_dicSort[columnName] = "";
						}
						break;
					default:
						{
						}
						break;
				}
			}

			(DataSource as DataTable).DefaultView.Sort = GetSortString(_dicSort);
			MarkUP(_dicSort);
		}

		/// <summary>
		/// 정렬을 초기화한다.
		/// </summary>
		/// <param name="dataGridView">초기화 할 DataGridView</param>
		/// <param name="dataSet">초기화 할 데이터</param>
		/// <param name="sort">정렬 기준정보도 초기화되어야 한다.</param>
		private void InitSort()
		{
			// 수정된 내용이 있으면 정렬금지
			DataTable dt = DataSource as DataTable;

			if (dt == null || dt.DataSet == null || dt.DataSet.HasChanges() == true)
			{
				return;
			}

			//초기화
			(DataSource as DataTable).DefaultView.Sort = "";

			List<string> listKey = _dicSort.Keys.ToList();

			foreach (string key in listKey)
			{
				_dicSort[key] = string.Empty;
			}
		}

		/// <summary>
		/// 정렬을 사용할 폼이면 Form_Load에서 호출해야 한다.
		/// </summary>
		/// <param name="dataGridView">정렬할 DataGridView</param>
		/// <returns></returns>
		private Dictionary<string, string> GetSortKey()
		{
			foreach (DataGridViewColumn col in Columns)
			{
				if (_lstCheckColumnNames != null && _lstCheckColumnNames.Contains(col.Name) == true)
				{
					col.SortMode = DataGridViewColumnSortMode.NotSortable;
				}
				else
				{
					col.SortMode = DataGridViewColumnSortMode.Programmatic;
				}
			}

			Dictionary<string, string> dic = new Dictionary<string, string>();

			foreach (DataGridViewColumn col in Columns)
			{
				if (_lstCheckColumnNames == null || _lstCheckColumnNames.Contains(col.Name) == false)
				{
					dic.Add(col.Name, "");
				}
			}

			return dic;
		}

		/// <summary>
		/// 정렬을 사용할 폼이면 Form_Load에서 호출해야 한다.
		/// </summary>
		/// <param name="dataGridView">정렬할 DataGridView</param>
		/// <returns></returns>
		private Dictionary<string, string> GetSortKey(List<string> excptList)
		{
			foreach (DataGridViewColumn col in Columns)
			{
				if (excptList.Contains(col.Name) == true || (_lstCheckColumnNames != null && _lstCheckColumnNames.Contains(col.Name) == true))
				{
					col.SortMode = DataGridViewColumnSortMode.NotSortable;
				}
				else
				{
					col.SortMode = DataGridViewColumnSortMode.Programmatic;
				}
			}

			Dictionary<string, string> dic = new Dictionary<string, string>();

			foreach (DataGridViewColumn col in Columns)
			{
				if (excptList.Contains(col.Name) == false && (_lstCheckColumnNames == null ||_lstCheckColumnNames.Contains(col.Name) == false))
				{
					dic.Add(col.Name, string.Empty);
				}
			}

			return dic;
		}

		/// <summary>
		/// 칼럼에 Glyph를 설정한다.
		/// </summary>
		/// <param name="dataGridView"></param>
		/// <param name="dic"></param>
		private void MarkUP(Dictionary<string, string> dic)
		{
			foreach (KeyValuePair<string, string> kvp in dic)
			{
				if (!string.IsNullOrEmpty(kvp.Value))
				{
					string[] order = kvp.Value.Split(' ');

					switch (order[1].ToUpper())
					{
						case "ASC":
							{
								Columns[kvp.Key].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Ascending;
							}
							break;
						case "DESC":
							{
								Columns[kvp.Key].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Descending;
							}
							break;
						default:
							{
								Columns[kvp.Key].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.None;
							}
							break;
					}
				}
			}
		}

		/// <summary>
		/// 실제 정렬정보를 읽어온다.
		/// </summary>
		/// <param name="dic"></param>
		/// <returns></returns>
		private string GetSortString(Dictionary<string, string> dic)
		{
			string strReturn = "";

			List<string> list = dic.Values.ToList<string>();
			list.Sort();

			foreach (string s in list)
			{
				if (!string.IsNullOrEmpty(s))
				{
					strReturn = strReturn + s.Split('/')[1] + ",";
				}
			}

			if (!string.IsNullOrEmpty(strReturn))
			{
				strReturn = strReturn.Remove(strReturn.LastIndexOf(','), 1);
			}

			return strReturn;
		}

		/// <summary>
		/// 정렬 및 체크박스칼럼 전체선택/해제 처리
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DataGridViewEx_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.ColumnIndex >= 0 && e.ColumnIndex < Columns.Count)
			{
				string columnName = Columns[e.ColumnIndex].Name;

				if (_dicSort.ContainsKey(columnName) == true)
				{
					IsInSort = true;
					Sort(columnName);
					IsInSort = false;
				}

				if (_lstCheckColumnNames != null && _lstCheckColumnNames.Contains(columnName) == true)
				{
					GridViewCheckToggle(columnName);
					RefreshEdit();
				}
			}
		}

		/// <summary>
		/// 정렬 초기화처리
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DataGridViewEx_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.ColumnIndex >= 0 && e.ColumnIndex < Columns.Count)
			{
				string columnName = Columns[e.ColumnIndex].Name;

				if (_dicSort.ContainsKey(columnName) == true)
				{
					IsInSort = true;
					InitSort();
					IsInSort = false;
				}
			}
		}

		#endregion



#if false // 선택된 셀 표시는 하지 않는다.
		#region 선택된 셀의 Border를 다시 그려준다.

        /// <summary>
        /// 선택된 셀의 Border를 다시 그려준다.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (//this.selectionmode == datagridviewselectionmode.cellselect && 
                this.EditMode == DataGridViewEditMode.EditOnEnter &&
                this.CurrentCell != null)
            {
                Brush brush = new SolidBrush(Color.FromArgb(98, 91, 12));
                Pen pen = new Pen(brush, 1);
                e.Graphics.DrawRectangle(pen, GetCurrentRectangle());
                //Invalidate();
            }
        }

        /// <summary>
        /// CurrentCell의 Border Rect를 가져온다.
        /// </summary>
        /// <returns></returns>
        private Rectangle GetCurrentRectangle()
        {
#if true
            bool isCurrentCellSpanned = false;

            DataGridViewTextBoxCellEx cell = CurrentCell as DataGridViewTextBoxCellEx;
            if (cell != null && cell.ColumnSpan > 1)
            {
                isCurrentCellSpanned = true;
            }

            if (isCurrentCellSpanned == true)
            {
                // Merged Cell의 테두리는 약간의 문제가 있어서 막음
                //for (int i = 0; i < spanStart + span; i++)
                //{
                //    if (i < spanStart)
                //    {
                //        xPos += this.Columns[i].Width;
                //    }
                //    else if (i == spanStart)
                //    {
                //        width = this.Columns[i].Width;
                //    }
                //    else
                //    {
                //        width += this.Columns[i].Width;
                //    }
                //}
                return GetCellDisplayRectangle(CurrentCell.ColumnIndex, CurrentCell.RowIndex, true);
            }
            else
            {
                return GetCellDisplayRectangle(CurrentCell.ColumnIndex, CurrentCell.RowIndex, true);
            }
#else
            int rowIndex = CurrentCell.RowIndex;
            int columnIndex = CurrentCell.ColumnIndex;

            int xPos = 1 - p_CurrentStartPosX;
            
            int yPos = this.ColumnHeadersHeight + (rowIndex * this.RowTemplate.Height) - p_CurrentStartPosY;
            int width = this.Columns[columnIndex].Width;
            int height = this.RowTemplate.Height;

            int span = 1;
            int spanStart = -1;
            bool isCurrentCellSpanned = false;

            if (RowHeadersVisible == true)
            {
                xPos += RowHeadersWidth;
            }

            for (int cellIndex = 0; cellIndex <= columnIndex; cellIndex++)
            {
                DataGridViewTextBoxCellEx cell = Rows[rowIndex].Cells[cellIndex] as DataGridViewTextBoxCellEx;
                if (cell != null && cell.ColumnSpan > 1)
                {
                    if (cellIndex + cell.ColumnSpan - 1 >= columnIndex)
                    {
                        isCurrentCellSpanned = true;
                        spanStart = cellIndex;
                        span = cell.ColumnSpan;
                        break;
                    }
                }
            }

            if (isCurrentCellSpanned == true)
            {
                // Merged Cell의 테두리는 약간의 문제가 있어서 막음
                //for (int i = 0; i < spanStart + span; i++)
                //{
                //    if (i < spanStart)
                //    {
                //        xPos += this.Columns[i].Width;
                //    }
                //    else if (i == spanStart)
                //    {
                //        width = this.Columns[i].Width;
                //    }
                //    else
                //    {
                //        width += this.Columns[i].Width;
                //    }
                //}
            }
            else
            {
                for (int i = 0; i < columnIndex; i++)
                {
                    xPos += this.Columns[i].Width;
                }
            }

            return new Rectangle(xPos, yPos, width, height);
#endif
        }

        /// <summary>
        /// CurrentCell이 변경되면 다시 그려준 Border가 적용되도록 Invalidate() 해줘야 한다.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCurrentCellChanged(EventArgs e)
        {
            base.OnCurrentCellChanged(e);

            if (CurrentRow != null && Rows.Count > CurrentRow.Index && CurrentRow.Index > -1)
            {
                Invalidate();
            }
        }

        private int p_CurrentStartPosX = 0;
        private int p_CurrentStartPosY = 0;
        protected override void OnScroll(ScrollEventArgs e)
        {
            base.OnScroll(e);

            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                p_CurrentStartPosX = e.NewValue;
            }
            else
            {
                p_CurrentStartPosY = e.NewValue;
            }

            Invalidate();
        }

		#endregion
#endif


		#region 인쇄/미리보기

		private string _strPrintTitle = string.Empty;
        private Dictionary<string, string> _dicPrintLeftHeader = null;
        private Dictionary<string, string> _dicPrintRightHeader = null;
        private string _strPrintFormInfo = string.Empty;
        private int _iCurrentPageIndex = 0;
        private int _iNextPrintRowIndex = 0;
        private int _iPrintableRowCount = 0;
        private const double INCH = 2.54;

        public void Print(string title, Dictionary<string, string> leftHeader, Dictionary<string, string> rightHeader, string formInfo, bool isPreview, bool isLandscape, int marginLeft, int marginRight, int marginTop, int marginBottom)
        {
            _strPrintTitle = title;
            _dicPrintLeftHeader = leftHeader;
            _dicPrintRightHeader = rightHeader;
            _strPrintFormInfo = formInfo;
            _iCurrentPageIndex = 0;
            _iNextPrintRowIndex = 0;

            this.printDocument1.DefaultPageSettings.Landscape = isLandscape;
            int paperWidth = Convert.ToInt32(21 / INCH * 100);
            int paperHeight = Convert.ToInt32(29.7 / INCH * 100);
            this.printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", paperWidth, paperHeight);

            int marginL = Convert.ToInt32(marginLeft * 100 / INCH);
            int marginR = Convert.ToInt32(marginRight * 100 / INCH);
            int marginT = Convert.ToInt32(marginTop * 100 / INCH);
            int marginB = Convert.ToInt32(marginBottom * 100 / INCH);

            this.printDocument1.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(marginL, marginR, marginT, marginB);

            if (isPreview == true)
            {
                this.printPreviewDialog1.Document = this.printDocument1;
                this.printPreviewDialog1.StartPosition = FormStartPosition.CenterScreen;
                this.printPreviewDialog1.WindowState = FormWindowState.Maximized;
                this.printPreviewDialog1.ShowDialog();
            }
            else
            {
                printDialog1.Document = this.printDocument1;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    this.printDocument1.Print();
                }
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
			//Pen penAreaCheck = new Pen(Color.Red, 1);   //영역확인용 - 완료후 제거
			_iCurrentPageIndex++;

			#region 공통
			StringFormat sf_CC = new StringFormat();
			sf_CC.Alignment = StringAlignment.Center;
			sf_CC.LineAlignment = StringAlignment.Center;

			StringFormat sf_LC = new StringFormat();
			sf_LC.Alignment = StringAlignment.Near;
			sf_LC.LineAlignment = StringAlignment.Center;

			StringFormat sf_RC = new StringFormat();
			sf_RC.Alignment = StringAlignment.Far;
			sf_RC.LineAlignment = StringAlignment.Center;

			Font font_20B = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(129)));
			Font font_10B = new Font("맑은 고딕", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(129)));
			Font font_10R = new Font("맑은 고딕", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(129)));

			int pagePosX = e.MarginBounds.Left;
			int pageWidth = e.MarginBounds.Width;
			int pageHeight = e.MarginBounds.Height;
			double landscapeRatio = this.printDocument1.DefaultPageSettings.Landscape == true ? 29.7 / 21 : 1;
			#endregion

			#region Title Area
			int posYTitle = e.MarginBounds.Top;
			int heightTitle = Convert.ToInt32(e.MarginBounds.Height / 25 * landscapeRatio);

			Rectangle rectTitle = new Rectangle(pagePosX, posYTitle, pageWidth, heightTitle);

			e.Graphics.DrawString("[" + _strPrintTitle + "]", font_20B, Brushes.Black, rectTitle, sf_CC);
			//e.Graphics.DrawRectangle(penAreaCheck, rectTitle);   //영역확인용 - 완료후 제거
			#endregion

			#region Header Area

			int posYPageNumber = Convert.ToInt32(posYTitle + heightTitle + e.MarginBounds.Height / 150 * landscapeRatio);
			int heightPageNumber = Convert.ToInt32(e.MarginBounds.Height / 45 * landscapeRatio);
			int posYHeader = Convert.ToInt32(posYPageNumber + heightPageNumber + e.MarginBounds.Height / 150 * landscapeRatio);
			int heightHeaderUnit = Convert.ToInt32(e.MarginBounds.Height / 50 * landscapeRatio);
			int headerCountMax = Math.Max(_dicPrintLeftHeader.Count, _dicPrintRightHeader.Count);
			int headerIndex = 0;

			foreach (string key in _dicPrintLeftHeader.Keys)
			{
				Rectangle rectHeaderLeft = new Rectangle(pagePosX, posYHeader + headerIndex++ * heightHeaderUnit, pageWidth, heightHeaderUnit);
				e.Graphics.DrawString(key + ": " + _dicPrintLeftHeader[key], font_10B, Brushes.Black, rectHeaderLeft, sf_LC);
				//e.Graphics.DrawRectangle(penAreaCheck, rectHeaderLeft);   //영역확인용 - 완료후 제거
			}

			headerIndex = 0;

			foreach (string key in _dicPrintRightHeader.Keys)
			{
				Rectangle rectHeaderRight = new Rectangle(pagePosX, posYHeader + headerIndex++ * heightHeaderUnit, pageWidth, heightHeaderUnit);
				e.Graphics.DrawString(key + ": " + _dicPrintRightHeader[key], font_10B, Brushes.Black, rectHeaderRight, sf_RC);
				//e.Graphics.DrawRectangle(penAreaCheck, rectHeaderRight);   //영역확인용 - 완료후 제거
			}
			#endregion

			#region Footer Area
			int heightFooter = Convert.ToInt32(e.MarginBounds.Height / 60 * landscapeRatio);
			int posYFooter = e.MarginBounds.Bottom - heightFooter;

			Rectangle rectFooter = new Rectangle(pagePosX, posYFooter, pageWidth, heightFooter);

			e.Graphics.DrawString(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), font_10R, Brushes.Black, rectFooter, sf_LC);
			e.Graphics.DrawString("[" + _strPrintFormInfo + "]", font_10R, Brushes.Black, rectFooter, sf_RC);
			//e.Graphics.DrawRectangle(penAreaCheck, rectFooter);   //영역확인용 - 완료후 제거
			#endregion

			#region Grid Area - ColumnHeader
			int heightRowUnit = Convert.ToInt32(e.MarginBounds.Height / 50 * landscapeRatio);
			int posYColumnHeader = posYHeader + (heightHeaderUnit * headerCountMax) + e.MarginBounds.Height / 150;
			int heightColumnHeader = heightRowUnit;
			int columnCount = this.Columns.Count;

			// 각 칼럼별 너비를 결정한다.
			List<int> widthColumns = new List<int>();
			int gridWidth = 0;

			foreach (DataGridViewColumn column in this.Columns)
			{
				if (column.Visible == true)
				{
					gridWidth += column.Width;
				}
				else
				{
					columnCount--;
				}
			}

			foreach (DataGridViewColumn column in this.Columns)
			{
				if (column.Visible == true)
				{
					widthColumns.Add(Convert.ToInt32(Convert.ToSingle(column.Width) / gridWidth * e.MarginBounds.Width));
				}
			}

			// 각 칼럼을 배경색, 박스, 텍스트 순으로 그린다.
			int posX = pagePosX;
			int hideCount = 0;
			Pen penLine = new Pen(Brushes.Black, 1);

			for (int i = 0; i < Columns.Count; i++)
			{
				DataGridViewColumn column = this.Columns[i];

				if (column.Visible == true)
				{
					Rectangle rectColumnHeader = new Rectangle(posX, posYColumnHeader, widthColumns[i - hideCount], heightRowUnit);

					e.Graphics.FillRectangle(Brushes.LightGray, rectColumnHeader);
					e.Graphics.DrawRectangle(penLine, rectColumnHeader);

					if (column.DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleRight)
					{
						e.Graphics.DrawString(column.HeaderText, font_10R, Brushes.Black, rectColumnHeader, sf_RC);
					}
					else if (column.DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleCenter)
					{
						e.Graphics.DrawString(column.HeaderText, font_10R, Brushes.Black, rectColumnHeader, sf_CC);
					}
					else
					{
						e.Graphics.DrawString(column.HeaderText, font_10R, Brushes.Black, rectColumnHeader, sf_LC);
					}

					posX += widthColumns[i - hideCount];
				}
				else
				{
					hideCount++;
				}
			}
			#endregion

			#region Grid Area - Cell Values
			posYColumnHeader += heightRowUnit;
			int rowLastIndex = 0;
			int spanWidth = 0;
			Rectangle rectCell;
			Brush brushBG;
			Brush brushFont;

			for (int rowIndex = _iNextPrintRowIndex; rowIndex < this.RowCount; rowIndex++)
			{
				DataGridViewRow row = this.Rows[rowIndex];
				posX = pagePosX;
				rowLastIndex++;
				hideCount = 0;
				int hideCouunt2 = 0;

				for (int i = 0; i < Columns.Count; i++)
				{
					DataGridViewCell cell = row.Cells[i];

                    if (Columns[i].Visible == true) //컬럼이 숨겨져있지 않을 경우
					{
                        DataGridViewTextBoxCellEx cellEx = null;

                        if (hideCount != 0 && i == 1)   //숨겨진 컬럼이 있고 i가 1일경우 1-hideCount 하면 숨겨진 컬럼을 가져오기때문에 이조건을 걸어줌
                        {
                            cellEx = row.Cells[i] as DataGridViewTextBoxCellEx;
                        }
                        else
                        {
                            cellEx = row.Cells[i - hideCount] as DataGridViewTextBoxCellEx;
                        }

                        //Cell을 DataGridViewTextBoxCellEx 타입으로 타입캐스팅. (DataGridViewTextBoxCellEx형태 일 경우에 CellMerge가 가능)
                        //DataGridViewTextBoxCellEx cellEx = row.Cells[i - hideCount] as DataGridViewTextBoxCellEx;
                        //DataGridViewTextBoxCellEx cellEx = row.Cells[i] as DataGridViewTextBoxCellEx;

                        

                        //정상적으로 타입캐스팅이 되었을 경우 && 셀 머지가 되어있을 경우
						if (cellEx != null && cellEx.ColumnSpan > 1)                                                               // Merged Cell인 경우
						{
							for (int j = i; j < i + cellEx.ColumnSpan; j++)
							{
								if (Columns[j].Visible == true)
								{
									spanWidth += widthColumns[j - hideCount - hideCouunt2];     //컬럼의 가로값 세팅 
                                    //spanWidth += widthColumns[j - hideCouunt2];
								}
								else
								{
									hideCouunt2++;      
								}
							}

							rectCell = new Rectangle(posX, posYColumnHeader, spanWidth, heightRowUnit);
						}
						else                                                                                    // Merged Cell이 아닌 경우
						{
							rectCell = new Rectangle(posX, posYColumnHeader, widthColumns[i - hideCount - hideCouunt2], heightRowUnit);
						}

						brushFont = GetFontBrush(cell.Style.ForeColor);
						brushBG = GetBgBrush(cell.Style.BackColor);

						e.Graphics.FillRectangle(brushBG, rectCell);
						e.Graphics.DrawRectangle(penLine, rectCell);

						StringFormat sf_rt = null;

						if (cell.Value != null)
						{
							switch (cell.Style.Alignment)
							{
								case DataGridViewContentAlignment.MiddleRight:
									{
										sf_rt = sf_RC;
									}
									break;
								case DataGridViewContentAlignment.MiddleCenter:
									{
										sf_rt = sf_CC;
									}
									break;
								default:
									{
										sf_rt = sf_LC;
									}
									break;
							}
						}

						//if (Columns[i].CellType.Name.Equals("DataGridViewCheckBoxCell") == true)
						//{
						//    e.Graphics.DrawString(cell.Value.ToString(), font_10R, brushFont, rectCell, sf_rt);
						//}
						//else
						{
							e.Graphics.DrawString(cell.FormattedValue.ToString(), font_10R, brushFont, rectCell, sf_rt);
						}

						//DataGridViewTextBoxCellEx cellEx = row.Cells[i] as DataGridViewTextBoxCellEx;

						if (cellEx != null && cellEx.ColumnSpan > 1)
						{
							posX += spanWidth;
							spanWidth = 0;
                            //i += cellEx.ColumnSpan - 1 - hideCount;
                            i += cellEx.ColumnSpan - 1;             //루프돌때 i값 설정
						}
						else
						{
							posX += widthColumns[i - hideCount - hideCouunt2];
						}
					}
					else
					{
						hideCount++;
					}
				}

				posYColumnHeader += heightRowUnit;

				if (posYColumnHeader > posYFooter - heightRowUnit)
				{
					_iNextPrintRowIndex = rowIndex + 1;
					break;
				}
			}

			#endregion


			#region PageNumber Area -- 그리드 개수에 따라서 페이지 수가 결정된다.

			int totalPage = 1;
			_iPrintableRowCount = Math.Max(_iPrintableRowCount, rowLastIndex);

			if (rowLastIndex > 0)
			{
				totalPage = this.RowCount / _iPrintableRowCount;

				if (this.RowCount % _iPrintableRowCount > 0)
				{
					totalPage++;
				}
			}

			Rectangle rectPageNumber = new Rectangle(pagePosX, posYPageNumber, pageWidth, heightPageNumber);

			e.Graphics.DrawString("Page : " + _iCurrentPageIndex.ToString() + " / " + totalPage.ToString(), font_10R, Brushes.Black, rectPageNumber, sf_RC);
			//e.Graphics.DrawRectangle(penAreaCheck, rectPageNumber);   //영역확인용 - 완료후 제거
			#endregion

			if (totalPage > _iCurrentPageIndex)
			{
				e.HasMorePages = true;
			}
			else
			{
				_iNextPrintRowIndex = 0;
			}
		}

        private Brush GetBgBrush(Color color)
        {
            Brush ret_val;

            if (color.A > 0)
            {
                ret_val = new SolidBrush(color);
            }
            else
            {
                ret_val = Brushes.White;
            }

            return ret_val;
        }

        private Brush GetFontBrush(Color color)
        {
            Brush ret_val;

            if (color.A > 0)
            {
                ret_val = new SolidBrush(color);
            }
            else
            {
                ret_val = Brushes.Black;
            }

            return ret_val;
        }
        #endregion



        #region 다중정렬(ColumnHeader Mouse Click) - 정렬초기화(Any ColumnHeader Mouse DouleClick) - 사용안함



		//#region 정렬을 취소하고 원래상태로 되돌린다.

		///// <summary>
		///// 아무 ColumnHeader를 MouseDoubleClick 시 정렬을 초기화한다.
		///// </summary>
		///// <param name="sender"></param>
		///// <param name="e"></param>
		//private void DataGridViewEx_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		//{
		//    if (IsContainsChanges() == true || DataSource != null)
		//    {
		//        return;
		//    }

		//    bool sortable = false;

		//    foreach (DataGridViewColumn col in Columns)
		//    {
		//        if (col.SortMode == DataGridViewColumnSortMode.Programmatic)
		//        {
		//            sortable = true;
		//            break;
		//        }
		//    }

		//    if (sortable == true)
		//    {
		//        CancelSort();
		//    }
		//}

		///// <summary>
		///// 정렬을 초기화한다.
		///// </summary>
		//private void CancelSort()
		//{
		//    _dicSortInformation.Clear();
		//    Sort(Columns[0], ListSortDirection.Ascending);

		//    foreach (DataGridViewColumn column in Columns)
		//    {
		//        column.HeaderCell.SortGlyphDirection = SortOrder.None;
		//    }
		//}

		///// <summary>
		///// 정렬을 초기화할 때만 이 함수로 들어온다.
		///// Sort함수의 Parameter를 다르게 쓰기 때문
		///// </summary>
		///// <param name="sender"></param>
		///// <param name="e"></param>
		//private void DataGridViewEx_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
		//{
		//    e.Handled = true;

		//    object value1 = Rows[e.RowIndex1].HeaderCell.Value;
		//    object value2 = Rows[e.RowIndex2].HeaderCell.Value;

		//    e.SortResult = (value1 as IComparable).CompareTo(value2);
		//}

		//#endregion



		///// <summary>
		///// 마우스로 Column Header가 클릭된 Column을 정렬한다. - 다중정렬가능
		///// </summary>
		///// <param name="sender"></param>
		///// <param name="e"></param>
		//private void DataGridViewEx_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		//{
		//    if (IsContainsChanges() == true || DataSource != null)
		//    {
		//        return;
		//    }

		//    if (Columns[e.ColumnIndex].SortMode == DataGridViewColumnSortMode.Programmatic)
		//    {
		//        string columnName = Columns[e.ColumnIndex].Name;

		//        if (_dicSortInformation.ContainsKey(columnName) == false)
		//        {
		//            _dicSortInformation.Add(Columns[e.ColumnIndex].Name, new KeyValuePair<DataGridViewColumn, bool>(Columns[e.ColumnIndex], true));
		//        }
		//        else
		//        {
		//            KeyValuePair<DataGridViewColumn, bool> values = _dicSortInformation[columnName];

		//            if (values.Value == true)
		//            {
		//                _dicSortInformation.Remove(columnName);
		//                _dicSortInformation.Add(Columns[e.ColumnIndex].Name, new KeyValuePair<DataGridViewColumn, bool>(Columns[e.ColumnIndex], false));
		//            }
		//            else
		//            {
		//                _dicSortInformation.Remove(columnName);

		//            }
		//        }

		//        if (_dicSortInformation.Count > 0)
		//        {
		//            Dictionary<string, KeyValuePair<DataGridViewColumn, bool>> backupDic = _dicSortInformation.Values.ToList().ToDictionary(p => p.Key.Name);

		//            CancelSort();

		//            _dicSortInformation = backupDic.Values.ToList().ToDictionary(p => p.Key.Name);
		//            GridRowComparer rowComparer = new GridRowComparer(_dicSortInformation.Values.ToList());
		//            Sort(rowComparer);
		//            MarkUP(columnName);
		//        }
		//        else
		//        {
		//            CancelSort();
		//        }
		//    }
		//}

		///// <summary>
		///// Column Header의 Sort Direction Glyph를 지정한다.
		///// </summary>
		///// <param name="columnName"></param>
		//private void MarkUP(string columnName)
		//{
		//    // 마우스 클릭된 Column Header의 Name이 p_Dic_SortInformation에 없으면 지워진 것이다.
		//    if (string.IsNullOrEmpty(columnName) == false && _dicSortInformation.ContainsKey(columnName) == true)
		//    {
		//        DataGridViewColumn column = Columns[columnName];
		//        column.HeaderCell.SortGlyphDirection = SortOrder.None;
		//    }

		//    // kvp.Value가 true이면 Ascending, false이면 Descending이다.
		//    foreach (KeyValuePair<DataGridViewColumn, bool> kvp in _dicSortInformation.Values)
		//    {
		//        if (kvp.Value == true)
		//        {
		//            kvp.Key.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
		//        }
		//        else
		//        {
		//            kvp.Key.HeaderCell.SortGlyphDirection = SortOrder.Descending;
		//        }
		//    }
		//}

		///// <summary>
		///// Sortind이 끝나면 RealIndex를 다시 맞춰준다.
		///// </summary>
		///// <param name="sender"></param>
		///// <param name="e"></param>
		//private void DataGridViewEx_Sorted(object sender, EventArgs e)
		//{
		//    _dicRealIndex.Clear();

		//    foreach (DataGridViewRow row in Rows)
		//    {
		//        _dicRealIndex.Add(row.Index, Convert.ToInt32(row.HeaderCell.Value));
		//    }
		//}

        #endregion



		#region 갱신 시 자동으로 이전 Row에 Focus가 Set되도록 함

		private int _iLastRowIndex = -1;
		private int _iLastColumnIndex = -1;
		private List<string> _lstLastKeyValue = new List<string>();
		private DataRow _drNewCurrent = null;

		/// <summary>
		/// Cell이 변경될 때마다 null이 아닌 경우 현재 Cell의 Index를 저장
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DataGridViewEx_CurrentCellChanged(object sender, EventArgs e)
		{
			if (CurrentCell != null)
			{
				_iLastRowIndex = CurrentCell.RowIndex;
				_iLastColumnIndex = CurrentCell.ColumnIndex;
			}
		}

		/// <summary>
		/// DataSource가 바뀐 경우 이전 Row를 찾아 Focus를 Set한다.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DataGridViewEx_DataSourceChanged(object sender, EventArgs e)
		{
			if (_drNewCurrent == null)
			{
				return;
			}

			foreach (DataGridViewRow row in Rows)
			{
				DataRow dr = Methods.GetDataRow(row);

				if (dr.Equals(_drNewCurrent) == true)
				{
					CurrentCell = row.Cells[_iLastColumnIndex];
					//row.Cells[_iLastColumnIndex].Selected = true;
					_drNewCurrent = null;
					break;
				}
			}
		}

		public new object DataSource
		{
			get { return base.DataSource; }
			set
			{
				// Backup
				if (_listPKColumnName == null)
				{
					base.DataSource = value;
					return;
				}

				//if (value == null && _iLastRowIndex >= 0)
				if (_iLastRowIndex >= 0 && _lstLastKeyValue.Count == 0)
				{
                    if (Rows.Count > 0)
                    {
                        DataRow dr = Methods.GetDataRow(Rows[_iLastRowIndex]);

                        foreach (string key in _listPKColumnName)
                        {
                            string pkValue = dr[key].ToString();

                            if (string.IsNullOrEmpty(pkValue) == true)
                            {
                                _lstLastKeyValue.Clear();
                                break;
                            }
                            _lstLastKeyValue.Add(pkValue);
                        }
                    }

					_iLastRowIndex = -1;
				}
				//else if (value != null && _lstLastKeyValue.Count > 0)
				
				if (value != null && _lstLastKeyValue.Count > 0)
				{
					DataTable dt = value as DataTable;
					string columnName = _listPKColumnName[0];
					string pkValue = _lstLastKeyValue[0];

					try
					{

						var query = dt.Select(columnName + " = '" + pkValue + "'");

						for (int i = 1; i < _listPKColumnName.Count; i++)
						{
							columnName = _listPKColumnName[i];
							pkValue = _lstLastKeyValue[i];

							query = dt.Select(columnName + " = '" + pkValue + "'");
						}

						if (query.Count() > 0)
						{
							_drNewCurrent = query[0];
						}
					}
					catch
					{
						_drNewCurrent = null;
					}

					_lstLastKeyValue.Clear();
				}

				base.DataSource = value;
				SetParentChanges(false);
			}
		}

		#endregion



		#region 부모에 변경여부 설정 - 화면을 닫을 때 수정여부에 따라서 메시지 보여주기 위해 사용
		private void SetParentChanges(bool changed)
		{
			Control Parent = this.Parent;

			while ((Parent as Frm_Base) == null)
			{
				if (Parent.Parent == null)
				{
					break;
				}

				Parent = Parent.Parent;
			}

			if ((Parent as Frm_Base) != null)
			{
				(Parent as Frm_Base)._hasChanges = changed;
			}
		}

		private void DataGridViewEx_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			SetParentChanges(true);
		}

		private void DataGridViewEx_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			SetParentChanges(true);
		}
		#endregion



		#region DataSource 연결 시 CheckBoxColumn Check/Uncheck 처리

		private void DataGridViewEx_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex < 0
				|| e.ColumnIndex >= Columns.Count
				|| e.RowIndex < 0
				|| e.RowIndex >= Rows.Count
				|| Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly == true
				)
			{
				return;
			}


			DataGridViewCheckBoxColumn col = Columns[e.ColumnIndex] as DataGridViewCheckBoxColumn;

			if (col == null)
			{
				return;
			}


			if (DataSource == null)
			{
				string columnName = col.Name;
				DataGridViewCheckBoxCell cell = Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewCheckBoxCell;

				if (col.ThreeState == true)
				{
					if (string.IsNullOrEmpty(col.TrueValue as string) == true)
					{
						col.TrueValue = "True";
						col.FalseValue = "False";
						col.IndeterminateValue = "Indeterminate";
					}

					if (cell.Value.Equals(col.TrueValue) == true)
					{
						cell.Value = col.FalseValue;
					}
					else if (cell.Value.Equals(col.IndeterminateValue) == true)
					{
						cell.Value = col.TrueValue;
					}
					else
					{
						cell.Value = col.IndeterminateValue;
					}
				}
				else
				{
					if (string.IsNullOrEmpty(col.TrueValue as string) == true)
					{
						col.TrueValue = "True";
						col.FalseValue = "False";
					}

					if (cell.Value.Equals(col.TrueValue) == true)
					{
						cell.Value = col.FalseValue;
					}
					else
					{
						cell.Value = col.TrueValue;
					}
				}
			}
			else
			{
				string columnName = col.Name;
				DataRow dr = Methods.GetDataRow(Rows[e.RowIndex]);

				if (col.ThreeState == true)
				{
					if (string.IsNullOrEmpty(col.TrueValue as string) == true)
					{
						col.TrueValue = "True";
						col.FalseValue = "False";
						col.IndeterminateValue = "Indeterminate";
					}

					if (dr[columnName].Equals(col.TrueValue) == true)
					{
						dr[columnName] = col.FalseValue;
					}
					else if (dr[columnName].Equals(col.IndeterminateValue) == true)
					{
						dr[columnName] = col.TrueValue;
					}
					else
					{
						dr[columnName] = col.IndeterminateValue;
					}
				}
				else
				{
					if (string.IsNullOrEmpty(col.TrueValue as string) == true)
					{
						col.TrueValue = "True";
						col.FalseValue = "False";
					}

					if (dr[columnName].Equals(col.TrueValue) == true)
					{
						dr[columnName] = col.FalseValue;
					}
					else
					{
						dr[columnName] = col.TrueValue;
					}
				}
			}

		}
		
		#endregion



		#region Combo Cell 키입력 처리
		private string _strComboIndex = string.Empty;
		private ComboBox _ctrlCombo = null;
		private DataTable _dtCombo = null;

		private void DataGridViewEx_CellEnter(object sender, DataGridViewCellEventArgs e)
		{
			_strComboIndex = string.Empty;
		}

		private void DataGridViewEx_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			if (CurrentCell == null)
			{
				return;
			}

			DataGridViewComboBoxColumn comboCol = Columns[CurrentCell.ColumnIndex] as DataGridViewComboBoxColumn;
			if (comboCol == null)
			{
				return;
			}

			_ctrlCombo = e.Control as ComboBox;
			_ctrlCombo.KeyPress += DataGridViewEx_KeyPress;
		}

		private void DataGridViewEx_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (CurrentCell == null)
			{
				return;
			}

			if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != 0x08)
			{
				return;
			}


			if (e.KeyChar == 0x08)
			{
				if (_strComboIndex.Length > 1)
				{
					_strComboIndex = _strComboIndex.Substring(0, _strComboIndex.Length - 1);
				}
				else
				{
					_strComboIndex = "";
				}
			}
			else
			{
				_strComboIndex += e.KeyChar;
			}

			DataGridViewComboBoxCell cell = CurrentCell as DataGridViewComboBoxCell;

			if (cell == null)
			{
				return;
			}

			if (_dtCombo == null)
			{
				_dtCombo = cell.DataSource as DataTable;
			}

			int indexInput = 0;

			if (_strComboIndex.Length > 0)
			{
				indexInput = Convert.ToInt32(_strComboIndex);
			}

			if (indexInput < 0 || indexInput >= _dtCombo.Rows.Count)
			{
				return;
			}

			string valueMember = cell.ValueMember;
			cell.Value = _dtCombo.Rows[indexInput][valueMember];
			_ctrlCombo.SelectedValue = cell.Value;
			this.Refresh();
		}

		#endregion



		#region DataError Handling

		private void DataGridViewEx_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			//아무것도 할 필요없음
		}

		#endregion
	}
}
