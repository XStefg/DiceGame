using System.CommandLine;
using System.CommandLine.Parsing;
using Microsoft.Extensions.Options;

namespace DiceGame;

public static class CommandLineArgumentExtension
{
  public static void ConfigureCommandLineArgument( this OptionsBuilder<CommandLineArgument> builder, string[] args )
  {
    Option<bool?> optionDebug               = new( new[] { "--debug", "-debug" }, "Running in debug" );
    Option<bool?> optionFakeExternalCommand = new( new[] { "--fakeExternalCommand", "-fakeExternalCommand" , "-fakeexternalcommand"}, "Fake external command when installing image" );
    RootCommand   rootCommand               = new() { optionDebug, optionFakeExternalCommand };

    ParseResult result = rootCommand.Parse( args );

    bool? debug               = result.GetValueForOption( optionDebug );
    bool? fakeExternalCommand = result.GetValueForOption( optionFakeExternalCommand );

    builder.Configure( options =>
                       {
                         options.IsDebug             = debug               ?? false;
                         options.FakeExternalCommand = fakeExternalCommand ?? false;
                       } );
  }
}