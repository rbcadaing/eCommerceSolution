﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO;

public record ProductDTO(Guid ProductID, string? ProductName, int Category, double UnitPrice, int QuantityInStock);
