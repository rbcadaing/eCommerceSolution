﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Dto;

public record UserDTO(Guid UserID,string? Email,string? PersonName, string? Gender);
