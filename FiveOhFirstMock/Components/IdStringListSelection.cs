using FiveOhFirstMock.Data;
using FiveOhFirstMock.Data.Enums;
using FiveOhFirstMock.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace FiveOhFirstMock.Components
{
    public class IdStringListSelect : InputBase<List<Trooper>>
    {
        [Parameter]
        public IServiceProvider Services { get; set; }
        [Parameter]
        public IRefreshRequestService Refresh { get; set; }
        private string DisplayValue { get; set; } = "";
        private List<string> InvalidSearches { get; set; } = new();

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var scope = Services.CreateScope();
            var manager = scope.ServiceProvider.GetRequiredService<UserManager<Trooper>>();

            builder.OpenElement(0, "div");
            builder.OpenElement(1, "input");
            builder.AddMultipleAttributes(2, AdditionalAttributes);
            builder.AddAttribute(3, "class", CssClass);
            builder.AddAttribute(4, "value", DisplayValue);
            builder.AddAttribute(5, "type", "text");
            builder.AddAttribute(6, "oninput", EventCallback.Factory.Create(this, async (x) =>
            {
                DisplayValue = ((string?)x.Value) ?? "";

                if(DisplayValue?.EndsWith(' ') ?? false)
                {
                    DisplayValue = DisplayValue.Trim();
                    Trooper t;
                    if (int.TryParse(DisplayValue, out _))
                    {
                        t = await manager.FindByIdAsync(DisplayValue);
                    }
                    else
                    {
                        t = await manager.FindByNameAsync(DisplayValue);
                    }

                    if(t is not null)
                    {
                        if (CurrentValue is null) CurrentValue = new();
                        
                        if(CurrentValue.FirstOrDefault(x => x.Id == t.Id) is null)
                            CurrentValue.Add(t);
                    }
                    else
                    {
                        InvalidSearches.Add(DisplayValue);
                    }

                    DisplayValue = "";

                    Refresh.CallRequestRefresh();
                }
            }));
            builder.CloseElement();

            builder.OpenElement(7, "div");
            if (CurrentValue is not null)
            {
                for(int i = 0; i < CurrentValue.Count; i++)
                {
                    builder.OpenElement(8, "span");

                    builder.AddAttribute(8, "class", "btn btn-outline-success m-1");
                    builder.AddContent(9, CurrentValue[i].UserName);

                    builder.OpenElement(10, "button");
                    builder.AddAttribute(11, "class", "oi oi-circle-x btn");

                    builder.AddAttribute(12, "onclick", GetRemoveFromCurrentEvent(i));

                    builder.CloseElement();
                    builder.CloseElement();
                }

                for(int i = 0; i < InvalidSearches.Count; i++)
                {
                    builder.OpenElement(8, "span");

                    builder.AddAttribute(8, "class", "btn btn-outline-danger m-1");
                    builder.AddContent(9, InvalidSearches[i]);

                    builder.OpenElement(10, "button");
                    builder.AddAttribute(11, "class", "oi oi-circle-x btn");
                    builder.AddAttribute(12, "onclick", GetRemoveFromBadEvent(i));

                    builder.CloseElement();
                    builder.CloseElement();
                }
            }

            builder.CloseElement();
            builder.CloseElement();
        }

        protected EventCallback GetRemoveFromCurrentEvent(int toRemove)
        {
            return EventCallback.Factory.Create(this, (x) =>
            {
                if (CurrentValue is not null)
                {
                    CurrentValue.RemoveAt(toRemove);
                    Refresh.CallRequestRefresh();
                }
            });
        }

        protected EventCallback GetRemoveFromBadEvent(int toRemove)
        {
            return EventCallback.Factory.Create(this, (x) =>
            {
                if (InvalidSearches is not null)
                {
                    InvalidSearches.RemoveAt(toRemove);
                    Refresh.CallRequestRefresh();
                }
            });
        }

        protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out List<Trooper> result, [NotNullWhen(false)] out string? validationErrorMessage)
        {
            if(value is null)
            {
                result = CurrentValue ?? new();
                validationErrorMessage = null;
                return true;
            }

            if (CurrentValue is null)
                CurrentValue = new();

            var parts = value.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            List<Trooper> troopers = new();



            //CurrentValue.AddRange();
            result = CurrentValue;
            validationErrorMessage = null;
            return true;
        }

        protected override string? FormatValueAsString(List<Trooper>? value)
        {
            return base.FormatValueAsString(value);
        }
    }
}
