using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Generic
{
    internal class AppVersion
    {
        private enum VersionId
        {
            Major,
            Minor,
            Bugfix,
            Model,
        }


        public AppVersion(ushort major, ushort minor, ushort bugfix, string model)
        {
            VersionMajor = major;
            VersionMinor = minor;
            VersionBugfix = bugfix;
            VersionModel = model;
        }

        public AppVersion(string ver_text)
        {
            try {
                var blocks = ver_text.Split('.');

                /* Major */
                if (blocks.Length > (int)VersionId.Major) {
                    VersionMajor = ushort.Parse(blocks[(int)VersionId.Major]);
                }

                /* Minor */
                if (blocks.Length > (int)VersionId.Minor) {
                    VersionMinor = ushort.Parse(blocks[(int)VersionId.Minor]);
                }

                /* Bugfix */
                if (blocks.Length > (int)VersionId.Bugfix) {
                    VersionBugfix = ushort.Parse(blocks[(int)VersionId.Bugfix]);
                }

                /* Model */
                if (blocks.Length > (int)VersionId.Model) {
                    VersionModel = blocks[(int)VersionId.Model];
                }
            } catch { }
        }

        public ushort VersionMajor  { get; } = 0;
        public ushort VersionMinor  { get; } = 0;
        public ushort VersionBugfix { get; } = 0;
        public string VersionModel  { get; } = "";

        public override string ToString()
        {
            var str = new StringBuilder();

            str.AppendFormat("{0}.{1}.{2}", VersionMajor, VersionMinor, VersionBugfix);

            if ((VersionModel.Length > 0) && (VersionModel != "0")) {
                str.AppendFormat("({0})", VersionModel);
            }

            return (str.ToString());
        }

        public ulong ToVersionCode()
        {
            return (  ((ulong)VersionMajor << 32)
                    | ((ulong)VersionMinor << 16)
                    | ((ulong)VersionBugfix << 0));
        }

        public bool IsNewVersion(AppVersion ver)
        {
            /* モデルが異なる場合は比較対象から外す */
            if (VersionModel != ver.VersionModel)return (false);

            return (ToVersionCode() > ver.ToVersionCode());
        }
    }
}
