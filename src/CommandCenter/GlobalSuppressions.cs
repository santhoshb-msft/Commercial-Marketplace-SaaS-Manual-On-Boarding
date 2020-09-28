// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "By design", Scope = "member", Target = "~P:CommandCenter.Models.AzureSubscriptionProvisionModel.AvailablePlans")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "By design", Scope = "member", Target = "~P:CommandCenter.Models.UpdateSubscriptionViewModel.AvailablePlans")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "By design", Scope = "member", Target = "~P:CommandCenter.Models.OperationsViewModel.Operations")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "By design", Scope = "member", Target = "~M:CommandCenter.Startup.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder,Microsoft.AspNetCore.Hosting.IWebHostEnvironment)")]
[assembly: SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "By design", Scope = "member", Target = "~M:CommandCenter.StringExtensions.GetDomainNameFromEmail(System.String)~System.String")]
[assembly: SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "By design", Scope = "member", Target = "~M:CommandCenter.Controllers.LandingPageController.Index(CommandCenter.Models.AzureSubscriptionProvisionModel,System.Threading.CancellationToken)~System.Threading.Tasks.Task{Microsoft.AspNetCore.Mvc.ActionResult}")]
[assembly: SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "By design", Scope = "member", Target = "~M:CommandCenter.StringExtensions.IsValidEmail(System.String)~System.Boolean")]
[assembly: SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "By design", Scope = "member", Target = "~M:CommandCenter.Program.Main(System.String[])~System.Int32")]
