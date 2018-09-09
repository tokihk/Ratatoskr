using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.PacketViews.Graph.DisplayModules
{
    internal class DisplayModule
    {
        private readonly Padding GRAPH_DETAILS_MARGIN;

        private const int GRID_X_LINES      = 50;
        private const int GRID_X_LINES_MAIN = 5;

        private const int GRID_Y_LINES      = 50;
        private const int GRID_Y_LINES_MAIN = 5;

        private readonly Brush GRAPH_DETAILS_BG_BRUSH;
        private readonly Font  GRAPH_DETAILS_GRID_SCALE_FONT;
        private readonly Brush GRAPH_DETAILS_GRID_SCALE_BRUSH;
        private readonly Pen   GRAPH_DETAILS_GRID_FRAME_PEN;
        private readonly Pen   GRAPH_DETAILS_GRID_LINE_MAIN_PEN;
        private readonly Pen   GRAPH_DETAILS_GRID_LINE_SUB_PEN;


        public DisplayModule()
        {
            GRAPH_DETAILS_MARGIN = new Padding(50, 50, 100, 50);

            GRAPH_DETAILS_BG_BRUSH = new SolidBrush(Color.FromArgb(0, 24, 0));

            GRAPH_DETAILS_GRID_SCALE_FONT = new Font("MS Gothic", 8);
            GRAPH_DETAILS_GRID_SCALE_BRUSH = new SolidBrush(Color.FromArgb(80, 80, 80));

            GRAPH_DETAILS_GRID_FRAME_PEN = new Pen(Color.FromArgb(90, 90, 90));

            GRAPH_DETAILS_GRID_LINE_MAIN_PEN = new Pen(Color.FromArgb(70, 70, 70));
            GRAPH_DETAILS_GRID_LINE_MAIN_PEN.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            GRAPH_DETAILS_GRID_LINE_SUB_PEN = new Pen(Color.FromArgb(40, 40, 40));
            GRAPH_DETAILS_GRID_LINE_SUB_PEN.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
        }

        public virtual uint PointCount
        {
            get { return (1); }
        }

        public void ClearData()
        {
            OnClearData();
        }

        protected virtual void OnClearData()
        {
        }

        public void InputData(decimal[] data)
        {
            OnAssignData(data);
        }

        protected virtual void OnAssignData(decimal[] data)
        {
        }

        public void DrawGraph(DisplayParam disp_param, DisplayLayerParam[] layer_params)
        {
            if (   (disp_param == null)
                || (disp_param.AxisX_Min == disp_param.AxisX_Max)
            ) {
                return;
            }

            DrawGraphDetails(disp_param, layer_params);
        }

        private void DrawGraphDetails(DisplayParam disp_param, DisplayLayerParam[] layer_params)
        {
            var graph_rect = new Rectangle(
                                    disp_param.CanvasRect.Left + GRAPH_DETAILS_MARGIN.Left,
                                    disp_param.CanvasRect.Top + GRAPH_DETAILS_MARGIN.Top,
                                    disp_param.CanvasRect.Width - GRAPH_DETAILS_MARGIN.Horizontal,
                                    disp_param.CanvasRect.Height - GRAPH_DETAILS_MARGIN.Vertical);

            if (   (graph_rect.Width > 0)
                && (graph_rect.Height > 0)
                && (disp_param.AxisX_Min < disp_param.AxisX_Max)
                && (disp_param.AxisY_Min < disp_param.AxisY_Max)
            ) {
                /* レイヤー描画 */
                {
                    var graphics_layer = BufferedGraphicsManager.Current.Allocate(disp_param.Canvas, graph_rect);

                    /* 背景を初期化 */
                    graphics_layer.Graphics.FillRectangle(GRAPH_DETAILS_BG_BRUSH, graph_rect);

                    /* グラフの線を描画 */
                    DrawGraphDetails_Grid(graphics_layer.Graphics, graph_rect, disp_param);

                    /* グラフのデータを描画 */
                    for (var layer_index = (uint)0; layer_index < layer_params.Length; layer_index++) {
                        DrawGraphDetails_Data(
                            graphics_layer.Graphics,
                            graph_rect,
                            disp_param.AxisX_Min,
                            disp_param.AxisX_Max,
                            disp_param.AxisY_Min,
                            disp_param.AxisY_Max,
                            layer_index,
                            layer_params[layer_index]);
                    }

                    /* グラフの枠を描画 */
                    DrawGraphDetails_Frame(graphics_layer.Graphics, graph_rect);

                    graphics_layer.Render(disp_param.Canvas);
                    graphics_layer.Dispose();
                }
            }
        }

        private void DrawGraphDetails_Grid(Graphics graphics, Rectangle graphics_rect, DisplayParam disp_param)
        {
            var draw_offset = 0;
            var draw_scale_rect = new Rectangle();
            var draw_scale_format = new StringFormat();

            /* --- X軸 --- */
            draw_scale_rect.Width = graphics_rect.Width * GRID_X_LINES_MAIN / GRID_X_LINES;
            draw_scale_rect.Height = GRAPH_DETAILS_GRID_SCALE_FONT.Height;
            draw_scale_rect.X = graphics_rect.Left - draw_scale_rect.Width / 2;
            draw_scale_rect.Y = graphics_rect.Bottom;

            draw_scale_format.Alignment = StringAlignment.Center;
            draw_scale_format.LineAlignment = StringAlignment.Near;

            for (var index = 0; index <= GRID_X_LINES; index++) {
                draw_offset = graphics_rect.Left + graphics_rect.Width * index / GRID_X_LINES;

                /* 目盛(最右を0とする) */
                if ((index % GRID_X_LINES_MAIN) == 0) {
                    disp_param.Canvas.DrawString(
                        (disp_param.AxisX_Min + index * ((disp_param.AxisX_Max - disp_param.AxisX_Min) / GRID_X_LINES) - PointCount).ToString(),
                        GRAPH_DETAILS_GRID_SCALE_FONT,
                        GRAPH_DETAILS_GRID_SCALE_BRUSH,
                        draw_scale_rect,
                        draw_scale_format);

                    draw_scale_rect.X += draw_scale_rect.Width;
                }

                /* ライン */
                if ((index > 0) && (index < GRID_X_LINES)) {
                    graphics.DrawLine(
                        ((index % 5) == 0) ? (GRAPH_DETAILS_GRID_LINE_MAIN_PEN) : (GRAPH_DETAILS_GRID_LINE_SUB_PEN),
                        draw_offset,
                        graphics_rect.Top,
                        draw_offset,
                        graphics_rect.Bottom);
                }
            }

            /* --- Y軸 --- */
            draw_scale_rect.Width = GRAPH_DETAILS_MARGIN.Right;
            draw_scale_rect.Height = graphics_rect.Height * GRID_Y_LINES_MAIN / GRID_Y_LINES;
            draw_scale_rect.X = graphics_rect.Right;
            draw_scale_rect.Y = graphics_rect.Top - draw_scale_rect.Height / 2;

            draw_scale_format.Alignment = StringAlignment.Near;
            draw_scale_format.LineAlignment = StringAlignment.Center;

            for (var index = 0; index <= GRID_Y_LINES; index++) {
                draw_offset = graphics_rect.Top + graphics_rect.Height * index / GRID_Y_LINES;

                /* 目盛 */
                if ((index % GRID_Y_LINES_MAIN) == 0) {
                    disp_param.Canvas.DrawString(
                        (disp_param.AxisY_Max - index * ((disp_param.AxisY_Max - disp_param.AxisY_Min) / GRID_Y_LINES)).ToString(),
                        GRAPH_DETAILS_GRID_SCALE_FONT,
                        GRAPH_DETAILS_GRID_SCALE_BRUSH,
                        draw_scale_rect,
                        draw_scale_format);

                    draw_scale_rect.Y += draw_scale_rect.Height;
                }

                /* ライン */
                if ((index > 0) && (index < GRID_Y_LINES)) {
                    graphics.DrawLine(
                        ((index % 5) == 0) ? (GRAPH_DETAILS_GRID_LINE_MAIN_PEN) : (GRAPH_DETAILS_GRID_LINE_SUB_PEN),
                        graphics_rect.Left,
                        draw_offset,
                        graphics_rect.Right,
                        draw_offset);
                }
            }
        }

        private void DrawGraphDetails_Frame(Graphics graphics, Rectangle graphics_rect)
        {
            graphics.DrawRectangle(GRAPH_DETAILS_GRID_FRAME_PEN, new Rectangle(graphics_rect.X, graphics_rect.Y, graphics_rect.Width - 1, graphics_rect.Height - 1));
        }

        private void DrawGraphDetails_Data(Graphics graphics, Rectangle graphics_rect, uint axis_x_min, uint axis_x_max, decimal axis_y_min, decimal axis_y_max, uint layer_index, DisplayLayerParam layer_param)
        {
            if (   (layer_param == null)
                || (!OnLayerExistCheck(layer_index))
            ) {
                return;
            }

            var value_y = (decimal)0;
            var value_y_canvas = 0;

            var value_x_canvas = 0;
            var value_x_canvas_last = 0;

            var value_x_step  = (decimal)graphics_rect.Width / (axis_x_max - axis_x_min);
            var value_y_mag = (decimal)graphics_rect.Height / (axis_y_max - axis_y_min);

            var draw_pen = new Pen(layer_param.ForeColor);
            var draw_points = new List<Point>();

            for (var value_x = axis_x_min; value_x <= axis_x_max; value_x++) {
                /* 実データを座標データに置き換える */
                value_x_canvas = (int)((value_x - axis_x_min) * value_x_step);

                if (   (value_x == axis_x_min)
                    || (value_x_canvas != value_x_canvas_last)
                ) {
                    /* 実データを取得 */
                    OnLoadGraphData(layer_index, value_x, ref value_y);

                    /* 実データを補正 */
                    value_y = value_y * layer_param.AxisY_Magnification + layer_param.AxisY_Offset;

                    /* 座標データに変換 */
                    value_y_canvas = (int)Math.Max(int.MinValue, Math.Min(int.MaxValue, (axis_y_max - value_y) * value_y_mag));

                    draw_points.Add(new Point(graphics_rect.Left + value_x_canvas, graphics_rect.Top + value_y_canvas));
                    value_x_canvas_last = value_x_canvas;
                }
            }

            if (draw_points.Count > 1) {
                graphics.DrawLines(draw_pen, draw_points.ToArray());
            }
        }

        protected virtual bool OnLayerExistCheck(uint layer_index)
        {
            return (false);
        }

        protected virtual void OnLoadGraphData(uint layer_index, uint data_index, ref decimal value)
        {
        }
    }
}
