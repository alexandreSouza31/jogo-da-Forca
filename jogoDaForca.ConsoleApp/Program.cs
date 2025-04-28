using System;

namespace jogoDaForca.ConsoleApp
{
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

                Palavra.SortearPalavra(jogadores[0]);

                for (int i = 0; i < jogadores.Length; i++)
                {
                    Jogar.PrepararRodada(jogadores[i]);
                }

                Jogar.IniciarPartida(jogadores);

                string opcaoContinuar = Exibir.DesejaContinuar();
                if (opcaoContinuar != "S") break;
            }
        }
    }
}
