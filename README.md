# nlog-exception-dialog
Exception dialog box for WinForms and WPF as Target ro NLOG (http://nlog-project.org/)

Writes log messages to the console with customizable coloring. 

Supported in .NET and Mono
## Configuration Syntax
```xml
<targets>
  <target xsi:type="MessageBox"
          name="String"
          caption="Layout"
          layout="Layout">
  </target>
  <target xsi:type="ExceptionBox"
          name="String"
          caption="Layout"
          layout="Layout"
          >
  </target>
</targets>
```
Read more about using the [[Configuration File]].
## Parameters
### General Options
* **name** - Name of the target.

### Layout Options
* **layout** - Text to be rendered. [Layout](Layout) Required. Default: `${message}`
* **caption** - Header. [Layout](Layout)  

### Programmatic example
You can also configure the target programmatically:
```csharp
            var target = new ExceptionBoxTarget();
            target.Caption = "program name";
            target.Layout = "${event-properties:item=MyValue} ${message}";
            NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Debug);
```
