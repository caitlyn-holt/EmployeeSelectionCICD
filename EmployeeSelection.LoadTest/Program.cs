using System;
using System.Diagnostics;
using System.Threading.Tasks;
using EmployeeSelection.Core.Services;

#pragma warning disable IDE0040 // Добавить модификаторы доступа
Console.WriteLine("==========================================");
Console.WriteLine("   СИСТЕМНОЕ НАГРУЗОЧНОЕ ТЕСТИРОВАНИЕ");
Console.WriteLine("   EmployeeSelection System");
Console.WriteLine("==========================================");
Console.WriteLine($"Дата и время: {DateTime.Now}");
Console.WriteLine();
int userCount = 1000;
Console.WriteLine($"[КОНФИГУРАЦИЯ]");
Console.WriteLine($"  Количество пользователей: {userCount}");
Console.WriteLine($"  Операции на пользователя: Регистрация + Вход");
Console.WriteLine();
Console.WriteLine($"[ТЕСТ] Запуск имитации {userCount} пользователей...");
Console.WriteLine();

var stopwatch = Stopwatch.StartNew();
int successCount = 0;
int errorCount = 0;
Parallel.For(0, userCount, i =>
{
    try
    {
        var auth = new AuthenticationService();
        auth.RegisterUser($"load_user_{i}", "password123", "Candidate");
        auth.Login($"load_user_{i}", "password123");
        System.Threading.Interlocked.Increment(ref successCount);
    }
    catch (Exception ex)
    {
        System.Threading.Interlocked.Increment(ref errorCount);
        Console.WriteLine($"[ОШИБКА] Поток {i}: {ex.Message}");
    }
});

stopwatch.Stop();

Console.WriteLine();
Console.WriteLine("==========================================");
Console.WriteLine("   РЕЗУЛЬТАТЫ НАГРУЗОЧНОГО ТЕСТИРОВАНИЯ");
Console.WriteLine("==========================================");
Console.WriteLine($"Всего запросов:        {userCount}");
Console.WriteLine($"Успешно выполнено:     {successCount}");
Console.WriteLine($"Ошибок:                {errorCount}");
Console.WriteLine($"Общее время:           {stopwatch.ElapsedMilliseconds} мс");
Console.WriteLine($"Среднее время/запрос:  {(double)stopwatch.ElapsedMilliseconds / userCount:F3} мс");
Console.WriteLine($"Производительность:    {(double)userCount / (stopwatch.ElapsedMilliseconds / 1000.0):F2} оп/сек");
Console.WriteLine($"Процент успеха:        {(double)successCount / userCount * 100:F2}%");
Console.WriteLine("==========================================");
Console.WriteLine();
Console.WriteLine("[INFO] Тестирование завершено!");
Console.WriteLine("Нажмите любую клавишу для выхода...");
Console.ReadKey();
