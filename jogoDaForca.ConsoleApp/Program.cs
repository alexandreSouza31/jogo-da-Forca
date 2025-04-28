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

    public class Jogador
    {
        public string nome;
        public int qtdErros;
        public string[] chutesRealizados = new string[100];
        public int contadorChutes;
        public int totalTentativas;
        public string historicoChutesJoin;
        public bool jogadorAcertou;
    }
    public class Exibir
    {
        public static string nomeJogo = "Jogo da Forca";
        public static char[] letrasEncontradas;
        public static string categoria;

        public static void NomeDoJogo()
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"              {nomeJogo}");
            Console.WriteLine("----------------------------------------------");
        }
        public static void CabecalhoJogo(Jogador jogador)
        {
            string cabecaDoBoneco = jogador.qtdErros >= 1 ? " O " : " ";
            string tronco = jogador.qtdErros >= 2 ? "x" : " ";
            string troncoBaixo = jogador.qtdErros >= 2 ? " x " : " ";
            string bracoEsquerdo = jogador.qtdErros >= 3 ? "/" : " ";
            string bracoDireito = jogador.qtdErros >= 4 ? @"\" : " ";
            string pernas = jogador.qtdErros >= jogador.totalTentativas ? "/ \\" : " ";

            Console.Clear();
            NomeDoJogo();
            Console.WriteLine($"Jogador:{jogador.nome}");
            Console.WriteLine(" ___________        ");
            Console.WriteLine(" |/        |        ");
            Console.WriteLine(" |        {0}       ", cabecaDoBoneco);
            Console.WriteLine(" |        {0}{1}{2} ", bracoEsquerdo, tronco, bracoDireito);
            Console.WriteLine(" |        {0}       ", troncoBaixo);
            Console.WriteLine(" |        {0}       ", pernas);
            Console.WriteLine(" |                  ");
            Console.WriteLine(" |                  ");
            Console.WriteLine($" |{String.Join(' ', letrasEncontradas)} | {letrasEncontradas.Length} letras");
            Console.WriteLine($"\nErros do jogador: {jogador.qtdErros} de {jogador.totalTentativas}");
            Console.WriteLine("\nHistórico de tentativas: " + jogador.historicoChutesJoin);
            Console.WriteLine($"Categoria: {categoria}");
            Console.WriteLine("----------------------------------------------");
        }

        public static string DesejaContinuar()
        {
            Console.Write("Deseja continuar? (S/N): ");

            string opcaoContinuar = Console.ReadLine()!.ToUpper();
            if (opcaoContinuar.Length == 0) ;
            return opcaoContinuar;
        }

        public static void MensagemFinal(Jogador jogador, bool jogadorEnforcou)
        {
            if (jogador.jogadorAcertou)
            {
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine($"Conseguiu, {jogador.nome}! A palavra secreta era {Palavra.palavraSecreta}, parabéns!");
                Console.WriteLine("----------------------------------------------");
            }
            else if (jogadorEnforcou)
            {
                Exibir.CabecalhoJogo(jogador);
                Console.WriteLine($"Você perdeu, {jogador.nome}! A palavra era {Palavra.palavraSecreta}");
                Console.WriteLine("Tente Novamente!");
                Console.ReadLine();
            }
        }
    }

    public class Palavra
    {
        public static string palavraSecreta;

        public static char EscolherCategoria(Jogador jogador)
        {
            while (true)
            {
                Console.Clear();
                Exibir.NomeDoJogo();
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

        public static void EscolherPalavra(Jogador jogador)
        {
            char metodoCategoriaPalavra = EscolherCategoria(jogador);

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

        public static void TentarLetra(Jogador jogador, string chuteUsuario)
        {
            char chute = chuteUsuario[0];

            if (Array.Exists(jogador.chutesRealizados, n => n == chute.ToString()) && !palavraSecreta.Contains(chute))
            {
                Console.WriteLine($"\n{chute} já foi digitado, e é diferente do sorteado!", "\n");
                Console.Write("Digite [Enter] para continuar:");
                Console.ReadLine();
                return;
            }
            else if (Array.Exists(jogador.chutesRealizados, n => n == chute.ToString()) && palavraSecreta.Contains(chute))
            {
                Console.WriteLine($"\n{chute} já foi digitado, e está na palavra!Digite outra letra!", "\n");
                Console.Write("Digite [Enter] para continuar:");
                Console.ReadLine();
            }

            jogador.chutesRealizados[jogador.contadorChutes++] = chute.ToString();

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

            jogador.jogadorAcertou = new string(Exibir.letrasEncontradas) == palavraSecreta;

            if (letraFoiEncontrada == false && !jogador.jogadorAcertou)
            {
                jogador.qtdErros++;
            }
        }

        public static void TentarPalavra(Jogador jogador, string chuteUsuario)
        {
            if (chuteUsuario.Replace(" ", "") == palavraSecreta.Replace(" ", "")) jogador.jogadorAcertou = true;
            else
            {
                Console.WriteLine("Você errou a palavra inteira!");
                jogador.qtdErros++;
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
                Console.Write("Quantos jogadores vão jogar? ");
                int numeroJogadores = int.Parse(Console.ReadLine()!);

                Jogador[] jogadores = new Jogador[numeroJogadores];

                for (int i = 0; i < numeroJogadores; i++)
                {
                    jogadores[i] = new Jogador();
                    Console.Write($"Digite o nome do jogador {i + 1}: ");
                    jogadores[i].nome = Console.ReadLine()!;
                }

                Palavra.EscolherPalavra(jogadores[0]);

                for (int i = 0; i < jogadores.Length; i++)
                {
                    PrepararRodada(jogadores[i]);
                }

                IniciarPartida(jogadores);

                string opcaoContinuar = Exibir.DesejaContinuar();
                if (opcaoContinuar != "S") break;
            }
        }

        static void PrepararRodada(Jogador jogador)
        {
            jogador.jogadorAcertou = false;
            jogador.totalTentativas = 6;
            jogador.contadorChutes = 0;
            jogador.qtdErros = 0;

            Exibir.letrasEncontradas = new char[Palavra.palavraSecreta.Length];
            for (int i = 0; i < Exibir.letrasEncontradas.Length; i++)
            {
                if (Palavra.palavraSecreta[i] == ' ') Exibir.letrasEncontradas[i] = ' ';
                else Exibir.letrasEncontradas[i] = '_';
            }
        }

        static void IniciarPartida(Jogador[] jogadores)
        {
            bool palavraEncontrada = false;
            bool todosEnforcados = false;
            int jogadorAtualIndex = 0;

            do
            {
                Jogador jogadorAtual = jogadores[jogadorAtualIndex];

                if (jogadorAtual.qtdErros >= jogadorAtual.totalTentativas)
                {
                    jogadorAtualIndex = (jogadorAtualIndex + 1) % jogadores.Length;
                    continue;
                }

                jogadorAtual.historicoChutesJoin = string.Join(", ", jogadorAtual.chutesRealizados.Where(n => !string.IsNullOrEmpty(n)));
                Exibir.CabecalhoJogo(jogadorAtual);

                Console.Write($"Digite uma letra ou a palavra, {jogadorAtual.nome}: ");
                string chuteUsuario = Console.ReadLine().ToUpper();
                if (string.IsNullOrWhiteSpace(chuteUsuario)) continue;

                if (chuteUsuario.Length == 1)
                    Palavra.TentarLetra(jogadorAtual, chuteUsuario);
                else
                    Palavra.TentarPalavra(jogadorAtual, chuteUsuario);

                palavraEncontrada = jogadorAtual.jogadorAcertou;

                Exibir.MensagemFinal(jogadorAtual, jogadorAtual.qtdErros >= jogadorAtual.totalTentativas);

                todosEnforcados = true;
                for (int i = 0; i < jogadores.Length; i++)
                {
                    if (jogadores[i].qtdErros < jogadores[i].totalTentativas)
                    {
                        todosEnforcados = false;
                        break;
                    }
                }

                jogadorAtualIndex = (jogadorAtualIndex + 1) % jogadores.Length;

            } while (!palavraEncontrada && !todosEnforcados);
        }
    }
}
