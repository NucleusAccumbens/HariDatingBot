using EblanistsDatingBot.Commands.AdminCommands.CallbackAdminCommands;
using EblanistsDatingBot.Commands.GeneralCommands.CallbackGeneralCommands;
using EblanistsDatingBot.Commands.GeneralCommands.TextGeneralCommands;
using EblanistsDatingBot.Commands.UserCommands;
using EblanistsDatingBot.Commands.UserCommands.CallbackUserCommands;
using EblanistsDatingBot.Commands.UserCommands.TextUserCommands;
using EblanistsDatingBot.Common;
using EblanistsDatingBot.Common.Abstractions;
using EblanistsDatingBot.Common.Interfaces;
using EblanistsDatingBot.Common.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureService
{
    public static IServiceCollection AddTelegramBotServices(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<TelegramBot>();
        services.AddSingleton<IMemoryCachService, MemoryCachService>();
        services.AddScoped<ICommandAnalyzer, CommandAnalyzer>();
        services.AddScoped<BaseTextCommand, StartTextCommand>();
        services.AddScoped<BaseTextCommand, AddAboutTextCommand>();
        services.AddScoped<BaseTextCommand, PhotoTextCommand>();
        services.AddScoped<BaseTextCommand, ProfileTextCommand>();
        services.AddScoped<BaseTextCommand, ViewPhotosTextCommand>();
        services.AddScoped<BaseTextCommand, DatingTextCommand>();
        services.AddScoped<BaseTextCommand, RequestsTextCommand>();
        services.AddScoped<BaseTextCommand, ChatingTextCommand>();
        services.AddScoped<BaseCallbackCommand, RegisterCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, VeganCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, NoncredistCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, ChildfreeCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, CosmopolitanCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, BdsmCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, MakeupCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, HeelsCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, TatoosCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, ExistenceCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, ShaveLegsCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, ShaveHeadCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, SacredCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, ProfileCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, StartCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, EditProfileCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, AgeCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, EditProfileParamsCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, EditAnswersCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, PhotoCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, VerifidePhotoCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, ViewPhotosCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, ManagePhotoCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, DatingCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, StartDatingCallbackCommandcs>();
        services.AddScoped<BaseCallbackCommand, PhotoForDatingCallbackCommand>();
        services.AddScoped<BaseCallbackCommand, RequestAChatCallbackCommand>();

        return services;
    }
}
