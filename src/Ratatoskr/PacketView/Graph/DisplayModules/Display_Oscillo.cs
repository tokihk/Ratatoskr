﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.PacketView.Graph.Configs;


namespace Ratatoskr.PacketView.Graph.DisplayModules
{
    internal class Display_Oscillo : DisplayModule
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


        private decimal[][]	points_;				// points_[record_point][display_point]
        private int			point_in_ = 0;
		private uint		ch_index_max_ = 0;

		private Graphics	gdc_all_;
		private Graphics	gdc_graph_;

		private Rectangle	graph_rect_;

		private uint		axisx_min_;
		private uint		axisx_max_;
		private uint		axisx_point_;


        public Display_Oscillo(PacketViewPropertyImpl prop) : base(prop)
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

            points_ = new decimal[Math.Max(1, (uint)prop.Oscillo_RecordPoint.Value)][];

			axisx_point_ = (uint)prop.Oscillo_DisplayPoint.Value;
        }

        public override uint PointCount
        {
            get { return ((uint)points_.Length); }
        }

		private uint GetAxisYPoint(ChannelConfig ch_cfg)
		{
			var point = (uint)0;

			switch (ch_cfg.OscilloVertRange) {
				case VertRangeType.Preset_100:		point = 100;									break;
				case VertRangeType.Preset_200:		point = 200;									break;
				case VertRangeType.Preset_500:		point = 500;									break;
				case VertRangeType.Preset_10000:	point = 10000;									break;
				case VertRangeType.Custom:			point = (uint)ch_cfg.OscilloVertRangeCustom;	break;
				default:							point = 1;										break;
			}

			return (point);
		}

        private void LoadGraphValue(uint ch_index, uint data_index, ref decimal value)
        {
            var draw_point_offset = point_in_ + data_index;

            if (draw_point_offset >= points_.Length) {
                draw_point_offset -= points_.Length;
            }

            if (   (points_[draw_point_offset] != null)
                && (ch_index < points_[draw_point_offset].Length)
            ) {
                value = points_[draw_point_offset][ch_index];
            }
        }

		private void DrawGraph(DisplayContext dc)
        {
			/* 背景を初期化 */
			gdc_all_.FillRectangle(GRAPH_DETAILS_BG_BRUSH, dc.CanvasRect);
			gdc_graph_.FillRectangle(GRAPH_DETAILS_BG_BRUSH, graph_rect_);

			/* グラフの線を描画 */
			DrawGraph_Grid(dc);

			/* グラフのデータを描画 */
			for (var ch_index = (uint)0; ch_index < dc.ChannelConfigs.Length; ch_index++) {
				if (ch_index < ch_index_max_) {
					DrawGraph_Data(dc, ch_index, dc.ChannelConfigs[ch_index]);
				}
			}

			/* グラフの枠を描画 */
			DrawGraph_Frame(dc);
        }

        private void DrawGraph_Grid(DisplayContext dc)
        {
            var draw_offset = 0;
            var draw_scale_rect = new Rectangle();
            var draw_scale_format = new StringFormat();

            /* --- X軸 --- */
            draw_scale_rect.Width = graph_rect_.Width * GRID_X_LINES_MAIN / GRID_X_LINES;
            draw_scale_rect.Height = GRAPH_DETAILS_GRID_SCALE_FONT.Height;
            draw_scale_rect.X = graph_rect_.Left - draw_scale_rect.Width / 2;
            draw_scale_rect.Y = graph_rect_.Bottom;

            draw_scale_format.Alignment = StringAlignment.Center;
            draw_scale_format.LineAlignment = StringAlignment.Near;

			{
				var rect = new Rectangle();
				var format = new StringFormat();

				rect.X = dc.CanvasRect.X;
				rect.Y = dc.CanvasRect.Y - 10;
				rect.Width = 100;
				rect.Height = 100;

				format.Alignment = StringAlignment.Near;
				format.LineAlignment = StringAlignment.Near;

				gdc_graph_.DrawString(
					"test",
					GRAPH_DETAILS_GRID_SCALE_FONT,
					GRAPH_DETAILS_GRID_SCALE_BRUSH,
					rect,
					format);
			}

            for (var index = 0; index <= GRID_X_LINES; index++) {
                draw_offset = graph_rect_.Left + graph_rect_.Width * index / GRID_X_LINES;

                /* 目盛(最右を0とする) */
                if ((index % GRID_X_LINES_MAIN) == 0) {
					var str = (dc.HScrollPos + index * (axisx_point_ / GRID_X_LINES) - PointCount);

                    gdc_all_.DrawString(
                        (dc.HScrollPos + index * (axisx_point_ / GRID_X_LINES) - PointCount).ToString(),
                        GRAPH_DETAILS_GRID_SCALE_FONT,
                        GRAPH_DETAILS_GRID_SCALE_BRUSH,
                        draw_scale_rect,
                        draw_scale_format);

                    draw_scale_rect.X += draw_scale_rect.Width;
                }

                /* 罫線 */
                if ((index > 0) && (index < GRID_X_LINES)) {
                    gdc_graph_.DrawLine(
                        ((index % 5) == 0) ? (GRAPH_DETAILS_GRID_LINE_MAIN_PEN) : (GRAPH_DETAILS_GRID_LINE_SUB_PEN),
                        draw_offset,
                        graph_rect_.Top,
                        draw_offset,
                        graph_rect_.Bottom);
                }
            }

            /* --- Y軸 --- */
            draw_scale_rect.Width = GRAPH_DETAILS_MARGIN.Right;
            draw_scale_rect.Height = graph_rect_.Height * GRID_Y_LINES_MAIN / GRID_Y_LINES;
            draw_scale_rect.X = graph_rect_.Right;
            draw_scale_rect.Y = graph_rect_.Top - draw_scale_rect.Height / 2;

            draw_scale_format.Alignment = StringAlignment.Near;
            draw_scale_format.LineAlignment = StringAlignment.Center;

            for (var index = 0; index <= GRID_Y_LINES; index++) {
                draw_offset = graph_rect_.Top + graph_rect_.Height * index / GRID_Y_LINES;

#if false
				/* 目盛 */
				if ((index % GRID_Y_LINES_MAIN) == 0) {
                    dc.Canvas.DrawString(
                        (disp_param.AxisY_Max - index * ((disp_param.AxisY_Max - disp_param.AxisY_Min) / GRID_Y_LINES)).ToString(),
                        GRAPH_DETAILS_GRID_SCALE_FONT,
                        GRAPH_DETAILS_GRID_SCALE_BRUSH,
                        draw_scale_rect,
                        draw_scale_format);

                    draw_scale_rect.Y += draw_scale_rect.Height;
                }
#endif

                /* 罫線 */
                if ((index > 0) && (index < GRID_Y_LINES)) {
                    gdc_graph_.DrawLine(
                        ((index % 5) == 0) ? (GRAPH_DETAILS_GRID_LINE_MAIN_PEN) : (GRAPH_DETAILS_GRID_LINE_SUB_PEN),
                        graph_rect_.Left,
                        draw_offset,
                        graph_rect_.Right,
                        draw_offset);
                }
            }
        }

        private void DrawGraph_Frame(DisplayContext dc)
        {
            gdc_graph_.DrawRectangle(
				GRAPH_DETAILS_GRID_FRAME_PEN,
				new Rectangle(
					graph_rect_.X,
					graph_rect_.Y,
					graph_rect_.Width - 1,
					graph_rect_.Height - 1)
			);
        }

        private void DrawGraph_Data(DisplayContext dc, uint ch_index, ChannelConfig ch_cfg)
        {
			var axisy_point = GetAxisYPoint(ch_cfg);
			var axisy_max = axisy_point * (GRID_Y_LINES / GRID_Y_LINES_MAIN);

            var value_y = (decimal)0;
            var value_y_canvas = 0;

            var value_x_canvas = 0;
            var value_x_canvas_last = 0;

            var value_x_step  = (decimal)graph_rect_.Width / axisx_point_;
            var value_y_mag = (decimal)graph_rect_.Height / axisy_point;

            var draw_pen = new Pen(ch_cfg.ForeColor);
            var draw_points = new List<Point>();

            for (var value_x = dc.HScrollPos; value_x <= axisx_max_; value_x++) {
                /* 実データを座標データに置き換える */
                value_x_canvas = (int)((value_x - axisx_min_) * value_x_step);

                if (   (value_x == axisx_min_)
                    || (value_x_canvas != value_x_canvas_last)
                ) {
                    /* 実データを取得 */
                    LoadGraphValue(ch_index, value_x, ref value_y);

                    /* 実データを補正 */
                    value_y = value_y + ch_cfg.OscilloVertOffset;

                    /* 座標データに変換(ウィンドウ座標はwordサイズ以内としなければ例外が発生) */
                    value_y_canvas = (int)Math.Max(short.MinValue, Math.Min(short.MaxValue, (axisy_max - value_y) * value_y_mag));

                    draw_points.Add(new Point(graph_rect_.Left + value_x_canvas, graph_rect_.Top + value_y_canvas));
                    value_x_canvas_last = value_x_canvas;
                }
            }

            if (draw_points.Count > 1) {
                gdc_graph_.DrawLines(draw_pen, draw_points.ToArray());
            }
        }

        protected override void OnClearValue()
        {
            points_ = new decimal[points_.Length][];
            point_in_ = 0;

			ch_index_max_ = 0;
        }

        protected override void OnInputValue(decimal[] value)
        {
            points_[point_in_++] = value;
            if (point_in_ >= points_.Length) {
                point_in_ = 0;
            }

            ch_index_max_ = Math.Max(ch_index_max_, (uint)value.Length);
        }

		protected override void OnDrawDisplay(DisplayContext dc)
		{
			graph_rect_.X = dc.CanvasRect.Left + GRAPH_DETAILS_MARGIN.Left;
			graph_rect_.Y = dc.CanvasRect.Top + GRAPH_DETAILS_MARGIN.Top;
			graph_rect_.Width = dc.CanvasRect.Width - GRAPH_DETAILS_MARGIN.Horizontal;
			graph_rect_.Height = dc.CanvasRect.Height - GRAPH_DETAILS_MARGIN.Vertical;

            if (   (graph_rect_.Width > 0)
                && (graph_rect_.Height > 0)
            ) {
                /* レイヤー描画 */
                var bgc_all = BufferedGraphicsManager.Current.Allocate(dc.Canvas, dc.CanvasRect);
				var bgc_graph = BufferedGraphicsManager.Current.Allocate(dc.Canvas, graph_rect_);

				gdc_all_ = bgc_all.Graphics;
				gdc_graph_ = bgc_graph.Graphics;

				/* グラフポイント数 */
				axisx_point_ = (uint)dc.Property.Oscillo_DisplayPoint.Value;
				axisx_min_ = dc.HScrollPos;
				axisx_max_ = dc.HScrollPos + axisx_point_;

				DrawGraph(dc);

                bgc_all.Render(dc.Canvas);
                bgc_all.Dispose();

                bgc_graph.Render(dc.Canvas);
                bgc_graph.Dispose();
            }
		}
    }
}