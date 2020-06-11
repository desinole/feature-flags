# Feature Toggle steps

###Prep project
Create a .NET Framework MVC web application in C#

Name it: FeatureToggle

Select .NET Framework V4.7.2

Select MVC, uncheck HTTPs

Switch browser to edge

Add this to homecontroller.cs
```csharp
    public static int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }
```

Add this to Home/Index.cshtml below ViewBag
```html
    <p>&nbsp;</p>
    <p><input type="button" value="Print" onclick="useAItoFixWorldIssues()" /></p>
    <p>&nbsp;</p>
```

```javascript
    <script>
        function useAItoFixWorldIssues() {
            var x = @(FeatureToggle.Controllers.HomeController.RandomNumber(1,100));
            var y = @(FeatureToggle.Controllers.HomeController.RandomNumber(0,1));
            alert(x+' '+y);
            alert(x/y);
        }
    </script>
```

###Add code flag
So next thing I want to do it have the ability to turn it on and off.

So I add a flag in HomeController Index method and pass as ViewBag to the view

```csharp
    var codeFlag = true;
    ViewBag.Flag = codeFlag;
```
In the view, encompass above code in this "if" statement
```cshtml
    @if (ViewBag.Flag)
    {
    ...
    }
```

Set flag to true and run

Set flag to false and run

###Add web.config flag
Next we want to show how to set flags from config.

Add the following appsetting to web.config 
```xml
    <add key="ShowAIButton" value="true"/>
```

Add this to C# code
```csharp
    var plainFlag = bool.Parse(ConfigurationManager.AppSettings["ShowAIButton"]);
```

However, you can make a typo if this is in a thousand places. What happens when you type it in wrong or worse make a typo if two settings are closer

###Add simple toggle
Enter featuretoggle

Add featuretoggle nuget package by Jason Roberts

Add this to the home controller at the top under namespace.
```csharp
    public class AISimpleFeature : SimpleFeatureToggle { }
```

Add this to web.config
```xml
  <add key="FeatureToggle.AIFeature" value="true" />
```
Add singleton to homecontroller
```csharp
    readonly IFeatureToggle aiSimpleFlag = new AISimpleFeature();
```
Use this code for flag
```csharp
    bool aiSimpleFlag = aiSimpleFeatureFlag.FeatureEnabled;
```
Run in non-debug mode and change flag

###Add date toggle
Add this to the home controller at the top under namespace.
```csharp
    public class AIDateFeature : EnabledOnOrAfterDateFeatureToggle { }
```

Add this to web.config
```xml
  <add key="FeatureToggle.AIDateFeature" value="11-Jun-2020 00:00:00" />
```
Add singleton to homecontroller
```csharp
    readonly IFeatureToggle aiDateFeatureFlag = new AIDateFeature();
```
Use this code for flag
```csharp
    bool aiDateFlag = aiDateFeatureFlag.FeatureEnabled;
```

Change key value to couple of days past current date
13-Jun-2020 00:00:00

Show disabled

###Add custom toggle

Create a custom toggle class
```csharp
    public class CustomFeatureToggle : IFeatureToggle
    {
        public bool FeatureEnabled => true;
    }
```
Add this to the home controller at the top under namespace.
```csharp
    public class AICustomFeature : CustomFeatureToggle { }
```
Add singleton to homecontroller
```csharp
    readonly IFeatureToggle aiCustomFeatureFlag = new AICustomFeature();
```
Use this code for flag
```csharp
    bool aiCustomFlag = aiCustomFeatureFlag.FeatureEnabled;
```

