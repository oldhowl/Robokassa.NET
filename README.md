# Robokassa.NET
Реализация платежей робокассы с робочеками (фискализацией) на платформе .NET Core 3.1

Пример использования в проекте Robokassa.NET.Example

Реализовано:

1. Сбор ордера на оплату
2. Возврат платежной ссылки
3. Callback на результат оплаты
4. Фискальные чеки

Для начала работы тестового проекта необходимо

Заполнить appsettings.Development.json (название магазина в системе, пароль1, пароль2) из настроек вашего магазина https://partner.robokassa.ru/Shops

Настроить ResultURL в технических настройках магазина http://example.com/paymentResult (ендпоинт по умолчанию /paymentResult) с методом **POST**


Для временного домена для тестов можно воспользоваться сервисом ngrok

Установите [ngrok](https://ngrok.com/download)

Вбейте в консоль


    ./ngrok http 5000 

![enter image description here](https://i.ibb.co/bgR2SZc/image.png)


Укажите ResultURL  в настройках магазина робокассы

![enter image description here](https://i.ibb.co/YBtbDXs/image.png)


После успешной оплаты робокасса отправит POST запрос на этот колбек.
