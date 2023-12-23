using System.Diagnostics;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

var botClient = new TelegramBotClient("6138919214:AAHl5qmPi2HvdrUqTEsHVNaZuHPS2e6qXHI");

using CancellationTokenSource cts = new();

ReceiverOptions receiverOptions = new()
{
    AllowedUpdates = Array.Empty<UpdateType>() 
};

botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    if (update.Message is not { } message)
        return;
 
    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");
 
    TimeSpan start = new TimeSpan(18, 06, 0);
    TimeSpan timeSpan = new TimeSpan(22, 0, 0);
    TimeSpan mows = new TimeSpan(0, 0, 0);

//    var messagec = update.Message;
    if (message.Text.ToLower().Contains("/start")) {
        await botClient.SendTextMessageAsync(message.Chat.Id, "Привіт я бот що повідомляє про планове вимкнення світла за 10хв до вимкниння (в Івано Франківскі області для очереді 3.3  3.4)");
    }

    while (true) {
        DateTime nw = DateTime.Now;
        DateTime nt1 = new DateTime(2023, 2, 17, 21, 23, 00);
        DateTime nt2 = new DateTime(2023, 2, 17, 21, 24, 00);
        DateTime nt3 = new DateTime(2023, 2, 17, 20, 30, 00);
        DateTime nt4 = new DateTime(2023, 2, 17, 20, 32, 00);
        DateTime nt5 = new DateTime(2023, 2, 17, 20, 24, 00);

        if (nw == nt1)
        {
            botClient.SendTextMessageAsync(message.Chat.Id, "1‼️Через 10хв імовірно вимкнуть світло‼️");
        } 

        if (nw == nt2)
        {
            botClient.SendTextMessageAsync(message.Chat.Id, "2‼️Через 10хв імовірно вимкнуть світло‼️");
        }

    }
  }

Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}