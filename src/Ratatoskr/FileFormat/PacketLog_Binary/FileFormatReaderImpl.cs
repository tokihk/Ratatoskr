using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General;
using Ratatoskr.General.BinaryText;
using Ratatoskr.General.Packet;

namespace Ratatoskr.FileFormat.PacketLog_Binary
{
    internal sealed class FileFormatReaderImpl : PacketLogReader
    {
        private BinaryReader               reader_ = null;
        private FileFormatReaderOptionImpl option_;

		private string		file_path_;

        private string		packet_alias_;

        private DateTime	packet_datetime_ = DateTime.MinValue;

		private byte[]		terminate_pattern_ = null;

        
        public FileFormatReaderImpl(FileFormatClass fmtc) : base(fmtc)
        {
        }

        protected override bool OnOpenPath(FileFormatOption option, string path)
        {
			file_path_ = path;

            packet_alias_ = Path.GetFileName(Path.GetDirectoryName(path));

            return base.OnOpenPath(option, path);
        }

        protected override bool OnOpenStream(FileFormatOption option, Stream stream)
        {
            reader_ = new BinaryReader(stream);
            option_ = option as FileFormatReaderOptionImpl;

            if (option_ == null)return (false);

			/* Packet - Alias */
			switch (option_.PacketAliasMode) {
				case PacketAliasModeType.FileName:
					packet_alias_ = Path.GetFileName(file_path_);
					break;
				case PacketAliasModeType.ParentDirectoryName:
					packet_alias_ = Path.GetFileName(Path.GetDirectoryName(file_path_));
					break;
				case PacketAliasModeType.Custom:
					packet_alias_ = option_.PacketCustomAlias;
					break;
			}

			/* Packet - Datetime */
			packet_datetime_ = option_.PacketBaseTime;

			/* 終端パターン */
			terminate_pattern_ = BinaryTextCompiler.Build(option_.DataTerminatePattern);

            return (true);
        }

        protected override PacketObject OnReadPacket()
        {
            var packet = (PacketObject)null;

            while ((reader_.BaseStream.Position < reader_.BaseStream.Length) && (packet == null)) {
				var packet_data = (byte[])null;

				if (terminate_pattern_ != null) {
					/* 終端パターンが設定されているときは1バイトずつ読み込む */
					var packet_data_work = new byte[option_.DataMaxSize];
					var packet_data_size = 0;
					var match_pos = 0;
					var match_size = 0;

					/* 終端パターンを検出するか、DataMaxSizeのデータが集まるまでループ */
					while ((reader_.BaseStream.Position < reader_.BaseStream.Length) && (packet_data_size < packet_data_work.Length)) {
						packet_data_work[packet_data_size++] = reader_.ReadByte();

						/* 終端パターンを検索 */
						if (packet_data_size >= terminate_pattern_.Length) {
							match_pos = packet_data_size - terminate_pattern_.Length;
							match_size = 0;

							/* データの最後尾と終端パターンを比較 */
							while (match_size < terminate_pattern_.Length) {
								if (packet_data_work[match_pos + match_size] != terminate_pattern_[match_size]) {
									break;
								}
								match_size++;
							}

							/* 終端パターンを検出した場合はデータ取得終了 */
							if (match_size >= terminate_pattern_.Length) {
								break;
							}
						}
					}

					/* 集めたデータをパケットデータに設定 */
					if (packet_data_size < packet_data_work.Length) {
						packet_data = ClassUtil.CloneCopy(packet_data_work, packet_data_size);
					} else {
						packet_data = packet_data_work;
					}

				} else {
					/* 終端パターンが設定されていないときはブロック単位で読み込む */
					packet_data = reader_.ReadBytes((int)Math.Min(option_.DataMaxSize, reader_.BaseStream.Length - reader_.BaseStream.Position));
				}

				/* パケット構築 */
				packet = BuildPacket(packet_data);
			}

            return (packet);
        }

		private PacketObject BuildPacket(byte[] packet_data)
		{
			var packet = new PacketObject(
				"Binary",
				PacketFacility.External,
				packet_alias_,
				PacketPriority.Standard,
				PacketAttribute.Data,
				packet_datetime_,
				"",
				PacketDirection.Recv,
				"",
				"",
				0,
				null,
				packet_data);

			packet_datetime_ = packet_datetime_.AddTicks(option_.PacketIntervalUsec * 10);

			return (packet);
		}
    }
}
