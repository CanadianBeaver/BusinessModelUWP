# Business Model in UWP applications

I would like to share my experience in developing the UWP applications based on Prism and Unity.

## Introduction

The [Universal Windows Platform](https://en.wikipedia.org/wiki/Universal_Windows_Platform) is a great idea by Microsoft (with "blackjack and technologies") that allows to develop desktop applications for different devices, such as Desktops, Phones, Xboxs, HoloLens and Surfaces. Microsoft offers a variety of technologies for developers, most of them are open-source and can be found on github. Also, SQLite is the recommended database for creating local storage in the UWP applications and [Entity Framework Core](https://docs.microsoft.com/en-us/windows/uwp/data-access/sqlite-databases) has been introduced for it.

Basically, the development of UWP applications is similar to the development of WPF applications based on the MVVM template. We can use data binding in View, as well as commands and dependency injection in ViewModel. The Prism library can help us to do it, because has been implemented for UWP and is helpful for developing applications based on MVVM pattern with dependency injection and commands. 

### Default UWP approach

With the classical approach the UWP application encapsulates visualization logic and business logic in the same place, as well as SQL queries or queries to the services. Developer has to create a page and code that is serving this page (code-behind). This approach is perfect for implementing the simple requirements and the simple business logic.

The key features are:
* In XAML code of the page should be defined controls that will be used for data visualization
* In code-behind of the page should be realized logic for extracting data and binding this data to the properties of controls by name

As an alternative, in code-behind can be defined properties for keeping extracted data. These properties can be bind in XAML via `{x:Bind}` markup extension.

[Example](https://github.com/CanadianBeaver/BusinessModelUWP/wiki/Classical-UWP-approach)

### Default MVVM approach

With the MVVM approach the UWP application encapsulates visualization logic and business logic separately. Developer has to create a page (that is View) just for visualization the data. The SQL queries or queries to the services must be implemented in the separated class (that is ViewModel), where results of these queries should be stored in public properties. This approach uses Two-Way Databinding and Commands for communicating between Views and ViewModels. In many cases, the ViewModel implements the INotifyPropertyChanged interface to notify the View about changing in the properties.

The key features are:
* In XAML code of View should be defined controls that will be bind to the public properties of ViewModel
* In code-behind of View should be realized logic for creating the ViewModel and linked it to the View by DataContext property
* In ViewModel should be realized logic for extracting data and storing this data into the public properties

Also, the ViewModel can implement the commands that can be executed from View.

[Example](https://github.com/CanadianBeaver/BusinessModelUWP/wiki/MVVM-approach)

## Animation

...
