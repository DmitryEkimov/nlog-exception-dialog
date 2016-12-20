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
	[Target("ExceptionBox")]
	public sealed class ExceptionBoxTarget : TargetWithLayout
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ExceptionBoxTarget"/> class.
		/// </summary>
		/// <remarks>The default value of the layout is: <code>${longdate}|${level:uppercase=true}|${logger}|${message}</code></remarks>
		public ExceptionBoxTarget()
		{
			Caption = "${processname}";
			DetailLayout = "${logger} ${message}${onexception:${newline}${exception:format=ToString}}";
		}
		//[RequiredParameter]
		/// <summary>
		/// Установить Заголовок MessageBox по умолчанию имя процесса.
		/// </summary>
		/// <value>The caption.</value>
		public Layout Caption { get; set; }

		/// <summary>
		/// Представление информации в окне для подробных сведений
		/// (по умолчанию "${logger} ${message}${onexception:${newline}${exception:format=ToString}}").
		/// </summary>
		public Layout DetailLayout { get; set; }

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

			ShowMessageDialog(sb.ToString(), sbDetail.ToString(), lastLogEvent.LogEvent);

			for (int i = 0; i < logEvents.Length; ++i)
			{
				logEvents[i].Continuation(null);
			}
		}

		private void ShowMessage(LogEventInfo logEvent)
		{
			ShowMessageDialog(Layout.Render(logEvent), logEvent.Exception != null ? DetailLayout.Render(logEvent) : null, logEvent);
		}

		private void ShowMessageDialog(string message, string detail, LogEventInfo lastLogEvent)
		{
			// определить стандартную айконку по уровню сообщения
			MessageBoxImage messageBoxIcon = ToMessageBoxImage(lastLogEvent.Level);
			// переопределить при наличии в свойствах имени заменяющей айконки
			if (lastLogEvent.Properties != null && lastLogEvent.Properties.Count > 0 && lastLogEvent.Properties.ContainsKey("MessageBoxIcon"))
			{
				messageBoxIcon = ToMessageBoxImage(lastLogEvent.Properties["MessageBoxIcon"].ToString());
			}

			// при отсутствии детальной информации используем стандартный диалог
			if (string.IsNullOrEmpty(detail))
			{
				MessageBox.Show(message ?? this.Layout.Render(lastLogEvent), Caption.Render(lastLogEvent),MessageBoxButton.OK, messageBoxIcon );
			}
			else
			{
				dlgException2.Show(detail, message ?? this.Layout.Render(lastLogEvent),  Caption.Render(lastLogEvent));
			}
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
