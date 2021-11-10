
namespace Perceptron
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.tablaSeparacion = new System.Windows.Forms.TableLayoutPanel();
            this.pnlParametros = new System.Windows.Forms.Panel();
            this.StartTrainning = new System.Windows.Forms.Button();
            this.Reset = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nudGen = new System.Windows.Forms.NumericUpDown();
            this.nudLR = new System.Windows.Forms.NumericUpDown();
            this.IniciarPesos = new System.Windows.Forms.Button();
            this.picbox = new System.Windows.Forms.PictureBox();
            this.tablaSeparacion.SuspendLayout();
            this.pnlParametros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbox)).BeginInit();
            this.SuspendLayout();
            // 
            // tablaSeparacion
            // 
            this.tablaSeparacion.ColumnCount = 2;
            this.tablaSeparacion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tablaSeparacion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tablaSeparacion.Controls.Add(this.pnlParametros, 1, 0);
            this.tablaSeparacion.Controls.Add(this.picbox, 0, 0);
            this.tablaSeparacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaSeparacion.Location = new System.Drawing.Point(0, 0);
            this.tablaSeparacion.Margin = new System.Windows.Forms.Padding(0);
            this.tablaSeparacion.Name = "tablaSeparacion";
            this.tablaSeparacion.RowCount = 1;
            this.tablaSeparacion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaSeparacion.Size = new System.Drawing.Size(800, 450);
            this.tablaSeparacion.TabIndex = 1;
            // 
            // pnlParametros
            // 
            this.pnlParametros.Controls.Add(this.StartTrainning);
            this.pnlParametros.Controls.Add(this.Reset);
            this.pnlParametros.Controls.Add(this.label2);
            this.pnlParametros.Controls.Add(this.label1);
            this.pnlParametros.Controls.Add(this.nudGen);
            this.pnlParametros.Controls.Add(this.nudLR);
            this.pnlParametros.Controls.Add(this.IniciarPesos);
            this.pnlParametros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlParametros.Location = new System.Drawing.Point(640, 0);
            this.pnlParametros.Margin = new System.Windows.Forms.Padding(0);
            this.pnlParametros.Name = "pnlParametros";
            this.pnlParametros.Size = new System.Drawing.Size(160, 450);
            this.pnlParametros.TabIndex = 0;
            // 
            // StartTrainning
            // 
            this.StartTrainning.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StartTrainning.Location = new System.Drawing.Point(7, 95);
            this.StartTrainning.Name = "StartTrainning";
            this.StartTrainning.Size = new System.Drawing.Size(141, 23);
            this.StartTrainning.TabIndex = 6;
            this.StartTrainning.Text = "Iniciar entrenamiento";
            this.StartTrainning.UseVisualStyleBackColor = true;
            this.StartTrainning.Click += new System.EventHandler(this.StartTrainning_Click);
            // 
            // Reset
            // 
            this.Reset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Reset.Location = new System.Drawing.Point(7, 415);
            this.Reset.Name = "Reset";
            this.Reset.Size = new System.Drawing.Size(141, 23);
            this.Reset.TabIndex = 5;
            this.Reset.Text = "Reiniciar";
            this.Reset.UseVisualStyleBackColor = true;
            this.Reset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Generaciones = ";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Learning Rate = ";
            // 
            // nudGen
            // 
            this.nudGen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudGen.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudGen.Location = new System.Drawing.Point(96, 40);
            this.nudGen.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudGen.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudGen.Name = "nudGen";
            this.nudGen.Size = new System.Drawing.Size(52, 20);
            this.nudGen.TabIndex = 2;
            this.nudGen.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // nudLR
            // 
            this.nudLR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudLR.DecimalPlaces = 3;
            this.nudLR.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudLR.Location = new System.Drawing.Point(96, 14);
            this.nudLR.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            196608});
            this.nudLR.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nudLR.Name = "nudLR";
            this.nudLR.Size = new System.Drawing.Size(52, 20);
            this.nudLR.TabIndex = 1;
            this.nudLR.Value = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            // 
            // IniciarPesos
            // 
            this.IniciarPesos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.IniciarPesos.Location = new System.Drawing.Point(7, 66);
            this.IniciarPesos.Name = "IniciarPesos";
            this.IniciarPesos.Size = new System.Drawing.Size(141, 23);
            this.IniciarPesos.TabIndex = 0;
            this.IniciarPesos.Text = "Iniciar pesos";
            this.IniciarPesos.UseVisualStyleBackColor = true;
            this.IniciarPesos.Click += new System.EventHandler(this.IniciarPesos_Click);
            // 
            // picbox
            // 
            this.picbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picbox.Location = new System.Drawing.Point(0, 0);
            this.picbox.Margin = new System.Windows.Forms.Padding(0);
            this.picbox.Name = "picbox";
            this.picbox.Size = new System.Drawing.Size(640, 450);
            this.picbox.TabIndex = 1;
            this.picbox.TabStop = false;
            this.picbox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DibujaElemento);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tablaSeparacion);
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Perceptron";
            this.SizeChanged += new System.EventHandler(this.ResizeTam);
            this.tablaSeparacion.ResumeLayout(false);
            this.pnlParametros.ResumeLayout(false);
            this.pnlParametros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tablaSeparacion;
        private System.Windows.Forms.Panel pnlParametros;
        private System.Windows.Forms.Button StartTrainning;
        private System.Windows.Forms.Button Reset;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudGen;
        private System.Windows.Forms.NumericUpDown nudLR;
        private System.Windows.Forms.Button IniciarPesos;
        private System.Windows.Forms.PictureBox picbox;
    }
}

