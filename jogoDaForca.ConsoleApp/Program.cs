namespace jogoDaForca.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true) {
                string palavraSecreta = "MELANCIA";


                char[] letrasEncontradas = new char[palavraSecreta.Length];

                for (int caractere = 0; caractere < letrasEncontradas.Length; caractere++)
                {
                    letrasEncontradas[caractere] = '_';

                }

                bool jogadorEnforcou = false;
                bool jogadorAcertou = false;


                do
                {
                    string dicaPalavra = String.Join(" ", letrasEncontradas);


                    Console.Clear();
                    Console.WriteLine("----------------------------------------------");
                    Console.WriteLine("              Jogo da Forca");
                    Console.WriteLine("----------------------------------------------");
                    Console.WriteLine("----------------------------------------------");
                    Console.WriteLine("Palavra escolhida: " + String.Join("", letrasEncontradas));
                    Console.Write("Digite uma letra: ");
                    char chute = Console.ReadLine().ToUpper()[0];
                    Console.WriteLine(chute);


                    bool letraFoiEncontrada=false;

                    for(int contador = 0; contador < palavraSecreta.Length; contador++)
                    {
                        char letraAtual = palavraSecreta[contador];

                        if (chute == letraAtual)
                        {
                            letrasEncontradas[contador] = letraAtual;
                            letraFoiEncontrada = true;
                        }
                    }


                    dicaPalavra= String.Join(" ", letrasEncontradas);

                }
                while (jogadorAcertou==false && jogadorEnforcou==false);

            }
        }
    }
}