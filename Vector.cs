using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron
{
    public class Vector
    {
        public float x, y;
        public Vector(float _x = 0, float _y = 0)
        {
            x = _x;
            y = _y;
        }
        public Vector(Vector v)
        {
            x = v.x;
            y = v.y;
        }

        public static Vector operator +(Vector a, Vector b)
            => new Vector(a.x + b.x, a.y + b.y);
        public static Vector operator +(Vector a, float b)
            => new Vector(a.x + b, a.y + b);
        public static Vector operator -(Vector a, Vector b)
            => new Vector(a.x - b.x, a.y - b.y);
        public static Vector operator *(Vector a, float scalar)
            => new Vector(a.x * scalar, a.y * scalar);
        public override string ToString() => "" + +this.x + " , " + this.y;

        public float Magnitud()
        {
            return (float)Math.Sqrt(x * x + y * y);
        }
        public void Normalizar()
        {
            float temp = Magnitud();
            x /= temp;
            y /= temp;
        }

        public void setMagnitud(float m)
        {
            Normalizar();
            x *= m;
            y *= m;
        }

        public void Limitar(float lim)
        {
            if (lim < Magnitud()) setMagnitud(lim);
        }
    }
}
