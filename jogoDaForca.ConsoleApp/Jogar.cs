namespace jogoDaForca.ConsoleApp
{
    public class Jogar
    {
        public static void PrepararRodada(Jogador jogador)
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
