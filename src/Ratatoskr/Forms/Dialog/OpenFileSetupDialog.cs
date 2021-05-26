using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Debugger;
using Ratatoskr.FileFormat;
using Ratatoskr.General;

namespace Ratatoskr.Forms.Dialog
{
    internal partial class OpenFileSetupDialog : Form
    {
		private enum OpenFileListViewColumnId
		{
			FilePath,
			FormatType,
			FormatOption,
		}

		private class OpenFileListViewParam
		{
			public string Text		= "";
			public Color  BackColor	= Color.White;
		}

        private const int      ITEM_HEIGHT = 40;
        private const int      ITEM_MARGIN_ROUND = 2;
        private const int      ITEM_MARGIN_TEXT  = 5;

        private readonly Font  ITEM_NAME_FONT  = new Font("Meiryo", 9);
        private readonly Brush ITEM_NAME_COLOR = Brushes.Black;

        private readonly Font  ITEM_DETAIL_FONT  = new Font("Meiryo", 8, FontStyle.Italic);
        private readonly Brush ITEM_DETAIL_COLOR = Brushes.Gray;


		private List<FileFormatClass>	file_formats_ = null;
		private List<FileControlParam>	files_ = null;

		private FileFormatClass			select_file_format_ = null;
		private FileFormatOption		select_file_format_option_ = null;
		private FileFormatOptionEditor	select_file_format_option_editor_ = null;

        internal OpenFileSetupDialog()
        {
            InitializeComponent();
			InitializeOpenFileList();

			Btn_FileOrder_Up.Image = Ratatoskr.Resource.Images.arrow_up_16x16;
			Btn_FileOrder_Down.Image = Ratatoskr.Resource.Images.arrow_down_16x16;
        }

        internal OpenFileSetupDialog(IEnumerable<FileFormatClass> formats) : this()
        {
			file_formats_ = formats.ToList();
        }

		private void InitializeOpenFileList()
		{
			var column_configs = new []
			{
				new { id = OpenFileListViewColumnId.FilePath,     text = "Path",          width = 400, },
				new { id = OpenFileListViewColumnId.FormatType,   text = "Format Type",   width = 200, },
				new { id = OpenFileListViewColumnId.FormatOption, text = "Format Option", width = -2,  },
			};

			LView_OpenFileList.BeginUpdate();
			{
				LView_OpenFileList.Clear();



				foreach (var config in column_configs) {
					LView_OpenFileList.Columns.Add(new ColumnHeader()
					{
						Tag = config.id,
						Text = config.text,
						Width = config.width,
					});
				}
			}
			LView_OpenFileList.EndUpdate();
		}

        public IEnumerable<FileFormatClass> FileFormats
		{
			get
			{
				return (file_formats_);
			}
			set
			{
				file_formats_ = value.Where(format => format.CanRead).ToList();

				UpdateFileFormatType();

				UpdateSelectFileFormatView();
			}
		}

		public IEnumerable<FileControlParam> Files
		{
			get
			{
				return (files_);
			}
			set
			{
				files_ = value.ToList();

				UpdateOpenFileListView();

				UpdateSelectFileFormatView();
			}
		}

		public IEnumerable<FileReadController> FileReadControllers
		{
			get
			{
				return (from file in files_
						let controller = FileReadController.Create(file)
						where controller != null
						select controller);
			}
		}

		private void UpdateOpenFileListView()
		{
			LView_OpenFileList.BeginUpdate();
			{
				LView_OpenFileList.Items.Clear();

				if (files_ != null) {
					foreach (var file in files_) {
						var item = new ListViewItem() { Tag = file };

						/* パラメータ更新 */
						UpdateOpenFileListItem(item);

						LView_OpenFileList.Items.Add(item);
					}
				}
			}
			LView_OpenFileList.EndUpdate();
		}

		private void UpdateOpenFileListItem(ListViewItem item)
		{
			var file = item.Tag as FileControlParam;
			var view_param = GetOpenFileListViewParam(file, (OpenFileListViewColumnId)LView_OpenFileList.Columns[0].Tag);

			/* Column 0 */
			item.Text = view_param.Text;
			item.BackColor = view_param.BackColor;

			/* Column 1...Last */
			ListViewItem.ListViewSubItem sub_item;

			for (var index = 1; index < LView_OpenFileList.Columns.Count; index++) {
				view_param = GetOpenFileListViewParam(file, (OpenFileListViewColumnId)LView_OpenFileList.Columns[index].Tag);

				if (index >= item.SubItems.Count) {
					item.SubItems.Add(sub_item = new ListViewItem.ListViewSubItem());
				} else {
					sub_item = item.SubItems[index];
				}

				sub_item.Text = view_param.Text;
			}
		}

		/* 見た目のみを更新する */
		private void RedrawOpenFileList()
		{
			LView_OpenFileList.BeginUpdate();
			{
				foreach (ListViewItem item in LView_OpenFileList.Items) {
					UpdateOpenFileListItem(item);
				}
			}
			LView_OpenFileList.EndUpdate();
		}

		private void NormalizeOpenFileList()
		{
			if (files_ != null) {
				foreach (var file in files_) {
					/* 選択中のフォーマットがフォーマット一覧に無ければフォーマット未選択とする */
					if (!file_formats_.Contains(file.Format)) {
						file.Format = null;
					}
				}
			}

			RedrawOpenFileList();
		}

		private void UpdateFileFormatType()
		{
			CBox_FileFormatType.BeginUpdate();
			{
				CBox_FileFormatType.Items.Clear();
				if (file_formats_ != null) {
					foreach (var format in file_formats_) {
						CBox_FileFormatType.Items.Add(format);
					}
				}
			}
			CBox_FileFormatType.EndUpdate();

			/* ファイルのフォーマットを調整 */
			NormalizeOpenFileList();
		}

		private void UpdateSelectFileFormatView()
		{
			int format_type = -1;		// -1: 未選択, 0>=: Format Index

			/* === 選択中ファイルのフォーマットを取得 === */
			if (LView_OpenFileList.SelectedItems.Count > 0) {
				var format_type_init = false;
				int format_type_item;

				foreach (ListViewItem item in LView_OpenFileList.SelectedItems) {
					if (item.Tag is FileControlParam file) {
						/* ファイルフォーマットのリスト番号を取得 */
						format_type_item = GetFileFormatIndex(file.Format);

						/* 総合ファイルフォーマットが未初期化であれば最初のファイルフォーマットで初期化 */
						if (!format_type_init) {
							format_type_init = true;
							format_type = format_type_item;
						}

						/* 総合ファイルフォーマットと現在のファイルフォーマットが異なる場合は未知フォーマットとする */
						if (format_type != format_type_item) {
							format_type = -1;
						}

						/* 未知フォーマットを検出した場合は走査終了 */
						if (format_type == -1) {
							break;
						}
					}
				}
			}

			/* フォーマットを設定 */
			CBox_FileFormatType.SelectedIndex = format_type;

			GBox_OpenFileSetting.Enabled = (LView_OpenFileList.SelectedItems.Count > 0);
		}

		private void UpdateSelectFileFormatOptionView()
		{
			/* 選択中フォーマットを取得 */
			var format = CBox_FileFormatType.SelectedItem as FileFormatClass;

			/* フォーマット変更時はオプションを初期化 */
			if (select_file_format_ != format) {
				select_file_format_ = format;
				select_file_format_option_ = null;
				select_file_format_option_editor_ = null;

				/* オプションエディタクリア */
				GBox_FileFormatOption.Controls.Clear();

				/* オプション初期化 */
				if (select_file_format_ != null) {
					select_file_format_option_ = select_file_format_.CreateReaderOption();

					/* オプションエディタ初期化 */
					if (select_file_format_option_ != null) {
						select_file_format_option_editor_ = select_file_format_option_.GetEditor();

						/* オプションエディタを設定 */
						if (select_file_format_option_editor_ != null) {
							select_file_format_option_editor_.Dock = DockStyle.Fill;
							GBox_FileFormatOption.Controls.Add(select_file_format_option_editor_);
						}
					}
				}
			}

			/* オプションエディタに設定値を表示 */
			if (select_file_format_option_editor_ != null) {
				select_file_format_option_editor_.LoadOption(select_file_format_option_);
			}
		}

		private void ApplySelectFileFormatOption()
		{
			/* 表示中のオプション値を保存 */
			if (select_file_format_option_editor_ != null) {
				select_file_format_option_editor_.BackupOption(select_file_format_option_);
			}

			/* 選択中のファイルに設定を適用 */
			foreach (ListViewItem item in LView_OpenFileList.SelectedItems) {
				if (item.Tag is FileControlParam file) {
					file.Format = select_file_format_;
					file.Option = (select_file_format_option_ != null) ? (ClassUtil.Clone(select_file_format_option_)) : (null);

					UpdateOpenFileListItem(item);
				}
			}

			LView_OpenFileList.Update();
		}

		private OpenFileListViewParam GetOpenFileListViewParam(FileControlParam file, OpenFileListViewColumnId id)
		{
			var param = new OpenFileListViewParam();

			switch (id) {
				case OpenFileListViewColumnId.FilePath:		param.Text = file.FilePath;															break;
				case OpenFileListViewColumnId.FormatType:	param.Text = ((file.Format != null) ? (file.Format.ToString()) : ("(No select)"));	break;
				case OpenFileListViewColumnId.FormatOption:	param.Text = ((file.Option != null) ? (file.Option.ToString()) : ("-"));			break;
			}

			switch (file.Format) {
				case UserConfigFormatClass format_i:	param.BackColor = Label_OpenListView_Color_UserConfig.BackColor;	break;
				case PacketLogFormatClass format_i:		param.BackColor = Label_OpenListView_Color_PacketLog.BackColor;		break;
			}

			return (param);
		}

		private int GetOpenFileInsertIndex(Point pos)
		{
			var insert_index = LView_OpenFileList.InsertionMark.NearestIndex(pos);

			if (LView_OpenFileList.Items.Count > 0) {
				var last_item_rect = LView_OpenFileList.GetItemRect(LView_OpenFileList.Items.Count - 1);

				if (pos.Y > last_item_rect.Bottom) {
					insert_index = LView_OpenFileList.Items.Count;
				}
			}

			return (insert_index);
		}

		private int GetFileFormatIndex(FileFormatClass format)
		{
			return (file_formats_.IndexOf(format));
		}

		private void Btn_Ok_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void Btn_Cancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void Btn_FileOrder_Up_Click(object sender, EventArgs e)
		{

		}

		private void Btn_FileOrder_Down_Click(object sender, EventArgs e)
		{

		}

		private void LView_OpenFileList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			UpdateSelectFileFormatView();
		}

		private void LView_OpenFileList_ColumnClick(object sender, ColumnClickEventArgs e)
		{

		}

		private void CBox_FileFormatType_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateSelectFileFormatOptionView();
		}

		private void Btn_OpenFileSetting_Apply_Click(object sender, EventArgs e)
		{
			ApplySelectFileFormatOption();
		}

		private void LView_OpenFileList_ItemDrag(object sender, ItemDragEventArgs e)
		{
			DebugManager.MessageOut("ItemDrag");

			/* D&D対象ファイルを取得 */
			if (LView_OpenFileList.SelectedIndices.Count > 0) {
				var select_files = new int[LView_OpenFileList.SelectedIndices.Count];

				LView_OpenFileList.SelectedIndices.CopyTo(select_files, 0);

				LView_OpenFileList.DoDragDrop(select_files, DragDropEffects.Move);
			}
		}

		private void LView_OpenFileList_DragEnter(object sender, DragEventArgs e)
		{
			DebugManager.MessageOut("DragEnter");

			e.Effect = e.AllowedEffect;
		}

		private void LView_OpenFileList_DragDrop(object sender, DragEventArgs e)
		{
			DebugManager.MessageOut("DragDrop");

			var target_indices = (int[])e.Data.GetData(typeof(int[]));
			var target_files = new List<FileControlParam>();

			foreach (var target_index in target_indices) {
				if (LView_OpenFileList.Items[target_index].Tag is FileControlParam file) {
					target_files.Add(file);
				}
			}

			var insert_index = GetOpenFileInsertIndex(LView_OpenFileList.PointToClient(new Point(e.X, e.Y)));

			files_.InsertRange(insert_index, target_files);

			foreach (var remove_index in target_indices.Reverse()) {
				if (remove_index > insert_index) {
					files_.RemoveAt(remove_index + target_files.Count);
				}
			}

			LView_OpenFileList.InsertionMark.Index = -1;
		}

		private void LView_OpenFileList_DragLeave(object sender, EventArgs e)
		{
			DebugManager.MessageOut("DragLeave");

		}

		private void LView_OpenFileList_DragOver(object sender, DragEventArgs e)
		{
			DebugManager.MessageOut("DragOver");

//			e.Effect = DragDropEffects.Move;

			var insert_index = GetOpenFileInsertIndex(LView_OpenFileList.PointToClient(new Point(e.X, e.Y)));

			if (insert_index >= LView_OpenFileList.Items.Count) {
				LView_OpenFileList.InsertionMark.AppearsAfterItem = true;
				insert_index = Math.Max(insert_index - 1, 0);
			} else {
				LView_OpenFileList.InsertionMark.AppearsAfterItem = false;
			}

			LView_OpenFileList.InsertionMark.Index = insert_index;
		}
	}
}
