/*
 * Этот файл используется в используется в проектах:
 *  NLog.Visar.WPF
 *  NLog.Visar.WinForms
 *  NLog.Visar.WinForms20
 * После изменений скопировать.
 */

using System;
using System.ComponentModel;
using System.Text;
using NLog.Common;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace NLog.Visar.Targets
{
    /// <summary>
    /// Замена айконки сообщения об ошибке (например, при реакции на ошибку отображаем айконку предупреждения).
    /// </summary>
    public enum LogMessageBoxIcon
    {
        /// <summary>
        /// Убрать айконку
        /// </summary>
        None,

        /// <summary>
        /// Заменить на айконку "внимание"
        /// </summary>
        Information,

        /// <summary>
        /// Заменить на айконку "предупреждение"
        /// </summary>
        Warning,

        /// <summary>
        /// Заменить на айконку "ошибка"
        /// </summary>
        Error,
    }

    /// <summary>
    /// Представляет класс с возможностью управления заголовком, айконкой, подробным сообщением.
    /// MessageBoxIcon / MessageBoxImage не используются, поэтому отвязано от System.Windows.Forms / System.Windows
    /// </summary>
    public abstract class TargetForDialog: TargetWithLayout
    {
        /// <summary>
        /// Инициализация класса значениями по-умолчанию
        /// </summary>
        protected TargetForDialog ()
	    {
            Caption = "${processname}";
            DetailLayout = "${onexception:${logger} ${message}${newline}${exception:format=ToString}}";
	    }

        /// <summary>
        /// Заголовок диалога сообщения
        /// </summary>
        [DefaultValue("${processname}")]
        [RequiredParameter]
        public virtual Layout Caption { get; set; }

        /// <summary>
        /// Детальная информаци об ошибке
        /// </summary>
        [DefaultValue("${onexception:${logger} ${message}${newline}${exception:format=ToString}}")]
        [RequiredParameter]
        public virtual Layout DetailLayout { get; set; }

        /// <summary>
        /// Собственно отображение диалога
        /// </summary>
        /// <param name="replaceIcon"></param>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <param name="detail"></param>
        protected abstract void ShowMessageDialog(LogMessageBoxIcon replaceIcon, string caption, string message, string detail);


        /// <summary>
        /// Writes the specified log event.
        /// </summary>
        /// <param name="logEvent">The log event.</param>
        protected override void Write(LogEventInfo logEvent)
        {
            try
            {
                ShowMessageDialog(ToLogMessageBoxIcon(logEvent), Caption.Render(logEvent),
                    Layout.Render(logEvent), DetailLayout.Render(logEvent));
            }
            catch (Exception ex)
            {
                InternalLogger.Warn(ex.ToString());
                if (LogManager.ThrowExceptions)
                    throw;
            }
        }

        /// <summary>
        ///  Writes an array of logging events to the log target. By default it iterates
        ///     on all events and passes them to "Write" method. Inheriting classes can use
        ///     this method to optimize batch writes.
        /// </summary>
        /// <param name="logEvents"></param>
        protected override void Write(AsyncLogEventInfo[] logEvents)
        {
            if (logEvents.Length == 0)
            {
                return;
            }

            var sb = new StringBuilder();
            var sbDetail = new StringBuilder();
            var lastLogEvent = logEvents[logEvents.Length - 1];
            foreach (var ev in logEvents)
            {
                sb.Append(this.Layout.Render(ev.LogEvent));
                sb.AppendLine();
                if (ev.LogEvent.Exception != null)
                {
                    sbDetail.Append(this.DetailLayout.Render(ev.LogEvent));
                    sb.AppendLine();
                }
            }

            ShowMessageDialog(ToLogMessageBoxIcon(lastLogEvent.LogEvent), Caption.Render(lastLogEvent.LogEvent),
                sb.ToString(), sbDetail.ToString());

            for (int i = 0; i < logEvents.Length; ++i)
            {
                logEvents[i].Continuation(null);
            }
        }

        // Вернуть иденитификатор айконки для события
        private LogMessageBoxIcon ToLogMessageBoxIcon(LogEventInfo logEvent)
        {
            if (logEvent.Properties != null && logEvent.Properties.Count > 0 && logEvent.Properties.ContainsKey("MessageBoxIcon"))
            {
                // переопределить при наличии в свойствах имени заменяющей айконки
                return ToLogMessageBoxIcon(logEvent.Properties["MessageBoxIcon"].ToString());
            }
            else
            {
                // определить стандартную айконку по уровню сообщения
                return ToLogMessageBoxIcon(logEvent.Level);
            }
        }

        // По имени получить идентификатор айконки
        private LogMessageBoxIcon ToLogMessageBoxIcon(string MessageBoxIconName)
        {
            if (MessageBoxIconName.Equals("info", StringComparison.OrdinalIgnoreCase) || MessageBoxIconName.Equals("information", StringComparison.OrdinalIgnoreCase) || MessageBoxIconName.Equals("debug", StringComparison.OrdinalIgnoreCase) || MessageBoxIconName.Equals("trace", StringComparison.OrdinalIgnoreCase) || MessageBoxIconName.Equals("asterisk", StringComparison.OrdinalIgnoreCase) || MessageBoxIconName.Equals("yes", StringComparison.OrdinalIgnoreCase))
                return LogMessageBoxIcon.Information;
            if (MessageBoxIconName.Equals("warning", StringComparison.OrdinalIgnoreCase) || MessageBoxIconName.Equals("warn", StringComparison.OrdinalIgnoreCase) || MessageBoxIconName.Equals("exclamation", StringComparison.OrdinalIgnoreCase))
                return LogMessageBoxIcon.Warning;
            if (MessageBoxIconName.Equals("error", StringComparison.OrdinalIgnoreCase) || MessageBoxIconName.Equals("stop", StringComparison.OrdinalIgnoreCase) || MessageBoxIconName.Equals("fatal", StringComparison.OrdinalIgnoreCase) || MessageBoxIconName.Equals("hand", StringComparison.OrdinalIgnoreCase))
                return LogMessageBoxIcon.Error;
            return LogMessageBoxIcon.None;
        }

        // По уровню ошибки получить идентификатор айконки
        private LogMessageBoxIcon ToLogMessageBoxIcon(LogLevel logLevel)
        {
            return logLevel == LogLevel.Trace ? LogMessageBoxIcon.Information
                : (logLevel == LogLevel.Debug ? LogMessageBoxIcon.Information
                : (logLevel == LogLevel.Info ? LogMessageBoxIcon.Information
                : (logLevel == LogLevel.Warn ? LogMessageBoxIcon.Warning
                : (logLevel == LogLevel.Error ? LogMessageBoxIcon.Error
                : (logLevel == LogLevel.Fatal ? LogMessageBoxIcon.Error
                : LogMessageBoxIcon.None)))));
        }
    }
}
