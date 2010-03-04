﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarmVille.Game.Classes;

namespace FarmVille.Game.Objects
{
    [AMFConstructableObject("Building")]
    public class BuildingObject
        : PlantableObject
    {
        [AMF("buildTime")]
        private double? _buildTime;
    }
}
