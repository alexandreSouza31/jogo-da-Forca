using System;

namespace jogoDaForca.ConsoleApp
{
    internal class Program
    {

        static void cabecalhoJogo(int qtdErros,int totalTentativas,string historicoChutesJoin, char[]letrasEncontradas, string categoria)
        {
            string cabecaDoBoneco = qtdErros >= 1 ? " O " : " ";
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
            Console.WriteLine($" |{String.Join(' ', letrasEncontradas)} | {letrasEncontradas.Length} letras");

            Console.WriteLine($"\nErros do jogador: {qtdErros} de {totalTentativas}");
            Console.WriteLine("\nHistórico de tentativas: " + historicoChutesJoin);
            Console.WriteLine($"Categoria: {categoria}");
            Console.WriteLine("----------------------------------------------");
        }

        static char  escolherCategoriaPalavra()
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

                if (categoriaPalavra== 'F' || categoriaPalavra == 'A' || categoriaPalavra == 'P')
                {
                    return categoriaPalavra;
                }
                    Console.Write("Escolha uma categoria válida! Pressione Enter para tentar novamente.");
                Console.ReadLine();

            }
        }

        static void Main(string[] args)
        {
            while (true) {

                char metodoCategoriaPalavra = escolherCategoriaPalavra();

                string categoria = "";
                if (metodoCategoriaPalavra == 'F') categoria = "Fruta";

                else if (metodoCategoriaPalavra == 'A') categoria = "Animal";

                else categoria = "País";

                string[] frutas = {"ABACATE","ABACAXI","ACEROLA","ACAI","ARACA","BACABA","BACURI","BANANA",
                    "CAJA","CAJU","CARAMBOLA","CUPUACU","GRAVIOLA","GOIABA","JABUTICABA","JENIPAPO",
                    "MACA","MANGABA","MANGA","MARACUJA","MURICI","PEQUI","PITANGA","PITAYA","SAPOTI","TANGERINA","UMBU","UVA","UVAIA"
                };

                string[] animais = {"ABELHA", "AGUIA", "ARANHA", "ARRAIA", "BALEIA", "CACHORRO", "CANGURU", "CAVALO", "COBRA", 
                    "CROCODILO", "ELEFANTE", "FALCAO", "FLAMINGO", "FORMIGA", "GATO", "GIRAFA", "GOLFINHO", "HIPOPOTAMO", "JACARE",
                    "LEMURE", "LEAO", "LOBO", "MACACO", "PINGUIM", "PEIXE", "RINOCERONTE", "TARTARUGA", "TIGRE", "URSO", "ZEBRA"
                };

                string[] paises = {"AFEGANISTAO", "ALEMANHA", "ANGOLA", "ARGENTINA", "AUSTRALIA", "BRASIL", "CANADA", "CHINA", "EGITO", 
                    "ESPANHA", "ESTADOS UNIDOS", "FRANCA", "GRECIA", "INDIA", "INDONESIA", "IRA", "ITALIA", "JAPAO", "MEXICO", "MOCAMBIQUE",
                    "NIGERIA", "NOVA ZELANDIA", "PAQUISTAO", "PERU", "PORTUGAL", "REINO UNIDO", "RUSSIA", "SUECIA", "TAILANDIA", "TURQUIA"
                };

                Random geradorDePalavras = new Random();
                int indiceAleatorio = 0;
                string palavraSecreta = "";

                if (categoria == "Fruta") 
                {
                    indiceAleatorio = geradorDePalavras.Next(frutas.Length);
                    palavraSecreta = frutas[indiceAleatorio];
                }

                else if (categoria == "Animal") 
                {
                    indiceAleatorio = geradorDePalavras.Next(animais.Length);
                    palavraSecreta = animais[indiceAleatorio];
                }

                else 
                { indiceAleatorio = geradorDePalavras.Next(paises.Length);
                    palavraSecreta = paises[indiceAleatorio];
                }

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
                     cabecalhoJogo(qtdErros, totalTentativas, historicoChutesJoin, letrasEncontradas,categoria);


                     Console.Write("Digite uma letra: ");
                     string chuteUsuario = Console.ReadLine().ToUpper();


                     if (chuteUsuario.Length==1)
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
                                 letrasEncontradas[contador] = letraAtual;
                                 letraFoiEncontrada = true;
                             }
                         }

                         jogadorAcertou = new string(letrasEncontradas) == palavraSecreta;


                         if (letraFoiEncontrada == false && !jogadorAcertou)
                         {
                            qtdErros++;
                         }

                     }
                     else if(chuteUsuario.Length > 1)
                            {
                         if (chuteUsuario == palavraSecreta)
                         {
                            jogadorAcertou = true;
                          }
                          else
                          {
                              Console.WriteLine("Você errou a palavra inteira!");
                              qtdErros++;
                              Console.Write("Digite [Enter] para continuar:");
                          }
                     }


                     jogadorEnforcou = qtdErros >= totalTentativas;

                     if (jogadorAcertou)
                     {
                     Console.WriteLine("----------------------------------------------");
                     Console.WriteLine($"Conseguiu! A palavra secreta era {palavraSecreta}, parabéns!");
                     Console.WriteLine("----------------------------------------------");
                     }
                     else if (jogadorEnforcou)
                     {
                        qtdErros -= 1;
                        Console.Clear();
                        cabecalhoJogo(qtdErros, totalTentativas, historicoChutesJoin, letrasEncontradas, categoria);
                        Console.WriteLine("Você perdeu! A palavra era " + palavraSecreta);
                        Console.WriteLine("Tente Novamente!");
                        Console.ReadLine();

                     }

                }
                while (jogadorAcertou == false && jogadorEnforcou == false);

                Console.Write("Deseja continuar? (S/N): ");

                string opcaoContinuar = Console.ReadLine().ToUpper();
                if (opcaoContinuar.Length == 0) continue;


                if (opcaoContinuar != "S")
                    break;

            }
        }
    }
}