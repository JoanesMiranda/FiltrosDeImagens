using ProcessamentoImg.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessamentoImg.Utils
{
    public class Normalize
    {
        /// <summary>
        /// Método utilizado para normalizar a imagem para o padrao de 255 bits
        /// </summary>
        /// <param name="image">Imagem a ser normalizada</param>
        /// <returns>Imagem normalizada</returns>
        public static Imagem NormalizeImage(Imagem image)
        {
            for (int i = 0; i < image.width; i++)
            {
                for (int j = 0; j < image.height; j++)
                {
                    if (image.pixels[i][j] > 255)
                    {
                        var pixelAtual = image.pixels[i][j];
                        var numerador = (pixelAtual - image.minVal);
                        var denominador = (image.maxVal - image.minVal);
                        int result = (int)(255 * (((float)numerador) / ((float)denominador)));

                        image.pixels[i][j] = result;
                    }
                    else
                    {
                        if (image.pixels[i][j] < 0)
                            image.pixels[i][j] = 0;
                    }
                }
            }
            return image;
        }
    }
}
