using System;
using System.Text;
using System.Windows;
using NLog.Targets;
using NLog.Common;
using NLog.Layouts;
using NLog.Visar.WPF.DlgException;

namespace NLog.Visar.WinForms.Targets
{

    /// <summary>
    /// Реализовано по мотивам MessageBoxTarget из NLog.Windows.Forms
    /// </summary>
    /// <seealso cref="NLog.Targets.TargetWithLayout" />
    [Target("ComplexBox")]
    public sealed class ComplexBoxTarget : TargetWithLayout
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComplexBoxTarget"/> class.
        /// </summary>
        /// <remarks>The default value of the layout is: <code>${longdate}|${level:uppercase=true}|${logger}|${message}</code></remarks>
        public ComplexBoxTarget()
        {
            Caption = "${processname}";
            Icon = null;
        }
        //[RequiredParameter]
        /// <summary>
        /// Установить Заголовок MessageBox по умолчанию имя процесса.
        /// </summary>
        /// <value>The caption.</value>
        public Layout Caption { get; set; }

        //[RequiredParameter]
        /// <summary>
        /// Установить Иконку MessageBox по умолчанию имя процесса.
        /// </summary>
        /// <value>The Icon.</value>
        public string Icon { get; set; }

        /// <summary>
        /// Writes the specified log event.
        /// </summary>
        /// <param name="logEvent">The log event.</param>
        protected override void Write(LogEventInfo logEvent)
        {
            try
            {
                ShowMessage(logEvent);
            }
            catch (Exception ex)
            {
                InternalLogger.Warn(ex.ToString());
                if (LogManager.ThrowExceptions)
                {
                    throw;
                }
            }
        }

        protected override void Write(AsyncLogEventInfo[] logEvents)
        {
            if (logEvents.Length == 0)
            {
                return;
            }

            var sb = new StringBuilder();
            var lastLogEvent = logEvents[logEvents.Length - 1];
            foreach (var ev in logEvents)
            {
                sb.Append(this.Layout.Render(ev.LogEvent));
                sb.Append("\n");
            }

            ShowMessage(lastLogEvent.LogEvent, sb.ToString());

            for (int i = 0; i < logEvents.Length; ++i)
            {
                logEvents[i].Continuation(null);
            }
        }
        private void ShowMessage(LogEventInfo logEvent, string message = null)
        {
            string caption = null;
            MessageBoxImage? messageBoxIcon = Icon == null ? (MessageBoxImage?)null : ToMessageBoxImage(Icon);
            if (logEvent.Properties != null && logEvent.Properties.Count > 0)
            {
                if (logEvent.Properties.ContainsKey("Caption")) caption = logEvent.Properties["Caption"].ToString();
                if (logEvent.Properties.ContainsKey("MessageBoxIcon")) messageBoxIcon = ToMessageBoxImage(logEvent.Properties["MessageBoxIcon"].ToString());
            }

            if (logEvent.Exception!=null)
                dlgException2.Show(logEvent.Exception, message ?? this.Layout.Render(logEvent), caption ?? this.Caption.Render(logEvent));
            else
                MessageBox.Show(message ?? this.Layout.Render(logEvent), caption ?? this.Caption.Render(logEvent),MessageBoxButton.OK, messageBoxIcon ?? ToMessageBoxImage(logEvent.Level));
        }


        private MessageBoxImage ToMessageBoxImage(string MessageBoxIconName)
        {
            if (MessageBoxIconName.Equals("question", StringComparison.OrdinalIgnoreCase))
                return MessageBoxImage.Question;
            if (MessageBoxIconName.Equals("info", StringComparison.OrdinalIgnoreCase) || MessageBoxIconName.Equals("information", StringComparison.OrdinalIgnoreCase) || MessageBoxIconName.Equals("debug", StringComparison.OrdinalIgnoreCase) || MessageBoxIconName.Equals("trace", StringComparison.OrdinalIgnoreCase) || MessageBoxIconName.Equals("asterisk", StringComparison.OrdinalIgnoreCase) || MessageBoxIconName.Equals("yes", StringComparison.OrdinalIgnoreCase))
                return MessageBoxImage.Information;
            if (MessageBoxIconName.Equals("Warning", StringComparison.OrdinalIgnoreCase) || MessageBoxIconName.Equals("warn", StringComparison.OrdinalIgnoreCase) || MessageBoxIconName.Equals("exclamation", StringComparison.OrdinalIgnoreCase))
                return MessageBoxImage.Warning;
            if (MessageBoxIconName.Equals("error", StringComparison.OrdinalIgnoreCase) || MessageBoxIconName.Equals("stop", StringComparison.OrdinalIgnoreCase) || MessageBoxIconName.Equals("fatal", StringComparison.OrdinalIgnoreCase) || MessageBoxIconName.Equals("hand", StringComparison.OrdinalIgnoreCase))
                return MessageBoxImage.Stop;
            if (MessageBoxIconName.Equals("no", StringComparison.OrdinalIgnoreCase) || MessageBoxIconName.Equals("none", StringComparison.OrdinalIgnoreCase))
                return MessageBoxImage.None;
            return MessageBoxImage.None;
        }

        private MessageBoxImage ToMessageBoxImage(LogLevel logLevel)
        {
            return logLevel == LogLevel.Trace ? MessageBoxImage.Information
                : (logLevel == LogLevel.Debug ? MessageBoxImage.Information
                : (logLevel == LogLevel.Info ? MessageBoxImage.Information
                : (logLevel == LogLevel.Warn ? MessageBoxImage.Warning
                : (logLevel == LogLevel.Error ? MessageBoxImage.Error
                : (logLevel == LogLevel.Fatal ? MessageBoxImage.Stop
                : MessageBoxImage.None)))));
        }
    }
}
