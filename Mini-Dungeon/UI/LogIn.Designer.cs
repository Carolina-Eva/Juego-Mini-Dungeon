namespace UI
{
    partial class LogIn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            groupBox1 = new GroupBox();
            btnLogIn = new Button();
            tbUsuario = new TextBox();
            tbPassword = new TextBox();
            pictureBox1 = new PictureBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(73, 51);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 0;
            label1.Text = "Usuario";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(73, 113);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 1;
            label2.Text = "Contraseña";
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.ButtonFace;
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btnLogIn);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(tbUsuario);
            groupBox1.Controls.Add(tbPassword);
            groupBox1.Location = new Point(396, 80);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(358, 295);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "LogIn";
            // 
            // btnLogIn
            // 
            btnLogIn.BackColor = Color.Cornsilk;
            btnLogIn.Location = new Point(99, 183);
            btnLogIn.Name = "btnLogIn";
            btnLogIn.Size = new Size(156, 60);
            btnLogIn.TabIndex = 2;
            btnLogIn.Text = "Ingresar";
            btnLogIn.UseVisualStyleBackColor = false;
            btnLogIn.Click += btnLogIn_Click;
            // 
            // tbUsuario
            // 
            tbUsuario.Location = new Point(73, 69);
            tbUsuario.Name = "tbUsuario";
            tbUsuario.Size = new Size(213, 23);
            tbUsuario.TabIndex = 1;
            // 
            // tbPassword
            // 
            tbPassword.Location = new Point(73, 131);
            tbPassword.Name = "tbPassword";
            tbPassword.PasswordChar = '*';
            tbPassword.Size = new Size(213, 23);
            tbPassword.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.sokoban;
            pictureBox1.InitialImage = Properties.Resources.sokoban;
            pictureBox1.Location = new Point(37, 80);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(321, 295);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // LogIn
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(800, 450);
            Controls.Add(pictureBox1);
            Controls.Add(groupBox1);
            Name = "LogIn";
            Text = "LogIn";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private GroupBox groupBox1;
        private Button btnLogIn;
        private TextBox tbUsuario;
        private TextBox tbPassword;
        private PictureBox pictureBox1;
    }
}