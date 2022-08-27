using System;
using System.Linq;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        String operation = "";
        bool operatorPressed = false;
        bool memoryStored = false;
        Double temp = 0;
        Double memory = 0;

        public static string[] textBox = new string[3];
        public Form1()
        {
            InitializeComponent();
        }

        private void update_MS_MC_Btn()
        {
            memRecall.Enabled = memoryStored;
            memClear.Enabled = memoryStored;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            update_MS_MC_Btn();
        }

        private void numberClick(object sender, EventArgs e)
        {
            if (InputBox.Text == "0" || operatorPressed)
                InputBox.Clear();

            if (InputBox.Text.Length == 2 && InputBox.Text == "-0")
                InputBox.Text = "-";

            Button button = sender as Button;
            operatorPressed = false;

            if (button.Text == "." && InputBox.Text.Contains("."))
                return;

            InputBox.Text += button.Text;
        }

        private void equalClick(object sender, EventArgs e)
        {
            switch (operation)
            {
                case "+":
                    InputBox.Text = (temp + Double.Parse(InputBox.Text)).ToString();
                    break;
                case "-":
                    InputBox.Text = (temp - Double.Parse(InputBox.Text)).ToString();
                    break;
                case "*":
                    InputBox.Text = (temp * Double.Parse(InputBox.Text)).ToString();
                    break;
                case "/":
                    InputBox.Text = (temp / Double.Parse(InputBox.Text)).ToString();
                    break;
                default:
                    break;
            }
            LabelBox.Clear();
            temp = Double.Parse(InputBox.Text);
            operation = "";
        }

        private void operatorClick(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if (temp != 0)
                equalBtn.PerformClick();
            else
                temp = Double.Parse(InputBox.Text);

            operatorPressed = true;
            operation = button.Text;
            LabelBox.Text = temp + " " + operation;
        }

        private void clearEntryClick(object sender, EventArgs e)
        {
            InputBox.Text = "0";
        }

        private void clearClick(object sender, EventArgs e)
        {
            if (InputBox.Text != "0") InputBox.Text = "0";
            if (LabelBox.Text.Length > 0) LabelBox.Clear();
            temp = 0;
            operation = "";
        }

        private void convertEntryPosNeg(object sender, EventArgs e)
        {
            if (InputBox.Text.First() == '0') return;
            if (InputBox.Text.First() == '-') InputBox.Text = InputBox.Text.Substring(1);
            else InputBox.Text = '-' + InputBox.Text;
        }

        private void delInput(object sender, EventArgs e)
        {
            if (InputBox.Text.Length <= 1)
            {
                if (InputBox.Text != "0")
                    InputBox.Text = "0";
                return;
            }

            InputBox.Text = InputBox.Text.Substring(0, InputBox.Text.Length - 1);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void quickOperate(object sender, EventArgs e)
        {
            if (InputBox.Text == "0")
                return;

            Button button = sender as Button;

            switch (button.Text)
            {
                case "√":
                    InputBox.Text = (Math.Sqrt(Double.Parse(InputBox.Text))).ToString();
                    break;
                case "%":
                    InputBox.Text = (Double.Parse(InputBox.Text) / 100).ToString();
                    break;
                case "1/x":
                    InputBox.Text = (1 / Double.Parse(InputBox.Text)).ToString();
                    break;
                default:
                    break;
            }

        }

        private void memClear_Click(object sender, EventArgs e)
        {
            memoryStored = false;
            memory = 0;
            update_MS_MC_Btn();
        }

        private void memStore_Click(object sender, EventArgs e)
        {
            memoryStored = true;
            memory = Double.Parse(InputBox.Text);
            update_MS_MC_Btn();
        }

        private void memRecall_Click(object sender, EventArgs e)
        {
            if (!memoryStored) return;
            InputBox.Text = memory.ToString();
        }

        private void memAddSub_Click(object sender, EventArgs e)
        {
            if (!memoryStored) return;
            Button button = sender as Button;
            Double num_todo = operatorPressed ? temp : Double.Parse(InputBox.Text);
            if (button.Text == "M+") memory += num_todo;
            else memory -= num_todo;
        }
    }
}
