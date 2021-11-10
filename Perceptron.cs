using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron
{
    public struct Elemento
    {
        public Vector pos;
        public float bias;
        public int clase;

        public Elemento(Vector posicion, int clase)
        {
            pos = posicion;
            this.clase = clase;
            bias = 1;
        }
    }
    public class Perceptron
    {
        public Vector pesos;
        public float learningRate, bias;

        public Perceptron(Vector pesos, float bias)
        {
            this.pesos = pesos;
            this.bias = bias;
        }

        public int Supocision(Vector entrada)
        {
            return (pesos.x * entrada.x + pesos.y * entrada.y - bias) >= 0 ?  1 : 0;
        }

        public void ActualizaPesos(float error, Vector entrada, float bias)
        {
            pesos += entrada* (error * learningRate);
            this.bias += (error * learningRate * bias);
        }

        public float evalua(float valor)
        {
            return ((-pesos.x * valor) + bias) / pesos.y;
        }

    }
}
