using FiveOhFirstMock.Data;
using FiveOhFirstMock.Data.Enums;
using FiveOhFirstMock.Services;
using Humanizer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FiveOhFirstMock.Components
{
    public class FlagInputSelect : InputBase<Qualifications>
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {

            builder.OpenElement(0, "ul");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", CssClass);
            builder.AddAttribute(3, "class", "list-group");
            //builder.AddAttribute(4, "value", BindConverter.FormatValue(CurrentValueAsString));
            var converter = TypeDescriptor.GetConverter(typeof(Qualifications));
            //builder.AddAttribute(5, "onchange", EventCallback.Factory.CreateBinder<TEnum>(this, value =>
            //{
            //    CurrentValueAsString = converter.ConvertToString(value);
            //}, (TEnum)converter.ConvertFromString(CurrentValueAsString), null));

            // Add an option element per enum value

            var enumType = typeof(Qualifications);
            var current = Convert.ToInt64(CurrentValue);
            foreach (Qualifications value in Enum.GetValues(enumType))
            {
                var intVal = Convert.ToInt32(value);
                builder.OpenElement(4, "li");
                builder.AddAttribute(5, "class", $"list-group-item d-flex justify-content-between align-items-center" +
                    $" {((current & intVal) == intVal ? "bg-primary text-light" : "")}");
                builder.AddAttribute(6, "onclick", EventCallback.Factory.Create(this, (x) =>
                {
                    if ((current & intVal) == intVal)
                        current ^= intVal;
                    else current |= intVal;

                    CurrentValue = (Qualifications)current;
                }));
                builder.AddContent(12, GetDisplayName(value));
                builder.CloseElement();
            }

            builder.CloseElement(); // close the select element
        }

        protected override bool TryParseValueFromString(string value, out Qualifications result, out string validationErrorMessage)
        {
            // Let's Blazor convert the value for us 😊
            if (BindConverter.TryConvertTo(value, CultureInfo.CurrentCulture, out Qualifications parsedValue))
            {
                result = parsedValue;
                validationErrorMessage = null;
                return true;
            }

            // Map null/empty value to null if the bound object is nullable
            if (string.IsNullOrEmpty(value))
            {
                var nullableType = Nullable.GetUnderlyingType(typeof(Qualifications));
                if (nullableType != null)
                {
                    result = default;
                    validationErrorMessage = null;
                    return true;
                }
            }

            // The value is invalid => set the error message
            result = default;
            validationErrorMessage = $"The {FieldIdentifier.FieldName} field is not valid.";
            return false;
        }

        private string GetDisplayName(Qualifications value)
        {
            // Read the Display attribute name
            var member = value.GetType().GetMember(value.ToString())[0];
            var displayAttribute = member.GetCustomAttribute<DisplayAttribute>();
            if (displayAttribute != null)
                return displayAttribute.GetName();

            // Require the NuGet package Humanizer.Core
            // <PackageReference Include = "Humanizer.Core" Version = "2.8.26" />
            return value.ToString().Humanize();
        }
    }
}
