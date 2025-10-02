
using SmpForm;
using SMPConsole;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace 图片分拣机
{
    public partial class SpilitForm : Form
    {
        static public void SmpChange(Button button)
        {
            button.Size = new System.Drawing.Size(190, 45);
            button.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            button.ForeColor = System.Drawing.Color.Black;
            button.BackColor = SmpColor.棕;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.MouseDownBackColor = button.FlatAppearance.MouseOverBackColor = SmpColor.金;
            button.Font = new System.Drawing.Font("黑体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        }

        public SpilitForm()
        {
            InitializeComponent();
            SmpChange(exit);
            SmpChange(useOrDelete);
            SmpChange(revoke);
            SmpChange(srcSet);
            SmpChange(srcsSet);
            SmpChange(aimSet);
            SmpChange(AimNameSet);

            revoke.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;


            picBox.Image = imgSpliter.LastImg;

            SetNumLabel();

            foreach (var x in imgSpliter.AimDirNames)
                AddButton(x);
            //FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;
        }

        enum BuState
        {
            use,
            pre,
            del
        }
        BuState buState = BuState.use;

        List<Button> buttons = new();
        ImgSpliter imgSpliter = new();

        void SetNumLabel()
        {
            remainNum.Text = $"剩余{imgSpliter.RemainNum}张";
        }

        void AddButton(string x)
        {
            var bu = new Button();
            bu.SmpChange();
            bu.Dock = DockStyle.Fill;
            bu.Text = x;
            bu.Click += (_, _) =>
            {
                if (buState == BuState.use)
                {
                    picBox.Image = imgSpliter.Choose(x);
                    SetNumLabel();
                }
                else
                if (buState == BuState.del)
                {
                    tableChoose.Controls.Remove(bu);
                    imgSpliter.RemoveAimName(x);
                    buttons.Remove(bu);
                }
                else
                {
                    buttons.Remove(bu);
                    buttons.Insert(0, bu);
                    ResetTable();
                    imgSpliter.PreSetAimName(x);
                }
            };
            tableChoose.Controls.Add(bu);
            buttons.Add(bu);
        }

        void ResetTable()
        {
            tableChoose.Controls.Clear();
            tableChoose.Controls.AddRange(buttons.ToArray());
        }



        private void AddFolder(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                string x = addBox.Text;
                addBox.Text = "";

                if (!SmpFile.IsDirectoryName(x))
                    return;

                imgSpliter.AddAimName(x);
                AddButton(x);

                return;
            }
            if (e.KeyChar == 27)
            {
                e.Handled = true;
                addBox.Text = "";
            }
        }


        private void Exit(object sender, EventArgs e)
        {
            Close();
        }

        private void ChangeUse(object sender, EventArgs e)
        {
            if (buState == BuState.use)
            {
                buState = BuState.pre;
                useOrDelete.Text = "前置";
                foreach (var bu in tableChoose.Controls)
                {
                    ((Button)bu).BackColor = SmpColor.碧绿;
                    ((Button)bu).FlatAppearance.MouseDownBackColor = ((Button)bu).FlatAppearance.MouseOverBackColor = SmpColor.绿;
                }
            }
            else if (buState == BuState.pre)
            {
                buState = BuState.del;
                useOrDelete.Text = "删除";
                foreach (var bu in tableChoose.Controls)
                {
                    ((Button)bu).BackColor = SmpColor.火烈鸟;
                    ((Button)bu).FlatAppearance.MouseDownBackColor = ((Button)bu).FlatAppearance.MouseOverBackColor = SmpColor.红;
                }
            }
            else
            {
                buState = BuState.use;
                useOrDelete.Text = "使用";
                foreach (var bu in tableChoose.Controls)
                {
                    ((Button)bu).BackColor = SmpColor.棕;
                    ((Button)bu).FlatAppearance.MouseDownBackColor = ((Button)bu).FlatAppearance.MouseOverBackColor = SmpColor.金;
                }
            }
        }

        private void Revoke(object sender, EventArgs e)
        {
            picBox.Image = imgSpliter.Revoke();
            SetNumLabel();
        }

        void SetAimName(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Path.Combine(Environment.CurrentDirectory, "memory");
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                imgSpliter.ReadAimName(openFileDialog1.FileName);
                tableChoose.Controls.Clear();
                foreach (var x in imgSpliter.AimDirNames)
                    AddButton(x);
            }
        }

        private void SrcSet(object sender, EventArgs e)
        {
            folderBrowserDialog1.InitialDirectory = imgSpliter.SourseFolder;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.Cancel) return;
            imgSpliter.LoadImg(imgSpliter.GetFiles(folderBrowserDialog1.SelectedPath));
            picBox.Image = imgSpliter.LastImg;
            SetNumLabel();
        }

        private void SrcsSet(object sender, EventArgs e)
        {
            folderBrowserDialog1.InitialDirectory = imgSpliter.SourseFolder;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.Cancel) return;
            imgSpliter.LoadImg(imgSpliter.GetDirFiles(folderBrowserDialog1.SelectedPath));
            picBox.Image = imgSpliter.LastImg;
            SetNumLabel();
        }

        private void AimSet(object sender, EventArgs e)
        {
            folderBrowserDialog1.InitialDirectory = imgSpliter.AimFolder;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.Cancel) return;
            imgSpliter.AimFolder = folderBrowserDialog1.SelectedPath;
        }

        private void remainNum_Click(object sender, EventArgs e)
        {
        }


        static Dictionary<Keys, int> keyDic = new();
        static SpilitForm()
        {
            for (int i = 0; i < 9; i++)
                keyDic.Add(Keys.D0 + i + 1, i);
            int x = 3;
            keyDic.Add(Keys.Q, x++);
            keyDic.Add(Keys.W, x++);
            keyDic.Add(Keys.E, x++);
            keyDic.Add(Keys.A, x++);
            keyDic.Add(Keys.S, x++);
            keyDic.Add(Keys.D, x++);
            keyDic.Add(Keys.Z, x++);
            keyDic.Add(Keys.X, x++);
            keyDic.Add(Keys.C, x++);
        }

        private void MainKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Oemtilde || e.KeyCode == Keys.P)
            {
                Revoke(new(), new());
                return;
            }
            if (keyDic.Keys.Contains(e.KeyCode))
                buttons[keyDic[e.KeyCode]].PerformClick();
        }

        private void addBox_MouseHover(object sender, EventArgs e)
        {
            KeyPreview = false;
        }

        private void addBox_MouseLeave(object sender, EventArgs e)
        {
            KeyPreview = true;
        }
    }
}