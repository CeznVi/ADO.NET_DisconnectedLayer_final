using _003_DisconnectedLayer.Parametr;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace _003_DisconnectedLayer
{
    public partial class Form1 : Form
    {
        private string _dPName;
        private string _connectionString;
        private string _activeTable;

        private DbProviderFactory _dbProviderFactory;
        private DbConnection _dbConnection;
        private DbDataAdapter _dataAdapter;

        private DataTable _dataTable;
        private DataSet _dataSet;

        private int _rowCount;
        private int _cellCount;

        public Form1()
        {
            InitializeComponent();

            _dPName = ConfigurationManager.AppSettings["SQLProvider"];
            _connectionString = ConfigurationManager.ConnectionStrings["SqlProvider"].ConnectionString;

            _dbProviderFactory = DbProviderFactories.GetFactory(_dPName);

            _dbConnection = _dbProviderFactory.CreateConnection();
            _dbConnection.ConnectionString = _connectionString;

            _dataAdapter = _dbProviderFactory.CreateDataAdapter();

            _dataSet = new DataSet();

            GetTableNameIntoCombobox();
            button_Update.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button_Execute_Click(object sender, EventArgs e)
        {
            try
            {
                string query = textBox_Query.Text;
                if (query.Length < 12)
                {
                    throw new Exception("Введите тело запроса");
                    return;
                }
                _dataTable = new DataTable();

                DbCommand dbCommand = _dbProviderFactory.CreateCommand();
                dbCommand.Connection = _dbConnection;
                dbCommand.CommandText = query;
                _dbConnection.Open();

                DbDataReader dbDataReader = dbCommand.ExecuteReader();

                //Формируем DataTable------------------------------------------------start
                int lineIndex = 0;
                do
                {
                    while (dbDataReader.Read())
                    {
                        if (lineIndex == 0)          //выгружаю шапку таблицы
                        {
                            for (int i = 0; i < dbDataReader.FieldCount; i++)
                            {
                                _dataTable.Columns.Add(dbDataReader.GetName(i));
                            }
                            lineIndex++;
                        }
                        DataRow dataRow = _dataTable.NewRow();
                        for (int i = 0; i < dbDataReader.FieldCount; i++)
                        {
                            dataRow[i] = dbDataReader[i];
                        }
                        _dataTable.Rows.Add(dataRow);
                    }

                } while (dbDataReader.NextResult());
                //Формируем DataTable------------------------------------------------end
                dataGridView_Results.DataSource = _dataTable;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        private void button_ExecDataSet_Click(object sender, EventArgs e)
        {
            try
            {
                string query = textBox_Query.Text;
                if (query.Length < 12)
                {
                    throw new Exception("Введите тело запроса");
                    return;
                }
                _dataSet.Clear();
                DbCommand dbCommand = _dbProviderFactory.CreateCommand();
                dbCommand.Connection = _dbConnection;
                dbCommand.CommandText = query;

                _dataAdapter.SelectCommand = dbCommand;

                //_dataAdapter.SelectCommand
                //_dataAdapter.InsertCommand
                //_dataAdapter.UpdateCommand
                //_dataAdapter.DeleteCommand
                _dataAdapter.Fill(_dataSet);

                dataGridView_Results.DataSource = _dataSet.Tables["users"];

                dataGridView_Results.DataSource = _dataSet.Tables[0];
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        private void GetTableNameIntoCombobox()
        {
            try
            {
                DbCommand dbCommand = _dbProviderFactory.CreateCommand();
                dbCommand.Connection = _dbConnection;
                dbCommand.CommandText = "SELECT * FROM INFORMATION_SCHEMA.TABLES";

                _dbConnection.Open();
                DbDataReader dbDataReader;

                using (dbDataReader = dbCommand.ExecuteReader())
                {
                    while (dbDataReader.Read())
                    {
                        if (dbDataReader["TABLE_NAME"].ToString() != "sysdiagrams")
                            comboBox_selectDB.Items.Add(dbDataReader["TABLE_NAME"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        private void SelectFromTable(string tableName)
        {
            try
            {
                _dataTable = new DataTable();

                DbCommand dbCommand = _dbProviderFactory.CreateCommand();
                dbCommand.Connection = _dbConnection;

                dbCommand.CommandText = $"SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'{tableName}'";
                _dbConnection.Open();

                //Формируем DataTable------------------------------------------------start
                using (DbDataReader dbR = dbCommand.ExecuteReader())
                {
                    while (dbR.Read())
                    {
                        _dataTable.Columns.Add(dbR["COLUMN_NAME"].ToString());
                    }
                }

                //Формируем DataTable------------------------------------------------end
                dataGridView_Results.DataSource = _dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        private void comboBox_selectDB_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView_Results.DataSource = null;
            _dataSet.Reset();
            _activeTable = comboBox_selectDB.SelectedItem.ToString();
            ////Активация кнопки Апдейт
            button_Update.Enabled = true;
            button_Update.BackColor = Color.PaleGreen;

            ///заполнение поля
            textBox_Query.Text = $"SELECT * FROM {_activeTable}";
        }

        /// <summary>
        /// Метод формирует ДБкоманду  в зависимости от команды и типа выбраной таблицы
        /// </summary>
        /// <param name="comandType">Ins, Upd, Del</param>
        /// <returns></returns>
        private DbCommand GetDbCommand(string comandType)
        {
            DbCommand dbCommand = _dbProviderFactory.CreateCommand();
            dbCommand.Connection = _dbConnection;

            List<UpdateParam> allParamsUpd = new List<UpdateParam>();

            if (comandType == "Upd")
            {
                dbCommand.CommandText = $"UPDATE {_activeTable} SET ";
            }
            else if(comandType == "Ins")
            {
                dbCommand.CommandText = $"INSERT INTO {_activeTable} ";
            }
            else if (comandType == "Del")
            {
                dbCommand.CommandText = $"DELETE FROM {_activeTable} WHERE Id = @Id";
            }
            else if(comandType == "Select")
            {
                dbCommand.CommandText = $"SELECT * FROM {_activeTable}";
                return dbCommand;
            }


            UpdateParam idParam = new UpdateParam() { NameCol = "Id", Parameter = dbCommand.CreateParameter() };
            idParam.Parameter.DbType = DbType.Int32;
            idParam.Parameter.ParameterName = "@Id";
            idParam.Parameter.SourceColumn = "Id";
            idParam.Parameter.SourceVersion = DataRowVersion.Original;

            if (_activeTable == "users")
            {
                if (comandType == "Ins")
                {
                    dbCommand.CommandText += "(login, email, password) VALUES (";
                }

                UpdateParam loginParam = new UpdateParam() { NameCol = "login", Parameter = dbCommand.CreateParameter() };

                loginParam.Parameter.DbType = DbType.String;
                loginParam.Parameter.ParameterName = "@login";
                loginParam.Parameter.SourceColumn = "login";
                loginParam.Parameter.SourceVersion = DataRowVersion.Current;

                allParamsUpd.Add(loginParam);

                UpdateParam emailParam = new UpdateParam() { NameCol = "email", Parameter = dbCommand.CreateParameter() };
                emailParam.Parameter.DbType = DbType.String;
                emailParam.Parameter.ParameterName = "@email";
                emailParam.Parameter.SourceColumn = "email";
                emailParam.Parameter.SourceVersion = DataRowVersion.Current;
                allParamsUpd.Add(emailParam);

                UpdateParam passParam = new UpdateParam() { NameCol = "password", Parameter = dbCommand.CreateParameter() };
                passParam.Parameter.DbType = DbType.String;
                passParam.Parameter.ParameterName = "@password";
                passParam.Parameter.SourceColumn = "password";
                passParam.Parameter.SourceVersion = DataRowVersion.Current;
                allParamsUpd.Add(passParam);

            }
            else if (_activeTable == "usersInfo")
            {
                if (comandType == "Ins")
                {
                    dbCommand.CommandText += "(userId, fio, inn, birthDate, gender) VALUES (";
                }

                UpdateParam userIdParam = new UpdateParam() { NameCol = "userId", Parameter = dbCommand.CreateParameter() };
                userIdParam.Parameter.DbType = DbType.Int32;
                userIdParam.Parameter.ParameterName = "@userId";
                userIdParam.Parameter.SourceColumn = "userId";
                userIdParam.Parameter.SourceVersion = DataRowVersion.Current;
                allParamsUpd.Add(userIdParam);

                UpdateParam fioParam = new UpdateParam() { NameCol = "fio", Parameter = dbCommand.CreateParameter() };
                fioParam.Parameter.DbType = DbType.String;
                fioParam.Parameter.ParameterName = "@fio";
                fioParam.Parameter.SourceColumn = "fio";
                fioParam.Parameter.SourceVersion = DataRowVersion.Current;
                allParamsUpd.Add(fioParam);

                UpdateParam innParam = new UpdateParam() { NameCol = "inn", Parameter = dbCommand.CreateParameter() };
                innParam.Parameter.DbType = DbType.String;
                innParam.Parameter.ParameterName = "@inn";
                innParam.Parameter.SourceColumn = "inn";
                innParam.Parameter.SourceVersion = DataRowVersion.Current;
                allParamsUpd.Add(innParam);

                UpdateParam bdayParam = new UpdateParam() { NameCol = "birthDate", Parameter = dbCommand.CreateParameter() };
                bdayParam.Parameter.DbType = DbType.DateTime;
                bdayParam.Parameter.ParameterName = "@birthDate";
                bdayParam.Parameter.SourceColumn = "birthDate";
                bdayParam.Parameter.SourceVersion = DataRowVersion.Current;
                allParamsUpd.Add(bdayParam);

                UpdateParam genderParam = new UpdateParam() { NameCol = "gender", Parameter = dbCommand.CreateParameter() };
                genderParam.Parameter.DbType = DbType.String;
                genderParam.Parameter.ParameterName = "@gender";
                genderParam.Parameter.SourceColumn = "gender";
                genderParam.Parameter.SourceVersion = DataRowVersion.Current;
                allParamsUpd.Add(genderParam);
            }

            if (comandType == "Upd")
            {
                foreach (var item in allParamsUpd)
                {
                    dbCommand.CommandText += $"{item.NameCol} = {item.Parameter.ParameterName}, ";
                    dbCommand.Parameters.Add(item.Parameter);
                }
                dbCommand.CommandText = dbCommand.CommandText.Substring(0, dbCommand.CommandText.Length - 2);
                dbCommand.CommandText += $" WHERE {idParam.NameCol} = {idParam.Parameter.ParameterName}";
                dbCommand.Parameters.Add(idParam.Parameter);
            }
            else if (comandType == "Ins")
            {
                foreach (var item in allParamsUpd)
                {
                    dbCommand.CommandText += $"{item.Parameter.ParameterName}, ";
                    dbCommand.Parameters.Add(item.Parameter);
                }
                dbCommand.CommandText = dbCommand.CommandText.Substring(0, dbCommand.CommandText.Length - 2);
                dbCommand.CommandText += $")";
            }
            else if (comandType == "Del")
            {
                dbCommand.Parameters.Add(idParam.Parameter);
            }

            return dbCommand;
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            try
            {
                _cellCount = 0;
                textBox_Query.Text = "";
                DbCommand dbCommandSelect = GetDbCommand("Select");
                _dataAdapter.SelectCommand = dbCommandSelect;
                
                if (dataGridView_Results.Columns.Count > 0)
                {
                    dataGridView_Results.Columns[0].ReadOnly = true;
                }

                _dataAdapter.Fill(_dataSet);
              
                DbCommand dbCommandUpdate = GetDbCommand("Upd");
                DbCommand dbCommandInsert = GetDbCommand("Ins");
                DbCommand dbCommandDelete = GetDbCommand("Del");

                _dataAdapter.UpdateCommand = dbCommandUpdate;
                _dataAdapter.InsertCommand = dbCommandInsert;
                _dataAdapter.DeleteCommand = dbCommandDelete;

                _dataAdapter.Update(_dataSet);

                _dataSet.Reset();
                _dataAdapter.Fill(_dataSet);

                dataGridView_Results.DataSource = _dataSet.Tables[0];
                _rowCount = dataGridView_Results.Rows.Count;
                toolStripStatusLabel.Text = "All changes are saved";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _dbConnection.Close();
            }


        }

        private void button_filterExec_Click(object sender, EventArgs e)
        {
            string filterStr = textBox_Filter.Text;
            
            if( filterStr.Length < 5)
            {
                MessageBox.Show("Заполните поле для ввода фильтра", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if(_dataSet == null || _dataSet.Tables.Count == 0) 
            {
                MessageBox.Show("Невозможно применить фильтра к пустому набору данных", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            DataViewManager dataViewManager = new DataViewManager(_dataSet);
            dataViewManager.DataViewSettings[0].RowFilter = filterStr;

            DataView dataViewFiltered = dataViewManager.CreateDataView(_dataSet.Tables[0]);

            dataGridView_Results.DataSource = dataViewFiltered;

        }

        private void button_SortExec_Click(object sender, EventArgs e)
        {
            string SortedStr = textBox_Sort.Text;

            if (SortedStr.Length < 5)
            {
                MessageBox.Show("Заполните поле для выбора сортировки", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (_dataSet == null || _dataSet.Tables.Count == 0)
            {
                MessageBox.Show("Невозможно применить сортировку к пустому набору данных", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            DataViewManager dataViewManager = new DataViewManager(_dataSet);
            dataViewManager.DataViewSettings[0].Sort = SortedStr;

            DataView dataViewSorted = dataViewManager.CreateDataView(_dataSet.Tables[0]);

            dataGridView_Results.DataSource = dataViewSorted;
        }

        private void dataGridView_Results_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
           toolStripStatusLabel.Text = $"Add rows : { dataGridView_Results.Rows.Count - _rowCount}";
        }

        private void dataGridView_Results_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            toolStripStatusLabel.Text = $"Delete rows : {_rowCount - dataGridView_Results.Rows.Count}";
        }

        private void dataGridView_Results_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            _cellCount++;
            toolStripStatusLabel.Text = $"Changed cells: {_cellCount}";

        }
    }
}
