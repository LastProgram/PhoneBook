using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PhoneBook
{
    public partial class EditNumber : Form
    {
        private PersonNumberDataBase numbers;
        private DialogType type;
        private TypeNumberDataBase tndb = new TypeNumberDataBase("Anatoly", "Operator");
        public EditNumber(PersonNumberDataBase numbers, DialogType type)
        {
            InitializeComponent();
            this.numbers = numbers;
            this.type = type;
        }

        private void EditNumber_Load(object sender, EventArgs e)
        {
            switch (type)
            {
                case DialogType.Add:
                    LoadForAdd();
                    break;
                case DialogType.Change:
                    LoadForChange();
                    break;
            }

            foreach (string i in tndb.GetListTypeName())
            {
                comboBox1.Items.Add(i);
            }

            if(comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void LoadForAdd()
        {
            this.Text = "Добавить запись";
            button2.Text = "Добавить";
            textBox1.Text = "";
        }
        private void LoadForChange()
        {
            this.Text = "Изменить запись";
            button2.Text = "Изменить";
            textBox1.Text = numbers.FocusNumber.number;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("Введите номер телефона!");
                return;
            }

            int idType = tndb.GetId(comboBox1.SelectedIndex);
            switch (type)
            {
                case DialogType.Add:
                    numbers.AddPersonNumber(textBox1.Text, idType);
                    break;
                case DialogType.Change:
                    numbers.ChangeNumber(idType, textBox1.Text);
                    break;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
