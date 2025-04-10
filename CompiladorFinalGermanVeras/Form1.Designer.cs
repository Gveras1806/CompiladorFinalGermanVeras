namespace CompiladorFinalGermanVeras
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.entrada = new System.Windows.Forms.TextBox();
            this.compila = new System.Windows.Forms.Button();
            this.sal = new System.Windows.Forms.TextBox();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoProyectoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirProyectoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarComoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.projectTree = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tablaSim = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.codint = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.toke = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.salida = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.errores = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.toke.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // entrada
            // 
            this.entrada.BackColor = System.Drawing.Color.Black;
            this.entrada.Font = new System.Drawing.Font("Consolas", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entrada.ForeColor = System.Drawing.Color.Lime;
            this.entrada.Location = new System.Drawing.Point(6, 42);
            this.entrada.Multiline = true;
            this.entrada.Name = "entrada";
            this.entrada.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.entrada.Size = new System.Drawing.Size(423, 479);
            this.entrada.TabIndex = 0;
            // 
            // compila
            // 
            this.compila.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.compila.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.compila.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.compila.Location = new System.Drawing.Point(347, 6);
            this.compila.Name = "compila";
            this.compila.Size = new System.Drawing.Size(82, 31);
            this.compila.TabIndex = 3;
            this.compila.Text = "Compilar";
            this.compila.UseVisualStyleBackColor = false;
            this.compila.Click += new System.EventHandler(this.button2_Click);
            // 
            // sal
            // 
            this.sal.BackColor = System.Drawing.Color.Black;
            this.sal.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sal.ForeColor = System.Drawing.Color.Lime;
            this.sal.Location = new System.Drawing.Point(596, 69);
            this.sal.Multiline = true;
            this.sal.Name = "sal";
            this.sal.ReadOnly = true;
            this.sal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.sal.Size = new System.Drawing.Size(326, 480);
            this.sal.TabIndex = 12;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.menuStrip1.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivosToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(932, 24);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivosToolStripMenuItem
            // 
            this.archivosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoProyectoToolStripMenuItem,
            this.abrirProyectoToolStripMenuItem,
            this.guardarToolStripMenuItem,
            this.guardarComoToolStripMenuItem});
            this.archivosToolStripMenuItem.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.archivosToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.archivosToolStripMenuItem.Image = global::CompiladorFinalGermanVeras.Properties.Resources.bookmark_9171972__1_;
            this.archivosToolStripMenuItem.Name = "archivosToolStripMenuItem";
            this.archivosToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.archivosToolStripMenuItem.Text = "Archivos";
            this.archivosToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // nuevoProyectoToolStripMenuItem
            // 
            this.nuevoProyectoToolStripMenuItem.Name = "nuevoProyectoToolStripMenuItem";
            this.nuevoProyectoToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.nuevoProyectoToolStripMenuItem.Text = "Nuevo Proyecto";
            this.nuevoProyectoToolStripMenuItem.Click += new System.EventHandler(this.nuevoProyectoToolStripMenuItem_Click);
            // 
            // abrirProyectoToolStripMenuItem
            // 
            this.abrirProyectoToolStripMenuItem.Name = "abrirProyectoToolStripMenuItem";
            this.abrirProyectoToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.abrirProyectoToolStripMenuItem.Text = "Abrir Proyecto";
            this.abrirProyectoToolStripMenuItem.Click += new System.EventHandler(this.abrirProyectoToolStripMenuItem_Click);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.guardarToolStripMenuItem.Text = "Guardar";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.guardarToolStripMenuItem_Click);
            // 
            // guardarComoToolStripMenuItem
            // 
            this.guardarComoToolStripMenuItem.Name = "guardarComoToolStripMenuItem";
            this.guardarComoToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.guardarComoToolStripMenuItem.Text = "Guardar como...";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(1, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.projectTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Panel2.Controls.Add(this.entrada);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.compila);
            this.splitContainer1.Size = new System.Drawing.Size(589, 524);
            this.splitContainer1.SplitterDistance = 153;
            this.splitContainer1.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Image = global::CompiladorFinalGermanVeras.Properties.Resources.bookmark_9171972__1_;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Location = new System.Drawing.Point(4, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Explorador      ";
            // 
            // projectTree
            // 
            this.projectTree.Location = new System.Drawing.Point(3, 26);
            this.projectTree.Name = "projectTree";
            this.projectTree.Size = new System.Drawing.Size(147, 495);
            this.projectTree.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Image = global::CompiladorFinalGermanVeras.Properties.Resources.skip_16785491;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Location = new System.Drawing.Point(3, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Codigo Entrada      ";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Window;
            this.tabPage3.Controls.Add(this.tablaSim);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(910, 272);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Tabla de Simbolos";
            // 
            // tablaSim
            // 
            this.tablaSim.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tablaSim.Location = new System.Drawing.Point(6, 26);
            this.tablaSim.Multiline = true;
            this.tablaSim.Name = "tablaSim";
            this.tablaSim.ReadOnly = true;
            this.tablaSim.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tablaSim.Size = new System.Drawing.Size(898, 240);
            this.tablaSim.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "Tabla de Simbolos";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Window;
            this.tabPage2.Controls.Add(this.codint);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(910, 272);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "Codigo Intermedio";
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // codint
            // 
            this.codint.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codint.Location = new System.Drawing.Point(6, 24);
            this.codint.Multiline = true;
            this.codint.Name = "codint";
            this.codint.ReadOnly = true;
            this.codint.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.codint.Size = new System.Drawing.Size(898, 242);
            this.codint.TabIndex = 16;
            this.codint.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(0, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(122, 17);
            this.label7.TabIndex = 17;
            this.label7.Text = "Codigo Intermedio";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // toke
            // 
            this.toke.BackColor = System.Drawing.SystemColors.Window;
            this.toke.Controls.Add(this.label4);
            this.toke.Controls.Add(this.salida);
            this.toke.Location = new System.Drawing.Point(4, 25);
            this.toke.Name = "toke";
            this.toke.Padding = new System.Windows.Forms.Padding(3);
            this.toke.Size = new System.Drawing.Size(910, 272);
            this.toke.TabIndex = 1;
            this.toke.Text = "TOKEN";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, -1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Token";
            // 
            // salida
            // 
            this.salida.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.salida.Location = new System.Drawing.Point(6, 19);
            this.salida.Multiline = true;
            this.salida.Name = "salida";
            this.salida.ReadOnly = true;
            this.salida.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.salida.Size = new System.Drawing.Size(898, 250);
            this.salida.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Window;
            this.tabPage1.Controls.Add(this.errores);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(910, 272);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ERRORES";
            // 
            // errores
            // 
            this.errores.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errores.Location = new System.Drawing.Point(6, 21);
            this.errores.Multiline = true;
            this.errores.Name = "errores";
            this.errores.ReadOnly = true;
            this.errores.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.errores.Size = new System.Drawing.Size(898, 244);
            this.errores.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Errores";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.toke);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(4, 558);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(918, 301);
            this.tabControl1.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Image = global::CompiladorFinalGermanVeras.Properties.Resources.logout_9975288__1_;
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Location = new System.Drawing.Point(596, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 25);
            this.label6.TabIndex = 13;
            this.label6.Text = "Salida     ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightCoral;
            this.button1.Image = global::CompiladorFinalGermanVeras.Properties.Resources.cleaning_9926109__2_;
            this.button1.Location = new System.Drawing.Point(884, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(38, 37);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.button2.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button2.Location = new System.Drawing.Point(139, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(77, 31);
            this.button2.TabIndex = 5;
            this.button2.Text = "Formato";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(932, 868);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.sal);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Compilador German Veras 1-18-0723";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.toke.ResumeLayout(false);
            this.toke.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox entrada;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button compila;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevoProyectoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirProyectoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarComoToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView projectTree;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox tablaSim;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox codint;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage toke;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox salida;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox errores;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
    }
}

