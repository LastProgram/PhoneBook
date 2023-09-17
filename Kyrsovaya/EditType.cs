using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneBook
{
    public partial class EditType : Form
    {
        private DialogType type;
        private TypeNumberDataBase tndb;
        public EditType(DialogType type)
        {
            InitializeComponent();
            this.type = type;
            tndb = new TypeNumberDataBase("Anatoly", "Operator");
        }

        private void EditType_Load(object sender, EventArgs e)
        {
            listBox1.MultiColumn = false;

            RefreshTypesList();
            switch(type)
            {
                case DialogType.Add:
                    LoadFormAdd();
                    break;
                case DialogType.Change:
                    LoadFormChange();
                    break;
                case DialogType.Delete:
                    LoadFormDelete();
                    break;
            }
        }

        private void LoadFormAdd()
        {
            this.Text = "Добавить тип";
            button1.Text = "Добавить";
            textBox1.Text = "";
            listBox1.Enabled = false;
        }

        private void LoadFormChange()
        {
            this.Text = "Редактирование типов";
            button1.Text = "Изменить";
            if(listBox1.Items.Count > 0)
            {
                listBox1.SelectedIndex = 0;
                textBox1.Text = listBox1.SelectedItem.ToString();
            }
                
        }

        private void LoadFormDelete()
        {
            this.Text = "Удаление записи";
            button1.Text = "Удалить";
            textBox1.Enabled = false;
            if(listBox1.Items.Count > 0)
            {
                listBox1.SelectedIndex = 0;
                textBox1.Text = listBox1.SelectedItem.ToString();
            }
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch(type)
            {
                case DialogType.Add:
                    AddType();
                    break;
                case DialogType.Change:
                    ChangeType();
                    break;
                case DialogType.Delete:
                    DeleteType();
                    break;
            }
        }

        private void RefreshTypesList()
        {
            listBox1.Items.Clear();
            foreach (string i in tndb.GetListTypeName())
            {
                listBox1.Items.Add(i);
            }
        }

        private void AddType()
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("Строка ввода типа должна быть заполнена!");
                return;
            }

            if (!tndb.AddType(textBox1.Text))
            {
                MessageBox.Show("Такой тип уже существует!");
                return;
            }

            RefreshTypesList();
        }

        private void ChangeType()
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("Строка ввода типа должна быть заполнена!");
                return;
            }

            tndb.ChangeType(tndb.GetId(listBox1.SelectedIndex), textBox1.Text);
            RefreshTypesList();
        }

        private void DeleteType()
        {
            tndb.DeleteType(tndb.GetId(listBox1.SelectedIndex));
            RefreshTypesList();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.SelectedItem.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
