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
        bool entrenado, flagPesos;
        List<Patron> patrones;
        PointF centro, unidad;
        const int radio = 10;
        Bitmap btmpD, btmpF;
        Perceptron p, a;
        Random r;

        public Form1()
        {
            InitializeComponent();
            r = new Random(DateTime.Now.Second *41 *DateTime.Now.Millisecond * 67);
            entrenado = flagPesos = false;
            patrones = new List<Patron>();
            resizePic();
        }

        #region Dibujado del fondo, funciones de resize y actualización

        private void ResizeTam(object sender, EventArgs e)
        {
            resizePic();
        }
        void resizePic()
        {

            centro = new PointF ( picbox.Width / 2, picbox.Height / 2 );

            btmpD = new Bitmap(picbox.Width, picbox.Height);
            btmpF = new Bitmap(picbox.Width, picbox.Height);

            areaDibujo = Graphics.FromImage(btmpD);
            areaFondo = Graphics.FromImage(btmpF);

            picbox.BackgroundImage = btmpF;
            picbox.Image = btmpD;

            DibujaEje(new PointF ( 10, 10 ));

            DibujaPatron();

            DibujaRecta();

            picbox.Refresh();
        }
        void DibujaPatron()
        {
            foreach (Patron patron in patrones) {
                PointF aux = EsARe(new PointF ((float)patron.atributo[0], (float)patron.atributo[1]));
                if (patron.objetivo == 1) areaFondo.FillRectangle(Brushes.Blue, aux.X - radio, aux.Y - radio, radio * 2, radio * 2);
                else areaFondo.FillEllipse(Brushes.Red, aux.X - radio, aux.Y - radio, radio * 2, radio * 2);
            }
        }
        void DibujaEje(PointF tamanio)//desde el negativo a atributoitivo
        {
            areaFondo.Clear(Color.White);

            areaFondo.DrawLine(new Pen(Color.Black), 0, centro.Y, picbox.Width, centro.Y);//eje x
            areaFondo.DrawLine(new Pen(Color.Black), centro.X, 0, centro.X, picbox.Height);//eje y

            unidad = new PointF ( picbox.Width / tamanio.X, picbox.Height / tamanio.Y );

            for (int x = 0; x < (int)tamanio.X; ++x)
            {
                areaFondo.DrawLine(new Pen(Color.DarkBlue), x * unidad.X, centro.Y - 10, x * unidad.X, centro.Y + 10);//lineas eje x
                areaFondo.DrawString("" + (x - (tamanio.X / 2)), new Font("Arial", 10, FontStyle.Italic), Brushes.DarkBlue, x * unidad.X, centro.Y);
            }
            for (int y = 1; y <= (int)tamanio.Y; ++y)
            {
                areaFondo.DrawLine(new Pen(Color.DarkBlue), centro.X - 10, picbox.Height - (y * unidad.Y), centro.X + 10, picbox.Height - (y * unidad.Y));//lineas eje y
                areaFondo.DrawString("" + (y - (tamanio.Y / 2)), new Font("Arial", 10, FontStyle.Italic), Brushes.DarkBlue, centro.X, picbox.Height - (y * unidad.Y));
            }
        }

        #endregion

        private void Reset_Click(object sender, EventArgs e)
        {
            graficoError.Series["Error Adaline ECM"].Points.Clear();
            graficoError.Series["Error Perceptrón"].Points.Clear();
            patrones = new List<Patron>();
            entrenado = flagPesos = false;
            p = a = null;
            resizePic();
        }
       
        private void picbox_MouseClick(object sender, MouseEventArgs e)
        {
            PointF aux = ReAEs(new PointF (e.X, e.Y));
            List<double> atributo = new List<double> { aux.X, aux.Y, -1};
            if (entrenado)
            {
                int ans = a.Prediccion(atributo);
                if (ans == 1)
                    areaFondo.FillRectangle(Brushes.Blue, e.X - radio, e.Y - radio, radio * 2, radio * 2);
                else
                    areaFondo.FillEllipse(Brushes.Red, e.X - radio, e.Y - radio, radio * 2, radio * 2);
                picbox.Refresh();
                return;
            }
            if (e.Button.Equals(MouseButtons.Right)) {
                patrones.Add(new Patron(atributo, 1));
                areaFondo.FillRectangle(Brushes.Blue, e.X - radio, e.Y - radio, radio * 2, radio * 2);
            }
            else if (e.Button.Equals(MouseButtons.Left)) {
                patrones.Add(new Patron(atributo, -1));
                areaFondo.FillEllipse(Brushes.Red, e.X - radio, e.Y - radio, radio * 2, radio * 2);
            }
            picbox.Refresh();
        }

        #region Inicializa pesos, Perceptron y Adaline
        public double Random(int lim)
        {
            double aux = (r.Next(lim) + r.NextDouble());
            return r.Next(2) == 1 ? aux: -aux;
        }
        private void IniciarPesos_Click(object sender, EventArgs e)
        {
            int lim = 10;
            p = new Perceptron(new List<double> { Random(lim), Random(lim), Random(lim) });//inicializa los pesos
            a = new Perceptron(new List<double>{ Random(lim), Random(lim), Random(lim) });//inicializa los pesos
            flagPesos = true;
            DibujaRecta();
        }
        private void btnPerceptron_Click(object sender, EventArgs e)
        {
            if (!flagPesos) return;
            p.learningRate = (double)nudLR.Value;
            bool needFix = true;
            for (int gen = 1, genLim = (int)nudGen.Value, contE, error; gen <= genLim && needFix; ++gen)
            {
                needFix = false;
                contE = 0;
                foreach (Patron patron in patrones)
                {
                    error = patron.objetivo - p.Prediccion(patron.atributo);
                    if (error != 0)
                    {
                        contE++;
                        needFix = true;
                        p.UpdateWP(error, patron.atributo);
                        DibujaRecta();
                    }
                }
                graficoError.Series["Error Perceptrón"].Points.AddXY("Generación: " + gen, contE);
                graficoError.Update();
            }
            if (!needFix) MessageBox.Show("El perceptrón llegó a una solución");
            else MessageBox.Show("El perceptrón no llegó a una solución");
        }
        private void btnAdaline_Click(object sender, EventArgs e)
        {
            if (!flagPesos) return;
            a.learningRate = (double)nudLR.Value * 2.0;
            double epsilon = (double)nudError.Value, error, eAcum = 1, Fy;
            int genLim = (int)nudGen.Value;
            for(int gen = 1; (gen <= genLim) && (eAcum > epsilon); ++gen)
            {
                eAcum = 0;
                foreach(Patron patron in patrones)
                {
                    Fy = a.Siglog(a.SumWX(patron.atributo));
                    error = (double)patron.objetivo - Fy;
                    a.UpdateWA(patron.atributo, error, Fy);
                    eAcum += error * error * 0.5;
                    DibujaRecta();
                }
                eAcum /= patrones.Count;
                graficoError.Series["Error Adaline ECM"].Points.AddXY("Generación: " + gen, eAcum);
                graficoError.Update();
            }
            entrenado = true;
        }

        public void DibujaRecta()
        {
            areaDibujo.Clear(Color.Transparent);
            PointF p0, p1;
            if (p != null)
            {
                p0 = EsARe(new PointF ( -5, p.Evalua(-5) ));
                p1 = EsARe(new PointF ( 5, p.Evalua(5) ));
                areaDibujo.DrawLine(new Pen(Color.DarkSlateBlue), p0.X, p0.Y, p1.X, p1.Y);
            }
            if (a != null)
            {
                p0 = EsARe(new PointF(-5, a.Evalua(-5)));
                p1 = EsARe(new PointF(5, a.Evalua(5)));
                areaDibujo.DrawLine(new Pen(Color.DarkSalmon), p0.X, p0.Y, p1.X, p1.Y);
            }
            picbox.Refresh();
        }

        #endregion region

        #region Escala
        PointF ReAEs(PointF punto)//real a escala
        {
            return new PointF ( (punto.X - centro.X) / unidad.X, (punto.Y - centro.Y) / -unidad.Y );
        }
        PointF EsARe(PointF punto)//escala a real
        {
            return new PointF ( centro.X + (unidad.X * punto.X), centro.Y + (unidad.Y * -punto.Y) );
        }
        #endregion
    }
}
