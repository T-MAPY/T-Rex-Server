﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TRexServer.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string AccessKey {
            get {
                return ((string)(this["AccessKey"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"[
  {
    ""ConnectionString"": ""DATA SOURCE=\""(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=david.ostrava.tmapy.cz)(PORT=1521))(CONNECT_DATA=(SID=david)))\"";USER ID=zzspraha;DBA PRIVILEGE=;PASSWORD=heslo"",
    ""TableName"": ""CPC_MU"",
    ""Ids"": [
      ""id1"",
      ""id2"",
      ""id3"",
      ""id4""
    ]
  },
  {
    ""ConnectionString"": ""DATA SOURCE=\""(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=david.ostrava.tmapy.cz)(PORT=1521))(CONNECT_DATA=(SID=david)))\"";USER ID=zzspraha;DBA PRIVILEGE=;PASSWORD=heslo"",
    ""TableName"": ""CPC_MU"",
    ""Ids"": [
      ""id1"",
      ""id2"",
      ""id3"",
      ""id4""
    ]
  }
]")]
        public string Connections {
            get {
                return ((string)(this["Connections"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("DATA SOURCE=\\\"(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=david.ostrava.tmapy.cz)(P" +
            "ORT=1521))(CONNECT_DATA=(SID=david)))\\\";USER ID=zzspraha;DBA PRIVILEGE=;PASSWORD" +
            "=heslo")]
        public string DefaultConnectionString {
            get {
                return ((string)(this["DefaultConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("CPC_MU")]
        public string DefaultTableName {
            get {
                return ((string)(this["DefaultTableName"]));
            }
        }
    }
}
