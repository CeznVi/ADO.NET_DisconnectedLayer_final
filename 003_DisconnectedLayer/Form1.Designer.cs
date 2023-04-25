namespace _003_DisconnectedLayer
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBox_Query = new System.Windows.Forms.TextBox();
            this.dataGridView_Results = new System.Windows.Forms.DataGridView();
            this.button_Execute = new System.Windows.Forms.Button();
            this.button_ExecDataSet = new System.Windows.Forms.Button();
            this.comboBox_selectDB = new System.Windows.Forms.ComboBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.button_Update = new System.Windows.Forms.Button();
            this.textBox_Filter = new System.Windows.Forms.TextBox();
            this.button_filterExec = new System.Windows.Forms.Button();
            this.textBox_Sort = new System.Windows.Forms.TextBox();
            this.button_SortExec = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Results)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_Query
            // 
            this.textBox_Query.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_Query.Location = new System.Drawing.Point(13, 13);
            this.textBox_Query.Multiline = true;
            this.textBox_Query.Name = "textBox_Query";
            this.textBox_Query.Size = new System.Drawing.Size(664, 83);
            this.textBox_Query.TabIndex = 0;
            // 
            // dataGridView_Results
            // 
            this.dataGridView_Results.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_Results.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Results.Location = new System.Drawing.Point(13, 103);
            this.dataGridView_Results.Name = "dataGridView_Results";
            this.dataGridView_Results.Size = new System.Drawing.Size(664, 335);
            this.dataGridView_Results.TabIndex = 1;
            this.dataGridView_Results.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Results_CellValueChanged);
            this.dataGridView_Results.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView_Results_UserAddedRow);
            this.dataGridView_Results.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView_Results_UserDeletedRow);
            // 
            // button_Execute
            // 
            this.button_Execute.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_Execute.Location = new System.Drawing.Point(684, 103);
            this.button_Execute.Name = "button_Execute";
            this.button_Execute.Size = new System.Drawing.Size(135, 43);
            this.button_Execute.TabIndex = 2;
            this.button_Execute.Text = "Fill DataTable";
            this.button_Execute.UseVisualStyleBackColor = true;
            this.button_Execute.Click += new System.EventHandler(this.button_Execute_Click);
            // 
            // button_ExecDataSet
            // 
            this.button_ExecDataSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_ExecDataSet.Location = new System.Drawing.Point(684, 152);
            this.button_ExecDataSet.Name = "button_ExecDataSet";
            this.button_ExecDataSet.Size = new System.Drawing.Size(135, 43);
            this.button_ExecDataSet.TabIndex = 3;
            this.button_ExecDataSet.Text = "Fill DataSet";
            this.button_ExecDataSet.UseVisualStyleBackColor = true;
            this.button_ExecDataSet.Click += new System.EventHandler(this.button_ExecDataSet_Click);
            // 
            // comboBox_selectDB
            // 
            this.comboBox_selectDB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_selectDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.comboBox_selectDB.FormattingEnabled = true;
            this.comboBox_selectDB.Location = new System.Drawing.Point(684, 13);
            this.comboBox_selectDB.Name = "comboBox_selectDB";
            this.comboBox_selectDB.Size = new System.Drawing.Size(135, 28);
            this.comboBox_selectDB.Sorted = true;
            this.comboBox_selectDB.TabIndex = 4;
            this.comboBox_selectDB.SelectedIndexChanged += new System.EventHandler(this.comboBox_selectDB_SelectedIndexChanged);
            // 
            // button_Update
            // 
            this.button_Update.BackColor = System.Drawing.SystemColors.Control;
            this.button_Update.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.button_Update.Location = new System.Drawing.Point(683, 201);
            this.button_Update.Name = "button_Update";
            this.button_Update.Size = new System.Drawing.Size(135, 43);
            this.button_Update.TabIndex = 5;
            this.button_Update.Text = "Update";
            this.button_Update.UseVisualStyleBackColor = false;
            this.button_Update.Click += new System.EventHandler(this.button_Update_Click);
            // 
            // textBox_Filter
            // 
            this.textBox_Filter.Location = new System.Drawing.Point(683, 303);
            this.textBox_Filter.Name = "textBox_Filter";
            this.textBox_Filter.Size = new System.Drawing.Size(135, 20);
            this.textBox_Filter.TabIndex = 6;
            // 
            // button_filterExec
            // 
            this.button_filterExec.Location = new System.Drawing.Point(683, 329);
            this.button_filterExec.Name = "button_filterExec";
            this.button_filterExec.Size = new System.Drawing.Size(135, 31);
            this.button_filterExec.TabIndex = 7;
            this.button_filterExec.Text = "Filter Exec";
            this.button_filterExec.UseVisualStyleBackColor = true;
            this.button_filterExec.Click += new System.EventHandler(this.button_filterExec_Click);
            // 
            // textBox_Sort
            // 
            this.textBox_Sort.Location = new System.Drawing.Point(683, 381);
            this.textBox_Sort.Name = "textBox_Sort";
            this.textBox_Sort.Size = new System.Drawing.Size(135, 20);
            this.textBox_Sort.TabIndex = 8;
            // 
            // button_SortExec
            // 
            this.button_SortExec.Location = new System.Drawing.Point(683, 407);
            this.button_SortExec.Name = "button_SortExec";
            this.button_SortExec.Size = new System.Drawing.Size(135, 31);
            this.button_SortExec.TabIndex = 9;
            this.button_SortExec.Text = "Sort Exec";
            this.button_SortExec.UseVisualStyleBackColor = true;
            this.button_SortExec.Click += new System.EventHandler(this.button_SortExec_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 439);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(822, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 461);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button_SortExec);
            this.Controls.Add(this.textBox_Sort);
            this.Controls.Add(this.button_filterExec);
            this.Controls.Add(this.textBox_Filter);
            this.Controls.Add(this.button_Update);
            this.Controls.Add(this.comboBox_selectDB);
            this.Controls.Add(this.button_ExecDataSet);
            this.Controls.Add(this.button_Execute);
            this.Controls.Add(this.dataGridView_Results);
            this.Controls.Add(this.textBox_Query);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DiconnectedLayer";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Results)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Query;
        private System.Windows.Forms.DataGridView dataGridView_Results;
        private System.Windows.Forms.Button button_Execute;
        private System.Windows.Forms.Button button_ExecDataSet;
        private System.Windows.Forms.ComboBox comboBox_selectDB;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button button_Update;
        private System.Windows.Forms.TextBox textBox_Filter;
        private System.Windows.Forms.Button button_filterExec;
        private System.Windows.Forms.TextBox textBox_Sort;
        private System.Windows.Forms.Button button_SortExec;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}

