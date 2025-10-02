namespace 春节组队双败赛
{
    partial class CForm
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
            team1 = new ComboBox();
            team2 = new ComboBox();
            label1 = new Label();
            vs1 = new Label();
            vs2 = new Label();
            swap = new Button();
            nextTeam = new Button();
            label4 = new Label();
            nextTeam2 = new ComboBox();
            nextTeam1 = new ComboBox();
            exit = new Button();
            label5 = new Label();
            fir1 = new Button();
            fir2 = new Button();
            sec1 = new Button();
            sec2 = new Button();
            nextSec2 = new Button();
            nextSec1 = new Button();
            nextFir2 = new Button();
            nextFir1 = new Button();
            label8 = new Label();
            button2 = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // team1
            // 
            team1.BackColor = Color.FromArgb(38, 38, 38);
            team1.FlatStyle = FlatStyle.Flat;
            team1.Font = new Font("黑体", 18F, FontStyle.Regular, GraphicsUnit.Point);
            team1.ForeColor = Color.FromArgb(215, 190, 135);
            team1.FormattingEnabled = true;
            team1.Location = new Point(12, 127);
            team1.Name = "team1";
            team1.Size = new Size(291, 32);
            team1.TabIndex = 1;
            team1.SelectionChangeCommitted += Team1Choose;
            // 
            // team2
            // 
            team2.BackColor = Color.FromArgb(38, 38, 38);
            team2.FlatStyle = FlatStyle.Flat;
            team2.Font = new Font("黑体", 18F, FontStyle.Regular, GraphicsUnit.Point);
            team2.ForeColor = Color.FromArgb(215, 190, 135);
            team2.FormattingEnabled = true;
            team2.Location = new Point(12, 197);
            team2.Name = "team2";
            team2.Size = new Size(291, 32);
            team2.TabIndex = 2;
            team2.SelectedIndexChanged += team2_SelectedIndexChanged;
            team2.SelectionChangeCommitted += Team2Choose;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Source Code Pro", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(215, 190, 135);
            label1.Location = new Point(141, 166);
            label1.Name = "label1";
            label1.Size = new Size(32, 24);
            label1.TabIndex = 3;
            label1.Text = "VS";
            // 
            // vs1
            // 
            vs1.AutoSize = true;
            vs1.Font = new Font("楷体", 15F, FontStyle.Bold, GraphicsUnit.Point);
            vs1.ForeColor = Color.FromArgb(215, 190, 135);
            vs1.Location = new Point(142, 261);
            vs1.Name = "vs1";
            vs1.Size = new Size(31, 20);
            vs1.TabIndex = 4;
            vs1.Text = "VS";
            vs1.Click += VSlabel;
            // 
            // vs2
            // 
            vs2.AutoSize = true;
            vs2.Font = new Font("楷体", 15F, FontStyle.Bold, GraphicsUnit.Point);
            vs2.ForeColor = Color.FromArgb(215, 190, 135);
            vs2.Location = new Point(142, 310);
            vs2.Name = "vs2";
            vs2.Size = new Size(31, 20);
            vs2.TabIndex = 5;
            vs2.Text = "VS";
            vs2.Click += VSlabel;
            // 
            // swap
            // 
            swap.BackColor = Color.FromArgb(88, 88, 88);
            swap.FlatAppearance.BorderSize = 2;
            swap.FlatStyle = FlatStyle.Flat;
            swap.Font = new Font("黑体", 15F, FontStyle.Bold, GraphicsUnit.Point);
            swap.ForeColor = Color.FromArgb(215, 190, 135);
            swap.Location = new Point(30, 864);
            swap.Name = "swap";
            swap.Size = new Size(63, 32);
            swap.TabIndex = 6;
            swap.Text = "换位";
            swap.UseVisualStyleBackColor = false;
            swap.Click += swap_Click;
            // 
            // nextTeam
            // 
            nextTeam.BackColor = Color.FromArgb(88, 88, 88);
            nextTeam.FlatAppearance.BorderSize = 2;
            nextTeam.FlatStyle = FlatStyle.Flat;
            nextTeam.Font = new Font("黑体", 15F, FontStyle.Bold, GraphicsUnit.Point);
            nextTeam.ForeColor = Color.FromArgb(215, 190, 135);
            nextTeam.Location = new Point(124, 864);
            nextTeam.Name = "nextTeam";
            nextTeam.Size = new Size(63, 32);
            nextTeam.TabIndex = 7;
            nextTeam.Text = "继续";
            nextTeam.UseVisualStyleBackColor = false;
            nextTeam.Click += nextTeam_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Source Code Pro", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.FromArgb(215, 190, 135);
            label4.Location = new Point(141, 541);
            label4.Name = "label4";
            label4.Size = new Size(32, 24);
            label4.TabIndex = 10;
            label4.Text = "VS";
            // 
            // nextTeam2
            // 
            nextTeam2.BackColor = Color.FromArgb(38, 38, 38);
            nextTeam2.FlatStyle = FlatStyle.Flat;
            nextTeam2.Font = new Font("黑体", 18F, FontStyle.Regular, GraphicsUnit.Point);
            nextTeam2.ForeColor = Color.FromArgb(215, 190, 135);
            nextTeam2.FormattingEnabled = true;
            nextTeam2.Location = new Point(12, 572);
            nextTeam2.Name = "nextTeam2";
            nextTeam2.Size = new Size(291, 32);
            nextTeam2.TabIndex = 9;
            nextTeam2.SelectedIndexChanged += NTeam2Choose;
            // 
            // nextTeam1
            // 
            nextTeam1.BackColor = Color.FromArgb(38, 38, 38);
            nextTeam1.FlatStyle = FlatStyle.Flat;
            nextTeam1.Font = new Font("黑体", 18F, FontStyle.Regular, GraphicsUnit.Point);
            nextTeam1.ForeColor = Color.FromArgb(215, 190, 135);
            nextTeam1.FormattingEnabled = true;
            nextTeam1.Location = new Point(12, 502);
            nextTeam1.Name = "nextTeam1";
            nextTeam1.Size = new Size(291, 32);
            nextTeam1.TabIndex = 8;
            nextTeam1.SelectedIndexChanged += NTeam1Choose;
            // 
            // exit
            // 
            exit.BackColor = Color.FromArgb(88, 88, 88);
            exit.FlatAppearance.BorderSize = 2;
            exit.FlatStyle = FlatStyle.Flat;
            exit.Font = new Font("黑体", 15F, FontStyle.Bold, GraphicsUnit.Point);
            exit.ForeColor = Color.FromArgb(215, 190, 135);
            exit.Location = new Point(221, 864);
            exit.Name = "exit";
            exit.Size = new Size(63, 32);
            exit.TabIndex = 11;
            exit.Text = "退出";
            exit.UseVisualStyleBackColor = false;
            exit.Click += exit_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("黑体", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.FromArgb(215, 190, 135);
            label5.Location = new Point(118, 462);
            label5.Name = "label5";
            label5.Size = new Size(79, 21);
            label5.TabIndex = 12;
            label5.Text = "下一组";
            // 
            // fir1
            // 
            fir1.BackColor = Color.FromArgb(88, 88, 88);
            fir1.FlatAppearance.BorderSize = 2;
            fir1.FlatStyle = FlatStyle.Flat;
            fir1.Font = new Font("黑体", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            fir1.ForeColor = Color.FromArgb(215, 190, 135);
            fir1.Location = new Point(12, 254);
            fir1.Name = "fir1";
            fir1.Size = new Size(124, 32);
            fir1.TabIndex = 13;
            fir1.UseVisualStyleBackColor = false;
            fir1.Click += ChangeColor;
            // 
            // fir2
            // 
            fir2.BackColor = Color.FromArgb(88, 88, 88);
            fir2.FlatAppearance.BorderSize = 2;
            fir2.FlatStyle = FlatStyle.Flat;
            fir2.Font = new Font("黑体", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            fir2.ForeColor = Color.FromArgb(215, 190, 135);
            fir2.Location = new Point(179, 254);
            fir2.Name = "fir2";
            fir2.Size = new Size(124, 32);
            fir2.TabIndex = 14;
            fir2.UseVisualStyleBackColor = false;
            fir2.Click += ChangeColor;
            // 
            // sec1
            // 
            sec1.BackColor = Color.FromArgb(88, 88, 88);
            sec1.FlatAppearance.BorderSize = 2;
            sec1.FlatStyle = FlatStyle.Flat;
            sec1.Font = new Font("黑体", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            sec1.ForeColor = Color.FromArgb(215, 190, 135);
            sec1.Location = new Point(12, 303);
            sec1.Name = "sec1";
            sec1.Size = new Size(124, 32);
            sec1.TabIndex = 15;
            sec1.UseVisualStyleBackColor = false;
            sec1.Click += ChangeColor;
            // 
            // sec2
            // 
            sec2.BackColor = Color.FromArgb(88, 88, 88);
            sec2.FlatAppearance.BorderSize = 2;
            sec2.FlatStyle = FlatStyle.Flat;
            sec2.Font = new Font("黑体", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            sec2.ForeColor = Color.FromArgb(215, 190, 135);
            sec2.Location = new Point(179, 303);
            sec2.Name = "sec2";
            sec2.Size = new Size(124, 32);
            sec2.TabIndex = 16;
            sec2.UseVisualStyleBackColor = false;
            sec2.Click += ChangeColor;
            // 
            // nextSec2
            // 
            nextSec2.BackColor = Color.FromArgb(88, 88, 88);
            nextSec2.FlatAppearance.BorderSize = 2;
            nextSec2.FlatStyle = FlatStyle.Flat;
            nextSec2.Font = new Font("黑体", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            nextSec2.ForeColor = Color.FromArgb(215, 190, 135);
            nextSec2.Location = new Point(196, 679);
            nextSec2.Name = "nextSec2";
            nextSec2.Size = new Size(108, 32);
            nextSec2.TabIndex = 22;
            nextSec2.UseVisualStyleBackColor = false;
            // 
            // nextSec1
            // 
            nextSec1.BackColor = Color.FromArgb(88, 88, 88);
            nextSec1.FlatAppearance.BorderSize = 2;
            nextSec1.FlatStyle = FlatStyle.Flat;
            nextSec1.Font = new Font("黑体", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            nextSec1.ForeColor = Color.FromArgb(215, 190, 135);
            nextSec1.Location = new Point(82, 679);
            nextSec1.Name = "nextSec1";
            nextSec1.Size = new Size(108, 32);
            nextSec1.TabIndex = 21;
            nextSec1.UseVisualStyleBackColor = false;
            // 
            // nextFir2
            // 
            nextFir2.BackColor = Color.FromArgb(88, 88, 88);
            nextFir2.FlatAppearance.BorderSize = 2;
            nextFir2.FlatStyle = FlatStyle.Flat;
            nextFir2.Font = new Font("黑体", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            nextFir2.ForeColor = Color.FromArgb(215, 190, 135);
            nextFir2.Location = new Point(196, 630);
            nextFir2.Name = "nextFir2";
            nextFir2.Size = new Size(108, 32);
            nextFir2.TabIndex = 20;
            nextFir2.UseVisualStyleBackColor = false;
            // 
            // nextFir1
            // 
            nextFir1.BackColor = Color.FromArgb(88, 88, 88);
            nextFir1.FlatAppearance.BorderSize = 2;
            nextFir1.FlatStyle = FlatStyle.Flat;
            nextFir1.Font = new Font("黑体", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            nextFir1.ForeColor = Color.FromArgb(215, 190, 135);
            nextFir1.Location = new Point(82, 630);
            nextFir1.Name = "nextFir1";
            nextFir1.Size = new Size(108, 32);
            nextFir1.TabIndex = 19;
            nextFir1.UseVisualStyleBackColor = false;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("黑体", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label8.ForeColor = Color.FromArgb(215, 190, 135);
            label8.Location = new Point(118, 84);
            label8.Name = "label8";
            label8.Size = new Size(79, 21);
            label8.TabIndex = 23;
            label8.Text = "春节杯";
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(88, 88, 88);
            button2.FlatAppearance.BorderSize = 2;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("黑体", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            button2.ForeColor = Color.FromArgb(215, 190, 135);
            button2.Location = new Point(10, 630);
            button2.Name = "button2";
            button2.Size = new Size(66, 32);
            button2.TabIndex = 25;
            button2.Text = "先锋";
            button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(88, 88, 88);
            button1.FlatAppearance.BorderSize = 2;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("黑体", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            button1.ForeColor = Color.FromArgb(215, 190, 135);
            button1.Location = new Point(10, 679);
            button1.Name = "button1";
            button1.Size = new Size(66, 32);
            button1.TabIndex = 26;
            button1.Text = "大将";
            button1.UseVisualStyleBackColor = false;
            // 
            // CForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(38, 38, 38);
            ClientSize = new Size(315, 1061);
            Controls.Add(button1);
            Controls.Add(button2);
            Controls.Add(label8);
            Controls.Add(nextSec2);
            Controls.Add(nextSec1);
            Controls.Add(nextFir2);
            Controls.Add(nextFir1);
            Controls.Add(sec2);
            Controls.Add(sec1);
            Controls.Add(fir2);
            Controls.Add(fir1);
            Controls.Add(label5);
            Controls.Add(exit);
            Controls.Add(label4);
            Controls.Add(nextTeam2);
            Controls.Add(nextTeam1);
            Controls.Add(nextTeam);
            Controls.Add(swap);
            Controls.Add(vs2);
            Controls.Add(vs1);
            Controls.Add(label1);
            Controls.Add(team2);
            Controls.Add(team1);
            Name = "CForm";
            Text = "Form1";
            Load += CForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox team1;
        private ComboBox team2;
        private Label label1;
        private Label vs1;
        private Label vs2;
        private Button swap;
        private Button nextTeam;
        private Label label4;
        private ComboBox nextTeam2;
        private ComboBox nextTeam1;
        private Button exit;
        private Label label5;
        private Button fir1;
        private Button fir2;
        private Button sec1;
        private Button sec2;
        private Button nextSec2;
        private Button nextSec1;
        private Button nextFir2;
        private Button nextFir1;
        private Label label8;
        private Button button2;
        private Button button1;
    }
}