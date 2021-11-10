using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Perceptron
{
    public partial class Form1 : Form
    {
        Graphics areaDibujo, areaFondo;
        Random r;
        Vector centro, unidad;
        Bitmap btmpD, btmpF;
        int generaciones;
        const int radio = 10;
        bool entrenado, flagPesos;
        Perceptron p, a;
        List<Elemento> elementos;

        public Form1()
        {
            InitializeComponent();
            r = new Random(DateTime.Today.Millisecond);
            entrenado = flagPesos = false;
            elementos = new List<Elemento>();
            resizePic();
        }

        #region Dibujado del fondo, funciones de resize y actualización

        private void ResizeTam(object sender, EventArgs e)
        {
            resizePic();
        }
        void resizePic()
        {

            centro = new Vector(picbox.Width / 2, picbox.Height / 2);

            btmpD = new Bitmap(picbox.Width, picbox.Height);
            btmpF = new Bitmap(picbox.Width, picbox.Height);

            areaDibujo = Graphics.FromImage(btmpD);
            areaFondo = Graphics.FromImage(btmpF);

            picbox.BackgroundImage = btmpF;
            picbox.Image = btmpD;

            DibujaEje(new Vector(10, 10));

            DibujaElementos();

            DibujaRecta();

            picbox.Refresh();
        }
        void DibujaElementos()
        {
            foreach(Elemento e in elementos){
                Vector aux = EsARe(e.pos);
                if(e.clase == 1)
                    areaFondo.FillRectangle(Brushes.Blue, aux.x - radio, aux.y - radio, radio * 2, radio * 2);
                else
                    areaFondo.FillEllipse(Brushes.Red, aux.x - radio, aux.y - radio, radio * 2, radio * 2);
            }
        }
        void DibujaEje(Vector tamanio)//desde el negativo a positivo
        {
            areaFondo.Clear(Color.White);

            areaFondo.DrawLine(new Pen(Color.Black), 0, centro.y, picbox.Width, centro.y);//eje x
            areaFondo.DrawLine(new Pen(Color.Black), centro.x, 0, centro.x, picbox.Height);//eje y

            unidad = new Vector(picbox.Width / tamanio.x, picbox.Height / tamanio.y);

            for (int x = 0; x < (int)tamanio.x; ++x)
            {
                areaFondo.DrawLine(new Pen(Color.DarkBlue), x * unidad.x, centro.y - 10, x * unidad.x, centro.y + 10);//lineas eje x
                areaFondo.DrawString("" + (x - (tamanio.x / 2)), new Font("Arial", 10, FontStyle.Italic), Brushes.DarkBlue, x * unidad.x, centro.y);
            }
            for (int y = 1; y <= (int)tamanio.y; ++y)
            {
                areaFondo.DrawLine(new Pen(Color.DarkBlue), centro.x - 10, picbox.Height - (y * unidad.y), centro.x + 10, picbox.Height - (y * unidad.y));//lineas eje y
                areaFondo.DrawString("" + (y - (tamanio.y / 2)), new Font("Arial", 10, FontStyle.Italic), Brushes.DarkBlue, centro.x, picbox.Height - (y * unidad.y));
            }
        }

        #endregion

        private void Reset_Click(object sender, EventArgs e)
        {
            p = null;
            a = null;
            elementos = new List<Elemento>();
            entrenado = false;

            resizePic();
        }
        private void picbox_MouseClick(object sender, MouseEventArgs e)
        {
            Vector pos = ReAEs(new Vector(e.X, e.Y));
            if (entrenado)
            {
                int ans = p.Supocision(pos);
                if (ans == 1)
                    areaFondo.FillRectangle(Brushes.Blue, e.X - radio, e.Y - radio, radio * 2, radio * 2);
                else
                    areaFondo.FillEllipse(Brushes.Red, e.X - radio, e.Y - radio, radio * 2, radio * 2);
                picbox.Refresh();
                return;
            }
            if (e.Button.Equals(MouseButtons.Right))
            {
                elementos.Add(new Elemento(pos, 1));
                areaFondo.FillRectangle(Brushes.Blue, e.X - radio, e.Y - radio, radio * 2, radio * 2);
            }
            else
            {
                elementos.Add(new Elemento(pos, 0));
                areaFondo.FillEllipse(Brushes.Red, e.X - radio, e.Y - radio, radio * 2, radio * 2);
            }
            picbox.Refresh();
        }
        
        #region Inicializa pesos, Perceptron y Adaline
        public float numAleatorio(int lim)
        {
            float aux = (float)(r.Next(11) + r.NextDouble());
            if (r.Next(2) == 0) aux *= -1;
            return aux;
        }
        private void IniciarPesos_Click(object sender, EventArgs e)
        {
            p = new Perceptron(new Vector(numAleatorio(11), numAleatorio(11)), numAleatorio(11));//inicializa los pesos
            a = new Perceptron(new Vector(numAleatorio(11), numAleatorio(11)), numAleatorio(11));//inicializa los pesos
            DibujaRecta();
            flagPesos = true;
        }
        private void btnPerceptron_Click(object sender, EventArgs e)
        {
            if (!flagPesos) return;
            int error = 1;
            generaciones = (int)nudGen.Value;
            p.learningRate = (float)nudLR.Value;
            bool ocurreError = true;
            for (int generacion = 0; generacion < generaciones && ocurreError; ++generacion)
            {
                ocurreError = false;
                foreach (Elemento elemento in elementos)
                {
                    error = elemento.clase - p.Supocision(elemento.pos);
                    if (error != 0)
                    {
                        ocurreError = true;
                        p.ActualizaPesos(error, elemento.pos, elemento.bias);
                        DibujaRecta();
                    }
                }
            }
        }
        private void btnAdaline_Click(object sender, EventArgs e)
        {
            if (!flagPesos) return;
            int error = 1;
            generaciones = (int)nudGen.Value;
            a.learningRate = (float)nudLR.Value;
            bool ocurreError = true;
            for (int generacion = 0; generacion < generaciones && ocurreError; ++generacion)
            {
                ocurreError = false;
                foreach (Elemento elemento in elementos)
                {
                    error = elemento.clase - a.Supocision(elemento.pos);
                    if (error != 0)
                    {
                        ocurreError = true;
                        a.ActualizaPesos(error, elemento.pos, elemento.bias);
                        DibujaRecta();
                    }
                }
            }
            entrenado = true;
        }

        public void DibujaRecta()
        {
            areaDibujo.Clear(Color.Transparent);
            if (p != null)
            {
                Vector p0 = EsARe(new Vector(-5, p.evalua(-5))), p1 = EsARe(new Vector(5, p.evalua(5)));
                areaDibujo.DrawLine(new Pen(Color.DarkSlateBlue), p0.x, p0.y, p1.x, p1.y);
            }
            if (a != null)
            {
                Vector p0 = EsARe(new Vector(-5, a.evalua(-5))), p1 = EsARe(new Vector(5,a.evalua(5)));
                areaDibujo.DrawLine(new Pen(Color.DarkSalmon), p0.x, p0.y, p1.x, p1.y);
            }
            picbox.Refresh();
        }

        #endregion region

        #region Escala
        Vector ReAEs(Vector punto)//real a escala
        {
            return new Vector((punto.x - centro.x)/ unidad.x, (punto.y - centro.y) / -unidad.y);
        }
        Vector EsARe(Vector punto)//escala a real
        {
            return new Vector(centro.x+(unidad.x*punto.x), centro.y+(unidad.y*-punto.y));
        }
        #endregion
    }
}
