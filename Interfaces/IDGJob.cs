using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DieteticaPuchiApi.Interfaces
{
    public interface IDGJobRepository : IJob
    {
        Task Execute(IJobExecutionContext context);
    }
}
