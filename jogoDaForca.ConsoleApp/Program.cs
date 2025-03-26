using System;

namespace jogoDaForca.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true) {
                string[] frutas = {"ABACATE","ABACAXI","ACEROLA","ACAI","ARACA","ABACATE","BACABA","BACURI","BANANA",
                    "CAJA","CAJU","CARAMBOLA","CUPUACU","GRAVIOLA","GOIABA","JABUTICABA","JENIPAPO",
                    "MACA","MANGABA","MANGA","MARACUJA","MURICI","PEQUI","PITANGA","PITAYA","SAPOTI","TANGERINA","UMBU","UVA","UVAIA"
                };

                Random geradorDeFrutas = new Random();
                int indiceAleatorio = geradorDeFrutas.Next(frutas.Length);
                string palavraSecreta = frutas[indiceAleatorio];
                int totalTentativas = 5;

                string[] chutesRealizados = new string[100];
                int contadorChutes = 0;

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
                string historicoChutesJoin = string.Join(", ", chutesRealizados.Where(n => !string.IsNullOrEmpty(n)));
                    string cabecaDoBoneco = qtdErros >= 1 ? " o " : " ";
                    string tronco = qtdErros >= 2 ? "x" : " ";
                    string troncoBaixo = qtdErros >= 2 ? " x " : " ";
                    string bracoEsquerdo = qtdErros >= 3 ? "/" : " ";
                    string bracoDireito = qtdErros >= 4 ? @"\" : " ";
                    string pernas = qtdErros >= totalTentativas ? "/ \\" : " ";

                    Console.Clear();
                    //Console.WriteLine("gabariro: "+ palavraSecreta);
                    Console.WriteLine("----------------------------------------------");
                    Console.WriteLine("              Jogo da Forca");
                    Console.WriteLine("----------------------------------------------");
                    Console.WriteLine(" ___________        ");
                    Console.WriteLine(" |/        |        ");
                    Console.WriteLine(" |        {0}       ", cabecaDoBoneco);
                    Console.WriteLine(" |        {0}{1}{2} ", bracoEsquerdo, tronco, bracoDireito);
                    Console.WriteLine(" |        {0}       ", troncoBaixo);
                    Console.WriteLine(" |        {0}       ", pernas);
                    Console.WriteLine(" |                  ");
                    Console.WriteLine(" |                  ");
                    Console.WriteLine("_|____              ");

                    Console.WriteLine($"\nErros do jogador: {qtdErros} de {totalTentativas}");
                    Console.WriteLine("\nHistórico de tentativas: " + historicoChutesJoin);
                    Console.WriteLine("----------------------------------------------");
                    Console.WriteLine("Palavra escolhida: " + String.Join(" ", letrasEncontradas));

                    Console.Write("Digite uma letra: ");
                    char chute = Console.ReadLine().ToUpper()[0];
                    Console.WriteLine(chute);

                    if (Array.Exists(chutesRealizados, n => n == chute.ToString()) && !palavraSecreta.Contains(chute))
                    {
                        Console.WriteLine($"\n{chute} já foi digitado, e é diferente do sorteado!", "\n");
                        Console.Write("Digite [Enter] para continuar:");
                        Console.ReadLine();
                        continue;
                    }else if(Array.Exists(chutesRealizados, n => n == chute.ToString()) && palavraSecreta.Contains(chute))
                    {
                        Console.WriteLine($"\n{chute} já foi digitado, e está na palavra!Digite outra letra!", "\n");
                        Console.Write("Digite [Enter] para continuar:");
                        Console.ReadLine();
                    }


                    chutesRealizados[contadorChutes++] = chute.ToString();

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

                    jogadorAcertou = new string(letrasEncontradas) == palavraSecreta;


                    jogadorEnforcou = qtdErros > totalTentativas;

                    if (jogadorAcertou)
                    {
                        Console.WriteLine("----------------------------------------------");
                        Console.WriteLine($"Acertou! A palavra secreta era {palavraSecreta}, parabéns!");
                        Console.WriteLine("----------------------------------------------");
                    }
                    else if (jogadorEnforcou)
                    {
                        Console.WriteLine("Errou! A palavra era "+palavraSecreta);
                        Console.WriteLine("Tente Novamente!");
                        Console.ReadLine();

                    }

                }
                while (jogadorAcertou==false && jogadorEnforcou==false);

                Console.Write("Deseja continuar? (S/N): ");

                string opcaoContinuar = Console.ReadLine().ToUpper();

                if (opcaoContinuar != "S")
                    break;

            }
        }
    }
}