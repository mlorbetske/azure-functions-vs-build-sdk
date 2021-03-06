﻿using System.IO;
using MakeFunctionJson;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Microsoft.NET.Sdk.Functions.Tasks
{
#if NET46
    [LoadInSeparateAppDomain]
    public class BuildFunctions : AppDomainIsolatedTask
#else
    public class BuildFunctions : Task
#endif
    {
        [Required]
        public string TargetPath { get; set; }

        [Required]
        public string OutputPath { get; set; }

        public bool GenerateHostJson { get; set; }

        public override bool Execute()
        {
            if (GenerateHostJson)
            {
                string hostJsonString = @"{ }";
                File.WriteAllText(Path.Combine(OutputPath, "host.json"), hostJsonString);
            }

            return FunctionJsonConvert.TryConvert(TargetPath, OutputPath, Log);
        }
    }
}

