using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizGame2
{
    public partial class Form1 : Form
    {
        public List<Question> _questions = new List<Question>();
        int _index = -1;
        int countOfCorrect = 0;
        int countOfWrong = 0;
        private readonly Random _random = new Random();

        string connectionString = "SERVER=MSI\\MSSQLSERVER01;Database=QuizDB;Trusted_Connection=True";

        public Form1()
        {
            InitializeComponent();
            btnA.Click += OnClick;
            btnB.Click += OnClick;
            btnC.Click += OnClick;
            btnD.Click += OnClick;
        }

        private List<Question> GetQuestions()
        {
            var list = new List<Question>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                string query = "SELECT Id,Text,OptionA,OptionB,OptionC,OptionD,CorrectOption FROM Questions";

                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {

                        list.Add(new Question
                        {

                            Id = (int)rd["Id"],
                            Text = (string)rd["Text"],
                            OptionA = (string)rd["OptionA"],
                            OptionB = (string)rd["OptionB"],
                            OptionC = (string)rd["OptionC"],
                            OptionD = (string)rd["OptionD"],
                            CorrectOption = rd["CorrectOption"].ToString()[0]

                        });
                    }
                }
            }
            return list.OrderBy(_ => _random.Next()).ToList();
        }

        public void ShowQuesiton()
        {
            var q = _questions[_index];

            lblText.Text = q.Text;
            btnA.Text = q.OptionA.ToString();
            btnB.Text = q.OptionB.ToString();
            btnC.Text = q.OptionC.ToString();
            btnD.Text = q.OptionD.ToString();

            SetOpartionsEnabled(true);
            SetColor();
            btnNext.Enabled = false;


            lblCorrect.Text = $"{countOfCorrect}";
            lblWrong.Text = $"{countOfWrong}";

        }

        private void SetOpartionsEnabled(bool enabled)
        {
            btnA.Enabled = enabled;
            btnB.Enabled = enabled;
            btnC.Enabled = enabled;
            btnD.Enabled = enabled;


        }

        private void SetColor()
        {
            btnA.BackColor = SystemColors.Control;
            btnB.BackColor = SystemColors.Control;
            btnC.BackColor = SystemColors.Control;
            btnD.BackColor = SystemColors.Control;
        }

        private void OnClick(object sender, EventArgs e)
        {

            var q = _questions[_index];

            var clicked = (Button)sender;
            var chosen = GetChosenFromButton(clicked);
            var correct = q.CorrectOption;

            if (char.ToUpperInvariant(chosen) == char.ToUpperInvariant(correct))
            {

                countOfCorrect++;
                clicked.BackColor = Color.LightGreen;
            }
            else
            {
                countOfWrong++;
                clicked.BackColor = Color.Red;

            }

            var correctBtn = GetButtonByOption(correct);
            if (correctBtn != null)
            {
                correctBtn.BackColor = Color.LightGreen;
            }

            lblCorrect.Text = $"{countOfCorrect}";
            lblWrong.Text = $"{countOfWrong}";


            SetOpartionsEnabled(false);
            btnNext.Enabled = true;
        }

        private char GetChosenFromButton(Button button)
        {
            if (button == btnA) return 'A';
            if (button == btnB) return 'B';
            if (button == btnC) return 'C';
            if (button == btnD) return 'D';

            return '?';
        }

        private Button GetButtonByOption(char Option)
        {
            switch (char.ToUpperInvariant(Option))
            {

                case 'A': return btnA;
                case 'B': return btnB;
                case 'C': return btnC;
                case 'D': return btnD;
                default: return null;

            }
        }
        private void EndGame()
        {
            SetOpartionsEnabled(false);
            btnNext.Enabled = false;
            lblCorrect.Text = $"{countOfCorrect}";
            lblWrong.Text = $"{countOfWrong}";

            lblText.Text = "Game completed!";

        }
        private void Form1_Load(object sender, EventArgs e)
        {

            _questions = GetQuestions();

            if (_index ==0)
            {
                MessageBox.Show("Failed");
                return;
            }

            else
            {

                _index = 0;
                ShowQuesiton();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _index++;

            if (_index >= _questions.Count)
            {
                EndGame();
                return;
            }
            ShowQuesiton();

        }
    }
}