using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
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

            //Опциональные пользовательские параметры shp_ https://docs.robokassa.ru/#1205
            var customFields = new CustomShpParameters();

            //Можно указать поля с префиксом Shp_
            customFields.Add("Shp_login", "Vasya");
            
            //Можно без него, метод Add поправит ключ
            customFields.Add("oplata", "1");
            customFields.Add("email", "test@example.com");
            customFields.Add("name", "Вася");


            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    //Достаем сервис робокассы из DI контейнера
                    var robokassaService = services.GetRequiredService<IRobokassaService>();

                    //Основной сценарий получения платежной ссылки
                    var paymentUrl = robokassaService
                        .GenerateAuthLink(receipt.TotalPrice, 123, receipt, customFields);

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