using System;
using System.IO;

using WPFApp.ViewModels;

namespace UnitTest.ViewModels;

using System.Linq;

using FluentAssertions;

using Logic.DTO;

using Xunit;

public class MatchViewModelTests
{
    [Fact]
    public void LoadTest()
    {
        string filename     = Path.GetTempFileName();
        var    matchManager = new Logic.MatchManager();
        var    match        = new Logic.DTO.Match();

        match.Team1  = new Team { Name = "Test" };
        match.Team2  = new Team { Name = "Test2" };
        match.Events = new Event[] { new Logic.DTO.Event() { Text = "Hallo", EventHalf = 1, EventTime = 1 } };
        matchManager.Save(match, filename);

        var mv = new MatchViewModel();
        mv.FileName = filename;

        mv.Load();

        mv.Match.TeamName1.Should().Be("Test");
        mv.Match.TeamName2.Should().Be("Test2");
        mv.Match.Events.Should().HaveCount(1);
        mv.Match.Events.First().EventText.Should().Be("Hallo");
        mv.Match.Events.First().EventTime.Should().Be("46'");

        //Test load again
        mv.Load();
        mv.Match.Events.Should().HaveCount(match.Events.Count());

        File.Delete(filename);
    }

    [Fact]
    public void SaveTest()
    {
        string filename = Path.GetTempFileName();

        var matchManager = new Logic.MatchManager();
        var match        = new Logic.DTO.Match();
        match.Team1  = new Team { Name = "Test" };
        match.Team2  = new Team { Name = "Team2" };
        match.Events = new Event[] { new Logic.DTO.Event() { Text = "Hallo", EventHalf = 1, EventTime = 46 } };
        matchManager.Save(match, filename);

        var mv = new MatchViewModel();
        mv.FileName = filename;

        mv.Load();

        mv.Match.NewEventText = "TOR";
        mv.Match.NewEventInfo = "Max Mustermann zum 1:0";
        mv.Match.NewEventTime = "89'";

        mv.Save();

        mv.Match.NewEventText.Should().BeNullOrEmpty();
        mv.Match.NewEventInfo.Should().BeNullOrEmpty();
        mv.Match.NewEventTime.Should().BeNullOrEmpty();

        // assert => read File

        var newMatch = matchManager.Load(filename);

        newMatch.Team1.Name.Should().Be("Test");
        newMatch.Events.Should().HaveCount(2);
        newMatch.Events.First().Text.Should().Be("Hallo");
        newMatch.Events.First().EventHalf.Should().Be(1);
        newMatch.Events.First().EventTime.Should().Be(46);
        newMatch.Events.Skip(1).First().Text.Should().Be("TOR");
        newMatch.Events.Skip(1).First().Information.Should().Be("Max Mustermann zum 1:0");
        newMatch.Events.Skip(1).First().EventHalf.Should().Be(1);
        newMatch.Events.Skip(1).First().EventTime.Should().Be(44);

        File.Delete(filename);
    }

    [Fact]
    public void CanLoadTest()
    {
        string filename     = Path.GetTempFileName();
        var    matchManager = new Logic.MatchManager();
        var    match        = new Logic.DTO.Match();

        match.Team1  = new Team { Name = "Test" };
        match.Team2  = new Team { Name = "Team2" };
        match.Events = new Event[] { new Logic.DTO.Event() { Text = "Hallo", EventHalf = 1, EventTime = 48 } };

        File.Delete(filename);

        var mv = new MatchViewModel();
        mv.FileName = filename;

        mv.LoadCommand.CanExecute(null).Should().BeFalse();

        matchManager.Save(match, filename);

        mv.LoadCommand.CanExecute(null).Should().BeTrue();

        File.Delete(filename);
    }

    [Fact]
    public void CanSaveTest()
    {
        string filename     = Path.GetTempFileName();
        var    matchManager = new Logic.MatchManager();
        var    match        = new Logic.DTO.Match();

        match.Team1  = new Team { Name = "Test" };
        match.Team2  = new Team { Name = "Team2" };
        match.Events = new Event[] { new Logic.DTO.Event() { Text = "Hallo", EventHalf = 1, EventTime = 47 } };

        matchManager.Save(match, filename);

        var mv = new MatchViewModel();
        mv.FileName = filename;

        mv.Match.NewEventInfo = "NewWord";
        mv.Match.NewEventText = "NewWord";
        mv.Match.NewEventTime = "45";
        mv.Match.TeamName1    = "Team1";
        mv.Match.TeamName2    = "Team2";

        mv.SaveCommand.CanExecute(null).Should().BeTrue();

        mv.Match.TeamName2 = "";
        mv.SaveCommand.CanExecute(null).Should().BeFalse();
        mv.Match.TeamName2 = "x";


        mv.Match.TeamName1 = "";
        mv.SaveCommand.CanExecute(null).Should().BeFalse();
        mv.Match.TeamName1 = "1";


        mv.Match.NewEventTime = "";
        mv.SaveCommand.CanExecute(null).Should().BeFalse();
        mv.Match.NewEventTime = "1";


        mv.Match.NewEventText = "";
        mv.SaveCommand.CanExecute(null).Should().BeFalse();
        mv.Match.NewEventText = "1";

        mv.Match.NewEventInfo = "";
        mv.SaveCommand.CanExecute(null).Should().BeFalse();
        mv.Match.NewEventInfo = "x";

        mv.SaveCommand.CanExecute(null).Should().BeTrue();

        File.Delete(filename);
    }
}