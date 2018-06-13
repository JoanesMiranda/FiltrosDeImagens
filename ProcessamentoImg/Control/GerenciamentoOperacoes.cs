using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessamentoImg.Model;
using System.Drawing;
using ProcessamentoImg.Utils;

namespace ProcessamentoImg.Control
{
    class GerenciamentoOperacoes
    {

        /// <summary>
        /// Método utlizado para realizar a multiplicação das imagens
        /// </summary>
        /// <param name="img1"></param>
        /// <param name="img2"></param>
        /// <returns>Imagem multiplicada</returns>
        public Bitmap Multiplicacao(Imagem img1, Imagem img2)
        {
            int width = menorWidth(img1.width, img2.width);
            int heigth = menorHeigth(img1.height, img2.height);

            Imagem resultadoImagem = new Imagem(width, heigth, img1.maxVal, (int[][])img1.pixels.Clone());

            //multiplica as imagens
            for (int i = 0; i < resultadoImagem.width; i++)
            {
                for (int j = 0; j < resultadoImagem.height; j++)
                {
                    var multiplicationResult = img1.pixels[i][j] * img2.pixels[i][j];

                    resultadoImagem.pixels[i][j] = multiplicationResult;
                }
            }
            //Para poder atualizar os atributos maximo e mínimo da imagem.
            resultadoImagem = new Imagem(width, heigth, img1.maxVal, (int[][])resultadoImagem.pixels.Clone());
            LeitorImagem leitor = new LeitorImagem(Normalize.NormalizeImage(resultadoImagem));
            return leitor.ConverterParaBitmap();
        }

      
        public int func_normalizacao(Imagem img, int val)
        {
            int valor = 255 * ((val - img.minVal) / (img.maxVal - img.minVal));
            return valor;
        }


        #region Private methods
        private int menorWidth(int width1, int width2)
        {
            if (width1 <= width2)
            {
                return width1;
            }
            else
            {
                return width2;
            }
        }

        private int menorHeigth(int hegth1, int heigth2)
        {
            if (hegth1 <= heigth2)
            {
                return hegth1;
            }
            else
            {
                return heigth2;
            }
        }
        #endregion
    }
}
