namespace jogoDaForca.ConsoleApp
{
    public class Palavra
    {
        public static string palavraSecreta;

        public char EscolherCategoria()
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

        public void Sortear()
        {
            char metodoCategoriaPalavra = EscolherCategoria();

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

        public  void TentarLetra(Jogador jogador, string chuteUsuario)
        {
            char chute = chuteUsuario[0];

            if (Array.Exists(jogador.chutesRealizados, n => n == chute.ToString()) && !Palavra.palavraSecreta.Contains(chute))
            {
                Console.WriteLine($"\n{chute} já foi digitado, e é diferente do sorteado!", "\n");
                Console.Write("Digite [Enter] para continuar:");
                Console.ReadLine();
                return;
            }
            else if (Array.Exists(jogador.chutesRealizados, n => n == chute.ToString()) && Palavra.palavraSecreta.Contains(chute))
            {
                Console.WriteLine($"\n{chute} já foi digitado, e está na palavra!Digite outra letra!", "\n");
                Console.Write("Digite [Enter] para continuar:");
                Console.ReadLine();
            }

            jogador.chutesRealizados[jogador.contadorChutes++] = chute.ToString();

            bool letraFoiEncontrada = false;

            for (int contador = 0; contador < Palavra.palavraSecreta.Length; contador++)
            {
                char letraAtual = Palavra.palavraSecreta[contador];

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

        public void TentarPalavra(Jogador jogador, string chuteUsuario)
        {
            if (chuteUsuario.Replace(" ", "") == Palavra.palavraSecreta.Replace(" ", "")) jogador.jogadorAcertou = true;
            else
            {
                Console.WriteLine($"Você errou a palavra inteira, {jogador.nome}!");
                jogador.qtdErros++;
                Console.Write("Digite [Enter] para continuar:");
                Console.ReadLine();
            }
        }
    }
}
