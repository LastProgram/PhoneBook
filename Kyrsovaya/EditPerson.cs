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
   
    public partial class EditPerson : Form
    {
        private PersonDataBase person;
        private DialogType type;
        
        public EditPerson(PersonDataBase persons, DialogType type)
        {
            InitializeComponent();
            this.person = persons;
            this.type = type;
        }

        private void EditPerson_Load(object sender, EventArgs e)
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
        }

        private void LoadForAdd()
        {
            this.Text = "Добавить запись";
            button1.Text = "Добавить";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
        private void LoadForChange()
        {
            this.Text = "Изменить запись";
            button1.Text = "Изменить";
            textBox1.Text = person.FocusRecord.surname;
            textBox2.Text = person.FocusRecord.name;
            textBox3.Text = person.FocusRecord.fathername;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Запись обязательно должна содержать имя и фамилию!");
                return;
            }
                
            switch (type)
            {
                case DialogType.Add:
                    person.AddReccord(textBox1.Text, textBox2.Text, textBox3.Text);
                    break;
                case DialogType.Change:
                    person.ChangeRecord(textBox1.Text, textBox2.Text, textBox3.Text);
                    break;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
