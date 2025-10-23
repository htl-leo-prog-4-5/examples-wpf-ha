using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleBindingNotenschnitt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        static bool IsValidGrade(uint grade)
        {
            return grade >= 1 && grade <= 5;
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            var grade = FindResource("AGrade") as Grades;

            if (IsValidGrade(grade.Mathematik) && IsValidGrade(grade.Englisch) && IsValidGrade(grade.Deutsch))
            {
                bool moreThan3 = grade.Mathematik > 3 || grade.Deutsch > 3 || grade.Englisch > 3;
                bool negativ = grade.Mathematik > 4 || grade.Deutsch > 4 || grade.Englisch > 4;
                double avg = (grade.Mathematik + grade.Deutsch + grade.Englisch) / 3.0;

                _resultAvg.Content = $"Notenschnitt: {avg}";

                if (negativ)
                {
                    _resultKlausel.Content = "nicht bestanden";
                }
                else
                {
                    if (avg <= 1.5 && !moreThan3)
                    {
                        _resultKlausel.Content = "mit Auszeichnung bestanden";
                    }
                    else if (avg <= 2.0 && !moreThan3)
                    {
                        _resultKlausel.Content = "mit gutem Erfolg bestanden";
                    }
                    else
                    {
                        _resultKlausel.Content = "bestanden";
                    }
                }
            }
            else
            {
                _resultKlausel.Content = "ungültige Eingabe";
                _resultAvg.Content = 0;
            }
        }
    }
}
