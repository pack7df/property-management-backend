﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagement.Domain.DTO
{
    public class FilterHostRequest : BaseFilterRequest
    {
        public string? FullName { get; private set; } = string.Empty;
        public string? Email { get; private set; } = string.Empty;
        public string? Phone { get; private set; } = string.Empty;
    }
}
