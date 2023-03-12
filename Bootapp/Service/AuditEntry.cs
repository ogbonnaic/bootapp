using System;
using System.Collections.Generic;
using Bootapp.Data.Model;
using Bootapp.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace Bootapp.Service
{
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }
        public EntityEntry Entry { get; }
        public string user_id { get; set; }
        public string table_name { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public AuditType AuditType { get; set; }
        public List<string> ChangedColumns { get; } = new List<string>();
        public audit ToAudit()
        {
            var audit = new audit();
            audit.user_id = user_id;
            audit.type = AuditType.ToString();
            audit.table_name = table_name;
            audit.date_time = DateTime.Now;
            audit.primary_key = JsonConvert.SerializeObject(KeyValues);
            audit.old_values = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues);
            audit.new_values = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);
            audit.affected_columns = ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns);
            return audit;
        }
    }
}
