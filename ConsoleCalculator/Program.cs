using System;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.WriteLine("Введите математическое выражение:");
string expression = Console.ReadLine();

try
{
#pragma warning disable CS8604 // Possible null reference argument.
    double result = Calculate(expression: expression);
#pragma warning restore CS8604 // Possible null reference argument.
    Console.WriteLine("Результат: " + result);
}
catch (Exception e)
{
    Console.WriteLine("Ошибка: " + e.Message);
}

Console.ReadLine();

double Calculate(string expression)
{
    expression = expression.Replace(" ", ""); // Удаляем пробелы из выражения

    return EvaluateExpression(expression);
}

double EvaluateExpression(string expression)
{
    char[] operators = { '+', '-', '*', '/' };
    foreach (char op in operators)
    {
        int opPos = expression.LastIndexOf(op);
        if (opPos != -1)
        {
            double leftOperand = EvaluateExpression(expression.Substring(0, opPos));
            double rightOperand = EvaluateExpression(expression.Substring(opPos + 1));
            return PerformOperation(leftOperand, rightOperand, op);
        }
    }

    double operand;
    if (double.TryParse(expression, out operand))
    {
        return operand;
    }

    throw new ArgumentException("Неправильный формат выражения.");
}

double PerformOperation(double leftOperand, double rightOperand, char op)
{
    switch (op)
    {
        case '+':
            return leftOperand + rightOperand;
        case '-':
            return leftOperand - rightOperand;
        case '*':
            return leftOperand * rightOperand;
        case '/':
            return leftOperand / rightOperand;
        default:
            throw new ArgumentException("Неподдерживаемая операция: " + op);
    }
}
