using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron
{
    public struct Patron
    {
        public List<double> atributo;
        public int objetivo;

        public Patron(List<double> atributo, int objetivo)
        {
            this.atributo = atributo;
            this.objetivo = objetivo;
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

        public int Prediccion(List<double> entrada)
        {
            double suma = 0;
            for(int i = 0; i < pesos.Count;++i) suma += pesos[i] * entrada[i];
            return suma >= 0 ? 1 : -1;
        }
        public double SumWX(List<double> entrada)//Sumatoria de pesos x entrada
        {
            double suma = 0;
            for(int i = 0 ; i < pesos.Count;++i) suma += pesos[i] * entrada[i];
            return suma;
        }

        public double Siglog(double WX)//Sigmoide Log
        {
            return 1 / (1 + Math.Exp(-WX));
        }

        public void UpdateWP(double error, List<double> entrada)//actualiza pesos como perceptrón
        {
            for(int i = 0; i < pesos.Count;++i) pesos[i] += entrada[i] * error * this.learningRate;
        }
        public void UpdateWA(List<double> entrada, double error,double Fy)//Actualiza los pesos como adaline
        {
            double df = Fy * (1.0 - Fy);
            double compact =  this.learningRate * error * df;
            for (int i = 0; i < pesos.Count;++i) 
                this.pesos[i] += compact * entrada[i];
        }

        public float Evalua(double x)
        {
            //w0x+w1y+w2b = 0
            //w1y = -w0x-w2b
            //y = (-w0x-w2b)/w1
            return (float)(((-pesos[0] * x) - (pesos[2]*-1)) / pesos[1]);
        }

    }
}
