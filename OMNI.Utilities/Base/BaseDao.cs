﻿using OMNI.Utilities.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace OMNI.Utilities.Base
{
    public class BaseDao
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [StringLength(100)]
        public string CreatedBy { get; set; }

        [StringLength(400)]
        public string UpdatedBy { get; set; }

        [StringLength(1)]
        public string IsDeleted { get; set; } = GeneralConstants.NO;

        [StringLength(1)]
        public string IsActive { get; set; } = GeneralConstants.YES;
    }
}
