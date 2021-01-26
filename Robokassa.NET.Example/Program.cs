using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Robokassa.NET.Enums;
using Robokassa.NET.Models;

namespace Robokassa.NET.Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            //Создаем чек 
            var receipt = new RobokassaReceiptRequest(
                SnoType.Patent,
                new Collection<ReceiptOrderItem>()
                {
                    new ReceiptOrderItem("Аквадискотека", 1, 1_000, Tax.Vat110, PaymentMethod.FullPayment,
                        PaymentObject.Payment)
                }
            );


            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    //Достаем сервис робокассы из DI контейнера
                    var robokassaService = services.GetRequiredService<IRobokassaService>();

                    //Основной сценарий получения платежной ссылки
                    var paymentUrl = robokassaService
                        .GenerateAuthLink(receipt.TotalPrice, 123, receipt);

                    //Запускаем браузер сразу со ссылкой оплаты если ваша ОС в списке
                    if (new[]
                    {
                        PlatformID.Win32NT,
                        PlatformID.MacOSX
                    }.Any(x => x == Environment.OSVersion.Platform))
                        new Process
                        {
                            StartInfo = {UseShellExecute = true, FileName = paymentUrl.Link}
                        }.Start();
                    else
                        //Или выводим ссылку в консоль
                        Console.WriteLine(paymentUrl.Link);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogCritical(ex.Message);
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}