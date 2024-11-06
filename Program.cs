namespace Baskara;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Digite a equação do segundo grau no formato 'ax^2 + bx + c = 0':");
        string expressao = Console.ReadLine();

        var coeficientes = EquacaoSegundoGrau.ExtrairCoeficientes(expressao);

        if (coeficientes == null)
        {
            Console.WriteLine("Expressão inválida. Certifique-se de que está no formato 'ax^2 + bx + c = 0'.");
            return;
        }

        Console.WriteLine($"Coeficiente A:{coeficientes.Value.a}");
        Console.WriteLine($"Coeficiente B:{coeficientes.Value.b}");
        Console.WriteLine($"Coeficiente C:{coeficientes.Value.c}");

        try
        {
            var equacao = new EquacaoSegundoGrau(coeficientes.Value.a, coeficientes.Value.b, coeficientes.Value.c);
            var (x1, x2, mensagem) = equacao.Resolver();

            Console.WriteLine($"\nDiscriminante (Δ): {equacao.CalcularDiscriminante()}");
            Console.WriteLine(mensagem);

            if (x1.HasValue && x2.HasValue)
            {
                Console.WriteLine($"x₁ = {x1.Value}");
                Console.WriteLine($"x₂ = {x2.Value}");
            }
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }

        Console.WriteLine("Pressione uma tecla para continuar.");
        Console.ReadKey();
    }
}
