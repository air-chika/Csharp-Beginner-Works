namespace 图片分拣机
{
    partial class SpilitForm
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
            picBox = new PictureBox();
            panel1 = new Panel();
            revoke = new Button();
            AimNameSet = new Button();
            remainNum = new Label();
            aimSet = new Button();
            srcSet = new Button();
            srcsSet = new Button();
            exit = new Button();
            useOrDelete = new Button();
            addBox = new TextBox();
            tableChoose = new TableLayoutPanel();
            folderBrowserDialog1 = new FolderBrowserDialog();
            openFileDialog1 = new OpenFileDialog();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picBox).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel1.Controls.Add(picBox, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 2, 0);
            tableLayoutPanel1.Controls.Add(tableChoose, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1424, 771);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // picBox
            // 
            picBox.Dock = DockStyle.Fill;
            picBox.Location = new Point(0, 0);
            picBox.Margin = new Padding(0);
            picBox.Name = "picBox";
            picBox.Size = new Size(712, 771);
            picBox.SizeMode = PictureBoxSizeMode.Zoom;
            picBox.TabIndex = 0;
            picBox.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(revoke);
            panel1.Controls.Add(AimNameSet);
            panel1.Controls.Add(remainNum);
            panel1.Controls.Add(aimSet);
            panel1.Controls.Add(srcSet);
            panel1.Controls.Add(srcsSet);
            panel1.Controls.Add(exit);
            panel1.Controls.Add(useOrDelete);
            panel1.Controls.Add(addBox);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(1210, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(214, 771);
            panel1.TabIndex = 0;
            // 
            // revoke
            // 
            revoke.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            revoke.Location = new Point(12, 63);
            revoke.Name = "revoke";
            revoke.Size = new Size(190, 45);
            revoke.TabIndex = 1;
            revoke.Text = "撤回";
            revoke.UseVisualStyleBackColor = false;
            revoke.Click += Revoke;
            // 
            // AimNameSet
            // 
            AimNameSet.Location = new Point(12, 308);
            AimNameSet.Name = "AimNameSet";
            AimNameSet.Size = new Size(190, 45);
            AimNameSet.TabIndex = 3;
            AimNameSet.Text = "设置标签";
            AimNameSet.UseVisualStyleBackColor = false;
            AimNameSet.Click += SetAimName;
            // 
            // remainNum
            // 
            remainNum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            remainNum.Font = new Font("黑体", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            remainNum.ForeColor = Color.FromArgb(215, 190, 135);
            remainNum.Location = new Point(12, 132);
            remainNum.Name = "remainNum";
            remainNum.Size = new Size(190, 45);
            remainNum.TabIndex = 8;
            remainNum.TextAlign = ContentAlignment.MiddleCenter;
            remainNum.Click += remainNum_Click;
            // 
            // aimSet
            // 
            aimSet.Location = new Point(12, 380);
            aimSet.Name = "aimSet";
            aimSet.Size = new Size(190, 45);
            aimSet.TabIndex = 7;
            aimSet.Text = "设置目标";
            aimSet.UseVisualStyleBackColor = false;
            aimSet.Click += AimSet;
            // 
            // srcSet
            // 
            srcSet.Location = new Point(12, 524);
            srcSet.Name = "srcSet";
            srcSet.Size = new Size(190, 45);
            srcSet.TabIndex = 6;
            srcSet.Text = "设置资源";
            srcSet.UseVisualStyleBackColor = false;
            srcSet.Click += SrcSet;
            // 
            // srcsSet
            // 
            srcsSet.Location = new Point(12, 452);
            srcsSet.Name = "srcsSet";
            srcsSet.Size = new Size(190, 45);
            srcsSet.TabIndex = 5;
            srcsSet.Text = "设置资源(群)";
            srcsSet.UseVisualStyleBackColor = false;
            srcsSet.Click += SrcsSet;
            // 
            // exit
            // 
            exit.Location = new Point(12, 668);
            exit.Name = "exit";
            exit.Size = new Size(190, 45);
            exit.TabIndex = 3;
            exit.Text = "退出";
            exit.UseVisualStyleBackColor = false;
            exit.Click += Exit;
            // 
            // useOrDelete
            // 
            useOrDelete.Location = new Point(12, 596);
            useOrDelete.Name = "useOrDelete";
            useOrDelete.Size = new Size(190, 45);
            useOrDelete.TabIndex = 2;
            useOrDelete.Text = "使用";
            useOrDelete.UseVisualStyleBackColor = false;
            useOrDelete.Click += ChangeUse;
            // 
            // addBox
            // 
            addBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            addBox.BackColor = SystemColors.ActiveCaptionText;
            addBox.BorderStyle = BorderStyle.FixedSingle;
            addBox.Font = new Font("黑体", 24F, FontStyle.Regular, GraphicsUnit.Point);
            addBox.ForeColor = Color.FromArgb(215, 190, 135);
            addBox.Location = new Point(12, 201);
            addBox.Name = "addBox";
            addBox.Size = new Size(190, 44);
            addBox.TabIndex = 1;
            addBox.KeyPress += AddFolder;
            addBox.MouseLeave += addBox_MouseLeave;
            addBox.MouseHover += addBox_MouseHover;
            // 
            // tableChoose
            // 
            tableChoose.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableChoose.ColumnCount = 3;
            tableChoose.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tableChoose.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33334F));
            tableChoose.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33334F));
            tableChoose.Location = new Point(715, 60);
            tableChoose.Margin = new Padding(3, 60, 3, 60);
            tableChoose.Name = "tableChoose";
            tableChoose.RowCount = 12;
            tableChoose.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tableChoose.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tableChoose.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tableChoose.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tableChoose.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tableChoose.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tableChoose.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tableChoose.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tableChoose.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tableChoose.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tableChoose.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tableChoose.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tableChoose.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableChoose.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableChoose.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableChoose.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableChoose.Size = new Size(492, 651);
            tableChoose.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // SpilitForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(38, 38, 38);
            ClientSize = new Size(1424, 771);
            Controls.Add(tableLayoutPanel1);
            KeyPreview = true;
            Name = "SpilitForm";
            Text = "Form1";
            KeyDown += MainKey;
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picBox).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox picBox;
        private Panel panel1;
        private TableLayoutPanel tableChoose;
        private Button exit;
        private Button useOrDelete;
        private Button revoke;
        private TextBox addBox;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button srcSet;
        private Button srcsSet;
        private Button aimSet;
        private Label remainNum;
        private Button AimNameSet;
        private OpenFileDialog openFileDialog1;
    }
}