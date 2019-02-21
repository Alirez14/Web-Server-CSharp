## Setup tests in Visual Studio

Test has to be executed in the `deploy` folder. To configure the Visual Studio Test Runner, add a `.runsettings` file (e.g. `my.runsettings`) to your project (not to the unit test project!):

    <?xml version="1.0" encoding="utf-8" ?>
    <RunSettings>
      <TestRunParameters>
        <Parameter name="targetpath" value="path\to\your\deploy\folder" />
      </TestRunParameters>
    </RunSettings>

In Visual Studio, select this file as "Test Settings".