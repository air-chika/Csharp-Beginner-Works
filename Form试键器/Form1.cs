namespace Form试键器
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.ForeColor = Color.FromArgb(215, 190, 135);
            FormBorderStyle = FormBorderStyle.None;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            var x = $"KeyValue: { e.KeyValue}\nKeyData: {e.KeyData}\nKeyCode: {e.KeyCode}";
            label1.Text = x ;
        }
    }
}