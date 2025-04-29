namespace jogoDaForca.ConsoleApp
{
    public class Jogar
    {
        public static Palavra palavra = new Palavra();
        public static void PrepararPartida(Jogador jogador)
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

        public static void IniciarPartida(Jogador[] jogadores)
        {
            bool palavraEncontrada = false;
            bool todosEnforcados = false;
            int indiceJogadorAtual = 0;

            bool[] jogadoresEliminados = new bool[jogadores.Length];

            do
            {
                Jogador jogadorAtual = jogadores[indiceJogadorAtual];

                if (jogadoresEliminados[indiceJogadorAtual])
                {
                    indiceJogadorAtual = (indiceJogadorAtual + 1) % jogadores.Length;
                    continue;
                }

                jogadorAtual.historicoChutesJoin = string.Join(", ", jogadorAtual.chutesRealizados.Where(n => !string.IsNullOrEmpty(n)));
                Exibir.CabecalhoJogo(jogadorAtual);

                Console.Write($"Digite uma letra ou a palavra, {jogadorAtual.nome}: ");
                string chuteUsuario = Console.ReadLine().ToUpper();
                if (string.IsNullOrWhiteSpace(chuteUsuario)) continue;

                if (chuteUsuario.Length == 1)
                    palavra.TentarLetra(jogadorAtual, chuteUsuario);
                else
                    palavra.TentarPalavra(jogadorAtual, chuteUsuario);

                palavraEncontrada = jogadorAtual.jogadorAcertou;

                if (palavraEncontrada)
                {
                    Exibir.MensagemFinal(jogadorAtual, false, false);
                    break;
                }

                if (jogadorAtual.qtdErros >= jogadorAtual.totalTentativas)
                {
                    jogadoresEliminados[indiceJogadorAtual] = true;

                    Exibir.MensagemFinal(jogadorAtual, true, false);
                }

                todosEnforcados = jogadores.All(j => j.qtdErros >= j.totalTentativas);
                if (todosEnforcados)
                {
                    Console.Clear();
                    Exibir.CabecalhoJogo(jogadorAtual);
                    Exibir.MensagemFinal(jogadorAtual, true, true);
                    break;
                }

                indiceJogadorAtual = (indiceJogadorAtual + 1) % jogadores.Length;

            } while (!palavraEncontrada && !todosEnforcados);
        }
    }
}
