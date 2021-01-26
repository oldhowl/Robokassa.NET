# Robokassa.NET
Реализация платежей робокассы с робочеками (фискализацией) на платформе .NET Core 3.1

Пример использования в проекте Robokassa.NET.Example

Алгоритм оплаты с фискализацией:
- собираем заказ 
- генерируем платежную ссылку
- юзер переходит по ссылке
- юзер оплачивает заказ
- при успешной оплате юзера возвращает на success-page
- параллельно робокасса отправляет запрос на сервер
- сервер валидирует запрос
- заказ 


Реализовано:

1. [Сбор ордера на оплату](https://github.com/oldhowl/Robokassa.NET/blob/57d98c8a4c8e94f29841bb5fb607206d3e06c0c4/Robokassa.NET/IRobokassaService.cs#L7)
2. [Возврат платежной ссылки](https://github.com/oldhowl/Robokassa.NET/blob/57d98c8a4c8e94f29841bb5fb607206d3e06c0c4/Robokassa.NET/Models/PaymentUrl.cs#L3)
3. [Callback на результат оплаты (в демо проекте)](https://github.com/oldhowl/Robokassa.NET/blob/57d98c8a4c8e94f29841bb5fb607206d3e06c0c4/Robokassa.NET.Example/Controllers/RobokassaTestController.cs#L13)
4. [Фискальные чеки](https://github.com/oldhowl/Robokassa.NET/blob/57d98c8a4c8e94f29841bb5fb607206d3e06c0c4/Robokassa.NET/Models/RobokassaReceiptRequest.cs#L18)

## Для начала работы тестового проекта необходимо

Заполнить appsettings.Development.json (название магазина в системе, пароль1, пароль2) из настроек вашего магазина https://partner.robokassa.ru/Shops

Настроить ResultURL в технических настройках магазина http://example.com/paymentResult 
(ендпоинт по умолчанию [/paymentResult](https://github.com/oldhowl/Robokassa.NET/blob/57d98c8a4c8e94f29841bb5fb607206d3e06c0c4/Robokassa.NET.Example/Controllers/RobokassaTestController.cs#L10)) с методом **POST**


### Для временного домена для тестов можно воспользоваться сервисом ngrok

Установите [ngrok](https://ngrok.com/download)

Вбейте в консоль


    ./ngrok http 5000 

![enter image description here](https://i.ibb.co/bgR2SZc/image.png)


Укажите **ResultURL**  в настройках магазина робокассы

![enter image description here](https://i.ibb.co/YBtbDXs/image.png)


### После успешной оплаты робокасса отправит POST запрос на этот колбек.


# Полный скрин настроек технического раздела магазина

ShopName, Password1 и Password2 редактируются в [appsettings.Development.json](https://github.com/oldhowl/Robokassa.NET/blob/14290e3ddad2454d7648111ea3803f654b46c1e3/Robokassa.NET.Example/appsettings.Development.json#L2)

Алгоритм рачета хеша MD5

ResultURL относится к контроллеру RobokassaTestController. Метод **POST**

Success Url и Fail Url относятся к контроллеру ResultPaymentViewController

![Полный скрин настроек](https://i.ibb.co/Jsw55dW/2021-01-26-17-39-43.png)
