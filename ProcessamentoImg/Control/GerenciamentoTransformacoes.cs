using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessamentoImg.Model;
using System.Drawing;

namespace ProcessamentoImg.Control
{
    class GerenciamentoTransformacoes
    {

        // Retorna a multiplicação de duas matrizes.
        public List<double[]> multiplicar(List<double[]> matriz1, List<double[]> matriz2)
        {
            List<double[]> novaMatriz = new List<double[]>();

            int a = matriz1.Count;
            int b = matriz2[0].Length;
            double[,] array = new double[a, b];

            for (int i = 0; i < matriz1.Count; i++) // Linhas da matriz 1.
            {
                for (int j = 0; j < matriz2[0].Length; j++) // Colunas da matriz 2.
                {
                    for (int k = 0; k < matriz1[0].Length; k++)
                    { // Linhas da matriz 2. 
                        array[i, j] += (matriz1[i][k] * matriz2[k][j]);
                    }
                }
            }
            // Passando para a lista.
            for (int i = 0; i < a; i++)
            {
                double[] linha = new double[b];
                for (int j = 0; j < b; j++)
                {
                    linha[j] = array[i, j];
                }
                novaMatriz.Add(linha);
            }

            return novaMatriz;
        }

        #region Private methods
        private Bitmap ConverteParaBitmap(int[][] matrizImagem, int width, int height)
        {
            Bitmap resultado = new Bitmap(width, height);


            for (int i = 0; i < resultado.Width; i++)
            {
                for (int j = 0; j < resultado.Height; j++)
                {
                    resultado.SetPixel(j, i, Color.FromArgb(255, matrizImagem[i][j], matrizImagem[i][j], matrizImagem[i][j]));
                }
            }
            return resultado;
        }

        // Limpa a imagem para uma única cor.
        private Imagem limparImagem(Imagem img)
        {
            Imagem imagemFormatada = new Imagem(img.width, img.height, img.maxVal, (int[][])img.pixels.Clone());

            for (int i = 1; i < img.width - 1; i++)
            {
                for (int j = 1; j < img.height - 1; j++)
                {
                    imagemFormatada.pixels[i][j] = 255;
                }
            }
            return imagemFormatada;
        }

        private double[] origemPixelCentro = new double[] { 0, 0 };

        #endregion
    }

}
