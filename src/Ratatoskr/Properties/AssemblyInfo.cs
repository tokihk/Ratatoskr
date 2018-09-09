using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

// アセンブリに関する一般情報は以下の属性セットをとおして制御されます。
// アセンブリに関連付けられている情報を変更するには、
// これらの属性値を変更してください。
[assembly: AssemblyTitle("Ratatoskr")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Ratatoskr")]
[assembly: AssemblyCopyright("Copyright © 2018 Toki.H.K")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// ComVisible を false に設定すると、その型はこのアセンブリ内で COM コンポーネントから 
// 参照不可能になります。COM からこのアセンブリ内の型にアクセスする場合は、 
// その型の ComVisible 属性を true に設定してください。
[assembly: ComVisible(false)]

// このプロジェクトが COM に公開される場合、次の GUID が typelib の ID になります
[assembly: Guid("577191D4-A8E0-409D-947D-BA57BEDD3E68")]

// アセンブリのバージョン情報は次の 4 つの値で構成されています:
//
//      メジャー バージョン
//      マイナー バージョン
//      ビルド番号
//      Revision
//
[assembly: AssemblyVersion("0.5.0.0")]
[assembly: AssemblyFileVersion("0.5.0.0")]

internal static class AppInfo
{
    public static string Name
    {
        get {
            return (((System.Reflection.AssemblyTitleAttribute)Attribute.GetCustomAttribute(
                        System.Reflection.Assembly.GetExecutingAssembly(), 
                        typeof(System.Reflection.AssemblyTitleAttribute))
                    ).Title);
        }
    }

    public static string Copyright
    {
        get {
            return (((System.Reflection.AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(
                        System.Reflection.Assembly.GetExecutingAssembly(), 
                        typeof(System.Reflection.AssemblyCopyrightAttribute))
                    ).Copyright);
        }
    }

    public static string Version
    {
        get {
            return (((System.Reflection.AssemblyFileVersionAttribute)Attribute.GetCustomAttribute(
                        System.Reflection.Assembly.GetExecutingAssembly(), 
                        typeof(System.Reflection.AssemblyFileVersionAttribute))
                    ).Version);
        }
    }
}
