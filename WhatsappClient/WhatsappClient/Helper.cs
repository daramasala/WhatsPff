using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace WhatsappClient
{
    public static class Helper
    {
        /// <summary>
        /// Get childrens of given type for a given controller
        /// </summary>
        /// <typeparam name="T">Type of requested object</typeparam>
        /// <param name="depObj">Parent object</param>
        /// <returns></returns>
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
