// neural v3.0
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace JaysAi.Finale.SystemLogic.Helpers
{
    public static class WpfUtilities
    {
        /// <summary>
        /// Recursively searches for a child of a given type and name.
        /// </summary>
        public static T? FindChild<T>(DependencyObject parent, string childName = "") where T : DependencyObject
        {
            if (parent == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child is T childType)
                {
                    if (string.IsNullOrEmpty(childName))
                        return childType;

                    if (child is FrameworkElement frameworkElement && frameworkElement.Name == childName)
                        return childType;
                }

                T? foundChild = FindChild<T>(child, childName);
                if (foundChild != null) return foundChild;
            }

            return null;
        }

        /// <summary>
        /// Finds the first ancestor of a specified type in the visual tree.
        /// </summary>
        public static T? FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            while (current != null)
            {
                if (current is T ancestor)
                    return ancestor;

                current = VisualTreeHelper.GetParent(current);
            }

            return null;
        }

        /// <summary>
        /// Tries to retrieve a resource of a given key and type.
        /// </summary>
        public static T? GetResource<T>(string key) where T : class
        {
            if (Application.Current.Resources.Contains(key))
                return Application.Current.Resources[key] as T;

            return null;
        }

        /// <summary>
        /// Applies a theme style to a WPF control from global resources.
        /// </summary>
        public static void ApplyStyle(Control control, string styleKey)
        {
            if (control == null || string.IsNullOrWhiteSpace(styleKey))
                return;

            if (Application.Current.Resources[styleKey] is Style style)
                control.Style = style;
        }

        /// <summary>
        /// Traverses the visual tree and applies an action to every element of a given type.
        /// </summary>
        public static void TraverseVisualTree<T>(DependencyObject root, Action<T> action) where T : DependencyObject
        {
            if (root == null || action == null) return;

            int count = VisualTreeHelper.GetChildrenCount(root);
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(root, i);
                if (child is T target)
                    action(target);

                TraverseVisualTree(child, action);
            }
        }
    }
}
