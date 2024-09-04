using System;
using Eddyfi.Core;
using GTeck.DiceGame;
using Microsoft.Extensions.DependencyInjection;

namespace DiceGame;

public static class ServicesExtension
{
  public static void ConfigureServices( this IServiceCollection services )
  {
//    services.AddTransient<IWimgAccess, WimgAccess>();

//    services.AddHostedService<MonitoredApplicationService>();

    //services.AddSingleton<ISystemService, SystemService>();
    services.AddSingleton<ILocalConfigStorage>( e => LocalConfigStorage.Instance );
    services.AddSingleton<MainWindowViewModel>();
    //services.AddOptions<EddyShellConfiguration>()
    //        .Configure<IConfiguration>( ( settings, configuration ) =>
    //                                    {
    //                                      configuration.Bind( settings );
    //                                    } );
    services.AddOptions<CommandLineArgument>()
            .ConfigureCommandLineArgument( Environment.GetCommandLineArgs() );
  }
}