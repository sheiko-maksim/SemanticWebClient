using API.Services;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MathController : ControllerBase
    {
        private readonly MathService mathService;
        private readonly IConfiguration configuration;

        public MathController(MathService mathService, IConfiguration configuration)
        {
            this.mathService = mathService;
            this.configuration = configuration;
        }

        [HttpPost("calculateMatrixMultiplication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<MatrixMultiplicationResponce> SignUp(
            [FromBody] MatrixMultiplicationRequest body)
        {
            var tempList1 = JsonConvert.DeserializeObject<List<List<int>>>(body.Matrix1);
            var tempList2 = JsonConvert.DeserializeObject<List<List<int>>>(body.Matrix2);


            int rows1 = tempList1.Count;
            int cols1 = tempList1[0].Count;

            int rows2 = tempList2.Count;
            int cols2 = tempList2[0].Count;


            double[,] matrix1 = new double[rows1, cols1];
            double[,] matrix2 = new double[rows2, cols2];


            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < cols1; j++)
                {
                    matrix1[i, j] = tempList1[i][j];
                }
            }

            for (int i = 0; i < rows2; i++)
            {
                for (int j = 0; j < cols2; j++)
                {
                    matrix2[i, j] = tempList2[i][j];
                }
            }

            var result = this.mathService.CalculateMatrixMultiplication(matrix1, matrix2);
            if (result.Matrix != null)
            {
                return this.Ok(result);
            }

            return this.BadRequest(result);
        }
    }
}
