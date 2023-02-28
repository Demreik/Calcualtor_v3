using Calculator_v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_v3
{
    class Program
    {
        static void GetSecondNumber(out double secondNumber)
        {
            Console.Write("Введите второе число: ");
            while (!double.TryParse(Console.ReadLine(), out secondNumber))
            {
                Console.WriteLine("Ошибка, необходимо ввести число");
                Console.Write("Введите второе число: ");
            }
        }
        static void Main(string[] args)
        {
            double secondNumber = 0; 
            double firstNumber = 0;
            
            
            Console.Write("Введите первое число: ");
            while (!double.TryParse(Console.ReadLine(), out firstNumber))
            {
                Console.WriteLine("Ошибка, необходимо ввести число");
                Console.Write("Введите первое число: ");
            }

            Console.WriteLine("Выберите операцию (+, -, *, /, sqrt): ");
            string operation = Console.ReadLine();


            IOperation operationStrategy;

            switch (operation)
            {
                case "+":
                    GetSecondNumber(out secondNumber);
                    operationStrategy = new AddOperation(secondNumber);
                    break;
                case "-":
                    GetSecondNumber(out secondNumber);
                    operationStrategy = new SubtractOperation(secondNumber);
                    break;
                case "*":
                    GetSecondNumber(out secondNumber);
                    operationStrategy = new MultiplyOperation(secondNumber);
                    break;
                case "/":
                    GetSecondNumber(out secondNumber);
                    operationStrategy = new DivideOperation(secondNumber);
                    break;
                case "sqrt":
                    operationStrategy = new SquareRootOperation();
                    break;
                default:
                    Console.WriteLine("Некорректная операция!");
                    return;
            }

            Calculator calculator = new Calculator();
            calculator.SetOperation(operationStrategy);
            double result = calculator.ExecuteOperation(firstNumber);
            Console.WriteLine("Результат: " + result);

            Console.ReadKey();
        }

        
    }


    public interface IOperation
    {
        double Execute(double firstNumber);
    }

    public class AddOperation : IOperation
    {
        private double _secondNumber;

        public AddOperation(double secondNumber)
        {
            _secondNumber = secondNumber;
        }

        public double Execute(double firstNumber)
        {
            return firstNumber + _secondNumber;
        }
    }

    public class SubtractOperation : IOperation
    {
        private double _secondNumber;

        public SubtractOperation(double secondNumber)
        {
            _secondNumber = secondNumber;
        }

        public double Execute(double firstNumber)
        {
            return firstNumber - _secondNumber;
        }
    }

    public class MultiplyOperation : IOperation
    {
        private double _secondNumber;

        public MultiplyOperation(double secondNumber)
        {
            _secondNumber = secondNumber;
        }

        public double Execute(double firstNumber)
        {
            return firstNumber * _secondNumber;
        }
    }

    public class DivideOperation : IOperation
    {
        private double _secondNumber;

        public DivideOperation(double secondNumber)
        {
            if (secondNumber == 0)
            {
                throw new DivideByZeroException("Нельзя делить на ноль!");
            }
            _secondNumber = secondNumber;
        }

        public double Execute(double firstNumber)
        {
            return firstNumber / _secondNumber;
        }
    }

    public class SquareRootOperation : IOperation
    {
        public double Execute(double firstNumber)
        {
            if (firstNumber < 0)
            {
                throw new ArgumentException("Нельзя извлечь квадратный корень из отрицательного числа!");
            }
            return Math.Sqrt(firstNumber);
        }
    }


    public class Calculator
    {
        private IOperation _operation;

        public void SetOperation(IOperation operation)
        {
            _operation = operation;
        }

        public double ExecuteOperation(double firstNumber)
        {
            return _operation.Execute(firstNumber);
        }
    }

}
