using System;
using System.Threading;

namespace CampoMinado

{
    class Program
    {
        static void ExibeMatriz(int[,] matriz, int n)
        {
            Console.Clear();
            Console.Write("\t\t");
            for (int i = 0; i < n; i++)
            {
                Console.Write(i + "\t");
            }
            Console.WriteLine();

            for (int i = 0; i < n; i++)
            {
                Console.Write(i + "\t-\t");
                for (int j = 0; j < n; j++)
                {
                    Console.Write(matriz[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
        static void initial(int[,] matriz, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matriz[i, j] = 0;
                }
            }
        }
        static int GerarBombas(int[,] matriz, int n)
        {
            Console.Clear();
            var rand = new Random();
            int i = 0, p1, p2;
            Console.Write("Entre a porcentagem das bombas: ");
            double porcent = Convert.ToDouble(Console.ReadLine()) / 100;

            int numBombas = (int)((n * n) * porcent);

            while (i < numBombas)
            {
                p1 = rand.Next(0, n);
                p2 = rand.Next(0, n);
                // Adiciona -2 nos locais que estaram as bombas.
                if (matriz[p1, p2] == 0)
                {
                    matriz[p1, p2] = -1;
                    i++;
                }
            }
            Console.Clear();
            return numBombas;
        }

        static int Jogo(int[,] mat, int n, int numBombas)
        {
            int score = 0, p1, p2, numError = 0;
            int[,] tela;
            tela = new int[n, n];
            initial(tela, n);

            while (numError != 3)
            {
                ExibeMatriz(tela, n);
                Console.WriteLine("Você possui mais {0} chances!", 3 - numError);
                Console.Write("Entre a primeira posicao: ");
                p1 = int.Parse(Console.ReadLine());

                Console.Write("Entre a segunda posicao: ");
                p2 = int.Parse(Console.ReadLine());

                if (mat[p1, p2] == 0)
                {
                    mat[p1, p2] = 3;
                    tela[p1, p2] = mat[p1, p2];
                    score += mat[p1, p2];

                }
                else if (mat[p1, p2] == -1)
                {
                    mat[p1, p2] = -2;
                    tela[p1, p2] = mat[p1, p2];
                    score += mat[p1, p2];
                    numError++;

                }
                else
                {
                    Console.WriteLine("Posição já selecionada");
                    Thread.Sleep(2000);
                }

                if ((n * n - numBombas) * 3 == score)
                {
                    ExibeMatriz(mat, n);
                    Console.WriteLine("Fim");
                    break;
                }
            }
            ExibeMatriz(mat, n);
            Console.WriteLine("Game Over.");
            return score;
        }
        static void Main(string[] args)
        {
            int[,] mat;

            char play = 'S';
            while (play == 'S')
            {
                Console.Clear();
                Console.Write("Entre com o tamanho de N: ");
                int n = int.Parse(Console.ReadLine());
                mat = new int[n, n];
                initial(mat, n);
                int numBombas = GerarBombas(mat, n);

                int score = Jogo(mat, n, numBombas);
                Console.WriteLine("Score: {0}", score);
                Console.Write("Jogar Novamente? <S> se sim: ");
                play = char.Parse(Console.ReadLine().ToUpper());//ToUpper= letra maiuscula.
            }

        }
    }
}