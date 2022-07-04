using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.YedekMalzemeTakip.Important;
using System.Windows.Forms;

namespace CreateDatabase
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fnVeriTabaniOlustur();
        }

        private void fnVeriTabaniOlustur()
        {
            try
            {
                XpoManager.Instance.InitXpo();

                MessageBox.Show("İşlem tamam");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int Sayi;
            string Deneme;
            DateTime _dTime;


           

         

            int xx = 0;

        }
    }
}
