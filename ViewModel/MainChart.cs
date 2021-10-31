using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StepsAnalysis.Model;
using LiveCharts.Definitions.Series;
using System.Windows.Media;

namespace StepsAnalysis.ViewModel {

    /// <summary>
    /// Class for drow chart on LiveCharts.
    /// </summary>
    public class MainChart {
        /// <summary>
        /// Points type for main chart.
        /// </summary>
        public struct Point {
            public int X;
            public int Y;
            public object Parameter;

            public Point(int x, int y, object parameter = null) {
                X = x;
                Y = y;
                Parameter = parameter;
            }
        } 

        private static MainChart _instance = null;

        private SeriesCollection _series;

        /// <summary>
        /// Get instance main char.
        /// </summary>
        public static MainChart Instance => _instance;

        /// <summary>
        /// Get series collection.
        /// </summary>
        public SeriesCollection Series => _series;

        /// <summary>
        /// 
        /// </summary>
        public MainChart() {
            var seriesType = Mappers.Xy<Point>()
                .X(point => point.X)
                .Y(point => point.Y)
                .Fill(point => {
                    string parameter = point.Parameter as string;
                    if (parameter != null) {
                        if (parameter == "max") {
                            return new SolidColorBrush(Color.FromRgb(100, 200, 100));
                        } else if (parameter == "min") {
                            return new SolidColorBrush(Color.FromRgb(250, 100, 100));
                        }
                    }
                    return null;
                })
                .Stroke(point => {
                     string parameter = point.Parameter as string;
                     if (parameter != null) {
                         if (parameter == "max") {
                             return new SolidColorBrush(Color.FromRgb(100, 200, 100));
                         } else if (parameter == "min") {
                             return new SolidColorBrush(Color.FromRgb(250, 100, 100));
                         }
                     }
                     return null;
                });
            _series = new SeriesCollection(seriesType);

            //AddLine(new Point[] { new Point(0, 100), new Point(30, 900) });
            

            if (_instance == null) {
                _instance = this;
            }
        }

        /// <summary>
        /// Adds an empty line on chart.
        /// </summary>
        /// <returns>New line index</returns>
        public int AddLine() {
            _series.Add(new LineSeries() {});
            return _series.Count - 1;
        }

        /// <summary>
        /// Adds an line on chart.
        /// </summary>
        /// <param name="series">Array (int int)</param>
        public void AddLine(Point[] series) {
            _series.Add(new LineSeries() {
                Values = new ChartValues<Point>(series)
            });
        }

        /// <summary>
        /// Add new point to a line o the index. The line must exist.
        /// </summary>
        /// <param name="lineIndex">The index of the line to which you want to add a point</param>
        /// <param name="point">Point (int, int)</param>
        public void AddPoint(int lineIndex, Point point) {
            if (lineIndex >= _series.Count) {
                return;
            }
            _series[lineIndex]?.Values.Add(point);
        }

        /// <summary>
        /// Add new point to a last line.
        /// </summary>
        public void AddPoint(Point point) {
            AddPoint(_series.Count - 1, point);
        }

        /// <summary>
        /// Clears all line on the chart.
        /// </summary>
        public void ClearLines() {
            _series.Clear();
        }
    }
}
