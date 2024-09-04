using System;
using System.Windows;
using System.Windows.Threading;
using Eddyfi.Core;
using Eddyfi.Core.Logging.NLog;
using GTeck.DiceGame;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Hosting;

namespace DiceGame;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
  public App()
  {
    LocalConfigStorage.Items["Configuration.IsTouch"] = false;

    string[] args = Environment.GetCommandLineArgs();

    IHost host = Host.CreateDefaultBuilder( args )
                     .ConfigureServices( ( hostContext, services ) =>
                                         {
                                           services.ConfigureServices();
                                         } )
                     .UseNLog()
                     .Build();

    NlogUtil.Configure( "DiceGame", Version );

    _host = host;

    _logger = _host.Services.GetRequiredService<ILogger<App>>();

    AppDomain.CurrentDomain.UnhandledException        += Application_CurrentDomainUnhandledException;
    Application.Current.Dispatcher.UnhandledException += Application_DispatcherUnhandledException;

  }

  #region Event Handler

  private void Application_DispatcherUnhandledException( object sender, DispatcherUnhandledExceptionEventArgs e )
  {
    _logger.Error( "Dispatcher Unhandled Exception", e.Exception );
    e.Handled = true;
  }

  /// <summary>
  /// This event is raised when an Unhandled exception is raised.  When this occurs, the application quits.  We should not try to
  /// display any message box because the application is exiting and it won't work anyway.  We ensure we logged the error
  /// </summary>
  private void Application_CurrentDomainUnhandledException( object sender, UnhandledExceptionEventArgs e )
  {
    switch ( e.ExceptionObject )
    {
      case AggregateException ae:
        foreach ( Exception exception in ae.Flatten().InnerExceptions )
        {
          _logger.Error( "CurrentDomain Unhandled Exception", exception );
        }
        break;
      case Exception exception:
        _logger.Error( "CurrentDomain Unhandled Exception", exception );
        break;
    }
  }

  #endregion

  #region Base Class Overrides

  protected override void OnStartup( StartupEventArgs e )
  {
    try
    {
      _host.StartAsync().Wait();
      base.OnStartup( e );

      MainWindow = new MainWindow();
      MainWindowViewModel mainWindowDataContext = _host.Services.GetRequiredService<MainWindowViewModel>();
      mainWindowDataContext.DialogService = new DialogService( MainWindow );
      MainWindow.DataContext              = mainWindowDataContext;

      MainWindow.Show();
    }
    catch ( Exception ex )
    {
      _logger.LogException( $"Exception during {nameof(OnStartup)}", ex );
    }
  }

  protected override void OnExit( ExitEventArgs e )
  {
    try
    {
      _host.Services.GetRequiredService<ILocalConfigStorage>().ForceSerialize();

      _host.StopAsync().Wait();
      base.OnExit( e );
    }
    catch ( Exception ex )
    {
      _logger.LogException( $"Exception during {nameof(OnExit)}", ex );
    }
  }

  #endregion


  #region Private Variables

  private readonly IHost        _host;
  private readonly ILogger<App> _logger;

  private static readonly EddyfiVersion Version = new( major: 1,
                                                       minor: 0,
                                                       versionQualifier: EddyfiVersionQualifier.Beta,
                                                       revision: 1,
                                                       isPrivateBuild: true,
                                                       isMaintenanceBuild: false,
                                                       build: 1,
                                                       ciBuild: 1 );

  #endregion

}
