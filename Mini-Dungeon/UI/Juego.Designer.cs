namespace UI
{
    partial class Juego
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            btnReset = new Button();
            lblPuntos = new Label();
            lblUsuario = new Label();
            btnLogOut = new Button();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Location = new Point(12, 31);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(607, 407);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(647, 332);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(114, 42);
            btnReset.TabIndex = 1;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // lblPuntos
            // 
            lblPuntos.AutoSize = true;
            lblPuntos.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblPuntos.Location = new Point(625, 68);
            lblPuntos.Name = "lblPuntos";
            lblPuntos.Size = new Size(49, 17);
            lblPuntos.TabIndex = 2;
            lblPuntos.Text = "Puntos";
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblUsuario.Location = new Point(625, 31);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(95, 21);
            lblUsuario.TabIndex = 3;
            lblUsuario.Text = "Bienvenido!";
            // 
            // btnLogOut
            // 
            btnLogOut.Location = new Point(647, 391);
            btnLogOut.Name = "btnLogOut";
            btnLogOut.Size = new Size(114, 37);
            btnLogOut.TabIndex = 4;
            btnLogOut.Text = "LogOut";
            btnLogOut.UseVisualStyleBackColor = true;
            btnLogOut.Click += this.btnLogOut_Click;
            // 
            // Juego
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnLogOut);
            Controls.Add(lblUsuario);
            Controls.Add(lblPuntos);
            Controls.Add(btnReset);
            Controls.Add(tableLayoutPanel1);
            Name = "Juego";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button btnReset;
        private Label lblPuntos;
        private Label lblUsuario;
        private Button btnLogOut;
    }
}
