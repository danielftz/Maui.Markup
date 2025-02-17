﻿using CommunityToolkit.Maui.Markup.RightToLeft;
using CommunityToolkit.Maui.Markup.UnitTests.Base;
using Microsoft.Maui.Controls;
using NUnit.Framework;

namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture]
class ViewExtensionsRightToLeftTests : BaseMarkupTestFixture<BoxView>
{
	[Test]
	public void Left()
		=> TestPropertiesSet(v => v.Left(), (View.HorizontalOptionsProperty, LayoutOptions.Start, LayoutOptions.End));

	[Test]
	public void Right()
		=> TestPropertiesSet(v => v.Right(), (View.HorizontalOptionsProperty, LayoutOptions.End, LayoutOptions.Start));

	[Test]
	public void SupportDerivedFromView()
	{
		Assert.IsInstanceOf<DerivedFromView>(new DerivedFromView().Left().Right());
	}

	class DerivedFromView : BoxView { }
}