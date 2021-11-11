using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron
{
    public struct Elemento
    {
        public List<double> caracteristicas;
        public int clase;

        public Elemento(List<double> caracteristicas, int clase)
        {
            this.caracteristicas = caracteristicas;
            this.clase = clase;
        }
    }
    public class Perceptron
    {
        public List<double> pesos;
        public double learningRate;

        public Perceptron(List<double> pesos)
        {
            this.pesos = pesos;
        }

        public int Supocision(List<double> entrada)
        {
            double suma = 0;
            for(int i = 0 ; i < pesos.Count;++i)
                suma += pesos[i] * entrada[i];
            return suma >= 0 ? 1 : -1;
        }

        public void ActualizaPesos(double error, List<double> entrada)
        {
            for(int i = 0; i < pesos.Count;++i)
                pesos[i] += entrada[i]* (error * learningRate);
        }

        public double evalua(double x)
        {
            //w0x+w1y+w2b = 0
            //w1y = -w0x-w2b
            //y = (-w0x-w2b)/w1
            return (-(pesos[0] * x) - (pesos[2] * -1)) / pesos[1];
        }

    }
}
