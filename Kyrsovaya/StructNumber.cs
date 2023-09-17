using System.Windows.Forms;

namespace PhoneBook
{
    public struct Number
    {
        public int id;
        public string number;
        public string type;

        public Number(int id, string number, string type)
        {
            this.id = id;
            this.number = number;
            this.type = type;
        }

        public Number(DataGridViewRow row)
        {
            this.id = int.Parse(row.Cells[0].Value.ToString());
            this.number = row.Cells[1].Value.ToString();
            if (row.Cells[2].Value is null)
            {
                this.type = "";
            }
            else
            {
                this.type = row.Cells[2].Value.ToString();
            }

        }
    }
}