﻿using FluentValidation;
using SyncTrayzor.Properties;
using System;

namespace SyncTrayzor.Pages.Settings
{
    public class SyncthingAddressValidator : AbstractValidator<SettingItem<string>>
    {
        public SyncthingAddressValidator()
        {
            RuleFor(x => x.Value).NotEmpty().WithMessage(Resources.SettingsView_Validation_NotShouldBeEmpty);
            RuleFor(x => x.Value).Must(str =>
            {
                // URI seems to think https://http://something is valid...
                if (str.StartsWith("http:") || str.StartsWith("https:"))
                    return false;

                str = "https://" + str;
                Uri uri;
                return Uri.TryCreate(str, UriKind.Absolute, out uri) && uri.IsWellFormedOriginalString();
            }).WithMessage(Resources.SettingsView_Validation_InvalidUrl);
        }
    }
}
