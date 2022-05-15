using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Ratatoskr.PacketView.Graph.DisplayModules
{
    internal class Display_Oscillo : DisplayModule
    {
		private class GraphicsLayer : IDisposable
		{
			public GraphicsLayer(Graphics main_graphics)
			{
				Canvas = main_graphics;
			}

			public GraphicsLayer(Graphics graphics, Rectangle rect)
			{
				Manager = BufferedGraphicsManager.Current.Allocate(graphics, rect);
				Canvas = Manager.Graphics;
			}

			public BufferedGraphics Manager		{ get; }
			public Graphics			Canvas		{ get; }


			public void Dispose()
			{
				Manager?.Dispose();
			}

			public void RenderTo(Graphics graphics)
			{
				Manager?.Render(graphics);
			}

			public void RenderTo(GraphicsLayer glayer)
			{
				Manager?.Render(glayer.Canvas);
			}
		}


        private readonly Padding GRAPH_DETAILS_MARGIN;

        private const int GRID_X_DIV		= 10;
        private const int GRID_X_LINES		= 50;
        private const int GRID_X_LINES_MAIN	= GRID_X_LINES / GRID_X_DIV;

        private const int GRID_Y_DIV		= 10;
        private const int GRID_Y_LINES		= 50;
        private const int GRID_Y_LINES_MAIN	= GRID_Y_LINES / GRID_Y_DIV;

        private readonly Brush GRAPH_DETAILS_BG_BRUSH;
        private readonly Font  GRAPH_DETAILS_GRID_SCALE_FONT;
        private readonly Brush GRAPH_DETAILS_GRID_SCALE_BRUSH;
        private readonly Pen   GRAPH_DETAILS_GRID_FRAME_PEN;
        private readonly Pen   GRAPH_DETAILS_GRID_LINE_MAIN_PEN;
        private readonly Pen   GRAPH_DETAILS_GRID_LINE_SUB_PEN;


        private long[][]	ch_values_;				// points_[channel][record_point]
        private uint		ch_values_in_ = 0;

		private long		ch_value_max_ = 0;

		private uint		ch_index_max_ = 0;


		private Rectangle		canvas_rect_main_ = new Rectangle();
		private Rectangle		canvas_rect_graph_ = new Rectangle();

		private GraphicsLayer	glayer_main_ = null;
		private GraphicsLayer	glayer_background_ = null;

		private bool			bg_update_request_ = false;

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

            InitGraphValueBuffer();

			axisx_point_ = (uint)prop.Oscillo_DisplayPoint.Value;
        }

		protected override void OnDisposed()
		{
			glayer_main_?.Dispose();
			glayer_background_?.Dispose();
		}

		public override uint PointCount
		{
			get
			{
				return ((uint)ch_values_[0].Length);
			}
		}

		private void InitGraphValueBuffer()
		{
			/* 最大ポイント数でバッファを確保 */
            ch_values_ = new long[Property.ChannelList.Value.Count][];
			for (var index = 0; index < ch_values_.Length; index++) {
				ch_values_[index] = new long[(uint)Property.Oscillo_RecordPoint.Value];
			}
		}

		private void UpdateDisplayParameter()
		{
			/* グラフポイント数 */
			axisx_point_ = Config.DisplayPoint;
			axisx_min_ = Config.DisplayAxisX_Offset;
			axisx_max_ = Config.DisplayAxisX_Offset + axisx_point_;

			/* グラフ描画エリアのサイズ更新 */
			var rect_graph = new Rectangle();

			rect_graph.X = Config.DisplayRect.Left + GRAPH_DETAILS_MARGIN.Left;
			rect_graph.Y = Config.DisplayRect.Top + GRAPH_DETAILS_MARGIN.Top;
			rect_graph.Width = Config.DisplayRect.Width - GRAPH_DETAILS_MARGIN.Horizontal;
			rect_graph.Height = Config.DisplayRect.Height - GRAPH_DETAILS_MARGIN.Vertical;

			/* 描画エリアサイズが更新されている場合はグラフィクスを削除 */
			if (   (!canvas_rect_main_.Equals(Config.DisplayRect))
				|| (!canvas_rect_graph_.Equals(rect_graph))
			) {
				canvas_rect_main_ = Config.DisplayRect;
				canvas_rect_graph_ = rect_graph;

				if (glayer_background_ != null) {
					glayer_background_.Dispose();
					glayer_background_ = null;
				}
			}
		}

		private (long value_min, long value_max) GetChannelValueRange(GraphChannelConfig ch_cfg)
		{
			var point = (uint)GRID_Y_DIV;

			/* DIV単位のポイント数 */
			switch (ch_cfg.OscilloVertRange) {
				case VertRangeType.Preset_100:		point *= 100;									break;
				case VertRangeType.Preset_200:		point *= 200;									break;
				case VertRangeType.Preset_500:		point *= 500;									break;
				case VertRangeType.Preset_10000:	point *= 10000;									break;
				case VertRangeType.Custom:			point *= (uint)ch_cfg.OscilloVertRangeCustom;	break;
				default:							point *= 1;										break;
			}

			return (0, point);
		}

        private void LoadGraphValue(ref long[] ch_value, uint data_index, ref long value)
        {
            var draw_point_offset = ch_values_in_ + data_index;

            if (draw_point_offset >= ch_value.Length) {
                draw_point_offset -= (uint)ch_value.Length;
            }

            value = ch_value[draw_point_offset];
        }

		private void DrawBackground()
		{
			if (bg_update_request_) {
				/* 背景を初期化 */
				glayer_background_.Canvas.FillRectangle(GRAPH_DETAILS_BG_BRUSH, canvas_rect_main_);

				/* グラフの線を描画 */
				DrawBackground_GraphGrid();

				bg_update_request_ = false;
			}

			/* メインバッファにコピー */
			glayer_background_.RenderTo(glayer_main_.Canvas);
		}

        private void DrawBackground_GraphGrid()
        {
            var draw_offset = 0;
            var draw_scale_rect = new Rectangle();
            var draw_scale_format = new StringFormat();

            /* --- X軸 --- */
            draw_scale_rect.Width = canvas_rect_graph_.Width * GRID_X_LINES_MAIN / GRID_X_LINES;
            draw_scale_rect.Height = GRAPH_DETAILS_GRID_SCALE_FONT.Height;
            draw_scale_rect.X = canvas_rect_graph_.Left - draw_scale_rect.Width / 2;
            draw_scale_rect.Y = canvas_rect_graph_.Bottom;

            draw_scale_format.Alignment = StringAlignment.Center;
            draw_scale_format.LineAlignment = StringAlignment.Near;

            for (var index = 0; index <= GRID_X_LINES; index++) {
                draw_offset = canvas_rect_graph_.Left + canvas_rect_graph_.Width * index / GRID_X_LINES;

                /* 目盛(最右を0とする) */
                if ((index % GRID_X_LINES_MAIN) == 0) {
					var str = (Config.DisplayAxisX_Offset + index * (axisx_point_ / GRID_X_LINES) - ch_values_.Length);

                    glayer_background_.Canvas.DrawString(
                        (Config.DisplayAxisX_Offset + index * (axisx_point_ / GRID_X_LINES) - ch_values_.Length).ToString(),
                        GRAPH_DETAILS_GRID_SCALE_FONT,
                        GRAPH_DETAILS_GRID_SCALE_BRUSH,
                        draw_scale_rect,
                        draw_scale_format);

                    draw_scale_rect.X += draw_scale_rect.Width;
                }

                /* 罫線 */
                if ((index > 0) && (index < GRID_X_LINES)) {
                    glayer_background_.Canvas.DrawLine(
                        ((index % 5) == 0) ? (GRAPH_DETAILS_GRID_LINE_MAIN_PEN) : (GRAPH_DETAILS_GRID_LINE_SUB_PEN),
                        draw_offset,
                        canvas_rect_graph_.Top,
                        draw_offset,
                        canvas_rect_graph_.Bottom);
                }
            }

            /* --- Y軸 --- */
            draw_scale_rect.Width = GRAPH_DETAILS_MARGIN.Right;
            draw_scale_rect.Height = canvas_rect_graph_.Height * GRID_Y_LINES_MAIN / GRID_Y_LINES;
            draw_scale_rect.X = canvas_rect_graph_.Right;
            draw_scale_rect.Y = canvas_rect_graph_.Top - draw_scale_rect.Height / 2;

            draw_scale_format.Alignment = StringAlignment.Near;
            draw_scale_format.LineAlignment = StringAlignment.Center;

            for (var index = 0; index <= GRID_Y_LINES; index++) {
                draw_offset = canvas_rect_graph_.Top + canvas_rect_graph_.Height * index / GRID_Y_LINES;

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
                    glayer_background_.Canvas.DrawLine(
                        ((index % 5) == 0) ? (GRAPH_DETAILS_GRID_LINE_MAIN_PEN) : (GRAPH_DETAILS_GRID_LINE_SUB_PEN),
                        canvas_rect_graph_.Left,
                        draw_offset,
                        canvas_rect_graph_.Right,
                        draw_offset);
                }
            }
        }

		private void DrawForeground()
        {
			/* グラフのデータを描画 */
			if (Config.ChannelConfigs != null) {
				for (var ch_index = (uint)0; ch_index < Config.ChannelConfigs.Length; ch_index++) {
					if (ch_index < ch_index_max_) {
						DrawGraph_Data(ch_values_[ch_index], Config.ChannelConfigs[ch_index]);
					}
				}
			}

			/* グラフの枠 */
            glayer_main_.Canvas.DrawRectangle(
				GRAPH_DETAILS_GRID_FRAME_PEN,
				new Rectangle(
					canvas_rect_graph_.X,
					canvas_rect_graph_.Y,
					canvas_rect_graph_.Width - 1,
					canvas_rect_graph_.Height - 1)
			);
        }

        private void DrawGraph_Data(long[] ch_values, GraphChannelConfig ch_cfg)
        {
            var value_y = (long)0;
			var (value_y_min, value_y_max) = GetChannelValueRange(ch_cfg);
            var value_y_canvas = (long)0;

            var value_x_canvas = 0;
            var value_x_canvas_last = -1;

            var value_canvas_x_step  = (double)canvas_rect_graph_.Width / axisx_point_;
            var value_canvas_y_mag = (double)canvas_rect_graph_.Height / value_y_max;

            var draw_pen = new Pen(ch_cfg.ForeColor);
            var draw_points = new Point[(uint)((axisx_max_ - axisx_min_) * value_canvas_x_step)];

			for (var value_x = axisx_min_; value_x < axisx_max_; value_x++) {
                /* 実データを座標データに置き換える */
                value_x_canvas = (int)((value_x - axisx_min_) * value_canvas_x_step);

				/* 直前のデータと同位置の場合は無視 */
                if (value_x_canvas != value_x_canvas_last) {
                    /* 実データを取得 */
                    LoadGraphValue(ref ch_values, value_x, ref value_y);

                    /* 座標データに変換 */
                    value_y_canvas = (long)((value_y_max - value_y) * value_canvas_y_mag) - canvas_rect_graph_.Height / 2;

					/* 表示位置をオフセット補正 */
					if (ch_cfg.OscilloVertOffset != 0) {
						value_y_canvas -= canvas_rect_graph_.Height * ch_cfg.OscilloVertOffset / 100;
					}

                    /* 座標データを補正(ウィンドウ座標はwordサイズ以内としなければ例外が発生) */
                    value_y_canvas = (long)Math.Max(short.MinValue, Math.Min(short.MaxValue, value_y_canvas));

					draw_points[value_x_canvas] = new Point(canvas_rect_graph_.Left + value_x_canvas, canvas_rect_graph_.Top + (int)value_y_canvas);

                    value_x_canvas_last = value_x_canvas;
                }
            }

            glayer_main_.Canvas.DrawLines(draw_pen, draw_points.ToArray());
        }

		protected override void OnDisplayConfigChanged(DisplayConfig config)
		{
			ch_index_max_ = (uint)Math.Min(ch_values_.Length, config.ChannelConfigs.Length);

			UpdateDisplayParameter();
		}

		protected override void OnClearValue()
        {
			InitGraphValueBuffer();
        }

        protected override void OnInputValue(long[] ch_values)
        {
			for (var ch_index = 0; ch_index < ch_values.Length; ch_index++) {
				ch_values_[ch_index][ch_values_in_] = ch_values[ch_index];
			}

			ch_values_in_++;
			ch_values_in_ %= (uint)ch_values_[0].Length;
        }

		protected override void OnDrawDisplay(DisplayContext dc)
		{
            if (   (canvas_rect_main_.Width > 0)
                && (canvas_rect_main_.Height > 0)
            ) {
				glayer_main_ = new GraphicsLayer(dc.Canvas);

                /* レイヤー生成 */
				if (glayer_background_ == null) {
					glayer_background_ = new GraphicsLayer(dc.Canvas, canvas_rect_main_);
					bg_update_request_ = true;
				}

				/* for Debug */
//				Debugger.DebugManager.MessageOut(" DrawBackground Begin");

				DrawBackground();

				/* for Debug */
//				Debugger.DebugManager.MessageOut(" DrawForeground Begin");

				DrawForeground();

				/* for Debug */
//				Debugger.DebugManager.MessageOut(" Draw End");

				glayer_main_.RenderTo(dc.Canvas);
            }
		}
    }
}
