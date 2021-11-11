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
        List<float> centro, unidad;
        Bitmap btmpD, btmpF;
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

            centro = new List<float> { picbox.Width / 2, picbox.Height / 2 };

            btmpD = new Bitmap(picbox.Width, picbox.Height);
            btmpF = new Bitmap(picbox.Width, picbox.Height);

            areaDibujo = Graphics.FromImage(btmpD);
            areaFondo = Graphics.FromImage(btmpF);

            picbox.BackgroundImage = btmpF;
            picbox.Image = btmpD;

            DibujaEje(new List<float> { 10, 10 });

            DibujaElementos();

            DibujaRecta();

            picbox.Refresh();
        }
        void DibujaElementos()
        {
            foreach (Elemento e in elementos) {
                List<float> aux = EsARe(new List<float> { (float)e.caracteristicas[0], (float)e.caracteristicas[1]});
                if (e.clase == 1)
                    areaFondo.FillRectangle(Brushes.Blue, aux[0] - radio, aux[1] - radio, radio * 2, radio * 2);
                else
                    areaFondo.FillEllipse(Brushes.Red, aux[0] - radio, aux[1] - radio, radio * 2, radio * 2);
            }
        }
        void DibujaEje(List<float> tamanio)//desde el negativo a caracteristicasitivo
        {
            areaFondo.Clear(Color.White);

            areaFondo.DrawLine(new Pen(Color.Black), 0, centro[1], picbox.Width, centro[1]);//eje x
            areaFondo.DrawLine(new Pen(Color.Black), centro[0], 0, centro[0], picbox.Height);//eje y

            unidad = new List<float> { picbox.Width / tamanio[0], picbox.Height / tamanio[1] };

            for (int x = 0; x < (int)tamanio[0]; ++x)
            {
                areaFondo.DrawLine(new Pen(Color.DarkBlue), x * unidad[0], centro[1] - 10, x * unidad[0], centro[1] + 10);//lineas eje x
                areaFondo.DrawString("" + (x - (tamanio[0] / 2)), new Font("Arial", 10, FontStyle.Italic), Brushes.DarkBlue, x * unidad[0], centro[1]);
            }
            for (int y = 1; y <= (int)tamanio[1]; ++y)
            {
                areaFondo.DrawLine(new Pen(Color.DarkBlue), centro[0] - 10, picbox.Height - (y * unidad[1]), centro[0] + 10, picbox.Height - (y * unidad[1]));//lineas eje y
                areaFondo.DrawString("" + (y - (tamanio[1] / 2)), new Font("Arial", 10, FontStyle.Italic), Brushes.DarkBlue, centro[0], picbox.Height - (y * unidad[1]));
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
            List<float> aux = ReAEs(new List<float> { e.X, e.Y});
            List<double> caracteristicas = new List<double> { aux[0], aux[1], -1};
            if (entrenado)
            {
                int ans = a.Supocision(caracteristicas);
                if (ans == 1)
                    areaFondo.FillRectangle(Brushes.Blue, e.X - radio, e.Y - radio, radio * 2, radio * 2);
                else
                    areaFondo.FillEllipse(Brushes.Red, e.X - radio, e.Y - radio, radio * 2, radio * 2);
                picbox.Refresh();
                return;
            }
            if (e.Button.Equals(MouseButtons.Right)) {
                elementos.Add(new Elemento(caracteristicas, 1));
                areaFondo.FillRectangle(Brushes.Blue, e.X - radio, e.Y - radio, radio * 2, radio * 2);
            }
            else if (e.Button.Equals(MouseButtons.Left)) {
                elementos.Add(new Elemento(caracteristicas, -1));
                areaFondo.FillEllipse(Brushes.Red, e.X - radio, e.Y - radio, radio * 2, radio * 2);
            }
            picbox.Refresh();
        }

        #region Inicializa pesos, Perceptron y Adaline
        public double Random(int lim)
        {
            return (double)(r.Next(11) + r.NextDouble()+(r.Next(1)*-1));
        }
        private void IniciarPesos_Click(object sender, EventArgs e)
        {
            p = new Perceptron(new List<double> { Random(11), Random(11), Random(11)});//inicializa los pesos
            a = new Perceptron(new List<double>{ Random(11), Random(11), Random(11)});//inicializa los pesos
            DibujaRecta();
            flagPesos = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            flagPesos = true;

            List<float> aux = EsARe(new List<float> { -1, -1 });
            elementos.Add(new Elemento(new List<double> { -1, -1, -1}, -1));
            areaFondo.FillRectangle(Brushes.Blue, aux[0] - radio, aux[1] - radio, radio * 2, radio * 2);

            aux = EsARe(new List<float> { 1, -1 });
            elementos.Add(new Elemento(new List<double> { 1, -1, -1 }, 1));
            areaFondo.FillRectangle(Brushes.Red, aux[0] - radio, aux[1] - radio, radio * 2, radio * 2);

            aux = EsARe(new List<float> { 1, 1 });
            elementos.Add(new Elemento(new List<double> { 1, 1, -1 }, 1));
            areaFondo.FillRectangle(Brushes.Red, aux[0] - radio, aux[1] - radio, radio * 2, radio * 2);

            aux = EsARe(new List<float> { -1, 1 });
            elementos.Add(new Elemento(new List<double> { -1, 1, -1 }, 1));
            areaFondo.FillRectangle(Brushes.Red, aux[0] - radio, aux[1] - radio, radio * 2, radio * 2);

            p = new Perceptron(new List<double> { 1, 1, 0.5 });
            p.learningRate = 0.25;
            DibujaRecta();

            if (!flagPesos) return;
            bool needFix = true;
            for (int gen = 0, genLim = (int)nudGen.Value, error; gen < genLim && needFix; ++gen)
            {
                needFix = false;
                foreach (Elemento elemento in elementos)
                {
                    error = elemento.clase - p.Supocision(elemento.caracteristicas);
                    if (error != 0)
                    {
                        needFix = true;
                        p.ActualizaPesos(error, elemento.caracteristicas);
                        DibujaRecta();
                    }
                }
            }
        }

        private void btnPerceptron_Click(object sender, EventArgs e)
        {
            if (!flagPesos) return;
            p.learningRate = (double)nudLR.Value;
            bool needFix = true;
            for (int gen = 0, genLim = (int)nudGen.Value, error; gen < genLim && needFix; ++gen)
            {
                needFix = false;
                foreach (Elemento elemento in elementos)
                {
                    error = elemento.clase - p.Supocision(elemento.caracteristicas);
                    if (error != 0)
                    {
                        needFix = true;
                        p.ActualizaPesos(error, elemento.caracteristicas);
                        DibujaRecta();
                    }
                }
            }
        }
        private void btnAdaline_Click(object sender, EventArgs e)
        {
            if (!flagPesos) return;
            a.learningRate = (double)nudLR.Value;
            bool needFix = true;
            for (int gen = 0, genLim = (int)nudGen.Value, error; gen < genLim && needFix; ++gen)
            {
                needFix = false;
                foreach (Elemento elemento in elementos)
                {
                    error = elemento.clase - a.Supocision(elemento.caracteristicas);
                    if (error != 0)
                    {
                        needFix = true;
                        a.ActualizaPesos(error, elemento.caracteristicas);
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
                List<float> p0 = EsARe(new List<float> { -5, (float)p.evalua(-5) }), p1 = EsARe(new List<float> { 5, (float)p.evalua(5) });
                areaDibujo.DrawLine(new Pen(Color.DarkSlateBlue), p0[0], p0[1], p1[0], p1[1]);
            }
            if (a != null)
            {
                List<float> p0 = EsARe(new List<float> { -5, (float)a.evalua(-5)}), p1 = EsARe(new List<float> {5, (float)a.evalua(5)});
                areaDibujo.DrawLine(new Pen(Color.DarkSalmon), p0[0], p0[1], p1[0], p1[1]);
            }
            picbox.Refresh();
        }

        #endregion region

        #region Escala
        List<float> ReAEs(List<float> punto)//real a escala
        {
            return new List<float> { (punto[0] - centro[0]) / unidad[0], (punto[1] - centro[1]) / -unidad[1] };
        }
        List<float> EsARe(List<float> punto)//escala a real
        {
            return new List<float> { centro[0] + (unidad[0] * punto[0]), centro[1] + (unidad[1] * -punto[1]) };
        }
        #endregion
    }
}
