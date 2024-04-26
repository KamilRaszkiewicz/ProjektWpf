using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsGenerator.Domain.Models.Tests;

namespace TestsGenerator.App.Interfaces
{
    public interface IPdfService
    {
        Task<byte[]?> GeneratePdfAsync(Test test, CancellationToken? ct = null);
    }
}
