# Contributing

Thank you for your interest in contributing to the .NET MAUI Community Toolkit! In this document we'll outline what you need to know about contributing and how to get started.

First and foremost: we're all friends here. Whether you are a first-time contributor or a core team member from one of the associated projects, we welcome any and all people to contribute to our lovely little project. I mean, it is called *community* toolkit after all.

Having that said, if you are a first-timer and you could use some help please reach out to any core member. They will be happy to help you out or find someone who can.

Furthermore, for anyone, we would like you to take into consideration the following guidelines.

### Make an effort to be nice

If you disagree, that's fine. We don't think about everything the same way, be respectful and at some point decide to agree to disagree. If a decision needs to be made, try to involve at least one other person without continuing an endless discussion.

When you disagree with a piece of code that is written, try to be helpful and explain why you disagree or how things can be improved (according to you). Always remember there are numerous ways to solve things, there is not one right way, but it's always good to learn about alternatives.

During a code review try to make a habit out of it to say at least one nice thing. Obviously about something you like in the code. If a change is not that big or so straight-forward that you can't comment nicely on that, find something else to compliment the person. Make an effort to look at their profile of blog and mention something you like, make that persons day a bit better! <3

### Make an effort to see it from their perspective

Remember English is not everyones native language. Written communication always lacks non-verbal communication. With written communication in a language that is not your native tongue it is even harder to express certain emotions.

Always assume that people mean to do right. Try to read a sentence a couple of times over and take things more literal. Try to place yourself in their shoes and see the message beyond the actual words. 

Things might come across different than they were intended, please keep that in mind and always check to see how someone meant it. If you're not sure, pull someone offline in a private channel on Twitter or email and chat about it for a bit. Maybe even jump on a call to collaborate. We're living in the 21st century, all the tools are there, why not use them to get to know each other and be friends?!

Besides language, we understand that contributing to open-source mostly happens in your spare time. Remember that priorities might change and we can only spend our time once. This works as a two-way street: don't expect things to be solved instantly, but also please let us know if you do not have the capacity to finish work you have in progress. There is no shame in that. That way it's clear to other people that they can step in and take over.

### THANK YOU!

Lastly, a big thank you for spending your precious time on our project. We appreciate any effort you make to help us with this project.

## Code of Conduct

Please see our [Code of Conduct](https://dotnetfoundation.org/code-of-conduct).

As should be clear by now: we assume everyone tries to do their best, everyone should be treated with respect and equally.

In the unfortunate event that doesn't happen, please feel free to report it to any of the team members or reach out to [Gerald](maillo:gerald.versluis@microsoft.com) directly.

We will take appropriate actions and measures if necessary.

## Prerequisites

You will need to complete a Contribution License Agreement before any pull request can be accepted. Complete the CLA at https://cla.dotnetfoundation.org/. This will also be triggered whenever you open a PR and the link should guide you through it.

## Opening a PR process

### TL;DR
* Find an issue/feature, make sure that the issue/feature has been `Approved` and is welcomed (also see [New Features](https://github.com/CommunityToolkit/Maui#submitting-a-new-feature))
* Fork repository
* Create branch
* Implement
* Open a PR
* We merge
* High-fives all-around

### Please consider

#### Tabs vs. Spaces?!
[Tabs](https://www.reddit.com/r/javascript/comments/c8drjo/nobody_talks_about_the_real_reason_to_use_tabs/).

#### Make your changes small, don't keep adding
We love your enthusiasm, but small changes and small PRs are easier to digest. We're all doing this in our spare time, it is easier to review a couple of small things and merge that and iterate from there than to have a PR with 100+ files changed that will sit there forever.

#### Added features should have tests, a sample and documentation
We like quality as much as the next person, so please provide tests.

In addition, we would want a new feature or change to be as clear as possible for other developers. Please add a sample to the sample app as part of your PR and also provide a PR to our [documentation repository](https://github.com/MicrosoftDocs/CommunityToolkit).

## Contributing Code - Best Practices

### Enums
* Always use `Unknown` at index 0 for return types that may have a value that is not known
* Always use `Default` at index 0 for option types that can use the system default option
* Follow naming guidelines for tense... `SensorSpeed` not `SensorSpeeds`
* Assign values (0,1,2,3) for all enums

### Property Names
* Include units only if one of the platforms includes it in their implementation. For instance HeadingMagneticNorth implies degrees on all platforms, but PressureInHectopascals is needed since platforms don't provide a consistent API for this.

### Units
* Use the standard units and most well accepted units when possible. For instance Hectopascals are used on UWP/Android and iOS uses Kilopascals so we have chosen Hectopascals.

### Pattern matching

#### Null checking
* Prefer using `is` when checking for null instead of `==`.

e.g. 

```csharp
// null
if (something is null)
{

}

// or not null
if (something is not null)
{
   
}
```

* Avoid using the `!` [null forgiving operator](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-forgiving) to avoid the unintended introduction of bugs.

#### Type checking

* Prefer `is` when checking for types instead of casting.

e.g.

```csharp
if (something is Bucket bucket)
{
   bucket.Empty();
}
```

### File Scoped Namespaces
* Use [file scoped namespaces](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-10.0/file-scoped-namespaces) to help reduce code verbosity.

e.g. 

```csharp
namespace CommunityToolkit.Maui.Converters;

using System;

class BoolToObjectConverter
{
}
```

### Braces

Please use `{ }` after `if`, `for`, `foreach`, `do`, `while`, etc.

e.g.

```csharp
if (something is not null)
{
   ActOnIt();
}
```

### Bug Fixes

If you're looking for something to fix, please browse [open issues](https://github.com/CommunityToolkit/Maui.Markup/issues). 

Follow the style used by the [.NET Foundation](https://github.com/dotnet/runtime/blob/master/docs/coding-guidelines/coding-style.md), with two primary exceptions:

- We do not use the `private` keyword as it is the default accessibility level in C#.
- We will **not** use `_` or `s_` as a prefix for internal or private field names
- We will use `camelCaseFieldName` for naming internal or private fields in both instance and static implementations

Read and follow our [Pull Request template](https://github.com/CommunityToolkit/Maui.Markup/blob/main/.github/PULL_REQUEST_TEMPLATE.md)

### Proposals

To propose a change or new feature, review the guidance on [Submitting a New Feature](https://github.com/CommunityToolkit/Maui.Markup#submitting-a-new-feature).

#### Non-Starter Topics
The following topics should generally not be proposed for discussion as they are non-starters:

* Large renames of APIs
* Large non-backward-compatible breaking changes
* Platform-Specifics which can be accomplished without changing the .NET MAUI Community Toolkit
* Avoid clutter posts like "+1" which do not serve to further the conversation, please use the emoji resonses for that