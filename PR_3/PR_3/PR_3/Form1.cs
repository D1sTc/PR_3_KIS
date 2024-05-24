using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PR_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ValueMatrix1.Validating += ValueMatrix1_Validating;
            ValueMatrix2.Validating += ValueMatrix2_Validating;
            ValueMatrix3.Validating += ValueMatrix3_Validating;
            ValueMatrix4.Validating += ValueMatrix4_Validating;
        }
        private void ValueMatrix1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(ValueMatrix1.Text)) errorProvider1.SetError(ValueMatrix1, "Не указано значение!");
            else if (!double.TryParse(ValueMatrix1.Text, out _)) errorProvider1.SetError(ValueMatrix1, "Некорретное значение!");
            else errorProvider1.Clear();
        }

        private void ValueMatrix2_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(ValueMatrix2.Text)) errorProvider1.SetError(ValueMatrix2, "Не указано значение!");
            else if (!double.TryParse(ValueMatrix2.Text, out _)) errorProvider1.SetError(ValueMatrix2, "Некорретное значение!");
            else errorProvider1.Clear();
        }

        private void ValueMatrix3_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(ValueMatrix3.Text)) errorProvider1.SetError(ValueMatrix3, "Не указано значение!");
            else if (!double.TryParse(ValueMatrix3.Text, out _)) errorProvider1.SetError(ValueMatrix3, "Некорретное значение!");
            else errorProvider1.Clear();
        }

        private void ValueMatrix4_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(ValueMatrix4.Text)) errorProvider1.SetError(ValueMatrix4, "Не указано значение!");
            else if (!double.TryParse(ValueMatrix4.Text, out _)) errorProvider1.SetError(ValueMatrix4, "Некорретное значение!");
            else errorProvider1.Clear();
        }
        private void AboutProgram_Click(object sender, EventArgs e)
        {
            Form2 newF = new Form2();
            newF.Show();
        }

        private void ClearMenu_Click(object sender, EventArgs e)
        {
            Clear_Click(sender, e);
        }

        private void ExitMenu_Click(object sender, EventArgs e)
        {
            Exit_Click(sender, e);
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            ValueMatrix1.Clear();
            ValueMatrix2.Clear();
            ValueMatrix3.Clear();
            ValueMatrix4.Clear();
            GeometricMeanMatrixExit.Clear();
            errorProvider1.Clear();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GeometricMeanMatrixButton_Click(object sender, EventArgs e)
        {
            string[] matrixTextBoxes = new string[] {ValueMatrix1.Text, ValueMatrix2.Text, ValueMatrix3.Text, ValueMatrix4.Text}; // массив, который содержит ссылки на четыре текстовых поля.
            if (matrixTextBoxes.All(textBox => double.TryParse(textBox, out _)))
            {
                double[,] matrix = GetMatrixFromTextBoxes<double>(); // вызов метода переноса данных из 4-ёх ячеечек в двумерную матрицу типа T (любого*)
                double geometricMean = CalculateGeometricMean(matrix); // вызов метода решения, в которую мы эту двумерную матрицу и передаём
                GeometricMeanMatrixExit.Text = geometricMean.ToString(); // преобразование выходного значения из Mean в текст exit'а
            }
            else
            {
                MessageBox.Show("Ошибка. Не все значения в полях являются допустимыми.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); // ИНАЧЕ! Если значение не double выводит блок с текстом
            }
        }

        private double[,] GetMatrixFromTextBoxes<T>() where T : IConvertible // реализовка интерфейса `IConvertible` -> обобщение | поддержка преобразований между базовыми типами данных
        {
            string[] matrixTextBoxes = new string[] {ValueMatrix1.Text, ValueMatrix2.Text, ValueMatrix3.Text, ValueMatrix4.Text}; // массив, который содержит ссылки на четыре текстовых поля.
            double[,] matrix = new double[2, 2];

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    int index = i * 2 + j;
                    matrix[i, j] = double.Parse(matrixTextBoxes[index]);
                }
            }

            return matrix;
        }

        private double CalculateGeometricMean<T>(T[,] matrix) where T : IConvertible // вычисление среднего геометрического значения
        {
            double product = 1.0; // Инициализация произведения положительных значений | 1.0 как нач. значение
            int count = 0; // Инициализация количества положительных значений

            for (int i = 0; i < matrix.GetLength(0); i++) // Перебор всех элементов матрицы 2x2 || строки
            {
                for (int j = 0; j < matrix.GetLength(1); j++) // Перебор всех элементов матрицы 2x2 || столбцы
                {
                    double value = Convert.ToDouble(matrix[i, j], null); // преобразование элемента матрицы в double | null - x
                    if (value > 0)
                    {
                        product *= value; // умножение на + значение
                        count++; // увеличение кол-ва + значений
                    }
                    else
                    {
                        MessageBox.Show("Ошибка. Были введены отрицательные значения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); // ошибка на случай отрицательных значений
                    }
                }
            }
            return count > 0 ? Math.Pow(product, 1.0 / count) : 0; // проверка кол-ва и деление на него при > 0
        }

        private void ApplicationGeometricMean_Click(object sender, EventArgs e)
        {
            GeometricMeanMatrixButton_Click(sender, e);
        }
    }
}
