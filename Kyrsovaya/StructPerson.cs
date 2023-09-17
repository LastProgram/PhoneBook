using System.Windows.Forms;

namespace PhoneBook
{
    public struct Person
    {
        public int idPerson;
        public string surname;
        public string name;
        public string fathername;

        public Person(int idPerson, string surname, string name, string fathername)
        {
            this.idPerson = idPerson;
            this.surname = surname;
            this.name = name;
            this.fathername = fathername;
        }

        public Person(DataGridViewRow row)
        {

            this.idPerson = int.Parse(row.Cells[0].Value.ToString());
            this.surname = row.Cells[1].Value.ToString();
            this.name = row.Cells[2].Value.ToString();
            this.fathername = row.Cells[3].Value.ToString();
        }
    }
}