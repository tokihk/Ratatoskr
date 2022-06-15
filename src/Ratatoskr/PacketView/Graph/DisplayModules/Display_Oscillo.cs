using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.General;

namespace Ratatoskr.PacketView.Graph.DisplayModules
{
    internal class Display_Oscillo : DisplayModule
    {
		private class GraphicsLayer : IDisposable
		{
			public GraphicsLayer(Graphics graphics, Rectangle rect)
			{
				Manager = BufferedGraphicsManager.Current.Allocate(graphics, rect);
				Canvas = Manager.Graphics;
			}

			public BufferedGraphics Manager		{ get; private set; } = null;
			public Graphics			Canvas		{ get; private set; } = null;


			public void Dispose()
			{
				Manager?.Dispose();
				Manager = null;
				Canvas = null;
			}

			public void RenderTo(Graphics graphics)
			{
				if ((Manager != null) && (Manager.Graphics != null)) {
					Manager.Render(graphics);
				}
			}

			public void RenderTo(GraphicsLayer glayer)
			{
				if ((Manager != null) && (Manager.Graphics != null)) {
					Manager?.Render(glayer.Canvas);
				}
			}
		}

		private class DrawChannelData
		{
			public uint					ChannelNo;
			public GraphChannelConfig	ChannelConfig;

			public Pen					GraphPen;

			public long					AxisY_ValueMax;
			public long[]				AxisY_Values;
			public double				AxisY_CanvasStep;
			public long					AxisY_CanvasOffset;

			public Point[]				DrawPoints;
		}

        private readonly Padding GRAPH_DETAILS_MARGIN = new Padding(50, 50, 100, 100);

		private readonly Size    CHANNEL_INFO_SIZE   = new Size(120, 25);
		private readonly Padding CHANNEL_INFO_MARGIN = new Padding(0, 20, 10, 0);

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

		private readonly Font			GRAPH_CH_INFO_FONT;
		private readonly Brush			GRAPH_CH_INFO_BRUSH;
		private readonly Pen			GRAPH_CH_INFO_PEN;
		private readonly StringFormat	GRAPH_CH_INFO_SFORMAT;


		private uint				graph_record_num_ = 0;
		private uint				graph_display_num_ = 0;

        private long[][]			ch_values_;				// points_[channel][record_point]
        private uint				ch_values_in_ = 0;

		private uint				ch_index_max_ = 0;

		private DrawChannelData[]	draw_ch_data_ = null;

		private Rectangle		canvas_rect_main_ = new Rectangle();
		private Rectangle		canvas_rect_graph_ = new Rectangle();
		private Rectangle		canvas_rect_ch_info_ = new Rectangle();

		private GraphicsLayer	glayer_main_ = null;
		private GraphicsLayer	glayer_main_background_ = null;

		private bool			update_canvas_ = false;

		private uint		axisx_value_min_;
		private uint		axisx_value_max_;
		private uint		axisx_point_num_;
		private double		axisx_canvas_step_;

		private uint[]		axisx_value_indexes_;
		private int[]		axisx_canvas_values_;

#if DEBUG
		private System.Diagnostics.Stopwatch	fps_timer_ = new System.Diagnostics.Stopwatch();
		private uint							fps_counter_;
#endif


		public Display_Oscillo() : base()
		{
			GRAPH_DETAILS_BG_BRUSH = new SolidBrush(Color.FromArgb(0, 24, 0));

			GRAPH_DETAILS_GRID_SCALE_FONT = new Font("MS Gothic", 8);
			GRAPH_DETAILS_GRID_SCALE_BRUSH = new SolidBrush(Color.FromArgb(80, 80, 80));

			GRAPH_DETAILS_GRID_FRAME_PEN = new Pen(Color.FromArgb(90, 90, 90));

			GRAPH_DETAILS_GRID_LINE_MAIN_PEN = new Pen(Color.FromArgb(70, 70, 70));
			GRAPH_DETAILS_GRID_LINE_MAIN_PEN.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

			GRAPH_DETAILS_GRID_LINE_SUB_PEN = new Pen(Color.FromArgb(40, 40, 40));
			GRAPH_DETAILS_GRID_LINE_SUB_PEN.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

			GRAPH_CH_INFO_FONT = new Font("Meiryo", 9);
			GRAPH_CH_INFO_BRUSH = new SolidBrush(Color.FromArgb(80, 80, 80));
			GRAPH_CH_INFO_PEN  = new Pen(Color.FromArgb(90, 90, 90));
			GRAPH_CH_INFO_SFORMAT = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };

			ResetGraphRecordBuffer();
		}

		protected override void OnDisposed()
		{
			CloseGraphicsLayer();
		}

		public override uint PointCount
		{
			get
			{
				return (graph_record_num_);
			}
		}

		private void ResetGraphRecordBuffer()
		{
			/* 最大ポイント数でバッファを確保 */
            ch_values_ = new long[graph_record_num_ + 1][];
			for (var index = 0; index < ch_values_.Length; index++) {
				ch_values_[index] = new long[(uint)graph_record_num_ + 1];
			}
		}

		private void OpenGraphicsLayer(Graphics graphics)
		{
			if (glayer_main_ == null) {
				glayer_main_ = new GraphicsLayer(graphics, canvas_rect_main_);
				update_canvas_ = true;
			}

			if (glayer_main_background_ == null) {
				glayer_main_background_ = new GraphicsLayer(graphics, canvas_rect_main_);
				update_canvas_ = true;
			}
		}

		private void CloseGraphicsLayer()
		{
			if (glayer_main_ != null) {
				glayer_main_.Dispose();
				glayer_main_ = null;
			}

			if (glayer_main_background_ != null) {
				glayer_main_background_.Dispose();
				glayer_main_background_ = null;
			}
		}

		protected override void OnDisplayConfigUpdated(DisplayConfig config)
		{
			/* グラフ描画エリアのサイズ更新 */
			if (canvas_rect_main_ != config.DisplayRect) {
				canvas_rect_main_ = config.DisplayRect;

				canvas_rect_graph_.X = canvas_rect_main_.Left + GRAPH_DETAILS_MARGIN.Left;
				canvas_rect_graph_.Y = canvas_rect_main_.Top + GRAPH_DETAILS_MARGIN.Top;
				canvas_rect_graph_.Width = canvas_rect_main_.Width - GRAPH_DETAILS_MARGIN.Horizontal;
				canvas_rect_graph_.Height = canvas_rect_main_.Height - GRAPH_DETAILS_MARGIN.Vertical;

				canvas_rect_ch_info_.X = canvas_rect_graph_.X;
				canvas_rect_ch_info_.Y = canvas_rect_graph_.Bottom + CHANNEL_INFO_MARGIN.Top;
				canvas_rect_ch_info_.Width = canvas_rect_graph_.Width;
				canvas_rect_ch_info_.Height = canvas_rect_main_.Bottom - canvas_rect_graph_.Y;

				CloseGraphicsLayer();
			}

			/* 記録ポイント数の更新 */
			if (graph_record_num_ != config.GraphRecordPoint) {
				graph_record_num_ = config.GraphRecordPoint;
				ResetGraphRecordBuffer();
			}

			/* グラフポイント数 */
			axisx_point_num_ = config.GraphDisplayPoint;
			axisx_value_min_ =  config.GraphDisplayOffset;
			axisx_value_max_ = config.GraphDisplayOffset + axisx_point_num_;
			axisx_canvas_step_ = (double)canvas_rect_graph_.Width / axisx_point_num_;

			/* 表示ポイント数を算出 */
			var axisx_canvas_value = 0;
			var axisx_canvas_value_last = -1;
			var axisx_canvas_value_list = new List<int>();
			var axisx_value_index_list = new List<uint>();

			for (var value_x = (uint)0; value_x <= axisx_point_num_; value_x++) {
				/* データ番号を座標データに置き換える */
				axisx_canvas_value = (int)(value_x * axisx_canvas_step_);

				/* 既に描画済みの座標と異なる場合に描画対象とする */
				if (axisx_canvas_value_last != axisx_canvas_value) {
					axisx_canvas_value_list.Add(axisx_canvas_value); ;
					axisx_value_index_list.Add(value_x + axisx_value_min_);
					axisx_canvas_value_last = axisx_canvas_value;
				}
			}
			axisx_canvas_values_ = axisx_canvas_value_list.ToArray();
			axisx_value_indexes_ = axisx_value_index_list.ToArray();

			/* 表示チャンネル情報を初期化 */
			var draw_ch_data_list = new List<DrawChannelData>();
			var ch_config = (GraphChannelConfig)null;
			var axisy_value_max = (long)0;

			ch_index_max_ = (uint)((config.GraphChannelConfigs != null) ? (Math.Min(ch_values_.Length, config.GraphChannelConfigs.Length)) : (0));

			for (var ch_index = (uint)0; ch_index < ch_index_max_; ch_index++) {
				ch_config = config.GraphChannelConfigs[ch_index];

				if (!ch_config.Visible) continue;

				axisy_value_max = GetChannelValueMax(ch_config);

				draw_ch_data_list.Add(new DrawChannelData()
				{
					ChannelNo = ch_index + 1,
					ChannelConfig = ch_config,
					GraphPen = new Pen(ch_config.ForeColor),
					AxisY_ValueMax = axisy_value_max,
					AxisY_Values = ch_values_[ch_index],
					AxisY_CanvasStep = (double)canvas_rect_graph_.Height / axisy_value_max,
					AxisY_CanvasOffset = (ch_config.OscilloVertOffset == 0) ? (0) : (canvas_rect_graph_.Height * ch_config.OscilloVertOffset / 100) + canvas_rect_graph_.Height / 2,
					DrawPoints = new Point[axisx_value_indexes_.Length]
				});
			}
			draw_ch_data_ = draw_ch_data_list.ToArray();
		}

		protected override void OnDisplayResized(Rectangle disp_rect)
		{
			/* グラフ描画エリアのサイズ更新 */
			var rect_graph = new Rectangle();

			rect_graph.X = disp_rect.Left + GRAPH_DETAILS_MARGIN.Left;
			rect_graph.Y = disp_rect.Top + GRAPH_DETAILS_MARGIN.Top;
			rect_graph.Width = disp_rect.Width - GRAPH_DETAILS_MARGIN.Horizontal;
			rect_graph.Height = disp_rect.Height - GRAPH_DETAILS_MARGIN.Vertical;

			canvas_rect_main_ = disp_rect;
			canvas_rect_graph_ = rect_graph;

			canvas_rect_ch_info_.X = canvas_rect_graph_.X;
			canvas_rect_ch_info_.Y = canvas_rect_graph_.Bottom + CHANNEL_INFO_MARGIN.Top;
			canvas_rect_ch_info_.Width = canvas_rect_graph_.Width;
			canvas_rect_ch_info_.Height = canvas_rect_main_.Bottom - canvas_rect_graph_.Y;
		}


		private long GetChannelValueMax(GraphChannelConfig ch_cfg)
		{
			var point = (long)GRID_Y_DIV;

			/* DIV単位のポイント数 */
			switch (ch_cfg.OscilloVertRange) {
				case VertRangeType.Preset_8bit_30DIV:		point *= 30;									break;
				case VertRangeType.Preset_16bit_8kDIV:		point *= 8000;									break;
				case VertRangeType.Preset_32bit_700MDIV:	point *= 700000000;								break;
				case VertRangeType.Preset_32bit_1000MDIV:	point *= 1000000000;							break;
				case VertRangeType.Custom:					point *= (uint)ch_cfg.OscilloVertRangeCustom;	break;
				default:									point *= 1;										break;
			}

			return (point);
		}

		private void DrawDisplayProc()
		{
#if DEBUG
			/* FPS */
			if ((!fps_timer_.IsRunning) || (fps_timer_.ElapsedMilliseconds >= 1000)) {
				Debugger.DebugManager.StatusOut("Oscillo FPS", fps_counter_);
				fps_counter_ = 0;
				fps_timer_.Restart();
			}
			fps_counter_++;
#endif

			/* for Debug */
			var sw = new System.Diagnostics.Stopwatch();
			sw.Start();

			/* 背景描画(キャッシュ有り) */
			DrawBackground();

			/* グラフのデータを描画(キャッシュ無し) */
			DrawGraph();

			/* グラフの前面描画(キャッシュ無し) */
			DrawForeground();

			update_canvas_ = false;

			/* for Debug */
			Debugger.DebugManager.StatusOut("DrawDisplayProc", sw.ElapsedTicks / 10);
		}

		private void DrawBackground()
		{
			if (update_canvas_) {
				/* 背景を初期化 */
				glayer_main_background_.Canvas.FillRectangle(GRAPH_DETAILS_BG_BRUSH, canvas_rect_main_);

				/* グラフの線を描画 */
				DrawBackground_GraphGrid();

				DrawBackground_ChannelInfo();
			}

			/* メインバッファにコピー */
			glayer_main_background_.RenderTo(glayer_main_.Canvas);
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

#if false
				/* 目盛(最右を0とする) */
				if ((index % GRID_X_LINES_MAIN) == 0) {
					var str = (axisx_value_min_ + index * (axisx_point_num_ / GRID_X_LINES) - ch_values_.Length);

                    glayer_main_background_.Canvas.DrawString(
                        (axisx_value_min_ + index * (axisx_point_num_ / GRID_X_LINES) - ch_values_.Length).ToString(),
                        GRAPH_DETAILS_GRID_SCALE_FONT,
                        GRAPH_DETAILS_GRID_SCALE_BRUSH,
                        draw_scale_rect,
                        draw_scale_format);

                    draw_scale_rect.X += draw_scale_rect.Width;
                }
#endif

                /* 罫線 */
                if ((index > 0) && (index < GRID_X_LINES)) {
                    glayer_main_background_.Canvas.DrawLine(
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
                    glayer_main_background_.Canvas.DrawLine(
                        ((index % 5) == 0) ? (GRAPH_DETAILS_GRID_LINE_MAIN_PEN) : (GRAPH_DETAILS_GRID_LINE_SUB_PEN),
                        canvas_rect_graph_.Left,
                        draw_offset,
                        canvas_rect_graph_.Right,
                        draw_offset);
                }
            }
        }

        private void DrawBackground_ChannelInfo()
		{
            var draw_rect = new Rectangle();

			draw_rect.X = canvas_rect_ch_info_.X;
			draw_rect.Y = canvas_rect_ch_info_.Y;
			draw_rect.Width = CHANNEL_INFO_SIZE.Width;
			draw_rect.Height = CHANNEL_INFO_SIZE.Height;

			foreach (var ch_data in draw_ch_data_) {
				glayer_main_background_.Canvas.FillRectangle(new SolidBrush(ch_data.ChannelConfig.ForeColor), draw_rect);

				glayer_main_background_.Canvas.DrawRectangle(GRAPH_CH_INFO_PEN, draw_rect);

				glayer_main_background_.Canvas.DrawString(
					String.Format(" CH{0}  {1}/DIV", ch_data.ChannelNo, TextUtil.DecToText((ulong)ch_data.AxisY_ValueMax / 10, 0)),
					GRAPH_CH_INFO_FONT,
					GRAPH_CH_INFO_BRUSH,
					draw_rect,
					GRAPH_CH_INFO_SFORMAT);

				draw_rect.X = draw_rect.Right + CHANNEL_INFO_MARGIN.Horizontal;
			}
		}

		private void DrawForeground()
        {
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

        private unsafe void DrawGraph()
        {
			if (draw_ch_data_ == null)return;

            var axisy_value = (long)0;
            var axisy_canvas_value = (long)0;
			var draw_points = new Point[axisx_value_indexes_.Length];
			var ch_value_in = ch_values_in_;

			/* 描画領域を設定 */
			glayer_main_.Canvas.SetClip(canvas_rect_graph_);
			glayer_main_.Canvas.TranslateTransform(canvas_rect_graph_.Left, canvas_rect_graph_.Top);

			fixed (int* paxisx_canvas_values = axisx_canvas_values_)
			fixed (Point* pdraw_points = draw_points)
			{
				foreach (var ch_data in draw_ch_data_) {
					int *		paxisx_canvas_values_work = paxisx_canvas_values;
					Point *		pdraw_points_work = pdraw_points;

					foreach (var axisx_value_index in axisx_value_indexes_) {
						/* 実データを取得 */
						axisy_value = ch_data.AxisY_Values[(ch_value_in + axisx_value_index) % ch_data.AxisY_Values.Length];

						/* 座標データに変換 */
						axisy_canvas_value = (long)(axisy_value * ch_data.AxisY_CanvasStep);

						/* 表示位置をオフセット補正 */
						axisy_canvas_value -= ch_data.AxisY_CanvasOffset;

						/* 座標データを補正(ウィンドウ座標はwordサイズ以内としなければ例外が発生) */
						if (axisy_canvas_value < short.MinValue) {
							axisy_canvas_value = short.MinValue;
						} else if (axisy_canvas_value > short.MaxValue) {
							axisy_canvas_value = short.MaxValue;
						}

						/* 座標データを登録 */
						pdraw_points_work->X = *paxisx_canvas_values_work++;
						pdraw_points_work->Y = (int)axisy_canvas_value;
						pdraw_points_work++;
					}

					glayer_main_.Canvas.DrawLines(ch_data.GraphPen, draw_points);
				}
			}

			glayer_main_.Canvas.ResetClip();
			glayer_main_.Canvas.ResetTransform();
        }

		protected override void OnClearValue()
		{
			ResetGraphRecordBuffer();
		}

        protected override unsafe void OnInputValue(long[] ch_values)
        {
			fixed (long *pch_values = ch_values)
			{
				long *pch_values_work = pch_values;

				for (var ch_index = 0; ch_index < ch_values.Length; ch_index++) {
					ch_values_[ch_index][ch_values_in_] = *pch_values_work++;
				}

				if (ch_values_in_ < (uint)(ch_values_[0].Length - 1)) {
					ch_values_in_++;
				} else {
					ch_values_in_ = 0;
				}
			}
        }

		protected override void OnDrawDisplay(DisplayContext dc)
		{
			/* for Debug */
			var sw = new System.Diagnostics.Stopwatch();
			sw.Start();

			/* メモリキャンバス準備 */
			OpenGraphicsLayer(dc.Canvas);

			/* 画面生成 */
			DrawDisplayProc();

			/* メモリキャンバスをメインキャンバスへコピー */
			glayer_main_.RenderTo(dc.Canvas);

			/* for Debug */
			Debugger.DebugManager.StatusOut("DrawDisplay", sw.ElapsedTicks / 10);
		}
    }
}
