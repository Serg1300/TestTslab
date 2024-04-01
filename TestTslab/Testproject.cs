using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Helpers;
using TSLab.Script.Optimization;

namespace TestTslab
{
    public class Testproject : IExternalScript
    {
        #region HttpClient и Uri  все варианты дают ошибку для сборки скрипта
        public HttpClient HttpClient { get; set; }

        private static readonly HttpClient httpClient = new HttpClient();

        private static readonly HttpClient http;
        static Testproject()
        {
            http = new HttpClient();
        }

        public Testproject()
        {
            HttpClient = new HttpClient();
        }

        public Uri uri = new Uri("https://dzen.ru");
        #endregion

        public OptimProperty HighPeriod = new OptimProperty(20, 10, 100, 5);
        public OptimProperty LowPeriod = new OptimProperty(20, 10, 100, 5);
        public void Execute(IContext ctx, ISecurity sec)
        {
            var high = ctx.GetData("Highest", new[] { HighPeriod.ToString() },
                () => Series.Highest(sec.GetHighPrices(ctx), HighPeriod));
            var low = ctx.GetData("Lowest", new[] { LowPeriod.ToString() },
                () => Series.Lowest(sec.GetLowPrices(ctx), LowPeriod));

            for (int i = 0; i < ctx.BarsCount; i++)
            {

            }
            if (ctx.IsOptimization)
            {
                return;
            }

            // Отрисовка графиков
            ctx.First.AddList(string.Format("High({0})", HighPeriod), high, ListStyles.LINE, ScriptColors.Green,
                LineStyles.SOLID, PaneSides.RIGHT);
            ctx.First.AddList(string.Format("Low({0})", LowPeriod), low, ListStyles.LINE, ScriptColors.Red,
                LineStyles.SOLID, PaneSides.RIGHT);
        }
    }
}
