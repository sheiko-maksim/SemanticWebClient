using GPRSService;
using Grpc.Core;

namespace GPRSService.Services
{
    public class CalculationService : Calculation.CalculationBase
    {
        private readonly ILogger<CalculationService> _logger;
        public CalculationService(ILogger<CalculationService> logger)
        {
            _logger = logger;
        }

        public override Task<CalcReply> CalculateTheFunction(CalcRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CalcReply
            {
                Y = request.X * 2 + 5 - 10
            });
        }
    }
}