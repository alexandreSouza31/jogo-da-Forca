namespace jogoDaForca.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true) {
                string palavraSecreta = "MELANCIA";

                char[] letrasEncontradas = new char[palavraSecreta.Length];

                for (int caractere = 0; caractere < letrasEncontradas.Length; caractere++)
                {
                    letrasEncontradas[caractere] = '_';
                }

                int qtdErros = 0;
                bool jogadorEnforcou = false;
                bool jogadorAcertou = false;


                do
                {
                    string dicaPalavra = String.Join(" ", letrasEncontradas);
                    jogadorEnforcou = qtdErros > 5;

                    Console.Clear();
                    Console.WriteLine("----------------------------------------------");
                    Console.WriteLine("              Jogo da Forca");
                    Console.WriteLine("----------------------------------------------");
                    Console.WriteLine(" ___________        ");
                    Console.WriteLine(" |/        |        ");
                    Console.WriteLine(" |                  ");
                    Console.WriteLine(" |                  ");
                    Console.WriteLine(" |                  ");
                    Console.WriteLine(" |                  ");
                    Console.WriteLine(" |                  ");
                    Console.WriteLine(" |                  ");
                    Console.WriteLine("_|____              ");

                    Console.WriteLine("\nErros do jogador: " + qtdErros);
                    Console.WriteLine("----------------------------------------------");
                    Console.WriteLine("Palavra escolhida: " + String.Join(" ", letrasEncontradas));
                    Console.Write("Digite uma letra: ");
                    char chute = Console.ReadLine().ToUpper()[0];
                    Console.WriteLine(chute);


                    bool letraFoiEncontrada=false;

                    for(int contador = 0; contador < palavraSecreta.Length; contador++)
                    {
                        char letraAtual = palavraSecreta[contador];

                        if (chute == letraAtual)
                        {
                            letrasEncontradas[contador] = letraAtual;
                            letraFoiEncontrada = true;
                        }
                    }

                    if (letraFoiEncontrada == false && !jogadorAcertou)
                    {
                        qtdErros++;
                    }

                    dicaPalavra = String.Join(" ", letrasEncontradas);

                    jogadorAcertou = new string(letrasEncontradas) == palavraSecreta;

                    jogadorEnforcou = qtdErros >= 5;

                    if (jogadorAcertou)
                    {
                        Console.WriteLine("Acertou! A palavra era "+palavraSecreta);
                        Console.ReadLine();
                        continue;
                    }
                    else if (jogadorEnforcou)
                    {
                        Console.WriteLine("Errou! A palavra era "+palavraSecreta);
                        Console.WriteLine("Tente Novamente!");
                        Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("----------------------------------------------");
                        Console.WriteLine("              Jogo da Forca");
                        Console.WriteLine("----------------------------------------------");
                        Console.WriteLine(" ___________        ");
                        Console.WriteLine(" |/        |        ");
                        Console.WriteLine(" |                  ");
                        Console.WriteLine(" |                  ");
                        Console.WriteLine(" |                  ");
                        Console.WriteLine(" |                  ");
                        Console.WriteLine(" |                  ");
                        Console.WriteLine(" |                  ");
                        Console.WriteLine("_|____              ");

                        Console.WriteLine("\nErros do jogador: " + qtdErros);
                        Console.WriteLine("----------------------------------------------");
                        Console.WriteLine("Palavra escolhida: " + String.Join(" ", palavraSecreta));
                        Console.ReadLine();

                    }

                }
                while (jogadorAcertou==false && jogadorEnforcou==false);

            }
        }
    }
}