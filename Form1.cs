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
        Vector centro, unidad;
        Bitmap btmpD, btmpF;
        float learningRate;
        int generaciones;
        const int radio = 10;
        bool entrenado;
        Perceptron p;
        List<Elemento> elementos;

        public Form1()
        {
            InitializeComponent();
            entrenado = false;
            elementos = new List<Elemento>();
            resizePic();
        }
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

        private void DibujaElemento(object sender, MouseEventArgs e)
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

        private void IniciarPesos_Click(object sender, EventArgs e)
        {
            p = new Perceptron();//inicializa los pesos
            DibujaRecta();
        }


        private void StartTrainning_Click(object sender, EventArgs e)
        {
            int error = 1;
            generaciones = (int)nudGen.Value;
            p.learningRate = (float)nudLR.Value;
            bool ocurreError = true;
            for (int generacion = 0; generacion < generaciones && ocurreError; ++generacion)
            {
                ocurreError = false;
                foreach (Elemento obj in elementos)
                {
                    error = obj.clase - p.Supocision(obj.pos);
                    if (error != 0)
                    {
                        ocurreError = true;
                        p.ActualizaPesos(error, obj.pos, obj.bias);
                        DibujaRecta();
                    }
                }
            }
            entrenado = true;
        }

        public void DibujaRecta()
        {
            if (p == null) return;
            areaDibujo.Clear(Color.Transparent);
            Vector p0 = EsARe(new Vector(-5, p.evalua(-5))), p1 = EsARe(new Vector(5, p.evalua(5)));
            areaDibujo.DrawLine(new Pen(Color.Orange), p0.x, p0.y, p1.x, p1.y);
            picbox.Refresh();
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            p = null;
            elementos = new List<Elemento>();
            entrenado = false;

            resizePic();
        }

        Vector ReAEs(Vector punto)//real a escala
        {
            return new Vector((punto.x - centro.x)/ unidad.x, (punto.y - centro.y) / -unidad.y);
        }
        Vector EsARe(Vector punto)//escala a real
        {
            return new Vector(centro.x+(unidad.x*punto.x), centro.y+(unidad.y*-punto.y));
        }

        void DibujaEje(Vector tamanio)//desde el negativo a positivo
        {
            areaFondo.Clear(Color.White);
            
            areaFondo.DrawLine(new Pen(Color.Black), 0, centro.y, picbox.Width, centro.y);//eje x
            areaFondo.DrawLine(new Pen(Color.Black), centro.x, 0, centro.x, picbox.Height);//eje y

            unidad = new Vector(picbox.Width / tamanio.x, picbox.Height / tamanio.y);

            for (int x = 1; x <= (int)tamanio.x; ++x)
            {
                areaFondo.DrawLine(new Pen(Color.DarkBlue), x * unidad.x, centro.y - 10, x * unidad.x, centro.y + 10);//lineas eje x
                areaFondo.DrawString(""+(x - (tamanio.x/2)), new Font("Arial", 10, FontStyle.Italic), Brushes.DarkBlue, x * unidad.x, centro.y);
            }
            for (int y = 0; y < (int)tamanio.y; ++y)
            {
                areaFondo.DrawLine(new Pen(Color.DarkBlue), centro.x - 10, picbox.Height - (y * unidad.y), centro.x + 10, picbox.Height - (y * unidad.y));//lineas eje y
                areaFondo.DrawString(""+(y - (tamanio.y / 2)), new Font("Arial", 10, FontStyle.Italic), Brushes.DarkBlue, centro.x, picbox.Height - (y * unidad.y));
            }
        }
    }
}
