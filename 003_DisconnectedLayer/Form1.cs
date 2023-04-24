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

namespace _003_DisconnectedLayer
{
    public partial class Form1 : Form
    {
        private string _dPName;
        private string _connectionString;



        private DbProviderFactory _dbProviderFactory;
        private DbConnection _dbConnection;
        private DbDataAdapter _dataAdapter;

        private DataTable _dataTable;
        private DataSet _dataSet;

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
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button_Execute_Click(object sender, EventArgs e)
        {
            try
            {
                string query = textBox_Query.Text;
                if(query.Length < 12)
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
                        if(lineIndex == 0)          //выгружаю шапку таблицы
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
            DbCommand dbCommand = _dbProviderFactory.CreateCommand();
            dbCommand.Connection = _dbConnection;
            dbCommand.CommandText = "SELECT * FROM INFORMATION_SCHEMA.TABLES";

            _dbConnection.Open();
            DbDataReader dbDataReader;

            using (dbDataReader = dbCommand.ExecuteReader())
            {
                while (dbDataReader.Read())
                {
                    if(dbDataReader["TABLE_NAME"].ToString() != "sysdiagrams")
                        comboBox_selectDB.Items.Add(dbDataReader["TABLE_NAME"].ToString());
                }
            }
            _dbConnection.Close();
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
            SelectFromTable(comboBox_selectDB.SelectedItem.ToString());

        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            try
            {
                DbCommand dbCommandSelect = _dbProviderFactory.CreateCommand();
                dbCommandSelect.Connection = _dbConnection;

                dbCommandSelect.CommandText = "SELECT * FROM users";

                _dataAdapter.SelectCommand = dbCommandSelect;
                if(dataGridView_Results.Columns.Count > 0 ) 
                {
                    dataGridView_Results.Columns[0].ReadOnly = true;
                }
                _dataAdapter.Fill(_dataSet);

                /////________________________UPDATE
                DbCommand dbCommandUpdate = _dbProviderFactory.CreateCommand();
                dbCommandUpdate.Connection = _dbConnection;

                dbCommandUpdate.CommandText = "UPDATE users SET password = @password WHERE Id = @Id";

                DbParameter dbPasswordUpdateParametr = dbCommandUpdate.CreateParameter();
                dbPasswordUpdateParametr.DbType = DbType.String;
                dbPasswordUpdateParametr.ParameterName = "@password";
                dbPasswordUpdateParametr.SourceColumn = "password";
                dbPasswordUpdateParametr.SourceVersion = DataRowVersion.Current;
                dbCommandUpdate.Parameters.Add(dbPasswordUpdateParametr);

                DbParameter dbIdUpdateParametr = dbCommandUpdate.CreateParameter();
                dbIdUpdateParametr.DbType = DbType.Int32;
                dbIdUpdateParametr.ParameterName = "@Id";
                dbIdUpdateParametr.SourceColumn = "Id";
                dbIdUpdateParametr.SourceVersion = DataRowVersion.Original;
                dbCommandUpdate.Parameters.Add(dbIdUpdateParametr);


                /////________________________

                /////________________________INSERT
                DbCommand dbCommandInsert = _dbProviderFactory.CreateCommand();
                dbCommandInsert.Connection = _dbConnection;
                dbCommandInsert.CommandText = "INSERT INTO users (login, email, password) VALUES (@log, @em, @pas)";


                DbParameter dbLoginInsertParametr = dbCommandInsert.CreateParameter();
                dbLoginInsertParametr.DbType= DbType.String;
                dbLoginInsertParametr.ParameterName = "@log";
                dbLoginInsertParametr.SourceColumn = "login";
                dbLoginInsertParametr.SourceVersion = DataRowVersion.Current;
                dbCommandInsert.Parameters.Add(dbLoginInsertParametr);

                DbParameter dbEmailInsertParametr = dbCommandInsert.CreateParameter();
                dbEmailInsertParametr.DbType = DbType.String;
                dbEmailInsertParametr.ParameterName = "@em";
                dbEmailInsertParametr.SourceColumn = "email";
                dbEmailInsertParametr.SourceVersion = DataRowVersion.Current;
                dbCommandInsert.Parameters.Add(dbEmailInsertParametr);

                DbParameter dbPassInsertParametr = dbCommandInsert.CreateParameter();
                dbPassInsertParametr.DbType = DbType.String;
                dbPassInsertParametr.ParameterName = "@pas";
                dbPassInsertParametr.SourceColumn = "password";
                dbPassInsertParametr.SourceVersion = DataRowVersion.Current;
                dbCommandInsert.Parameters.Add(dbPassInsertParametr);
                /////________________________INSERT-------END


                /////________________________DELETE
                DbCommand dbCommandDelete = _dbProviderFactory.CreateCommand();
                dbCommandDelete.Connection = _dbConnection;

                dbCommandDelete.CommandText = "DELETE FROM users WHERE Id = @Id";

                DbParameter dbIdDeleteParametr = dbCommandDelete.CreateParameter();
                dbIdDeleteParametr.DbType = DbType.Int32;
                dbIdDeleteParametr.ParameterName = "@Id";
                dbIdDeleteParametr.SourceColumn = "Id";
                dbIdDeleteParametr.SourceVersion = DataRowVersion.Original;
                dbCommandDelete.Parameters.Add(dbIdDeleteParametr);

                /////________________________DELETE-------END


                _dataAdapter.SelectCommand = dbCommandSelect;
                _dataAdapter.InsertCommand = dbCommandInsert;
                _dataAdapter.DeleteCommand = dbCommandDelete;

                _dataAdapter.UpdateCommand = dbCommandUpdate;
                _dataAdapter.Update(_dataSet);
                _dataSet.Clear();

                _dataAdapter.Fill(_dataSet);
                dataGridView_Results.DataSource = _dataSet.Tables[0];
            }
            catch (Exception ex )
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
    }
}
