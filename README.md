# UWP-Testers

**AsyncTester**

1. Progress and Cancel
2. DataOperations - ProjectData (Read), LocalData (Read/Write), LocalData with Picker (Read/Write)


**AdaptiveTester**

1. Bugnion Sample
2. Prosise Sample
3. Responsive Grid


**CSharpTester**

Instead of using Console apps for examples I used LinqPad which saves code as .linq files. To get the free Standard version of LinqPad go to http://www.linqpad.net/download.aspx.

1. Delegates


**DataBindingTester**

1. Basic MVVM
2. Binding to a UserControl DataTemplate
3. Interactivity EventBinding
4. MVVM with CollectionViewSource
5. MVVM with x:Bind
6. ReUseable UserControl with DataBinding
7. SemanticZoom


**DesignPatternsTester**

1. Observer Pattern


**DialogsTester**

1. Confirmation
2. Notification
3. SearchParameters
4. Sign In
5. Terms Of Use


**ItemsControlTester**

1. GridView
2. ListView
3. FlipView (future)
4. ListBox (future)
5. ComboBox (future)
6. SemanticZoom
7. CollectionViewSource
8. AdaptiveTrigger


**LinqTester**

Files with extension .linq in the LinqLibrary were created with LinqPad. Some of the LinqPad queries depend on ADO.NET Data Models which are located in the DataLibrary. To get the free Standard version of LinqPad go to http://www.linqpad.net/download.aspx.

1. Rattz - examples from the book Pro LINQ by Joe Rattz
2. OrderIT - examples from the book Entity Framework 4 In Action by Stefano Mostarda. Includes class inheritance.

**MemoryGame** 

A fairly complex Microsoft Blend tutorial. Originally created for Visual Studio 2013. Converted to a Visual Studio 2015 Universal Windows App. Sample code not available anymore. Tutorial steps are located at: https://msdn.microsoft.com/en-us/library/windows/apps/jj129462(v=vs.110).aspx.


**UWP.Petzold**

Converts the samples in "Programming Windows Sixth Edition Writing Windows 8 Apps with C# and XAML" by Charles Petzold to UWP. Each chapter has its own project. Each project was created using the UWPShellTemplate at  https://github.com/ronlemire2/UWPProjectTemplates. Each sample has its own Page in the Views folder. Most of the samples are exactly as they were written by Charles Petzold. A few samples are my own variation on the original but have the same output. These variations have names that begin with 'rjl'.

# UWP-ProjectTemplates
Visual Studio 2015 Project Templates for Universal Windows Platform (UWP)

**UWPSimpleTemplate Notes:** 

Bare bones. No SplitView but some converters. Saves creating basic folders for a MVVM type project. Implements simple MVVM with Commanding.

**UWPShellTemplate Notes:** 

Acknowledgement - Most of the code for UWPShellTemplate came from these 2 sources:

1. Channel 9 Build 2015 - Mical Lewis - Universal Navigation and Commanding for Your XAML - https://channel9.msdn.com/Events/Build/2015/2-97
2. Universal Windows Sample - https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlNavigation which is the code from the Channel 9 video.

Features:

1. AppShell with SplitView/Frame navigation pattern
2. Custom NavMenuListView for SplitView.Pane
3. Highlights select item in NavMenuListView
4. Keyboard support is very good
5. Supports Hierarchical navigation with a DrillinPage and a BasicSubPage
6. CommandBar Page uses CommandBar and Menu flyouts in CommandBar.SecondaryCommands
7. AdaptiveTrigger changes SplitView DisplayMode at "0" and "720" widths

My Changes:

1. AppShell.xaml
	* Replaced HamburgerButton ToogleButton with a Button because ToogleButton wouldn't respond to the Enter key.
	* Added HamburgerButton_Click in code behind to toggle SplitView pane.
	* Moved BackButton from controls:NavMenuListView to the right side of HamburgerButton.
	* Added  horizontal StackPanel to contain HamburgerButton, BackButton, Page Title, AutoSuggestBox.
	* Changed BackButton IsEnabled attribute to a Visibility attribute.
	* Added a BooleanToVisibilityConverter to BackButton's Visibility attribute
2. Styles.xaml
	* Commented out TextBlock in NavigationBackButtonStyle to make BackButton content just the back arrow
3. Changes are commented in the code.

Sample Views:

1. LandingPage - place for app features and instructions
2. BasicPage - ordinary page
3. DrillInPage - example of Hierarchical navigation
4. BasicSubPage - subPage of DrillInPage's hierarchy
5. CommandBarPage - example of commands and button flyouts

Feel free to delete any of the example pages and substitute your own.

Remember to:

1. Give each page a name so that it's shown in the PageTitle
2. Update AppShell.xaml.cs with NavMenuItems (including item icon and label) corresponding to your pages.
3. Update AppShell.xaml with SplitView 'OpenPanelLength' to show complete NavMenuItem label

