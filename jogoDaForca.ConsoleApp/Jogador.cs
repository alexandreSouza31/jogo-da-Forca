namespace jogoDaForca.ConsoleApp
{
    public class Jogador
    {
        public string nome;
        public int qtdErros;
        public string[] chutesRealizados = new string[100];
        public int contadorChutes;
        public int totalTentativas;
        public string historicoChutesJoin;
        public bool jogadorAcertou;

        public static Jogador[] DefinirJogadores()
        {
            Exibir.NomeDoJogo();
            Console.Write("Quantos jogadores vão jogar? ");
            int numeroJogadores = int.Parse(Console.ReadLine()!);

            Jogador[] jogadores = new Jogador[numeroJogadores];

            for (int i = 0; i < numeroJogadores; i++)
            {
                jogadores[i] = new Jogador();
                Console.Write($"Digite o nome do jogador {i + 1}: ");
                jogadores[i].nome = Console.ReadLine()!;
            }

            return jogadores;
        }
    }
}
