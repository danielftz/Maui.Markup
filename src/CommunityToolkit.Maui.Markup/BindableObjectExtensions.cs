﻿namespace CommunityToolkit.Maui.Markup;

/// <summary>
/// Extension Methods for Bindable Objects
/// </summary>
public static class BindableObjectExtensions
{
	const string bindingContextPath = Binding.SelfPath;

	/// <summary>Bind to a specified property</summary>
	public static TBindable Bind<TBindable>(
		this TBindable bindable,
		BindableProperty targetProperty,
		string path = bindingContextPath,
		BindingMode mode = BindingMode.Default,
		IValueConverter? converter = null,
		object? converterParameter = null,
		string? stringFormat = null,
		object? source = null,
		object? targetNullValue = null,
		object? fallbackValue = null) where TBindable : BindableObject
	{
		bindable.SetBinding(
			targetProperty,
			new Binding(path, mode, converter, converterParameter, stringFormat, source)
			{
				TargetNullValue = targetNullValue,
				FallbackValue = fallbackValue
			});

		return bindable;
	}

	/// <summary>Bind to a specified property with inline conversion</summary>
	public static TBindable Bind<TBindable, TSource, TDest>(
		this TBindable bindable,
		BindableProperty targetProperty,
		string path = bindingContextPath,
		BindingMode mode = BindingMode.Default,
		Func<TSource?, TDest>? convert = null,
		Func<TDest?, TSource>? convertBack = null,
		string? stringFormat = null,
		object? source = null,
		TDest? targetNullValue = default,
		TDest? fallbackValue = default) where TBindable : BindableObject
	{
		var converter = new FuncConverter<TSource, TDest, object>(convert, convertBack);
		bindable.SetBinding(
			targetProperty,
			new Binding(path, mode, converter, null, stringFormat, source)
			{
				TargetNullValue = targetNullValue,
				FallbackValue = fallbackValue
			});

		return bindable;
	}

	/// <summary>Bind to a specified property with inline conversion and conversion parameter</summary>
	public static TBindable Bind<TBindable, TSource, TParam, TDest>(
		this TBindable bindable,
		BindableProperty targetProperty,
		string path = bindingContextPath,
		BindingMode mode = BindingMode.Default,
		Func<TSource?, TParam?, TDest>? convert = null,
		Func<TDest?, TParam?, TSource>? convertBack = null,
		TParam? converterParameter = default,
		string? stringFormat = null,
		object? source = null,
		TDest? targetNullValue = default,
		TDest? fallbackValue = default) where TBindable : BindableObject
	{
		var converter = new FuncConverter<TSource, TDest, TParam>(convert, convertBack);
		bindable.SetBinding(
			targetProperty,
			new Binding(path, mode, converter, converterParameter, stringFormat, source)
			{
				TargetNullValue = targetNullValue,
				FallbackValue = fallbackValue
			});

		return bindable;
	}

	/// <summary>Bind to the default property</summary>
	public static TBindable Bind<TBindable>(
		this TBindable bindable,
		string path = bindingContextPath,
		BindingMode mode = BindingMode.Default,
		IValueConverter? converter = null,
		object? converterParameter = null,
		string? stringFormat = null,
		object? source = null,
		object? targetNullValue = null,
		object? fallbackValue = null) where TBindable : BindableObject
	{
		bindable.Bind(
			DefaultBindableProperties.GetDefaultProperty<TBindable>(),
			path, mode, converter, converterParameter, stringFormat, source, targetNullValue, fallbackValue);

		return bindable;
	}

	/// <summary>Bind to the default property with inline conversion</summary>
	public static TBindable Bind<TBindable, TSource, TDest>(
		this TBindable bindable,
		string path = bindingContextPath,
		BindingMode mode = BindingMode.Default,
		Func<TSource?, TDest>? convert = null,
		Func<TDest?, TSource>? convertBack = null,
		string? stringFormat = null,
		object? source = null,
		TDest? targetNullValue = default,
		TDest? fallbackValue = default) where TBindable : BindableObject
	{
		var converter = new FuncConverter<TSource, TDest, object>(convert, convertBack);

		bindable.Bind(
			DefaultBindableProperties.GetDefaultProperty<TBindable>(),
			path, mode, converter, null, stringFormat, source, targetNullValue, fallbackValue);

		return bindable;
	}

	/// <summary>Bind to the default property with inline conversion and conversion parameter</summary>
	public static TBindable Bind<TBindable, TSource, TParam, TDest>(
		this TBindable bindable,
		string path = bindingContextPath,
		BindingMode mode = BindingMode.Default,
		Func<TSource?, TParam?, TDest>? convert = null,
		Func<TDest?, TParam?, TSource>? convertBack = null,
		TParam? converterParameter = default,
		string? stringFormat = null,
		object? source = null,
		TDest? targetNullValue = default,
		TDest? fallbackValue = default) where TBindable : BindableObject
	{
		var converter = new FuncConverter<TSource, TDest, TParam>(convert, convertBack);
		bindable.Bind(
			DefaultBindableProperties.GetDefaultProperty<TBindable>(),
			path, mode, converter, converterParameter, stringFormat, source, targetNullValue, fallbackValue);

		return bindable;
	}

	/// <summary>Bind to the <typeparamref name="TBindable"/>'s default Command and CommandParameter properties </summary>
	/// <param name="bindable">The Bindable Object</param>
	/// <param name="path">Binding Path</param>
	/// <param name="source">Binding Source</param>
	/// <param name="parameterPath">If null, no binding is created for the CommandParameter property</param>
	/// <param name="parameterSource">Parameter Binding Source</param>
	public static TBindable BindCommand<TBindable>(
		this TBindable bindable,
		string path = bindingContextPath,
		object? source = null,
		string? parameterPath = bindingContextPath,
		object? parameterSource = null) where TBindable : BindableObject
	{
		(var commandProperty, var parameterProperty) = DefaultBindableProperties.GetCommandAndCommandParameterProperty<TBindable>();

		bindable.SetBinding(commandProperty, new Binding(path: path, source: source));

		if (parameterPath is not null)
		{
			bindable.SetBinding(parameterProperty, new Binding(path: parameterPath, source: parameterSource));
		}

		return bindable;
	}

	/// <summary>
	/// Sets the property values for <paramref name="light"/> and <paramref name="dark"/> themes respectively.
	/// </summary>
	/// <typeparam name="TBindable">The type of the <paramref name="bindable"/> object.</typeparam>
	/// <typeparam name="TValue">The value type to be set for <paramref name="light"/> and <paramref name="dark"/> theme.</typeparam>
	/// <param name="bindable">The bindable object to <c>SetAppTheme</c> on.</param>
	/// <param name="bindableProperty">The property to apply to the <paramref name="light"/> and <paramref name="dark"/> values to.</param>
	/// <param name="light">The value to use when the device is configured to use a light theme.</param>
	/// <param name="dark">The value to use when the device is configured to use a dark theme.</param>
	/// <returns>The <paramref name="bindable"/> instance to allow for fluently building the user interface.</returns>
	public static TBindable AppThemeBinding<TBindable, TValue>(this TBindable bindable, BindableProperty bindableProperty, TValue light, TValue dark) where TBindable : BindableObject
	{
		bindable.SetAppTheme(bindableProperty, light, dark);

		return bindable;
	}

	/// <summary>
	/// Set the app theme color for <paramref name="light"/> and <paramref name="dark"/> themes respectively.
	/// </summary>
	/// <typeparam name="TBindable">The type of the <paramref name="bindable"/> object.</typeparam>
	/// <param name="bindable">The bindable object to <c>SetAppThemeColor</c> on.</param>
	/// <param name="bindableProperty">The property to apply to the <paramref name="light"/> and <paramref name="dark"/> values to.</param>
	/// <param name="light">The <see cref="Color"/> to use when the device is configured to use a light theme.</param>
	/// <param name="dark">The <see cref="Color"/> to use when the device is configured to use a dark theme.</param>
	/// <returns>The <paramref name="bindable"/> instance to allow for fluently building the user interface.</returns>
	public static TBindable AppThemeColorBinding<TBindable>(this TBindable bindable, BindableProperty bindableProperty, Color light, Color dark) where TBindable : BindableObject
	{
		bindable.SetAppThemeColor(bindableProperty, light, dark);

		return bindable;
	}
}