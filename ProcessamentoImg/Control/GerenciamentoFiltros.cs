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
    class GerenciamentoFiltros
    {
        #region Fieds
        private Dictionary<int, int> _frequenciaInicial = null;
        private Dictionary<int, int> _frequenciaFinal = null;
        public Dictionary<int, int> FrequenciaInicial { get { return _frequenciaInicial; } }
        public Dictionary<int, int> FrequenciaFinal { get { return _frequenciaFinal; } }
        #endregion

        #region Public methods
        /// <summary>
        /// Método utilizado para realizar a operação de média
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public Bitmap FiltroMedia(Imagem img)
        {
            Imagem resultadoImagem = new Imagem(img.width, img.height, img.maxVal, (int[][])img.pixels.Clone());

            for (int i = 1; i < img.width - 1; i++)
            {
                for (int j = 1; j < img.height - 1; j++)
                {
                    int mask1 = img.pixels[i - 1][j - 1] / 9;
                    int mask2 = img.pixels[i - 1][j] / 9;
                    int mask3 = img.pixels[i - 1][j + 1] / 9;
                    int mask4 = img.pixels[i][j - 1] / 9;
                    int mask5 = img.pixels[i][j] / 9;
                    int mask6 = img.pixels[i][j + 1] / 9;
                    int mask7 = img.pixels[i + 1][j - 1] / 9;
                    int mask8 = img.pixels[i + 1][j] / 9;
                    int mask9 = img.pixels[i + 1][j + 1] / 9;

                    int media = mask1 + mask2 + mask3 + mask4 + mask5 + mask6 + mask7 + mask8 + mask9;
                    resultadoImagem.pixels[i][j] = media;
                }
            }
            //Para poder atualizar os atributos maximo e mínimo da imagem.
            resultadoImagem = new Imagem(img.width, img.height, img.maxVal, (int[][])resultadoImagem.pixels.Clone());

            LeitorImagem leitor = new LeitorImagem(Normalize.NormalizeImage(resultadoImagem));
            return leitor.ConverterParaBitmap();
        }
        
        public Bitmap FiltroNegativo(Imagem img)
        {
            Imagem resultadoImagem = new Imagem(img.width, img.height, img.maxVal, (int[][])img.pixels.Clone());

            for (int i = 0; i < resultadoImagem.width; i++)
            {
                for (int j = 0; j < resultadoImagem.height; j++)
                {
                    resultadoImagem.pixels[i][j] = 255 - img.pixels[i][j];
                }
            }//Para poder atualizar os atributos maximo e mínimo da imagem.
            resultadoImagem = new Imagem(img.width, img.height, img.maxVal, (int[][])resultadoImagem.pixels.Clone());

            LeitorImagem leitor = new LeitorImagem(Normalize.NormalizeImage(resultadoImagem));
            return leitor.ConverterParaBitmap();
        }


        public Bitmap Equalizar(Imagem img)
        {
            Imagem resultadoImagem = new Imagem(img.width, img.height, img.maxVal, (int[][])img.pixels.Clone());
            _frequenciaInicial = new Dictionary<int, int>();
            _frequenciaFinal = new Dictionary<int, int>();

            Dictionary<int, List<double>> frequencia = new Dictionary<int, List<double>>();

            for (int i = 0; i < resultadoImagem.width; i++)
            {
                for (int j = 0; j < resultadoImagem.height; j++)
                {
                    int valor = img.pixels[i][j];
                    if (frequencia.Keys.Contains(valor))
                    {
                        frequencia[valor][0]++;
                        _frequenciaInicial[valor]++;
                    }
                    else
                    {
                        List<double> lista = new List<double>();
                        lista.Add(1);
                        frequencia.Add(valor, lista);
                        _frequenciaInicial.Add(valor, 1);
                    }
                }
            }

            foreach (var item in frequencia.OrderBy(k => k.Key))
            {
                //Calcula a frequencia
                frequencia[item.Key].Add(frequencia[item.Key][0] / (double)(img.height * img.width));
                frequencia[item.Key].Add(frequencia[item.Key][1]);
                if (item.Key != 0)
                {
                    frequencia[item.Key][2] += (frequencia[item.Key - 1][2]);
                }
            }

            Dictionary<int, int> escalaCinza = new Dictionary<int, int>();

            foreach (var item in frequencia.OrderBy(k => k.Key))
            {
                int novoValor = Convert.ToInt32(item.Value[2] * (img.maxVal - 1));
                escalaCinza.Add(Convert.ToInt32(item.Key), novoValor);
            }

            for (int i = 0; i < img.width; i++)
            {
                for (int j = 0; j < img.height; j++)
                {
                    int antigoValor = resultadoImagem.pixels[i][j];
                    resultadoImagem.pixels[i][j] = escalaCinza[antigoValor];
                }
            }

            for (int i = 0; i < resultadoImagem.width; i++)
            {
                for (int j = 0; j < resultadoImagem.height; j++)
                {
                    int valor = resultadoImagem.pixels[i][j];
                    if (_frequenciaFinal.Keys.Contains(valor))
                    {
                        _frequenciaFinal[valor]++;
                    }
                    else
                    {
                        _frequenciaFinal.Add(valor, 1);
                    }
                }
            }
            //Para poder atualizar os atributos maximo e mínimo da imagem.
            resultadoImagem = new Imagem(img.width, img.height, img.maxVal, (int[][])resultadoImagem.pixels.Clone());

            LeitorImagem leitor = new LeitorImagem(Normalize.NormalizeImage(resultadoImagem));
            return leitor.ConverterParaBitmap();
        } 
        #endregion
    }

}
