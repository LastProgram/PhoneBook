using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneBook
{
    public partial class Form1 : Form
    {
        private PersonDataBase person;
        private PersonNumberDataBase numberPerson;

        public Form1()
        {
            InitializeComponent();
            this.person = new PersonDataBase(dataGridView1, "Anatoly", "Operator");
            this.numberPerson = new PersonNumberDataBase(dataGridView2, "Anatoly", "Operator");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            person.MakeDataGrid();
            numberPerson.MakeDataGrid();

            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView1.MultiSelect = false;
            dataGridView2.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            RefreshData();
        }

        private void toolStripMenuClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if(textBoxSearch.Text != "")
            {
                person.SearchRecord(textBoxSearch.Text);
            }
        }

        private void toolStripMenuAdd_Click(object sender, EventArgs e)
        {
            OpenFormAddPerson();
        }
        private void toolStripMenuEdit_Click(object sender, EventArgs e)
        {
            OpenFormChangePerson();
        }
        private void toolStripMenuDelete_Click(object sender, EventArgs e)
        {
            person.DeleteReccord();
        }
        private void toolStripMenuRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void toolStripAdd_Click(object sender, EventArgs e)
        {
            OpenFormAddPerson();
        }
        private void toolStripChange_Click(object sender, EventArgs e)
        {
            OpenFormChangePerson();
        }
        private void toolStripDelete_Click(object sender, EventArgs e)
        {
            DeletePerson();
        }
        private void toolStripRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            person.FocusRecord = new Person(dataGridView1.CurrentRow);
            numberPerson.IdPerson = person.FocusRecord.idPerson;
            numberPerson.RefreashPersonNumbers();
            if(dataGridView2.RowCount > 0)
            {
                dataGridView2.Rows[0].Selected = true;
                numberPerson.FocusNumber = new Number(dataGridView2.CurrentRow);
            }
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(dataGridView2.RowCount > 0)
            {
                numberPerson.FocusNumber = new Number(dataGridView2.CurrentRow);
            }
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFormAddNumber();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFormChangeNumber();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteNumber();
        }


        private void OpenFormAddPerson()
        {
            EditPerson addPerson = new EditPerson(person, DialogType.Add);
            if (addPerson.ShowDialog() == DialogResult.OK)
            {
                person.RefreshRecord();
                
                dataGridView1.Rows[dataGridView1.RowCount - 1].Selected = true;
                person.FocusRecord = new Person(dataGridView1.Rows[dataGridView1.RowCount - 1]);
                numberPerson.IdPerson = person.FocusRecord.idPerson;
                numberPerson.RefreashPersonNumbers();
            }
        }

        private void OpenFormChangePerson()
        {
            if(dataGridView1.RowCount == 0)
            {
                MessageBox.Show("Нет записей!");
                return;
            }

            int currentIndexRecord = dataGridView1.CurrentRow.Index;
            EditPerson changePerson = new EditPerson(person, DialogType.Change);
            if (changePerson.ShowDialog() == DialogResult.OK)
            {
                person.RefreshRecord();
                dataGridView1.Rows[currentIndexRecord].Selected = true;
                person.FocusRecord = new Person(dataGridView1.CurrentRow);
            }

        }

        private void DeletePerson()
        {
            if(dataGridView1.RowCount == 0)
            {
                MessageBox.Show("Нет записей!");
                return;
            }

            int currentIndexRecord = dataGridView1.CurrentRow.Index;
            DialogResult dialogResult = MessageBox.Show("Удалить запись!", "Удалить", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                for (int i = dataGridView2.RowCount - 1; i >= 0; i--)
                {
                    numberPerson.FocusNumber = new Number(dataGridView2.Rows[i]);
                    numberPerson.DeleteNumber();
                }

                person.DeleteReccord();
                person.RefreshRecord();
                
                if (dataGridView1.RowCount == 0)
                    return;
                if (currentIndexRecord > 0)
                    currentIndexRecord--;
                dataGridView1.Rows[currentIndexRecord].Selected = true;
                person.FocusRecord = new Person(dataGridView1.CurrentRow);
                numberPerson.IdPerson = person.FocusRecord.idPerson;
                numberPerson.RefreashPersonNumbers();

                if(dataGridView2.RowCount > 0)
                {
                    dataGridView2.Rows[0].Selected = true;
                    numberPerson.FocusNumber = new Number(dataGridView2.Rows[0]);
                }
            }
        }

        private void OpenFormAddNumber()
        {
            EditNumber addNumber = new EditNumber(numberPerson, DialogType.Add);
            if (addNumber.ShowDialog() == DialogResult.OK)
            {
                numberPerson.RefreashPersonNumbers();
                dataGridView2.Rows[dataGridView2.RowCount - 1].Selected = true;
                numberPerson.FocusNumber = new Number(dataGridView2.CurrentRow);
            }
        }

        private void OpenFormChangeNumber()
        {
            if (dataGridView2.RowCount == 0)
            {
                MessageBox.Show("Нет записей!");
                return;
            }

            int currentIndexRow = dataGridView2.CurrentRow.Index;

            EditNumber changeNumber = new EditNumber(numberPerson, DialogType.Change);
            if (changeNumber.ShowDialog() == DialogResult.OK)
            {
                numberPerson.RefreashPersonNumbers();
                dataGridView2.Rows[currentIndexRow].Selected = true;
                numberPerson.FocusNumber = new Number(dataGridView2.CurrentRow);
            }
        }

        private void DeleteNumber()
        {
            if (dataGridView2.RowCount == 0)
            {
                MessageBox.Show("Нет записей!");
                return;
            }


            int currentIndexRow = dataGridView2.CurrentRow.Index;
            DialogResult dialogResult = MessageBox.Show("Удалить запись!", "Удалить", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                numberPerson.DeleteNumber();
                numberPerson.RefreashPersonNumbers();

                if (dataGridView2.RowCount == 0)
                    return;
                if (currentIndexRow > 0)
                    currentIndexRow--;

                dataGridView2.Rows[currentIndexRow].Selected = true;
                numberPerson.FocusNumber = new Number(dataGridView2.CurrentRow);

            }
        }

        private void RefreshData()
        {
            person.RefreshRecord();
            if (dataGridView1.RowCount > 0)
            {
                dataGridView1.Rows[0].Selected = true;
                person.FocusRecord = new Person(dataGridView1.Rows[0]);
                numberPerson.IdPerson = person.FocusRecord.idPerson;
            }

            numberPerson.RefreashPersonNumbers();
            if (dataGridView2.RowCount > 0)
            {
                dataGridView2.Rows[0].Selected = true;
                numberPerson.FocusNumber = new Number(dataGridView2.Rows[0]);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            EditType AdditerType = new EditType(DialogType.Add);
            if (AdditerType.ShowDialog() == DialogResult.Cancel) { }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EditType ChangeType = new EditType(DialogType.Change);
            if(ChangeType.ShowDialog() == DialogResult.Cancel)
            {
                numberPerson.RefreashPersonNumbers();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            EditType ChangeType = new EditType(DialogType.Delete);
            if (ChangeType.ShowDialog() == DialogResult.Cancel)
            {
                numberPerson.RefreashPersonNumbers();
            }
        }
    }
}
