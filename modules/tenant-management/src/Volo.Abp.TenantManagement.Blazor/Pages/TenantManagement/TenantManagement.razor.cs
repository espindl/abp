﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.BlazoriseUI;
using Volo.Abp.FeatureManagement.Blazor.Components;
using Volo.Abp.TenantManagement.Localization;

namespace Volo.Abp.TenantManagement.Blazor.Pages.TenantManagement
{
    public partial class TenantManagement
    {
        protected const string FeatureProviderName = "T";

        protected bool HasManageConnectionStringsPermission;
        protected bool HasManageFeaturesPermission;
        protected string ManageConnectionStringsPolicyName;
        protected string ManageFeaturesPolicyName;

        protected FeatureManagementModal FeatureManagementModal;

        protected TenantInfoModel TenantInfo;

        public TenantManagement()
        {
            LocalizationResource = typeof(AbpTenantManagementResource);
            ObjectMapperContext = typeof(AbpTenantManagementBlazorModule);

            CreatePolicyName = TenantManagementPermissions.Tenants.Create;
            UpdatePolicyName = TenantManagementPermissions.Tenants.Update;
            DeletePolicyName = TenantManagementPermissions.Tenants.Delete;

            TenantInfo = new TenantInfoModel();
        }

        protected override async Task SetPermissionsAsync()
        {
            await base.SetPermissionsAsync();

            HasManageConnectionStringsPermission = await AuthorizationService.IsGrantedAsync(ManageConnectionStringsPolicyName);
            HasManageFeaturesPermission = await AuthorizationService.IsGrantedAsync(ManageFeaturesPolicyName);
        }

        protected override string GetDeleteConfirmationMessage(TenantDto entity)
        {
            return string.Format(L["TenantDeletionConfirmationMessage"], entity.Name);
        }
    }

    public class TenantInfoModel
    {
        public Guid Id { get; set; }

        public bool UseSharedDatabase { get; set; }

        [Required]
        public string DefaultConnectionString { get; set; }
    }
}
