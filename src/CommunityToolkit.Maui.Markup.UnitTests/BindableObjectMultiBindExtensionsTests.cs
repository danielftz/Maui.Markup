﻿using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Maui.Markup.UnitTests.Base;
using Microsoft.Maui.Controls;
using NUnit.Framework;

namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture]
class BindableObjectMultiBindExtensionsTests : BaseMarkupTestFixture
{
	ViewModel? viewModel;
	List<BindingBase>? testBindings;
	List<object>? testConvertValues;

	[SetUp]
	public override void Setup()
	{
		base.Setup();

		viewModel = new ViewModel();

		testBindings = new List<BindingBase>
		{
			new Binding(nameof(viewModel.Text)),
			new Binding(nameof(viewModel.Id)),
			new Binding(nameof(viewModel.IsDone)),
			new Binding(nameof(viewModel.Fraction)),
			new Binding(nameof(viewModel.Count))
		};

		testConvertValues = new List<object>
		{
			"Hi",
			Guid.Parse("{272383A4-92E3-46BA-99DC-438D81E039AB}"),
			true,
			0.5,
			3
		};
	}

	[TearDown]
	public override void TearDown()
	{
		viewModel = null;
		testBindings = null;
		testConvertValues = null;
		base.TearDown();
	}

	[Test]
	[TestCase(true, false)]
	[TestCase(false, true)]
	[TestCase(true, true)]
	public void BindSpecifiedPropertyWith2BindingsAndInlineConvert(bool testConvert, bool testConvertBack)
	{
		ArgumentNullException.ThrowIfNull(testBindings);

		var label = new Label();

		// Repeat inline converter code to test that the Bind overloads allow inferring the generic parameter types
		if (testConvert && testConvertBack)
		{
			label.Bind<Label, string?, Guid, string>(
				Label.TextProperty,
				testBindings[0], testBindings[1],
				((string? text, Guid id) v) => Format(0, v.text, v.id),
				(string? formatted) =>
				{
					ArgumentNullException.ThrowIfNull(formatted);

					var u = Unformat(0, formatted);
					return (u.Text, u.Id);
				}
			);
		}
		else if (testConvert && !testConvertBack)
		{
			label.Bind(
				Label.TextProperty,
				testBindings[0], testBindings[1],
				((string? text, Guid id) v) => Format(0, v.text, v.id)
			);
		}
		else if (!testConvert && testConvertBack)
		{
			label.Bind(
				Label.TextProperty,
				testBindings[0], testBindings[1],
				convertBack: (string? formatted) =>
				{
					ArgumentNullException.ThrowIfNull(formatted);

					var u = Unformat(0, formatted);
					return (u.Text, u.Id);
				}
			);
		}

		AssertLabelTextMultiBound(label, 2, testConvert, testConvertBack);
	}

	[Test]
	[TestCase(true, false)]
	[TestCase(false, true)]
	[TestCase(true, true)]
	public void BindSpecifiedPropertyWith2BindingsAndInlineConvertAndParameter(bool testConvert, bool testConvertBack)
	{
		ArgumentNullException.ThrowIfNull(testBindings);

		var label = new Label();

		// Repeat inline converter code to test that the Bind overloads allow inferring the generic parameter types
		if (testConvert && testConvertBack)
		{
			label.Bind<Label, string?, Guid, int?, string>(
				Label.TextProperty,
				testBindings[0],
				testBindings[1],
				((string? text, Guid id) v, int? parameter) =>
				{
					ArgumentNullException.ThrowIfNull(parameter);

					var formattedText = Format(parameter.Value, v.text, v.id);
					return formattedText;
				},
				(string? formatted, int? parameter) =>
				{
					ArgumentNullException.ThrowIfNull(parameter);
					ArgumentNullException.ThrowIfNull(formatted);

					var unformattedResult = Unformat(parameter.Value, formatted);
					return (unformattedResult.Text, unformattedResult.Id);
				},
				converterParameter: 2
			);
		}
		else if (testConvert && !testConvertBack)
		{
			label.Bind<Label, string?, Guid, int?, string>(
				Label.TextProperty,
				testBindings[0], testBindings[1],
				((string? text, Guid id) v, int? parameter) =>
				{
					ArgumentNullException.ThrowIfNull(parameter);

					var formattedText = Format(parameter.Value, v.text, v.id);
					return formattedText;
				},
				converterParameter: 2
			);
		}
		else if (!testConvert && testConvertBack)
		{
			label.Bind<Label, string?, Guid, int?, string>(
				Label.TextProperty,
				testBindings[0], testBindings[1],
				convertBack: (string? formatted, int? parameter) =>
				{
					ArgumentNullException.ThrowIfNull(parameter);
					ArgumentNullException.ThrowIfNull(formatted);

					var unformattedResult = Unformat(parameter.Value, formatted);
					return (unformattedResult.Text, unformattedResult.Id);
				},
				converterParameter: 2
			);
		}

		AssertLabelTextMultiBound(label, 2, testConvert, testConvertBack, 2);
	}

	[Test]
	[TestCase(true, false)]
	[TestCase(false, true)]
	[TestCase(true, true)]
	public void BindSpecifiedPropertyWith3BindingsAndInlineConvert(bool testConvert, bool testConvertBack)
	{
		ArgumentNullException.ThrowIfNull(testBindings);

		var label = new Label();

		// Repeat inline converter code to test that the Bind overloads allow inferring the generic parameter types
		if (testConvert && testConvertBack)
		{
			label.Bind<Label, string?, Guid, bool, string>(
				Label.TextProperty,
				testBindings[0], testBindings[1], testBindings[2],
				((string? text, Guid id, bool isDone) v) => Format(0, v.text, v.id, v.isDone),
				(string? formatted) =>
				{
					ArgumentNullException.ThrowIfNull(formatted);

					var unformattedResult = Unformat(0, formatted);
					return (unformattedResult.Text, unformattedResult.Id, unformattedResult.IsDone);
				}
			);
		}
		else if (testConvert && !testConvertBack)
		{
			label.Bind<Label, string?, Guid, bool, string>(
				Label.TextProperty,
				testBindings[0], testBindings[1], testBindings[2],
				((string? text, Guid id, bool isDone) v) => Format(0, v.text, v.id, v.isDone)
			);
		}
		else if (!testConvert && testConvertBack)
		{
			label.Bind<Label, string?, Guid, bool, string>(
				Label.TextProperty,
				testBindings[0], testBindings[1], testBindings[2],
				convertBack: (string? formatted) =>
				{
					ArgumentNullException.ThrowIfNull(formatted);

					var unformatedResult = Unformat(0, formatted);
					return (unformatedResult.Text, unformatedResult.Id, unformatedResult.IsDone);
				}
			);
		}

		AssertLabelTextMultiBound(label, 3, testConvert, testConvertBack);
	}

	[Test]
	[TestCase(true, false)]
	[TestCase(false, true)]
	[TestCase(true, true)]
	public void BindSpecifiedPropertyWith3BindingsAndInlineConvertAndParameter(bool testConvert, bool testConvertBack)
	{
		ArgumentNullException.ThrowIfNull(testBindings);

		var label = new Label();

		// Repeat inline converter code to test that the Bind overloads allow inferring the generic parameter types
		if (testConvert && testConvertBack)
		{
			label.Bind(
					Label.TextProperty,
					testBindings[0], testBindings[1], testBindings[2],
					((string? text, Guid id, bool isDone) v, int? parameter) =>
					{
						ArgumentNullException.ThrowIfNull(parameter);

						return Format(parameter.Value, v.text, v.id, v.isDone);
					},
					(string? formatted, int? parameter) =>
					{
						ArgumentNullException.ThrowIfNull(parameter);
						ArgumentNullException.ThrowIfNull(formatted);

						var unformattedResult = Unformat(parameter.Value, formatted);
						return (unformattedResult.Text ?? throw new NullReferenceException(), unformattedResult.Id, unformattedResult.IsDone);
					},
					converterParameter: 2
				);
		}
		else if (testConvert && !testConvertBack)
		{
			label.Bind(
				Label.TextProperty,
				testBindings[0], testBindings[1], testBindings[2],
				((string? text, Guid id, bool isDone) v, int? parameter) =>
				{
					ArgumentNullException.ThrowIfNull(parameter);

					return Format(parameter.Value, v.text, v.id, v.isDone);
				},
				converterParameter: 2
			);
		}
		else if (!testConvert && testConvertBack)
		{
			label.Bind(
				Label.TextProperty,
				testBindings[0], testBindings[1], testBindings[2],
				convertBack: (string? formatted, int? parameter) =>
				{
					ArgumentNullException.ThrowIfNull(formatted);
					ArgumentNullException.ThrowIfNull(parameter);

					var unformattedResult = Unformat(parameter.Value, formatted);
					return (unformattedResult.Text, unformattedResult.Id, unformattedResult.IsDone);
				},
				converterParameter: 2
			);
		}

		AssertLabelTextMultiBound(label, 3, testConvert, testConvertBack, 2);
	}

	[Test]
	[TestCase(true, false)]
	[TestCase(false, true)]
	[TestCase(true, true)]
	public void BindSpecifiedPropertyWith4BindingsAndInlineConvert(bool testConvert, bool testConvertBack)
	{
		ArgumentNullException.ThrowIfNull(testBindings);

		var label = new Label();

		// Repeat inline converter code to test that the Bind overloads allow inferring the generic parameter types
		if (testConvert && testConvertBack)
		{
			label.Bind(
				Label.TextProperty,
				testBindings[0], testBindings[1], testBindings[2], testBindings[3],
				((string? text, Guid id, bool isDone, double fraction) v) => Format(0, v.text, v.id, v.isDone, v.fraction),
				(string? formatted) =>
				{
					ArgumentNullException.ThrowIfNull(formatted);

					var unformattedResult = Unformat(0, formatted);
					return (unformattedResult.Text ?? string.Empty, unformattedResult.Id, unformattedResult.IsDone, unformattedResult.Fraction);
				}
			);
		}
		else if (testConvert && !testConvertBack)
		{
			label.Bind(
				Label.TextProperty,
				testBindings[0], testBindings[1], testBindings[2], testBindings[3],
				((string? text, Guid id, bool isDone, double fraction) v) => Format(0, v.text, v.id, v.isDone, v.fraction)
			);
		}
		else if (!testConvert && testConvertBack)
		{
			label.Bind(
				Label.TextProperty,
				testBindings[0], testBindings[1], testBindings[2], testBindings[3],
				convertBack: (string? formatted) =>
				{
					ArgumentNullException.ThrowIfNull(formatted);

					var unformattedResult = Unformat(0, formatted);
					return (unformattedResult.Text, unformattedResult.Id, unformattedResult.IsDone, unformattedResult.Fraction);
				}
			);
		}

		AssertLabelTextMultiBound(label, 4, testConvert, testConvertBack);
	}

	[Test]
	[TestCase(true, false)]
	[TestCase(false, true)]
	[TestCase(true, true)]
	public void BindSpecifiedPropertyWith4BindingsAndInlineConvertAndParameter(bool testConvert, bool testConvertBack)
	{
		ArgumentNullException.ThrowIfNull(testBindings);

		var label = new Label();

		// Repeat inline converter code to test that the Bind overloads allow inferring the generic parameter types
		if (testConvert && testConvertBack)
		{
			label.Bind(
				Label.TextProperty,
				testBindings[0], testBindings[1], testBindings[2], testBindings[3],
				((string? text, Guid id, bool isDone, double fraction) v, int? parameter) =>
				{
					ArgumentNullException.ThrowIfNull(parameter);
					return Format(parameter.Value, v.text, v.id, v.isDone, v.fraction);
				},
				(string? formatted, int? parameter) =>
				{
					ArgumentNullException.ThrowIfNull(formatted);
					ArgumentNullException.ThrowIfNull(parameter);

					var unformattedResult = Unformat(parameter.Value, formatted);
					return (unformattedResult.Text ?? string.Empty, unformattedResult.Id, unformattedResult.IsDone, unformattedResult.Fraction);
				},
				converterParameter: 2
			);
		}
		else if (testConvert && !testConvertBack)
		{
			label.Bind(
				Label.TextProperty,
				testBindings[0], testBindings[1], testBindings[2], testBindings[3],
				((string? text, Guid id, bool isDone, double fraction) v, int? parameter) =>
				{
					ArgumentNullException.ThrowIfNull(parameter);
					return Format(parameter.Value, v.text, v.id, v.isDone, v.fraction);
				},
				converterParameter: 2
			);
		}
		else if (!testConvert && testConvertBack)
		{
			label.Bind(
				Label.TextProperty,
				testBindings[0], testBindings[1], testBindings[2], testBindings[3],
				convertBack: (string? formatted, int? parameter) =>
				{
					ArgumentNullException.ThrowIfNull(formatted);
					ArgumentNullException.ThrowIfNull(parameter);

					var unformattedResult = Unformat(parameter.Value, formatted);
					return (unformattedResult.Text, unformattedResult.Id, unformattedResult.IsDone, unformattedResult.Fraction);
				},
				converterParameter: 2
			);
		}

		AssertLabelTextMultiBound(label, 4, testConvert, testConvertBack, 2);
	}

	[Test]
	[TestCase(true, false)]
	[TestCase(false, true)]
	[TestCase(true, true)]
	public void BindSpecifiedPropertyWithMultipleBindings(bool testConvert, bool testConvertBack)
	{
		ArgumentNullException.ThrowIfNull(testBindings);

		Func<object[], string>? convert = null;
		if (testConvert)
		{
			convert = (object[] v) => Format(0, v[0], v[1], v[2], v[3], v[4]);
		}

		Func<string?, object[]>? convertBack = null;
		if (testConvertBack)
		{
			convertBack = (string? formatted) =>
			{
				ArgumentNullException.ThrowIfNull(formatted);

				var result = Unformat(0, formatted);
				return new object[] { result.Text ?? string.Empty, result.Id, result.IsDone, result.Fraction, result.Count };
			};
		}

		var converter = new FuncMultiConverter<string, object>(convert, convertBack);
		var label = new Label { }.Bind(Label.TextProperty, GetTestBindings(5), converter);
		AssertLabelTextMultiBound(label, 5, testConvert, testConvertBack, converter: converter);
	}

	[Test]
	[TestCase(true, false)]
	[TestCase(false, true)]
	[TestCase(true, true)]
	public void BindSpecifiedPropertyWithMultipleBindingsAndParameter(bool testConvert, bool testConvertBack)
	{
		Func<object[], int, string?>? convert = null;
		if (testConvert)
		{
			convert = (object[] v, int parameter) => Format(parameter,
			v[0], v[1], v[2], v[3], v[4]);
		}

		Func<string?, int, object?[]>? convertBack = null;
		if (testConvertBack)
		{
			convertBack = (string? text, int parameter) =>
			{
				ArgumentNullException.ThrowIfNull(text);

				var unformattedResult = Unformat(parameter, text);
				return new object[] { unformattedResult.Text ?? string.Empty, unformattedResult.Id, unformattedResult.IsDone, unformattedResult.Fraction, unformattedResult.Count };
			};
		}

		var converter = new FuncMultiConverter<string?, int>(convert, convertBack);
		var label = new Label { }.Bind(Label.TextProperty, GetTestBindings(5), converter, 2);
		AssertLabelTextMultiBound(label, 5, testConvert, testConvertBack, 2, converter);
	}

	List<BindingBase> GetTestBindings(int count) => testBindings?.Take(count).ToList() ?? Enumerable.Empty<BindingBase>().ToList();

	object[] GetTestConvertValues(int count) => testConvertValues?.Take(count).ToArray() ?? Array.Empty<BindingBase>();

	static string PrefixDots(object? value, int count) => $"{new string('.', count)}{value}";

	static string RemoveDots(string text, int count) => text.Substring(count);

	static string Format(int parameter, params object?[] values)
	{
		var formatted = $"'{PrefixDots(values[0], parameter)}'";
		for (var i = 1; i < values.Length; i++)
		{
			formatted += $", '{values[i]}'";
		}

		return formatted;
	}

	static (string? Text, Guid Id, bool IsDone, double Fraction, int Count) Unformat(int parameter, string formatted)
	{
		var split = formatted.Split('\'');
		var n = split.Length;

		return (
			n > 1 ? RemoveDots(split[1], parameter) : null,
			n > 3 ? Guid.Parse(split[3]) : Guid.Empty,
			n > 5 && bool.Parse(split[5]),
			n > 7 ? double.Parse(split[7]) : 0,
			n > 9 ? int.Parse(split[9]) : 0);
	}

	void AssertLabelTextMultiBound(Label label, int nBindings, bool testConvert, bool testConvertBack, int parameter, IMultiValueConverter? converter = null)
	{
		var values = GetTestConvertValues(nBindings);
		var expected = Format(parameter, values);

		BindingHelpers.AssertBindingExists<string, int>(
			label,
			targetProperty: Label.TextProperty,
			GetTestBindings(nBindings),
			converter,
			parameter,
			assertConverterInstanceIsAnyNotNull: converter is null,
			assertConvert: c => c.AssertConvert(values, parameter, expected, twoWay: testConvert && testConvertBack, backOnly: !testConvert && testConvertBack));
	}

	void AssertLabelTextMultiBound(Label label, int nBindings, bool testConvert, bool testConvertBack, IMultiValueConverter? converter = null)
	{
		var values = GetTestConvertValues(nBindings);
		var expected = Format(0, values);

		BindingHelpers.AssertBindingExists<string>(
			label,
			targetProperty: Label.TextProperty,
			GetTestBindings(nBindings),
			converter,
			assertConverterInstanceIsAnyNotNull: converter is null,
			assertConvert: c => c.AssertConvert(values, expected, twoWay: testConvert && testConvertBack, backOnly: !testConvert && testConvertBack));
	}

	class ViewModel
	{
		public Guid Id { get; set; }

		public string Text { get; set; } = string.Empty;

		public bool IsDone { get; set; }

		public double Fraction { get; set; }

		public int Count { get; set; }
	}
}