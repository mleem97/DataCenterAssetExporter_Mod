using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AssetExporter
{
    public sealed class Il2CppEventCatalogService
    {
        private static readonly string[] TriggerKeywords =
        {
            "event",
            "trigger",
            "dispatch",
            "invoke",
            "callback",
            "notify",
            "on"
        };

        public string ExportCatalog(string outputDirectory)
        {
            Directory.CreateDirectory(outputDirectory);

            var lines = new List<string>
            {
                "# IL2CPP Event and Trigger Catalog",
                $"timestamp_utc={DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}",
                ""
            };

            int totalEntries = 0;

            totalEntries += AppendRuntimeAssemblyEvents(lines);
            totalEntries += AppendRuntimeTriggerMethods(lines);
            totalEntries += AppendDecompiledCodeHints(lines);

            lines.Insert(2, $"entries={totalEntries}");

            string filePath = Path.Combine(outputDirectory, "il2cpp-event-catalog.txt");
            File.WriteAllLines(filePath, lines);
            return filePath;
        }

        private static int AppendRuntimeAssemblyEvents(List<string> lines)
        {
            lines.Add("## Runtime CLR/IL2CPP Events (Reflection)");
            int count = 0;

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type[] types;
                try
                {
                    types = assembly.GetTypes();
                }
                catch
                {
                    continue;
                }

                foreach (Type type in types)
                {
                    EventInfo[] events;
                    try
                    {
                        events = type.GetEvents(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                    }
                    catch
                    {
                        continue;
                    }

                    foreach (EventInfo evt in events)
                    {
                        lines.Add($"runtime_event | asm={assembly.GetName().Name} | type={type.FullName} | event={evt.Name}");
                        count++;
                    }
                }
            }

            lines.Add(string.Empty);
            return count;
        }

        private static int AppendRuntimeTriggerMethods(List<string> lines)
        {
            lines.Add("## Runtime Trigger-like Methods (Reflection)");
            int count = 0;

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type[] types;
                try
                {
                    types = assembly.GetTypes();
                }
                catch
                {
                    continue;
                }

                foreach (Type type in types)
                {
                    MethodInfo[] methods;
                    try
                    {
                        methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                    }
                    catch
                    {
                        continue;
                    }

                    foreach (MethodInfo method in methods)
                    {
                        string name = method.Name ?? string.Empty;
                        if (!LooksLikeTrigger(name))
                            continue;

                        lines.Add($"runtime_trigger | asm={assembly.GetName().Name} | type={type.FullName} | method={name}");
                        count++;
                    }
                }
            }

            lines.Add(string.Empty);
            return count;
        }

        private static int AppendDecompiledCodeHints(List<string> lines)
        {
            lines.Add("## Decompiled IL2CPP Code Hints");
            int count = 0;

            string decompiledRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "il2cpp-unpack");
            if (!Directory.Exists(decompiledRoot))
            {
                lines.Add("decompiled_source_missing | path=il2cpp-unpack");
                lines.Add(string.Empty);
                return count;
            }

            foreach (string file in Directory.GetFiles(decompiledRoot, "*.cs", SearchOption.AllDirectories))
            {
                string[] content;
                try
                {
                    content = File.ReadAllLines(file);
                }
                catch
                {
                    continue;
                }

                for (int i = 0; i < content.Length; i++)
                {
                    string line = content[i];
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string lineLower = line.ToLowerInvariant();
                    if (lineLower.Contains(" event ")
                        || lineLower.Contains("unityevent")
                        || lineLower.Contains(".invoke(")
                        || lineLower.Contains("trigger")
                        || lineLower.Contains("dispatch")
                        || lineLower.Contains("callback"))
                    {
                        string relative = file.Replace(decompiledRoot + Path.DirectorySeparatorChar, string.Empty);
                        lines.Add($"decompiled_hint | file={relative} | line={i + 1} | text={line.Trim()}");
                        count++;
                    }
                }
            }

            lines.Add(string.Empty);
            return count;
        }

        private static bool LooksLikeTrigger(string methodName)
        {
            string lower = methodName.ToLowerInvariant();
            foreach (string keyword in TriggerKeywords)
            {
                if (lower.Contains(keyword))
                    return true;
            }

            return false;
        }
    }
}
