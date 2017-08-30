# nlog-exception-dialog
Exception dialog box for WinForms and WPF as Target ro NLOG (http://nlog-project.org/)

Writes log messages to prepared dialog box. 

Supported in .NET and Mono
## Configuration Syntax
```xml
<targets>
  <target xsi:type="MessageBox"
          name="String"
          caption="Layout"
          layout="Layout">
  </target>
  <target xsi:type="ComplexBox"
          name="String"
          caption="String"
          
          layout="Layout"
          >
  </target>
</targets>
```
## Parameters
### General Options
* **name** - Name of the target.

### Layout Options
* **layout** - Text to be rendered. [Layout](Layout) Required. Default: `${message}`
* **caption** - Header. [String](String) (optional). Default: `${processname}`
* **icon** - Icon name (optional). Possible values (case insenitive): `question`,`info`,`warning`,`error`,`no` [String](String) 

### Programmatic example
You can also configure the target programmatically:
```csharp
            var target = new ComplexBoxTarget();
            target.Caption = "program name";
            target.Icon = "error";
            target.Layout = "${event-properties:item=MyValue} ${message}";
            NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Debug);
```
