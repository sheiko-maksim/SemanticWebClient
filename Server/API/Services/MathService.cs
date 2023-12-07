using API.ViewModels;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace API.Services
{
    public record class Worker(int max, double[,] result, int currentRow, double[,] matrix1, double[,] matrix2)
    {
        public void Run()
        {
            for (int j = 0; j < max; j++)
            {
                for (int k = 0; k < matrix2.GetLength(0); k++)
                {
                    result[currentRow, j] += matrix1[currentRow, k] * matrix2[k, j];
                }
            }
        }
    }
    public class MathService
    {
        private readonly IConfiguration config;
        private readonly ILogger logger;

        public MathService(
            IConfiguration configuration,
            ILogger<AuthService> logger)
        {
            this.config = configuration;
            this.logger = logger;
        }


        public MatrixMultiplicationResponce CalculateMatrixMultiplication(double[,] matrix1, double[,] matrix2)
        {

            int MAX_ROW = matrix1.GetLength(0);
            int MAX_COLUMN = matrix2.GetLength(1);
            double[,] result = new double[MAX_ROW, MAX_COLUMN];
            int currentRow = 0;

            Thread[] threads = new Thread[MAX_ROW];

            for (int i = 0; i < MAX_ROW; i++)
            {
                threads[i] = new Thread(new Worker(MAX_COLUMN, result, currentRow++, matrix1, matrix2).Run);
                threads[i].Start();
            }

            for (int i = 0; i < MAX_ROW; i++)
            {
                threads[i].Join();
            }

            var response = new MatrixMultiplicationResponce();
            response.Matrix = ArrayToString(result);
            return response;
        }


        static string ArrayToString(double[,] array)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append('[');
            for (int i = 0; i < array.GetLength(0); i++)
            {
                sb.Append('[');
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    sb.Append(array[i, j]);
                    if (j < array.GetLength(1) - 1)
                    {
                        sb.Append(", ");
                    }
                }
                sb.Append(']');
                if (i < array.GetLength(0) - 1)
                {
                    sb.Append(", ");
                }
            }
            sb.Append(']');

            return sb.ToString();
        }
    }
}
