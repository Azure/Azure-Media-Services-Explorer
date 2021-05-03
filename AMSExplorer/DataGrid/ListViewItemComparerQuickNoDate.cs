using System.Collections;
using System.Windows.Forms;

namespace AMSExplorer
{
    public class ListViewItemComparerQuickNoDate : IComparer
    {
        private readonly int col;
        private readonly SortOrder order;
        public ListViewItemComparerQuickNoDate()
        {
            col = 0;
            order = SortOrder.Ascending;
        }
        public ListViewItemComparerQuickNoDate(int column, SortOrder order)
        {
            col = column;
            this.order = order;
        }
        public int Compare(object x, object y)
        {
            int returnVal = 0;
            bool Error = false;

            string sx = ((ListViewItem)x).SubItems[col].Text;
            string sy = ((ListViewItem)y).SubItems[col].Text;


            // Inverse_FormatByteSize
            // let's compare if the field is a size format (512 B or 45 KB...)

            // Parse the two objects passed as a parameter as a DateTime.
            try
            {
                long? firstSize = AssetTools.Inverse_FormatByteSize(sx);
                long? secondSize = AssetTools.Inverse_FormatByteSize(sy);
                if (firstSize != null && secondSize != null)
                {
                    long firstSizel = (long)firstSize;
                    long secondSizel = (long)secondSize;
                    if (firstSizel < secondSizel)
                    {
                        returnVal = -1;
                    }
                    else if (firstSizel > secondSizel)
                    {
                        returnVal = 1;
                    }
                    else
                    {
                        returnVal = 0;
                    }
                }
                else
                {
                    Error = true;
                }

            }
            catch
            {
                Error = true;
            }

            if (Error)
            {
                // Compare the two items as a string.
                returnVal = string.Compare(sx, sy);
            }


            // Determine whether the sort order is descending.
            if (order == SortOrder.Descending)
            {
                // Invert the value returned by String.Compare.
                returnVal *= -1;
            }

            return returnVal;
        }

        public static void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView ThisListView = (ListView)sender;
            // Determine whether the column is the same as the last column clicked.
            if (e.Column != ((int)ThisListView.Tag))
            {
                // Set the sort column to the new column.
                ThisListView.Tag = e.Column;
                // Set the sort order to ascending by default.
                ThisListView.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (ThisListView.Sorting == SortOrder.Ascending)
                {
                    ThisListView.Sorting = SortOrder.Descending;
                }
                else
                {
                    ThisListView.Sorting = SortOrder.Ascending;
                }
            }

            // Call the sort method to manually sort.
            ThisListView.Sort();
            // Set the ListViewItemSorter property to a new ListViewItemComparer
            // object.
            ThisListView.ListViewItemSorter = new ListViewItemComparer(e.Column,
                                                              ThisListView.Sorting);
        }
    }

}
