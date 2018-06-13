using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProcessamentoImg.Control;
using ProcessamentoImg.Model;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;

namespace ProcessamentoImg
{
    public partial class Form1 : Form
    {
        Imagem _imagem1 = null;
        Imagem _imagem2 = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void comboBoxOpcoes_SelectedValueChanged(object sender, EventArgs e)
        {
            inputPanel1.Visible = false;
            inputPanel2.Visible = false;
            pictureResultado.Image = null;

            if (comboBoxOpcoes.Text.Equals("Filtros"))
            {
                pictureBox2.Visible = false;
                btn_carregarImg2.Visible = false;
                comboBox2.Visible = true;
                comboBox2.Items.Clear();
                comboBox2.Items.Add("Média");
               
                comboBox2.Items.Add("Negativo");
             
            }
            else if (comboBoxOpcoes.Text.Equals("Operações Numéricas"))
            {
                pictureBox2.Visible = true;
                btn_carregarImg2.Visible = true;
                comboBox2.Visible = true;
                comboBox2.Items.Clear();            
                comboBox2.Items.Add("Multiplicação");
            }
 
        }

        private void btn_carregarImg1_Click(object sender, EventArgs e)
        {
            DialogResult userResult = openFileDialog1.ShowDialog();
            if (userResult == DialogResult.OK)
            {
                LeitorImagem leitor = new LeitorImagem(openFileDialog1.FileName);
                pictureBox1.Image = (Image)leitor.ConverterParaBitmap().Clone();
                _imagem1 = leitor.imagemCarregada;
                pictureBox1.Update();
            }
        }

        private void btn_carregarImg2_Click(object sender, EventArgs e)
        {
            DialogResult userResult = openFileDialog2.ShowDialog();
            if (userResult == DialogResult.OK)
            {
                LeitorImagem leitor = new LeitorImagem(openFileDialog2.FileName);
                pictureBox2.Image = (Image)leitor.ConverterParaBitmap().Clone();
                _imagem2 = leitor.imagemCarregada;
                pictureBox2.Update();
            }
        }

        private void btn_aplicar_Click(object sender, EventArgs e)
        {
            if (_imagem1 == null)
            {
                MessageBox.Show("Por favor selecione uma imagem!");
            }

            if (comboBoxOpcoes.Text.Equals("Filtros"))
            {
                filtros();
            }

            else if (comboBoxOpcoes.Text.Equals("Operações Numéricas"))
            {
                if (_imagem2 == null)
                    MessageBox.Show("Por favor carregue a segunda imagem!");
                else
                    opNumericas();
            }

            else
            {
                MessageBox.Show("Por favor selecione uma opção!");
            }
        }

      
        private void filtros()
        {
            GerenciamentoFiltros gerFiltros = new GerenciamentoFiltros();
            
            string op = comboBox2.Text;
            switch (op)
            {
                case "Média":
                    pictureResultado.Image = (Image)gerFiltros.FiltroMedia(_imagem1).Clone();
                    pictureResultado.Update();
                    break;
                case "Negativo":
                    pictureResultado.Image = (Image)gerFiltros.FiltroNegativo(_imagem1).Clone();
                    pictureResultado.Update();
                    break;
                default:
                    MessageBox.Show("Por favor selecione o filtro que deseja utilizar!");
                    break;
            }
        }

        private void opNumericas()
        {
            GerenciamentoOperacoes gerOperacoes = new GerenciamentoOperacoes();

            string op = comboBox2.Text;
            switch (op)
            {
                case "Multiplicação":
                    pictureResultado.Image = gerOperacoes.Multiplicacao(_imagem1, _imagem2);
                    pictureResultado.Refresh();
                    break;
                default:
                    MessageBox.Show("Por favor selecione a operação que deseja fazer!");
                    break;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
