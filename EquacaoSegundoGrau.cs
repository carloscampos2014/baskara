using System.Text.RegularExpressions;

namespace Baskara;
using System;
using System.Text.RegularExpressions;

using System;
using System.Text.RegularExpressions;

public class EquacaoSegundoGrau
{
    public double A { get; private set; }
    public double B { get; private set; }
    public double C { get; private set; }

    public EquacaoSegundoGrau(double a, double b, double c)
    {
        if (a == 0)
        {
            throw new ArgumentException("O coeficiente 'a' deve ser diferente de zero para ser uma equação do segundo grau.");
        }

        A = a;
        B = b;
        C = c;
    }
    public static (double a, double b, double c)? ExtrairCoeficientes(string expressao)
    {
        // Modifiquei o regex para capturar o sinal correto de cada coeficiente
        var padrao = @"(?<a>-?\d*\.?\d*)x\^2\s*(?<bSign>[+-]?)\s*(?<b>\d*\.?\d*)x\s*(?<cSign>[+-]?)\s*(?<c>\d*\.?\d*)\s*=\s*0";
        var match = Regex.Match(expressao, padrao);

        if (match.Success)
        {
            // Extrair coeficiente 'a'
            double a = string.IsNullOrEmpty(match.Groups["a"].Value) ? 1 : double.Parse(match.Groups["a"].Value.Replace(" ", ""));

            // Extrair coeficiente 'b' e considerar o sinal
            string bSign = match.Groups["bSign"].Value.Replace(" ", "");
            double b = string.IsNullOrEmpty(match.Groups["b"].Value) ? 0 : double.Parse(match.Groups["b"].Value.Replace(" ", ""));
            if (bSign == "-") b = -b;

            // Extrair coeficiente 'c' e considerar o sinal
            string cSign = match.Groups["cSign"].Value.Replace(" ", "");
            double c = string.IsNullOrEmpty(match.Groups["c"].Value) ? 0 : double.Parse(match.Groups["c"].Value.Replace(" ", ""));
            if (cSign == "-") c = -c;

            return (a, b, c);
        }
        return null;
    }

    public double CalcularDiscriminante()
    {
        return Math.Pow(B, 2) - 4 * A * C;
    }

    public (double? x1, double? x2, string mensagem) Resolver()
    {
        double delta = CalcularDiscriminante();

        if (delta > 0)
        {
            double raizDelta = Math.Sqrt(delta);
            double x1 = ((-1 * B) + raizDelta) / (2 * A);
            double x2 = ((-1 * B) - raizDelta) / (2 * A);
            return (x1, x2, "Existem duas raízes reais e diferentes.");
        }
        else if (delta == 0)
        {
            double x = -B / (2 * A);
            return (x, x, "Existe uma raiz real dupla.");
        }
        else
        {
            return (null, null, "Não existem raízes reais.");
        }
    }
}
