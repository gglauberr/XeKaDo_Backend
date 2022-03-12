using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XeKaDo.Api
{
    public partial class Startup
    {
        private static readonly TimeZoneInfo TimeZone =
            TimeZoneInfo.CreateCustomTimeZone("BRT", TimeSpan.FromHours(-3), "(UTC-03:00) Brasília", "Hora oficial do Brasil");

        private static void RegisterRecurringJobs()
        {
            //RecurringJob.AddOrUpdate<IOrcamentoJobs>((o)
            //    => o.CancelaOrcamentosVencidos(null, null),
            //    Cron.Daily(07, 00),
            //    TimeZone
            //    );
        }
    }
}
