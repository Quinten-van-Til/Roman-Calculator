using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace Roman_Calculator_1._2
{
    public partial class MainWindow : Window
    {
        #region Public Variables
        public int Arabic;
        public string ArabicString;
        public int i;

        public bool Ans;
        public string Calculator;
        public int Number_A;
        public int Number_B;
        public int Number_C;
        public string Roman_A;
        public string Roman_B;
        public string Roman_C;
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            Textbox_1.Focus();
        }
        #endregion

        #region Buttons [I-M]
        private void Button_I_Click(object sender, RoutedEventArgs e)
        {
            InsertRomanNumber("I");
        }

        private void Button_V_Click(object sender, RoutedEventArgs e)
        {
            InsertRomanNumber("V");
        }

        private void Button_X_Click(object sender, RoutedEventArgs e)
        {
            InsertRomanNumber("X");
        }

        private void Button_L_Click(object sender, RoutedEventArgs e)
        {
            InsertRomanNumber("L");
        }

        private void Button_C_Click(object sender, RoutedEventArgs e)
        {
            InsertRomanNumber("C");
        }

        private void Button_D_Click(object sender, RoutedEventArgs e)
        {
            InsertRomanNumber("D");
        }

        private void Button_M_Click(object sender, RoutedEventArgs e)
        {
            InsertRomanNumber("M");
        }
        #endregion

        #region Operational Buttons [= CE + - x ÷]
        private void Button_Equal_Click(object sender, RoutedEventArgs e)
        {
            Number_B = RomanToArabic(Textbox_1.Text);
            Roman_B = Textbox_1.Text;

            switch (Calculator)
            {
                case "+":
                    Number_C = Number_A + Number_B;
                    Number_A = Number_A + Number_B;
                    break;
                case "-":
                    Number_C = Number_A - Number_B;
                    Number_A = Number_A - Number_B;
                    break;

                case "x":
                    Number_C = Number_A * Number_B;
                    Number_A = Number_A * Number_B;
                    break;
                case "÷":
                    Number_C = Number_A / Number_B;
                    Number_A = Number_A / Number_B;
                    break;
            }

            ArabicToRoman();
            Textbox_1.Text = "";
            Display_1.Text += " " + Roman_B + "=" + " " + Roman_C;
            Roman_A = Roman_C;
            Roman_C = "";
            Ans = true;
        }

        private void Button_CE_Click(object sender, RoutedEventArgs e)
        {
            Number_A = 0;
            Number_B = 0;
            Number_C = 0;
            Roman_A = "";
            Roman_B = "";
            Roman_C = "";

            Display_1.Text = "";
            Textbox_1.Text = "";
        }

        private void Button_Plus_Click(object sender, RoutedEventArgs e)
        {
            Calculation("+");
        }

        private void Button_Minus_Click(object sender, RoutedEventArgs e)
        {
            Calculation("-");
        }

        private void Button_Multiply_Click(object sender, RoutedEventArgs e)
        {
            Calculation("x");
        }

        private void Button_Divide_Click(object sender, RoutedEventArgs e)
        {
            Calculation("÷");
        }
        #endregion

        #region Functions

        public void Calculation(string value)
        {
            if (Ans == false)
            {
                Number_A = RomanToArabic(Textbox_1.Text);
                Roman_A = Textbox_1.Text;
            }

            Calculator = value;

            Display_1.Text = Roman_A + " " + Calculator;
            Textbox_1.Text = "";
        }

        #region Textbox Input Handlers
        private void RomanNumbersOnly(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^i,v,x,l,c,d,m,I,V,X,L,C,D,M]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void InsertRomanNumber(string value)
        {
            Textbox_1.SelectedText = value;
            Textbox_1.SelectionStart++;
            Textbox_1.SelectionLength = 0;
            Textbox_1.Focus();
            Ans = false;
        }
        #endregion        

        #region Converters
        public int Input(char x)
        {
            if (x == 'I')
                return 1;
            if (x == 'V')
                return 5;
            if (x == 'X')
                return 10;
            if (x == 'L')
                return 50;
            if (x == 'C')
                return 100;
            if (x == 'D')
                return 500;
            if (x == 'M')
                return 1000;
            return -1;
        }

        public int RomanToArabic(string value)
        {
            Arabic = 0;
            int Max = value.Length;

            for (i = 0; i < Max; i++)
            {
                int i2 = i + 1;
                int char_1 = Input(value[i]);
                if (i2 < value.Length)
                {
                    int char_2 = Input(value[i2]);

                    if (char_1 >= char_2)
                    {
                        Arabic += char_1;
                    }
                    else
                    {
                        Arabic += (char_2 - char_1);
                        i++;
                    }
                }
                else
                {
                    Arabic += char_1;
                    i++;
                }
            }
            return Arabic;
        }

        public void ArabicToRoman()
        {
            ArabicString = Number_C.ToString();
            int Max = ArabicString.Length;
            int Min;
            int x = 0;
            char Trial;

            string[,] array = new string[,]
            {
            {"M", "", "", "" },
            {"C", "CD", "D", "CM"},
            {"X", "XL", "L", "XC"},
            {"I", "IV", "V", "IX"},
            };

            Min = (4 - Max);

            for (i = Min; i < 4; i++)
            {
                Trial = ArabicString[x];
                int AA = int.Parse(Trial.ToString());
                for (; AA != 0;)
                {
                    if (AA == 9)
                    {
                        Roman_C += array[i, 3];
                        AA -= 9;
                    }
                    else if (AA >= 5)
                    {
                        Roman_C += array[i, 2];
                        AA -= 5;
                    }
                    else if (AA == 4)
                    {
                        Roman_C += array[i, 1];
                        AA -= 4;
                    }
                    else
                    {
                        Roman_C += array[i, 0];
                        AA--;
                    }
                }
                x++;
            }
        }
        #endregion
        #endregion
    }
}
