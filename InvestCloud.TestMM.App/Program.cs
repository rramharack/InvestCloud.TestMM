using InvestCloud.TestMM.Application.Common;
using InvestCloud.TestMM.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InvestCloud.TestMM.App;

internal sealed class Program
{
    private static Task Main(string[] args)
    {
        try
        {
            // Dependency Injection
            var services = StartUp.CreateServices();

            // Check if created successfully...
            var multiplyOperation = services.GetService<IMultiply2D>() ?? throw new ArgumentNullException(nameof(IMultiply2D));

            // Do the actual work of the Mathematical Operation 
            Console.WriteLine(multiplyOperation.GetComputation().Result);
            Console.WriteLine();
        }
        catch (Exception e)
        {
            Console.WriteLine(@"ERROR: " + e.GetFullMessage());
        }

        return Task.CompletedTask;
    }
}