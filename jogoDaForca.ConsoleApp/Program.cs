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

        public string[] paises = { "ESTADOS UNIDOS", "FRANCA", "GRECIA", "INDIA", "INDONESIA", "IRA", "ITALIA", "JAPAO", "MEXICO", "MOCAMBIQUE",
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

        public static void MensagemFinal(bool jogadorEnforcou)
        {
            if (Palavra.jogadorAcertou)
            {
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine($"Conseguiu! A palavra secreta era {Palavra.palavraSecreta}, parabéns!");
                Console.WriteLine("----------------------------------------------");
            }
            else if (jogadorEnforcou)
            {
                Exibir.CabecalhoJogo();
                Console.WriteLine("Você perdeu! A palavra era " + Palavra.palavraSecreta);
                Console.WriteLine("Tente Novamente!");
                Console.ReadLine();
            }
        }
    }

    public class Palavra
    {
        public static string palavraSecreta;
        public static string[] chutesRealizados = new string[100];
        public static int contadorChutes;
        public static bool jogadorAcertou;

        public static char EscolherCategoria()
        {
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

                string palavraUsuarioImput = Console.ReadLine().ToUpper();
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

        public static void EscolherPalavra()
        {
            char metodoCategoriaPalavra = EscolherCategoria();

            if (metodoCategoriaPalavra == 'F') Exibir.categoria = "Fruta";
            else if (metodoCategoriaPalavra == 'A') Exibir.categoria = "Animal";
            else Exibir.categoria = "País";

            Categorias categorias = new Categorias();

            Random geradorDePalavras = new Random();
            int indiceAleatorio = 0;

            if (Exibir.categoria == "Fruta")
                palavraSecreta = categorias.frutas[geradorDePalavras.Next(categorias.frutas.Length)];
            else if (Exibir.categoria == "Animal")
                palavraSecreta = categorias.animais[geradorDePalavras.Next(categorias.animais.Length)];
            else
                palavraSecreta = categorias.paises[geradorDePalavras.Next(categorias.paises.Length)];
        }

        public static void TentarLetra(string chuteUsuario)
        {
            char chute = chuteUsuario[0];

            if (Array.Exists(chutesRealizados, n => n == chute.ToString()) && !palavraSecreta.Contains(chute))
            {
                Console.WriteLine($"\n{chute} já foi digitado, e é diferente do sorteado!", "\n");
                Console.Write("Digite [Enter] para continuar:");
                Console.ReadLine();
                return;
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

        public static void TentarPalavra(string chuteUsuario)
        {
            if (chuteUsuario.Replace(" ", "") == palavraSecreta.Replace(" ", "")) jogadorAcertou = true;
            else
            {
                Console.WriteLine("Você errou a palavra inteira!");
                Exibir.qtdErros++;
                Console.Write("Digite [Enter] para continuar:");
                Console.ReadLine();
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Palavra.EscolherPalavra();
                Array.Clear(Palavra.chutesRealizados, 0, Palavra.chutesRealizados.Length);
                IniciarRodada();

                bool jogadorEnforcou = false;

                do
                {
                    Exibir.historicoChutesJoin = string.Join(", ", Palavra.chutesRealizados.Where(n => !string.IsNullOrEmpty(n)));
                    Exibir.CabecalhoJogo();

                    Console.Write("Digite uma letra ou a palavra: ");
                    string chuteUsuario = Console.ReadLine().ToUpper();
                    if (string.IsNullOrWhiteSpace(chuteUsuario)) continue;

                    if (chuteUsuario.Length == 1) Palavra.TentarLetra(chuteUsuario);
                    else Palavra.TentarPalavra(chuteUsuario);

                    jogadorEnforcou = Exibir.qtdErros >= Exibir.totalTentativas;
                    Exibir.MensagemFinal(jogadorEnforcou);
                }
                while (Palavra.jogadorAcertou == false && jogadorEnforcou == false);

                string opcaoContinuar = Exibir.DesejaContinuar();
                if (opcaoContinuar != "S") break;
                continue;
            }
        }

        static void IniciarRodada()
        {
            Palavra.jogadorAcertou = false;
            Exibir.totalTentativas = 6;
            Palavra.contadorChutes = 0;
            Exibir.qtdErros = 0;

            Exibir.letrasEncontradas = new char[Palavra.palavraSecreta.Length];
            for (int i = 0; i < Exibir.letrasEncontradas.Length; i++)
            {
                if (Palavra.palavraSecreta[i] == ' ') Exibir.letrasEncontradas[i] = ' ';
                else Exibir.letrasEncontradas[i] = '_';
            }
        }

    }
}
