using Ch04_Panels.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Ch04_Panels.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DependencyObjectClassHierarchyPage : Page {
        Type rootType = typeof(DependencyObject);
        TypeInfo rootTypeInfo = typeof(DependencyObject).GetTypeInfo();
        List<Type> classes = new List<Type>();
        Brush highlightBrush;

        public DependencyObjectClassHierarchyPage() {
            this.InitializeComponent();

            highlightBrush = new SolidColorBrush(new UISettings().UIElementColor(UIElementType.Highlight));

            // Accumulate all the classes that derive from DependencyObject
            AddToClassList(typeof(Windows.UI.Xaml.DependencyObject));

            // Sort them alphabetically by name
            classes.Sort((t1, t2) => {
                return String.Compare(t1.GetTypeInfo().Name, t2.GetTypeInfo().Name);
            });

            // Put all these sorted classes into a tree structure
            ClassAndSubclasses rootClass = new ClassAndSubclasses(rootType);
            AddToTree(rootClass, classes);

            // Display the tree using TextBlock's added to StackPanel
            Display(rootClass, 0);
        }

        void AddToClassList(Type sampleType) {
            Assembly assembly = sampleType.GetTypeInfo().Assembly;
            
            foreach (Type type in assembly.ExportedTypes) {
                TypeInfo typeInfo = type.GetTypeInfo();

                if (typeInfo.IsPublic && rootTypeInfo.IsAssignableFrom(typeInfo))
                    classes.Add(type);
            }
        }

        void AddToTree(ClassAndSubclasses parentClass, List<Type> classes) {
            foreach (Type type in classes) {
                Type baseType = type.GetTypeInfo().BaseType;

                if (baseType == parentClass.Type) {
                    ClassAndSubclasses subClass = new ClassAndSubclasses(type);
                    parentClass.Subclasses.Add(subClass);
                    AddToTree(subClass, classes);
                }
            }
        }

        void Display(ClassAndSubclasses parentClass, int indent) {
            TypeInfo typeInfo = parentClass.Type.GetTypeInfo();

            // Create TextBlock with type name
            TextBlock txtblk = new TextBlock();
            txtblk.Inlines.Add(new Run { Text = new string(' ', 8 * indent) });
            txtblk.Inlines.Add(new Run { Text = typeInfo.Name });

            // Indicate if the class is sealed
            if (typeInfo.IsSealed) {
                txtblk.Inlines.Add(new Run {Text = " (sealed)", Foreground = highlightBrush });
            }

            // Indicated if the class can't be instantiated
            IEnumerable<ConstructorInfo> constructorInfos = typeInfo.DeclaredConstructors;
            int publicConstructCount = 0;

            foreach (ConstructorInfo constructorInfo in constructorInfos) {
                if (constructorInfo.IsPublic) {
                    publicConstructCount += 1;
                }
            }

            if (publicConstructCount == 0) {
                txtblk.Inlines.Add(new Run { Text = " (non-instantiable)", Foreground = highlightBrush });
            }

            // Add to the StackPanel
            stackPanel.Children.Add(txtblk);

            // Call this method recursively for all subclasses
            foreach (ClassAndSubclasses subClass in parentClass.Subclasses) {
                Display(subClass, indent + 1);
            }


        }
    }
}
