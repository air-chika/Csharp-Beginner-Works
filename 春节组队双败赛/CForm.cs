using System.Linq;


namespace 春节组队双败赛
{
    public record Team(string name, string fir, string sec);



    public partial class CForm : Form
    {
        Team[] teams;

        public CForm()
        {
            var lines = File.ReadAllLines("data.txt");
            teams = lines.Select(x =>
            {
                var strs = x.Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);
                return new Team(strs[0], strs[1], strs[2]);
            }).ToArray();


            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            this.FormBorderStyle = FormBorderStyle.None;
            team1.Items.AddRange(teams.Select(x => x.name).ToArray());
            team2.Items.AddRange(teams.Select(x => x.name).ToArray());
            nextTeam1.Items.AddRange(teams.Select(x => x.name).ToArray());
            nextTeam2.Items.AddRange(teams.Select(x => x.name).ToArray());
        }


        private void exit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void ChangeColor(object sender, EventArgs e)
        {
            var but = (Button)sender;
            if (but.ForeColor == Color.FromArgb(215, 190, 135))
                but.ForeColor = Color.FromArgb(128, 128, 128);
            else
                but.ForeColor = Color.FromArgb(215, 190, 135);
        }

        void ResetTeam()
        {
            fir1.ForeColor = sec1.ForeColor = fir2.ForeColor = sec2.ForeColor = vs1.ForeColor = SmpForm.SmpColor.金;
            vs2.ForeColor = SmpForm.SmpColor.背景の黑;
        }

        private void Team1Choose(object sender, EventArgs e)
        {
            var team = teams[team1.SelectedIndex];
            fir1.Text = team.fir;
            sec1.Text = team.sec;
            ResetTeam();
        }
        private void Team2Choose(object sender, EventArgs e)
        {
            var team = teams[team2.SelectedIndex];
            fir2.Text = team.fir;
            sec2.Text = team.sec;
            ResetTeam();
        }
        private void NTeam1Choose(object sender, EventArgs e)
        {
            var team = teams[nextTeam1.SelectedIndex];
            nextFir1.Text = team.fir;
            nextSec1.Text = team.sec;
        }
        private void NTeam2Choose(object sender, EventArgs e)
        {
            var team = teams[nextTeam2.SelectedIndex];
            nextFir2.Text = team.fir;
            nextSec2.Text = team.sec;
        }

        private void VSlabel(object sender, EventArgs e)
        {
            var vs = (Label)sender;
            if (vs.ForeColor == Color.FromArgb(215, 190, 135))
                vs.ForeColor = Color.FromArgb(38, 38, 38);
            else
                vs.ForeColor = Color.FromArgb(215, 190, 135);
        }

        public record TeamUIInfo(string teamName, string fir, Color fircolor, string sec, Color seccolor)
        {
            public TeamUIInfo(ComboBox box, Button fi, Button se) : this(box.Text, fi.Text, fi.ForeColor, se.Text, se.ForeColor) { }

            public void Set(ComboBox box, Button fi, Button se)
            {
                box.Text = teamName;
                fi.Text = fir;
                fi.ForeColor = fircolor;
                se.Text = sec;
                se.ForeColor = seccolor;
            }

        }
        private void swap_Click(object sender, EventArgs e)
        {
            TeamUIInfo info1 = new(team1, fir1, sec1);
            TeamUIInfo info2 = new(team2, fir2, sec2);
            info1.Set(team2, fir2, sec2);
            info2.Set(team1, fir1, sec1);
        }

        void ResetTeam(ComboBox box, Button b1, Button b2)
        {
            box.Text = string.Empty;
            b1.Text = b2.Text = string.Empty;
        }

        private void CForm_Load(object sender, EventArgs e)
        {

        }

        private void nextTeam_Click(object sender, EventArgs e)
        {
            team1.SelectedIndex = nextTeam1.SelectedIndex;
            Team1Choose(team1, new());
            team2.SelectedIndex = nextTeam2.SelectedIndex;
            Team2Choose(team2, new());
            ResetTeam(nextTeam1, nextFir1, nextSec1);
            ResetTeam(nextTeam2, nextFir2, nextSec2);
        }

        private void team2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}