namespace DuocOfCourseAdmin
{
    partial class CorreoEnviado
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
            panel1 = new Panel();
            textLabel = new Label();
            pictureBox1 = new PictureBox();
            button1 = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(textLabel);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(button1);
            panel1.Location = new Point(219, 76);
            panel1.Name = "panel1";
            panel1.Size = new Size(363, 298);
            panel1.TabIndex = 1;
            // 
            // textLabel
            // 
            textLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textLabel.AutoSize = true;
            textLabel.ImageAlign = ContentAlignment.BottomLeft;
            textLabel.Location = new Point(29, 139);
            textLabel.Name = "textLabel";
            textLabel.Size = new Size(38, 15);
            textLabel.TabIndex = 5;
            textLabel.Text = "label1";
            textLabel.TextAlign = ContentAlignment.TopCenter;
            textLabel.Click += textLabel_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top;
            pictureBox1.Image = Properties.Resources.Change_Password_Icon;
            pictureBox1.Location = new Point(112, 34);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(143, 92);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(253, 181, 34);
            button1.Font = new Font("Segoe UI", 12F);
            button1.Location = new Point(112, 228);
            button1.Name = "button1";
            button1.Size = new Size(134, 54);
            button1.TabIndex = 3;
            button1.Text = "Restablecer Contraseña";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // CorreoEnviado
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(1, 44, 86);
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Name = "CorreoEnviado";
            Text = "Correo enviado";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox1;
        private Button button1;
        private Label textLabel;
    }
}