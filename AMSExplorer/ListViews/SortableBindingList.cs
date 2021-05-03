using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AMSExplorer
{
    public class SortableBindingList<T> : BindingList<T>
    {
        private ListSortDirection _sortDirectionValue;
        private PropertyDescriptor _sortPropertyValue;

        private List<T> _originalList;

        private readonly Action<SortableBindingList<T>, List<T>>
            _populateBaseList = (a, b) => a.ResetItems(b);

        public SortableBindingList(IEnumerable<T> enumerable)
        {
            _originalList = enumerable.ToList();
            _populateBaseList(this, _originalList);
        }

        protected override void ApplySortCore(PropertyDescriptor prop,
            ListSortDirection direction)
        {
            if (!IsComparable(prop.PropertyType))
            {
                throw new NotSupportedException(
                    "Cannot sort by " + prop.Name + ". " +
                    prop.PropertyType + " does not implement IComparable");
            }

            _sortPropertyValue = prop;
            _sortDirectionValue = direction;

            IEnumerable<T> query = Items;

            query = direction == ListSortDirection.Ascending
                ? query.OrderBy(i => prop.GetValue(i))
                : query.OrderByDescending(i => prop.GetValue(i));

            int newIndex = 0;

            foreach (object item in query)
            {
                Items[newIndex++] = (T)item;
            }

            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        private void ResetItems(List<T> items)
        {
            base.ClearItems();

            for (int i = 0; i < items.Count; i++)
            {
                base.InsertItem(i, items[i]);
            }
        }

        protected override bool SupportsSortingCore =>
                // indeed we do
                true;

        protected override ListSortDirection SortDirectionCore => _sortDirectionValue;

        protected override PropertyDescriptor SortPropertyCore => _sortPropertyValue;

        private bool IsComparable(Type type)
        {
            if (type.GetInterface("IComparable") != null)
            {
                return true;
            }

            if (!type.IsValueType)
            {
                return false;
            }

            Type underlyingType = Nullable.GetUnderlyingType(type);

            return underlyingType != null && underlyingType.GetInterface("IComparable") != null;
        }

        protected override void OnListChanged(ListChangedEventArgs e)
        {
            _originalList = Items.ToList();
        }
    }

}
