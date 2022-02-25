using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
//ıntegrated
namespace holding
{
    public partial class Goruntule : Form
    {
        public Goruntule()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Server=Z29-10\\SA;Database=Holding;uid=sa;pwd=11072010");

        private void Listele(string ulas)
        {
            SqlDataAdapter goruntule = new SqlDataAdapter(ulas, baglanti);
            DataSet doldur = new DataSet();
            goruntule.Fill(doldur);
            dataGridView1.DataSource = doldur.Tables[0];
            //DataTable.doldur =new DataTable()
            //
            //

        }

        private void Görüntülee_Click(object sender, EventArgs e)
        {
            Listele("select * from Musteri");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Musteri(MAdSoyad,DogumTarihi,Cinsiyet,Telefon,Resim)values(@MAdSoyad,@DogumTarihi,@Cinsiyet,@Telefon,@Resim)", baglanti);
            komut.Parameters.AddWithValue("@MAdSoyad", textBox2.Text);
            komut.Parameters.AddWithValue("@DogumTarihi", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@Cinsiyet", comboBox1.Text);
            komut.Parameters.AddWithValue("@Telefon", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@Resim", textBox4.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            Listele("select * from Musteri");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from Musteri where MüşteriNo=@MüşteriNo", baglanti);
            komut.Parameters.AddWithValue("@MüşteriNo", textBox1.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            Listele("select * from Musteri");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int sectim = dataGridView1.SelectedCells[0].RowIndex;
            string ID = dataGridView1.Rows[sectim].Cells[0].Value.ToString();
            string MAdSoyad = dataGridView1.Rows[sectim].Cells[1].Value.ToString();
            string DogumTarihi = dataGridView1.Rows[sectim].Cells[2].Value.ToString();
            string Cinsiyet = dataGridView1.Rows[sectim].Cells[3].Value.ToString();
            string Telefon = dataGridView1.Rows[sectim].Cells[4].Value.ToString();
            string Resim = dataGridView1.Rows[sectim].Cells[5].Value.ToString();
            textBox1.Text = ID;
            textBox2.Text = MAdSoyad;
            dateTimePicker1.Text = DogumTarihi;
            comboBox1.Text = Cinsiyet;
            maskedTextBox1.Text = Telefon;
            textBox4.Text = Resim;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand(
                "Update Musteri set MAdSoyad='" + textBox2.Text.ToString() +"',DogumTarihi='" + dateTimePicker1.Value+"',Cinsiyet='" + comboBox1.SelectedItem.ToString()+ "',Telefon='" + maskedTextBox1.Text.ToString()+ "',Resim='" + textBox4.Text.ToString() + "'where MüşteriNo='"+ textBox1.Text.ToString() + "'", baglanti);

            komut.ExecuteNonQuery();
            Listele("select * from Musteri");
            baglanti.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            textBox4.Text = openFileDialog1.FileName;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Musteri where MAdSoyad like '%" + textBox2.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }
    }
}
