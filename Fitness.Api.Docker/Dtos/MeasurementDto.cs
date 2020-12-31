// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************

namespace Fitness.Api.Dtos
{
    public class MeasurementDto
    {
        public long MeasurementId { get; set; }
        public float Weight { get; set; }
        public int Unit { get; set; }
        public int Neck { get; set; }
        public int Chest { get; set; }
        public int Waist { get; set; }
        public int Arm { get; set; }
        public int Hip { get; set; }
        public int Leg { get; set; }
        public long UserId { get; set; }
    }
}
