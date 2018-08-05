using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs.Types;
using Ratatoskr.Configs.SystemConfigs;
using Ratatoskr.Configs.UserConfigs;
using Ratatoskr.Configs.LanguageConfigs;
using Ratatoskr.Generic;

namespace Ratatoskr.Forms.OptionEditForm
{
    internal class OptionDataMap
    {
        public AutoTimeStampTriggerType AutoTimeStampTrigger;
        public decimal                  AutoTimeStampValue_LastRecvPeriod;

        public string              AutoSaveDirectory;
        public string              AutoSavePrefix;
        public AutoPacketSaveFormatType  AutoSaveFormat;
        public AutoPacketSaveTargetType  AutoSaveTarget;
        public AutoPacketSaveTimmingType AutoSaveTimming;
        public decimal             AutoSaveValue_Interval;
        public decimal             AutoSaveValue_FileSize;
        public decimal             AutoSaveValue_PacketCount;

        public decimal RawPacketCountLimit;
        public decimal Packet_ViewPacketCountLimit;

        public List<MailConfig> MailList = new List<MailConfig>();


        public OptionDataMap(SystemConfig sys_conf, UserConfig user_conf, LanguageConfig lang_conf)
        {
            Load(sys_conf, user_conf, lang_conf);
        }

        public void Load(SystemConfig sys_conf, UserConfig user_conf, LanguageConfig lang_conf)
        {
            AutoTimeStampTrigger = sys_conf.AutoTimeStamp.Trigger.Value;
            AutoTimeStampValue_LastRecvPeriod = sys_conf.AutoTimeStamp.Value_LastRecvPeriod.Value;

            AutoSaveDirectory = sys_conf.AutoPacketSave.SaveDirectory.Value;
            AutoSavePrefix = sys_conf.AutoPacketSave.SavePrefix.Value;
            AutoSaveFormat = sys_conf.AutoPacketSave.SaveFormat.Value;
            AutoSaveTarget = sys_conf.AutoPacketSave.SaveTarget.Value;
            AutoSaveTimming = sys_conf.AutoPacketSave.SaveTimming.Value;
            AutoSaveValue_Interval = sys_conf.AutoPacketSave.SaveValue_Interval.Value;
            AutoSaveValue_FileSize = sys_conf.AutoPacketSave.SaveValue_FileSize.Value;
            AutoSaveValue_PacketCount = sys_conf.AutoPacketSave.SaveValue_PacketCount.Value;

            RawPacketCountLimit = sys_conf.ApplicationCore.RawPacketCountLimit.Value;
            Packet_ViewPacketCountLimit = sys_conf.ApplicationCore.Packet_ViewPacketCountLimit.Value;

            MailList.Clear();
            foreach (var config in sys_conf.MailList.Value) {
                MailList.Add(ClassUtil.Clone(config));
            }
        }

        public void Backup(SystemConfig sys_conf, UserConfig user_conf, LanguageConfig lang_conf)
        {
            sys_conf.AutoTimeStamp.Trigger.Value = AutoTimeStampTrigger;
            sys_conf.AutoTimeStamp.Value_LastRecvPeriod.Value = AutoTimeStampValue_LastRecvPeriod;

            sys_conf.AutoPacketSave.SaveDirectory.Value = AutoSaveDirectory;
            sys_conf.AutoPacketSave.SavePrefix.Value = AutoSavePrefix;
            sys_conf.AutoPacketSave.SaveFormat.Value = AutoSaveFormat;
            sys_conf.AutoPacketSave.SaveTarget.Value = AutoSaveTarget;
            sys_conf.AutoPacketSave.SaveTimming.Value = AutoSaveTimming;
            sys_conf.AutoPacketSave.SaveValue_Interval.Value = AutoSaveValue_Interval;
            sys_conf.AutoPacketSave.SaveValue_FileSize.Value = AutoSaveValue_FileSize;
            sys_conf.AutoPacketSave.SaveValue_PacketCount.Value = AutoSaveValue_PacketCount;

            sys_conf.ApplicationCore.RawPacketCountLimit.Value = RawPacketCountLimit;
            sys_conf.ApplicationCore.Packet_ViewPacketCountLimit.Value = Packet_ViewPacketCountLimit;

            sys_conf.MailList.Value.Clear();
            foreach (var config in MailList) {
                sys_conf.MailList.Value.Add(ClassUtil.Clone(config));
            }
        }
    }
}
