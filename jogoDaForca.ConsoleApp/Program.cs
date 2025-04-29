using System;

namespace jogoDaForca.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Jogador[] jogadores = Jogador.DefinirJogadores();

                Jogar.palavra.Sortear();

                for (int i = 0; i < jogadores.Length; i++)
                {
                    Jogar.PrepararPartida(jogadores[i]);
                }

                Jogar.IniciarPartida(jogadores);

                string opcaoContinuar = Exibir.DesejaContinuar();
                if (opcaoContinuar != "S") break;
            }
        }
    }
}
