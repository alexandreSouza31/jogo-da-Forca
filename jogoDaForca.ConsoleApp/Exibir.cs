namespace jogoDaForca.ConsoleApp
{
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

        public static void BonecoForca(Jogador jogador)
        {
            string cabecaDoBoneco = jogador.qtdErros >= 1 ? " O " : " ";
            string tronco = jogador.qtdErros >= 2 ? "x" : " ";
            string troncoBaixo = jogador.qtdErros >= 2 ? " x " : " ";
            string bracoEsquerdo = jogador.qtdErros >= 3 ? "/" : " ";
            string bracoDireito = jogador.qtdErros >= 4 ? @"\" : " ";
            string pernas = jogador.qtdErros >= jogador.totalTentativas ? "/ \\" : " ";

            Console.WriteLine(" ___________        ");
            Console.WriteLine(" |/        |        ");
            Console.WriteLine(" |        {0}       ", cabecaDoBoneco);
            Console.WriteLine(" |        {0}{1}{2} ", bracoEsquerdo, tronco, bracoDireito);
            Console.WriteLine(" |        {0}       ", troncoBaixo);
            Console.WriteLine(" |        {0}       ", pernas);
            Console.WriteLine(" |                  ");
            Console.WriteLine(" |                  ");
        }
        public static void CabecalhoJogo(Jogador jogador)
        {
            Console.Clear();
            NomeDoJogo();
            Console.WriteLine($"Jogador:{jogador.nome}");
            BonecoForca(jogador);
            Console.WriteLine($" |{String.Join(' ', letrasEncontradas)} | {letrasEncontradas.Length} letras");
            Console.WriteLine($"\nErros do jogador: {jogador.qtdErros} de {jogador.totalTentativas}");
            Console.WriteLine("\nHistórico de tentativas: " + jogador.historicoChutesJoin);
            Console.WriteLine($"Categoria: {categoria}");
            Console.WriteLine("----------------------------------------------");
        }

        public static string DesejaContinuar()
        {
            string opcaoContinuar = "";

            do
            {
                Console.Write("Deseja continuar? (S/N): ");
                opcaoContinuar = Console.ReadLine()!.Trim().ToUpper();
            } while (opcaoContinuar != "S" && opcaoContinuar != "N");

            return opcaoContinuar;
        }

        public static void MensagemFinal(Jogador jogador, bool jogadorEnforcou, bool todosEnforcados)
        {
            if (jogador.jogadorAcertou)
            {
                CabecalhoJogo(jogador);
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine($"Conseguiu, {jogador.nome}! A palavra secreta era {Palavra.palavraSecreta}, parabéns!");
                Console.WriteLine("----------------------------------------------");
                Console.Write("Digite [Enter] para continuar:");
                Console.ReadLine();
            }
            else if (todosEnforcados == true)
            {
                Console.WriteLine($"Fim de jogo! A palavra era {Palavra.palavraSecreta}");
                Console.WriteLine("Tente Novamente!");
                Console.Write("Digite [Enter] para continuar:");
                Console.ReadLine();
            }
            else if (jogadorEnforcou)
            {
                Console.WriteLine($"Você perdeu, {jogador.nome}!");
                Console.Write("Digite [Enter] para continuar:");
                Console.ReadLine();
            }
        }
    }
}
