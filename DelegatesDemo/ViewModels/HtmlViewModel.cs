using NFWCommonsLibrary.ViewModels;
using System;

namespace DelegatesDemo.ViewModels
{
    class HtmlViewModel : ViewModelBase
	{
		private const string cssTemplate = @"
				.container {
					position: absolute;
					top: 50%;
					left: 50%;
					-moz-transform: translateX(-50%) translateY(-50%);
					-webkit-transform: translateX(-50%) translateY(-50%);
					transform: translateX(-50%) translateY(-50%);
					font-family: Arial, Helvetica, sans-serif;
				}";

		
		private string _inputText;
		public string InputString
		{
			get { return _inputText; }
			set
			{
				_inputText = value;
				OnPropertyChanged(nameof(InputString));
				ComposeContent();
			}
		}

		private string _textContent;
		public string TextContent
		{
			get { return _textContent; }
			set
			{
				_textContent = value;
				OnPropertyChanged(nameof(TextContent));
			}
		}

		private string _htmlContent;
		public string HtmlContent
		{
			get { return _htmlContent; }
			set
			{
				_htmlContent = value;
				OnPropertyChanged(nameof(HtmlContent));
			}
		}

		private bool _bold;
		public bool Bold
		{
			get { return _bold; }
			set
			{
				_bold = value;

				OnPropertyChanged(nameof(Bold));
				ComposeContent();
			}
		}

		private bool _italic;
		public bool Italic
		{
			get { return _italic; }
			set
			{
				_italic = value;

				OnPropertyChanged(nameof(Italic));
				ComposeContent();
			}
		}

		private bool _underlined;
		public bool Underlined
		{
			get { return _underlined; }
			set
			{
				_underlined = value;

				OnPropertyChanged(nameof(Underlined));
				ComposeContent();
			}
		}

		private bool _red;
		public bool Red
		{
			get { return _red; }
			set
			{
				_red = value;

				OnPropertyChanged(nameof(Red));
				ComposeContent();
			}
		}

		private void ComposeContent()
		{
			Action contentComposer = null;

			if (!string.IsNullOrWhiteSpace(InputString))
			{
				contentComposer = () => TextContent = InputString;

				if (Bold) contentComposer += () => TextContent = $"<b>\n{TextContent}\n</b>";
				if (Italic) contentComposer += () => TextContent = $"<i>\n{TextContent}\n</i>";
				if (Underlined) contentComposer += () => TextContent = $"<u>\n{TextContent}\n</u>";
				if (Red) contentComposer += () => TextContent = $"<font color='red'>\n{TextContent}\n</font>";
			}
			else
			{
				TextContent = string.Empty;
			}

			contentComposer?.Invoke();

			HtmlContent = $@"
				<!DOCTYPE html>
				<html>
					<head>
						<style>
						{cssTemplate}
						</style>
					</head>
					<body>
						<div class='container'> 
							<span>{TextContent}</span>
						</div>
					</body>
				</html>";
		}

	}
}

