using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AMSExplorer
{
    /// <summary>
    /// AataGridViewColumn implementation that provides a column that
    /// will display a progress bar.
    /// </summary>
    public class DataGridViewProgressBarColumn : DataGridViewTextBoxColumn
    {
        public DataGridViewProgressBarColumn()
        {
            // Set the cell template
            CellTemplate = new DataGridViewProgressBarCell();

            // Set the default style padding
            Padding pad = new(
              DataGridViewProgressBarCell.STANDARD_HORIZONTAL_MARGIN,
              DataGridViewProgressBarCell.STANDARD_VERTICAL_MARGIN,
              DataGridViewProgressBarCell.STANDARD_HORIZONTAL_MARGIN,
              DataGridViewProgressBarCell.STANDARD_VERTICAL_MARGIN);
            DefaultCellStyle.Padding = pad;

            // Set the default format
            DefaultCellStyle.Format = "## \\%";
        }
    }

    /// <summary>
    /// A DataGridViewCell class that is used to display a progress bar
    /// within a grid cell.
    /// </summary>
    public class DataGridViewProgressBarCell : DataGridViewTextBoxCell
    {
        /// <summary>
        /// Standard value to use for horizontal margins
        /// </summary>
        internal const int STANDARD_HORIZONTAL_MARGIN = 4;

        /// <summary>
        /// Standard value to use for vertical margins
        /// </summary>
        internal const int STANDARD_VERTICAL_MARGIN = 4;

        /// <summary>
        /// Constructor sets the expected type to int
        /// </summary>
        public DataGridViewProgressBarCell()
        {
            ValueType = typeof(int);
        }

        /// <summary>
        /// Paints the content of the cell
        /// </summary>
        protected override void Paint(Graphics g, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds,
          int rowIndex, DataGridViewElementStates cellState,
          object value, object formattedValue,
          string errorText,
          DataGridViewCellStyle cellStyle,
          DataGridViewAdvancedBorderStyle advancedBorderStyle,
          DataGridViewPaintParts paintParts)
        {
            int leftMargin = STANDARD_HORIZONTAL_MARGIN;
            int rightMargin = STANDARD_HORIZONTAL_MARGIN;
            int topMargin = STANDARD_VERTICAL_MARGIN;
            int bottomMargin = STANDARD_VERTICAL_MARGIN;
            PointF fontPlacement = new(0, 0);

            int progressVal;
            if (value != null)
            {
                //progressVal = (int)value;
                progressVal = (int)((double)value);
            }
            else
            {
                progressVal = 0;
            }

            // Draws the cell grid
            base.Paint(g, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, (paintParts & ~DataGridViewPaintParts.ContentForeground));

            // Get margins from the style
            if (null != cellStyle)
            {
                leftMargin = cellStyle.Padding.Left;
                rightMargin = cellStyle.Padding.Right;
                topMargin = cellStyle.Padding.Top;
                bottomMargin = cellStyle.Padding.Bottom;
            }

            // Calculate the sizes
            int imgHeight = cellBounds.Bottom - cellBounds.Top - (topMargin + bottomMargin);
            int imgWidth = cellBounds.Right - cellBounds.Left - (leftMargin + rightMargin);
            if (imgWidth <= 0)
            {
                imgWidth = 1;
            }
            if (imgHeight <= 0)
            {
                imgHeight = 1;
            }

            // Calculate the progress
            int progressWidth = imgWidth * progressVal / 100;
            if (progressWidth <= 0)
            {
                if (progressVal > 0)
                {
                    progressWidth = 1;
                }
                else
                {
                    progressWidth = 0;
                }
            }

            // Calculate the font
            if (null != formattedValue)
            {
                SizeF availArea = new(imgWidth, imgHeight);
                SizeF fontSize = g.MeasureString(formattedValue.ToString(), cellStyle.Font, availArea);

                #region [Font Placement Calc]

                if (null == cellStyle)
                {
                    fontPlacement.Y = cellBounds.Y + topMargin + ((imgHeight - fontSize.Height) / 2);
                    fontPlacement.X = cellBounds.X + leftMargin + ((imgWidth - fontSize.Width) / 2);
                }
                else
                {
                    // Set the Y vertical position
                    switch (cellStyle.Alignment)
                    {
                        case DataGridViewContentAlignment.BottomCenter:
                        case DataGridViewContentAlignment.BottomLeft:
                        case DataGridViewContentAlignment.BottomRight:
                            {
                                fontPlacement.Y = cellBounds.Y + topMargin + imgHeight - fontSize.Height;
                                break;
                            }
                        case DataGridViewContentAlignment.TopCenter:
                        case DataGridViewContentAlignment.TopLeft:
                        case DataGridViewContentAlignment.TopRight:
                            {
                                fontPlacement.Y = cellBounds.Y + topMargin - fontSize.Height;
                                break;
                            }
                        case DataGridViewContentAlignment.MiddleCenter:
                        case DataGridViewContentAlignment.MiddleLeft:
                        case DataGridViewContentAlignment.MiddleRight:
                        case DataGridViewContentAlignment.NotSet:
                        default:
                            {
                                fontPlacement.Y = cellBounds.Y + topMargin + ((imgHeight - fontSize.Height) / 2);
                                break;
                            }
                    }
                    // Set the X horizontal position
                    switch (cellStyle.Alignment)
                    {

                        case DataGridViewContentAlignment.BottomLeft:
                        case DataGridViewContentAlignment.MiddleLeft:
                        case DataGridViewContentAlignment.TopLeft:
                            {
                                fontPlacement.X = cellBounds.X + leftMargin;
                                break;
                            }
                        case DataGridViewContentAlignment.BottomRight:
                        case DataGridViewContentAlignment.MiddleRight:
                        case DataGridViewContentAlignment.TopRight:
                            {
                                fontPlacement.X = cellBounds.X + leftMargin + imgWidth - fontSize.Width;
                                break;
                            }
                        case DataGridViewContentAlignment.BottomCenter:
                        case DataGridViewContentAlignment.MiddleCenter:
                        case DataGridViewContentAlignment.TopCenter:
                        case DataGridViewContentAlignment.NotSet:
                        default:
                            {
                                fontPlacement.X = cellBounds.X + leftMargin + ((imgWidth - fontSize.Width) / 2);
                                break;
                            }
                    }
                }
                #endregion [Font Placement Calc]
            }

            if (progressVal <= 100) // because when job is done or in error, we set progress > 100 % to avoid displaying the progress bar
            {
                // Draw the background
                System.Drawing.Rectangle backRectangle = new(cellBounds.X + leftMargin, cellBounds.Y + topMargin, imgWidth, imgHeight);
                using (SolidBrush backgroundBrush = new(Color.FromKnownColor(KnownColor.LightGray)))
                {
                    g.FillRectangle(backgroundBrush, backRectangle);
                }

                // Draw the progress bar
                if (progressWidth > 0)
                {
                    System.Drawing.Rectangle progressRectangle = new(cellBounds.X + leftMargin, cellBounds.Y + topMargin, progressWidth, imgHeight);
                    using (LinearGradientBrush progressGradientBrush = new(progressRectangle, Color.LightGreen, Color.MediumSeaGreen, LinearGradientMode.Vertical))
                    {
                        progressGradientBrush.SetBlendTriangularShape((float).5);
                        g.FillRectangle(progressGradientBrush, progressRectangle);
                    }
                }

                // Draw the text
                if (null != formattedValue && null != cellStyle)
                {
                    using (SolidBrush fontBrush = new(cellStyle.ForeColor))
                    {
                        g.DrawString(formattedValue.ToString(), cellStyle.Font, fontBrush, fontPlacement);
                    }
                }
            }
        }
    }
}
