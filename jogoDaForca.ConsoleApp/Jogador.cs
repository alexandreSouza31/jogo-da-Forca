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
    }
}
