﻿using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.API
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Conflict
    {
        public object CurrentValue { get; set; }
        public object SentValue { get; set; }
    }

    public class ConcurrencyProblemDetails : StatusCodeProblemDetails
    {
        public Dictionary<string, Conflict> Conflicts { get; }

        public ConcurrencyProblemDetails(DbUpdateConcurrencyException ex)
            :base(StatusCodes.Status409Conflict)
        {
            Conflicts = new Dictionary<string, Conflict>();
            var entry = ex.Entries[0];
            var props = entry.Properties.Where(p => !p.Metadata.IsConcurrencyToken).ToArray();
            var currentValues = props.ToDictionary(p => p.Metadata.Name, p => p.CurrentValue);

            entry.Reload();

            foreach (var property in props)
            {
                if (!currentValues[property.Metadata.Name].Equals(property.CurrentValue))
                {
                    Conflicts[property.Metadata.Name] = new Conflict
                    {
                        CurrentValue = property.CurrentValue,
                        SentValue = currentValues[property.Metadata.Name]
                    };
                }
            }
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member