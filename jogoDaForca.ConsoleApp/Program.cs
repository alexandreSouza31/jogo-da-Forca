using System;

namespace jogoDaForca.ConsoleApp
{
    public class Categorias
    {
        public string[] frutas = { "ABACATE", "ABACAXI", "ACEROLA", "ACAI", "ARACA", "BACABA", "BACURI", "BANANA",
                    "CAJA", "CAJU", "CARAMBOLA", "CUPUACU", "GRAVIOLA", "GOIABA", "JABUTICABA", "JENIPAPO",
                    "MACA", "MANGABA", "MANGA", "MARACUJA", "MURICI", "PEQUI", "PITANGA", "PITAYA", "SAPOTI", "TANGERINA", "UMBU", "UVA", "UVAIA"
                };

        public string[] animais = { "ABELHA", "AGUIA", "ARANHA", "ARRAIA", "BALEIA", "CACHORRO", "CANGURU", "CAVALO", "COBRA",
                    "CROCODILO", "ELEFANTE", "FALCAO", "FLAMINGO", "FORMIGA", "GATO", "GIRAFA", "GOLFINHO", "HIPOPOTAMO", "JACARE",
                    "LEMURE", "LEAO", "LOBO", "MACACO", "PINGUIM", "PEIXE", "RINOCERONTE", "TARTARUGA", "TIGRE", "URSO", "ZEBRA"
                };

        public string[] paises = { "AFEGANISTAO", "ALEMANHA", "ANGOLA", "ARGENTINA", "AUSTRALIA", "BRASIL", "CANADA", "CHINA", "EGITO",
                    "ESPANHA", "ESTADOS UNIDOS", "FRANCA", "GRECIA", "INDIA", "INDONESIA", "IRA", "ITALIA", "JAPAO", "MEXICO", "MOCAMBIQUE",
                    "NIGERIA", "NOVA ZELANDIA", "PAQUISTAO", "PERU", "PORTUGAL", "REINO UNIDO", "RUSSIA", "SUECIA", "TAILANDIA", "TURQUIA"
                };
    }
    public class Exibir
    {
        public static int qtdErros;
        public static int totalTentativas;
        public static string historicoChutesJoin;
        public static char[] letrasEncontradas;
        public static string categoria;

        public static void CabecalhoJogo()
        {
            string cabecaDoBoneco = qtdErros >= 1 ? " O " : " ";
            string tronco = qtdErros >= 2 ? "x" : " ";
            string troncoBaixo = qtdErros >= 2 ? " x " : " ";
            string bracoEsquerdo = qtdErros >= 3 ? "/" : " ";
            string bracoDireito = qtdErros >= 4 ? @"\" : " ";
            string pernas = qtdErros >= totalTentativas ? "/ \\" : " ";

            Console.Clear();
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
            Console.WriteLine($" |{String.Join(' ', letrasEncontradas)} | {letrasEncontradas.Length} letras");
            Console.WriteLine($"\nErros do jogador: {qtdErros} de {totalTentativas}");
            Console.WriteLine("\nHistórico de tentativas: " + historicoChutesJoin);
            Console.WriteLine($"Categoria: {categoria}");
            Console.WriteLine("----------------------------------------------");
        }

        public static string DesejaContinuar()
        {
            Console.Write("Deseja continuar? (S/N): ");

            string opcaoContinuar = Console.ReadLine().ToUpper();
            if (opcaoContinuar.Length == 0) ;
            return opcaoContinuar;
        }
    }

    public class Palavra
    {
        public static char EscolherCategoria()
        {
            //CabecalhoJogo();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("              Jogo da Forca");
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("Escolha uma categoria de palavra:");
                Console.WriteLine("F - Frutas");
                Console.WriteLine("A - Animais");
                Console.WriteLine("P - Países");

                string palavraUsuarioImput = Console.ReadLine()!.ToUpper();
                if (palavraUsuarioImput.Length == 0) continue;

                char categoriaPalavra = palavraUsuarioImput[0];

                if (categoriaPalavra == 'F' || categoriaPalavra == 'A' || categoriaPalavra == 'P')
                {
                    return categoriaPalavra;
                }
                Console.Write("Escolha uma categoria válida! Pressione Enter para tentar novamente.");
                Console.ReadLine();
            }
        }

        public static string EscolherPalavra()
        {
            char metodoCategoriaPalavra = EscolherCategoria();

            if (metodoCategoriaPalavra == 'F') Exibir.categoria = "Fruta";
            else if (metodoCategoriaPalavra == 'A') Exibir.categoria = "Animal";
            else Exibir.categoria = "País";

            Categorias categorias = new Categorias();

            Random geradorDePalavras = new Random();
            int indiceAleatorio = 0;
            string palavraEscolhida = "";

            if (Exibir.categoria == "Fruta")
            {
                indiceAleatorio = geradorDePalavras.Next(categorias.frutas.Length);
                palavraEscolhida = categorias.frutas[indiceAleatorio];
            }
            else if (Exibir.categoria == "Animal")
            {
                indiceAleatorio = geradorDePalavras.Next(categorias.animais.Length);
                palavraEscolhida = categorias.animais[indiceAleatorio];
            }
            else
            {
                indiceAleatorio = geradorDePalavras.Next(categorias.paises.Length);
                palavraEscolhida = categorias.paises[indiceAleatorio];
            }
            return palavraEscolhida;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string palavraSecreta = Palavra.EscolherPalavra();

                int totalTentativas = 6;
                Exibir.totalTentativas=totalTentativas;
                string[] chutesRealizados = new string[100];
                int contadorChutes = 0;

                Exibir.letrasEncontradas = new char[palavraSecreta.Length];
                for (int caractere = 0; caractere < Exibir.letrasEncontradas.Length; caractere++)
                {
                    Exibir.letrasEncontradas[caractere] = '_';
                }

                Exibir.qtdErros = 0;
                bool jogadorEnforcou = false;
                bool jogadorAcertou = false;

                do
                {
                    Exibir.historicoChutesJoin = string.Join(", ", chutesRealizados.Where(n => !string.IsNullOrEmpty(n)));
                    Exibir.CabecalhoJogo();

                    Console.Write("Digite uma letra: ");
                    string chuteUsuario = Console.ReadLine().ToUpper();

                    if (chuteUsuario.Length == 1)
                    {
                        char chute = chuteUsuario[0];

                        if (Array.Exists(chutesRealizados, n => n == chute.ToString()) && !palavraSecreta.Contains(chute))
                        {
                            Console.WriteLine($"\n{chute} já foi digitado, e é diferente do sorteado!", "\n");
                            Console.Write("Digite [Enter] para continuar:");
                            Console.ReadLine();
                            continue;
                        }
                        else if (Array.Exists(chutesRealizados, n => n == chute.ToString()) && palavraSecreta.Contains(chute))
                        {
                            Console.WriteLine($"\n{chute} já foi digitado, e está na palavra!Digite outra letra!", "\n");
                            Console.Write("Digite [Enter] para continuar:");
                            Console.ReadLine();
                        }

                        chutesRealizados[contadorChutes++] = chute.ToString();

                        bool letraFoiEncontrada = false;

                        for (int contador = 0; contador < palavraSecreta.Length; contador++)
                        {
                            char letraAtual = palavraSecreta[contador];

                            if (chute == letraAtual)
                            {
                                Exibir.letrasEncontradas[contador] = letraAtual;
                                letraFoiEncontrada = true;
                            }
                        }

                        jogadorAcertou = new string(Exibir.letrasEncontradas) == palavraSecreta;

                        if (letraFoiEncontrada == false && !jogadorAcertou)
                        {
                            Exibir.qtdErros++;
                        }
                    }
                    else if (chuteUsuario.Length > 1)
                    {
                        if (chuteUsuario == palavraSecreta)
                        {
                            jogadorAcertou = true;
                        }
                        else
                        {
                            Console.WriteLine("Você errou a palavra inteira!");
                            Exibir.qtdErros++;
                            Console.Write("Digite [Enter] para continuar:");
                        }
                    }

                    jogadorEnforcou = Exibir.qtdErros >= totalTentativas;

                    if (jogadorAcertou)
                    {
                        Console.WriteLine("----------------------------------------------");
                        Console.WriteLine($"Conseguiu! A palavra secreta era {palavraSecreta}, parabéns!");
                        Console.WriteLine("----------------------------------------------");
                    }
                    else if (jogadorEnforcou)
                    {
                        Exibir.CabecalhoJogo();
                        Console.WriteLine("Você perdeu! A palavra era " + palavraSecreta);
                        Console.WriteLine("Tente Novamente!");
                        Console.ReadLine();
                    }

                }
                while (jogadorAcertou == false && jogadorEnforcou == false);

                string opcaoContinuar = Exibir.DesejaContinuar();

                if (opcaoContinuar != "S") break;
                continue;
            }
        }
    }
}
